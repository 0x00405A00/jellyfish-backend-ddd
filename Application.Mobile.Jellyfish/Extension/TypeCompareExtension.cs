using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Extension
{
    public static class TypeCompareExtension
    {
        public class TypeCompareResult
        {
            public int EqualHits { get; set; }
            public List<PropertyInfo> NotEqualDataTypes { get; set; } = new List<PropertyInfo>();
            public List<PropertyInfo> NotFoundTypes { get; set; } = new List<PropertyInfo>();
            public Dictionary<PropertyInfo, PropertyInfo> Equal { get; set; } = new Dictionary<PropertyInfo, PropertyInfo>();
            public List<PropertyInfo> LeadSourceProperties { get; private set; } 
            public List<PropertyInfo> ConmpareSourceProperties { get; private set; }
            public bool IsEqualityGiven
            {
                get
                {
                    return LeadSourceProperties != null && LeadSourceProperties.Count == EqualHits;
                }
            }
            public bool PropertyTypeCompareSensitivity { get; set; }
            public bool PropertyNameCaseSensitivity { get; set; }
            public TypeCompareResult(List<PropertyInfo> leadSourceProperties, List<PropertyInfo> conmpareSourceProperties)
            {
                LeadSourceProperties = leadSourceProperties;
                ConmpareSourceProperties = 
                    conmpareSourceProperties;

            }
        }
        public static TypeCompareResult IsClassStructEqualLike(this Type type,Type compareType,bool propertyTypeCompareSensitivity = true,bool propertyNameCaseSensitivity = true)
        {
            var propertiesFromCompareType = compareType.GetProperties()?.ToList();
            var propertiesFromType = type.GetProperties()?.ToList();

            TypeCompareResult typeCompareResult = new TypeCompareResult(propertiesFromType, propertiesFromCompareType);
            typeCompareResult.PropertyTypeCompareSensitivity= propertyTypeCompareSensitivity;
            typeCompareResult.PropertyNameCaseSensitivity = propertyNameCaseSensitivity;    


            if (compareType == null) 
                return typeCompareResult;

            if (propertiesFromCompareType == null || propertiesFromType == null) 
                return typeCompareResult;

            int hits = 0;

            foreach (var classPropertiesFromType in propertiesFromType) 
            {
                var foundInCompareTypeByNameComparison = propertiesFromCompareType.Find(x => 
                (propertyNameCaseSensitivity ? x.Name:x.Name.ToLower()) == (propertyNameCaseSensitivity ? classPropertiesFromType.Name: classPropertiesFromType.Name.ToLower()));
                if(foundInCompareTypeByNameComparison != null)
                {
                    if(propertyTypeCompareSensitivity)
                    {
                        if(foundInCompareTypeByNameComparison.PropertyType == classPropertiesFromType.PropertyType)
                        {
                            hits++;
                            typeCompareResult.Equal.Add(classPropertiesFromType, foundInCompareTypeByNameComparison);
                        }
                        else
                        {
                            typeCompareResult.NotEqualDataTypes.Add(foundInCompareTypeByNameComparison);
                        }
                    }
                    else
                    {
                        hits++;
                        typeCompareResult.Equal.Add(classPropertiesFromType, foundInCompareTypeByNameComparison);
                    }
                }
                else
                {
                    typeCompareResult.NotFoundTypes.Add(foundInCompareTypeByNameComparison);
                }
            }
            typeCompareResult.EqualHits = hits;
            return typeCompareResult;
        }
    }
}
