using System.Runtime.InteropServices;

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
        // Image Directory
        string imageFilePath { set; get; }

        // Current Image
        Image currentImage { set; get; }

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

            // Hide Coordinate Label
            CoordinateLabel.Visible = false;

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

        private void mainPictureBox_DoubleClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageFilePath = openFileDialog.FileName; // Save file path
                    currentImage = Image.FromFile(imageFilePath); // Load image
                    mainPictureBox.Image = currentImage; // Load into PictureBox
                    bSave.Enabled = true;
                    bCalibrate.Enabled = true;
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
        }

        private void mainPictureBox_MouseLeave(object sender, EventArgs e)
        {
            if ((CurrentCoordinateState == CoordinateState.Coordinates) || (CurrentCoordinateState == CoordinateState.Units))
            {
                CoordinateLabel.Visible = false;
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
                                double xAxisStartValue = double.Parse(xAxisStartValueBox.Text);
                                double xAxisEndValue = double.Parse(xAxisEndValueBox.Text);

                                double xValue = 0.0;
                                double xScale = (double)(GraphRelativeCursorLocation.X) / (double)GraphWidth;
                                if (xAxisLogarithmic.Checked)
                                {
                                    // X axis is logarithmic

                                }
                                else
                                {
                                    // X axis is linear
                                    xValue = xScale * (xAxisEndValue - xAxisStartValue) + xAxisStartValue;
                                }

                                // Calculate Y data
                                double yAxisStartValue = double.Parse(yAxisStartValueBox.Text);
                                double yAxisEndValue = double.Parse(yAxisEndValueBox.Text);

                                double yValue = 0.0;
                                double yScale = (double)(GraphRelativeCursorLocation.Y) / (double)GraphHeight;
                                if (yAxisLogarithmic.Checked)
                                {
                                    // Y axis is logarithmic

                                }
                                else
                                {
                                    // Y axis is linear
                                    yValue = yScale * (yAxisEndValue - yAxisStartValue) + yAxisStartValue;
                                }

                                // Update coordinate label
                                CoordinateLabel.ForeColor = SystemColors.Control;
                                CoordinateLabel.Text = "X: " + RoundToSignificantDigits(xValue, 3).ToString() + xAxisUnitBox.Text + " / Y: " + RoundToSignificantDigits(yValue, 3).ToString() + yAxisUnitBox.Text;
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
