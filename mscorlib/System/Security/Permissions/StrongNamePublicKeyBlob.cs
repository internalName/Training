// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.StrongNamePublicKeyBlob
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Представляет сведения об открытом ключе (называемые большой двоичный объект) для строгого имени.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongNamePublicKeyBlob
  {
    internal byte[] PublicKey;

    internal StrongNamePublicKeyBlob()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> класса с необработанными байтами объекта blob открытого ключа.
    /// </summary>
    /// <param name="publicKey">
    ///   Массив байтов, представляющий необработанные данные открытого ключа.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="publicKey" /> имеет значение <see langword="null" />.
    /// </exception>
    public StrongNamePublicKeyBlob(byte[] publicKey)
    {
      if (publicKey == null)
        throw new ArgumentNullException(nameof (PublicKey));
      this.PublicKey = new byte[publicKey.Length];
      Array.Copy((Array) publicKey, 0, (Array) this.PublicKey, 0, publicKey.Length);
    }

    internal StrongNamePublicKeyBlob(string publicKey)
    {
      this.PublicKey = Hex.DecodeHexString(publicKey);
    }

    private static bool CompareArrays(byte[] first, byte[] second)
    {
      if (first.Length != second.Length)
        return false;
      int length = first.Length;
      for (int index = 0; index < length; ++index)
      {
        if ((int) first[index] != (int) second[index])
          return false;
      }
      return true;
    }

    internal bool Equals(StrongNamePublicKeyBlob blob)
    {
      if (blob == null)
        return false;
      return StrongNamePublicKeyBlob.CompareArrays(this.PublicKey, blob.PublicKey);
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, равен ли текущий blob открытого ключа объекта blob указанного открытого ключа.
    /// </summary>
    /// <param name="obj">
    ///   Объект, содержащий большой двоичный объект открытого ключа.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если для объекта blob открытого ключа текущего объекта равен объекта blob открытого ключа из <paramref name="obj" /> параметр; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is StrongNamePublicKeyBlob))
        return false;
      return this.Equals((StrongNamePublicKeyBlob) obj);
    }

    private static int GetByteArrayHashCode(byte[] baData)
    {
      if (baData == null)
        return 0;
      int num = 0;
      for (int index = 0; index < baData.Length; ++index)
        num = num << 8 ^ (int) baData[index] ^ num >> 24;
      return num;
    }

    /// <summary>Возвращает хэш-код на основе открытого ключа.</summary>
    /// <returns>Хэш-код на основе открытого ключа.</returns>
    public override int GetHashCode()
    {
      return StrongNamePublicKeyBlob.GetByteArrayHashCode(this.PublicKey);
    }

    /// <summary>
    ///   Создает и возвращает строковое представление объекта blob открытого ключа.
    /// </summary>
    /// <returns>
    ///   Шестнадцатеричная версия объекта blob открытого ключа.
    /// </returns>
    public override string ToString()
    {
      return Hex.EncodeHexString(this.PublicKey);
    }
  }
}
