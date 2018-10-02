// Decompiled with JetBrains decompiler
// Type: System.STAThreadAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает, что потоковой моделью COM для приложения является однопотоковое подразделение (STA).
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class STAThreadAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.STAThreadAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public STAThreadAttribute()
    {
    }
  }
}
