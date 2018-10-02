// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibVarAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Содержит <see cref="T:System.Runtime.InteropServices.VARFLAGS" /> ранее импортированные для данного поля из библиотеки COM-типов.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  public sealed class TypeLibVarAttribute : Attribute
  {
    internal TypeLibVarFlags _val;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.TypeLibVarAttribute" /> заданным значением <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" />.
    /// </summary>
    /// <param name="flags">
    ///   <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> Значение для поля с атрибутом найден в библиотеке типов, он был импортирован.
    /// </param>
    public TypeLibVarAttribute(TypeLibVarFlags flags)
    {
      this._val = flags;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.TypeLibVarAttribute" /> заданным значением <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" />.
    /// </summary>
    /// <param name="flags">
    ///   <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> Значение для поля с атрибутом найден в библиотеке типов, он был импортирован.
    /// </param>
    public TypeLibVarAttribute(short flags)
    {
      this._val = (TypeLibVarFlags) flags;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> значение для этого поля.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> Значение для этого поля.
    /// </returns>
    public TypeLibVarFlags Value
    {
      get
      {
        return this._val;
      }
    }
  }
}
