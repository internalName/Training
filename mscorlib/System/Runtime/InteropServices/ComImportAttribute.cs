// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComImportAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, что тип с атрибутом был ранее определен в модели COM.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComImportAttribute : Attribute
  {
    internal static Attribute GetCustomAttribute(RuntimeType type)
    {
      if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.NotPublic)
        return (Attribute) null;
      return (Attribute) new ComImportAttribute();
    }

    internal static bool IsDefined(RuntimeType type)
    {
      return (uint) (type.Attributes & TypeAttributes.Import) > 0U;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ComImportAttribute()
    {
    }
  }
}
