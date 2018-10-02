// Decompiled with JetBrains decompiler
// Type: System.Configuration.Assemblies.AssemblyHashAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
  /// <summary>
  ///   Указывает все алгоритмы хэширования, используемый для хэширования файлов и генерации строгого имени.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum AssemblyHashAlgorithm
  {
    None = 0,
    MD5 = 32771, // 0x00008003
    SHA1 = 32772, // 0x00008004
    [ComVisible(false)] SHA256 = 32780, // 0x0000800C
    [ComVisible(false)] SHA384 = 32781, // 0x0000800D
    [ComVisible(false)] SHA512 = 32782, // 0x0000800E
  }
}
