// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DisablePrivateReflectionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, что все закрытые члены, содержащиеся в типах сборки, недоступны для отражения.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class DisablePrivateReflectionAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новые экземпляры класса <see cref="T:System.Runtime.CompilerServices.DisablePrivateReflectionAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public DisablePrivateReflectionAttribute()
    {
    }
  }
}
