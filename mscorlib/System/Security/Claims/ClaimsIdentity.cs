// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.ClaimsIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.Claims
{
  /// <summary>Предоставляет удостоверение на основе утверждений.</summary>
  [ComVisible(true)]
  [Serializable]
  public class ClaimsIdentity : IIdentity
  {
    [NonSerialized]
    private List<Claim> m_instanceClaims = new List<Claim>();
    [NonSerialized]
    private Collection<IEnumerable<Claim>> m_externalClaims = new Collection<IEnumerable<Claim>>();
    [NonSerialized]
    private string m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    [NonSerialized]
    private string m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    [OptionalField(VersionAdded = 2)]
    private string m_version = "1.0";
    [NonSerialized]
    private byte[] m_userSerializationData;
    [NonSerialized]
    private const string PreFix = "System.Security.ClaimsIdentity.";
    [NonSerialized]
    private const string ActorKey = "System.Security.ClaimsIdentity.actor";
    [NonSerialized]
    private const string AuthenticationTypeKey = "System.Security.ClaimsIdentity.authenticationType";
    [NonSerialized]
    private const string BootstrapContextKey = "System.Security.ClaimsIdentity.bootstrapContext";
    [NonSerialized]
    private const string ClaimsKey = "System.Security.ClaimsIdentity.claims";
    [NonSerialized]
    private const string LabelKey = "System.Security.ClaimsIdentity.label";
    [NonSerialized]
    private const string NameClaimTypeKey = "System.Security.ClaimsIdentity.nameClaimType";
    [NonSerialized]
    private const string RoleClaimTypeKey = "System.Security.ClaimsIdentity.roleClaimType";
    [NonSerialized]
    private const string VersionKey = "System.Security.ClaimsIdentity.version";
    /// <summary>Издатель по умолчанию; «LOCAL AUTHORITY».</summary>
    [NonSerialized]
    public const string DefaultIssuer = "LOCAL AUTHORITY";
    /// <summary>
    ///   Имя по умолчанию утверждения типа; <see cref="F:System.Security.Claims.ClaimTypes.Name" />.
    /// </summary>
    [NonSerialized]
    public const string DefaultNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    /// <summary>
    ///   По умолчанию роль утверждения типа; <see cref="F:System.Security.Claims.ClaimTypes.Role" />.
    /// </summary>
    [NonSerialized]
    public const string DefaultRoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    [OptionalField(VersionAdded = 2)]
    private ClaimsIdentity m_actor;
    [OptionalField(VersionAdded = 2)]
    private string m_authenticationType;
    [OptionalField(VersionAdded = 2)]
    private object m_bootstrapContext;
    [OptionalField(VersionAdded = 2)]
    private string m_label;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedNameType;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedRoleType;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedClaims;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> класса с пустой коллекции утверждений.
    /// </summary>
    public ClaimsIdentity()
      : this((IEnumerable<Claim>) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> класса с помощью имени и проверки подлинности с указанным <see cref="T:System.Security.Principal.IIdentity" />.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение на основе утверждений удостоверений, из которого необходимо создать новый.
    /// </param>
    public ClaimsIdentity(IIdentity identity)
      : this(identity, (IEnumerable<Claim>) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> класса с помощью перечисления коллекции <see cref="T:System.Security.Claims.Claim" /> объектов.
    /// </summary>
    /// <param name="claims">
    ///   Утверждения, с которого следует заполнить удостоверение утверждений.
    /// </param>
    public ClaimsIdentity(IEnumerable<Claim> claims)
      : this((IIdentity) null, claims, (string) null, (string) null, (string) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> утверждений класс с пустой коллекции и типом проверки подлинности.
    /// </summary>
    /// <param name="authenticationType">Тип проверки подлинности.</param>
    public ClaimsIdentity(string authenticationType)
      : this((IIdentity) null, (IEnumerable<Claim>) null, authenticationType, (string) null, (string) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> класса с указанным утверждения и тип проверки подлинности.
    /// </summary>
    /// <param name="claims">
    ///   Утверждения, с которого следует заполнить удостоверение утверждений.
    /// </param>
    /// <param name="authenticationType">Тип проверки подлинности.</param>
    public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType)
      : this((IIdentity) null, claims, authenticationType, (string) null, (string) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> класса, используя указанные утверждения и указанным <see cref="T:System.Security.Principal.IIdentity" />.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение на основе утверждений удостоверений, из которого необходимо создать новый.
    /// </param>
    /// <param name="claims">
    ///   Утверждения, с которого следует заполнить удостоверение утверждений.
    /// </param>
    public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims)
      : this(identity, claims, (string) null, (string) null, (string) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> тип утверждения с типом проверки подлинности, имя типа утверждения и роли.
    /// </summary>
    /// <param name="authenticationType">Тип проверки подлинности.</param>
    /// <param name="nameType">
    ///   Тип утверждения для утверждения с именем.
    /// </param>
    /// <param name="roleType">Тип утверждения для заявок роли.</param>
    public ClaimsIdentity(string authenticationType, string nameType, string roleType)
      : this((IIdentity) null, (IEnumerable<Claim>) null, authenticationType, nameType, roleType)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> тип утверждения с указанным утверждения, тип проверки подлинности, имя типа утверждения и роли.
    /// </summary>
    /// <param name="claims">
    ///   Утверждения, с которого следует заполнить удостоверение утверждений.
    /// </param>
    /// <param name="authenticationType">Тип проверки подлинности.</param>
    /// <param name="nameType">
    ///   Тип утверждения для утверждения с именем.
    /// </param>
    /// <param name="roleType">Тип утверждения для заявок роли.</param>
    public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType)
      : this((IIdentity) null, claims, authenticationType, nameType, roleType)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> класс из указанного <see cref="T:System.Security.Principal.IIdentity" /> тип утверждения с помощью указанного утверждений, тип проверки подлинности, имя типа утверждения и роли.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение на основе утверждений удостоверений, из которого необходимо создать новый.
    /// </param>
    /// <param name="claims">
    ///   Утверждения, с которого следует заполнить новое удостоверение утверждений.
    /// </param>
    /// <param name="authenticationType">Тип проверки подлинности.</param>
    /// <param name="nameType">
    ///   Тип утверждения для утверждения с именем.
    /// </param>
    /// <param name="roleType">Тип утверждения для заявок роли.</param>
    public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType)
      : this(identity, claims, authenticationType, nameType, roleType, true)
    {
    }

    internal ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType, bool checkAuthType)
    {
      bool flag1 = false;
      bool flag2 = false;
      if (checkAuthType && identity != null && string.IsNullOrEmpty(authenticationType))
      {
        if (identity is WindowsIdentity)
        {
          try
          {
            this.m_authenticationType = identity.AuthenticationType;
          }
          catch (UnauthorizedAccessException ex)
          {
            this.m_authenticationType = (string) null;
          }
        }
        else
          this.m_authenticationType = identity.AuthenticationType;
      }
      else
        this.m_authenticationType = authenticationType;
      if (!string.IsNullOrEmpty(nameType))
      {
        this.m_nameType = nameType;
        flag1 = true;
      }
      if (!string.IsNullOrEmpty(roleType))
      {
        this.m_roleType = roleType;
        flag2 = true;
      }
      ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
      if (claimsIdentity != null)
      {
        this.m_label = claimsIdentity.m_label;
        if (!flag1)
          this.m_nameType = claimsIdentity.m_nameType;
        if (!flag2)
          this.m_roleType = claimsIdentity.m_roleType;
        this.m_bootstrapContext = claimsIdentity.m_bootstrapContext;
        if (claimsIdentity.Actor != null)
        {
          if (this.IsCircular(claimsIdentity.Actor))
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
          this.m_actor = AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity ? claimsIdentity.Actor : claimsIdentity.Actor.Clone();
        }
        if (claimsIdentity is WindowsIdentity && !(this is WindowsIdentity))
          this.SafeAddClaims(claimsIdentity.Claims);
        else
          this.SafeAddClaims((IEnumerable<Claim>) claimsIdentity.m_instanceClaims);
        if (claimsIdentity.m_userSerializationData != null)
          this.m_userSerializationData = claimsIdentity.m_userSerializationData.Clone() as byte[];
      }
      else if (identity != null && !string.IsNullOrEmpty(identity.Name))
        this.SafeAddClaim(new Claim(this.m_nameType, identity.Name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
      if (claims == null)
        return;
      this.SafeAddClaims(claims);
    }

    /// <summary />
    /// <param name="reader" />
    public ClaimsIdentity(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      this.Initialize(reader);
    }

    /// <summary />
    /// <param name="other" />
    protected ClaimsIdentity(ClaimsIdentity other)
    {
      if (other == null)
        throw new ArgumentNullException(nameof (other));
      if (other.m_actor != null)
        this.m_actor = other.m_actor.Clone();
      this.m_authenticationType = other.m_authenticationType;
      this.m_bootstrapContext = other.m_bootstrapContext;
      this.m_label = other.m_label;
      this.m_nameType = other.m_nameType;
      this.m_roleType = other.m_roleType;
      if (other.m_userSerializationData != null)
        this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
      this.SafeAddClaims((IEnumerable<Claim>) other.m_instanceClaims);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> класс из сериализованного потока, созданных с помощью <see cref="T:System.Runtime.Serialization.ISerializable" />.
    /// </summary>
    /// <param name="info">Сериализованные данные.</param>
    /// <param name="context">Контекст сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="info" /> имеет значение NULL.
    /// </exception>
    [SecurityCritical]
    protected ClaimsIdentity(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.Deserialize(info, context, true);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Claims.ClaimsIdentity" /> класс из сериализованного потока, созданных с помощью <see cref="T:System.Runtime.Serialization.ISerializable" />.
    /// </summary>
    /// <param name="info">Сериализованные данные.</param>
    [SecurityCritical]
    protected ClaimsIdentity(SerializationInfo info)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      StreamingContext context = new StreamingContext();
      this.Deserialize(info, context, false);
    }

    /// <summary>Возвращает тип проверки подлинности.</summary>
    /// <returns>Тип проверки подлинности.</returns>
    public virtual string AuthenticationType
    {
      get
      {
        return this.m_authenticationType;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, прошел ли проверку подлинности удостоверения.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если удостоверение прошел проверку подлинности; в противном случае — false.
    /// </returns>
    public virtual bool IsAuthenticated
    {
      get
      {
        return !string.IsNullOrEmpty(this.m_authenticationType);
      }
    }

    /// <summary>
    ///   Возвращает или задает удостоверение вызывающей стороны, в которой были предоставлены права делегирования.
    /// </summary>
    /// <returns>
    ///   Вызывающей стороны, в которой были предоставлены права делегирования.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Происходит попытка задать свойство с текущим экземпляром.
    /// </exception>
    public ClaimsIdentity Actor
    {
      get
      {
        return this.m_actor;
      }
      set
      {
        if (value != null && this.IsCircular(value))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
        this.m_actor = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает маркер, который использовался для создания этого утверждений удостоверений.
    /// </summary>
    /// <returns>Контекст загрузки.</returns>
    public object BootstrapContext
    {
      get
      {
        return this.m_bootstrapContext;
      }
      [SecurityCritical] set
      {
        this.m_bootstrapContext = value;
      }
    }

    /// <summary>
    ///   Возвращает утверждения, связанные с этого утверждений удостоверений.
    /// </summary>
    /// <returns>
    ///   Коллекция утверждений, связанных с этого утверждений удостоверений.
    /// </returns>
    public virtual IEnumerable<Claim> Claims
    {
      get
      {
        for (int i = 0; i < this.m_instanceClaims.Count; ++i)
          yield return this.m_instanceClaims[i];
        if (this.m_externalClaims != null)
        {
          for (int j = 0; j < this.m_externalClaims.Count; ++j)
          {
            if (this.m_externalClaims[j] != null)
            {
              foreach (Claim claim in this.m_externalClaims[j])
                yield return claim;
            }
          }
        }
      }
    }

    /// <summary />
    protected virtual byte[] CustomSerializationData
    {
      get
      {
        return this.m_userSerializationData;
      }
    }

    internal Collection<IEnumerable<Claim>> ExternalClaims
    {
      [FriendAccessAllowed] get
      {
        return this.m_externalClaims;
      }
    }

    /// <summary>
    ///   Возвращает или задает метку для этого утверждений удостоверений.
    /// </summary>
    /// <returns>Метка.</returns>
    public string Label
    {
      get
      {
        return this.m_label;
      }
      set
      {
        this.m_label = value;
      }
    }

    /// <summary>
    ///   Возвращает имя этого аспекта утверждений удостоверений.
    /// </summary>
    /// <returns>
    ///   Имя или <see langword="null" />.
    /// </returns>
    public virtual string Name
    {
      get
      {
        return this.FindFirst(this.m_nameType)?.Value;
      }
    }

    /// <summary>
    ///   Возвращает тип утверждения, который используется для определения, какие утверждения предоставить значение для <see cref="P:System.Security.Claims.ClaimsIdentity.Name" /> Свойства объекта утверждений удостоверений.
    /// </summary>
    /// <returns>Имя типа утверждения.</returns>
    public string NameClaimType
    {
      get
      {
        return this.m_nameType;
      }
    }

    /// <summary>
    ///   Возвращает тип утверждения, будут интерпретироваться как роль .NET Framework среди утверждений в этом утверждений удостоверений.
    /// </summary>
    /// <returns>Тип утверждения роли.</returns>
    public string RoleClaimType
    {
      get
      {
        return this.m_roleType;
      }
    }

    /// <summary>
    ///   Возвращает новый <see cref="T:System.Security.Claims.ClaimsIdentity" /> копируются из этого утверждений удостоверений.
    /// </summary>
    /// <returns>Копия текущего экземпляра.</returns>
    public virtual ClaimsIdentity Clone()
    {
      ClaimsIdentity claimsIdentity = new ClaimsIdentity((IEnumerable<Claim>) this.m_instanceClaims);
      claimsIdentity.m_authenticationType = this.m_authenticationType;
      claimsIdentity.m_bootstrapContext = this.m_bootstrapContext;
      claimsIdentity.m_label = this.m_label;
      claimsIdentity.m_nameType = this.m_nameType;
      claimsIdentity.m_roleType = this.m_roleType;
      if (this.Actor != null)
      {
        if (this.IsCircular(this.Actor))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
        claimsIdentity.Actor = AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity ? this.Actor : this.Actor.Clone();
      }
      return claimsIdentity;
    }

    /// <summary>
    ///   Добавляет одно утверждение этого утверждений удостоверений.
    /// </summary>
    /// <param name="claim">Чтобы добавить утверждение.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="claim" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual void AddClaim(Claim claim)
    {
      if (claim == null)
        throw new ArgumentNullException(nameof (claim));
      if (claim.Subject == this)
        this.m_instanceClaims.Add(claim);
      else
        this.m_instanceClaims.Add(claim.Clone(this));
    }

    /// <summary>
    ///   Добавляет список утверждений, это удостоверение на основе утверждений.
    /// </summary>
    /// <param name="claims">Чтобы добавить утверждения.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="claims" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual void AddClaims(IEnumerable<Claim> claims)
    {
      if (claims == null)
        throw new ArgumentNullException(nameof (claims));
      foreach (Claim claim in claims)
      {
        if (claim != null)
          this.AddClaim(claim);
      }
    }

    /// <summary>
    ///   Пытается удалить утверждение из удостоверения утверждений.
    /// </summary>
    /// <param name="claim">Чтобы удалить утверждение.</param>
    /// <returns>
    ///   <see langword="true" /> Если утверждение был успешно удален; в противном случае — <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    public virtual bool TryRemoveClaim(Claim claim)
    {
      bool flag = false;
      for (int index = 0; index < this.m_instanceClaims.Count; ++index)
      {
        if (this.m_instanceClaims[index] == claim)
        {
          this.m_instanceClaims.RemoveAt(index);
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>
    ///   Пытается удалить утверждение из удостоверения утверждений.
    /// </summary>
    /// <param name="claim">Чтобы удалить утверждение.</param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Невозможно удалить утверждение.
    /// </exception>
    [SecurityCritical]
    public virtual void RemoveClaim(Claim claim)
    {
      if (!this.TryRemoveClaim(claim))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ClaimCannotBeRemoved", (object) claim));
    }

    [SecuritySafeCritical]
    private void SafeAddClaims(IEnumerable<Claim> claims)
    {
      foreach (Claim claim in claims)
      {
        if (claim.Subject == this)
          this.m_instanceClaims.Add(claim);
        else
          this.m_instanceClaims.Add(claim.Clone(this));
      }
    }

    [SecuritySafeCritical]
    private void SafeAddClaim(Claim claim)
    {
      if (claim.Subject == this)
        this.m_instanceClaims.Add(claim);
      else
        this.m_instanceClaims.Add(claim.Clone(this));
    }

    /// <summary>
    ///   Извлекает все утверждения, сопоставленные указанным предикатом.
    /// </summary>
    /// <param name="match">
    ///   Функция, выполняющая логику сопоставления.
    /// </param>
    /// <returns>
    ///   Утверждения сопоставления.
    ///    Список доступен только для чтения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="match" /> имеет значение NULL.
    /// </exception>
    public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
    {
      if (match == null)
        throw new ArgumentNullException(nameof (match));
      List<Claim> claimList = new List<Claim>();
      foreach (Claim claim in this.Claims)
      {
        if (match(claim))
          claimList.Add(claim);
      }
      return (IEnumerable<Claim>) claimList.AsReadOnly();
    }

    /// <summary>
    ///   Извлекает все утверждения, которые имеют заданный тип утверждения.
    /// </summary>
    /// <param name="type">
    ///   Тип утверждения, с которым будут сопоставляться утверждения.
    /// </param>
    /// <returns>
    ///   Утверждения сопоставления.
    ///    Список доступен только для чтения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="type" /> имеет значение NULL.
    /// </exception>
    public virtual IEnumerable<Claim> FindAll(string type)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      List<Claim> claimList = new List<Claim>();
      foreach (Claim claim in this.Claims)
      {
        if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
          claimList.Add(claim);
      }
      return (IEnumerable<Claim>) claimList.AsReadOnly();
    }

    /// <summary>
    ///   Определяет, является ли это утверждений удостоверений имеется утверждение, соответствует заданному предикату.
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
      foreach (Claim claim in this.Claims)
      {
        if (match(claim))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Определяет, является ли это утверждений удостоверений имеется утверждение с указанным утверждения типа и значения.
    /// </summary>
    /// <param name="type">Тип утверждения для сопоставления.</param>
    /// <param name="value">
    ///   Значение утверждения для сопоставления.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если совпадение найдено; в противном случае — <see langword="false" />.
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
      foreach (Claim claim in this.Claims)
      {
        if (claim != null && claim != null && (string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase) && string.Equals(claim.Value, value, StringComparison.Ordinal)))
          return true;
      }
      return false;
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
      foreach (Claim claim in this.Claims)
      {
        if (match(claim))
          return claim;
      }
      return (Claim) null;
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
      foreach (Claim claim in this.Claims)
      {
        if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
          return claim;
      }
      return (Claim) null;
    }

    [OnSerializing]
    [SecurityCritical]
    private void OnSerializingMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      this.m_serializedClaims = this.SerializeClaims();
      this.m_serializedNameType = this.m_nameType;
      this.m_serializedRoleType = this.m_roleType;
    }

    [OnDeserialized]
    [SecurityCritical]
    private void OnDeserializedMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      if (!string.IsNullOrEmpty(this.m_serializedClaims))
      {
        this.DeserializeClaims(this.m_serializedClaims);
        this.m_serializedClaims = (string) null;
      }
      this.m_nameType = string.IsNullOrEmpty(this.m_serializedNameType) ? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" : this.m_serializedNameType;
      this.m_roleType = string.IsNullOrEmpty(this.m_serializedRoleType) ? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" : this.m_serializedRoleType;
    }

    [OnDeserializing]
    private void OnDeserializingMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      this.m_instanceClaims = new List<Claim>();
      this.m_externalClaims = new Collection<IEnumerable<Claim>>();
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> данными, необходимыми для сериализации текущего <see cref="T:System.Security.Claims.ClaimsIdentity" /> объекта.
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
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      info.AddValue("System.Security.ClaimsIdentity.version", (object) this.m_version);
      if (!string.IsNullOrEmpty(this.m_authenticationType))
        info.AddValue("System.Security.ClaimsIdentity.authenticationType", (object) this.m_authenticationType);
      info.AddValue("System.Security.ClaimsIdentity.nameClaimType", (object) this.m_nameType);
      info.AddValue("System.Security.ClaimsIdentity.roleClaimType", (object) this.m_roleType);
      if (!string.IsNullOrEmpty(this.m_label))
        info.AddValue("System.Security.ClaimsIdentity.label", (object) this.m_label);
      if (this.m_actor != null)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          binaryFormatter.Serialize((Stream) memoryStream, (object) this.m_actor, (Header[]) null, false);
          info.AddValue("System.Security.ClaimsIdentity.actor", (object) Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length));
        }
      }
      info.AddValue("System.Security.ClaimsIdentity.claims", (object) this.SerializeClaims());
      if (this.m_bootstrapContext == null)
        return;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        binaryFormatter.Serialize((Stream) memoryStream, this.m_bootstrapContext, (Header[]) null, false);
        info.AddValue("System.Security.ClaimsIdentity.bootstrapContext", (object) Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length));
      }
    }

    [SecurityCritical]
    private void DeserializeClaims(string serializedClaims)
    {
      if (!string.IsNullOrEmpty(serializedClaims))
      {
        using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(serializedClaims)))
        {
          this.m_instanceClaims = (List<Claim>) new BinaryFormatter().Deserialize((Stream) memoryStream, (HeaderHandler) null, false);
          for (int index = 0; index < this.m_instanceClaims.Count; ++index)
            this.m_instanceClaims[index].Subject = this;
        }
      }
      if (this.m_instanceClaims != null)
        return;
      this.m_instanceClaims = new List<Claim>();
    }

    [SecurityCritical]
    private string SerializeClaims()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, (object) this.m_instanceClaims, (Header[]) null, false);
        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length);
      }
    }

    private bool IsCircular(ClaimsIdentity subject)
    {
      if (this == subject)
        return true;
      for (ClaimsIdentity claimsIdentity = subject; claimsIdentity.Actor != null; claimsIdentity = claimsIdentity.Actor)
      {
        if (this == claimsIdentity.Actor)
          return true;
      }
      return false;
    }

    private void Initialize(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      ClaimsIdentity.SerializationMask serializationMask = (ClaimsIdentity.SerializationMask) reader.ReadInt32();
      if ((serializationMask & ClaimsIdentity.SerializationMask.AuthenticationType) == ClaimsIdentity.SerializationMask.AuthenticationType)
        this.m_authenticationType = reader.ReadString();
      if ((serializationMask & ClaimsIdentity.SerializationMask.BootstrapConext) == ClaimsIdentity.SerializationMask.BootstrapConext)
        this.m_bootstrapContext = (object) reader.ReadString();
      this.m_nameType = (serializationMask & ClaimsIdentity.SerializationMask.NameClaimType) != ClaimsIdentity.SerializationMask.NameClaimType ? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" : reader.ReadString();
      this.m_roleType = (serializationMask & ClaimsIdentity.SerializationMask.RoleClaimType) != ClaimsIdentity.SerializationMask.RoleClaimType ? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" : reader.ReadString();
      if ((serializationMask & ClaimsIdentity.SerializationMask.HasClaims) != ClaimsIdentity.SerializationMask.HasClaims)
        return;
      int num = reader.ReadInt32();
      for (int index = 0; index < num; ++index)
        this.m_instanceClaims.Add(new Claim(reader, this));
    }

    /// <summary />
    /// <param name="reader" />
    protected virtual Claim CreateClaim(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      return new Claim(reader, this);
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
      ClaimsIdentity.SerializationMask serializationMask = ClaimsIdentity.SerializationMask.None;
      if (this.m_authenticationType != null)
      {
        serializationMask |= ClaimsIdentity.SerializationMask.AuthenticationType;
        ++num;
      }
      if (this.m_bootstrapContext != null && this.m_bootstrapContext is string)
      {
        serializationMask |= ClaimsIdentity.SerializationMask.BootstrapConext;
        ++num;
      }
      if (!string.Equals(this.m_nameType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", StringComparison.Ordinal))
      {
        serializationMask |= ClaimsIdentity.SerializationMask.NameClaimType;
        ++num;
      }
      if (!string.Equals(this.m_roleType, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", StringComparison.Ordinal))
      {
        serializationMask |= ClaimsIdentity.SerializationMask.RoleClaimType;
        ++num;
      }
      if (!string.IsNullOrWhiteSpace(this.m_label))
      {
        serializationMask |= ClaimsIdentity.SerializationMask.HasLabel;
        ++num;
      }
      if (this.m_instanceClaims.Count > 0)
      {
        serializationMask |= ClaimsIdentity.SerializationMask.HasClaims;
        ++num;
      }
      if (this.m_actor != null)
      {
        serializationMask |= ClaimsIdentity.SerializationMask.Actor;
        ++num;
      }
      if (userData != null && userData.Length != 0)
      {
        ++num;
        serializationMask |= ClaimsIdentity.SerializationMask.UserData;
      }
      writer.Write((int) serializationMask);
      writer.Write(num);
      if ((serializationMask & ClaimsIdentity.SerializationMask.AuthenticationType) == ClaimsIdentity.SerializationMask.AuthenticationType)
        writer.Write(this.m_authenticationType);
      if ((serializationMask & ClaimsIdentity.SerializationMask.BootstrapConext) == ClaimsIdentity.SerializationMask.BootstrapConext)
        writer.Write(this.m_bootstrapContext as string);
      if ((serializationMask & ClaimsIdentity.SerializationMask.NameClaimType) == ClaimsIdentity.SerializationMask.NameClaimType)
        writer.Write(this.m_nameType);
      if ((serializationMask & ClaimsIdentity.SerializationMask.RoleClaimType) == ClaimsIdentity.SerializationMask.RoleClaimType)
        writer.Write(this.m_roleType);
      if ((serializationMask & ClaimsIdentity.SerializationMask.HasLabel) == ClaimsIdentity.SerializationMask.HasLabel)
        writer.Write(this.m_label);
      if ((serializationMask & ClaimsIdentity.SerializationMask.HasClaims) == ClaimsIdentity.SerializationMask.HasClaims)
      {
        writer.Write(this.m_instanceClaims.Count);
        foreach (Claim instanceClaim in this.m_instanceClaims)
          instanceClaim.WriteTo(writer);
      }
      if ((serializationMask & ClaimsIdentity.SerializationMask.Actor) == ClaimsIdentity.SerializationMask.Actor)
        this.m_actor.WriteTo(writer);
      if ((serializationMask & ClaimsIdentity.SerializationMask.UserData) == ClaimsIdentity.SerializationMask.UserData)
      {
        writer.Write(userData.Length);
        writer.Write(userData);
      }
      writer.Flush();
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
    private void Deserialize(SerializationInfo info, StreamingContext context, bool useContext)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      BinaryFormatter binaryFormatter = !useContext ? new BinaryFormatter() : new BinaryFormatter((ISurrogateSelector) null, context);
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        switch (enumerator.Name)
        {
          case "System.Security.ClaimsIdentity.actor":
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.actor"))))
            {
              this.m_actor = (ClaimsIdentity) binaryFormatter.Deserialize((Stream) memoryStream, (HeaderHandler) null, false);
              continue;
            }
          case "System.Security.ClaimsIdentity.authenticationType":
            this.m_authenticationType = info.GetString("System.Security.ClaimsIdentity.authenticationType");
            continue;
          case "System.Security.ClaimsIdentity.bootstrapContext":
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.bootstrapContext"))))
            {
              this.m_bootstrapContext = binaryFormatter.Deserialize((Stream) memoryStream, (HeaderHandler) null, false);
              continue;
            }
          case "System.Security.ClaimsIdentity.claims":
            this.DeserializeClaims(info.GetString("System.Security.ClaimsIdentity.claims"));
            continue;
          case "System.Security.ClaimsIdentity.label":
            this.m_label = info.GetString("System.Security.ClaimsIdentity.label");
            continue;
          case "System.Security.ClaimsIdentity.nameClaimType":
            this.m_nameType = info.GetString("System.Security.ClaimsIdentity.nameClaimType");
            continue;
          case "System.Security.ClaimsIdentity.roleClaimType":
            this.m_roleType = info.GetString("System.Security.ClaimsIdentity.roleClaimType");
            continue;
          case "System.Security.ClaimsIdentity.version":
            info.GetString("System.Security.ClaimsIdentity.version");
            continue;
          default:
            continue;
        }
      }
    }

    private enum SerializationMask
    {
      None = 0,
      AuthenticationType = 1,
      BootstrapConext = 2,
      NameClaimType = 4,
      RoleClaimType = 8,
      HasClaims = 16, // 0x00000010
      HasLabel = 32, // 0x00000020
      Actor = 64, // 0x00000040
      UserData = 128, // 0x00000080
    }
  }
}
