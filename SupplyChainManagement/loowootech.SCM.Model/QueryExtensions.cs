using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    public static class QueryExtensions
    {
        public static IQueryable<T> SetPage<T>(this IQueryable<T> query, PageFilter page)
        {
            if (page == null || page.PageSize == int.MaxValue) return query;

            if (page.RecordCount == 0)
            {
                page.RecordCount = query.Count();
            }
            return query.Skip(page.PageSize * (page.PageIndex - 1)).Take(page.PageSize);
        }
    }
}
