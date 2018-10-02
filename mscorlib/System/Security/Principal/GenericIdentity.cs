// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.GenericIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
  /// <summary>Представляет универсального пользователя.</summary>
  [ComVisible(true)]
  [Serializable]
  public class GenericIdentity : ClaimsIdentity
  {
    private string m_name;
    private string m_type;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Principal.GenericIdentity" /> класс, представляющий пользователя с указанным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя пользователя, от лица которого выполняется код.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public GenericIdentity(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      this.m_name = name;
      this.m_type = "";
      this.AddNameClaim();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Principal.GenericIdentity" /> класс, представляющий пользователя с указанным типом имени и проверки подлинности.
    /// </summary>
    /// <param name="name">
    ///   Имя пользователя, от лица которого выполняется код.
    /// </param>
    /// <param name="type">
    ///   Тип проверки подлинности, применяемой для идентификации пользователя.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public GenericIdentity(string name, string type)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      this.m_name = name;
      this.m_type = type;
      this.AddNameClaim();
    }

    private GenericIdentity()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.GenericIdentity" />, используя указанный объект <see cref="T:System.Security.Principal.GenericIdentity" />.
    /// </summary>
    /// <param name="identity">
    ///   Объект, из которого создается новый экземпляр <see cref="T:System.Security.Principal.GenericIdentity" />.
    /// </param>
    protected GenericIdentity(GenericIdentity identity)
      : base((ClaimsIdentity) identity)
    {
      this.m_name = identity.m_name;
      this.m_type = identity.m_type;
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Копия текущего экземпляра.</returns>
    public override ClaimsIdentity Clone()
    {
      return (ClaimsIdentity) new GenericIdentity(this);
    }

    /// <summary>
    ///   Возвращает все утверждения для пользователя, представленного этим универсальным идентификатором.
    /// </summary>
    /// <returns>
    ///   Коллекция утверждений для этого объекта <see cref="T:System.Security.Principal.GenericIdentity" />.
    /// </returns>
    public override IEnumerable<Claim> Claims
    {
      get
      {
        return base.Claims;
      }
    }

    /// <summary>Возвращает имя пользователя.</summary>
    /// <returns>Имя пользователя, от лица которого запущен код.</returns>
    public override string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>
    ///   Получает тип аутентификации, используемой для идентификации пользователя.
    /// </summary>
    /// <returns>
    ///   Тип проверки подлинности, применяемой для идентификации пользователя.
    /// </returns>
    public override string AuthenticationType
    {
      get
      {
        return this.m_type;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, прошел ли пользователь проверку подлинности.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если пользователь прошел проверку подлинности; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool IsAuthenticated
    {
      get
      {
        return !this.m_name.Equals("");
      }
    }

    [OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
      bool flag = false;
      using (IEnumerator<Claim> enumerator = base.Claims.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          Claim current = enumerator.Current;
          flag = true;
        }
      }
      if (flag)
        return;
      this.AddNameClaim();
    }

    [SecuritySafeCritical]
    private void AddNameClaim()
    {
      if (this.m_name == null)
        return;
      this.AddClaim(new Claim(this.NameClaimType, this.m_name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", (ClaimsIdentity) this));
    }
  }
}
