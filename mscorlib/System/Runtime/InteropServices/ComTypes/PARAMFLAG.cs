// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.PARAMFLAG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Описывает порядок передачи элемента структуры, параметра или возвращаемого функцией значения из одного процесса в другой.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum PARAMFLAG : short
  {
    [__DynamicallyInvokable] PARAMFLAG_NONE = 0,
    [__DynamicallyInvokable] PARAMFLAG_FIN = 1,
    [__DynamicallyInvokable] PARAMFLAG_FOUT = 2,
    [__DynamicallyInvokable] PARAMFLAG_FLCID = 4,
    [__DynamicallyInvokable] PARAMFLAG_FRETVAL = 8,
    [__DynamicallyInvokable] PARAMFLAG_FOPT = 16, // 0x0010
    [__DynamicallyInvokable] PARAMFLAG_FHASDEFAULT = 32, // 0x0020
    [__DynamicallyInvokable] PARAMFLAG_FHASCUSTDATA = 64, // 0x0040
  }
}
