// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.StrongNameIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongNameIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_name;
    private string m_version;
    private string m_blob;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.StrongNameIdentityPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public StrongNameIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает имя идентификатора строгого имени.
    /// </summary>
    /// <returns>
    ///   Имя для сравнения с именем, указанным поставщиком безопасности.
    /// </returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        this.m_name = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает версию идентификатора строгого имени.
    /// </summary>
    /// <returns>Номер версии идентификатора строгого имени.</returns>
    public string Version
    {
      get
      {
        return this.m_version;
      }
      set
      {
        this.m_version = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение открытого ключа идентификатора строгого имени, выраженного в виде шестнадцатеричной строки.
    /// </summary>
    /// <returns>
    ///   Значение открытого ключа идентификатора строгого имени, выраженного в виде шестнадцатеричной строки.
    /// </returns>
    public string PublicKey
    {
      get
      {
        return this.m_blob;
      }
      set
      {
        this.m_blob = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> соответствует этому атрибуту.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Этот метод не выполнен, так как ключ <see langword="null" />.
    /// </exception>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new StrongNameIdentityPermission(PermissionState.Unrestricted);
      if (this.m_blob == null && this.m_name == null && this.m_version == null)
        return (IPermission) new StrongNameIdentityPermission(PermissionState.None);
      if (this.m_blob == null)
        throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Key"));
      StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob(this.m_blob);
      if (this.m_version == null || this.m_version.Equals(string.Empty))
        return (IPermission) new StrongNameIdentityPermission(blob, this.m_name, (System.Version) null);
      return (IPermission) new StrongNameIdentityPermission(blob, this.m_name, new System.Version(this.m_version));
    }
  }
}
