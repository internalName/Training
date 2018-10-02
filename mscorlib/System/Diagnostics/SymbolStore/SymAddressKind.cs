// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.SymAddressKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Задает типы адресов для локальных переменных, параметров и полей в методах <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.DefineLocalVariable(System.String,System.Reflection.FieldAttributes,System.Byte[],System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" />, <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.DefineParameter(System.String,System.Reflection.ParameterAttributes,System.Int32,System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32)" />, и <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.DefineField(System.Diagnostics.SymbolStore.SymbolToken,System.String,System.Reflection.FieldAttributes,System.Byte[],System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32)" /> из <see cref="T:System.Diagnostics.SymbolStore.ISymbolWriter" /> интерфейса.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum SymAddressKind
  {
    ILOffset = 1,
    NativeRVA = 2,
    NativeRegister = 3,
    NativeRegisterRelative = 4,
    NativeOffset = 5,
    NativeRegisterRegister = 6,
    NativeRegisterStack = 7,
    NativeStackRegister = 8,
    BitField = 9,
    NativeSectionOffset = 10, // 0x0000000A
  }
}
