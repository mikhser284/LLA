using System;


namespace LLA.Core
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class UiTextColorAttribute : Attribute
    {
        public Int32 Hue { get; }

        public Int32 Saturation { get; }

        public Int32 Value { get; }

        public Int32 Alpha { get; }

        public UiTextColorAttribute(Int32 hue, Int32 saturation, Int32 value, Int32 alpla = 255)
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
            Alpha = alpla;
        }
    }
}
