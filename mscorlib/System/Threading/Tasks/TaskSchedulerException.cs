// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskSchedulerException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Представляет исключение, используемое для передачи недопустимой операции, <see cref="T:System.Threading.Tasks.TaskScheduler" />.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public class TaskSchedulerException : Exception
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> системным сообщением, содержащим описание ошибки.
    /// </summary>
    [__DynamicallyInvokable]
    public TaskSchedulerException()
      : base(Environment.GetResourceString("TaskSchedulerException_ctor_DefaultMessage"))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> с использованием заданного сообщения, содержащего описание ошибки.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public TaskSchedulerException(string message)
      : base(message)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> класса по умолчанию сообщение об ошибке и ссылкой на внутреннее исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    /// </param>
    [__DynamicallyInvokable]
    public TaskSchedulerException(Exception innerException)
      : base(Environment.GetResourceString("TaskSchedulerException_ctor_DefaultMessage"), innerException)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public TaskSchedulerException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected TaskSchedulerException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
