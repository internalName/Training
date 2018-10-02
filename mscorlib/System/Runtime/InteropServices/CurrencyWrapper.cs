// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CurrencyWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Инкапсулирует объекты, которые необходимо маршалировать, как <see langword="VT_CY" />.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class CurrencyWrapper
  {
    private Decimal m_WrappedObject;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> класса <see langword="Decimal" /> для перезаписи и маршалинга в качестве типа <see langword="VT_CY" />.
    /// </summary>
    /// <param name="obj">
    ///   <see langword="Decimal" /> Для перезаписи и маршалинга в качестве <see langword="VT_CY" />.
    /// </param>
    [__DynamicallyInvokable]
    public CurrencyWrapper(Decimal obj)
    {
      this.m_WrappedObject = obj;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> класса с объектом, содержащим <see langword="Decimal" /> для перезаписи и маршалинга в качестве типа <see langword="VT_CY" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект, содержащий <see langword="Decimal" /> для перезаписи и маршалинга в качестве <see langword="VT_CY" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="obj" /> Параметр не <see cref="T:System.Decimal" /> типа.
    /// </exception>
    [__DynamicallyInvokable]
    public CurrencyWrapper(object obj)
    {
      if (!(obj is Decimal))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDecimal"), nameof (obj));
      this.m_WrappedObject = (Decimal) obj;
    }

    /// <summary>
    ///   Возвращает инкапсулированный объект, который должен быть маршалирован как тип <see langword="VT_CY" />.
    /// </summary>
    /// <returns>
    ///   Объект оболочки для маршалинга в качестве типа <see langword="VT_CY" />.
    /// </returns>
    [__DynamicallyInvokable]
    public Decimal WrappedObject
    {
      [__DynamicallyInvokable] get
      {
        return this.m_WrappedObject;
      }
    }
  }
}
