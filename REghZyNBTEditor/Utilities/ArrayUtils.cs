namespace REghZyNBTEditor.Utilities {
    public static class ArrayUtils {
        public static T[] Copy<T>(T[] array) {
            T[] copy = new T[array.Length];
            for (int i = 0, len = array.Length; i < len; i++) {
                copy[i] = array[i];
            }

            return copy;
        }
    }
}