// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibTypeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Содержит <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> ранее импортированные для данного типа из библиотеки COM-типов.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  public sealed class TypeLibTypeAttribute : Attribute
  {
    internal TypeLibTypeFlags _val;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="TypeLibTypeAttribute" /> заданным значением <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" />.
    /// </summary>
    /// <param name="flags">
    ///   <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> Значение для типа с атрибутом найден в библиотеке типов, он был импортирован.
    /// </param>
    public TypeLibTypeAttribute(TypeLibTypeFlags flags)
    {
      this._val = flags;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="TypeLibTypeAttribute" /> заданным значением <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" />.
    /// </summary>
    /// <param name="flags">
    ///   <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> Значение для типа с атрибутом найден в библиотеке типов, он был импортирован.
    /// </param>
    public TypeLibTypeAttribute(short flags)
    {
      this._val = (TypeLibTypeFlags) flags;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> значение для этого типа.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> Значение для этого типа.
    /// </returns>
    public TypeLibTypeFlags Value
    {
      get
      {
        return this._val;
      }
    }
  }
}
