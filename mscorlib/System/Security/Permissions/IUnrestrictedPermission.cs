// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IUnrestrictedPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Позволяет разрешению предоставлять неограниченное состояние.
  /// </summary>
  [ComVisible(true)]
  public interface IUnrestrictedPermission
  {
    /// <summary>
    ///   Возвращает значение, указывающее, разрешен ли неограниченный доступ к ресурсу, защищенному с помощью разрешения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если разрешено неограниченное использование ресурса, защищенного разрешением. В противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsUnrestricted();
  }
}
