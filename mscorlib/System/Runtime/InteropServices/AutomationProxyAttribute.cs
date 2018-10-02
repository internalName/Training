// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.AutomationProxyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, следует ли маршалировать типа с помощью автоматического маршалера или пользовательского прокси и заглушки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  public sealed class AutomationProxyAttribute : Attribute
  {
    internal bool _val;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.AutomationProxyAttribute" />.
    /// </summary>
    /// <param name="val">
    ///   <see langword="true" /> Если маршалинг класса должна выполняться с помощью автоматического маршалера. <see langword="false" /> Если нужно использовать маршалер заглушки прокси-сервера.
    /// </param>
    public AutomationProxyAttribute(bool val)
    {
      this._val = val;
    }

    /// <summary>
    ///   Возвращает значение, показывающее тип используемого маршалера.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если маршалинг класса должна выполняться с помощью автоматического маршалера. <see langword="false" /> Если нужно использовать маршалер заглушки прокси-сервера.
    /// </returns>
    public bool Value
    {
      get
      {
        return this._val;
      }
    }
  }
}
