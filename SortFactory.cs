namespace SortVisualizer
{
    class SortFactory
    {
        public static ISortEngine LoadSort(int i)
        {
            switch (i)
            {
                case 0:
                    return new BubbleSortEngine();
                case 1:
                    return new SelectionSortEngine();
                case 2:
                    return new InsertionSortEngine();
                case 3:
                    return new RadixSortEngine();
                case 4:
                    return new BogoSortEngine();
                case 5:
                    return new SlowSortEngine();
                case 6:
                    return new DropSortEngine();
                case 7:
                    return new QuickSortEngine();
            }
            return new BubbleSortEngine();
        }
    }
}
