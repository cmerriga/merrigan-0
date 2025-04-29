namespace Merrigan0 {
    [Untested]
    public static class CompareResult {
        public static bool Equal(int compareResult) { return compareResult == 0; }
        public static bool LeftBigger(int compareResult) { return compareResult > 0; }
        public static bool RightBigger(int compareResult) { return compareResult < 0; }
    }
}
