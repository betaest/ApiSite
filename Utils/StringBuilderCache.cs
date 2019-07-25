using System;
using System.Text;

namespace ApiSite.Utils{
    internal class StringBuilderCache {
        private const int MAX_BUILDER_SIZE = 360;

        [ThreadStatic]
        private static StringBuilder cachedInstance;

        public static StringBuilder Acquire(int capacity = 16) {
            if (capacity <= MAX_BUILDER_SIZE) {
                var stringBuilder = cachedInstance;

                if (stringBuilder != null && capacity <= stringBuilder.Capacity) {
                    cachedInstance = null;
                    stringBuilder.Clear();
                    return stringBuilder;
                }
            }
            return new StringBuilder(capacity);
        }

        private static void Release(StringBuilder sb) {
            if (sb.Capacity > MAX_BUILDER_SIZE)
                return;
            cachedInstance = sb;
        }

        public static string GetStringAndRelease(StringBuilder sb) {
            var str = sb.ToString();
            Release(sb);
            return str;
        }
    }
}