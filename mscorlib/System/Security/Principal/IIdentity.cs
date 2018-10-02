// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>
  ///   Определяет основные возможности объекта удостоверения.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IIdentity
  {
    /// <summary>Получает имя текущего пользователя.</summary>
    /// <returns>Имя пользователя, от лица которого выполняется код.</returns>
    [__DynamicallyInvokable]
    string Name { [__DynamicallyInvokable] get; }

    /// <summary>Возвращает тип проверки подлинности.</summary>
    /// <returns>
    ///   Тип проверки подлинности, применяемой для идентификации пользователя.
    /// </returns>
    [__DynamicallyInvokable]
    string AuthenticationType { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает значение, указывающее, прошел ли пользователь проверку подлинности.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если пользователь прошел проверку подлинности; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool IsAuthenticated { [__DynamicallyInvokable] get; }
  }
}
