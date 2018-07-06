namespace IQBackOffice.Despeckle.Demo
{
    #region Usings

    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    #endregion

    public partial class DemoForm : Form
    {
        #region Private Fields

        private byte[,] _imageMatrix;

        private string _openedFilePath;

        #endregion Private Fields

        #region Public Constructors

        public DemoForm()
        {
            InitializeComponent();

            FilterButton.Enabled = false;
            FilterAgainButton.Enabled = false;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Display the given image on the given PictureBox object
        /// </summary>
        /// <param name="imageMatrix">
        ///     2D array that contains the image
        /// </param>
        /// <param name="pictureBox">
        ///     PictureBox object to display the image on it
        /// </param>
        public static void DisplayImage(byte[,] imageMatrix, PictureBox pictureBox)
        {
            // Create Image:
            var height = Filtering.GetMatrixHeight(imageMatrix);
            var width = Filtering.GetMatrixWidth(imageMatrix);

            var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            unsafe
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                var imageWidth = width * 3;
                var strideOffset = bitmapData.Stride - imageWidth;
                var pixelPtr = (byte*)bitmapData.Scan0;
                if (pixelPtr == null)
                    return;

                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        pixelPtr[0] = pixelPtr[1] = pixelPtr[2] = imageMatrix[y, x];
                        pixelPtr += 3;
                    }

                    pixelPtr += strideOffset;
                }

                bitmap.UnlockBits(bitmapData);
            }

            var zoomFactor = 1.0;
            var newSize = new Size((int)(bitmap.Width * zoomFactor), (int)(bitmap.Height * zoomFactor));
            var bmp = new Bitmap(bitmap, newSize);
            pictureBox.Image = bmp;
            pictureBox.SizeMode = PictureBoxSizeMode.Normal;
        }

        #endregion Public Methods

        #region Private Methods

        private Task<double> FilterImage(byte[,] imageMatrix, Sorting.SortType sortType, Filtering.FilterType filterType, int maxSize)
        {
            return Task.Run(() =>
                            {
                                var start = Environment.TickCount;
                                Filtering.DespeckleImage(imageMatrix, maxSize, sortType, filterType);
                                var end = Environment.TickCount;
                                DisplayImage(imageMatrix, FilteredPictureBox);
                                double time = end - start;
                                time /= 1000;
                                return time;
                            });
        }

        private async Task FilterImageAsync()
        {
            if (SortCombo.SelectedIndex == -1)
                SortCombo.SelectedIndex = (int)Sorting.SortType.NativeArraySort;

            var sortType = (Sorting.SortType)SortCombo.SelectedIndex;

            if (FilterTypeCombo.SelectedIndex == -1)
                FilterTypeCombo.SelectedIndex = (int)Filtering.FilterType.AdaptiveMedian;

            var filterType = (Filtering.FilterType)(FilterTypeCombo.SelectedIndex + 1);

            if (!int.TryParse(MaxSizeTextbox.Text, out var maxSize))
            {
                maxSize = 3;
                MaxSizeTextbox.Text = maxSize.ToString();
            }

            FilterButton.Enabled = false;
            FilterAgainButton.Enabled = false;

            ElapsedTimeLabel.Text = string.Empty;
            var result = await FilterImage(_imageMatrix, sortType, filterType, maxSize);
            ElapsedTimeLabel.Text = result.ToString(CultureInfo.InvariantCulture);
            ElapsedTimeLabel.Text += @" s";

            FilterButton.Enabled = true;
            FilterAgainButton.Enabled = true;
        }

        private async void OnFilterAgainButtonClick(object sender, EventArgs e)
        {
            if (_imageMatrix == null)
                return;

            await FilterImageAsync();
        }

        private async void OnFilterButtonClick(object sender, EventArgs e)
        {
            if (_openedFilePath == null)
                return;

            await FilterImageAsync();
        }

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
            _imageMatrix = Filtering.OpenImage(_openedFilePath);
            DisplayImage(_imageMatrix, OriginalPictureBox);

            FilterButton.Enabled = true;
        }

        private void OnQuitButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion Private Methods
    }
}