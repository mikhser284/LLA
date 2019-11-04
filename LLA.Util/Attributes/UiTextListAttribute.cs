using System;
using System.Drawing;


namespace LLA.Core
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class UiTextListAttribute : Attribute
    {
        public Int32? Order { get; }

        public Boolean CanBeSelectedByUser { get; }

        public String FontName { get; set; }

        public Int32? FontSize { get; set; }

        public ETextAlignment TextAlignment { get; set; } = ETextAlignment.Left;

        

        public UiTextListAttribute(Boolean canBeSelectedByUser = true)
        {
            CanBeSelectedByUser = canBeSelectedByUser;
        }

        public UiTextListAttribute(Int32 order, Boolean canBeSelectedByUser = true) : this(canBeSelectedByUser)
        {
            Order = order;
        }
    }
}
