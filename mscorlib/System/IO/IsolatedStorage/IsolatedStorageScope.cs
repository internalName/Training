// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageScope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO.IsolatedStorage
{
  /// <summary>
  ///   Перечисление уровней области изолированного хранилища, которые поддерживаются <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" />.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum IsolatedStorageScope
  {
    None = 0,
    User = 1,
    Domain = 2,
    Assembly = 4,
    Roaming = 8,
    Machine = 16, // 0x00000010
    Application = 32, // 0x00000020
  }
}
