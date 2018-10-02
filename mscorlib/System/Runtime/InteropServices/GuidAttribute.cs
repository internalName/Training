// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.GuidAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет явный <see cref="T:System.Guid" /> при автоматического идентификатора GUID нежелательно.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class GuidAttribute : Attribute
  {
    internal string _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.GuidAttribute" /> класса с указанным идентификатором GUID.
    /// </summary>
    /// <param name="guid">
    ///   <see cref="T:System.Guid" /> Могут быть назначены.
    /// </param>
    [__DynamicallyInvokable]
    public GuidAttribute(string guid)
    {
      this._val = guid;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Guid" /> класса.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Guid" /> Класса.
    /// </returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}
