// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolBinder1
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
  public interface ISymbolBinder1
  {
    /// <summary>
    ///   Возвращает интерфейс средства чтения символов для текущего файла.
    /// </summary>
    /// <param name="importer">
    ///   <see cref="T:System.IntPtr" /> Ссылается на интерфейс импорта метаданных.
    /// </param>
    /// <param name="filename">
    ///   Имя файла, для которого требуется интерфейс средства чтения.
    /// </param>
    /// <param name="searchPath">
    ///   Путь поиска, используемый для поиска файла символов.
    /// </param>
    /// <returns>
    ///   Интерфейс <see cref="T:System.Diagnostics.SymbolStore.ISymbolReader" /> для чтения символов отладки.
    /// </returns>
    ISymbolReader GetReader(IntPtr importer, string filename, string searchPath);
  }
}
