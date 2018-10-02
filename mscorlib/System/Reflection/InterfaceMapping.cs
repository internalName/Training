// Decompiled with JetBrains decompiler
// Type: System.Reflection.InterfaceMapping
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Получает сопоставление интерфейса в фактических методах для класса, реализующего этот интерфейс.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public struct InterfaceMapping
  {
    /// <summary>
    ///   Представляет тип, который использовался для создания отображения интерфейса.
    /// </summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public Type TargetType;
    /// <summary>Показывает тип, представляющий интерфейс.</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public Type InterfaceType;
    /// <summary>Показывает методы, которые реализуют интерфейс.</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public MethodInfo[] TargetMethods;
    /// <summary>Показывает методы, определенные в интерфейсе.</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public MethodInfo[] InterfaceMethods;
  }
}
