using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Common
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null) return null;
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static UnitType GetEnum(this string value)
        {
            UnitType type=UnitType.CPU;
            foreach (UnitType item in Enum.GetValues(typeof(UnitType)))
            {
                if (item.GetDescription() == value)
                {
                    type = item;
                    break;
                }
            }
            return type;
        }
    }
}
