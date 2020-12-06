using System;
using System.Reflection;

namespace Faker
{
    public class AdditionalFunction
    {
        public static bool IsFilled(MemberInfo fillingMember, object obj)
        {

            switch (fillingMember.MemberType)
            {
                case MemberTypes.Property:
                    {
                        if (!((PropertyInfo)fillingMember).CanRead)
                            return false;
                        object TargetValue = ((PropertyInfo)fillingMember).GetValue(obj);
                        object DefaultValue = GetDefaultValue(((PropertyInfo)fillingMember).PropertyType);
                        return !Equals(DefaultValue, TargetValue);
                    }
                case MemberTypes.Field:
                    {
                        object TargetValue = ((FieldInfo)fillingMember).GetValue(obj);
                        object DefaultValue = GetDefaultValue(((FieldInfo)fillingMember).FieldType);
                        return !Equals(DefaultValue, TargetValue);
                    }
            }
            return false;
        }

        public static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            else
                return null;
        }

        public static string ProceedParameterName(String name)
        {
            string tmp = name.Remove(0, 1);
            name = name.ToUpper();
            name = name.Remove(1, name.Length - 1);
            return name + tmp;
        }
    }
}
