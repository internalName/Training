// Decompiled with JetBrains decompiler
// Type: System.Security.IStackWalk
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>
  ///   Управляет проверкой стека, которая определяет, имеют ли все вызывающие объекты в стеке вызовов разрешения, необходимые для доступа к защищенному ресурсу.
  /// </summary>
  [ComVisible(true)]
  public interface IStackWalk
  {
    /// <summary>
    ///   Подтверждает, что вызывающий код может получить доступ к ресурсу, определяемому текущим объектом разрешения, даже если вызывающим объектам выше в стеке вызовов не предоставлено разрешение на доступ к ресурсу.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего кода отсутствует <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Assertion" />.
    /// </exception>
    void Assert();

    /// <summary>
    ///   Определяет во время выполнения, было ли разрешение, указанное текущим объектом разрешений, предоставлено всем вызывающим методам в стеке вызовов.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий метод, расположенный выше в стеке вызовов, не имеет разрешения, указанного текущим объектом разрешений.
    /// 
    ///   -или-
    /// 
    ///   Вызывающий метод в стеке вызовов вызвал <see cref="M:System.Security.IStackWalk.Deny" /> в текущем объекте разрешений.
    /// </exception>
    void Demand();

    /// <summary>
    ///   Вызывает ошибку каждого <see cref="M:System.Security.IStackWalk.Demand" /> для текущего объекта, проходящего через вызывающий код.
    /// </summary>
    void Deny();

    /// <summary>
    ///   Вызывает сбой всех <see cref="M:System.Security.IStackWalk.Demand" /> для всех объектов, кроме текущего, проходящих через вызывающий код, даже если коду выше в стеке вызовов было предоставлено разрешение на доступ к другим ресурсам.
    /// </summary>
    void PermitOnly();
  }
}
