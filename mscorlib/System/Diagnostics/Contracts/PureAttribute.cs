// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.PureAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>
  ///   Указывает, что тип или метод является чистым, то есть не вносит изменения в состояние видимости.
  /// </summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = false, Inherited = true)]
  [__DynamicallyInvokable]
  public sealed class PureAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Contracts.PureAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public PureAttribute()
    {
    }
  }
}
