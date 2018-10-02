// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibExporterFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>Показывает способ библиотеки типов.</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TypeLibExporterFlags
  {
    None = 0,
    OnlyReferenceRegistered = 1,
    CallerResolvedReferences = 2,
    OldNames = 4,
    ExportAs32Bit = 16, // 0x00000010
    ExportAs64Bit = 32, // 0x00000020
  }
}
