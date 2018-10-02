// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.InternalActivationContextHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal
{
  /// <summary>
  ///   Предоставляет доступ к данным из <see cref="T:System.ActivationContext" /> объекта.
  /// </summary>
  [ComVisible(false)]
  public static class InternalActivationContextHelper
  {
    /// <summary>
    ///   Возвращает содержимое приложения манифеста из <see cref="T:System.ActivationContext" /> объекта.
    /// </summary>
    /// <param name="appInfo">Объект, содержащий манифест.</param>
    /// <returns>
    ///   Манифест приложения, которое находится <see cref="T:System.ActivationContext" /> объекта.
    /// </returns>
    [SecuritySafeCritical]
    public static object GetActivationContextData(ActivationContext appInfo)
    {
      return (object) appInfo.ActivationContextData;
    }

    /// <summary>
    ///   Возвращает манифест последнего развертывания компонента <see cref="T:System.ActivationContext" /> объекта.
    /// </summary>
    /// <param name="appInfo">Объект, содержащий манифест.</param>
    /// <returns>
    ///   Манифест для последнего развертывания компонента <see cref="T:System.ActivationContext" /> объекта.
    /// </returns>
    [SecuritySafeCritical]
    public static object GetApplicationComponentManifest(ActivationContext appInfo)
    {
      return (object) appInfo.ApplicationComponentManifest;
    }

    /// <summary>
    ///   Возвращает манифест первым компонентом развертывания <see cref="T:System.ActivationContext" /> объекта.
    /// </summary>
    /// <param name="appInfo">Объект, содержащий манифест.</param>
    /// <returns>
    ///   Манифест компонента первого развертывания в <see cref="T:System.ActivationContext" /> объекта.
    /// </returns>
    [SecuritySafeCritical]
    public static object GetDeploymentComponentManifest(ActivationContext appInfo)
    {
      return (object) appInfo.DeploymentComponentManifest;
    }

    /// <summary>
    ///   Сообщает <see cref="T:System.ActivationContext" /> для получения готов к выполнению.
    /// </summary>
    /// <param name="appInfo">Объект для оповещения.</param>
    public static void PrepareForExecution(ActivationContext appInfo)
    {
      appInfo.PrepareForExecution();
    }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли первый раз это <see cref="T:System.ActivationContext" /> объекта будет выполнена.
    /// </summary>
    /// <param name="appInfo">Объект для проверки.</param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.ActivationContext" /> Указывает, его выполнение в первый раз, в противном случае — <see langword="false" />.
    /// </returns>
    public static bool IsFirstRun(ActivationContext appInfo)
    {
      return appInfo.LastApplicationStateResult == ActivationContext.ApplicationStateDisposition.RunningFirstTime;
    }

    /// <summary>
    ///   Возвращает массив байтов, содержащий необработанное содержимое манифеста приложения...
    /// </summary>
    /// <param name="appInfo">Для получения байтов из объекта.</param>
    /// <returns>
    ///   Массив, содержащий манифест приложения в виде необработанных данных.
    /// </returns>
    public static byte[] GetApplicationManifestBytes(ActivationContext appInfo)
    {
      if (appInfo == null)
        throw new ArgumentNullException(nameof (appInfo));
      return appInfo.GetApplicationManifestBytes();
    }

    /// <summary>
    ///   Возвращает массив байтов, содержащий необработанное содержимое манифеста развертывания.
    /// </summary>
    /// <param name="appInfo">Для получения байтов из объекта.</param>
    /// <returns>
    ///   Массив, содержащий манифест развертывания в виде необработанных данных.
    /// </returns>
    public static byte[] GetDeploymentManifestBytes(ActivationContext appInfo)
    {
      if (appInfo == null)
        throw new ArgumentNullException(nameof (appInfo));
      return appInfo.GetDeploymentManifestBytes();
    }
  }
}
