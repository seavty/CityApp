﻿using CityApp.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityApp.Utils.Extension
{
    public static class PagingExtension
    {
        //used by LINQ to SQL
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int currentPage)
        {
            return source.Skip((currentPage - 1) * PaginationHelper.PAGE_SIZE).Take(PaginationHelper.PAGE_SIZE);
        }

        //used by LINQ
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}