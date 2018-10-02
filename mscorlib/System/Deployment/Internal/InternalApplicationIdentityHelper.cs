// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.InternalApplicationIdentityHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal
{
  /// <summary>
  ///   Предоставляет доступ к свойствам внутренних <see cref="T:System.ApplicationIdentity" /> объекта.
  /// </summary>
  [ComVisible(false)]
  public static class InternalApplicationIdentityHelper
  {
    /// <summary>
    ///   Возвращает Интерфейс IDefinitionAppId представляющий уникальный идентификатор для <see cref="T:System.ApplicationIdentity" /> объекта.
    /// </summary>
    /// <param name="id">
    ///   Объект, из которого извлекается идентификатор.
    /// </param>
    /// <returns>
    ///   Уникальный идентификатор, удерживаемые <see cref="T:System.ApplicationIdentity" /> объекта.
    /// </returns>
    [SecurityCritical]
    public static object GetInternalAppId(ApplicationIdentity id)
    {
      return (object) id.Identity;
    }
  }
}
