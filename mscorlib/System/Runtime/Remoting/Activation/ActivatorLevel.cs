// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.ActivatorLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>
  ///   Определяет подходящую позицию для <see cref="T:System.Activator" /> в цепи активаторов.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum ActivatorLevel
  {
    Construction = 4,
    Context = 8,
    AppDomain = 12, // 0x0000000C
    Process = 16, // 0x00000010
    Machine = 20, // 0x00000014
  }
}
