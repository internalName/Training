// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolBinder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Представляет модуль привязки символов для управляемого кода.
  /// </summary>
  [ComVisible(true)]
  public interface ISymbolBinder
  {
    /// <summary>
    ///   Возвращает интерфейс средства чтения символов для текущего файла.
    /// </summary>
    /// <param name="importer">Интерфейс импорта метаданных.</param>
    /// <param name="filename">
    ///   Имя файла, для которого требуется интерфейс средства чтения.
    /// </param>
    /// <param name="searchPath">
    ///   Путь поиска, используемый для поиска файла символов.
    /// </param>
    /// <returns>
    ///   Интерфейс <see cref="T:System.Diagnostics.SymbolStore.ISymbolReader" /> для чтения символов отладки.
    /// </returns>
    [Obsolete("The recommended alternative is ISymbolBinder1.GetReader. ISymbolBinder1.GetReader takes the importer interface pointer as an IntPtr instead of an Int32, and thus works on both 32-bit and 64-bit architectures. http://go.microsoft.com/fwlink/?linkid=14202=14202")]
    ISymbolReader GetReader(int importer, string filename, string searchPath);
  }
}
