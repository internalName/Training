// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ExtensionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, что метод является методом расширения либо что класс или сборка содержат методы расширения.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
  [__DynamicallyInvokable]
  public sealed class ExtensionAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.ExtensionAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ExtensionAttribute()
    {
    }
  }
}
