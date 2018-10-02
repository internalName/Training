// Decompiled with JetBrains decompiler
// Type: System.TimeZoneNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
  /// <summary>
  ///   Это исключение вызывается, когда не удается найти часовой пояс.
  /// </summary>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public class TimeZoneNotFoundException : Exception
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TimeZoneNotFoundException" /> указанной строкой сообщения.
    /// </summary>
    /// <param name="message">Строка с описанием исключения.</param>
    public TimeZoneNotFoundException(string message)
      : base(message)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TimeZoneNotFoundException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">Строка с описанием исключения.</param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    /// </param>
    public TimeZoneNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TimeZoneNotFoundException" /> из сериализованных данных.
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
    protected TimeZoneNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TimeZoneNotFoundException" />, используя системное сообщение.
    /// </summary>
    public TimeZoneNotFoundException()
    {
    }
  }
}
