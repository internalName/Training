// Decompiled with JetBrains decompiler
// Type: System.ApplicationId
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System
{
  /// <summary>
  ///   Содержит сведения, используемые для уникальной идентификации приложения на основе манифеста.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ApplicationId
  {
    private string m_name;
    private Version m_version;
    private string m_processorArchitecture;
    private string m_culture;
    internal byte[] m_publicKeyToken;

    internal ApplicationId()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ApplicationId" />.
    /// </summary>
    /// <param name="publicKeyToken">
    ///   Массив байтов, представляющий необработанные данные открытого ключа.
    /// </param>
    /// <param name="name">Имя приложения.</param>
    /// <param name="version">
    ///   Объект <see cref="T:System.Version" /> , указывающий версию приложения.
    /// </param>
    /// <param name="processorArchitecture">
    ///   Архитектура процессора приложения.
    /// </param>
    /// <param name="culture">
    ///   Язык и региональные параметры приложения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name " /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="version " /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="publicKeyToken " /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name " />является пустой строкой.
    /// </exception>
    public ApplicationId(byte[] publicKeyToken, string name, Version version, string processorArchitecture, string culture)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyApplicationName"));
      if (version == (Version) null)
        throw new ArgumentNullException(nameof (version));
      if (publicKeyToken == null)
        throw new ArgumentNullException(nameof (publicKeyToken));
      this.m_publicKeyToken = new byte[publicKeyToken.Length];
      Array.Copy((Array) publicKeyToken, 0, (Array) this.m_publicKeyToken, 0, publicKeyToken.Length);
      this.m_name = name;
      this.m_version = version;
      this.m_processorArchitecture = processorArchitecture;
      this.m_culture = culture;
    }

    /// <summary>Возвращает токен открытого ключа для приложения.</summary>
    /// <returns>
    ///   Массив байтов, содержащий токен открытого ключа для приложения.
    /// </returns>
    public byte[] PublicKeyToken
    {
      get
      {
        byte[] numArray = new byte[this.m_publicKeyToken.Length];
        Array.Copy((Array) this.m_publicKeyToken, 0, (Array) numArray, 0, this.m_publicKeyToken.Length);
        return numArray;
      }
    }

    /// <summary>Возвращает имя приложения.</summary>
    /// <returns>Имя приложения.</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>Возвращает версию приложения.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Version" /> указывающий версию приложения.
    /// </returns>
    public Version Version
    {
      get
      {
        return this.m_version;
      }
    }

    /// <summary>
    ///   Возвращает целевую архитектуру процессора для приложения.
    /// </summary>
    /// <returns>Архитектура процессора приложения.</returns>
    public string ProcessorArchitecture
    {
      get
      {
        return this.m_processorArchitecture;
      }
    }

    /// <summary>
    ///   Получает строку, представляющую сведения о культуре для приложения.
    /// </summary>
    /// <returns>Сведения о культуре для приложения.</returns>
    public string Culture
    {
      get
      {
        return this.m_culture;
      }
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего удостоверения приложения.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.ApplicationId" /> , Представляющий точную копию исходного объекта.
    /// </returns>
    public ApplicationId Copy()
    {
      return new ApplicationId(this.m_publicKeyToken, this.m_name, this.m_version, this.m_processorArchitecture, this.m_culture);
    }

    /// <summary>
    ///   Создает и возвращает строковое представление удостоверения приложения.
    /// </summary>
    /// <returns>Строковое представление удостоверения приложения.</returns>
    public override string ToString()
    {
      StringBuilder sb = StringBuilderCache.Acquire(16);
      sb.Append(this.m_name);
      if (this.m_culture != null)
      {
        sb.Append(", culture=\"");
        sb.Append(this.m_culture);
        sb.Append("\"");
      }
      sb.Append(", version=\"");
      sb.Append(this.m_version.ToString());
      sb.Append("\"");
      if (this.m_publicKeyToken != null)
      {
        sb.Append(", publicKeyToken=\"");
        sb.Append(Hex.EncodeHexString(this.m_publicKeyToken));
        sb.Append("\"");
      }
      if (this.m_processorArchitecture != null)
      {
        sb.Append(", processorArchitecture =\"");
        sb.Append(this.m_processorArchitecture);
        sb.Append("\"");
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    ///   Определяет, является ли указанный <see cref="T:System.ApplicationId" /> эквивалентно значению текущего объекта <see cref="T:System.ApplicationId" />.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.ApplicationId" /> Объект, сравниваемый с текущим <see cref="T:System.ApplicationId" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный <see cref="T:System.ApplicationId" /> объект эквивалентен текущему <see cref="T:System.ApplicationId" />; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      ApplicationId applicationId = o as ApplicationId;
      if (applicationId == null || !object.Equals((object) this.m_name, (object) applicationId.m_name) || (!object.Equals((object) this.m_version, (object) applicationId.m_version) || !object.Equals((object) this.m_processorArchitecture, (object) applicationId.m_processorArchitecture)) || (!object.Equals((object) this.m_culture, (object) applicationId.m_culture) || this.m_publicKeyToken.Length != applicationId.m_publicKeyToken.Length))
        return false;
      for (int index = 0; index < this.m_publicKeyToken.Length; ++index)
      {
        if ((int) this.m_publicKeyToken[index] != (int) applicationId.m_publicKeyToken[index])
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего удостоверения приложения.
    /// </summary>
    /// <returns>Хэш-код текущего удостоверения приложения.</returns>
    public override int GetHashCode()
    {
      return this.m_name.GetHashCode() ^ this.m_version.GetHashCode();
    }
  }
}
