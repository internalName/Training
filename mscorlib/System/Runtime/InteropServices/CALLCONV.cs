// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CALLCONV
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.CALLCONV" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.CALLCONV instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Serializable]
  public enum CALLCONV
  {
    CC_CDECL = 1,
    CC_MSCPASCAL = 2,
    CC_PASCAL = 2,
    CC_MACPASCAL = 3,
    CC_STDCALL = 4,
    CC_RESERVED = 5,
    CC_SYSCALL = 6,
    CC_MPWCDECL = 7,
    CC_MPWPASCAL = 8,
    CC_MAX = 9,
  }
}
