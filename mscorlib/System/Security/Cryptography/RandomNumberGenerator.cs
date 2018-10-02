// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RandomNumberGenerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный класс, из которого создаются все реализации криптографических генераторов случайных чисел.
  /// </summary>
  [ComVisible(true)]
  public abstract class RandomNumberGenerator : IDisposable
  {
    /// <summary>
    ///   При переопределении в производном классе создает экземпляр реализации по умолчанию криптографического генератора случайных чисел, позволяющего генерировать случайные данные.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр криптографического генератора случайных чисел.
    /// </returns>
    public static RandomNumberGenerator Create()
    {
      return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
    }

    /// <summary>
    ///   При переопределении в производном классе создает экземпляр указанной реализации криптографического генератора случайных чисел.
    /// </summary>
    /// <param name="rngName">
    ///   Имя реализации генератора случайных чисел, которую необходимо использовать.
    /// </param>
    /// <returns>
    ///   Новый экземпляр криптографического генератора случайных чисел.
    /// </returns>
    public static RandomNumberGenerator Create(string rngName)
    {
      return (RandomNumberGenerator) CryptoConfig.CreateFromName(rngName);
    }

    /// <summary>
    ///   Если переопределено в производном классе, освобождает все ресурсы, используемые текущим объектом <see cref="T:System.Security.Cryptography.RandomNumberGenerator" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   При переопределении в производном классе освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Cryptography.RandomNumberGenerator" />, и опционально освобождает управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>
    ///   При переопределении в производном классе заполняет массив байтов криптостойкой случайной последовательностью значений.
    /// </summary>
    /// <param name="data">
    ///   Массив, который заполняется криптостойкими случайными байтами.
    /// </param>
    public abstract void GetBytes(byte[] data);

    /// <summary>
    ///   Заполняет указанный массив байтов криптостойкой случайной последовательностью значений.
    /// </summary>
    /// <param name="data">
    ///   Массив, который заполняется криптостойкими случайными байтами.
    /// </param>
    /// <param name="offset">
    ///   Индекс в массиве, с которого требуется начать заполнение.
    /// </param>
    /// <param name="count">
    ///   Число байтов, которые требуется заполнить.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> или <paramref name="count" /> меньше 0
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> плюс <paramref name="count" /> превышает длину <paramref name="data" />.
    /// </exception>
    public virtual void GetBytes(byte[] data, int offset, int count)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (offset + count > data.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (count <= 0)
        return;
      byte[] data1 = new byte[count];
      this.GetBytes(data1);
      Array.Copy((Array) data1, 0, (Array) data, offset, count);
    }

    /// <summary>
    ///   При переопределении в производном классе заполняет массив байтов криптостойкой случайной последовательностью ненулевых значений.
    /// </summary>
    /// <param name="data">
    ///   Массив, который заполняется криптостойкими случайными ненулевыми байтами.
    /// </param>
    public virtual void GetNonZeroBytes(byte[] data)
    {
      throw new NotImplementedException();
    }
  }
}
