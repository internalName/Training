// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSASignaturePadding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Задает режим заполнения и параметры для использования с операциями создания или проверки подписи RSA.
  /// </summary>
  public sealed class RSASignaturePadding : IEquatable<RSASignaturePadding>
  {
    private static readonly RSASignaturePadding s_pkcs1 = new RSASignaturePadding(RSASignaturePaddingMode.Pkcs1);
    private static readonly RSASignaturePadding s_pss = new RSASignaturePadding(RSASignaturePaddingMode.Pss);
    private readonly RSASignaturePaddingMode _mode;

    private RSASignaturePadding(RSASignaturePaddingMode mode)
    {
      this._mode = mode;
    }

    /// <summary>
    ///   Получает объект, который использует режим заполнения PKCS #1 v1.5.
    /// </summary>
    /// <returns>
    ///   Объект, использующий режим заполнения <see cref="F:System.Security.Cryptography.RSASignaturePaddingMode.Pkcs1" />.
    /// </returns>
    public static RSASignaturePadding Pkcs1
    {
      get
      {
        return RSASignaturePadding.s_pkcs1;
      }
    }

    /// <summary>Получает объект, использующий режим заполнения PSS.</summary>
    /// <returns>
    ///   Объект, который использует режим заполнения <see cref="F:System.Security.Cryptography.RSASignaturePaddingMode.Pss" /> с количеством байт случайных данных, равным размеру хэша.
    /// </returns>
    public static RSASignaturePadding Pss
    {
      get
      {
        return RSASignaturePadding.s_pss;
      }
    }

    /// <summary>
    ///   Получает режим заполнения данного экземпляра <see cref="T:System.Security.Cryptography.RSASignaturePadding" />.
    /// </summary>
    /// <returns>
    ///   Режим заполнения (<see cref="F:System.Security.Cryptography.RSASignaturePaddingMode.Pkcs1" /> или <see cref="F:System.Security.Cryptography.RSASignaturePaddingMode.Pss" />) данного экземпляра.
    /// </returns>
    public RSASignaturePaddingMode Mode
    {
      get
      {
        return this._mode;
      }
    }

    /// <summary>
    ///   Возвращает хэш-код данного экземпляра <see cref="T:System.Security.Cryptography.RSASignaturePadding" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код данного экземпляра <see cref="T:System.Security.Cryptography.RSASignaturePadding" />.
    /// </returns>
    public override int GetHashCode()
    {
      return this._mode.GetHashCode();
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект равен текущему объекту; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      return this.Equals(obj as RSASignaturePadding);
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли экземпляр указанному объекту <see cref="T:System.Security.Cryptography.RSASignaturePadding" />.
    /// </summary>
    /// <param name="other">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект равен текущему объекту; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool Equals(RSASignaturePadding other)
    {
      if (other != (RSASignaturePadding) null)
        return this._mode == other._mode;
      return false;
    }

    /// <summary>
    ///   Указывает, равны ли значения двух заданных объектов <see cref="T:System.Security.Cryptography.RSASignaturePadding" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <see langword="left" /> и <see langword="right" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(RSASignaturePadding left, RSASignaturePadding right)
    {
      if ((object) left == null)
        return (object) right == null;
      return left.Equals(right);
    }

    /// <summary>
    ///   Указывает, равны ли значения двух заданных объектов <see cref="T:System.Security.Cryptography.RSASignaturePadding" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <see langword="left" /> и <see langword="right" /> не равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator !=(RSASignaturePadding left, RSASignaturePadding right)
    {
      return !(left == right);
    }

    /// <summary>
    ///   Возвращает строковое представление текущего экземпляра <see cref="T:System.Security.Cryptography.RSASignaturePadding" />.
    /// </summary>
    /// <returns>Строковое представление текущего объекта.</returns>
    public override string ToString()
    {
      return this._mode.ToString();
    }
  }
}
