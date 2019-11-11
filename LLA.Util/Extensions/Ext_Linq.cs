using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLA.GUI
{
    public static class Ext_Linq
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection) action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Int32 startIndex, Int32 count, Action<T> action)
        {
            Int32 endIndex = startIndex + count - 1;
            Int32 currentIndex = 0;
            foreach (var item in collection)
            {
                if(currentIndex++ < startIndex) continue;
                if(currentIndex > endIndex) break;
                action(item);
            }
        }

        public static List<T> NewSet<T>(this Int32 count, Func<T> constructor)
        {
            List<T> set = new List<T>();
            for (Int32 i = 0; i < count; i++)
            {
                set.Add(constructor());
            }
            return set;
        }

        public static IEnumerable<T> Intersperse<T>(this IEnumerable<T> source, Func<T> element)
        {
            foreach (T value in source)
            {
                yield return value;
                yield return element();
            }
        }

        public static List<T> SplitedSet<T>(this IEnumerable<T> source,  Func<T> element)
        {
            List<T> splitedSet = source.Intersperse(element).ToList();
            if (splitedSet.Count > 1) splitedSet.RemoveAt(splitedSet.Count - 1);
            return splitedSet;
        }
    }
}
