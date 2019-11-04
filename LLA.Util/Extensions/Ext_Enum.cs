using System;
using System.Collections.Generic;
using System.Text;


namespace LLA.Core
{
    public static class Ext_Enum
    {
        private static Dictionary<Type, EnumInfo> _enumInfos = new Dictionary<Type, EnumInfo>();

        public static String GetDisplayName<T>(this T enumValue) where T : Enum
        {
            Type enumType = enumValue.GetType();
            if (!_enumInfos.ContainsKey(enumType)) _enumInfos.Add(enumType, new EnumInfo(enumType));
            return _enumInfos[enumType].GetDisplayName(enumValue);
        }

        public static List<String> GetDisplayNames<TEnum>() where TEnum : System.Enum
        {
            Type enumType = typeof(TEnum);
            if (!_enumInfos.ContainsKey(enumType)) _enumInfos.Add(enumType, new EnumInfo(enumType));
            return _enumInfos[enumType].DisplayNames;
        }

        public static List<String> GetUiAvailableDisplayNames<TEnum>() where TEnum : System.Enum
        {
            Type enumType = typeof(TEnum);
            if (!_enumInfos.ContainsKey(enumType)) _enumInfos.Add(enumType, new EnumInfo(enumType));
            return _enumInfos[enumType].UiAvailableDisplayNames;
        }

        public static Enum GetEnumValue(Type enumType, String displayName)
        {
            if (!_enumInfos.ContainsKey(enumType)) _enumInfos.Add(enumType, new EnumInfo(enumType));
            return _enumInfos[enumType].GetEnumValue(enumType, displayName);
        }
    }

    
}
