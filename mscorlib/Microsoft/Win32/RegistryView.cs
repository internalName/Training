// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryView
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace Microsoft.Win32
{
  /// <summary>
  ///   Задает представление реестра, которое целевой объект на 64-разрядной операционной системе.
  /// </summary>
  public enum RegistryView
  {
    Default = 0,
    Registry64 = 256, // 0x00000100
    Registry32 = 512, // 0x00000200
  }
}
