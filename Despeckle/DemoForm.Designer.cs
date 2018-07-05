namespace IQBackOffice.Despeckle
{
    partial class DemoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ViewGroupBox = new System.Windows.Forms.GroupBox();
            this.ImagesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.OriginalPictureBox = new System.Windows.Forms.PictureBox();
            this.FilteredPictureBox = new System.Windows.Forms.PictureBox();
            this.ElapsedTimeLabel = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.ControlGroupBox = new System.Windows.Forms.GroupBox();
            this.QuitButton = new System.Windows.Forms.Button();
            this.FilterAgainButton = new System.Windows.Forms.Button();
            this.FilterTypeCombo = new System.Windows.Forms.ComboBox();
            this.SortCombo = new System.Windows.Forms.ComboBox();
            this.MaxSizeTextbox = new System.Windows.Forms.TextBox();
            this.FilterButton = new System.Windows.Forms.Button();
            this.MaxSizeLabel = new System.Windows.Forms.Label();
            this.OpenImageButton = new System.Windows.Forms.Button();
            this.ViewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagesSplitContainer)).BeginInit();
            this.ImagesSplitContainer.Panel1.SuspendLayout();
            this.ImagesSplitContainer.Panel2.SuspendLayout();
            this.ImagesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilteredPictureBox)).BeginInit();
            this.ControlGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewGroupBox
            // 
            this.ViewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewGroupBox.Controls.Add(this.ImagesSplitContainer);
            this.ViewGroupBox.Location = new System.Drawing.Point(17, 5);
            this.ViewGroupBox.Name = "ViewGroupBox";
            this.ViewGroupBox.Size = new System.Drawing.Size(1221, 442);
            this.ViewGroupBox.TabIndex = 0;
            this.ViewGroupBox.TabStop = false;
            this.ViewGroupBox.Text = "View Box";
            // 
            // ImagesSplitContainer
            // 
            this.ImagesSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagesSplitContainer.IsSplitterFixed = true;
            this.ImagesSplitContainer.Location = new System.Drawing.Point(18, 18);
            this.ImagesSplitContainer.Name = "ImagesSplitContainer";
            // 
            // ImagesSplitContainer.Panel1
            // 
            this.ImagesSplitContainer.Panel1.Controls.Add(this.OriginalPictureBox);
            // 
            // ImagesSplitContainer.Panel2
            // 
            this.ImagesSplitContainer.Panel2.Controls.Add(this.FilteredPictureBox);
            this.ImagesSplitContainer.Size = new System.Drawing.Size(1188, 409);
            this.ImagesSplitContainer.SplitterDistance = 594;
            this.ImagesSplitContainer.SplitterWidth = 1;
            this.ImagesSplitContainer.TabIndex = 4;
            // 
            // OriginalPictureBox
            // 
            this.OriginalPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OriginalPictureBox.Location = new System.Drawing.Point(0, 0);
            this.OriginalPictureBox.Name = "OriginalPictureBox";
            this.OriginalPictureBox.Size = new System.Drawing.Size(592, 409);
            this.OriginalPictureBox.TabIndex = 0;
            this.OriginalPictureBox.TabStop = false;
            // 
            // FilteredPictureBox
            // 
            this.FilteredPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilteredPictureBox.Location = new System.Drawing.Point(0, 0);
            this.FilteredPictureBox.Name = "FilteredPictureBox";
            this.FilteredPictureBox.Size = new System.Drawing.Size(593, 409);
            this.FilteredPictureBox.TabIndex = 1;
            this.FilteredPictureBox.TabStop = false;
            // 
            // ElapsedTimeLabel
            // 
            this.ElapsedTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ElapsedTimeLabel.AutoSize = true;
            this.ElapsedTimeLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElapsedTimeLabel.Location = new System.Drawing.Point(937, 29);
            this.ElapsedTimeLabel.Name = "ElapsedTimeLabel";
            this.ElapsedTimeLabel.Size = new System.Drawing.Size(0, 20);
            this.ElapsedTimeLabel.TabIndex = 3;
            // 
            // TimeLabel
            // 
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.Location = new System.Drawing.Point(885, 29);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(48, 20);
            this.TimeLabel.TabIndex = 2;
            this.TimeLabel.Text = "Time:";
            // 
            // ControlGroupBox
            // 
            this.ControlGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlGroupBox.Controls.Add(this.QuitButton);
            this.ControlGroupBox.Controls.Add(this.ElapsedTimeLabel);
            this.ControlGroupBox.Controls.Add(this.FilterAgainButton);
            this.ControlGroupBox.Controls.Add(this.TimeLabel);
            this.ControlGroupBox.Controls.Add(this.FilterTypeCombo);
            this.ControlGroupBox.Controls.Add(this.SortCombo);
            this.ControlGroupBox.Controls.Add(this.MaxSizeTextbox);
            this.ControlGroupBox.Controls.Add(this.FilterButton);
            this.ControlGroupBox.Controls.Add(this.MaxSizeLabel);
            this.ControlGroupBox.Controls.Add(this.OpenImageButton);
            this.ControlGroupBox.Location = new System.Drawing.Point(17, 453);
            this.ControlGroupBox.Name = "ControlGroupBox";
            this.ControlGroupBox.Size = new System.Drawing.Size(1221, 75);
            this.ControlGroupBox.TabIndex = 1;
            this.ControlGroupBox.TabStop = false;
            this.ControlGroupBox.Text = "Control Box";
            // 
            // QuitButton
            // 
            this.QuitButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QuitButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuitButton.Location = new System.Drawing.Point(1100, 24);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(106, 31);
            this.QuitButton.TabIndex = 9;
            this.QuitButton.Text = "Quit";
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.OnQuitButtonClick);
            // 
            // FilterAgainButton
            // 
            this.FilterAgainButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterAgainButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterAgainButton.Location = new System.Drawing.Point(733, 24);
            this.FilterAgainButton.Name = "FilterAgainButton";
            this.FilterAgainButton.Size = new System.Drawing.Size(106, 31);
            this.FilterAgainButton.TabIndex = 8;
            this.FilterAgainButton.Text = "Filter Again";
            this.FilterAgainButton.UseVisualStyleBackColor = true;
            this.FilterAgainButton.Click += new System.EventHandler(this.OnFilterAgainButtonClick);
            // 
            // FilterTypeCombo
            // 
            this.FilterTypeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterTypeCombo.FormattingEnabled = true;
            this.FilterTypeCombo.Items.AddRange(new object[] {
            "1)Alpha-trim filter",
            "2)Adaptive median filter"});
            this.FilterTypeCombo.Location = new System.Drawing.Point(426, 44);
            this.FilterTypeCombo.Name = "FilterTypeCombo";
            this.FilterTypeCombo.Size = new System.Drawing.Size(179, 21);
            this.FilterTypeCombo.TabIndex = 7;
            // 
            // SortCombo
            // 
            this.SortCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SortCombo.FormattingEnabled = true;
            this.SortCombo.Items.AddRange(new object[] {
            "0)None",
            "1)Insertion Sort",
            "2)Selection Sort",
            "3)Bubble Sort",
            "4)Modified Bubble Sort",
            "5)Merge Sort",
            "6)Quick Sort",
            "7)Counting Sort",
            "8)Heap Sort"});
            this.SortCombo.Location = new System.Drawing.Point(426, 17);
            this.SortCombo.Name = "SortCombo";
            this.SortCombo.Size = new System.Drawing.Size(179, 21);
            this.SortCombo.TabIndex = 6;
            // 
            // MaxSizeTextbox
            // 
            this.MaxSizeTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MaxSizeTextbox.Location = new System.Drawing.Point(232, 30);
            this.MaxSizeTextbox.Name = "MaxSizeTextbox";
            this.MaxSizeTextbox.Size = new System.Drawing.Size(83, 20);
            this.MaxSizeTextbox.TabIndex = 4;
            // 
            // FilterButton
            // 
            this.FilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterButton.Location = new System.Drawing.Point(621, 24);
            this.FilterButton.Name = "FilterButton";
            this.FilterButton.Size = new System.Drawing.Size(106, 31);
            this.FilterButton.TabIndex = 1;
            this.FilterButton.Text = "Filter";
            this.FilterButton.UseVisualStyleBackColor = true;
            this.FilterButton.Click += new System.EventHandler(this.OnFilterButtonClick);
            // 
            // MaxSizeLabel
            // 
            this.MaxSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MaxSizeLabel.AutoSize = true;
            this.MaxSizeLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxSizeLabel.Location = new System.Drawing.Point(151, 30);
            this.MaxSizeLabel.Name = "MaxSizeLabel";
            this.MaxSizeLabel.Size = new System.Drawing.Size(74, 20);
            this.MaxSizeLabel.TabIndex = 3;
            this.MaxSizeLabel.Text = "Max Size:";
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenImageButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenImageButton.Location = new System.Drawing.Point(21, 24);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(106, 31);
            this.OpenImageButton.TabIndex = 0;
            this.OpenImageButton.Text = "Open Image";
            this.OpenImageButton.UseVisualStyleBackColor = true;
            this.OpenImageButton.Click += new System.EventHandler(this.OnOpenImageButtonClick);
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 544);
            this.Controls.Add(this.ControlGroupBox);
            this.Controls.Add(this.ViewGroupBox);
            this.Name = "DemoForm";
            this.Text = "Image Filters";
            this.ViewGroupBox.ResumeLayout(false);
            this.ImagesSplitContainer.Panel1.ResumeLayout(false);
            this.ImagesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImagesSplitContainer)).EndInit();
            this.ImagesSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilteredPictureBox)).EndInit();
            this.ControlGroupBox.ResumeLayout(false);
            this.ControlGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ViewGroupBox;
        private System.Windows.Forms.PictureBox FilteredPictureBox;
        private System.Windows.Forms.PictureBox OriginalPictureBox;
        private System.Windows.Forms.GroupBox ControlGroupBox;
        private System.Windows.Forms.TextBox MaxSizeTextbox;
        private System.Windows.Forms.Label MaxSizeLabel;
        private System.Windows.Forms.Button FilterButton;
        private System.Windows.Forms.Button OpenImageButton;
        private System.Windows.Forms.ComboBox SortCombo;
        private System.Windows.Forms.ComboBox FilterTypeCombo;
        private System.Windows.Forms.Label ElapsedTimeLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button FilterAgainButton;
        private System.Windows.Forms.SplitContainer ImagesSplitContainer;
        private System.Windows.Forms.Button QuitButton;
    }
}

