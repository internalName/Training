// Decompiled with JetBrains decompiler
// Type: System.ArgumentOutOfRangeException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается, если значение аргумента не соответствует допустимому диапазону значений, установленному вызванным методом.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ArgumentOutOfRangeException : ArgumentException, ISerializable
  {
    private static volatile string _rangeMessage;
    private object m_actualValue;

    private static string RangeMessage
    {
      get
      {
        if (ArgumentOutOfRangeException._rangeMessage == null)
          ArgumentOutOfRangeException._rangeMessage = Environment.GetResourceString("Arg_ArgumentOutOfRangeException");
        return ArgumentOutOfRangeException._rangeMessage;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentOutOfRangeException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException()
      : base(ArgumentOutOfRangeException.RangeMessage)
    {
      this.SetErrorCode(-2146233086);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArgumentOutOfRangeException" /> класс с именем параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего данное исключение.
    /// </param>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException(string paramName)
      : base(ArgumentOutOfRangeException.RangeMessage, paramName)
    {
      this.SetErrorCode(-2146233086);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArgumentOutOfRangeException" /> класс с именем параметра, который вызывает это исключение и указанное сообщение об ошибке.
    /// </summary>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего данное исключение.
    /// </param>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException(string paramName, string message)
      : base(message, paramName)
    {
      this.SetErrorCode(-2146233086);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArgumentOutOfRangeException" /> класс с указанным сообщением об ошибке и исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке с объяснением причины исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, вызвавшее текущее исключение, или пустая ссылка (<see langword="Nothing" /> в Visual Basic), если внутреннее исключение не задано.
    /// </param>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233086);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArgumentOutOfRangeException" /> имя класса с параметром, значение аргумента и указанное сообщение об ошибке.
    /// </summary>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего данное исключение.
    /// </param>
    /// <param name="actualValue">
    ///   Значение аргумента, вызвавшего данное исключение.
    /// </param>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException(string paramName, object actualValue, string message)
      : base(message, paramName)
    {
      this.m_actualValue = actualValue;
      this.SetErrorCode(-2146233086);
    }

    /// <summary>
    ///   Возвращает сообщение об ошибке и строковое представление недопустимого значения аргумента или только сообщение об ошибке, если значение аргумента равно null.
    /// </summary>
    /// <returns>
    /// Текстовое сообщение для этого исключения.
    ///  Значение этого свойства принимает одну из двух форм, как показано ниже.
    /// 
    ///         Условие
    /// 
    ///         Значение
    /// 
    ///         Значение параметра <paramref name="actualValue" /> — <see langword="null" />.
    /// 
    ///         <paramref name="message" /> Строка, передаваемая в конструктор.
    /// 
    ///         Свойство <paramref name="actualValue" /> не является методом <see langword="null" />.
    /// 
    ///         <paramref name="message" /> Строка дополняется строковое представление недопустимого значения аргумента.
    ///       </returns>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        string message = base.Message;
        if (this.m_actualValue == null)
          return message;
        string resourceString = Environment.GetResourceString("ArgumentOutOfRange_ActualValue", (object) this.m_actualValue.ToString());
        if (message == null)
          return resourceString;
        return message + Environment.NewLine + resourceString;
      }
    }

    /// <summary>
    ///   Возвращает значение аргумента, вызвавшего данное исключение.
    /// </summary>
    /// <returns>
    ///   <see langword="Object" /> , Содержащий значение параметра, вызвавшего текущее <see cref="T:System.Exception" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual object ActualValue
    {
      [__DynamicallyInvokable] get
      {
        return this.m_actualValue;
      }
    }

    /// <summary>
    ///   Наборы <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект с недопустимым значением аргумента и дополнительными сведениями об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Объект, описывающий источник или цель сериализованных данных.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="info" /> Объект <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      info.AddValue("ActualValue", this.m_actualValue, typeof (object));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentOutOfRangeException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Объект, описывающий источник или цель сериализованных данных.
    /// </param>
    protected ArgumentOutOfRangeException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.m_actualValue = info.GetValue(nameof (ActualValue), typeof (object));
    }
  }
}
