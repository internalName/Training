// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryHive
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
  /// <summary>
  ///   Представляет возможные значения для узла верхнего уровня на чужом компьютере.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum RegistryHive
  {
    ClassesRoot = -2147483648, // -0x80000000
    CurrentUser = -2147483647, // -0x7FFFFFFF
    LocalMachine = -2147483646, // -0x7FFFFFFE
    Users = -2147483645, // -0x7FFFFFFD
    PerformanceData = -2147483644, // -0x7FFFFFFC
    CurrentConfig = -2147483643, // -0x7FFFFFFB
    DynData = -2147483642, // -0x7FFFFFFA
  }
}
