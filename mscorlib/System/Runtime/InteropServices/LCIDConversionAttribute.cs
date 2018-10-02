// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.LCIDConversionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, что сигнатура неуправляемого метода предполагает наличие параметра идентификатора (LCID) языка.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  public sealed class LCIDConversionAttribute : Attribute
  {
    internal int _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="LCIDConversionAttribute" /> класса с позицией для идентификатора LCID в неуправляемой подписи.
    /// </summary>
    /// <param name="lcid">
    ///   Указывает позицию аргумента LCID в неуправляемой подписи, в которой первым аргументом является 0.
    /// </param>
    public LCIDConversionAttribute(int lcid)
    {
      this._val = lcid;
    }

    /// <summary>
    ///   Возвращает позицию аргумента LCID в неуправляемой подписи.
    /// </summary>
    /// <returns>
    ///   Позиция аргумента LCID в неуправляемой подписи, где первым аргументом является 0.
    /// </returns>
    public int Value
    {
      get
      {
        return this._val;
      }
    }
  }
}
