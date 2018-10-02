// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventOpcode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Определяет стандартные коды операций, источник события присоединяет к событиям.
  /// </summary>
  [FriendAccessAllowed]
  [__DynamicallyInvokable]
  public enum EventOpcode
  {
    [__DynamicallyInvokable] Info = 0,
    [__DynamicallyInvokable] Start = 1,
    [__DynamicallyInvokable] Stop = 2,
    [__DynamicallyInvokable] DataCollectionStart = 3,
    [__DynamicallyInvokable] DataCollectionStop = 4,
    [__DynamicallyInvokable] Extension = 5,
    [__DynamicallyInvokable] Reply = 6,
    [__DynamicallyInvokable] Resume = 7,
    [__DynamicallyInvokable] Suspend = 8,
    [__DynamicallyInvokable] Send = 9,
    [__DynamicallyInvokable] Receive = 240, // 0x000000F0
  }
}
