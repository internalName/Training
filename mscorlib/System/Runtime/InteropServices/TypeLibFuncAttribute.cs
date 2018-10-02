// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibFuncAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Содержит <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> ранее импортированные для данного метода из библиотеки COM-типов.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  public sealed class TypeLibFuncAttribute : Attribute
  {
    internal TypeLibFuncFlags _val;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="TypeLibFuncAttribute" /> заданным значением <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" />.
    /// </summary>
    /// <param name="flags">
    ///   <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> Значение для метода с атрибутом, найденные в библиотеке типов, он был импортирован.
    /// </param>
    public TypeLibFuncAttribute(TypeLibFuncFlags flags)
    {
      this._val = flags;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="TypeLibFuncAttribute" /> заданным значением <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" />.
    /// </summary>
    /// <param name="flags">
    ///   <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> Значение для метода с атрибутом, найденные в библиотеке типов, он был импортирован.
    /// </param>
    public TypeLibFuncAttribute(short flags)
    {
      this._val = (TypeLibFuncFlags) flags;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> значением для данного метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> Значением для данного метода.
    /// </returns>
    public TypeLibFuncFlags Value
    {
      get
      {
        return this._val;
      }
    }
  }
}
