namespace GraphEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            PicturePanel = new Panel();
            AlignImageView = new PictureBox();
            mainPictureBox = new PictureBox();
            bCalibrate = new Button();
            bSave = new Button();
            PropertiesPanel = new Panel();
            yAxisEndValueBox = new TextBox();
            yAxisEndValueLabel = new Label();
            yAxisStartValueBox = new TextBox();
            yAxisStartValueLabel = new Label();
            xAxisEndValueBox = new TextBox();
            xAxisEndValueLabel = new Label();
            xAxisStartValueBox = new TextBox();
            xAxisStartValueLabel = new Label();
            yAxisLogarithmic = new CheckBox();
            xAxisLogarithmic = new CheckBox();
            yAxisLabel = new Label();
            yAxisUnitBox = new TextBox();
            yAxisUnitLabel = new Label();
            xAxisUnitBox = new TextBox();
            xAxisUnitLabel = new Label();
            xAxisLabel = new Label();
            CoordinateLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            PicturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AlignImageView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            PropertiesPanel.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.FromArgb(61, 61, 61);
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.FromArgb(26, 26, 26);
            splitContainer1.Panel1.Controls.Add(AlignImageView);
            splitContainer1.Panel1.Controls.Add(PicturePanel);
            splitContainer1.Panel1.ForeColor = SystemColors.Control;
            splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.FromArgb(31, 31, 31);
            splitContainer1.Panel2.Controls.Add(bCalibrate);
            splitContainer1.Panel2.Controls.Add(bSave);
            splitContainer1.Panel2.Controls.Add(PropertiesPanel);
            splitContainer1.Panel2MinSize = 200;
            splitContainer1.Size = new Size(784, 541);
            splitContainer1.SplitterDistance = 539;
            splitContainer1.TabIndex = 0;
            // 
            // PicturePanel
            // 
            PicturePanel.AutoScroll = true;
            PicturePanel.Controls.Add(mainPictureBox);
            PicturePanel.Dock = DockStyle.Fill;
            PicturePanel.Location = new Point(0, 0);
            PicturePanel.Name = "PicturePanel";
            PicturePanel.Size = new Size(539, 541);
            PicturePanel.TabIndex = 0;
            PicturePanel.Resize += PicturePanel_Resize;
            // 
            // AlignImageView
            // 
            AlignImageView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AlignImageView.BackColor = Color.FromArgb(31, 31, 31);
            AlignImageView.Location = new Point(11, 325);
            AlignImageView.Margin = new Padding(2);
            AlignImageView.Name = "AlignImageView";
            AlignImageView.Size = new Size(205, 205);
            AlignImageView.SizeMode = PictureBoxSizeMode.Zoom;
            AlignImageView.TabIndex = 3;
            AlignImageView.TabStop = false;
            // 
            // mainPictureBox
            // 
            mainPictureBox.BackColor = Color.FromArgb(31, 31, 31);
            mainPictureBox.Location = new Point(25, 25);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new Size(500, 500);
            mainPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            mainPictureBox.TabIndex = 1;
            mainPictureBox.TabStop = false;
            mainPictureBox.Click += mainPictureBox_Click;
            mainPictureBox.DoubleClick += mainPictureBox_DoubleClick;
            mainPictureBox.MouseEnter += mainPictureBox_MouseEnter;
            mainPictureBox.MouseLeave += mainPictureBox_MouseLeave;
            mainPictureBox.MouseMove += mainPictureBox_MouseMove;
            // 
            // bCalibrate
            // 
            bCalibrate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            bCalibrate.BackColor = Color.FromArgb(56, 56, 56);
            bCalibrate.Enabled = false;
            bCalibrate.FlatAppearance.BorderColor = Color.FromArgb(66, 66, 66);
            bCalibrate.FlatStyle = FlatStyle.Flat;
            bCalibrate.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bCalibrate.ForeColor = SystemColors.Control;
            bCalibrate.Location = new Point(12, 54);
            bCalibrate.Name = "bCalibrate";
            bCalibrate.Size = new Size(217, 30);
            bCalibrate.TabIndex = 1;
            bCalibrate.Text = "Select Graph Area";
            bCalibrate.UseVisualStyleBackColor = false;
            bCalibrate.Click += bCalibrate_Click;
            // 
            // bSave
            // 
            bSave.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            bSave.BackColor = Color.FromArgb(56, 56, 56);
            bSave.Enabled = false;
            bSave.FlatAppearance.BorderColor = Color.FromArgb(66, 66, 66);
            bSave.FlatStyle = FlatStyle.Flat;
            bSave.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bSave.ForeColor = SystemColors.Control;
            bSave.Location = new Point(12, 12);
            bSave.Name = "bSave";
            bSave.Size = new Size(217, 30);
            bSave.TabIndex = 0;
            bSave.Text = "Save Image As...";
            bSave.UseVisualStyleBackColor = false;
            bSave.Click += bSave_Click;
            // 
            // PropertiesPanel
            // 
            PropertiesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PropertiesPanel.Controls.Add(yAxisEndValueBox);
            PropertiesPanel.Controls.Add(yAxisEndValueLabel);
            PropertiesPanel.Controls.Add(yAxisStartValueBox);
            PropertiesPanel.Controls.Add(yAxisStartValueLabel);
            PropertiesPanel.Controls.Add(xAxisEndValueBox);
            PropertiesPanel.Controls.Add(xAxisEndValueLabel);
            PropertiesPanel.Controls.Add(xAxisStartValueBox);
            PropertiesPanel.Controls.Add(xAxisStartValueLabel);
            PropertiesPanel.Controls.Add(yAxisLogarithmic);
            PropertiesPanel.Controls.Add(xAxisLogarithmic);
            PropertiesPanel.Controls.Add(yAxisLabel);
            PropertiesPanel.Controls.Add(yAxisUnitBox);
            PropertiesPanel.Controls.Add(yAxisUnitLabel);
            PropertiesPanel.Controls.Add(xAxisUnitBox);
            PropertiesPanel.Controls.Add(xAxisUnitLabel);
            PropertiesPanel.Controls.Add(xAxisLabel);
            PropertiesPanel.Location = new Point(0, 90);
            PropertiesPanel.Name = "PropertiesPanel";
            PropertiesPanel.Size = new Size(241, 451);
            PropertiesPanel.TabIndex = 0;
            // 
            // yAxisEndValueBox
            // 
            yAxisEndValueBox.BackColor = Color.FromArgb(31, 31, 31);
            yAxisEndValueBox.BorderStyle = BorderStyle.None;
            yAxisEndValueBox.ForeColor = SystemColors.Control;
            yAxisEndValueBox.Location = new Point(79, 185);
            yAxisEndValueBox.MaxLength = 16;
            yAxisEndValueBox.Name = "yAxisEndValueBox";
            yAxisEndValueBox.PlaceholderText = "Y Axis End Value";
            yAxisEndValueBox.Size = new Size(160, 16);
            yAxisEndValueBox.TabIndex = 16;
            // 
            // yAxisEndValueLabel
            // 
            yAxisEndValueLabel.AutoSize = true;
            yAxisEndValueLabel.ForeColor = SystemColors.Control;
            yAxisEndValueLabel.Location = new Point(12, 185);
            yAxisEndValueLabel.Name = "yAxisEndValueLabel";
            yAxisEndValueLabel.Size = new Size(61, 15);
            yAxisEndValueLabel.TabIndex = 15;
            yAxisEndValueLabel.Text = "End Value:";
            // 
            // yAxisStartValueBox
            // 
            yAxisStartValueBox.BackColor = Color.FromArgb(31, 31, 31);
            yAxisStartValueBox.BorderStyle = BorderStyle.None;
            yAxisStartValueBox.ForeColor = SystemColors.Control;
            yAxisStartValueBox.Location = new Point(83, 165);
            yAxisStartValueBox.MaxLength = 16;
            yAxisStartValueBox.Name = "yAxisStartValueBox";
            yAxisStartValueBox.PlaceholderText = "Y Axis Start Value";
            yAxisStartValueBox.Size = new Size(156, 16);
            yAxisStartValueBox.TabIndex = 14;
            // 
            // yAxisStartValueLabel
            // 
            yAxisStartValueLabel.AutoSize = true;
            yAxisStartValueLabel.ForeColor = SystemColors.Control;
            yAxisStartValueLabel.Location = new Point(12, 165);
            yAxisStartValueLabel.Name = "yAxisStartValueLabel";
            yAxisStartValueLabel.Size = new Size(65, 15);
            yAxisStartValueLabel.TabIndex = 13;
            yAxisStartValueLabel.Text = "Start Value:";
            // 
            // xAxisEndValueBox
            // 
            xAxisEndValueBox.BackColor = Color.FromArgb(31, 31, 31);
            xAxisEndValueBox.BorderStyle = BorderStyle.None;
            xAxisEndValueBox.ForeColor = SystemColors.Control;
            xAxisEndValueBox.Location = new Point(79, 65);
            xAxisEndValueBox.MaxLength = 16;
            xAxisEndValueBox.Name = "xAxisEndValueBox";
            xAxisEndValueBox.PlaceholderText = "X Axis End Value";
            xAxisEndValueBox.Size = new Size(160, 16);
            xAxisEndValueBox.TabIndex = 12;
            // 
            // xAxisEndValueLabel
            // 
            xAxisEndValueLabel.AutoSize = true;
            xAxisEndValueLabel.ForeColor = SystemColors.Control;
            xAxisEndValueLabel.Location = new Point(12, 65);
            xAxisEndValueLabel.Name = "xAxisEndValueLabel";
            xAxisEndValueLabel.Size = new Size(61, 15);
            xAxisEndValueLabel.TabIndex = 11;
            xAxisEndValueLabel.Text = "End Value:";
            // 
            // xAxisStartValueBox
            // 
            xAxisStartValueBox.BackColor = Color.FromArgb(31, 31, 31);
            xAxisStartValueBox.BorderStyle = BorderStyle.None;
            xAxisStartValueBox.ForeColor = SystemColors.Control;
            xAxisStartValueBox.Location = new Point(83, 45);
            xAxisStartValueBox.MaxLength = 16;
            xAxisStartValueBox.Name = "xAxisStartValueBox";
            xAxisStartValueBox.PlaceholderText = "X Axis Start Value";
            xAxisStartValueBox.Size = new Size(156, 16);
            xAxisStartValueBox.TabIndex = 10;
            // 
            // xAxisStartValueLabel
            // 
            xAxisStartValueLabel.AutoSize = true;
            xAxisStartValueLabel.ForeColor = SystemColors.Control;
            xAxisStartValueLabel.Location = new Point(12, 45);
            xAxisStartValueLabel.Name = "xAxisStartValueLabel";
            xAxisStartValueLabel.Size = new Size(65, 15);
            xAxisStartValueLabel.TabIndex = 9;
            xAxisStartValueLabel.Text = "Start Value:";
            // 
            // yAxisLogarithmic
            // 
            yAxisLogarithmic.AutoSize = true;
            yAxisLogarithmic.ForeColor = SystemColors.Control;
            yAxisLogarithmic.Location = new Point(12, 205);
            yAxisLogarithmic.Name = "yAxisLogarithmic";
            yAxisLogarithmic.Size = new Size(136, 19);
            yAxisLogarithmic.TabIndex = 8;
            yAxisLogarithmic.Text = "Y Axis is Logarithmic";
            yAxisLogarithmic.UseVisualStyleBackColor = true;
            // 
            // xAxisLogarithmic
            // 
            xAxisLogarithmic.AutoSize = true;
            xAxisLogarithmic.ForeColor = SystemColors.Control;
            xAxisLogarithmic.Location = new Point(12, 85);
            xAxisLogarithmic.Name = "xAxisLogarithmic";
            xAxisLogarithmic.Size = new Size(136, 19);
            xAxisLogarithmic.TabIndex = 7;
            xAxisLogarithmic.Text = "X Axis is Logarithmic";
            xAxisLogarithmic.UseVisualStyleBackColor = true;
            // 
            // yAxisLabel
            // 
            yAxisLabel.AutoSize = true;
            yAxisLabel.ForeColor = SystemColors.Control;
            yAxisLabel.Location = new Point(12, 120);
            yAxisLabel.Name = "yAxisLabel";
            yAxisLabel.Size = new Size(95, 15);
            yAxisLabel.TabIndex = 6;
            yAxisLabel.Text = "Y Axis Properties";
            // 
            // yAxisUnitBox
            // 
            yAxisUnitBox.BackColor = Color.FromArgb(31, 31, 31);
            yAxisUnitBox.BorderStyle = BorderStyle.None;
            yAxisUnitBox.ForeColor = SystemColors.Control;
            yAxisUnitBox.Location = new Point(50, 145);
            yAxisUnitBox.MaxLength = 16;
            yAxisUnitBox.Name = "yAxisUnitBox";
            yAxisUnitBox.PlaceholderText = "Y Axis Unit";
            yAxisUnitBox.Size = new Size(189, 16);
            yAxisUnitBox.TabIndex = 5;
            // 
            // yAxisUnitLabel
            // 
            yAxisUnitLabel.AutoSize = true;
            yAxisUnitLabel.ForeColor = SystemColors.Control;
            yAxisUnitLabel.Location = new Point(12, 145);
            yAxisUnitLabel.Name = "yAxisUnitLabel";
            yAxisUnitLabel.Size = new Size(32, 15);
            yAxisUnitLabel.TabIndex = 4;
            yAxisUnitLabel.Text = "Unit:";
            // 
            // xAxisUnitBox
            // 
            xAxisUnitBox.BackColor = Color.FromArgb(31, 31, 31);
            xAxisUnitBox.BorderStyle = BorderStyle.None;
            xAxisUnitBox.ForeColor = SystemColors.Control;
            xAxisUnitBox.Location = new Point(50, 25);
            xAxisUnitBox.MaxLength = 16;
            xAxisUnitBox.Name = "xAxisUnitBox";
            xAxisUnitBox.PlaceholderText = "X Axis Unit";
            xAxisUnitBox.Size = new Size(189, 16);
            xAxisUnitBox.TabIndex = 3;
            // 
            // xAxisUnitLabel
            // 
            xAxisUnitLabel.AutoSize = true;
            xAxisUnitLabel.ForeColor = SystemColors.Control;
            xAxisUnitLabel.Location = new Point(12, 25);
            xAxisUnitLabel.Name = "xAxisUnitLabel";
            xAxisUnitLabel.Size = new Size(32, 15);
            xAxisUnitLabel.TabIndex = 2;
            xAxisUnitLabel.Text = "Unit:";
            // 
            // xAxisLabel
            // 
            xAxisLabel.AutoSize = true;
            xAxisLabel.ForeColor = SystemColors.Control;
            xAxisLabel.Location = new Point(12, 0);
            xAxisLabel.Name = "xAxisLabel";
            xAxisLabel.Size = new Size(95, 15);
            xAxisLabel.TabIndex = 0;
            xAxisLabel.Text = "X Axis Properties";
            // 
            // CoordinateLabel
            // 
            CoordinateLabel.AutoSize = true;
            CoordinateLabel.BackColor = Color.FromArgb(26, 26, 26);
            CoordinateLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CoordinateLabel.ForeColor = SystemColors.Control;
            CoordinateLabel.Location = new Point(0, 0);
            CoordinateLabel.Name = "CoordinateLabel";
            CoordinateLabel.Size = new Size(64, 17);
            CoordinateLabel.TabIndex = 2;
            CoordinateLabel.Text = "X: 0 / Y: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(784, 541);
            Controls.Add(CoordinateLabel);
            Controls.Add(splitContainer1);
            MinimumSize = new Size(498, 293);
            Name = "Form1";
            Text = "Graph Editor";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            PicturePanel.ResumeLayout(false);
            PicturePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AlignImageView).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            PropertiesPanel.ResumeLayout(false);
            PropertiesPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel PicturePanel;
        private PictureBox mainPictureBox;
        private Panel PropertiesPanel;
        private Button bSave;
        private Label xAxisLabel;
        private Label xAxisUnitLabel;
        private TextBox xAxisUnitBox;
        private TextBox yAxisUnitBox;
        private Label yAxisUnitLabel;
        private Label CoordinateLabel;
        private Label yAxisLabel;
        private Button bCalibrate;
        private CheckBox xAxisLogarithmic;
        private CheckBox yAxisLogarithmic;
        private Label xAxisStartValueLabel;
        private TextBox xAxisStartValueBox;
        private TextBox xAxisEndValueBox;
        private Label xAxisEndValueLabel;
        private TextBox yAxisEndValueBox;
        private Label yAxisEndValueLabel;
        private TextBox yAxisStartValueBox;
        private Label yAxisStartValueLabel;
        private PictureBox AlignImageView;
    }
}
