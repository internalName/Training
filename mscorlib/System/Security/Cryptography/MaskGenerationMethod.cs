// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.MaskGenerationMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный класс, от которого должны наследоваться все алгоритмы создания масок.
  /// </summary>
  [ComVisible(true)]
  public abstract class MaskGenerationMethod
  {
    /// <summary>
    ///   При переопределении в производном классе создает маску указанной длины с помощью заданного случайного начального значения.
    /// </summary>
    /// <param name="rgbSeed">
    ///   Случайное начальное значение, используемое для вычисления маски.
    /// </param>
    /// <param name="cbReturn">Длина созданной маски в байтах.</param>
    /// <returns>
    ///   Случайно созданная маска, длина которой равна параметру <paramref name="cbReturn" />.
    /// </returns>
    [ComVisible(true)]
    public abstract byte[] GenerateMask(byte[] rgbSeed, int cbReturn);
  }
}
