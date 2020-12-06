using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyInformation
{
    public class AccessSpecificators
    {
        public static string MethodSpecificator(MethodInfo method)
        {
            string result = "";

            if (method.IsPrivate)
                result += "private ";
            if (method.IsFamily)
                result += "protected ";
            if (method.IsFamilyOrAssembly)
                result += "protected internal ";
            if (method.IsAssembly)
                result += "internal ";
            if (method.IsPublic)
                result += "public ";
            if (method.IsStatic)
                result += "static ";
            if (method.IsAbstract)
                result += "abstract ";
            if (method.IsVirtual)
                result += "virtual ";
            return result;
        }

        public static string ClassSpecificator(Type type)
        {
            string result = "";

            if (type.IsNestedPublic)
                result += "public ";
            if (type.IsAbstract)
                result += "abstract ";
            if (type.IsNestedFamily)
                result += "protected ";
            if (type.IsNestedPrivate)
                result += "private ";
            return result;
        }

        public static string fieldSpecificator(FieldInfo field)
        {
            string result = "";

            if (field.IsPrivate)
                result += "private ";
            if (field.IsFamily)
                result += "protected ";
            if (field.IsFamilyOrAssembly)
                result += "protected internal ";
            if (field.IsAssembly)
                result += "internal ";
            if (field.IsPublic)
                result += "public ";
            if (field.IsStatic)
                result += "static ";

            return result;

        }

        /*public static string propertySpecificator(PropertyInfo prop)
        {
            string result = "";
            if()
        }/*/

    }

}
