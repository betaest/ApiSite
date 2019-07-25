using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApiSite.Utils {
    public class Data {
        private readonly Dictionary<string, object> mDict;

        public Data(Dictionary<string, object> raw) {
            mDict = raw ?? new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
        }

        public Data(params Tuple<string, object>[] items) {
            mDict = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);

            foreach (var i in items)
                mDict.Add(i.Item1, i.Item2);
        }

        public Data(object raw, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance, bool ignoreCase = true) {
            var ignore = ignoreCase ? StringComparer.CurrentCultureIgnoreCase : StringComparer.CurrentCulture;

            mDict = raw?.GetType().GetProperties(flags).Where(p => p.CanRead).ToDictionary(p => p.Name, p => p.GetValue(raw), ignore) ??
                    new Dictionary<string, object>(ignore);
        }

        public object this[string index] => mDict.ContainsKey(index) ? mDict[index] : null;
        public object this[int index] => mDict.Skip(index).FirstOrDefault();

        public int Count => mDict.Count;
        public IEnumerable<string> Indexes => mDict.Keys;

        public bool HasIndex(string index) => mDict.ContainsKey(index);
        public bool HasIndex(int index) => mDict.Count > index;

        public static implicit operator Data(Dictionary<string, object> raw) => new Data(raw);
        public static implicit operator Data(Tuple<string, object>[] items) => new Data(items);

        public static Data FromObject(object me,
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance, bool ignoreCase = true) {
            var raw = me as Dictionary<string, object>;

            return raw != null ? FromDictionary(raw, ignoreCase) : new Data(me, flags, ignoreCase);
        }

        public static Data FromDictionary<T>(Dictionary<string, T> raw, bool ignoreCase = true)
            =>
                new Data(raw?.ToDictionary(r => r.Key, r => (object) r.Value,
                    ignoreCase ? StringComparer.CurrentCultureIgnoreCase : StringComparer.CurrentCulture));
    }
}