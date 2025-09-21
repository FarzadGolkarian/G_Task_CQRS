using G_Task.Common.Exceptions;
using System.ComponentModel;
using System.Reflection;

namespace G_Task.Common.Helpers
{
    public static class EnumHelper
    {

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();

            FieldInfo field = type.GetField(value.ToString());

            if (field == null) return string.Empty;        

            DescriptionAttribute[] array 
                = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
          
            if (array.Length > 0) return array[0].Description;            

            return Enum.GetName(type, value).Trim();

        }

        public static void ValidationEnumDefined(Type enumType, Enum ename, string value)
        {
            if (!enumType.IsEnumDefined(ename))
               
                throw new KeyNotFoundException(string.Format(ErrorMessages.ValidationEnumType, value));
        }
    }
}
