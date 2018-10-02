// Decompiled with JetBrains decompiler
// Type: System.Reflection.MemberTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Отмечает каждый тип элемент, определенный как производный класс класса <see cref="T:System.Reflection.MemberInfo" />.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum MemberTypes
  {
    Constructor = 1,
    Event = 2,
    Field = 4,
    Method = 8,
    Property = 16, // 0x00000010
    TypeInfo = 32, // 0x00000020
    Custom = 64, // 0x00000040
    NestedType = 128, // 0x00000080
    All = NestedType | TypeInfo | Property | Method | Field | Event | Constructor, // 0x000000BF
  }
}
