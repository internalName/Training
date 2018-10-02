// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMethodReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Определяет интерфейс возвращаемого сообщения о вызове метода.
  /// </summary>
  [ComVisible(true)]
  public interface IMethodReturnMessage : IMethodMessage, IMessage
  {
    /// <summary>
    ///   Возвращает число аргументов в вызове метода помечен как <see langword="ref" /> или <see langword="out" /> Параметры.
    /// </summary>
    /// <returns>
    ///   Число аргументов в вызове метода помечен как <see langword="ref" /> или <see langword="out" /> Параметры.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    int OutArgCount { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает имя указанного аргумента, помеченного как <see langword="ref" /> или <see langword="out" /> параметра.
    /// </summary>
    /// <param name="index">Номер запрашиваемого имени аргумента.</param>
    /// <returns>
    ///   Имя аргумента или <see langword="null" /> если текущий метод не реализован.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    string GetOutArgName(int index);

    /// <summary>
    ///   Возвращает указанный аргумент, отмеченный как <see langword="ref" /> или <see langword="out" /> параметра.
    /// </summary>
    /// <param name="argNum">Номер запрашиваемого аргумента.</param>
    /// <returns>
    ///   Указанный аргумент, помеченный как <see langword="ref" /> или <see langword="out" /> параметра.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    object GetOutArg(int argNum);

    /// <summary>
    ///   Возвращает указанный аргумент, отмеченный как <see langword="ref" /> или <see langword="out" /> параметра.
    /// </summary>
    /// <returns>
    ///   Указанный аргумент, помеченный как <see langword="ref" /> или <see langword="out" /> параметра.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    object[] OutArgs { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает исключение, инициированное во время вызова метода.
    /// </summary>
    /// <returns>
    ///   Объект исключения для вызова метода или <see langword="null" /> Если метод не выдал исключение.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    Exception Exception { [SecurityCritical] get; }

    /// <summary>Получает возвращаемое значение вызова метода.</summary>
    /// <returns>Возвращаемое значение вызова метода.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    object ReturnValue { [SecurityCritical] get; }
  }
}
