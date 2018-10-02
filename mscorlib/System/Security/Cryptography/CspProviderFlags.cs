// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CspProviderFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Задает флаги, которые изменяют режим работы поставщиков служб шифрования.
  /// </summary>
  [ComVisible(true)]
  [Flags]
  [Serializable]
  public enum CspProviderFlags
  {
    NoFlags = 0,
    UseMachineKeyStore = 1,
    UseDefaultKeyContainer = 2,
    UseNonExportableKey = 4,
    UseExistingKey = 8,
    UseArchivableKey = 16, // 0x00000010
    UseUserProtectedKey = 32, // 0x00000020
    NoPrompt = 64, // 0x00000040
    CreateEphemeralKey = 128, // 0x00000080
  }
}
