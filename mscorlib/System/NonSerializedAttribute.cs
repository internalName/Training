// Decompiled with JetBrains decompiler
// Type: System.NonSerializedAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает, что поле сериализуемого класса не должно быть сериализовано.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  public sealed class NonSerializedAttribute : Attribute
  {
    internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
    {
      if ((field.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope)
        return (Attribute) null;
      return (Attribute) new NonSerializedAttribute();
    }

    internal static bool IsDefined(RuntimeFieldInfo field)
    {
      return (uint) (field.Attributes & FieldAttributes.NotSerialized) > 0U;
    }
  }
}
