// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.ClaimsPrincipal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Security.Claims
{
  /// <summary>
  ///   Реализация интерфейса <see cref="T:System.Security.Principal.IPrincipal" />, которая поддерживает несколько удостоверений, основанных на утверждениях.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class ClaimsPrincipal : IPrincipal
  {
    [NonSerialized]
    private static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> s_identitySelector = new Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity>(ClaimsPrincipal.SelectPrimaryIdentity);
    [NonSerialized]
    private static Func<ClaimsPrincipal> s_principalSelector = ClaimsPrincipal.ClaimsPrincipalSelector;
    [OptionalField(VersionAdded = 2)]
    private string m_version = "1.0";
    [NonSerialized]
    private List<ClaimsIdentity> m_identities = new List<ClaimsIdentity>();
    [NonSerialized]
    private byte[] m_userSerializationData;
    [NonSerialized]
    private const string PreFix = "System.Security.ClaimsPrincipal.";
    [NonSerialized]
    private const string IdentitiesKey = "System.Security.ClaimsPrincipal.Identities";
    [NonSerialized]
    private const string VersionKey = "System.Security.ClaimsPrincipal.Version";
    [OptionalField(VersionAdded = 2)]
    private string m_serializedClaimsIdentities;

    private static ClaimsIdentity SelectPrimaryIdentity(IEnumerable<ClaimsIdentity> identities)
    {
      if (identities == null)
        throw new ArgumentNullException(nameof (identities));
      ClaimsIdentity claimsIdentity = (ClaimsIdentity) null;
      foreach (ClaimsIdentity identity in identities)
      {
        if (identity is WindowsIdentity)
        {
          claimsIdentity = identity;
          break;
        }
        if (claimsIdentity == null)
          claimsIdentity = identity;
      }
      return claimsIdentity;
    }

    private static ClaimsPrincipal SelectClaimsPrincipal()
    {
      return Thread.CurrentPrincipal as ClaimsPrincipal ?? new ClaimsPrincipal(Thread.CurrentPrincipal);
    }

    /// <summary>
    ///   Возвращает или задает делегат, используемый для выбора удостоверения утверждений, возвращенного свойством <see cref="P:System.Security.Claims.ClaimsPrincipal.Identity" />.
    /// </summary>
    /// <returns>
    ///   Делегат.
    ///    Значение по умолчанию — <see langword="null" />.
    /// </returns>
    public static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> PrimaryIdentitySelector
    {
      get
      {
        return ClaimsPrincipal.s_identitySelector;
      }
      [SecurityCritical] set
      {
        ClaimsPrincipal.s_identitySelector = value;
      }
    }

    /// <summary>
    ///   Получает или задает делегат, используемый для выбора субъекта утверждений, возвращенного свойством <see cref="P:System.Security.Claims.ClaimsPrincipal.Current" />.
    /// </summary>
    /// <returns>
    ///   Делегат.
    ///    Значение по умолчанию — <see langword="null" />.
    /// </returns>
    public static Func<ClaimsPrincipal> ClaimsPrincipalSelector
    {
      get
      {
        return ClaimsPrincipal.s_principalSelector;
      }
      [SecurityCritical] set
      {
        ClaimsPrincipal.s_principalSelector = value;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.ClaimsPrincipal" />.
    /// </summary>
    public ClaimsPrincipal()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.ClaimsPrincipal" />, используя указанные удостоверения, основанные на утверждениях.
    /// </summary>
    /// <param name="identities">
    ///   Удостоверения, из которых требуется инициализировать новый субъект утверждений.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="identities" /> имеет значение NULL.
    /// </exception>
    public ClaimsPrincipal(IEnumerable<ClaimsIdentity> identities)
    {
      if (identities == null)
        throw new ArgumentNullException(nameof (identities));
      this.m_identities.AddRange(identities);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.ClaimsPrincipal" /> из указанного удостоверения.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, из которого требуется инициализировать новый субъект утверждений.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="identity" /> имеет значение NULL.
    /// </exception>
    public ClaimsPrincipal(IIdentity identity)
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
      if (claimsIdentity != null)
        this.m_identities.Add(claimsIdentity);
      else
        this.m_identities.Add(new ClaimsIdentity(identity));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Claims.ClaimsPrincipal" /> из указанного субъекта.
    /// </summary>
    /// <param name="principal">
    ///   Субъект, из которого требуется инициализировать новый субъект утверждений.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="principal" /> имеет значение NULL.
    /// </exception>
    public ClaimsPrincipal(IPrincipal principal)
    {
      if (principal == null)
        throw new ArgumentNullException(nameof (principal));
      ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;
      if (claimsPrincipal == null)
      {
        this.m_identities.Add(new ClaimsIdentity(principal.Identity));
      }
      else
      {
        if (claimsPrincipal.Identities == null)
          return;
        this.m_identities.AddRange(claimsPrincipal.Identities);
      }
    }

    /// <summary />
    /// <param name="reader" />
    public ClaimsPrincipal(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      this.Initialize(reader);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsPrincipal" /> класс из сериализованного потока, созданных с помощью <see cref="T:System.Runtime.Serialization.ISerializable" />.
    /// </summary>
    /// <param name="info">Сериализованные данные.</param>
    /// <param name="context">Контекст сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="info" /> имеет значение NULL.
    /// </exception>
    [SecurityCritical]
    protected ClaimsPrincipal(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.Deserialize(info, context);
    }

    /// <summary />
    protected virtual byte[] CustomSerializationData
    {
      get
      {
        return this.m_userSerializationData;
      }
    }

    /// <summary>Возвращает копию этого экземпляра.</summary>
    /// <returns>
    ///   Новая копия объекта <see cref="T:System.Security.Claims.ClaimsPrincipal" />.
    /// </returns>
    public virtual ClaimsPrincipal Clone()
    {
      return new ClaimsPrincipal((IPrincipal) this);
    }

    /// <summary>
    ///   Создает новое удостоверение, основанное на утверждениях.
    /// </summary>
    /// <param name="reader">Средство чтения двоичных данных.</param>
    /// <returns>Удостоверение, основанное на утверждениях.</returns>
    protected virtual ClaimsIdentity CreateClaimsIdentity(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      return new ClaimsIdentity(reader);
    }

    [OnSerializing]
    [SecurityCritical]
    private void OnSerializingMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      this.m_serializedClaimsIdentities = this.SerializeIdentities();
    }

    [OnDeserialized]
    [SecurityCritical]
    private void OnDeserializedMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      this.DeserializeIdentities(this.m_serializedClaimsIdentities);
      this.m_serializedClaimsIdentities = (string) null;
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> данными, необходимыми для сериализации текущего объекта <see cref="T:System.Security.Claims.ClaimsPrincipal" />.
    /// </summary>
    /// <param name="info">
    ///   Объект, который требуется заполнить данными.
    /// </param>
    /// <param name="context">
    ///   Целевой объект этой сериализации.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
    protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("System.Security.ClaimsPrincipal.Identities", (object) this.SerializeIdentities());
      info.AddValue("System.Security.ClaimsPrincipal.Version", (object) this.m_version);
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
    private void Deserialize(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        if (!(name == "System.Security.ClaimsPrincipal.Identities"))
        {
          if (name == "System.Security.ClaimsPrincipal.Version")
            this.m_version = info.GetString("System.Security.ClaimsPrincipal.Version");
        }
        else
          this.DeserializeIdentities(info.GetString("System.Security.ClaimsPrincipal.Identities"));
      }
    }

    [SecurityCritical]
    private void DeserializeIdentities(string identities)
    {
      this.m_identities = new List<ClaimsIdentity>();
      if (string.IsNullOrEmpty(identities))
        return;
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      using (MemoryStream memoryStream1 = new MemoryStream(Convert.FromBase64String(identities)))
      {
        List<string> stringList = (List<string>) binaryFormatter.Deserialize((Stream) memoryStream1, (HeaderHandler) null, false);
        int index = 0;
        while (index < stringList.Count)
        {
          ClaimsIdentity claimsIdentity = (ClaimsIdentity) null;
          using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(stringList[index + 1])))
            claimsIdentity = (ClaimsIdentity) binaryFormatter.Deserialize((Stream) memoryStream2, (HeaderHandler) null, false);
          if (!string.IsNullOrEmpty(stringList[index]))
          {
            long result;
            if (!long.TryParse(stringList[index], NumberStyles.Integer, (IFormatProvider) NumberFormatInfo.InvariantInfo, out result))
              throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
            claimsIdentity = (ClaimsIdentity) new WindowsIdentity(claimsIdentity, new IntPtr(result));
          }
          this.m_identities.Add(claimsIdentity);
          index += 2;
        }
      }
    }

    [SecurityCritical]
    private string SerializeIdentities()
    {
      List<string> stringList = new List<string>();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      foreach (ClaimsIdentity identity in this.m_identities)
      {
        if (identity.GetType() == typeof (WindowsIdentity))
        {
          WindowsIdentity windowsIdentity = identity as WindowsIdentity;
          stringList.Add(windowsIdentity.GetTokenInternal().ToInt64().ToString((IFormatProvider) NumberFormatInfo.InvariantInfo));
          using (MemoryStream memoryStream = new MemoryStream())
          {
            binaryFormatter.Serialize((Stream) memoryStream, (object) windowsIdentity.CloneAsBase(), (Header[]) null, false);
            stringList.Add(Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length));
          }
        }
        else
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            stringList.Add("");
            binaryFormatter.Serialize((Stream) memoryStream, (object) identity, (Header[]) null, false);
            stringList.Add(Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length));
          }
        }
      }
      using (MemoryStream memoryStream = new MemoryStream())
      {
        binaryFormatter.Serialize((Stream) memoryStream, (object) stringList, (Header[]) null, false);
        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length);
      }
    }

    /// <summary>
    ///   Добавляет указанное удостоверение утверждения к этому участнику утверждений.
    /// </summary>
    /// <param name="identity">
    ///   Добавляемое удостоверение утверждения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="identity" /> имеет значение NULL.
    /// </exception>
    [SecurityCritical]
    public virtual void AddIdentity(ClaimsIdentity identity)
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      this.m_identities.Add(identity);
    }

    /// <summary>
    ///   Добавляет указанные удостоверения, основанные на утверждениях, в этот субъект утверждений.
    /// </summary>
    /// <param name="identities">
    ///   Добавляемые удостоверения, основанные на утверждениях.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="identities" /> имеет значение NULL.
    /// </exception>
    [SecurityCritical]
    public virtual void AddIdentities(IEnumerable<ClaimsIdentity> identities)
    {
      if (identities == null)
        throw new ArgumentNullException(nameof (identities));
      this.m_identities.AddRange(identities);
    }

    /// <summary>
    ///   Возвращает коллекцию, содержащую все утверждения из всех удостоверений, основанных на утверждениях, которые связаны с этим субъектом утверждений.
    /// </summary>
    /// <returns>Утверждения, связанные с этим субъектом.</returns>
    public virtual IEnumerable<Claim> Claims
    {
      get
      {
        foreach (ClaimsIdentity identity in this.Identities)
        {
          foreach (Claim claim in identity.Claims)
            yield return claim;
        }
      }
    }

    /// <summary>Получает текущий субъект утверждений.</summary>
    /// <returns>Текущий субъект утверждений.</returns>
    public static ClaimsPrincipal Current
    {
      get
      {
        if (ClaimsPrincipal.s_principalSelector != null)
          return ClaimsPrincipal.s_principalSelector();
        return ClaimsPrincipal.SelectClaimsPrincipal();
      }
    }

    /// <summary>
    ///   Извлекает все утверждения, сопоставленные указанным предикатом.
    /// </summary>
    /// <param name="match">
    ///   Функция, выполняющая логику сопоставления.
    /// </param>
    /// <returns>Утверждения сопоставления.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="match" /> имеет значение NULL.
    /// </exception>
    public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
    {
      if (match == null)
        throw new ArgumentNullException(nameof (match));
      List<Claim> claimList = new List<Claim>();
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          foreach (Claim claim in identity.FindAll(match))
            claimList.Add(claim);
        }
      }
      return (IEnumerable<Claim>) claimList.AsReadOnly();
    }

    /// <summary>
    ///   Извлекает все утверждения или утверждения, которые имеют заданный тип утверждения.
    /// </summary>
    /// <param name="type">
    ///   Тип утверждения, с которым будут сопоставляться утверждения.
    /// </param>
    /// <returns>Соответствующие утверждения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="type" /> имеет значение NULL.
    /// </exception>
    public virtual IEnumerable<Claim> FindAll(string type)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      List<Claim> claimList = new List<Claim>();
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          foreach (Claim claim in identity.FindAll(type))
            claimList.Add(claim);
        }
      }
      return (IEnumerable<Claim>) claimList.AsReadOnly();
    }

    /// <summary>
    ///   Извлекает первое утверждение, которое соответствует указанному предикату.
    /// </summary>
    /// <param name="match">
    ///   Функция, выполняющая логику сопоставления.
    /// </param>
    /// <returns>
    ///   Первое соответствующее утверждение или <see langword="null" />, если соответствие не найдено.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="match" /> имеет значение NULL.
    /// </exception>
    public virtual Claim FindFirst(Predicate<Claim> match)
    {
      if (match == null)
        throw new ArgumentNullException(nameof (match));
      Claim claim = (Claim) null;
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          claim = identity.FindFirst(match);
          if (claim != null)
            return claim;
        }
      }
      return claim;
    }

    /// <summary>
    ///   Извлекает первое утверждение с заданным типом утверждения.
    /// </summary>
    /// <param name="type">Тип утверждения для сопоставления.</param>
    /// <returns>
    ///   Первое соответствующее утверждение или <see langword="null" />, если соответствие не найдено.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="type" /> имеет значение NULL.
    /// </exception>
    public virtual Claim FindFirst(string type)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      Claim claim = (Claim) null;
      for (int index = 0; index < this.m_identities.Count; ++index)
      {
        if (this.m_identities[index] != null)
        {
          claim = this.m_identities[index].FindFirst(type);
          if (claim != null)
            return claim;
        }
      }
      return claim;
    }

    /// <summary>
    ///   Определяет, содержат ли удостоверения, основанные на утверждениях и связанные с субъектом утверждений, утверждения, которые соответствуют указанному предикату.
    /// </summary>
    /// <param name="match">
    ///   Функция, выполняющая логику сопоставления.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если совпадающее утверждение существует; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="match" /> имеет значение NULL.
    /// </exception>
    public virtual bool HasClaim(Predicate<Claim> match)
    {
      if (match == null)
        throw new ArgumentNullException(nameof (match));
      for (int index = 0; index < this.m_identities.Count; ++index)
      {
        if (this.m_identities[index] != null && this.m_identities[index].HasClaim(match))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Определяет, содержат ли удостоверения утверждений, связанные с субъектом утверждений, утверждения с указанным типом и значением.
    /// </summary>
    /// <param name="type">Тип утверждения для сопоставления.</param>
    /// <param name="value">
    ///   Значение утверждения для сопоставления.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если совпадающее утверждение существует; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="type" /> имеет значение NULL.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> имеет значение NULL.
    /// </exception>
    public virtual bool HasClaim(string type, string value)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      for (int index = 0; index < this.m_identities.Count; ++index)
      {
        if (this.m_identities[index] != null && this.m_identities[index].HasClaim(type, value))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Возвращает коллекцию, содержащую все удостоверения утверждений, связанные с этим субъектом утверждений.
    /// </summary>
    /// <returns>Коллекция удостоверений утверждений.</returns>
    public virtual IEnumerable<ClaimsIdentity> Identities
    {
      get
      {
        return (IEnumerable<ClaimsIdentity>) this.m_identities.AsReadOnly();
      }
    }

    /// <summary>
    ///   Получает основное удостоверение, основанное на утверждениях, связанное с этим субъектом утверждений.
    /// </summary>
    /// <returns>
    ///   Основное удостоверение, основанное на утверждениях, связанное с этим субъектом утверждений.
    /// </returns>
    public virtual IIdentity Identity
    {
      get
      {
        if (ClaimsPrincipal.s_identitySelector != null)
          return (IIdentity) ClaimsPrincipal.s_identitySelector((IEnumerable<ClaimsIdentity>) this.m_identities);
        return (IIdentity) ClaimsPrincipal.SelectPrimaryIdentity((IEnumerable<ClaimsIdentity>) this.m_identities);
      }
    }

    /// <summary>
    ///   Возвращает значение, которое указывает, находится ли сущность (пользователь), представленная этим субъектом утверждений, в указанной роли.
    /// </summary>
    /// <param name="role">Роль, которую требуется проверить.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если субъект утверждения находится в указанной роли; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool IsInRole(string role)
    {
      for (int index = 0; index < this.m_identities.Count; ++index)
      {
        if (this.m_identities[index] != null && this.m_identities[index].HasClaim(this.m_identities[index].RoleClaimType, role))
          return true;
      }
      return false;
    }

    private void Initialize(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      ClaimsPrincipal.SerializationMask serializationMask = (ClaimsPrincipal.SerializationMask) reader.ReadInt32();
      int num1 = reader.ReadInt32();
      int num2 = 0;
      if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
      {
        ++num2;
        int num3 = reader.ReadInt32();
        for (int index = 0; index < num3; ++index)
          this.m_identities.Add(this.CreateClaimsIdentity(reader));
      }
      if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
      {
        int count = reader.ReadInt32();
        this.m_userSerializationData = reader.ReadBytes(count);
        ++num2;
      }
      for (int index = num2; index < num1; ++index)
        reader.ReadString();
    }

    /// <summary />
    /// <param name="writer" />
    public virtual void WriteTo(BinaryWriter writer)
    {
      this.WriteTo(writer, (byte[]) null);
    }

    /// <summary />
    /// <param name="writer" />
    /// <param name="userData" />
    protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      int num = 0;
      ClaimsPrincipal.SerializationMask serializationMask = ClaimsPrincipal.SerializationMask.None;
      if (this.m_identities.Count > 0)
      {
        serializationMask |= ClaimsPrincipal.SerializationMask.HasIdentities;
        ++num;
      }
      if (userData != null && userData.Length != 0)
      {
        ++num;
        serializationMask |= ClaimsPrincipal.SerializationMask.UserData;
      }
      writer.Write((int) serializationMask);
      writer.Write(num);
      if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
      {
        writer.Write(this.m_identities.Count);
        foreach (ClaimsIdentity identity in this.m_identities)
          identity.WriteTo(writer);
      }
      if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
      {
        writer.Write(userData.Length);
        writer.Write(userData);
      }
      writer.Flush();
    }

    private enum SerializationMask
    {
      None,
      HasIdentities,
      UserData,
    }
  }
}
