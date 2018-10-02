// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PermissionRequestEvidence
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет свидетельство, представляющее запросы разрешений.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
  [Serializable]
  public sealed class PermissionRequestEvidence : EvidenceBase
  {
    private PermissionSet m_request;
    private PermissionSet m_optional;
    private PermissionSet m_denied;
    private string m_strRequest;
    private string m_strOptional;
    private string m_strDenied;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.PermissionRequestEvidence" /> класса с запросом разрешения кода сборки.
    /// </summary>
    /// <param name="request">
    ///   Минимальные разрешения, необходимые для запуска кода.
    /// </param>
    /// <param name="optional">
    ///   Разрешения, можно использовать код, если они предоставляются, но не требуются.
    /// </param>
    /// <param name="denied">
    ///   Разрешения, которые код явно запрашивает ему не предоставлять.
    /// </param>
    public PermissionRequestEvidence(PermissionSet request, PermissionSet optional, PermissionSet denied)
    {
      this.m_request = request != null ? request.Copy() : (PermissionSet) null;
      this.m_optional = optional != null ? optional.Copy() : (PermissionSet) null;
      if (denied == null)
        this.m_denied = (PermissionSet) null;
      else
        this.m_denied = denied.Copy();
    }

    /// <summary>
    ///   Возвращает минимальный набор разрешений, требуемый для запуска кода.
    /// </summary>
    /// <returns>
    ///   Минимальные разрешения, необходимые для запуска кода.
    /// </returns>
    public PermissionSet RequestedPermissions
    {
      get
      {
        return this.m_request;
      }
    }

    /// <summary>
    ///   Получает разрешения, которые код может использовать, если они предоставляются, но не являются обязательными.
    /// </summary>
    /// <returns>
    ///   Разрешения, можно использовать код, если они предоставляются, но не являются обязательными.
    /// </returns>
    public PermissionSet OptionalPermissions
    {
      get
      {
        return this.m_optional;
      }
    }

    /// <summary>
    ///   Получает разрешения, которые код явно запрашивает ему не предоставлять.
    /// </summary>
    /// <returns>
    ///   Разрешения, которые код явно запрашивает ему не предоставлять.
    /// </returns>
    public PermissionSet DeniedPermissions
    {
      get
      {
        return this.m_denied;
      }
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) this.Copy();
    }

    /// <summary>
    ///   Создает эквивалентную копию текущего объекта <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.
    /// </summary>
    /// <returns>
    ///   Эквивалентная копия текущего объекта <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.
    /// </returns>
    public PermissionRequestEvidence Copy()
    {
      return new PermissionRequestEvidence(this.m_request, this.m_optional, this.m_denied);
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.PermissionRequestEvidence");
      securityElement.AddAttribute("version", "1");
      if (this.m_request != null)
      {
        SecurityElement child = new SecurityElement("Request");
        child.AddChild(this.m_request.ToXml());
        securityElement.AddChild(child);
      }
      if (this.m_optional != null)
      {
        SecurityElement child = new SecurityElement("Optional");
        child.AddChild(this.m_optional.ToXml());
        securityElement.AddChild(child);
      }
      if (this.m_denied != null)
      {
        SecurityElement child = new SecurityElement("Denied");
        child.AddChild(this.m_denied.ToXml());
        securityElement.AddChild(child);
      }
      return securityElement;
    }

    /// <summary>
    ///   Возвращает строковое представление состояния <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.
    /// </summary>
    /// <returns>
    ///   Представление состояния <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.
    /// </returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }
  }
}
