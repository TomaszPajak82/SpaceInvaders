using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Extensions
{
    public static class EnumeratorExtensions
    {

        public static void AddToList<T>(this IEnumerator<T> enumerator,IList<T> destination) {

            while (enumerator.MoveNext()) {
                destination.Add(enumerator.Current);
            }

        }

        public static T[] ToArray<T>(this IEnumerator<T> enumerator) {

            List<T> tmpList = new List<T>();
            while (enumerator.MoveNext()) {
                tmpList.Add(enumerator.Current);
            }

            return tmpList.ToArray();
        }

    }

}