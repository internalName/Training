// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.PKCS1MaskGenerationMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет маски, соответствующие стандарту PKCS #1, для использования алгоритмами обмена ключами.
  /// </summary>
  [ComVisible(true)]
  public class PKCS1MaskGenerationMethod : MaskGenerationMethod
  {
    private string HashNameValue;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.PKCS1MaskGenerationMethod" />.
    /// </summary>
    public PKCS1MaskGenerationMethod()
    {
      this.HashNameValue = "SHA1";
    }

    /// <summary>
    ///   Возвращает или задает имя типа хэш-алгоритма, используемого для создания маски.
    /// </summary>
    /// <returns>
    ///   Имя типа, который реализует хэш-алгоритм для вычисления маски.
    /// </returns>
    public string HashName
    {
      get
      {
        return this.HashNameValue;
      }
      set
      {
        this.HashNameValue = value;
        if (this.HashNameValue != null)
          return;
        this.HashNameValue = "SHA1";
      }
    }

    /// <summary>
    ///   Создает и возвращает маску из заданного случайного начального значения указанной длины.
    /// </summary>
    /// <param name="rgbSeed">
    ///   Случайное начальное значение, используемое для вычисления маски.
    /// </param>
    /// <param name="cbReturn">Длина создаваемой маски в байтах.</param>
    /// <returns>
    ///   Случайно созданная маска, длина которой равна параметру <paramref name="cbReturn" />.
    /// </returns>
    public override byte[] GenerateMask(byte[] rgbSeed, int cbReturn)
    {
      HashAlgorithm fromName = (HashAlgorithm) CryptoConfig.CreateFromName(this.HashNameValue);
      byte[] counter = new byte[4];
      byte[] numArray = new byte[cbReturn];
      uint num = 0;
      int dstOffset = 0;
      while (dstOffset < numArray.Length)
      {
        Utils.ConvertIntToByteArray(num++, ref counter);
        fromName.TransformBlock(rgbSeed, 0, rgbSeed.Length, rgbSeed, 0);
        fromName.TransformFinalBlock(counter, 0, 4);
        byte[] hash = fromName.Hash;
        fromName.Initialize();
        if (numArray.Length - dstOffset > hash.Length)
          Buffer.BlockCopy((Array) hash, 0, (Array) numArray, dstOffset, hash.Length);
        else
          Buffer.BlockCopy((Array) hash, 0, (Array) numArray, dstOffset, numArray.Length - dstOffset);
        dstOffset += fromName.Hash.Length;
      }
      return numArray;
    }
  }
}
