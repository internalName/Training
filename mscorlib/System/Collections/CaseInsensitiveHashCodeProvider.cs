// Decompiled with JetBrains decompiler
// Type: System.Collections.CaseInsensitiveHashCodeProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Предоставляет хэш-код объекта, используя алгоритм хэширования, который не учитывается регистр строк.
  /// </summary>
  [Obsolete("Please use StringComparer instead.")]
  [ComVisible(true)]
  [Serializable]
  public class CaseInsensitiveHashCodeProvider : IHashCodeProvider
  {
    private TextInfo m_text;
    private static volatile CaseInsensitiveHashCodeProvider m_InvariantCaseInsensitiveHashCodeProvider;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> класса <see cref="P:System.Threading.Thread.CurrentCulture" /> текущего потока.
    /// </summary>
    public CaseInsensitiveHashCodeProvider()
    {
      this.m_text = CultureInfo.CurrentCulture.TextInfo;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> с использованием указанного объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <param name="culture">
    ///   <see cref="T:System.Globalization.CultureInfo" /> Для использования нового <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    public CaseInsensitiveHashCodeProvider(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      this.m_text = culture.TextInfo;
    }

    /// <summary>
    ///   Возвращает экземпляр класса <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> связанный с <see cref="P:System.Threading.Thread.CurrentCulture" /> из текущего потока и доступна всегда.
    /// </summary>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> связанный с <see cref="P:System.Threading.Thread.CurrentCulture" /> текущего потока.
    /// </returns>
    public static CaseInsensitiveHashCodeProvider Default
    {
      get
      {
        return new CaseInsensitiveHashCodeProvider(CultureInfo.CurrentCulture);
      }
    }

    /// <summary>
    ///   Возвращает экземпляр класса <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> связанный с <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> и всегда доступна.
    /// </summary>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> связанный с <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.
    /// </returns>
    public static CaseInsensitiveHashCodeProvider DefaultInvariant
    {
      get
      {
        if (CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider == null)
          CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
        return CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider;
      }
    }

    /// <summary>
    ///   Возвращает хэш-код для заданного объекта, используя алгоритм хэширования, который не учитывается регистр строк.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Object" /> Для которого должен быть возвращен хэш-код.
    /// </param>
    /// <returns>
    ///   Хэш-код для данного объекта, используя алгоритм хэширования, который не учитывается регистр строк.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    public int GetHashCode(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      string str = obj as string;
      if (str == null)
        return obj.GetHashCode();
      return this.m_text.GetCaseInsensitiveHashCode(str);
    }
  }
}
