// Decompiled with JetBrains decompiler
// Type: System.Text.EncodingProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Text
{
  /// <summary>
  ///   Предоставляет базовый класс для поставщика кодировки, обеспечивающего кодировки, недоступные в определенной платформе.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public abstract class EncodingProvider
  {
    private static object s_InternalSyncObject = new object();
    private static volatile EncodingProvider[] s_providers;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.EncodingProvider" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EncodingProvider()
    {
    }

    /// <summary>Возвращает кодировку с указанным именем.</summary>
    /// <param name="name">Имя запрошенной кодировки.</param>
    /// <returns>
    ///   Кодировка, связанная с указанным именем, или <see langword="null" />, если этот поставщик <see cref="T:System.Text.EncodingProvider" /> не может вернуть допустимую кодировку, соответствующую <paramref name="name" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Encoding GetEncoding(string name);

    /// <summary>
    ///   Возвращает кодировку, связанную с указанным идентификатором кодовой страницы.
    /// </summary>
    /// <param name="codepage">
    ///   Идентификатор кодовой страницы запрошенной кодировки.
    /// </param>
    /// <returns>
    ///   Кодировка, связанная с указанной кодовой страницей, или <see langword="null" />, если этот поставщик <see cref="T:System.Text.EncodingProvider" /> не может вернуть допустимую кодировку, соответствующую <paramref name="codepage" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Encoding GetEncoding(int codepage);

    /// <summary>
    ///   Возвращает кодировку, связанную с заданным именем.
    ///    С помощью параметров задается обработчик ошибок для символов, которые не удается закодировать, и последовательностей байтов, которые не удается декодировать.
    /// </summary>
    /// <param name="name">Имя предпочтительной кодировки.</param>
    /// <param name="encoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда символ не может быть закодирован с использованием этой кодировки.
    /// </param>
    /// <param name="decoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда последовательность байтов не может быть декодирована с использованием текущей кодировки.
    /// </param>
    /// <returns>
    ///   Кодировка, связанная с указанным именем, или <see langword="null" />, если этот поставщик <see cref="T:System.Text.EncodingProvider" /> не может вернуть допустимую кодировку, соответствующую <paramref name="name" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      Encoding encoding = this.GetEncoding(name);
      if (encoding != null)
      {
        encoding = (Encoding) this.GetEncoding(name).Clone();
        encoding.EncoderFallback = encoderFallback;
        encoding.DecoderFallback = decoderFallback;
      }
      return encoding;
    }

    /// <summary>
    ///   Возвращает кодировку, связанную с указанным идентификатором кодовой страницы.
    ///    С помощью параметров задается обработчик ошибок для символов, которые не удается закодировать, и последовательностей байтов, которые не удается декодировать.
    /// </summary>
    /// <param name="codepage">
    ///   Идентификатор кодовой страницы запрошенной кодировки.
    /// </param>
    /// <param name="encoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда символ не может быть закодирован с использованием этой кодировки.
    /// </param>
    /// <param name="decoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда последовательность байтов не может быть декодирована с использованием этой кодировки.
    /// </param>
    /// <returns>
    ///   Кодировка, связанная с указанной кодовой страницей, или <see langword="null" />, если этот поставщик <see cref="T:System.Text.EncodingProvider" /> не может вернуть допустимую кодировку, соответствующую <paramref name="codepage" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      Encoding encoding = this.GetEncoding(codepage);
      if (encoding != null)
      {
        encoding = (Encoding) this.GetEncoding(codepage).Clone();
        encoding.EncoderFallback = encoderFallback;
        encoding.DecoderFallback = decoderFallback;
      }
      return encoding;
    }

    internal static void AddProvider(EncodingProvider provider)
    {
      if (provider == null)
        throw new ArgumentNullException(nameof (provider));
      lock (EncodingProvider.s_InternalSyncObject)
      {
        if (EncodingProvider.s_providers == null)
        {
          EncodingProvider.s_providers = new EncodingProvider[1]
          {
            provider
          };
        }
        else
        {
          if (Array.IndexOf<EncodingProvider>(EncodingProvider.s_providers, provider) >= 0)
            return;
          EncodingProvider[] encodingProviderArray = new EncodingProvider[EncodingProvider.s_providers.Length + 1];
          Array.Copy((Array) EncodingProvider.s_providers, (Array) encodingProviderArray, EncodingProvider.s_providers.Length);
          encodingProviderArray[encodingProviderArray.Length - 1] = provider;
          EncodingProvider.s_providers = encodingProviderArray;
        }
      }
    }

    internal static Encoding GetEncodingFromProvider(int codepage)
    {
      if (EncodingProvider.s_providers == null)
        return (Encoding) null;
      foreach (EncodingProvider provider in EncodingProvider.s_providers)
      {
        Encoding encoding = provider.GetEncoding(codepage);
        if (encoding != null)
          return encoding;
      }
      return (Encoding) null;
    }

    internal static Encoding GetEncodingFromProvider(string encodingName)
    {
      if (EncodingProvider.s_providers == null)
        return (Encoding) null;
      foreach (EncodingProvider provider in EncodingProvider.s_providers)
      {
        Encoding encoding = provider.GetEncoding(encodingName);
        if (encoding != null)
          return encoding;
      }
      return (Encoding) null;
    }

    internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
    {
      if (EncodingProvider.s_providers == null)
        return (Encoding) null;
      foreach (EncodingProvider provider in EncodingProvider.s_providers)
      {
        Encoding encoding = provider.GetEncoding(codepage, enc, dec);
        if (encoding != null)
          return encoding;
      }
      return (Encoding) null;
    }

    internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
    {
      if (EncodingProvider.s_providers == null)
        return (Encoding) null;
      foreach (EncodingProvider provider in EncodingProvider.s_providers)
      {
        Encoding encoding = provider.GetEncoding(encodingName, enc, dec);
        if (encoding != null)
          return encoding;
      }
      return (Encoding) null;
    }
  }
}
