﻿namespace IQBackOffice.Despeckle
{
    #region Usings

    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Windows.Forms;

    #endregion

    public class ImageOperations
    {
        /// <summary>
        ///     Open an image, convert it to gray scale and load it into 2D array of size (Height x Width)
        /// </summary>
        /// <param name="imagePath">
        ///     Image file path
        /// </param>
        /// <returns>
        ///     2D array of gray values
        /// </returns>
        public static byte[,] OpenImage(string imagePath)
        {
            var originalImage = new Bitmap(imagePath);
            var height = originalImage.Height;
            var width = originalImage.Width;

            var buffer = new byte[height, width];

            unsafe
            {
                var bitmapData = originalImage.LockBits(new Rectangle(0, 0, width, height),
                                                        ImageLockMode.ReadWrite,
                                                        originalImage.PixelFormat);
                int x, y;
                int byteWidth;
                int bitsPerPixel;

                switch (originalImage.PixelFormat)
                {
                    case PixelFormat.Format24bppRgb:

                        bitsPerPixel = 24;
                        byteWidth = width * 3;
                        break;

                    case PixelFormat.Format32bppArgb:
                    case PixelFormat.Format32bppRgb:
                    case PixelFormat.Format32bppPArgb:

                        bitsPerPixel = 32;
                        byteWidth = width * 4;
                        break;

                    case PixelFormat.Format8bppIndexed:

                        bitsPerPixel = 8;
                        byteWidth = width;
                        break;

                    case PixelFormat.Format1bppIndexed:

                        bitsPerPixel = 1;
                        byteWidth = width / 8;
                        byteWidth += width % 8 == 0 ? 0 : 1;
                        break;

                    default:

                        // TODO: Throw up error dialog.
                        return null;
                }

                var strideOffset = bitmapData.Stride - byteWidth;
                var pixelPtr = (byte*)bitmapData.Scan0;
                if (pixelPtr == null)
                    return null;

                for (y = 0; y < height; y++)
                {
                    byte bitMask = 128;
                    for (x = 0; x < width; x++)
                    {
                        if (bitsPerPixel == 1)
                        {
                            // More complicated than the others
                            buffer[y, x] = (pixelPtr[0] & bitMask) == 0 ? (byte)0 : (byte)255;

                            bitMask >>= 1;
                            if (bitMask == 0)
                            {
                                bitMask = 128;
                                pixelPtr++;
                            }

                            continue;
                        }

                        buffer[y, x] = bitsPerPixel == 8 ? pixelPtr[0] : (byte)((pixelPtr[0] + pixelPtr[1] + pixelPtr[2]) / 3);
                        pixelPtr += bitsPerPixel == 8 ? 1 : (bitsPerPixel == 24 ? 3 : 4);
                    }

                    if (bitsPerPixel == 1 && bitMask != 128)
                        pixelPtr++;

                    pixelPtr += strideOffset;
                }

                originalImage.UnlockBits(bitmapData);
            }

            return buffer;
        }

        /// <summary>
        ///     Get the height of the image
        /// </summary>
        /// <param name="imageMatrix">
        ///     2D array that contains the image
        /// </param>
        /// <returns>
        ///     Image Height
        /// </returns>
        public static int GetHeight(byte[,] imageMatrix)
        {
            return imageMatrix.GetLength(0);
        }

        /// <summary>
        ///     Get the width of the image
        /// </summary>
        /// <param name="imageMatrix">
        ///     2D array that contains the image
        /// </param>
        /// <returns>
        ///     Image Width
        /// </returns>
        public static int GetWidth(byte[,] imageMatrix)
        {
            return imageMatrix.GetLength(1);
        }

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
            var height = GetHeight(imageMatrix);
            var width = GetWidth(imageMatrix);

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

        public static byte AlphaTrimFilter(byte[,] imageMatrix, int x, int y, int maxSize, int sort)
        {
            byte[] array;
            int[] dx, dy;
            if (maxSize % 2 != 0)
            {
                array = new byte[maxSize * maxSize];
                dx = new int[maxSize * maxSize];
                dy = new int[maxSize * maxSize];
            }
            else
            {
                array = new byte[(maxSize + 1) * (maxSize + 1)];
                dx = new int[(maxSize + 1) * (maxSize + 1)];
                dy = new int[(maxSize + 1) * (maxSize + 1)];
            }

            var index = 0;
            for (var yShift = -(maxSize / 2); yShift <= maxSize / 2; yShift++)
            {
                for (var xShift = -(maxSize / 2); xShift <= maxSize / 2; xShift++)
                {
                    dx[index] = xShift;
                    dy[index] = yShift;
                    index++;
                }
            }

            var sum = 0;
            byte max = 0;
            byte min = 255;
            var arrayLength = 0;
            var imageWidth = GetWidth(imageMatrix);
            var imageHeight = GetHeight(imageMatrix);
            for (var i = 0; i < maxSize * maxSize; i++)
            {
                var newY = y + dy[i];
                var newX = x + dx[i];
                if (newX < 0 || newX >= imageWidth || newY < 0 || newY >= imageHeight)
                    continue;

                array[arrayLength] = imageMatrix[newY, newX];
                if (array[arrayLength] > max)
                    max = array[arrayLength];

                if (array[arrayLength] < min)
                    min = array[arrayLength];

                sum += array[arrayLength];
                arrayLength++;
            }

            sum -= max;
            sum -= min;
            arrayLength -= 2;
            var avg = sum / arrayLength;
            return (byte)avg;
        }

        public static byte AdaptiveMedianFilter(byte[,] imageMatrix, int x, int y, int windowSize = 3, int maxWindowSize = 5, int sortType = 10)
        {
            while (true)
            {
                var windowArraySize = windowSize * windowSize;
                var array = new byte[windowArraySize];
                var dx = new int[windowArraySize];
                var dy = new int[windowArraySize];
                var halfWindowSize = windowSize / 2;
                var index = 0;
                for (var fromCenterY = -halfWindowSize; fromCenterY <= halfWindowSize; fromCenterY++)
                {
                    for (var fromCenterX = -halfWindowSize; fromCenterX <= halfWindowSize; fromCenterX++)
                    {
                        dx[index] = fromCenterX;
                        dy[index] = fromCenterY;
                        index++;
                    }
                }

                byte maxValue = 0;
                byte minValue = 255;
                var arrayLength = 0;
                var currentValue = imageMatrix[y, x];
                var imageHeight = GetHeight(imageMatrix);
                var imageWidth = GetWidth(imageMatrix);
                for (var i = 0; i < windowArraySize; i++)
                {
                    var newY = y + dy[i];
                    var newX = x + dx[i];
                    if (newX < 0 || newX >= imageWidth || newY < 0 || newY >= imageHeight)
                        continue;

                    array[arrayLength] = imageMatrix[newY, newX];
                    if (array[arrayLength] > maxValue)
                        maxValue = array[arrayLength];

                    if (array[arrayLength] < minValue)
                        minValue = array[arrayLength];

                    arrayLength++;
                }

                switch (sortType)
                {
                    case 1:
                        array = Sorting.InsertionSort(array, arrayLength);
                        break;

                    case 2:
                        array = Sorting.SelectionSort(array, arrayLength);
                        break;

                    case 3:
                        array = Sorting.BubbleSort(array, arrayLength);
                        break;

                    case 4:
                        array = Sorting.ModifiedBubbleSort(array, arrayLength);
                        break;

                    case 5:
                        array = Sorting.MergeSort(array, 0, arrayLength - 1);
                        break;

                    case 6:
                        array = Sorting.QuickSort(array, 0, arrayLength - 1);
                        break;

                    case 7:
                        array = Sorting.CountingSort(array, arrayLength, maxValue, minValue);
                        break;

                    case 8:
                        array = Sorting.HeapSort(array, arrayLength);
                        break;

                    case 9:
                        array = Sorting.RadixSort(array, arrayLength);
                        break;

                    case 10:
                        var sortArray = new byte[arrayLength];
                        if (arrayLength != array.Length)
                            Array.Copy(array, sortArray, arrayLength);
                        else
                            sortArray = array;
                        Array.Sort(sortArray);
                        break;
                }

                minValue = array[0];
                var medianValue = array[arrayLength / 2];
                var medianDistanceFromMin = medianValue - minValue;
                var medianDistanceFromMax = maxValue - medianValue;
                if (medianDistanceFromMin <= 0 || medianDistanceFromMax <= 0)
                    return medianValue;

                var currentDistanceFromMin = currentValue - minValue;
                var currentDistanceFromMax = maxValue - currentValue;
                if (currentDistanceFromMin > 0 && currentDistanceFromMax > 0)
                    return currentValue;

                if (windowSize + 2 >= maxWindowSize)
                    return medianValue;

                windowSize = windowSize + 2;
            }
        }

        public static byte[,] DespeckleImage(string imagePath)
        {
            var imageMatrix = OpenImage(imagePath);
            DespeckleImage(imageMatrix);
            return imageMatrix;
        }

        public static byte[,] DespeckleImage(byte[,] imageMatrix, int maxSize = 5, int sort = 10, int filter = 2)
        {
            var imageMatrix2 = imageMatrix;
            var imageHeight = GetHeight(imageMatrix);
            var imageWidth = GetWidth(imageMatrix);
            for (var y = 0; y < imageHeight; y++)
            {
                for (var x = 0; x < imageWidth; x++)
                {
                    if (filter == 1)
                        imageMatrix2[y, x] = AlphaTrimFilter(imageMatrix, x, y, maxSize, sort);
                    else
                        imageMatrix2[y, x] = AdaptiveMedianFilter(imageMatrix, x, y, 3, maxSize, sort);
                }
            }

            return imageMatrix2;
        }
    }
}