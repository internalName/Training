// Decompiled with JetBrains decompiler
// Type: System.ApplicationIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Предоставляет возможность уникальной идентификации манифеста приложения.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(false)]
  [Serializable]
  public sealed class ApplicationIdentity : ISerializable
  {
    private IDefinitionAppId _appId;

    private ApplicationIdentity()
    {
    }

    [SecurityCritical]
    private ApplicationIdentity(SerializationInfo info, StreamingContext context)
    {
      string Identity = (string) info.GetValue(nameof (FullName), typeof (string));
      if (Identity == null)
        throw new ArgumentNullException("fullName");
      this._appId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, Identity);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ApplicationIdentity" />.
    /// </summary>
    /// <param name="applicationIdentityFullName">
    ///   Полное имя приложения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="applicationIdentityFullName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public ApplicationIdentity(string applicationIdentityFullName)
    {
      if (applicationIdentityFullName == null)
        throw new ArgumentNullException(nameof (applicationIdentityFullName));
      this._appId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, applicationIdentityFullName);
    }

    [SecurityCritical]
    internal ApplicationIdentity(IDefinitionAppId applicationIdentity)
    {
      this._appId = applicationIdentity;
    }

    /// <summary>Возвращает полное имя приложения.</summary>
    /// <returns>
    ///   Полное имя приложения, также известный как отображаемое имя.
    /// </returns>
    public string FullName
    {
      [SecuritySafeCritical] get
      {
        return IsolationInterop.AppIdAuthority.DefinitionToText(0U, this._appId);
      }
    }

    /// <summary>
    ///   Возвращает расположение манифеста развертывания как URL-адрес.
    /// </summary>
    /// <returns>URL-адрес манифеста развертывания.</returns>
    public string CodeBase
    {
      [SecuritySafeCritical] get
      {
        return this._appId.get_Codebase();
      }
    }

    /// <summary>Возвращает полное имя манифеста приложения.</summary>
    /// <returns>Полное имя манифеста приложения.</returns>
    public override string ToString()
    {
      return this.FullName;
    }

    internal IDefinitionAppId Identity
    {
      [SecurityCritical] get
      {
        return this._appId;
      }
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("FullName", (object) this.FullName, typeof (string));
    }
  }
}
