// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMethodMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>Определяет интерфейс сообщения метода.</summary>
  [ComVisible(true)]
  public interface IMethodMessage : IMessage
  {
    /// <summary>
    ///   Возвращает URI определенного объекта, для которого предназначен вызов.
    /// </summary>
    /// <returns>
    ///   URI удаленного объекта, который содержит вызываемый метод.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    string Uri { [SecurityCritical] get; }

    /// <summary>Возвращает имя вызванного метода.</summary>
    /// <returns>Имя вызываемого метода.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    string MethodName { [SecurityCritical] get; }

    /// <summary>
    ///   Получает полное <see cref="T:System.Type" /> имя определенного объекта, для которого предназначен вызов.
    /// </summary>
    /// <returns>
    ///   Полный <see cref="T:System.Type" /> имя определенного объекта, для которого предназначен вызов.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    string TypeName { [SecurityCritical] get; }

    /// <summary>Возвращает объект, содержащий подпись метода.</summary>
    /// <returns>Объект, содержащий подпись метода.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    object MethodSignature { [SecurityCritical] get; }

    /// <summary>Возвращает число аргументов, передаваемых в метод.</summary>
    /// <returns>Число аргументов передано в метод.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    int ArgCount { [SecurityCritical] get; }

    /// <summary>Получает имя аргумента, переданного методу.</summary>
    /// <param name="index">Номер запрашиваемого аргумента.</param>
    /// <returns>
    ///   Имя заданного аргумента, переданное методу, или <see langword="null" /> если текущий метод не реализован.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    string GetArgName(int index);

    /// <summary>
    ///   Получает определенный аргумент как <see cref="T:System.Object" />.
    /// </summary>
    /// <param name="argNum">Номер запрашиваемого аргумента.</param>
    /// <returns>Аргумент, передаваемый в метод.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    object GetArg(int argNum);

    /// <summary>Возвращает массив аргументов, передаваемых в метод.</summary>
    /// <returns>
    ///   <see cref="T:System.Object" /> Массив, содержащий аргументы, передаваемые методу.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    object[] Args { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает значение, указывающее, имеет ли сообщение переменные аргументы.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если метод может принимать переменное число аргументов; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    bool HasVarArgs { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> для текущего вызова метода.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> для текущего вызова метода.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    LogicalCallContext LogicalCallContext { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.MethodBase" /> вызванного метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.MethodBase" /> Вызванного метода.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    MethodBase MethodBase { [SecurityCritical] get; }
  }
}
