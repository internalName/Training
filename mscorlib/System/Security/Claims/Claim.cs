// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.Claim
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace System.Security.Claims
{
  /// <summary>Представляет утверждение.</summary>
  [Serializable]
  public class Claim
  {
    [NonSerialized]
    private object m_propertyLock = new object();
    private string m_issuer;
    private string m_originalIssuer;
    private string m_type;
    private string m_value;
    private string m_valueType;
    [NonSerialized]
    private byte[] m_userSerializationData;
    private Dictionary<string, string> m_properties;
    [NonSerialized]
    private ClaimsIdentity m_subject;

    /// <summary />
    /// <param name="reader" />
    public Claim(BinaryReader reader)
      : this(reader, (ClaimsIdentity) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.Claim" /> заданным модулем чтения и субъектом.
    /// </summary>
    /// <param name="reader">Модуль чтения двоичных данных.</param>
    /// <param name="subject">
    ///   Субъект, который описывает данное утверждение.
    /// </param>
    public Claim(BinaryReader reader, ClaimsIdentity subject)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      this.Initialize(reader, subject);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.Claim" /> с указанным типом утверждения и значением.
    /// </summary>
    /// <param name="type">Тип утверждения.</param>
    /// <param name="value">Значение утверждения.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> или <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public Claim(string type, string value)
      : this(type, value, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", (ClaimsIdentity) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.Claim" /> указанным типом утверждения, значением и типом значения.
    /// </summary>
    /// <param name="type">Тип утверждения.</param>
    /// <param name="value">Значение утверждения.</param>
    /// <param name="valueType">
    ///   Тип значения утверждения.
    ///    Если этот параметр имеет значение <see langword="null" />, используется <see cref="F:System.Security.Claims.ClaimValueTypes.String" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> или <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public Claim(string type, string value, string valueType)
      : this(type, value, valueType, "LOCAL AUTHORITY", "LOCAL AUTHORITY", (ClaimsIdentity) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.Claim" /> указанным типом утверждения, значением, типом значения и издателем.
    /// </summary>
    /// <param name="type">Тип утверждения.</param>
    /// <param name="value">Значение утверждения.</param>
    /// <param name="valueType">
    ///   Тип значения утверждения.
    ///    Если этот параметр имеет значение <see langword="null" />, используется <see cref="F:System.Security.Claims.ClaimValueTypes.String" />.
    /// </param>
    /// <param name="issuer">
    ///   Издатель утверждения.
    ///    Если этот параметр пуст или имеет значение <see langword="null" />, используется <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> или <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public Claim(string type, string value, string valueType, string issuer)
      : this(type, value, valueType, issuer, issuer, (ClaimsIdentity) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.Claim" /> с заданным типом утверждения, значением, типом значения, издателем и первоначальным издателем.
    /// </summary>
    /// <param name="type">Тип утверждения.</param>
    /// <param name="value">Значение утверждения.</param>
    /// <param name="valueType">
    ///   Тип значения утверждения.
    ///    Если этот параметр имеет значение <see langword="null" />, используется <see cref="F:System.Security.Claims.ClaimValueTypes.String" />.
    /// </param>
    /// <param name="issuer">
    ///   Издатель утверждения.
    ///    Если этот параметр пуст или имеет значение <see langword="null" />, используется <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" />.
    /// </param>
    /// <param name="originalIssuer">
    ///   Первоначальный издатель утверждения.
    ///    Если этот параметр пуст или имеет значение <see langword="null" />, то для свойства <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> задается значение свойства <see cref="P:System.Security.Claims.Claim.Issuer" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> или <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public Claim(string type, string value, string valueType, string issuer, string originalIssuer)
      : this(type, value, valueType, issuer, originalIssuer, (ClaimsIdentity) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.Claim" /> с заданным типом утверждения, значением, типом значения, издателем, первоначальным издателем и темой.
    /// </summary>
    /// <param name="type">Тип утверждения.</param>
    /// <param name="value">Значение утверждения.</param>
    /// <param name="valueType">
    ///   Тип значения утверждения.
    ///    Если этот параметр имеет значение <see langword="null" />, используется <see cref="F:System.Security.Claims.ClaimValueTypes.String" />.
    /// </param>
    /// <param name="issuer">
    ///   Издатель утверждения.
    ///    Если этот параметр пуст или имеет значение <see langword="null" />, используется <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" />.
    /// </param>
    /// <param name="originalIssuer">
    ///   Первоначальный издатель утверждения.
    ///    Если этот параметр пуст или имеет значение <see langword="null" />, то <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> свойству задается значение свойства <see cref="P:System.Security.Claims.Claim.Issuer" />.
    /// </param>
    /// <param name="subject">
    ///   Тема, которую описывает данное утверждение.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> или <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject)
      : this(type, value, valueType, issuer, originalIssuer, subject, (string) null, (string) null)
    {
    }

    internal Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject, string propertyKey, string propertyValue)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      this.m_type = type;
      this.m_value = value;
      this.m_valueType = !string.IsNullOrEmpty(valueType) ? valueType : "http://www.w3.org/2001/XMLSchema#string";
      this.m_issuer = !string.IsNullOrEmpty(issuer) ? issuer : "LOCAL AUTHORITY";
      this.m_originalIssuer = !string.IsNullOrEmpty(originalIssuer) ? originalIssuer : this.m_issuer;
      this.m_subject = subject;
      if (propertyKey == null)
        return;
      this.Properties.Add(propertyKey, propertyValue);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.Claim" />.
    /// </summary>
    /// <param name="other">Утверждение безопасности.</param>
    protected Claim(Claim other)
      : this(other, other == null ? (ClaimsIdentity) null : other.m_subject)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.Claim" /> указанным утверждением безопасности и субъектом.
    /// </summary>
    /// <param name="other">Утверждение безопасности.</param>
    /// <param name="subject">
    ///   Субъект, который описывает данное утверждение.
    /// </param>
    protected Claim(Claim other, ClaimsIdentity subject)
    {
      if (other == null)
        throw new ArgumentNullException(nameof (other));
      this.m_issuer = other.m_issuer;
      this.m_originalIssuer = other.m_originalIssuer;
      this.m_subject = subject;
      this.m_type = other.m_type;
      this.m_value = other.m_value;
      this.m_valueType = other.m_valueType;
      if (other.m_properties != null)
      {
        this.m_properties = new Dictionary<string, string>();
        foreach (string key in other.m_properties.Keys)
          this.m_properties.Add(key, other.m_properties[key]);
      }
      if (other.m_userSerializationData == null)
        return;
      this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
    }

    /// <summary />
    /// <returns>
    ///   Возвращает <see cref="T:System.Byte" />.
    /// </returns>
    protected virtual byte[] CustomSerializationData
    {
      get
      {
        return this.m_userSerializationData;
      }
    }

    /// <summary>Возвращает издатель утверждения.</summary>
    /// <returns>Имя, которое ссылается на издатель утверждения.</returns>
    public string Issuer
    {
      get
      {
        return this.m_issuer;
      }
    }

    [OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
      this.m_propertyLock = new object();
    }

    /// <summary>Возвращает исходный издатель утверждения.</summary>
    /// <returns>
    ///   Имя, которое ссылается на исходный издатель утверждения.
    /// </returns>
    public string OriginalIssuer
    {
      get
      {
        return this.m_originalIssuer;
      }
    }

    /// <summary>
    ///   Возвращает словарь, содержащий дополнительные свойства, связанные с этим утверждением.
    /// </summary>
    /// <returns>
    ///   Словарь, содержащий дополнительные свойства, связанные с этим утверждением.
    ///    Свойства представлены в виде пар "имя-значение".
    /// </returns>
    public IDictionary<string, string> Properties
    {
      get
      {
        if (this.m_properties == null)
        {
          lock (this.m_propertyLock)
          {
            if (this.m_properties == null)
              this.m_properties = new Dictionary<string, string>();
          }
        }
        return (IDictionary<string, string>) this.m_properties;
      }
    }

    /// <summary>Получает субъект утверждения.</summary>
    /// <returns>Субъект утверждения.</returns>
    public ClaimsIdentity Subject
    {
      get
      {
        return this.m_subject;
      }
      internal set
      {
        this.m_subject = value;
      }
    }

    /// <summary>Получает тип утверждения.</summary>
    /// <returns>Тип утверждения.</returns>
    public string Type
    {
      get
      {
        return this.m_type;
      }
    }

    /// <summary>Возвращает значение утверждения.</summary>
    /// <returns>Значение утверждения.</returns>
    public string Value
    {
      get
      {
        return this.m_value;
      }
    }

    /// <summary>Возвращает тип значения утверждения.</summary>
    /// <returns>Тип значения утверждения.</returns>
    public string ValueType
    {
      get
      {
        return this.m_valueType;
      }
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.Security.Claims.Claim" />, скопированный из этого объекта.
    ///    Новое утверждение не имеет субъекта.
    /// </summary>
    /// <returns>Новый объект утверждения.</returns>
    public virtual Claim Clone()
    {
      return this.Clone((ClaimsIdentity) null);
    }

    /// <summary>
    ///   Возвращает новый объект <see cref="T:System.Security.Claims.Claim" />, скопированный из этого объекта.
    ///    Субъект нового утверждения имеет значение указанного ClaimsIdentity.
    /// </summary>
    /// <param name="identity">
    ///   Предполагаемый субъект нового утверждения.
    /// </param>
    /// <returns>Новый объект утверждения.</returns>
    public virtual Claim Clone(ClaimsIdentity identity)
    {
      return new Claim(this, identity);
    }

    private void Initialize(BinaryReader reader, ClaimsIdentity subject)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      this.m_subject = subject;
      Claim.SerializationMask serializationMask = (Claim.SerializationMask) reader.ReadInt32();
      int num1 = 1;
      int num2 = reader.ReadInt32();
      this.m_value = reader.ReadString();
      if ((serializationMask & Claim.SerializationMask.NameClaimType) == Claim.SerializationMask.NameClaimType)
        this.m_type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
      else if ((serializationMask & Claim.SerializationMask.RoleClaimType) == Claim.SerializationMask.RoleClaimType)
      {
        this.m_type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
      }
      else
      {
        this.m_type = reader.ReadString();
        ++num1;
      }
      if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
      {
        this.m_valueType = reader.ReadString();
        ++num1;
      }
      else
        this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
      if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
      {
        this.m_issuer = reader.ReadString();
        ++num1;
      }
      else
        this.m_issuer = "LOCAL AUTHORITY";
      if ((serializationMask & Claim.SerializationMask.OriginalIssuerEqualsIssuer) == Claim.SerializationMask.OriginalIssuerEqualsIssuer)
        this.m_originalIssuer = this.m_issuer;
      else if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
      {
        this.m_originalIssuer = reader.ReadString();
        ++num1;
      }
      else
        this.m_originalIssuer = "LOCAL AUTHORITY";
      if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
      {
        int num3 = reader.ReadInt32();
        for (int index = 0; index < num3; ++index)
          this.Properties.Add(reader.ReadString(), reader.ReadString());
      }
      if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
      {
        int count = reader.ReadInt32();
        this.m_userSerializationData = reader.ReadBytes(count);
        ++num1;
      }
      for (int index = num1; index < num2; ++index)
        reader.ReadString();
    }

    /// <summary />
    /// <param name="writer" />
    public virtual void WriteTo(BinaryWriter writer)
    {
      this.WriteTo(writer, (byte[]) null);
    }

    /// <summary>
    ///   Записывает это утверждение <see cref="T:System.Security.Claims.Claim" /> в модуль записи.
    /// </summary>
    /// <param name="writer">
    ///   Модуль записи, в который производится запись этого утверждения.
    /// </param>
    /// <param name="userData">
    ///   Данные пользователя для утверждения.
    /// </param>
    protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      int num = 1;
      Claim.SerializationMask serializationMask = Claim.SerializationMask.None;
      if (string.Equals(this.m_type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
        serializationMask |= Claim.SerializationMask.NameClaimType;
      else if (string.Equals(this.m_type, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
        serializationMask |= Claim.SerializationMask.RoleClaimType;
      else
        ++num;
      if (!string.Equals(this.m_valueType, "http://www.w3.org/2001/XMLSchema#string", StringComparison.Ordinal))
      {
        ++num;
        serializationMask |= Claim.SerializationMask.StringType;
      }
      if (!string.Equals(this.m_issuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
      {
        ++num;
        serializationMask |= Claim.SerializationMask.Issuer;
      }
      if (string.Equals(this.m_originalIssuer, this.m_issuer, StringComparison.Ordinal))
        serializationMask |= Claim.SerializationMask.OriginalIssuerEqualsIssuer;
      else if (!string.Equals(this.m_originalIssuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
      {
        ++num;
        serializationMask |= Claim.SerializationMask.OriginalIssuer;
      }
      if (this.Properties.Count > 0)
      {
        ++num;
        serializationMask |= Claim.SerializationMask.HasProperties;
      }
      if (userData != null && userData.Length != 0)
      {
        ++num;
        serializationMask |= Claim.SerializationMask.UserData;
      }
      writer.Write((int) serializationMask);
      writer.Write(num);
      writer.Write(this.m_value);
      if ((serializationMask & Claim.SerializationMask.NameClaimType) != Claim.SerializationMask.NameClaimType && (serializationMask & Claim.SerializationMask.RoleClaimType) != Claim.SerializationMask.RoleClaimType)
        writer.Write(this.m_type);
      if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
        writer.Write(this.m_valueType);
      if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
        writer.Write(this.m_issuer);
      if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
        writer.Write(this.m_originalIssuer);
      if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
      {
        writer.Write(this.Properties.Count);
        foreach (string key in (IEnumerable<string>) this.Properties.Keys)
        {
          writer.Write(key);
          writer.Write(this.Properties[key]);
        }
      }
      if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
      {
        writer.Write(userData.Length);
        writer.Write(userData);
      }
      writer.Flush();
    }

    /// <summary>
    ///   Возвращает строковое представление этого объекта <see cref="T:System.Security.Claims.Claim" />.
    /// </summary>
    /// <returns>
    ///   Строковое представление этого объекта <see cref="T:System.Security.Claims.Claim" />.
    /// </returns>
    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}: {1}", (object) this.m_type, (object) this.m_value);
    }

    private enum SerializationMask
    {
      None = 0,
      NameClaimType = 1,
      RoleClaimType = 2,
      StringType = 4,
      Issuer = 8,
      OriginalIssuerEqualsIssuer = 16, // 0x00000010
      OriginalIssuer = 32, // 0x00000020
      HasProperties = 64, // 0x00000040
      UserData = 128, // 0x00000080
    }
  }
}
