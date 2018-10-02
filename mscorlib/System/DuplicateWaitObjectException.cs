// Decompiled with JetBrains decompiler
// Type: System.DuplicateWaitObjectException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее, когда объект присутствует в массиве объектов синхронизации более одного раза.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class DuplicateWaitObjectException : ArgumentException
  {
    private static volatile string _duplicateWaitObjectMessage;

    private static string DuplicateWaitObjectMessage
    {
      get
      {
        if (DuplicateWaitObjectException._duplicateWaitObjectMessage == null)
          DuplicateWaitObjectException._duplicateWaitObjectMessage = Environment.GetResourceString("Arg_DuplicateWaitObjectException");
        return DuplicateWaitObjectException._duplicateWaitObjectMessage;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DuplicateWaitObjectException" />.
    /// </summary>
    public DuplicateWaitObjectException()
      : base(DuplicateWaitObjectException.DuplicateWaitObjectMessage)
    {
      this.SetErrorCode(-2146233047);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.DuplicateWaitObjectException" /> класс с именем параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <param name="parameterName">
    ///   Имя параметра, вызвавшего данное исключение.
    /// </param>
    public DuplicateWaitObjectException(string parameterName)
      : base(DuplicateWaitObjectException.DuplicateWaitObjectMessage, parameterName)
    {
      this.SetErrorCode(-2146233047);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DuplicateWaitObjectException" /> с указанным сообщением об ошибке и именем параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <param name="parameterName">
    ///   Имя параметра, вызвавшего данное исключение.
    /// </param>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public DuplicateWaitObjectException(string parameterName, string message)
      : base(message, parameterName)
    {
      this.SetErrorCode(-2146233047);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DuplicateWaitObjectException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public DuplicateWaitObjectException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233047);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DuplicateWaitObjectException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
