namespace IQBackOffice.Despeckle
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class ImageOperations
    {
        /// <summary>
        /// Open an image, convert it to gray scale and load it into 2D array of size (Height x Width)
        /// </summary>
        /// <param name="imagePath">Image file path</param>
        /// <returns>2D array of gray values</returns>
        public static byte[,] OpenImage(string imagePath)
        {
            var originalImage = new Bitmap(imagePath);
            var height = originalImage.Height;
            var width = originalImage.Width;

            var buffer = new byte[height, width];

            unsafe
            {
                var bitmapData = originalImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, originalImage.PixelFormat);
                int x, y;
                int byteWidth;
                var bitsPerPixel = 0;

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
        /// Get the height of the image 
        /// </summary>
        /// <param name="imageMatrix">2D array that contains the image</param>
        /// <returns>Image Height</returns>
        public static int GetHeight(byte[,] imageMatrix)
        {
            return imageMatrix.GetLength(0);
        }

        /// <summary>
        /// Get the width of the image 
        /// </summary>
        /// <param name="imageMatrix">2D array that contains the image</param>
        /// <returns>Image Width</returns>
        public static int GetWidth(byte[,] imageMatrix)
        {
            return imageMatrix.GetLength(1);
        }

        /// <summary>
        /// Display the given image on the given PictureBox object
        /// </summary>
        /// <param name="imageMatrix">2D array that contains the image</param>
        /// <param name="pictureBox">PictureBox object to display the image on it</param>
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

        //1-InsertionSort
        public static byte[] InsertionSort(byte[] array, int arrayLength)
        {
            for (var j = 1; j < arrayLength; j++)
            {
                var key = array[j];
                var i = j - 1;
                while (i >= 0 && array[i] > key)
                    array[i + 1] = array[i--];

                array[++i] = key;
            }

            return array;
        }

        //2-SelectionSort
        public static byte[] SelectionSort(byte[] array, int arrayLength)
        {
            for (var j = 0; j < arrayLength - 1; j++)
            {
                var smallest = j;
                for (var i = j + 1; i < arrayLength; i++)
                {
                    if (array[i] < array[smallest])
                        smallest = i;
                }

                if (smallest == j)
                    continue;

                var temp = array[j];
                array[j] = array[smallest];
                array[smallest] = temp;
            }

            return array;
        }

        //3-BubbleSort
        public static byte[] BubbleSort(byte[] array, int arrayLength)
        {
            for (var i = 0; i < arrayLength - 1; i++)
            {
                for (var j = 0; j < arrayLength - 1 - i; j++)
                {
                    if (array[j + 1] >= array[j])
                        continue;

                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }

            return array;
        }

        //4-ModifiedBubbleSort
        public static byte[] ModifiedBubbleSort(byte[] array, int arrayLength)
        {
            for (var i = 0; i < arrayLength - 1; i++)
            {
                var sorted = true;
                for (var j = 0; j < arrayLength - 1 - i; j++)
                {
                    if (array[j + 1] >= array[j])
                        continue;

                    sorted = false;
                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }

                if (sorted)
                    break;
            }

            return array;
        }

        //5-MergeSort
        public static void Merge(byte[] array, int p, int q, int r)
        {
            var n1 = q - p + 1;
            var n2 = r - q;
            var leftArray = new byte[n1];
            var rightArray = new byte[n2];
            for (var i = 0; i < n1; i++)
                leftArray[i] = array[p + i];

            for (var i = 0; i < n2; i++)
                rightArray[i] = array[q + i + 1];

            int k = 0, j = 0, index = 0;
            while (k < n1 && j < n2)
            {
                if (leftArray[k] <= rightArray[j])
                    array[p + index] = leftArray[k++];
                else
                    array[p + index] = rightArray[j++];

                index++;
            }

            if (k < n1)
            {
                for (; k < n1; k++, index++)
                    array[p + index] = leftArray[k];
            }
            else if (j < n2)
            {
                for (; j < n2; j++, index++)
                    array[p + index] = rightArray[j];
            }
        }

        public static byte[] MergeSort(byte[] array, int p, int r)
        {
            if (p >= r)
                return array;

            var q = (p + r) / 2;
            MergeSort(array, p, q);
            MergeSort(array, q + 1, r);
            Merge(array, p, q, r);
            return array;
        }

        //6-QuickSort
        public static int Partition(byte[] array, int p, int r)
        {
            var x = array[r];
            byte temp;
            var i = p;
            for (var j = p; j < r; j++)
            {
                if (array[j] > x)
                    continue;

                temp = array[j];
                array[j] = array[i];
                array[i++] = temp;
            }

            temp = array[i];
            array[i] = array[r];
            array[r] = temp;
            return i;
        }

        public static byte[] QuickSort(byte[] array, int p, int r)
        {
            if (p >= r)
                return array;

            var q = Partition(array, p, r);
            QuickSort(array, p, q - 1);
            QuickSort(array, q + 1, r);
            return array;
        }

        //7-CountingSort (XXXXX)
        public static byte[] CountingSort(byte[] array, int arrayLength, byte max, byte min)
        {
            var count = new byte[max - min + 1];
            var z = 0;

            for (var i = 0; i < count.Length; i++) { count[i] = 0; }
            for (var i = 0; i < arrayLength; i++) { count[array[i] - min]++; }

            for (int i = min; i <= max; i++)
            {
                while (count[i - min]-- > 0)
                {
                    array[z] = (byte)i;
                    z++;
                }
            }
            return array;
        }

        //8-HeapSort
        public static int Left(int i)
        {
            return i << 1 + 1;
        }

        public static int Right(int i)
        {
            return i << 1 + 2;
        }

        public static void MaxHeapify(byte[] array, int arrayLength, int i)
        {
            while (true)
            {
                var left = Left(i);
                var right = Right(i);
                int largest;
                if (left < arrayLength && array[left] > array[i])
                    largest = left;
                else
                    largest = i;

                if (right < arrayLength && array[right] > array[largest])
                    largest = right;

                if (largest == i)
                    return;

                var temp = array[i];
                array[i] = array[largest];
                array[largest] = temp;
                i = largest;
            }
        }

        public static void BuildMaxHeap(byte[] array, int arrayLength)
        {
            for (var i = arrayLength / 2 - 1; i >= 0; i--)
                MaxHeapify(array, arrayLength, i);
        }

        public static byte[] HeapSort(byte[] array, int arrayLength)
        {
            var heapSize = arrayLength;
            BuildMaxHeap(array, arrayLength);
            for (var i = arrayLength - 1; i > 0; i--)
            {
                var temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                heapSize--;
                MaxHeapify(array, heapSize, 0);
            }

            return array;
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

        public static byte AdaptiveMedianFilter(byte[,] imageMatrix, int x, int y, int w, int maxWidth, int sort)
        {
            while (true)
            {
                var array = new byte[w * w];
                var dx = new int[w * w];
                var dy = new int[w * w];
                var index = 0;
                var wHalf = w / 2;
                for (var yShift = -wHalf; yShift <= wHalf; yShift++)
                {
                    for (var xShift = -wHalf; xShift <= wHalf; xShift++)
                    {
                        dx[index] = xShift;
                        dy[index] = yShift;
                        index++;
                    }
                }

                byte max = 0;
                byte min = 255;
                var arrayLength = 0;
                var z = imageMatrix[y, x];
                var imageHeight = GetHeight(imageMatrix);
                var imageWidth = GetWidth(imageMatrix);
                for (var i = 0; i < w * w; i++)
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

                    arrayLength++;
                }

                switch (sort)
                {
                    case 1:
                        array = InsertionSort(array, arrayLength);
                        break;

                    case 2:
                        array = SelectionSort(array, arrayLength);
                        break;

                    case 3:
                        array = BubbleSort(array, arrayLength);
                        break;

                    case 4:
                        array = ModifiedBubbleSort(array, arrayLength);
                        break;

                    case 5:
                        array = MergeSort(array, 0, arrayLength - 1);
                        break;

                    case 6:
                        array = QuickSort(array, 0, arrayLength - 1);
                        break;

                    case 7:
                        array = CountingSort(array, arrayLength, max, min);
                        break;

                    case 8:
                        array = HeapSort(array, arrayLength);
                        break;
                }

                min = array[0];
                var med = array[arrayLength / 2];
                var a1 = med - min;
                var a2 = max - med;
                if (a1 <= 0 || a2 <= 0)
                    return med;

                var b1 = z - min;
                var b2 = max - z;
                if (b1 > 0 && b2 > 0)
                    return z;

                if (w + 2 >= maxWidth)
                    return med;

                w = w + 2;
            }
        }

        public static byte[,] ImageFilter(byte[,] imageMatrix, int maxSize, int sort, int filter)
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
