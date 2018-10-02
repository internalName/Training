// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibTypeFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Описывает исходные параметры <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> в библиотеке типов COM, из которой был импортирован данный тип.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TypeLibTypeFlags
  {
    FAppObject = 1,
    FCanCreate = 2,
    FLicensed = 4,
    FPreDeclId = 8,
    FHidden = 16, // 0x00000010
    FControl = 32, // 0x00000020
    FDual = 64, // 0x00000040
    FNonExtensible = 128, // 0x00000080
    FOleAutomation = 256, // 0x00000100
    FRestricted = 512, // 0x00000200
    FAggregatable = 1024, // 0x00000400
    FReplaceable = 2048, // 0x00000800
    FDispatchable = 4096, // 0x00001000
    FReverseBind = 8192, // 0x00002000
  }
}
