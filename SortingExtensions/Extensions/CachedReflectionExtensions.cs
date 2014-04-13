using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace SortingExtensions.Extensions
{
    public static class CachedReflectionExtensions
    {
        private static readonly char[] ExpressionPartSeparator = new char[] { '.' };
        private static readonly ConcurrentDictionary<Type, PropertyDescriptorCollection> PropertyCache = new ConcurrentDictionary<Type, PropertyDescriptorCollection>();
        private static readonly ConcurrentDictionary<Type, FieldInfo[]> FieldCache = new ConcurrentDictionary<Type, FieldInfo[]>();

        private static bool enableCaching = true;

        public static bool EnableCaching
        {
            get
            {
                return enableCaching;
            }
            set
            {
                if (!(enableCaching = value))
                {
                    Clear();
                }
            }
        }

        internal static void Clear()
        {
            PropertyCache.Clear();
            FieldCache.Clear();
        }

        internal static FieldInfo[] GetCachedFields(this object container)
        {
            Type containerType = container.GetType();
            if (enableCaching)
            {
                FieldInfo[] fields = null;
                if (!FieldCache.TryGetValue(containerType, out fields))
                {
                    fields = containerType.GetFields();
                    FieldCache.TryAdd(containerType, fields);
                }
                return fields;
            }

            return container.GetType().GetFields();
        }

        internal static PropertyDescriptorCollection GetCachedProperties(this object container)
        {
            if (EnableCaching && !(container is ICustomTypeDescriptor))
            {
                PropertyDescriptorCollection properties = null;
                Type containerType = container.GetType();
                if (!PropertyCache.TryGetValue(containerType, out properties))
                {
                    properties = TypeDescriptor.GetProperties(containerType);
                    PropertyCache.TryAdd(containerType, properties);
                }
                return properties;
            }

            return TypeDescriptor.GetProperties(container);
        }

        internal static FieldInfo FindCachedField(this object container, string fieldName)
        {
            return GetCachedFields(container).FirstOrDefault(p => p.Name == fieldName);
        }

        internal static PropertyDescriptor FindCachedProperty(this object container, string propertyName)
        {
            return GetCachedProperties(container).Find(propertyName, true);
        }

        internal static object GetDataMemberValue(this object container, string expression)
        {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            expression = expression.Trim();

            if (expression.Length == 0) {
                throw new ArgumentNullException("expression");
            }

            if (container == null) {
                return null;
            }

            string[] expressionParts = expression.Split(ExpressionPartSeparator);

            object member;
            int i;

            for (member = container, i = 0; (i < expressionParts.Length) && (member != null); i++)
            {
                member = GetMemberValue(member, expressionParts[i]);
            }

            return member;
        }

        private static object GetMemberValue(object container, string memberName)
        {
            if (container == null) {
                throw new ArgumentNullException("container");
            }
            if (String.IsNullOrEmpty(memberName)) {
                throw new ArgumentNullException("memberName");
            }
            Contract.EndContractBlock();

            PropertyDescriptor pd = container.FindCachedProperty(memberName);
            if (pd != null)
            {
                return pd.GetValue(container);
            }

            FieldInfo field = container.FindCachedField(memberName);
            if (field != null)
            {
                return field.GetValue(container);
            }

            throw new ArgumentException(string.Format("Type {0} has no property or field {1}", container.GetType().FullName, memberName));
        }
    }
}
