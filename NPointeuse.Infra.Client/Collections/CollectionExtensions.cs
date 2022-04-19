using System;
using System.Collections.Generic;
using System.Text;

namespace NPointeuse.Infra.Client.Collections
{
  public static  class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            foreach (var item in source)
                target.Add(item);
        }
    }
}
