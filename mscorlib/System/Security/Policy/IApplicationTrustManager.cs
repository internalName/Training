// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.IApplicationTrustManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет, следует ли выполнять приложение и какой набор разрешений должен быть предоставлен к нему.
  /// </summary>
  [ComVisible(true)]
  public interface IApplicationTrustManager : ISecurityEncodable
  {
    /// <summary>
    ///   Определяет, следует ли выполнять приложение и какой набор разрешений должен быть предоставлен к нему.
    /// </summary>
    /// <param name="activationContext">
    ///   Контекст активации для приложения.
    /// </param>
    /// <param name="context">
    ///   Контекст диспетчер доверия для приложения.
    /// </param>
    /// <returns>
    ///   Объект, содержащий решений по безопасности о приложении.
    /// </returns>
    ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context);
  }
}
