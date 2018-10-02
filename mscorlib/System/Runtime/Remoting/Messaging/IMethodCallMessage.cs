// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMethodCallMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>Определяет интерфейс сообщения о вызове метода.</summary>
  [ComVisible(true)]
  public interface IMethodCallMessage : IMethodMessage, IMessage
  {
    /// <summary>
    ///   Возвращает число аргументов в вызове, которые не помечены как <see langword="out" /> параметров.
    /// </summary>
    /// <returns>
    ///   Число аргументов в вызове, которые не помечены как <see langword="out" /> параметров.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    int InArgCount { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает имя заданного аргумента, который не помечен как <see langword="out" /> параметр.
    /// </summary>
    /// <param name="index">
    ///   Номер запрашиваемого <see langword="in" /> аргумент.
    /// </param>
    /// <returns>
    ///   Имя заданного аргумента, который не помечен как <see langword="out" /> параметр.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    string GetInArgName(int index);

    /// <summary>
    ///   Возвращает заданный аргумент, который не помечен как <see langword="out" /> параметр.
    /// </summary>
    /// <param name="argNum">
    ///   Номер запрашиваемого <see langword="in" /> аргумент.
    /// </param>
    /// <returns>
    ///   Требуемый аргумент, который не помечен как <see langword="out" /> параметр.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    object GetInArg(int argNum);

    /// <summary>
    ///   Возвращает массив аргументов, которые не помечены как <see langword="out" /> параметров.
    /// </summary>
    /// <returns>
    ///   Массив аргументов, которые не помечены как <see langword="out" /> параметров.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    object[] InArgs { [SecurityCritical] get; }
  }
}
