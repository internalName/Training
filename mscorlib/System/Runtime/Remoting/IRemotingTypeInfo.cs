// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IRemotingTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>Предоставляет сведения о типе объекта.</summary>
  [ComVisible(true)]
  public interface IRemotingTypeInfo
  {
    /// <summary>
    ///   Возвращает или задает полное имя типа объекта сервера в <see cref="T:System.Runtime.Remoting.ObjRef" />.
    /// </summary>
    /// <returns>
    ///   Полное имя типа объекта сервера в <see cref="T:System.Runtime.Remoting.ObjRef" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    string TypeName { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>
    ///   Проверяет ли прокси-сервер, представляющий тип указанного объекта может быть приведен к типу, представленному <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> интерфейса.
    /// </summary>
    /// <param name="fromType">
    ///   Тип, к которому осуществляется приведение.
    /// </param>
    /// <param name="o">
    ///   Объект, для которого необходимо проверить приведение.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если преобразование прошло успешно; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    bool CanCastTo(Type fromType, object o);
  }
}
