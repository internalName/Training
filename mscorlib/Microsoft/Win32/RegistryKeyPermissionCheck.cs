// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryKeyPermissionCheck
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace Microsoft.Win32
{
  /// <summary>
  ///   Выполнение проверок безопасности при открытии разделов реестра и доступ к их пары имя значение.
  /// </summary>
  public enum RegistryKeyPermissionCheck
  {
    Default,
    ReadSubTree,
    ReadWriteSubTree,
  }
}
