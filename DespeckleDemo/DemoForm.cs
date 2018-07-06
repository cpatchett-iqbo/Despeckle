namespace IQBackOffice.Despeckle.Demo
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();

            FilterButton.Enabled = false;
            FilterAgainButton.Enabled = false;
        }

        private byte[,] _imageMatrix;
        private string _openedFilePath;

        private void OnOpenImageButtonClick(object sender, EventArgs e)
        {
            var oldFilterButtonEnabled = FilterButton.Enabled;
            var oldFilterAgainButtonEnabled = FilterAgainButton.Enabled;
            FilterButton.Enabled = false;
            FilterAgainButton.Enabled = false;
            ElapsedTimeLabel.Text = string.Empty;

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                FilterButton.Enabled = oldFilterButtonEnabled;
                FilterAgainButton.Enabled = oldFilterAgainButtonEnabled;
                return;
            }

            //Open the browsed image and display it
            _openedFilePath = openFileDialog.FileName;
            _imageMatrix = ImageOperations.OpenImage(_openedFilePath);
            ImageOperations.DisplayImage(_imageMatrix, OriginalPictureBox);

            FilterButton.Enabled = true;
        }

        private async void OnFilterButtonClick(object sender, EventArgs e)
        {
            if (_openedFilePath == null)
                return;

            await FilterImageAsync();
        }

        private async void OnFilterAgainButtonClick(object sender, EventArgs e)
        {
            if (_imageMatrix == null)
                return;

            await FilterImageAsync();
        }

        private async Task FilterImageAsync()
        {
            var sort = SortCombo.SelectedIndex;
            if (sort == -1)
            {
                SortCombo.SelectedIndex = 10;
                sort = 10;
            }

            var filter = FilterTypeCombo.SelectedIndex;
            if (filter == -1)
            {
                FilterTypeCombo.SelectedIndex = 1;
                filter = 2;
            }

            if (!int.TryParse(MaxSizeTextbox.Text, out var maxSize))
            {
                maxSize = 3;
                MaxSizeTextbox.Text = maxSize.ToString();
            }

            FilterButton.Enabled = false;
            FilterAgainButton.Enabled = false;

            ElapsedTimeLabel.Text = string.Empty;
            var result = await FilterImage(_imageMatrix, sort, filter, maxSize);
            ElapsedTimeLabel.Text = result.ToString(CultureInfo.InvariantCulture);
            ElapsedTimeLabel.Text += @" s";

            FilterButton.Enabled = true;
            FilterAgainButton.Enabled = true;
        }

        private Task<double> FilterImage(byte[,] imageMatrix, int sort, int filter, int maxSize)
        {
            return Task.Run(() =>
                            {
                                var start = Environment.TickCount;

                                ImageOperations.DespeckleImage(imageMatrix, maxSize, sort, filter);

                                var end = Environment.TickCount;
                                ImageOperations.DisplayImage(imageMatrix, FilteredPictureBox);
                                double time = end - start;
                                time /= 1000;
                                return time;
                            });
        }

        private void OnQuitButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
