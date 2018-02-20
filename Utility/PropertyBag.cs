using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Utility
{
    [ExcludeFromCodeCoverage]
    public class PropertyBag
    {
        public PropertyBag(string typeName, Type typeDefinition, string propertyName, Type propertyType,
            List<PropertyBagTypeInfo> collectionInnerTypes, 
            TransformablePropertyTypeEnum transformablePropertyType,
            bool hasDefaultValue, object defaultValue)
        {
            TypeName = typeName;
            PropertyName = propertyName;
            PropertyType = propertyType;
            CollectionInnerTypes = collectionInnerTypes;
            HasDefaultValue = hasDefaultValue;
            DefaultValue = defaultValue;
            TransformablePropertyType = transformablePropertyType;
            TypeDefinition = typeDefinition;
        }

        public string TypeName { get; set; }
        public string PropertyName { get; set; }
        public Type PropertyType { get; set; }
        public TransformablePropertyTypeEnum TransformablePropertyType { get; set; }
        public List<PropertyBagTypeInfo> CollectionInnerTypes { get; set; }
        public bool HasDefaultValue { get; set; }
        public object DefaultValue { get; set; }
        public Type TypeDefinition { get; set; }

        public enum TransformablePropertyTypeEnum
        {
            Primitive = 1,
            CollectionType = 2,
            DictionaryType = 3,
            ReferenceType = 4
        }
    }
    
    [ExcludeFromCodeCoverage]
    public class PropertyBagTypeInfo
    {
        public Type Type { get; set; }
        public bool IsDictionaryKey { get; set; }
        public bool IsPrimitiveType { get; set; }
        public bool IsEnumType { get; set; }
    }
}
