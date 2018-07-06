namespace IQBackOffice.Despeckle
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
        }

        private byte[,] _imageMatrix;
        private string _openedFilePath;

        private void OnOpenImageButtonClick(object sender, EventArgs e)
        {
            ElapsedTimeLabel.Text = string.Empty;
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            //Open the browsed image and display it
            _openedFilePath = openFileDialog.FileName;
            _imageMatrix = ImageOperations.OpenImage(_openedFilePath);
            ImageOperations.DisplayImage(_imageMatrix, OriginalPictureBox);
        }

        private void OnFilterButtonClick(object sender, EventArgs e)
        {
            if (_openedFilePath == null)
                return;

            FilterImage(_imageMatrix);
        }

        private void OnFilterAgainButtonClick(object sender, EventArgs e)
        {
            if (_imageMatrix == null)
                return;

            FilterImage(_imageMatrix);
        }

        private void FilterImage(byte[,] imageMatrix)
        {
            ElapsedTimeLabel.Text = string.Empty;

            var sort = SortCombo.SelectedIndex >= 0 ? SortCombo.SelectedIndex : 0;
            var filter = FilterTypeCombo.SelectedIndex >= 0 ? FilterTypeCombo.SelectedIndex + 1 : 1;
            if (!int.TryParse(MaxSizeTextbox.Text, out var maxSize))
                maxSize = 1;

            var start = Environment.TickCount;

            ImageOperations.DespeckleImage(imageMatrix, maxSize, sort, filter);

            var end = Environment.TickCount;
            ImageOperations.DisplayImage(imageMatrix, FilteredPictureBox);
            double time = end - start;
            time /= 1000;
            ElapsedTimeLabel.Text = time.ToString(CultureInfo.InvariantCulture);
            ElapsedTimeLabel.Text += @" s";
        }

        private void OnQuitButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
