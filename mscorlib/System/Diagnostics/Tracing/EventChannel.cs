// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
  /// <summary>Указывает канал журнала событий для события.</summary>
  [FriendAccessAllowed]
  [__DynamicallyInvokable]
  public enum EventChannel : byte
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] Admin = 16, // 0x10
    [__DynamicallyInvokable] Operational = 17, // 0x11
    [__DynamicallyInvokable] Analytic = 18, // 0x12
    [__DynamicallyInvokable] Debug = 19, // 0x13
  }
}
