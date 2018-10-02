// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.GCHandleType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Представляет типы дескрипторов <see cref="T:System.Runtime.InteropServices.GCHandle" /> класса можно выделить.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum GCHandleType
  {
    [__DynamicallyInvokable] Weak,
    [__DynamicallyInvokable] WeakTrackResurrection,
    [__DynamicallyInvokable] Normal,
    [__DynamicallyInvokable] Pinned,
  }
}
