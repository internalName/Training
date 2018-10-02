// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IDLFLAG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IDLFLAG" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum IDLFLAG : short
  {
    IDLFLAG_NONE = 0,
    IDLFLAG_FIN = 1,
    IDLFLAG_FOUT = 2,
    IDLFLAG_FLCID = 4,
    IDLFLAG_FRETVAL = 8,
  }
}
