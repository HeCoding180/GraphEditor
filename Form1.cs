using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GraphEditor
{
    public enum CoordinateState
    {
        Coordinates,
        CalStart,
        CalEnd,
        Units
    }

    public partial class Form1 : Form
    {
        public IntPtr hookId = IntPtr.Zero;

        // Colors
        readonly Color DarkBG = Color.FromArgb(26, 26, 26);
        readonly Color ObjectBG = Color.FromArgb(31, 31, 31);

        // AlignImageView sizes
        readonly Size DefaultAlignSize = new Size(205, 205);
        readonly Size LargeAlignSize = new Size(410, 410);

        // Image Directory
        string imageFilePath { set; get; }

        // Current Image
        Image currentImage { set; get; }
        bool ImageLoaded { set; get; }

        // Zoom view image
        Bitmap zoomBMP = new Bitmap(205, 205);

        CoordinateState CurrentCoordinateState;

        Point StartEdge { set; get; }
        Point EndEdge { set; get; }

        int GraphWidth
        {
            get
            {
                return Math.Abs(EndEdge.X - StartEdge.X);
            }
        }
        int GraphHeight
        {
            get
            {
                return Math.Abs(EndEdge.Y - StartEdge.Y);
            }
        }

        bool AllAxisParametersValid
        {
            get
            {
                return double.TryParse(xAxisStartValueBox.Text, out _)
                    && double.TryParse(xAxisEndValueBox.Text, out _)
                    && double.TryParse(yAxisStartValueBox.Text, out _)
                    && double.TryParse(yAxisEndValueBox.Text, out _);
            }
        }

        // Darkmode title bar
        // Source: https://stackoverflow.com/a/64927217
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        public const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        public Form1()
        {
            InitializeComponent();

            // Center Picture Box
            PicturePanel_Resize(this, new EventArgs());

            // Hide coordinate label and image align view
            CoordinateLabel.Visible = false;
            AlignImageView.Visible = false;

            // No image loaded yet
            ImageLoaded = false;

            CurrentCoordinateState = CoordinateState.Coordinates;
        }

        bool IsPointInGraphArea(Point testPoint)
        {
            bool isInXRange = testPoint.X >= Math.Min(StartEdge.X, EndEdge.X) && testPoint.X <= Math.Max(StartEdge.X, EndEdge.X);
            bool isInYRange = testPoint.Y >= Math.Min(StartEdge.Y, EndEdge.Y) && testPoint.Y <= Math.Max(StartEdge.Y, EndEdge.Y);

            return isInXRange && isInYRange;
        }

        Point PointToGraph(Point p)
        {
            return new Point(Math.Abs(p.X - StartEdge.X), Math.Abs(p.Y - StartEdge.Y));
        }

        // Source: https://stackoverflow.com/questions/374316/round-a-double-to-x-significant-figures
        static double RoundToSignificantDigits(double d, int digits)
        {
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale * Math.Round(d / scale, digits);
        }

        public IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if ((Keys)vkCode == Keys.LShiftKey || (Keys)vkCode == Keys.RShiftKey)
                {
                    if (wParam == (IntPtr)WM_KEYDOWN)
                    {
                        AlignImageView.Size = LargeAlignSize;
                    }
                    else if (wParam == (IntPtr)WM_KEYUP)
                    {
                        AlignImageView.Size = DefaultAlignSize;
                        AlignImageView.Location = new Point(12, splitContainer1.Size.Height - 12 - AlignImageView.Size.Height);
                    }
                }
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        private void mainPictureBox_DoubleClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save file path
                    imageFilePath = openFileDialog.FileName;
                    // Load image
                    currentImage = Image.FromFile(imageFilePath);
                    // Load into PictureBox
                    mainPictureBox.Image = currentImage;

                    // Enable controls
                    bSave.Enabled = true;
                    bCalibrate.Enabled = true;
                    ImageLoaded = true;

                    PicturePanel_Resize(this, new EventArgs());
                }
            }
        }

        private void PicturePanel_Resize(object sender, EventArgs e)
        {
            Point PictureBoxPosition = mainPictureBox.Location;

            if (PicturePanel.Width < mainPictureBox.Width)
            {
                PictureBoxPosition.X = 0;
            }
            else
            {
                PictureBoxPosition.X = PicturePanel.Width / 2 - mainPictureBox.Width / 2;
            }

            if (PicturePanel.Height < mainPictureBox.Height)
            {
                PictureBoxPosition.Y = 0;
            }
            else
            {
                PictureBoxPosition.Y = PicturePanel.Height / 2 - mainPictureBox.Height / 2;
            }

            mainPictureBox.Location = PictureBoxPosition;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            string directory = Path.GetDirectoryName(imageFilePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(imageFilePath);
            string extension = Path.GetExtension(imageFilePath);
            string editedFileName = $"{fileNameWithoutExtension}_edited{extension}";
            string editedFilePath = Path.Combine(directory, editedFileName);

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = directory;
                saveFileDialog.FileName = editedFileName;
                saveFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                saveFileDialog.Title = "Save Edited Image";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the file (example: save the PictureBox image)
                    currentImage.Save(saveFileDialog.FileName);
                }
            }
        }

        private void bCalibrate_Click(object sender, EventArgs e)
        {
            switch (CurrentCoordinateState)
            {
                case CoordinateState.Coordinates:
                case CoordinateState.Units:
                    CurrentCoordinateState = CoordinateState.CalStart;
                    break;
            }
        }

        private void mainPictureBox_MouseEnter(object sender, EventArgs e)
        {
            CoordinateLabel.Visible = true;
            if (ImageLoaded) AlignImageView.Visible = true;
        }

        private void mainPictureBox_MouseLeave(object sender, EventArgs e)
        {
            if ((CurrentCoordinateState == CoordinateState.Coordinates) || (CurrentCoordinateState == CoordinateState.Units))
            {
                CoordinateLabel.Visible = false;
                AlignImageView.Visible = false;
            }
            else if (CurrentCoordinateState == CoordinateState.CalStart)
            {
                CoordinateLabel.ForeColor = SystemColors.Control;
                CoordinateLabel.Text = "Select Edge Location with lowest Value";
            }
            else if (CurrentCoordinateState == CoordinateState.CalEnd)
            {
                CoordinateLabel.ForeColor = SystemColors.Control;
                CoordinateLabel.Text = "Select Edge Location with highest Value";
            }
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Update zoomed view
            if (ImageLoaded)
            {
                using (Graphics gfx = Graphics.FromImage(zoomBMP))
                using (Bitmap CurrentImage_BMP = new Bitmap(currentImage))
                {
                    // Fill background
                    using (Brush BGbrush = new SolidBrush(ObjectBG))
                    {
                        gfx.FillRectangle(BGbrush, 0, 0, zoomBMP.Width, zoomBMP.Height);
                    }

                    for (int x = 0; x < 41; x++)
                    {
                        int dx = x - 20;
                        int imageX = e.Location.X + dx;
                        // Check if x is within image
                        if ((imageX >= 0) && (imageX < currentImage.Width))
                        {
                            for (int y = 0; y < 41; y++)
                            {
                                int dy = y - 20;
                                int imageY = e.Location.Y + dy;

                                if ((imageY >= 0) && (imageY < currentImage.Height))
                                {
                                    using (Brush pixelBrush = new SolidBrush(CurrentImage_BMP.GetPixel(imageX, imageY)))
                                    {
                                        gfx.FillRectangle(pixelBrush, 5 * x, 5 * y, 5, 5);
                                    }
                                }
                            }
                        }
                    }

                    // Add crosshair
                    Rectangle[] OuterCrosshairRectangles =
                    {
                        // Top rect
                        new Rectangle(101, 0, 3, 99),
                        // Bottom rect
                        new Rectangle(101, 106, 3, 99),
                        // Left rect
                        new Rectangle(0, 101, 99, 3),
                        // Right rect
                        new Rectangle(106, 101, 99, 3)
                    };

                    using (Brush OuterCrosshairBrush = new SolidBrush(Color.Black))
                    {
                        gfx.FillRectangles(OuterCrosshairBrush, OuterCrosshairRectangles);
                    }

                    using (Pen InnerCrosshairPen = new Pen(Color.White))
                    {
                        // Top line
                        gfx.DrawLine(InnerCrosshairPen, 102.0f, 0.0f, 102.0f, 97.0f);
                        // Bottom line
                        gfx.DrawLine(InnerCrosshairPen, 102.0f, 107.0f, 102.0f, 204.0f);
                        // Left line
                        gfx.DrawLine(InnerCrosshairPen, 0.0f, 102.0f, 97.0f, 102.0f);
                        // Right line
                        gfx.DrawLine(InnerCrosshairPen, 107.0f, 102.0f, 204.0f, 102.0f);
                    }
                }

                // Update AlignImageView control
                AlignImageView.Image = zoomBMP;
            }

            // Update coordinate label
            switch (CurrentCoordinateState)
            {
                case CoordinateState.Coordinates:
                    CoordinateLabel.ForeColor = SystemColors.Control;
                    CoordinateLabel.Text = "X: " + e.Location.X.ToString() + " / Y: " + e.Location.Y.ToString();
                    break;
                case CoordinateState.CalStart:
                    CoordinateLabel.ForeColor = SystemColors.Control;
                    CoordinateLabel.Text = "Select Edge Location with lowest Value | X: " + e.Location.X.ToString() + xAxisUnitBox.Text + " / Y: " + e.Location.Y.ToString() + yAxisUnitBox.Text;
                    break;
                case CoordinateState.CalEnd:
                    CoordinateLabel.ForeColor = SystemColors.Control;
                    CoordinateLabel.Text = "Select Edge Location with highest Value | X: " + e.Location.X.ToString() + xAxisUnitBox.Text + " / Y: " + e.Location.Y.ToString() + yAxisUnitBox.Text;
                    break;
                case CoordinateState.Units:
                    if (AllAxisParametersValid)
                    {
                        try
                        {
                            if (IsPointInGraphArea(e.Location))
                            {
                                Point GraphRelativeCursorLocation = PointToGraph(e.Location);

                                // Calculate X data
                                double xValue = 0.0;
                                double xScale = (double)(GraphRelativeCursorLocation.X) / (double)GraphWidth;
                                if (xAxisLogarithmic.Checked)
                                {
                                    // X axis is logarithmic
                                    double xAxisStartValue = Math.Log10(double.Parse(xAxisStartValueBox.Text));
                                    double xAxisEndValue = Math.Log10(double.Parse(xAxisEndValueBox.Text));

                                    xValue = Math.Pow(10, xAxisStartValue + xScale * (xAxisEndValue - xAxisStartValue));
                                }
                                else
                                {
                                    // X axis is linear
                                    double xAxisStartValue = double.Parse(xAxisStartValueBox.Text);
                                    double xAxisEndValue = double.Parse(xAxisEndValueBox.Text);

                                    xValue = xScale * (xAxisEndValue - xAxisStartValue) + xAxisStartValue;
                                }

                                // Calculate Y data
                                double yValue = 0.0;
                                double yScale = (double)(GraphRelativeCursorLocation.Y) / (double)GraphHeight;
                                if (yAxisLogarithmic.Checked)
                                {
                                    // Y axis is logarithmic
                                    double yAxisStartValue = Math.Log10(double.Parse(yAxisStartValueBox.Text));
                                    double yAxisEndValue = Math.Log10(double.Parse(yAxisEndValueBox.Text));

                                    yValue = Math.Pow(10, yAxisStartValue + yScale * (yAxisEndValue - yAxisStartValue));
                                }
                                else
                                {
                                    // Y axis is linear
                                    double yAxisStartValue = double.Parse(yAxisStartValueBox.Text);
                                    double yAxisEndValue = double.Parse(yAxisEndValueBox.Text);

                                    yValue = yScale * (yAxisEndValue - yAxisStartValue) + yAxisStartValue;
                                }

                                // Update coordinate label
                                CoordinateLabel.ForeColor = SystemColors.Control;
                                CoordinateLabel.Text = "X: " + RoundToSignificantDigits(xValue, 3).ToString("G15") + xAxisUnitBox.Text + " / Y: " + RoundToSignificantDigits(yValue, 3).ToString("G15") + yAxisUnitBox.Text;
                            }
                            else
                            {
                                CoordinateLabel.ForeColor = SystemColors.Control;
                                CoordinateLabel.Text = "Outside of graph area!";
                            }
                        }
                        catch
                        {
                            CoordinateLabel.ForeColor = Color.Red;
                            CoordinateLabel.Text = "Calculation falied!";
                        }
                    }
                    else
                    {
                        // Axis parameters missing, update label
                        CoordinateLabel.ForeColor = Color.Red;
                        CoordinateLabel.Text = "Some axis parameters are missing!";
                    }
                    break;
            }
        }

        private void mainPictureBox_Click(object sender, EventArgs e)
        {
            switch (CurrentCoordinateState)
            {
                case CoordinateState.CalStart:
                    // Get the current mouse position relative to the picturebox
                    StartEdge = mainPictureBox.PointToClient(Cursor.Position);
                    // Update the current coordinate state
                    CurrentCoordinateState = CoordinateState.CalEnd;
                    break;
                case CoordinateState.CalEnd:
                    // Get the current mouse position relative to the picturebox
                    EndEdge = mainPictureBox.PointToClient(Cursor.Position);
                    // Update the current coordinate state
                    CurrentCoordinateState = CoordinateState.Units;
                    break;
            }
        }
    }
}