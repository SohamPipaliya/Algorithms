using System;
using static System.Console;
using static System.Math;
using static Algorithms.SearchingAlgorithms;
using static Algorithms.SortingAlgorithms;

namespace Algorithms
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            WriteLine("------------------------------------->");
            WriteLine("<-------------------------------------");

            WriteLine("-----Selection Sort-----");
            foreach (var item in SelectionSort(new int[] { 10, 1, 9, 2, 8, 3, 7, 4, 6, 5, 0 }))
            {
                Console.WriteLine(item);
            }

            WriteLine("-----Selection Sort With Recursion-----");
            foreach (var item in SelectionSortWithRecursion(new int[] { 10, 1, 9, 2, 8, 3, 7, 4, 6, 5, 0 }))
            {
                Console.WriteLine(item);
            }

            WriteLine("-----Bubble Sort-----");
            foreach (var item in BubbleSort(new int[] { 10, 1, 9, 2, 8, 3, 7, 4, 6, 5, 0 }))
            {
                Console.WriteLine(item);
            }

            WriteLine("-----Bubble Sort With Recursion-----");
            foreach (var item in BubbleSortWithRecursion(new int[] { 10, 1, 9, 2, 8, 3, 7, 4, 6, 5, 0 }))
            {
                Console.WriteLine(item);
            }

            //writeline("-----improved bubble sort-----");
            //foreach (var item in improvedbubblesort(new int[] { 0, 1, 2, 5, 3, 4, 5, 6, 5 }))
            //{
            //    console.writeline(item);
            //}

            WriteLine("-----Insertion Sort-----");
            foreach (var item in InsertionSort(new int[] { 10, 1, 9, 2, 8, 3, 7, 4, 6, 5, 0 }))
            {
                Console.WriteLine(item);
            }

            WriteLine("-----Insertion Sort With Recursion-----");
            foreach (var item in InsertionSortWithRecursion(new int[] { 10, 1, 9, 2, 8, 3, 7, 4, 6, 5, 0 }))
            {
                Console.WriteLine(item);
            }

            WriteLine("----- Searching Started -----");
            CallSearching();
        }

        static void CallSearching()
        {
            var arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var length = arr.Length - 1;
        here:
            Write("Enter Element to Search: - ");
            var input = Int32.Parse(ReadLine());
            WriteLine("------------------------------------->");
            WriteLine("<-------------------------------------");

            WriteLine("-----Linear Search-----");
            WriteLine(LinearSearch(arr, input));
            WriteLine(LinearSearchWithRecursion(arr, input));
            WriteLine(ImprovedLinearSearch(arr, input));
            WriteLine(ImprovedLinearSearchWithRecursion(arr, input));

            WriteLine("-----Jump Search-----");
            WriteLine(JumpSearch(arr, input));
            WriteLine(JumpSearchWithRecursion(arr, find: input, sqareRoot: (int)Floor(Sqrt(length + 1)), minimal: (int)Floor(Sqrt(length + 1))));

            WriteLine("-----Interpolation Search-----");
            WriteLine(InterpolationSearch(arr, find: input, left: 0, rigth: length));
            WriteLine(InterpolationSearchWithRecursion(arr, find: input, left: 0, rigth: length));

            WriteLine("-----Binary Search-----");
            WriteLine(BinarySearch(arr, find: input));
            WriteLine(BinarySearchWithRecursion(arr, find: input, right: length));

            WriteLine("-----Exponential Search-----");
            WriteLine(ExponentialSearch(arr, input));
            WriteLine(ExponentialSearchWithRecursion(arr, input));

            WriteLine("-----Fibonancci Search-----");
            WriteLine(FibonancciSearch(arr, input));
            WriteLine(FibonancciSearchWithRecursion(arr, input));

            WriteLine("------------------------------------->");
            WriteLine("<-------------------------------------");
            goto here;
        }
    }

    internal static class SearchingAlgorithms
    {
        #region Linear Search
        // The time complexity of the below algorithm is O(n)
        // It is not necessary to have sorted array

        internal static int LinearSearch(int[] arr, int find)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (find == arr[i]) return i;
            }
            return -1;
        }

        internal static int LinearSearchWithRecursion(int[] arr, int find, int index = 0)
        {
            if (index >= 0 && index < arr.Length)
            {
                if (arr[index] == find) return index;
                else return LinearSearchWithRecursion(arr, find, index + 1);
            }
            return -1;
        }

        // Improving algorithm in some case. if element Found at last  O(n) to O(1). if element Not found O(n) to O(n/2)

        internal static int ImprovedLinearSearch(int[] arr, int find)
        {
            int left = 0, length = arr.Length, right = length - 1;
            //for (left = 0; left <= right;)
            //{
            //    if (arr[left] == find) return left;
            //    if (arr[right] == find) return right;
            //    left++; right--;
            //}

            // or

            while (left <= right)
            {
                if (arr[left] == find) return left;
                if (arr[right] == find) return right;
                left++; right--;
            }
            return -1;
        }

        internal static int ImprovedLinearSearchWithRecursion(int[] arr, int find)
        {
            int InnerRecursion(int[] arr, int find, int right, int left = 0)
            {
                if (left >= 0 && left <= right && right < arr.Length)
                {
                    if (arr[left] == find) return left;
                    else if (arr[right] == find) return right;
                    else return InnerRecursion(arr, find, right: --right, left: ++left);
                }
                return -1;
            }
            return InnerRecursion(arr, find, arr.Length - 1);
        }
        #endregion

        #region InterPolation Search
        // Time Complexity: O(log2(log2 n)) for the average case, and O(n) for the worst case (when items are distributed exponentially)
        // It is necessary to have sorted array

        internal static int InterpolationSearchWithRecursion(int[] arr, int find, int left, int rigth, int position = 0)
        {
            if (left <= rigth && find >= arr[left] && find <= arr[rigth])
            {
                position = left + (((rigth - left) / (arr[rigth] - arr[left])) * (find - arr[left]));

                if (arr[position] == find) return position;
                else if (arr[position] < find) return InterpolationSearchWithRecursion(arr, find, position + 1, rigth);
                else return InterpolationSearchWithRecursion(arr, find, left, position - 1);
            }
            return -1;
        }

        internal static int InterpolationSearch(int[] arr, int find, int left, int rigth)
        {
            int position = 0;

            while (left <= rigth && find >= arr[left] && find <= arr[rigth])
            {
                position = left + (((rigth - left) / (arr[rigth] - arr[left])) * (find - arr[left]));

                if (arr[position] == find) return position;
                else if (arr[position] < find) left = position + 1;
                else rigth = position - 1;
            }
            return -1;
        }
        #endregion

        #region Jump Search
        // Worst case time complexity: O(√N). Average case time complexity: O(√N). Best case time complexity: O(1)
        // It is necessary to have sorted array

        internal static int JumpSearch(int[] arr, int find)
        {
            var length = arr.Length;
            var sqaureRoot = (int)(Floor(Sqrt(length)));
            var minimal = (int)Floor(Sqrt(length));

            if (find < arr[minimal]) sqaureRoot = 0;
            else if (arr[sqaureRoot] == find) return sqaureRoot;
            else
            {
                while (sqaureRoot <= length - minimal && arr[sqaureRoot + minimal - 1] < find)
                {
                    sqaureRoot += minimal;
                }
            }

            if (arr[sqaureRoot] == find) return sqaureRoot;

            while (arr[sqaureRoot] < find && sqaureRoot < length - 1)
            {
                sqaureRoot++;
            }

            if (arr[sqaureRoot] == find) return sqaureRoot;
            return -1;
        }

        internal static int JumpSearchWithRecursion(int[] arr, int find, int sqareRoot, int minimal)
        {
            if (sqareRoot > 0 && sqareRoot <= arr.Length && find <= arr[arr.Length - 1] && find >= arr[0])
            {
                if (find < arr[sqareRoot - 1]) return JumpSearchWithRecursion(arr, find, --sqareRoot, minimal);
                else if (arr[sqareRoot - 1] == find) return sqareRoot - 1;
                else // find > arr[squareRoot - 1]
                {
                    var index = Min(sqareRoot + minimal, arr.Length); //sqareRoot + minimal <= arr.Length ? sqareRoot + minimal : arr.Length;
                    if (arr[index - 1] <= find) return JumpSearchWithRecursion(arr, find, index, minimal);
                    else return JumpSearchWithRecursion(arr, find, ++sqareRoot, minimal);
                }
            }
            return -1;
        }
        #endregion

        #region Binary Search
        // The time complexity to O(Log n)
        // It is necessary to have sorted array

        internal static int BinarySearch(int[] arr, int find)
        {
            int right = arr.Length - 1, left = 0;
            while (left <= right)
            {
                int midIndex = (int)Floor((double)(left + right) / 2);

                if (arr[midIndex] == find) return midIndex;
                else if (find > arr[midIndex]) left = midIndex + 1;
                else right = midIndex - 1;
            }
            return -1;
        }

        internal static int BinarySearchWithRecursion(int[] arr, int find, int right, int left = 0)
        {
            if (left <= right)
            {
                int midIndex = (int)Floor((double)(left + right) / 2);
                if (arr[midIndex] == find) return midIndex;
                else if (find > arr[midIndex]) return BinarySearchWithRecursion(arr, find, right: right, left = midIndex + 1);
                else return BinarySearchWithRecursion(arr, find, right: midIndex - 1, left);
            }
            return -1;
        }
        #endregion

        #region Exponential Search
        // Time Complexity : O(Log n)
        // Auxiliary Space : The above implementation of Binary Search is recursive and requires O(Log n) space.With iterative Binary Search, we need only O(1) space.
        // It uses BinarySearch at the end

        internal static int ExponentialSearch(int[] arr, int find)
        {
            //if (arr[0] == find) return 0;
            int i = 1;
            while (i < arr.Length && arr[i] <= find) i *= 2;
            return BinarySearch(arr, find);
        }

        internal static int ExponentialSearchWithRecursion(int[] arr, int find, int index = 1)
        {
            //if (arr[0] == find) return 0;
            if (index < arr.Length && arr[index] <= find)
                return (ExponentialSearchWithRecursion(arr, find, index * 2));
            return BinarySearch(arr, find);
        }
        #endregion

        #region Fibonacci Search
        // Similarities with Binary Search:
        // Works for sorted arrays
        // A Divide and Conquer Algorithm.
        // Has Log n time complexity.

        // Differences with Binary Search:
        // Fibonacci Search divides given array in unequal parts
        // Binary Search uses division operator to divide range.Fibonacci Search doesn’t use /, but uses + and -. The division operator may be costly on some CPUs.
        // Fibonacci Search examines relatively closer elements in subsequent steps. So when input array is big that cannot fit in CPU cache or even in RAM, Fibonacci Search can be useful.

        internal static int FibonancciSearch(int[] arr, int find)
        {
            int x = 0, y = 1, z = x + y, index = -1;
            while (z < arr.Length)
            {
                x = y;
                y = z;
                z = x + y;
            }
            while (z > 1)
            {
                int i = Min(index + x, arr.Length - 1);
                if (arr[i] < find)
                {
                    z = y;
                    y = x;
                    x = z - y;
                    index = i;
                }
                else if (arr[i] > find)
                {
                    z = x;
                    y = y - x;
                    x = z - y;
                }
                else return i;
            }
            var actualIndex = Min(index + 1, arr.Length - 1);
            if (y == 1 && arr[actualIndex] == find) return actualIndex;
            return -1;
        }

        // not possible because on increase value and other one decrease valueso it gives statckoverflow exception.
        internal static int FibonancciSearchWithRecursion(int[] arr, int find, int minimal = -1, int x = 0, int y = 1, int z = 1, int count = 0)
        {
            if (z < arr.Length && count < arr.Length) return FibonancciSearchWithRecursion(arr, find, minimal: minimal, x: x = y, y: y = z, z: z = x + y, count: z);
            else if (z > 1)
            {
                int index = Min(x + minimal, arr.Length - 1);
                if (arr[index] < find) return FibonancciSearchWithRecursion(arr, find, minimal: index, z: z = y, y: y = x, x: x = z - y, count: count);
                else if (arr[index] > find) return FibonancciSearchWithRecursion(arr, find, minimal: minimal, z: z = x, y: y = y - x, x: x = z - y, count: count);
                else return index;
            }
            else if (y == 1 && arr[Min(minimal + 1, arr.Length - 1)] == find) return Min(minimal + 1, arr.Length - 1);
            return -1;
        }
        #endregion
    }

    internal static class SortingAlgorithms
    {
        #region Selection Sort
        // Time Complexity: O(n2) as there are two nested loops.
        // Auxiliary Space: O(1)
        // The good thing about selection sort is it never makes more than O(n) swaps and can be useful when memory write is a costly operation.

        internal static int[] SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int index = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[index]) index = j;
                }
                (arr[index], arr[i]) = (arr[i], arr[index]); // Functional way of swapping.
            }
            return arr;
        }

        internal static int[] SelectionSortWithRecursion(int[] arr, int i = 0, int j = 1, int index = 0)
        {
            if (i >= 0 && i < arr.Length - 1)
            {
                if (j < arr.Length)
                {
                    if (arr[j] < arr[index]) index = j;
                    return SelectionSortWithRecursion(arr, i, j: ++j, index: index);
                }
                (arr[index], arr[i]) = (arr[i], arr[index]); // Functional way of swapping.
                return SelectionSortWithRecursion(arr, ++i, j = i + 1, index: index = i);
            }
            return arr;
        }
        #endregion

        #region Bubble Sort
        // Worst and Average Case Time Complexity: O(n* n). Worst case occurs when array is reverse sorted.
        // Best Case Time Complexity: O(n). Best case occurs when array is already sorted.
        // Auxiliary Space: O(1)
        // Boundary Cases: Bubble sort takes minimum time(Order of n) when elements are already sorted.

        internal static int[] BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j]) (arr[j], arr[i]) = (arr[i], arr[j]); // Swapp
                }
            }
            return arr;
        }

        internal static int[] BubbleSortWithRecursion(int[] arr, int i = 0, int j = 1)
        {
            if (i >= 0 && i < arr.Length - 1)
            {
                if (j < arr.Length)
                {
                    if (arr[i] > arr[j]) (arr[j], arr[i]) = (arr[i], arr[j]);
                    return BubbleSortWithRecursion(arr, i: i, j: ++j);
                }
                return BubbleSortWithRecursion(arr, i: ++i, j: j = i + 1);
            }
            return arr;
        }

        //internal static int[] ImprovedBubbleSort(int[] arr)
        //{
        //    bool swaped = false;
        //    for (int i = 0; i < arr.Length - 1; i++)
        //    {
        //        swaped = false;
        //        for (int j = i + 1; j < arr.Length - i - 1; j++)
        //        {
        //            if (arr[i] > arr[j])
        //            {
        //                (arr[j], arr[i]) = (arr[i], arr[j]); // Swapp
        //                swaped = true;
        //            }
        //        }
        //        if (!swaped) break;
        //    }
        //    return arr;
        //}
        #endregion

        #region Insertion Sort
        // Time Complexity: O(n*2)
        // Auxiliary Space: O(1)
        // Boundary Cases: Insertion sort takes maximum time to sort if elements are sorted in reverse order.And it takes minimum time (Order of n) when elements are already sorted

        internal static int[] InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
            return arr;
        }

        internal static int[] InsertionSortWithRecursion(int[] arr, int i = 1, int j = 0, int key = 0, bool check = true)
        {
            if (i >= 1 && i < arr.Length)
            {
                if (check) key = arr[i];
                if (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    return InsertionSortWithRecursion(arr, i: i, j: --j, key: key, check: false);
                }
                arr[j + 1] = key;
                return InsertionSortWithRecursion(arr, i: ++i, j: j = i - 1, check: true);
            }
            return arr;
        }
        #endregion
    }
}