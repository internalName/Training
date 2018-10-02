// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.PARAMDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Содержит информацию о порядке передачи элемента структуры, параметра или возвращаемого функцией значения из одного процесса в другой.
  /// </summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct PARAMDESC
  {
    /// <summary>
    ///   Представляет указатель на значение, передаваемое между процессами.
    /// </summary>
    public IntPtr lpVarValue;
    /// <summary>
    ///   Представляет значения битовой маски, описывающие структуру элемента, параметра или возвращаемого значения.
    /// </summary>
    [__DynamicallyInvokable]
    public PARAMFLAG wParamFlags;
  }
}
