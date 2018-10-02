// Decompiled with JetBrains decompiler
// Type: System.AggregateException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет одну или несколько ошибок, возникающих во время выполнения приложения.
  /// </summary>
  [DebuggerDisplay("Count = {InnerExceptionCount}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class AggregateException : Exception
  {
    private ReadOnlyCollection<Exception> m_innerExceptions;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AggregateException" /> с системным сообщением, содержащим описание ошибки.
    /// </summary>
    [__DynamicallyInvokable]
    public AggregateException()
      : base(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"))
    {
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) new Exception[0]);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AggregateException" /> с использованием заданного сообщения, содержащего описание ошибки.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public AggregateException(string message)
      : base(message)
    {
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) new Exception[0]);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AggregateException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="innerException" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    public AggregateException(string message, Exception innerException)
      : base(message, innerException)
    {
      if (innerException == null)
        throw new ArgumentNullException(nameof (innerException));
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) new Exception[1]
      {
        innerException
      });
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.AggregateException" /> класса со ссылками на внутренние исключения, которые являются причиной данного исключения.
    /// </summary>
    /// <param name="innerExceptions">
    ///   Исключения, которые являются причиной текущего исключения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="innerExceptions" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="innerExceptions" /> имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public AggregateException(IEnumerable<Exception> innerExceptions)
      : this(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"), innerExceptions)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.AggregateException" /> класса со ссылками на внутренние исключения, которые являются причиной данного исключения.
    /// </summary>
    /// <param name="innerExceptions">
    ///   Исключения, которые являются причиной текущего исключения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="innerExceptions" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="innerExceptions" /> имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public AggregateException(params Exception[] innerExceptions)
      : this(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"), innerExceptions)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.AggregateException" /> класс с указанным сообщением об ошибке и ссылки на внутренние исключения, которые являются причиной данного исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerExceptions">
    ///   Исключения, которые являются причиной текущего исключения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="innerExceptions" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="innerExceptions" /> имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public AggregateException(string message, IEnumerable<Exception> innerExceptions)
      : this(message, innerExceptions as IList<Exception> ?? (innerExceptions == null ? (IList<Exception>) null : (IList<Exception>) new List<Exception>(innerExceptions)))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.AggregateException" /> класс с указанным сообщением об ошибке и ссылки на внутренние исключения, которые являются причиной данного исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerExceptions">
    ///   Исключения, которые являются причиной текущего исключения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="innerExceptions" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="innerExceptions" /> имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public AggregateException(string message, params Exception[] innerExceptions)
      : this(message, (IList<Exception>) innerExceptions)
    {
    }

    private AggregateException(string message, IList<Exception> innerExceptions)
      : base(message, innerExceptions == null || innerExceptions.Count <= 0 ? (Exception) null : innerExceptions[0])
    {
      if (innerExceptions == null)
        throw new ArgumentNullException(nameof (innerExceptions));
      Exception[] exceptionArray = new Exception[innerExceptions.Count];
      for (int index = 0; index < exceptionArray.Length; ++index)
      {
        exceptionArray[index] = innerExceptions[index];
        if (exceptionArray[index] == null)
          throw new ArgumentException(Environment.GetResourceString("AggregateException_ctor_InnerExceptionNull"));
      }
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) exceptionArray);
    }

    internal AggregateException(IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
      : this(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"), innerExceptionInfos)
    {
    }

    internal AggregateException(string message, IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
      : this(message, innerExceptionInfos as IList<ExceptionDispatchInfo> ?? (innerExceptionInfos == null ? (IList<ExceptionDispatchInfo>) null : (IList<ExceptionDispatchInfo>) new List<ExceptionDispatchInfo>(innerExceptionInfos)))
    {
    }

    private AggregateException(string message, IList<ExceptionDispatchInfo> innerExceptionInfos)
      : base(message, innerExceptionInfos == null || innerExceptionInfos.Count <= 0 || innerExceptionInfos[0] == null ? (Exception) null : innerExceptionInfos[0].SourceException)
    {
      if (innerExceptionInfos == null)
        throw new ArgumentNullException(nameof (innerExceptionInfos));
      Exception[] exceptionArray = new Exception[innerExceptionInfos.Count];
      for (int index = 0; index < exceptionArray.Length; ++index)
      {
        ExceptionDispatchInfo innerExceptionInfo = innerExceptionInfos[index];
        if (innerExceptionInfo != null)
          exceptionArray[index] = innerExceptionInfo.SourceException;
        if (exceptionArray[index] == null)
          throw new ArgumentException(Environment.GetResourceString("AggregateException_ctor_InnerExceptionNull"));
      }
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) exceptionArray);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AggregateException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="info" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Не удалось правильно выполнить десериализацию исключения.
    /// </exception>
    [SecurityCritical]
    protected AggregateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      Exception[] exceptionArray = info.GetValue(nameof (InnerExceptions), typeof (Exception[])) as Exception[];
      if (exceptionArray == null)
        throw new SerializationException(Environment.GetResourceString("AggregateException_DeserializationFailure"));
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) exceptionArray);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AggregateException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="info" /> Аргумент имеет значение null.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      Exception[] array = new Exception[this.m_innerExceptions.Count];
      this.m_innerExceptions.CopyTo(array, 0);
      info.AddValue("InnerExceptions", (object) array, typeof (Exception[]));
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.AggregateException" /> то есть корневой причиной данного исключения.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.AggregateException" /> то есть корневой причиной данного исключения.
    /// </returns>
    [__DynamicallyInvokable]
    public override Exception GetBaseException()
    {
      Exception exception = (Exception) this;
      for (AggregateException aggregateException = this; aggregateException != null && aggregateException.InnerExceptions.Count == 1; aggregateException = exception as AggregateException)
        exception = exception.InnerException;
      return exception;
    }

    /// <summary>
    ///   Возвращает коллекцию только для чтения <see cref="T:System.Exception" /> экземпляры, вызвавшее текущее исключение.
    /// </summary>
    /// <returns>
    ///   Возвращает коллекцию только для чтения <see cref="T:System.Exception" /> экземпляры, вызвавшее текущее исключение.
    /// </returns>
    [__DynamicallyInvokable]
    public ReadOnlyCollection<Exception> InnerExceptions
    {
      [__DynamicallyInvokable] get
      {
        return this.m_innerExceptions;
      }
    }

    /// <summary>
    ///   Вызывает обработчик в каждом <see cref="T:System.Exception" />, содержащемся в этом объекте <see cref="T:System.AggregateException" />.
    /// </summary>
    /// <param name="predicate">
    ///   Предикат, который необходимо выполнить для каждого исключения.
    ///    Предикат принимает в качестве аргумента <see cref="T:System.Exception" /> для обработки и возвращает логическое значение для указания, было ли это исключение обработано.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="predicate" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.AggregateException">
    ///   Исключение, содержащихся в этом объекте <see cref="T:System.AggregateException" />, не было обработано.
    /// </exception>
    [__DynamicallyInvokable]
    public void Handle(Func<Exception, bool> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException(nameof (predicate));
      List<Exception> exceptionList = (List<Exception>) null;
      for (int index = 0; index < this.m_innerExceptions.Count; ++index)
      {
        if (!predicate(this.m_innerExceptions[index]))
        {
          if (exceptionList == null)
            exceptionList = new List<Exception>();
          exceptionList.Add(this.m_innerExceptions[index]);
        }
      }
      if (exceptionList != null)
        throw new AggregateException(this.Message, (IList<Exception>) exceptionList);
    }

    /// <summary>
    ///   Выполняет сведение экземпляров <see cref="T:System.AggregateException" /> в один новый экземпляр.
    /// </summary>
    /// <returns>
    ///   Новый сведенный экземпляр <see cref="T:System.AggregateException" />.
    /// </returns>
    [__DynamicallyInvokable]
    public AggregateException Flatten()
    {
      List<Exception> exceptionList = new List<Exception>();
      List<AggregateException> aggregateExceptionList = new List<AggregateException>();
      aggregateExceptionList.Add(this);
      int num = 0;
      while (aggregateExceptionList.Count > num)
      {
        IList<Exception> innerExceptions = (IList<Exception>) aggregateExceptionList[num++].InnerExceptions;
        for (int index = 0; index < innerExceptions.Count; ++index)
        {
          Exception exception = innerExceptions[index];
          if (exception != null)
          {
            AggregateException aggregateException = exception as AggregateException;
            if (aggregateException != null)
              aggregateExceptionList.Add(aggregateException);
            else
              exceptionList.Add(exception);
          }
        }
      }
      return new AggregateException(this.Message, (IList<Exception>) exceptionList);
    }

    /// <summary>
    ///   Создает и возвращает строковое представление текущего объекта <see cref="T:System.AggregateException" />.
    /// </summary>
    /// <returns>Строковое представление текущего исключения.</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      string str = base.ToString();
      for (int index = 0; index < this.m_innerExceptions.Count; ++index)
        str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("AggregateException_ToString"), (object) str, (object) Environment.NewLine, (object) index, (object) this.m_innerExceptions[index].ToString(), (object) "<---", (object) Environment.NewLine);
      return str;
    }

    private int InnerExceptionCount
    {
      get
      {
        return this.InnerExceptions.Count;
      }
    }
  }
}
