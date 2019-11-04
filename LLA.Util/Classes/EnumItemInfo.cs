using System;


namespace LLA.Core
{
    public class EnumItemInfo
    {
        public Int32? Order { get; }

        public String DisplayName { get; }

        public Boolean CanBeSelectedByUser { get; }

        public EnumItemInfo(UiNameAttribute uiListItem)
        {
            //Order = uiListItem?.Order;
            DisplayName = uiListItem?.Value;
            //CanBeSelectedByUser = uiListItem?.CanBeSelectedByUser ?? true;
        }
    }

    
}
