// Decompiled with JetBrains decompiler
// Type: System.Resources.IResourceWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>
  ///   Предоставляет базовую функциональность для записи ресурсов в выходной файл или поток.
  /// </summary>
  [ComVisible(true)]
  public interface IResourceWriter : IDisposable
  {
    /// <summary>
    ///   Добавляет именованный ресурс типа <see cref="T:System.String" /> в список ресурсов для записи.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="value">Значение ресурса.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    void AddResource(string name, string value);

    /// <summary>
    ///   Добавляет именованный ресурс типа <see cref="T:System.Object" /> в список ресурсов для записи.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="value">Значение ресурса.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    void AddResource(string name, object value);

    /// <summary>
    ///   Добавляет массив 8-разрядное целое число без знака в качестве именованного ресурса в список ресурсов для записи.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="value">
    ///   Значение ресурса в виде массива 8-разрядное целое число без знака.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    void AddResource(string name, byte[] value);

    /// <summary>
    ///   Закрывает основной файл ресурсов или потока, обеспечивая все данные записаны в файл.
    /// </summary>
    void Close();

    /// <summary>
    ///   Записывает все ресурсы, добавленные методом <see cref="M:System.Resources.IResourceWriter.AddResource(System.String,System.String)" /> выходной файл или поток.
    /// </summary>
    void Generate();
  }
}
