using System;
using System.ComponentModel;
using System.Linq;

namespace SFA.DAS.ASK.Application.Utils
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            if (value == null)
            {
                return "";
            }
            
            var fi = value.GetType().GetField(value.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}