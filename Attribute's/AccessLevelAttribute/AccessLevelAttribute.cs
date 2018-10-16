using System;

namespace AccessLevelAttribute
{
    [AttributeUsage(AttributeTargets.Class),Serializable]
   public sealed class AccessLevelAttribute:Attribute
   {
       private readonly EnumAccessLevel? _accessValue;

       public string User { get; set; } = null;

       public EnumAccessLevel? AccessLevel => _accessValue;

       public AccessLevelAttribute(EnumAccessLevel accessLevel)=> _accessValue = accessLevel;
    }
}
