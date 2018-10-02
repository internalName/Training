// Decompiled with JetBrains decompiler
// Type: System.OperationCanceledException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается в потоке при отмене операции, выполняемой этим потоком.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class OperationCanceledException : SystemException
  {
    [NonSerialized]
    private CancellationToken _cancellationToken;

    /// <summary>
    ///   Возвращает токен отмены, связанный с отмененной операцией.
    /// </summary>
    /// <returns>
    ///   Токен отмены, связанный с отмененной операцией, или токен по умолчанию.
    /// </returns>
    [__DynamicallyInvokable]
    public CancellationToken CancellationToken
    {
      [__DynamicallyInvokable] get
      {
        return this._cancellationToken;
      }
      private set
      {
        this._cancellationToken = value;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.OperationCanceledException" /> класса системное сообщение.
    /// </summary>
    [__DynamicallyInvokable]
    public OperationCanceledException()
      : base(Environment.GetResourceString("OperationCanceled"))
    {
      this.SetErrorCode(-2146233029);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.OperationCanceledException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    /// </param>
    [__DynamicallyInvokable]
    public OperationCanceledException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233029);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.OperationCanceledException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public OperationCanceledException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233029);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.OperationCanceledException" /> класса с токеном отмены.
    /// </summary>
    /// <param name="token">
    ///   Токен отмены, связанный с отмененной операцией.
    /// </param>
    [__DynamicallyInvokable]
    public OperationCanceledException(CancellationToken token)
      : this()
    {
      this.CancellationToken = token;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.OperationCanceledException" /> с указанным сообщением об ошибке и токен отмены.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="token">
    ///   Токен отмены, связанный с отмененной операцией.
    /// </param>
    [__DynamicallyInvokable]
    public OperationCanceledException(string message, CancellationToken token)
      : this(message)
    {
      this.CancellationToken = token;
    }

    /// <summary>
    ///   Инициализирует экземпляр класса <see cref="T:System.OperationCanceledException" /> указанным сообщением об ошибке, ссылкой на внутреннее исключение, вызвавшее данное исключение, и токеном отмены.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    /// <param name="token">
    ///   Токен отмены, связанный с отмененной операцией.
    /// </param>
    [__DynamicallyInvokable]
    public OperationCanceledException(string message, Exception innerException, CancellationToken token)
      : this(message, innerException)
    {
      this.CancellationToken = token;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.OperationCanceledException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected OperationCanceledException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
