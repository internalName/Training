// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventManifestOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Определяет способ создания манифеста трассировки событий Windows для источника событий.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum EventManifestOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] Strict = 1,
    [__DynamicallyInvokable] AllCultures = 2,
    [__DynamicallyInvokable] OnlyIfNeededForRegistration = 4,
    [__DynamicallyInvokable] AllowEventSourceOverride = 8,
  }
}
