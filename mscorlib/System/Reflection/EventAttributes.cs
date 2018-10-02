// Decompiled with JetBrains decompiler
// Type: System.Reflection.EventAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Задает атрибуты события.</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum EventAttributes
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] SpecialName = 512, // 0x00000200
    ReservedMask = 1024, // 0x00000400
    [__DynamicallyInvokable] RTSpecialName = ReservedMask, // 0x00000400
  }
}
