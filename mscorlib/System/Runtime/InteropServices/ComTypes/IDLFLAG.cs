// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IDLFLAG
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
  public enum IDLFLAG : short
  {
    [__DynamicallyInvokable] IDLFLAG_NONE = 0,
    [__DynamicallyInvokable] IDLFLAG_FIN = 1,
    [__DynamicallyInvokable] IDLFLAG_FOUT = 2,
    [__DynamicallyInvokable] IDLFLAG_FLCID = 4,
    [__DynamicallyInvokable] IDLFLAG_FRETVAL = 8,
  }
}
