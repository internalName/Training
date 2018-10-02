// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IPrincipal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>Определяет основные возможности объекта участника.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IPrincipal
  {
    /// <summary>Возвращает удостоверение текущего участника.</summary>
    /// <returns>
    ///   <see cref="T:System.Security.Principal.IIdentity" /> Объект, связанный с текущим участником.
    /// </returns>
    [__DynamicallyInvokable]
    IIdentity Identity { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Определяет, относится ли текущий участник к указанной роли.
    /// </summary>
    /// <param name="role">
    ///   Имя роли, для которой требуется проверить членство.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий участник является членом указанной роли; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool IsInRole(string role);
  }
}
