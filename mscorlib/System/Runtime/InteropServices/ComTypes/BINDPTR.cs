// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.BINDPTR
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Содержит указатель на связанный с <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> структуры <see cref="T:System.Runtime.InteropServices.VARDESC" /> структуру, или <see langword="ITypeComp" /> интерфейса.
  /// </summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
  public struct BINDPTR
  {
    /// <summary>
    ///   Представляет указатель на <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> структуры.
    /// </summary>
    [FieldOffset(0)]
    public IntPtr lpfuncdesc;
    /// <summary>
    ///   Представляет указатель на <see cref="T:System.Runtime.InteropServices.VARDESC" /> структуры.
    /// </summary>
    [FieldOffset(0)]
    public IntPtr lpvardesc;
    /// <summary>
    ///   Представляет указатель на <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeComp" /> интерфейса.
    /// </summary>
    [FieldOffset(0)]
    public IntPtr lptcomp;
  }
}
