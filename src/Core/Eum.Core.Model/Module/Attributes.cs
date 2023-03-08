using System;
using System.Collections.Generic;
using System.Text;

namespace Eum.Core.Module
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class IgnoreRegisterAttribute : Attribute { }

    public enum RegisterServiceOptions
    {
        Singleton,
        Transient,
        Scoped
    }

    [AttributeUsage(AttributeTargets.Interface)]
    public class RegisterServiceAttribute : Attribute
    {
        public RegisterServiceAttribute(RegisterServiceOptions option = RegisterServiceOptions.Singleton)
        {
            Option = option;
        }

        public RegisterServiceOptions Option { get; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterImplementationAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataTableIgnoreAttribute : Attribute
    {
    }
}
