using System;
using System.Collections.Generic;

namespace UnderscoreCs
{
    public partial class _
    {
        public static IEnumerable<TListOut> Map<TListIn, TListOut>(IEnumerable<TListIn> list, Func<TListIn, TListOut> iteratee)
        {
            TListOut value;
            foreach (var item in list)
            {
                value = iteratee(item);
                yield return value; 
            }
            value = default(TListOut);
        }
    }
}