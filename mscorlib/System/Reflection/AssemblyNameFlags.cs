// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyNameFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Содержит сведения о <see cref="T:System.Reflection.Assembly" /> ссылки.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum AssemblyNameFlags
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] PublicKey = 1,
    EnableJITcompileOptimizer = 16384, // 0x00004000
    EnableJITcompileTracking = 32768, // 0x00008000
    [__DynamicallyInvokable] Retargetable = 256, // 0x00000100
  }
}
