// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;

namespace System.Security
{
  /// <summary>
  ///   Предоставляет базовый класс для запросов состояния безопасности действия из <see cref="T:System.AppDomainManager" /> объекта.
  /// </summary>
  [SecurityCritical]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class SecurityState
  {
    /// <summary>
    ///   Возвращает значение, указывающее, является ли состояние для данной реализации <see cref="T:System.Security.SecurityState" /> класс доступен на текущем узле.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если состояние доступно; в противном случае — <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    public bool IsStateAvailable()
    {
      AppDomainManager appDomainManager = AppDomainManager.CurrentAppDomainManager;
      if (appDomainManager == null)
        return false;
      return appDomainManager.CheckSecuritySettings(this);
    }

    /// <summary>
    ///   При переопределении в производном классе проверяет, что состояние, которое представлено <see cref="T:System.Security.SecurityState" /> доступен на узле.
    /// </summary>
    public abstract void EnsureState();
  }
}
