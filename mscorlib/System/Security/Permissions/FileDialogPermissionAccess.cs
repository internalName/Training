// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileDialogPermissionAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Указывает тип доступа к файлам, допустимый при использовании файл диалоговые окна.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum FileDialogPermissionAccess
  {
    None = 0,
    Open = 1,
    Save = 2,
    OpenSave = Save | Open, // 0x00000003
  }
}
