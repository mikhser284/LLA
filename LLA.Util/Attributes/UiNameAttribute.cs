using System;


namespace LLA.Core
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class UiNameAttribute : Attribute
    {
        public String Value { get; }        

        public UiNameAttribute(String value)
        {   
            Value = value;            
        }

        
    }
}
