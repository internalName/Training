// Decompiled with JetBrains decompiler
// Type: System.Reflection.IReflectableType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>
  ///   Представляет тип, по которому можно выполнять отражение.
  /// </summary>
  [__DynamicallyInvokable]
  public interface IReflectableType
  {
    /// <summary>Извлекает объект, который представляет этот тип.</summary>
    /// <returns>Объект, представляющий этот тип.</returns>
    [__DynamicallyInvokable]
    TypeInfo GetTypeInfo();
  }
}
