// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PublisherIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Представляет удостоверение издателя программного обеспечения.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PublisherIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    private bool m_unrestricted;
    private X509Certificate[] m_certs;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> указанным значением <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public PublisherIdentityPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_unrestricted = true;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_unrestricted = false;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> указанным сертификатом Authenticode X.509v3.
    /// </summary>
    /// <param name="certificate">
    ///   Сертификат X.509, представляющий удостоверение издателя программного обеспечения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="certificate" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="certificate" /> не является допустимым сертификатом.
    /// </exception>
    public PublisherIdentityPermission(X509Certificate certificate)
    {
      this.Certificate = certificate;
    }

    /// <summary>
    ///   Получает или задает сертификат Authenticode X.509v3, представляющий удостоверение издателя программного обеспечения.
    /// </summary>
    /// <returns>
    ///   Сертификат X.509, представляющий удостоверение издателя программного обеспечения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> не является допустимым сертификатом.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Нельзя задать свойство, поскольку удостоверение неоднозначно.
    /// </exception>
    public X509Certificate Certificate
    {
      set
      {
        PublisherIdentityPermission.CheckCertificate(value);
        this.m_unrestricted = false;
        this.m_certs = new X509Certificate[1];
        this.m_certs[0] = new X509Certificate(value);
      }
      get
      {
        if (this.m_certs == null || this.m_certs.Length < 1)
          return (X509Certificate) null;
        if (this.m_certs.Length > 1)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
        if (this.m_certs[0] == null)
          return (X509Certificate) null;
        return new X509Certificate(this.m_certs[0]);
      }
    }

    private static void CheckCertificate(X509Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException(nameof (certificate));
      if (certificate.GetRawCertData() == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_UninitializedCertificate"));
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      PublisherIdentityPermission identityPermission = new PublisherIdentityPermission(PermissionState.None);
      identityPermission.m_unrestricted = this.m_unrestricted;
      if (this.m_certs != null)
      {
        identityPermission.m_certs = new X509Certificate[this.m_certs.Length];
        for (int index = 0; index < this.m_certs.Length; ++index)
          identityPermission.m_certs[index] = this.m_certs[index] == null ? (X509Certificate) null : new X509Certificate(this.m_certs[index]);
      }
      return (IPermission) identityPermission;
    }

    /// <summary>
    ///   Определяет, является ли текущее разрешение подмножеством указанного разрешения.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, для которого требуется проверить отношение подмножества.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является подмножеством указанного разрешения. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return !this.m_unrestricted && (this.m_certs == null || this.m_certs.Length == 0);
      PublisherIdentityPermission identityPermission = target as PublisherIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (identityPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted)
        return false;
      if (this.m_certs != null)
      {
        foreach (X509Certificate cert1 in this.m_certs)
        {
          bool flag = false;
          if (identityPermission.m_certs != null)
          {
            foreach (X509Certificate cert2 in identityPermission.m_certs)
            {
              if (cert1.Equals(cert2))
              {
                flag = true;
                break;
              }
            }
          }
          if (!flag)
            return false;
        }
      }
      return true;
    }

    /// <summary>
    ///   Создает и возвращает разрешение, представляющее собой пересечение текущего и указанного разрешений.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, пересекающееся с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой пересечение текущего и указанного разрешений.
    ///    Это новое разрешение равно <see langword="null" />, если пересечение является пустым.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      PublisherIdentityPermission identityPermission = target as PublisherIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted && identityPermission.m_unrestricted)
        return (IPermission) new PublisherIdentityPermission(PermissionState.None)
        {
          m_unrestricted = true
        };
      if (this.m_unrestricted)
        return identityPermission.Copy();
      if (identityPermission.m_unrestricted)
        return this.Copy();
      if (this.m_certs == null || identityPermission.m_certs == null || (this.m_certs.Length == 0 || identityPermission.m_certs.Length == 0))
        return (IPermission) null;
      ArrayList arrayList = new ArrayList();
      foreach (X509Certificate cert1 in this.m_certs)
      {
        foreach (X509Certificate cert2 in identityPermission.m_certs)
        {
          if (cert1.Equals(cert2))
            arrayList.Add((object) new X509Certificate(cert1));
        }
      }
      if (arrayList.Count == 0)
        return (IPermission) null;
      return (IPermission) new PublisherIdentityPermission(PermissionState.None)
      {
        m_certs = (X509Certificate[]) arrayList.ToArray(typeof (X509Certificate))
      };
    }

    /// <summary>
    ///   Создает разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, которое требуется объединить с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// 
    ///   -или-
    /// 
    ///   Два разрешения не равны друг другу.
    /// </exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if ((this.m_certs == null || this.m_certs.Length == 0) && !this.m_unrestricted)
          return (IPermission) null;
        return this.Copy();
      }
      PublisherIdentityPermission identityPermission = target as PublisherIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted || identityPermission.m_unrestricted)
        return (IPermission) new PublisherIdentityPermission(PermissionState.None)
        {
          m_unrestricted = true
        };
      if (this.m_certs == null || this.m_certs.Length == 0)
      {
        if (identityPermission.m_certs == null || identityPermission.m_certs.Length == 0)
          return (IPermission) null;
        return identityPermission.Copy();
      }
      if (identityPermission.m_certs == null || identityPermission.m_certs.Length == 0)
        return this.Copy();
      ArrayList arrayList = new ArrayList();
      foreach (X509Certificate cert in this.m_certs)
        arrayList.Add((object) cert);
      foreach (X509Certificate cert in identityPermission.m_certs)
      {
        bool flag = false;
        foreach (X509Certificate other in arrayList)
        {
          if (cert.Equals(other))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          arrayList.Add((object) cert);
      }
      return (IPermission) new PublisherIdentityPermission(PermissionState.None)
      {
        m_certs = (X509Certificate[]) arrayList.ToArray(typeof (X509Certificate))
      };
    }

    /// <summary>
    ///   Восстанавливает разрешение с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="esd">
    ///   Кодировка XML, используемая для восстановления разрешения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="esd" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="esd" /> не является допустимым элементом разрешения.
    /// 
    ///   -или-
    /// 
    ///   Недопустимый номер версии параметра <paramref name="esd" />.
    /// </exception>
    public override void FromXml(SecurityElement esd)
    {
      this.m_unrestricted = false;
      this.m_certs = (X509Certificate[]) null;
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      string strA = esd.Attribute("Unrestricted");
      if (strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0)
      {
        this.m_unrestricted = true;
      }
      else
      {
        string hexString1 = esd.Attribute("X509v3Certificate");
        ArrayList arrayList = new ArrayList();
        if (hexString1 != null)
          arrayList.Add((object) new X509Certificate(Hex.DecodeHexString(hexString1)));
        ArrayList children = esd.Children;
        if (children != null)
        {
          foreach (SecurityElement securityElement in children)
          {
            string hexString2 = securityElement.Attribute("X509v3Certificate");
            if (hexString2 != null)
              arrayList.Add((object) new X509Certificate(Hex.DecodeHexString(hexString2)));
          }
        }
        if (arrayList.Count == 0)
          return;
        this.m_certs = (X509Certificate[]) arrayList.ToArray(typeof (X509Certificate));
      }
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.PublisherIdentityPermission");
      if (this.m_unrestricted)
        permissionElement.AddAttribute("Unrestricted", "true");
      else if (this.m_certs != null)
      {
        if (this.m_certs.Length == 1)
        {
          permissionElement.AddAttribute("X509v3Certificate", this.m_certs[0].GetRawCertDataString());
        }
        else
        {
          for (int index = 0; index < this.m_certs.Length; ++index)
          {
            SecurityElement child = new SecurityElement("Cert");
            child.AddAttribute("X509v3Certificate", this.m_certs[index].GetRawCertDataString());
            permissionElement.AddChild(child);
          }
        }
      }
      return permissionElement;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return PublisherIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 10;
    }
  }
}
