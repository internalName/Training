// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibFuncFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Описывает исходные параметры <see langword="FUNCFLAGS" /> в библиотеке типов COM, из которой был импортирован данный метод.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TypeLibFuncFlags
  {
    FRestricted = 1,
    FSource = 2,
    FBindable = 4,
    FRequestEdit = 8,
    FDisplayBind = 16, // 0x00000010
    FDefaultBind = 32, // 0x00000020
    FHidden = 64, // 0x00000040
    FUsesGetLastError = 128, // 0x00000080
    FDefaultCollelem = 256, // 0x00000100
    FUiDefault = 512, // 0x00000200
    FNonBrowsable = 1024, // 0x00000400
    FReplaceable = 2048, // 0x00000800
    FImmediateBind = 4096, // 0x00001000
  }
}
