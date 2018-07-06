namespace IQBackOffice.Despeckle
{
    using System;

    public class Sorting
    {
        public enum SortType
        {
            None,
            BubbleSort,
            CountingSort,
            HeapSort,
            InsertionSort,
            MergeSort,
            ModifiedBubbleSort,
            QuickSort,
            RadixSort,
            SelectionSort,
            NativeArraySort
        }

        #region Public Methods

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

        public static void BuildMaxHeap(byte[] array, int arrayLength)
        {
            for (var i = arrayLength / 2 - 1; i >= 0; i--)
                MaxHeapify(array, arrayLength, i);
        }

        public static byte[] CountingSort(byte[] array, int arrayLength, byte max, byte min)
        {
            var count = new byte[max - min + 1];
            var z = 0;

            for (var i = 0; i < count.Length; i++)
                count[i] = 0;

            for (var i = 0; i < arrayLength; i++)
                count[array[i] - min]++;

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

        public static int Left(int i)
        {
            return i << 1 + 1;
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
                for (; k < n1; k++, index++)
                    array[p + index] = leftArray[k];
            else if (j < n2)
                for (; j < n2; j++, index++)
                    array[p + index] = rightArray[j];
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

        public static byte[] RadixSort(byte[] array, int arrayLength)
        {
            // Our helper array 
            var tempArray = new byte[array.Length];

            // Number of bits our group will be long 
            const int GROUP_BITS = 2;

            // Number of bits of a C# byte 
            const int BITS_PER_BYTE = 8;

            // Counting and prefix arrays
            // (note dimensions 2^r which is the number of all possible values of a r-bit number) 
            var counts = new int[1 << GROUP_BITS];
            var prefixes = new int[1 << GROUP_BITS];

            // Number of groups 
            var numGroups = (int)Math.Ceiling(BITS_PER_BYTE / (double)GROUP_BITS);

            // The mask to identify groups 
            const int GROUP_MASK = (1 << GROUP_BITS) - 1;

            // The algorithm: 
            for (int group = 0, shift = 0; group < numGroups; group++, shift += GROUP_BITS)
            {
                // Reset count array 
                for (var c = 0; c < counts.Length; c++)
                    counts[c] = 0;

                // Count elements of the c-th group 
                foreach (var element in array)
                    counts[(element >> shift) & GROUP_MASK]++;

                // Calculate prefixes 
                prefixes[0] = 0;
                for (var i = 1; i < counts.Length; i++)
                    prefixes[i] = prefixes[i - 1] + counts[i - 1];

                // From array[] to tempArray[] elements ordered by c-th group 
                foreach (var element in array)
                    tempArray[prefixes[(element >> shift) & GROUP_MASK]++] = element;

                // array[] = tempArray[] and start again until the last group 
                tempArray.CopyTo(array, 0);
            }

            // Array is sorted 
            return array;
        }

        public static int Right(int i)
        {
            return i << 1 + 2;
        }

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

        #endregion Public Methods
    }
}