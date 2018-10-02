// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.RegistrationConnectionType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>Определяет типы подключений к объекту класса.</summary>
  [Flags]
  public enum RegistrationConnectionType
  {
    SingleUse = 0,
    MultipleUse = 1,
    MultiSeparate = 2,
    Suspended = 4,
    Surrogate = 8,
  }
}
