// Decompiled with JetBrains decompiler
// Type: System.Resources.IResourceReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>
  ///   Предоставляет базовую функциональность для чтения данных из файлов ресурсов.
  /// </summary>
  [ComVisible(true)]
  public interface IResourceReader : IEnumerable, IDisposable
  {
    /// <summary>
    ///   Закрывает средство чтения ресурсов после освобождения все ресурсы, связанные с ним.
    /// </summary>
    void Close();

    /// <summary>
    ///   Возвращает перечислитель словаря ресурсов для данного устройства чтения.
    /// </summary>
    /// <returns>
    ///   Перечислитель словаря для ресурсов данного устройства чтения.
    /// </returns>
    IDictionaryEnumerator GetEnumerator();
  }
}
