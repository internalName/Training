// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ProgIdAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Позволяет пользователю задавать идентификатор ProgID класса.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = false)]
  [ComVisible(true)]
  public sealed class ProgIdAttribute : Attribute
  {
    internal string _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="ProgIdAttribute" /> с указанным идентификатором ProgID.
    /// </summary>
    /// <param name="progId">
    ///   Идентификатор ProgID для назначения класса.
    /// </param>
    public ProgIdAttribute(string progId)
    {
      this._val = progId;
    }

    /// <summary>Возвращает идентификатор ProgID класса.</summary>
    /// <returns>Идентификатор ProgID класса.</returns>
    public string Value
    {
      get
      {
        return this._val;
      }
    }
  }
}
