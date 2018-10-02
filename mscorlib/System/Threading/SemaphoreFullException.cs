// Decompiled with JetBrains decompiler
// Type: System.Threading.SemaphoreFullException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>
  ///   Это исключение создается при вызове метода <see cref="Overload:System.Threading.Semaphore.Release" /> семафора, счетчик которого уже имеет максимальное значение.
  /// </summary>
  [ComVisible(false)]
  [TypeForwardedFrom("System, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  [Serializable]
  public class SemaphoreFullException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SemaphoreFullException" /> со значениями по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public SemaphoreFullException()
      : base(Environment.GetResourceString("Threading_SemaphoreFullException"))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SemaphoreFullException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public SemaphoreFullException(string message)
      : base(message)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SemaphoreFullException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public SemaphoreFullException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SemaphoreFullException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта по возникающему исключению.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected SemaphoreFullException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
