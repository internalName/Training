// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractOptionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>
  ///   Позволяет задать параметры контракта и средства на уровне гранулярности сборки, типа или метода.
  /// </summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
  [Conditional("CONTRACTS_FULL")]
  [__DynamicallyInvokable]
  public sealed class ContractOptionAttribute : Attribute
  {
    private string _category;
    private string _setting;
    private bool _enabled;
    private string _value;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> используя указанную категорию, настройку и включение или отключение значение.
    /// </summary>
    /// <param name="category">
    ///   Категория параметр должен иметь значение.
    /// </param>
    /// <param name="setting">Значение параметра.</param>
    /// <param name="enabled">
    ///   <see langword="true" /> для включения параметра; <see langword="false" /> отключить возможность.
    /// </param>
    [__DynamicallyInvokable]
    public ContractOptionAttribute(string category, string setting, bool enabled)
    {
      this._category = category;
      this._setting = setting;
      this._enabled = enabled;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> класса с помощью предоставленной категорией, параметр и значение.
    /// </summary>
    /// <param name="category">
    ///   Категория параметр должен иметь значение.
    /// </param>
    /// <param name="setting">Значение параметра.</param>
    /// <param name="value">Значение параметра.</param>
    [__DynamicallyInvokable]
    public ContractOptionAttribute(string category, string setting, string value)
    {
      this._category = category;
      this._setting = setting;
      this._value = value;
    }

    /// <summary>Возвращает категорию параметра.</summary>
    /// <returns>Категория параметра.</returns>
    [__DynamicallyInvokable]
    public string Category
    {
      [__DynamicallyInvokable] get
      {
        return this._category;
      }
    }

    /// <summary>Возвращает значение параметра.</summary>
    /// <returns>Значение для параметра.</returns>
    [__DynamicallyInvokable]
    public string Setting
    {
      [__DynamicallyInvokable] get
      {
        return this._setting;
      }
    }

    /// <summary>Определяет, включена ли параметр.</summary>
    /// <returns>
    ///   <see langword="true" /> Если включен параметр; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Enabled
    {
      [__DynamicallyInvokable] get
      {
        return this._enabled;
      }
    }

    /// <summary>Возвращает значение для параметра.</summary>
    /// <returns>Значение параметра.</returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this._value;
      }
    }
  }
}
