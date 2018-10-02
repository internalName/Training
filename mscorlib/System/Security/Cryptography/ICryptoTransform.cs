// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.ICryptoTransform
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Определяет базовые операции криптографических преобразований.
  /// </summary>
  [ComVisible(true)]
  public interface ICryptoTransform : IDisposable
  {
    /// <summary>Возвращает размер входного блока.</summary>
    /// <returns>Размер входного блока данных в байтах.</returns>
    int InputBlockSize { get; }

    /// <summary>Возвращает размер выходного блока.</summary>
    /// <returns>Размер выходного блока данных в байтах.</returns>
    int OutputBlockSize { get; }

    /// <summary>
    ///   Возвращает значение, указывающее, возможно ли преобразование нескольких блоков.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если возможно преобразование нескольких блоков; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool CanTransformMultipleBlocks { get; }

    /// <summary>
    ///   Возвращает значение, указывающее, возможно ли повторное использование текущего преобразования.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текущее преобразование может быть повторно использован; в противном случае — <see langword="false" />.
    /// </returns>
    bool CanReuseTransform { get; }

    /// <summary>
    ///   Преобразует заданную область входного массива байтов и копирует результирующее преобразование в заданную область выходного массива байтов.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные, для которых вычисляется преобразование.
    /// </param>
    /// <param name="inputOffset">
    ///   Смещение во входном массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="inputCount">
    ///   Число байтов во входном массиве для использования в качестве данных.
    /// </param>
    /// <param name="outputBuffer">
    ///   Выходные данные для записи преобразования.
    /// </param>
    /// <param name="outputOffset">
    ///   Смещение в выходном массиве байтов, начиная с которого следует записывать данные.
    /// </param>
    /// <returns>Число записанных байтов.</returns>
    int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

    /// <summary>
    ///   Преобразует заданную область заданного массива байтов.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные, для которых вычисляется преобразование.
    /// </param>
    /// <param name="inputOffset">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="inputCount">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    /// <returns>Вычисленное преобразование.</returns>
    byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
  }
}
