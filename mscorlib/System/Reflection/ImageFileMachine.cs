// Decompiled with JetBrains decompiler
// Type: System.Reflection.ImageFileMachine
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Идентифицирует платформы, исполняемый файл.</summary>
  [ComVisible(true)]
  [Serializable]
  public enum ImageFileMachine
  {
    I386 = 332, // 0x0000014C
    ARM = 452, // 0x000001C4
    IA64 = 512, // 0x00000200
    AMD64 = 34404, // 0x00008664
  }
}
