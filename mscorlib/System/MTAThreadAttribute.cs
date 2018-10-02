// Decompiled with JetBrains decompiler
// Type: System.MTAThreadAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает на то, что потоковой моделью COM для приложения является многопотоковое подразделение (MTA).
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class MTAThreadAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MTAThreadAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public MTAThreadAttribute()
    {
    }
  }
}
