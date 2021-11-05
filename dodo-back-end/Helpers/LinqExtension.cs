﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DodoApp.Helpers
{
    public static class LinqExtension
    {
        public static string ConvertSortByParams(string sortByFields, string sortByDescs)
        {
            string orderByStr = "";
            if (!String.IsNullOrEmpty(sortByFields))
            {
                List<string> sortFields = sortByFields.Trim().Split(',').Select(x => x.Trim()).ToList();
                List<string> descFields = sortByDescs.Trim().Split(',').Select(x => x.Trim()).ToList();
                for (var i = 0; i < sortFields.Count; i++)
                {
                    if (orderByStr != "")
                        orderByStr += ", ";
                    orderByStr = orderByStr + sortFields[i].First().ToString().ToUpper() + sortFields[i][1..];
                    if (descFields.Count > i)
                        if (descFields[i].Trim() == "true")
                            orderByStr += " DESC";
                }
            }
            return orderByStr;
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByStrValues) where TEntity : class
        {
            var queryExpr = source.Expression;
            var methodAsc = "OrderBy";
            var methodDesc = "OrderByDescending";

            var orderByValues = orderByStrValues.Trim().Split(',').Select(x => x.Trim()).ToList();

            foreach (var orderPairCommand in orderByValues)
            {
                var command = orderPairCommand.ToUpper().EndsWith(" DESC") ? methodDesc : methodAsc;

                //Get propertyname and remove optional ASC or DESC
                var propertyName = orderPairCommand.Split(' ')[0].Trim();

                var type = typeof(TEntity);
                var parameter = Expression.Parameter(type, "p");

                PropertyInfo property;
                MemberExpression propertyAccess;

                if (propertyName.Contains('.'))
                {
                    // support to be sorted on child fields. 
                    var childProperties = propertyName.Split('.');

                    property = SearchProperty(typeof(TEntity), childProperties[0]);
                    if (property == null)
                        continue;

                    propertyAccess = Expression.MakeMemberAccess(parameter, property);

                    for (int i = 1; i < childProperties.Length; i++)
                    {
                        var t = property.PropertyType;
                        if (!t.IsGenericType)
                            property = SearchProperty(t, childProperties[i]);
                        else
                            property = SearchProperty(t, childProperties[i]);

                        if (property == null)
                            continue;

                        propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                    }
                }
                else
                {
                    property = null;
                    property = SearchProperty(type, propertyName);

                    if (property == null)
                        continue;

                    propertyAccess = Expression.MakeMemberAccess(parameter, property);
                }

                var orderByExpression = Expression.Lambda(propertyAccess, parameter);

                queryExpr = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, queryExpr, Expression.Quote(orderByExpression));
                methodAsc = "ThenBy";
                methodDesc = "ThenByDescending";
            }
            return source.Provider.CreateQuery<TEntity>(queryExpr); ;
        }

        private static PropertyInfo SearchProperty(Type type, string propertyName)
        {
            foreach (var item in type.GetProperties())
                if (item.Name.ToLower() == propertyName.ToLower())
                    return item;
            return null;
        }
    }
}
