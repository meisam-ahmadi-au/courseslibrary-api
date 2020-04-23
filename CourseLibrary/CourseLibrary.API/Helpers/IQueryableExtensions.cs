using CourseLibrary.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T> (this IQueryable<T> source, string orderBy, 
                                                    Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (mappingDictionary == null) throw new ArgumentNullException(nameof(mappingDictionary));
            if (string.IsNullOrEmpty(orderBy)) return source;
            // orderby string is comma separated so we split it by ,
            var orderByAfterSplit = orderBy.Split(',');

            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                var trimmedOrderByClause = orderByClause.Trim();
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                if (!mappingDictionary.ContainsKey(propertyName)) throw new ArgumentException($"Key mapping for {propertyName} is missing");
                var propertyMappingValue = mappingDictionary[propertyName];
                if (propertyMappingValue == null) throw new ArgumentNullException("Property Mapping Value");

                foreach (var destinationProperty in propertyMappingValue.DestinationProperties.Reverse())
                {
                if (propertyMappingValue.Revert) orderDescending = !orderDescending;
                    source = source.OrderBy(destinationProperty + (orderDescending ? " descending" : " ascending"));
                }

            };

            return source;

        }
    }
}
