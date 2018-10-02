// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SecurityAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Указывает базовый класс атрибута для декларативной безопасности, из которого <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> является производным.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public abstract class SecurityAttribute : Attribute
  {
    internal SecurityAction m_action;
    internal bool m_unrestricted;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Permissions.SecurityAttribute" /> с указанным <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    protected SecurityAttribute(SecurityAction action)
    {
      this.m_action = action;
    }

    /// <summary>
    ///   Возвращает или задает действие по обеспечению безопасности.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </returns>
    public SecurityAction Action
    {
      get
      {
        return this.m_action;
      }
      set
      {
        this.m_action = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли полное (неограниченное) разрешение доступа к ресурсу, защищенному атрибутом.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если полный доступ к защищенному ресурсу объявлено; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Unrestricted
    {
      get
      {
        return this.m_unrestricted;
      }
      set
      {
        this.m_unrestricted = value;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе создает объект разрешения, могут затем быть сериализован в двоичной форме и постоянно хранятся вместе с <see cref="T:System.Security.Permissions.SecurityAction" /> в метаданных сборки.
    /// </summary>
    /// <returns>Сериализуемый объект разрешения.</returns>
    public abstract IPermission CreatePermission();

    [SecurityCritical]
    internal static IntPtr FindSecurityAttributeTypeHandle(string typeName)
    {
      PermissionSet.s_fullTrust.Assert();
      Type type = Type.GetType(typeName, false, false);
      if (type == (Type) null)
        return IntPtr.Zero;
      return type.TypeHandle.Value;
    }
  }
}
