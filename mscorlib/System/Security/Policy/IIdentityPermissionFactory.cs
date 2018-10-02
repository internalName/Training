// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.IIdentityPermissionFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет метод, создающий новое разрешение идентификации.
  /// </summary>
  [ComVisible(true)]
  public interface IIdentityPermissionFactory
  {
    /// <summary>
    ///   Создает новое разрешение идентификации для указанного свидетельства.
    /// </summary>
    /// <param name="evidence">
    ///   Свидетельство, для которого создается новое разрешение идентификации.
    /// </param>
    /// <returns>Новое разрешение идентификации.</returns>
    IPermission CreateIdentityPermission(Evidence evidence);
  }
}
