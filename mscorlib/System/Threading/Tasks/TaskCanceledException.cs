// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskCanceledException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Представляет исключение, используемое для передачи отмены задачи.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public class TaskCanceledException : OperationCanceledException
  {
    [NonSerialized]
    private Task m_canceledTask;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Tasks.TaskCanceledException" /> системным сообщением, содержащим описание ошибки.
    /// </summary>
    [__DynamicallyInvokable]
    public TaskCanceledException()
      : base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Tasks.TaskCanceledException" /> с использованием заданного сообщения, содержащего описание ошибки.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public TaskCanceledException(string message)
      : base(message)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Tasks.TaskCanceledException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
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
    public TaskCanceledException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.Tasks.TaskCanceledException" /> класса со ссылкой на <see cref="T:System.Threading.Tasks.Task" /> была отменена.
    /// </summary>
    /// <param name="task">Задача, которая была отменена.</param>
    [__DynamicallyInvokable]
    public TaskCanceledException(Task task)
      : base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"), task != null ? task.CancellationToken : new CancellationToken())
    {
      this.m_canceledTask = task;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Tasks.TaskCanceledException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected TaskCanceledException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>Возвращает задачу, связанную с этим исключением.</summary>
    /// <returns>
    ///   Ссылку на <see cref="T:System.Threading.Tasks.Task" /> связанной с этим исключением.
    /// </returns>
    [__DynamicallyInvokable]
    public Task Task
    {
      [__DynamicallyInvokable] get
      {
        return this.m_canceledTask;
      }
    }
  }
}
