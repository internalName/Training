// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
  /// <summary>
  ///   Определяет, куда и как импортируется закрытый ключ сертификата X.509.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum X509KeyStorageFlags
  {
    DefaultKeySet = 0,
    UserKeySet = 1,
    MachineKeySet = 2,
    Exportable = 4,
    UserProtected = 8,
    PersistKeySet = 16, // 0x00000010
    EphemeralKeySet = 32, // 0x00000020
  }
}
