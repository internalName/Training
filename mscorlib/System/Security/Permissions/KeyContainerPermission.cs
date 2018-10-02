// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Управляет возможностью доступа к контейнерам ключей.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class KeyContainerPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private KeyContainerPermissionFlags m_flags;
    private KeyContainerPermissionAccessEntryCollection m_accessEntries;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.KeyContainerPermission" /> состоянием разрешения: ограниченным или неограниченным.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="state" /> не является допустимым значением <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public KeyContainerPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_flags = KeyContainerPermissionFlags.AllFlags;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_flags = KeyContainerPermissionFlags.NoFlags;
      }
      this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.KeyContainerPermission" /> с заданным доступом.
    /// </summary>
    /// <param name="flags">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="flags" /> не является допустимым сочетанием значений <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" />.
    /// </exception>
    public KeyContainerPermission(KeyContainerPermissionFlags flags)
    {
      KeyContainerPermission.VerifyFlags(flags);
      this.m_flags = flags;
      this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.KeyContainerPermission" /> с указанным глобальным доступом и правами доступа к конкретному контейнеру ключа.
    /// </summary>
    /// <param name="flags">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" />.
    /// </param>
    /// <param name="accessList">
    ///   Массив объектов <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" />, определяющих права доступа к конкретному контейнеру ключа.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="flags" /> не является допустимым сочетанием значений <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="accessList" /> имеет значение <see langword="null" />.
    /// </exception>
    public KeyContainerPermission(KeyContainerPermissionFlags flags, KeyContainerPermissionAccessEntry[] accessList)
    {
      if (accessList == null)
        throw new ArgumentNullException(nameof (accessList));
      KeyContainerPermission.VerifyFlags(flags);
      this.m_flags = flags;
      this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
      for (int index = 0; index < accessList.Length; ++index)
        this.m_accessEntries.Add(accessList[index]);
    }

    /// <summary>
    ///   Возвращает флаги разрешения контейнера ключей, применимые для всех контейнеров ключей, связанных с разрешением.
    /// </summary>
    /// <returns>
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" />.
    /// </returns>
    public KeyContainerPermissionFlags Flags
    {
      get
      {
        return this.m_flags;
      }
    }

    /// <summary>
    ///   Возвращает коллекцию объектов <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" />, связанных с текущим разрешением.
    /// </summary>
    /// <returns>
    ///   Коллекция <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" />, содержащая объекты <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> для этого <see cref="T:System.Security.Permissions.KeyContainerPermission" />.
    /// </returns>
    public KeyContainerPermissionAccessEntryCollection AccessEntries
    {
      get
      {
        return this.m_accessEntries;
      }
    }

    /// <summary>
    ///   Определяет, является ли текущее разрешение неограниченным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является неограниченным. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      if (this.m_flags != KeyContainerPermissionFlags.AllFlags)
        return false;
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
      {
        if ((accessEntry.Flags & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.AllFlags)
          return false;
      }
      return true;
    }

    private bool IsEmpty()
    {
      if (this.Flags != KeyContainerPermissionFlags.NoFlags)
        return false;
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
      {
        if (accessEntry.Flags != KeyContainerPermissionFlags.NoFlags)
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Определяет, является ли текущее разрешение подмножеством указанного разрешения.
    /// </summary>
    /// <param name="target">
    ///   Разрешение на проверку наличия связи подмножеств.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является подмножеством указанного разрешения. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="target" /> не имеет значение <see langword="null" /> и не указывает разрешение того же типа, что и текущее разрешение.
    /// </exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.IsEmpty();
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      KeyContainerPermission target1 = (KeyContainerPermission) target;
      if ((this.m_flags & target1.m_flags) != this.m_flags)
        return false;
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
      {
        KeyContainerPermissionFlags applicableFlags = KeyContainerPermission.GetApplicableFlags(accessEntry, target1);
        if ((accessEntry.Flags & applicableFlags) != accessEntry.Flags)
          return false;
      }
      foreach (KeyContainerPermissionAccessEntry accessEntry in target1.AccessEntries)
      {
        KeyContainerPermissionFlags applicableFlags = KeyContainerPermission.GetApplicableFlags(accessEntry, this);
        if ((applicableFlags & accessEntry.Flags) != applicableFlags)
          return false;
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
    ///   <paramref name="target" /> не имеет значение <see langword="null" /> и не указывает разрешение того же типа, что и текущее разрешение.
    /// </exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      KeyContainerPermission target1 = (KeyContainerPermission) target;
      if (this.IsEmpty() || target1.IsEmpty())
        return (IPermission) null;
      KeyContainerPermission containerPermission = new KeyContainerPermission(target1.m_flags & this.m_flags);
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
        containerPermission.AddAccessEntryAndIntersect(accessEntry, target1);
      foreach (KeyContainerPermissionAccessEntry accessEntry in target1.AccessEntries)
        containerPermission.AddAccessEntryAndIntersect(accessEntry, this);
      if (!containerPermission.IsEmpty())
        return (IPermission) containerPermission;
      return (IPermission) null;
    }

    /// <summary>
    ///   Создает разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, которое требуется объединить с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой объединение текущего разрешения и указанного разрешения.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="target" /> не имеет значение <see langword="null" /> и не указывает разрешение того же типа, что и текущее разрешение.
    /// </exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      KeyContainerPermission target1 = (KeyContainerPermission) target;
      if (this.IsUnrestricted() || target1.IsUnrestricted())
        return (IPermission) new KeyContainerPermission(PermissionState.Unrestricted);
      KeyContainerPermission containerPermission = new KeyContainerPermission(this.m_flags | target1.m_flags);
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
        containerPermission.AddAccessEntryAndUnion(accessEntry, target1);
      foreach (KeyContainerPermissionAccessEntry accessEntry in target1.AccessEntries)
        containerPermission.AddAccessEntryAndUnion(accessEntry, this);
      if (!containerPermission.IsEmpty())
        return (IPermission) containerPermission;
      return (IPermission) null;
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      if (this.IsEmpty())
        return (IPermission) null;
      KeyContainerPermission containerPermission = new KeyContainerPermission(this.m_flags);
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
        containerPermission.AccessEntries.Add(accessEntry);
      return (IPermission) containerPermission;
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.SecurityElement" />, содержащий кодировку XML разрешения, включая сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.KeyContainerPermission");
      if (!this.IsUnrestricted())
      {
        permissionElement.AddAttribute("Flags", this.m_flags.ToString());
        if (this.AccessEntries.Count > 0)
        {
          SecurityElement child1 = new SecurityElement("AccessList");
          foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
          {
            SecurityElement child2 = new SecurityElement("AccessEntry");
            child2.AddAttribute("KeyStore", accessEntry.KeyStore);
            child2.AddAttribute("ProviderName", accessEntry.ProviderName);
            SecurityElement securityElement1 = child2;
            string name1 = "ProviderType";
            int num = accessEntry.ProviderType;
            string str1 = num.ToString((string) null, (IFormatProvider) null);
            securityElement1.AddAttribute(name1, str1);
            child2.AddAttribute("KeyContainerName", accessEntry.KeyContainerName);
            SecurityElement securityElement2 = child2;
            string name2 = "KeySpec";
            num = accessEntry.KeySpec;
            string str2 = num.ToString((string) null, (IFormatProvider) null);
            securityElement2.AddAttribute(name2, str2);
            child2.AddAttribute("Flags", accessEntry.Flags.ToString());
            child1.AddChild(child2);
          }
          permissionElement.AddChild(child1);
        }
      }
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
    }

    /// <summary>
    ///   Восстанавливает разрешение с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="securityElement">
    ///   Объект <see cref="T:System.Security.SecurityElement" />, содержащий кодировку XML, используемую для восстановления разрешения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="securityElement" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="securityElement" /> не является допустимым элементом разрешения.
    /// 
    ///   -или-
    /// 
    ///   Номер версии <paramref name="securityElement" /> не поддерживается.
    /// </exception>
    public override void FromXml(SecurityElement securityElement)
    {
      CodeAccessPermission.ValidateElement(securityElement, (IPermission) this);
      if (XMLUtil.IsUnrestricted(securityElement))
      {
        this.m_flags = KeyContainerPermissionFlags.AllFlags;
        this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
      }
      else
      {
        this.m_flags = KeyContainerPermissionFlags.NoFlags;
        string str = securityElement.Attribute("Flags");
        if (str != null)
        {
          KeyContainerPermissionFlags flags = (KeyContainerPermissionFlags) Enum.Parse(typeof (KeyContainerPermissionFlags), str);
          KeyContainerPermission.VerifyFlags(flags);
          this.m_flags = flags;
        }
        this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
        if (securityElement.InternalChildren == null || securityElement.InternalChildren.Count == 0)
          return;
        foreach (SecurityElement child in securityElement.Children)
        {
          if (child != null && string.Equals(child.Tag, "AccessList"))
            this.AddAccessEntries(child);
        }
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return KeyContainerPermission.GetTokenIndex();
    }

    private void AddAccessEntries(SecurityElement securityElement)
    {
      if (securityElement.InternalChildren == null || securityElement.InternalChildren.Count == 0)
        return;
      foreach (SecurityElement child in securityElement.Children)
      {
        if (child != null && string.Equals(child.Tag, "AccessEntry"))
        {
          int count = child.m_lAttributes.Count;
          string keyStore = (string) null;
          string providerName = (string) null;
          int providerType = -1;
          string keyContainerName = (string) null;
          int keySpec = -1;
          KeyContainerPermissionFlags flags = KeyContainerPermissionFlags.NoFlags;
          int index = 0;
          while (index < count)
          {
            string lAttribute1 = (string) child.m_lAttributes[index];
            string lAttribute2 = (string) child.m_lAttributes[index + 1];
            if (string.Equals(lAttribute1, "KeyStore"))
              keyStore = lAttribute2;
            if (string.Equals(lAttribute1, "ProviderName"))
              providerName = lAttribute2;
            else if (string.Equals(lAttribute1, "ProviderType"))
              providerType = Convert.ToInt32(lAttribute2, (IFormatProvider) null);
            else if (string.Equals(lAttribute1, "KeyContainerName"))
              keyContainerName = lAttribute2;
            else if (string.Equals(lAttribute1, "KeySpec"))
              keySpec = Convert.ToInt32(lAttribute2, (IFormatProvider) null);
            else if (string.Equals(lAttribute1, "Flags"))
              flags = (KeyContainerPermissionFlags) Enum.Parse(typeof (KeyContainerPermissionFlags), lAttribute2);
            index += 2;
          }
          this.AccessEntries.Add(new KeyContainerPermissionAccessEntry(keyStore, providerName, providerType, keyContainerName, keySpec, flags));
        }
      }
    }

    private void AddAccessEntryAndUnion(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
    {
      KeyContainerPermissionAccessEntry accessEntry1 = new KeyContainerPermissionAccessEntry(accessEntry);
      accessEntry1.Flags |= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
      this.AccessEntries.Add(accessEntry1);
    }

    private void AddAccessEntryAndIntersect(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
    {
      KeyContainerPermissionAccessEntry accessEntry1 = new KeyContainerPermissionAccessEntry(accessEntry);
      accessEntry1.Flags &= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
      this.AccessEntries.Add(accessEntry1);
    }

    internal static void VerifyFlags(KeyContainerPermissionFlags flags)
    {
      if ((flags & ~KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.NoFlags)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) flags));
    }

    private static KeyContainerPermissionFlags GetApplicableFlags(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
    {
      KeyContainerPermissionFlags containerPermissionFlags = KeyContainerPermissionFlags.NoFlags;
      bool flag = true;
      int index = target.AccessEntries.IndexOf(accessEntry);
      if (index != -1)
        return target.AccessEntries[index].Flags;
      foreach (KeyContainerPermissionAccessEntry accessEntry1 in target.AccessEntries)
      {
        if (accessEntry.IsSubsetOf(accessEntry1))
        {
          if (!flag)
          {
            containerPermissionFlags &= accessEntry1.Flags;
          }
          else
          {
            containerPermissionFlags = accessEntry1.Flags;
            flag = false;
          }
        }
      }
      if (flag)
        containerPermissionFlags = target.Flags;
      return containerPermissionFlags;
    }

    private static int GetTokenIndex()
    {
      return 16;
    }
  }
}
