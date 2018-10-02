// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;

namespace Microsoft.Win32
{
  /// <summary>
  ///   Задает параметры для использования при создании раздела реестра.
  /// </summary>
  [Flags]
  public enum RegistryOptions
  {
    None = 0,
    Volatile = 1,
  }
}
