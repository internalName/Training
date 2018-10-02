// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ITypeLibExporterNameProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет управление регистром имен при экспортировании в библиотеку типов.
  /// </summary>
  [Guid("FA1F3615-ACB9-486d-9EAC-1BEF87E36B09")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface ITypeLibExporterNameProvider
  {
    /// <summary>Возвращает список имен для управления регистром.</summary>
    /// <returns>
    ///   Массив строк, каждый элемент которого содержит имя типа для управления регистром.
    /// </returns>
    [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
    string[] GetNames();
  }
}
