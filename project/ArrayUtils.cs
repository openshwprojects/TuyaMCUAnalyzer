using System;
using System.Collections.Generic;
using System.Text;

namespace TuyaMCUAnalyzer
{
    public class ArrayUtils
    {
        public static T FindMax<T>(List<T> list) where T : IComparable<T>
        {
            if (list == null || list.Count == 0)
            {
                return default(T);
            }

            T max = list[0];

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].CompareTo(max) > 0)
                {
                    max = list[i];
                }
            }

            return max;
        }

        public static T FindMin<T>(List<T> list) where T : IComparable<T>
        {
            if (list == null || list.Count == 0)
            {
                return default(T);
            }

            T min = list[0];

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].CompareTo(min) < 0)
                {
                    min = list[i];
                }
            }

            return min;
        }
    }
}
