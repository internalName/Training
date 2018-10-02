// Decompiled with JetBrains decompiler
// Type: System.InvalidTimeZoneException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается в случае недопустимых данных часового пояса.
  /// </summary>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public class InvalidTimeZoneException : Exception
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidTimeZoneException" /> с указанной строкой сообщения.
    /// </summary>
    /// <param name="message">Строка с описанием исключения.</param>
    [__DynamicallyInvokable]
    public InvalidTimeZoneException(string message)
      : base(message)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidTimeZoneException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">Строка с описанием исключения.</param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    /// </param>
    [__DynamicallyInvokable]
    public InvalidTimeZoneException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidTimeZoneException" /> из сериализованных данных.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные.
    /// </param>
    /// <param name="context">
    ///   Поток, содержащий сериализованные данные.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="info" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="context" /> имеет значение <see langword="null" />.
    /// </exception>
    protected InvalidTimeZoneException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidTimeZoneException" /> системным сообщением.
    /// </summary>
    [__DynamicallyInvokable]
    public InvalidTimeZoneException()
    {
    }
  }
}
