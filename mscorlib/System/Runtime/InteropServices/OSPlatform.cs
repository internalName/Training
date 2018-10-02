// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.OSPlatform
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>Представляет платформу операционной системы.</summary>
  public struct OSPlatform : IEquatable<OSPlatform>
  {
    private readonly string _osPlatform;

    /// <summary>
    ///   Получает объект, представляющий операционную систему Linux.
    /// </summary>
    /// <returns>Объект, представляющий операционную систему Linux.</returns>
    public static OSPlatform Linux { get; } = new OSPlatform("LINUX");

    /// <summary>
    ///   Получает объект, представляющий операционную систему OSX.
    /// </summary>
    /// <returns>Объект, представляющий операционную систему OSX.</returns>
    public static OSPlatform OSX { get; } = new OSPlatform(nameof (OSX));

    /// <summary>
    ///   Получает объект, представляющий операционную систему Windows.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий операционную систему Windows.
    /// </returns>
    public static OSPlatform Windows { get; } = new OSPlatform("WINDOWS");

    private OSPlatform(string osPlatform)
    {
      if (osPlatform == null)
        throw new ArgumentNullException(nameof (osPlatform));
      if (osPlatform.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyValue"), nameof (osPlatform));
      this._osPlatform = osPlatform;
    }

    /// <summary>
    ///   Создает новый экземпляр <see cref="T:System.Runtime.InteropServices.OSPlatform" />.
    /// </summary>
    /// <param name="osPlatform">
    ///   Имя платформы, представляемой этим экземпляром.
    /// </param>
    /// <returns>
    ///   Объект, представляющий операционную систему <paramref name="osPlatform" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="osPlatform" /> равен пустой строке.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="osPlatform" /> имеет значение <see langword="null" />.
    /// </exception>
    public static OSPlatform Create(string osPlatform)
    {
      return new OSPlatform(osPlatform);
    }

    /// <summary>
    ///   Определяет, равен ли текущий экземпляр указанному экземпляру <see cref="T:System.Runtime.InteropServices.OSPlatform" />.
    /// </summary>
    /// <param name="other">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <paramref name="other" /> равен текущему экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool Equals(OSPlatform other)
    {
      return this.Equals(other._osPlatform);
    }

    internal bool Equals(string other)
    {
      return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
    }

    /// <summary>
    ///   Определяет, равен ли текущий экземпляр <see cref="T:System.Runtime.InteropServices.OSPlatform" /> указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   <see langword="true" />, если <paramref name="obj" /> является экземпляром <see cref="T:System.Runtime.InteropServices.OSPlatform" /> и его имя совпадает с именем текущего объекта; в противном случае — <paramref name="false" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="obj" /> является экземпляром <see cref="T:System.Runtime.InteropServices.OSPlatform" /> и его имя совпадает с именем текущего объекта.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is OSPlatform)
        return this.Equals((OSPlatform) obj);
      return false;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>Хэш-код данного экземпляра.</returns>
    public override int GetHashCode()
    {
      if (this._osPlatform != null)
        return this._osPlatform.GetHashCode();
      return 0;
    }

    /// <summary>
    ///   Возвращает строковое представление этого экземпляра <see cref="T:System.Runtime.InteropServices.OSPlatform" />.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая данный экземпляр <see cref="T:System.Runtime.InteropServices.OSPlatform" />.
    /// </returns>
    public override string ToString()
    {
      return this._osPlatform ?? string.Empty;
    }

    /// <summary>
    ///   Определяет равенство двух объектов <see cref="T:System.Runtime.InteropServices.OSPlatform" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(OSPlatform left, OSPlatform right)
    {
      return left.Equals(right);
    }

    /// <summary>
    ///   Определяет неравенство двух экземпляров <see cref="T:System.Runtime.InteropServices.OSPlatform" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator !=(OSPlatform left, OSPlatform right)
    {
      return !(left == right);
    }
  }
}
