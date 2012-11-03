// Copyright (c)  2012 QuadAutomotive Group.
// All rights reserved.
// 
// Redistribution and use in source and binary forms are permitted
// provided that the above copyright notice and this paragraph are
// duplicated in all such forms and that any documentation,
// advertising materials, and other materials related to such
// distribution and use acknowledge that the software was developed
// by the <organization>.  The name of the
// University may not be used to endorse or promote products derived
// from this software without specific prior written permission.
// THIS SOFTWARE IS PROVIDED ``AS IS'' AND WITHOUT ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace QuadAuto.Server.Domain.MassTransit
{
    internal class PrivateReflectionDynamicObject : DynamicObject
    {
        private static readonly IDictionary<Type, IDictionary<string, IProperty>> PropertiesOnType =
            new ConcurrentDictionary<Type, IDictionary<string, IProperty>>();

        private interface IProperty
        {
            string Name { get; }
            object GetValue(object obj, object[] index);
            void SetValue(object obj, object val, object[] index);
        }

        private class Property : IProperty
        {
            internal PropertyInfo PropertyInfo { private get; set; }

            string IProperty.Name
            {
                get { return PropertyInfo.Name; }
            }

            object IProperty.GetValue(object obj, object[] index)
            {
                return PropertyInfo.GetValue(obj, index);
            }

            void IProperty.SetValue(object obj, object val, object[] index)
            {
                PropertyInfo.SetValue(obj, val, index);
            }
        }

        private class Field : IProperty
        {
            internal FieldInfo FieldInfo { private get; set; }

            string IProperty.Name
            {
                get { return FieldInfo.Name; }
            }


            object IProperty.GetValue(object obj, object[] index)
            {
                return FieldInfo.GetValue(obj);
            }

            void IProperty.SetValue(object obj, object val, object[] index)
            {
                FieldInfo.SetValue(obj, val);
            }
        }


        private object RealObject { get; set; }
        private const BindingFlags BindingFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        internal static object WrapObjectIfNeeded(object o)
        {
            if (o == null || o.GetType().IsPrimitive || o is string)
                return o;

            return new PrivateReflectionDynamicObject {RealObject = o};
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var prop = GetProperty(binder.Name);
            result = prop.GetValue(RealObject, null);
            result = WrapObjectIfNeeded(result);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var prop = GetProperty(binder.Name);
            prop.SetValue(RealObject, value, null);
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var prop = GetIndexProperty();
            result = prop.GetValue(RealObject, indexes);
            result = WrapObjectIfNeeded(result);
            return true;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            var prop = GetIndexProperty();
            prop.SetValue(RealObject, value, indexes);
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = InvokeMemberOnType(RealObject.GetType(), RealObject, binder.Name, args);
            result = WrapObjectIfNeeded(result);
            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = Convert.ChangeType(RealObject, binder.Type);
            return true;
        }

        public override string ToString()
        {
            return RealObject.ToString();
        }

        private IProperty GetIndexProperty()
        {
            return GetProperty("Item");
        }

        private IProperty GetProperty(string propertyName)
        {
            var typeProperties = GetTypeProperties(RealObject.GetType());

            IProperty property;
            if (typeProperties.TryGetValue(propertyName, out property))
                return property;

            var propNames = typeProperties.Keys.Where(name => name[0] != '<').OrderBy(name => name);
            throw new ArgumentException(
                String.Format("The property {0} doesn't exist on type {1}. Supported properties are: {2}", propertyName,
                              RealObject.GetType(), String.Join(", ", propNames)));
        }

        private static IDictionary<string, IProperty> GetTypeProperties(Type type)
        {
            IDictionary<string, IProperty> typeProperties;
            if (PropertiesOnType.TryGetValue(type, out typeProperties))
                return typeProperties;

            typeProperties = new ConcurrentDictionary<string, IProperty>();

            foreach (var prop in type.GetProperties(BindingFlag).Where(p => p.DeclaringType == type))
                typeProperties[prop.Name] = new Property {PropertyInfo = prop};

            foreach (var field in type.GetFields(BindingFlag).Where(p => p.DeclaringType == type))
                typeProperties[field.Name] = new Field {FieldInfo = field};

            if (type.BaseType != null)
                foreach (var prop in GetTypeProperties(type.BaseType).Values)
                    typeProperties[prop.Name] = prop;

            PropertiesOnType[type] = typeProperties;

            return typeProperties;
        }

        private static object InvokeMemberOnType(Type type, object target, string name, object[] args)
        {
            try
            {
                return type.InvokeMember(name, BindingFlags.InvokeMethod | BindingFlag, null, target, args);
            }
            catch (MissingMethodException)
            {
                return type.BaseType != null ? InvokeMemberOnType(type.BaseType, target, name, args) : null;
            }
        }
    }
}