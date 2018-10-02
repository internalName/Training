// Decompiled with JetBrains decompiler
// Type: System.Security.NamedPermissionSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Security
{
  /// <summary>
  ///   Определяет набор разрешений, который имеет имя и описание, связанное с ним.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class NamedPermissionSet : PermissionSet
  {
    private string m_name;
    private string m_description;
    [OptionalField(VersionAdded = 2)]
    internal string m_descrResource;
    private static object s_InternalSyncObject;

    internal NamedPermissionSet()
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Security.NamedPermissionSet" /> класса с заданным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя нового именованного набора разрешений.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> Параметр <see langword="null" /> или является пустой строкой («»).
    /// </exception>
    public NamedPermissionSet(string name)
    {
      NamedPermissionSet.CheckName(name);
      this.m_name = name;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.NamedPermissionSet" /> класса с заданным именем в неограниченный или полностью ограниченном состоянии.
    /// </summary>
    /// <param name="name">
    ///   Имя нового именованного набора разрешений.
    /// </param>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> Параметр <see langword="null" /> или является пустой строкой («»).
    /// </exception>
    public NamedPermissionSet(string name, PermissionState state)
      : base(state)
    {
      NamedPermissionSet.CheckName(name);
      this.m_name = name;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.NamedPermissionSet" /> класса с заданным именем из набора разрешений.
    /// </summary>
    /// <param name="name">Имя именованного набора разрешений.</param>
    /// <param name="permSet">
    ///   Разрешение, набор, из которого берется значение нового именованного набора разрешений.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> Параметр <see langword="null" /> или является пустой строкой («»).
    /// </exception>
    public NamedPermissionSet(string name, PermissionSet permSet)
      : base(permSet)
    {
      NamedPermissionSet.CheckName(name);
      this.m_name = name;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.NamedPermissionSet" /> класс из другой именованный набор разрешений.
    /// </summary>
    /// <param name="permSet">
    ///   Именованный набор разрешений из которого создается новый экземпляр.
    /// </param>
    public NamedPermissionSet(NamedPermissionSet permSet)
      : base((PermissionSet) permSet)
    {
      this.m_name = permSet.m_name;
      this.m_description = permSet.Description;
    }

    internal NamedPermissionSet(SecurityElement permissionSetXml)
      : base(PermissionState.None)
    {
      this.FromXml(permissionSetXml);
    }

    /// <summary>
    ///   Возвращает или задает имя текущего именованного набора разрешений.
    /// </summary>
    /// <returns>Имя именованного набора разрешений.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Имя — <see langword="null" /> или является пустой строкой («»).
    /// </exception>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        NamedPermissionSet.CheckName(value);
        this.m_name = value;
      }
    }

    private static void CheckName(string name)
    {
      if (name == null || name.Equals(""))
        throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInvalidName"));
    }

    /// <summary>
    ///   Возвращает или задает текстовое описание текущего именованного набора разрешений.
    /// </summary>
    /// <returns>Текстовое описание именованного набора разрешений.</returns>
    public string Description
    {
      get
      {
        if (this.m_descrResource != null)
        {
          this.m_description = Environment.GetResourceString(this.m_descrResource);
          this.m_descrResource = (string) null;
        }
        return this.m_description;
      }
      set
      {
        this.m_description = value;
        this.m_descrResource = (string) null;
      }
    }

    /// <summary>
    ///   Создает копию набора разрешений из именованного набора разрешений.
    /// </summary>
    /// <returns>
    ///   Набор разрешений, являющийся копией разрешений в именованный набор разрешений.
    /// </returns>
    public override PermissionSet Copy()
    {
      return (PermissionSet) new NamedPermissionSet(this);
    }

    /// <summary>
    ///   Создает копию именованного набора разрешений с другим именем, но те же разрешения.
    /// </summary>
    /// <param name="name">
    ///   Имя нового именованного набора разрешений.
    /// </param>
    /// <returns>
    ///   Копия именованного набора разрешений с новым именем.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> Параметр <see langword="null" /> или является пустой строкой («»).
    /// </exception>
    public NamedPermissionSet Copy(string name)
    {
      return new NamedPermissionSet(this) { Name = name };
    }

    /// <summary>
    ///   Создает элемент XML-описание именованного набора разрешений.
    /// </summary>
    /// <returns>XML-представление именованного набора разрешений.</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement xml = this.ToXml("System.Security.NamedPermissionSet");
      if (this.m_name != null && !this.m_name.Equals(""))
        xml.AddAttribute("Name", SecurityElement.Escape(this.m_name));
      if (this.Description != null && !this.Description.Equals(""))
        xml.AddAttribute("Description", SecurityElement.Escape(this.Description));
      return xml;
    }

    /// <summary>
    ///   Восстанавливает именованный набор разрешений с определенным состоянием из кодировки XML.
    /// </summary>
    /// <param name="et">
    ///   Элемент безопасности, содержащий XML-представление именованного набора разрешений.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="et" /> Параметр не является допустимым представлением именованного набора разрешений.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="et" /> имеет значение <see langword="null" />.
    /// </exception>
    public override void FromXml(SecurityElement et)
    {
      this.FromXml(et, false, false);
    }

    internal override void FromXml(SecurityElement et, bool allowInternalOnly, bool ignoreTypeLoadFailures)
    {
      if (et == null)
        throw new ArgumentNullException(nameof (et));
      string str1 = et.Attribute("Name");
      this.m_name = str1 == null ? (string) null : str1;
      string str2 = et.Attribute("Description");
      this.m_description = str2 == null ? "" : str2;
      this.m_descrResource = (string) null;
      base.FromXml(et, allowInternalOnly, ignoreTypeLoadFailures);
    }

    internal void FromXmlNameOnly(SecurityElement et)
    {
      string str = et.Attribute("Name");
      this.m_name = str == null ? (string) null : str;
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект <see cref="T:System.Security.NamedPermissionSet" /> текущему объекту <see cref="T:System.Security.NamedPermissionSet" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Security.NamedPermissionSet" />, который требуется сравнить с текущим объектом <see cref="T:System.Security.NamedPermissionSet" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект <see cref="T:System.Security.NamedPermissionSet" /> равен текущему объекту <see cref="T:System.Security.NamedPermissionSet" />; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    /// <summary>
    ///   Возвращает хэш-код для объекта <see cref="T:System.Security.NamedPermissionSet" />, который можно использовать в алгоритмах хэширования и структурах данных, например в хэш-таблице.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Security.NamedPermissionSet" />.
    /// </returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    private static object InternalSyncObject
    {
      get
      {
        if (NamedPermissionSet.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref NamedPermissionSet.s_InternalSyncObject, obj, (object) null);
        }
        return NamedPermissionSet.s_InternalSyncObject;
      }
    }
  }
}
