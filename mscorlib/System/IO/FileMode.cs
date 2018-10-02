// Decompiled with JetBrains decompiler
// Type: System.IO.FileMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>
  ///   Указывает, каким образом операционная система должна открыть файл.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum FileMode
  {
    CreateNew = 1,
    Create = 2,
    Open = 3,
    OpenOrCreate = 4,
    Truncate = 5,
    Append = 6,
  }
}
