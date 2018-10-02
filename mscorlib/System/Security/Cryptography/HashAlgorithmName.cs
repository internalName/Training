// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HashAlgorithmName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  /// <summary>Задает имя криптографического хэш-алгоритма.</summary>
  public struct HashAlgorithmName : IEquatable<HashAlgorithmName>
  {
    private readonly string _name;

    /// <summary>
    ///   Получает имя хэш-алгоритма, которое представляет "MD5".
    /// </summary>
    /// <returns>Имя хэш-алгоритма, которое представляет "MD5".</returns>
    public static HashAlgorithmName MD5
    {
      get
      {
        return new HashAlgorithmName(nameof (MD5));
      }
    }

    /// <summary>
    ///   Получает имя хэш-алгоритма, которое представляет "SHA1".
    /// </summary>
    /// <returns>Имя хэш-алгоритма, которое представляет "SHA1".</returns>
    public static HashAlgorithmName SHA1
    {
      get
      {
        return new HashAlgorithmName(nameof (SHA1));
      }
    }

    /// <summary>
    ///   Получает имя хэш-алгоритма, которое представляет "SHA256".
    /// </summary>
    /// <returns>Имя хэш-алгоритма, которое представляет "SHA256".</returns>
    public static HashAlgorithmName SHA256
    {
      get
      {
        return new HashAlgorithmName(nameof (SHA256));
      }
    }

    /// <summary>
    ///   Получает имя хэш-алгоритма, которое представляет "SHA384".
    /// </summary>
    /// <returns>Имя хэш-алгоритма, которое представляет "SHA384".</returns>
    public static HashAlgorithmName SHA384
    {
      get
      {
        return new HashAlgorithmName(nameof (SHA384));
      }
    }

    /// <summary>
    ///   Получает имя хэш-алгоритма, которое представляет "SHA512".
    /// </summary>
    /// <returns>Имя хэш-алгоритма, которое представляет "SHA512".</returns>
    public static HashAlgorithmName SHA512
    {
      get
      {
        return new HashAlgorithmName(nameof (SHA512));
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> с пользовательским именем.
    /// </summary>
    /// <param name="name">Имя пользовательского хэш-алгоритма.</param>
    public HashAlgorithmName(string name)
    {
      this._name = name;
    }

    /// <summary>
    ///   Получает базовое представление строки для имени алгоритма.
    /// </summary>
    /// <returns>
    ///   Представление строки для имени алгоритма, <see langword="null" /> или <see cref="F:System.String.Empty" />, если хэш-алгоритм недоступен.
    /// </returns>
    public string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>
    ///   Возвращает строковое представление текущего экземпляра <see cref="T:System.Security.Cryptography.HashAlgorithmName" />.
    /// </summary>
    /// <returns>
    ///   Представление строки текущего экземпляра <see cref="T:System.Security.Cryptography.HashAlgorithmName" />.
    /// </returns>
    public override string ToString()
    {
      return this._name ?? string.Empty;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли данный экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="obj" /> является объектом <see cref="T:System.Security.Cryptography.HashAlgorithmName" />, а его свойство <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> равно свойству текущего экземпляра.
    ///    Сравнение выполняется по порядку, без учета регистра.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is HashAlgorithmName)
        return this.Equals((HashAlgorithmName) obj);
      return false;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равны ли два экземпляра <see cref="T:System.Security.Cryptography.HashAlgorithmName" />.
    /// </summary>
    /// <param name="other">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> для <paramref name="other" /> равно свойству текущего экземпляра.
    ///    Сравнение выполняется по порядку, без учета регистра.
    /// </returns>
    public bool Equals(HashAlgorithmName other)
    {
      return this._name == other._name;
    }

    /// <summary>Возвращает хэш-код текущего экземпляра.</summary>
    /// <returns>
    ///   Хэш-код для текущего экземпляра или 0, если значение <paramref name="name" /> не было предоставлено конструктору <see cref="T:System.Security.Cryptography.HashAlgorithmName" />.
    /// </returns>
    public override int GetHashCode()
    {
      if (this._name != null)
        return this._name.GetHashCode();
      return 0;
    }

    /// <summary>
    ///   Определение равенства двух заданных объектов <see cref="T:System.Security.Cryptography.HashAlgorithmName" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если и <paramref name="left" />, и <paramref name="right" /> имеют одно значение <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
    {
      return left.Equals(right);
    }

    /// <summary>
    ///   Определение неравенства двух заданных объектов <see cref="T:System.Security.Cryptography.HashAlgorithmName" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если и <paramref name="left" />, и <paramref name="right" /> имеют разные значения <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" />; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(HashAlgorithmName left, HashAlgorithmName right)
    {
      return !(left == right);
    }
  }
}
