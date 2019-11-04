using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace LLA.Core
{
    public class EnumInfo
    {
        private Dictionary<Enum, EnumItemInfo> _enum_DisplayName;
        private Dictionary<String, Enum> _displayName_Enum;

        private List<String> _displayNames = null;

        public List<String> DisplayNames
        {
            get
            {
                return _displayNames ??= _enum_DisplayName
                    .OrderBy(x => x.Value.Order).ThenBy(x => x.Value)
                    .Select(x => x.Value.DisplayName).ToList();
            }
        }

        private List<String> _userAvailableDisplayNames = null;

        public List<String> UiAvailableDisplayNames
        {
            get
            {
                return _userAvailableDisplayNames ??= _enum_DisplayName
                    .Where(x => x.Value.CanBeSelectedByUser == true)
                    .OrderBy(x => x.Value.Order).ThenBy(x => x.Value)
                    .Select(x => x.Value.DisplayName)
                    .ToList();
            }
        }

        public EnumInfo(Type enumType)
        {
            _enum_DisplayName = new Dictionary<Enum, EnumItemInfo>();
            _displayName_Enum = new Dictionary<String, Enum>();
            //
            Array enumValues = System.Enum.GetValues(enumType);
            foreach (var enumVal in enumValues)
            {
                MemberInfo memberInfo = enumType.GetMember(enumVal.ToString()).FirstOrDefault();
                if (memberInfo == null) continue;
                Object enumAttribute = memberInfo.GetCustomAttributes(typeof(UiNameAttribute), false).FirstOrDefault();
                Add((Enum)enumVal, new EnumItemInfo((UiNameAttribute)enumAttribute));
            }
        }

        private Boolean Add(Enum enumValue, EnumItemInfo enumItemInfo)
        {
            if (_enum_DisplayName.ContainsKey(enumValue)) return false;
            _enum_DisplayName.Add(enumValue, enumItemInfo);
            if (_displayName_Enum.ContainsKey(enumItemInfo.DisplayName))
                throw new InvalidOperationException($"Перечисление \"{enumValue.GetType()}\" уже содержит отображаемое значение \"{enumItemInfo}\"");
            _displayName_Enum.Add(enumItemInfo.DisplayName, enumValue);
            return true;
        }

        public String GetDisplayName(Enum enumValue) => _enum_DisplayName[enumValue].DisplayName;

        public Enum GetEnumValue(Type enumType, String enumDisplayName)
        {
            return _displayName_Enum[enumDisplayName];
        }
    }

    
}
