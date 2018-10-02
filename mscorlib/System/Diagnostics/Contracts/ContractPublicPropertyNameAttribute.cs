// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractPublicPropertyNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>
  ///   Указывает, что поле может использоваться в контрактах методов, если видимость поля меньше, чем видимость метода.
  /// </summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Field)]
  [__DynamicallyInvokable]
  public sealed class ContractPublicPropertyNameAttribute : Attribute
  {
    private string _publicName;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Contracts.ContractPublicPropertyNameAttribute" />.
    /// </summary>
    /// <param name="name">Имя свойства, применяемые к полю.</param>
    [__DynamicallyInvokable]
    public ContractPublicPropertyNameAttribute(string name)
    {
      this._publicName = name;
    }

    /// <summary>Возвращает имя свойства, применяемые к полю.</summary>
    /// <returns>Имя свойства, применяемые к полю.</returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this._publicName;
      }
    }
  }
}
