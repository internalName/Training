// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ImporterEventKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Описывает обратные вызовы, выполняемые импортером библиотеки типов при импорте библиотеки типов.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum ImporterEventKind
  {
    NOTIF_TYPECONVERTED,
    NOTIF_CONVERTWARNING,
    ERROR_REFTOINVALIDTYPELIB,
  }
}
