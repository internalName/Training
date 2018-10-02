// Decompiled with JetBrains decompiler
// Type: System.Reflection.PortableExecutableKinds
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Определяет природу кода в исполняемом файле.</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum PortableExecutableKinds
  {
    NotAPortableExecutableImage = 0,
    ILOnly = 1,
    Required32Bit = 2,
    PE32Plus = 4,
    Unmanaged32Bit = 8,
    [ComVisible(false)] Preferred32Bit = 16, // 0x00000010
  }
}
