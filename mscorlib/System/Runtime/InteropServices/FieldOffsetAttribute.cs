// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.FieldOffsetAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает физическое расположение полей в неуправляемом представлении класса или структуры.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class FieldOffsetAttribute : Attribute
  {
    internal int _val;

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
    {
      int offset;
      if (field.DeclaringType != (Type) null && field.GetRuntimeModule().MetadataImport.GetFieldOffset(field.DeclaringType.MetadataToken, field.MetadataToken, out offset))
        return (Attribute) new FieldOffsetAttribute(offset);
      return (Attribute) null;
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeFieldInfo field)
    {
      return FieldOffsetAttribute.GetCustomAttribute(field) != null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.FieldOffsetAttribute" /> класс со смещением в структуре к началу поля.
    /// </summary>
    /// <param name="offset">
    ///   Смещение в байтах от начала структуры до начала поля.
    /// </param>
    [__DynamicallyInvokable]
    public FieldOffsetAttribute(int offset)
    {
      this._val = offset;
    }

    /// <summary>
    ///   Возвращает смещение от начала структуры до начала поля.
    /// </summary>
    /// <returns>Смещение от начала структуры до начала поля.</returns>
    [__DynamicallyInvokable]
    public int Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}
