// Decompiled with JetBrains decompiler
// Type: System.SerializableAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает, что класс может быть сериализован.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate, Inherited = false)]
  [ComVisible(true)]
  public sealed class SerializableAttribute : Attribute
  {
    internal static Attribute GetCustomAttribute(RuntimeType type)
    {
      if ((type.Attributes & TypeAttributes.Serializable) != TypeAttributes.Serializable)
        return (Attribute) null;
      return (Attribute) new SerializableAttribute();
    }

    internal static bool IsDefined(RuntimeType type)
    {
      return type.IsSerializable;
    }
  }
}
