// Decompiled with JetBrains decompiler
// Type: System.Threading.SynchronizationLockException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>
  ///   Исключение, которое создается в то время, когда методу требуется вызвавший его объект для получения блокировки данного монитора, а метод вызван объектом, не являющимся владельцем блокировки.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class SynchronizationLockException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SynchronizationLockException" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public SynchronizationLockException()
      : base(Environment.GetResourceString("Arg_SynchronizationLockException"))
    {
      this.SetErrorCode(-2146233064);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SynchronizationLockException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public SynchronizationLockException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233064);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SynchronizationLockException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public SynchronizationLockException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233064);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.SynchronizationLockException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected SynchronizationLockException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
