// Decompiled with JetBrains decompiler
// Type: System.ArgumentException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Это исключение выбрасывается, если один из передаваемых методу аргументов является недопустимым.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ArgumentException : SystemException, ISerializable
  {
    private string m_paramName;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ArgumentException()
      : base(Environment.GetResourceString("Arg_ArgumentException"))
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public ArgumentException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем NULL, текущее исключение возникло в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public ArgumentException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentException" /> с указанным сообщением об ошибке, именем параметра и ссылкой на внутреннее исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего текущее исключение.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем NULL, текущее исключение возникло в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public ArgumentException(string message, string paramName, Exception innerException)
      : base(message, innerException)
    {
      this.m_paramName = paramName;
      this.SetErrorCode(-2147024809);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentException" /> с указанным сообщением об ошибке и именем параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего текущее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public ArgumentException(string message, string paramName)
      : base(message)
    {
      this.m_paramName = paramName;
      this.SetErrorCode(-2147024809);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected ArgumentException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.m_paramName = info.GetString(nameof (ParamName));
    }

    /// <summary>
    ///   Возвращает сообщение об ошибке и имя параметра или только сообщение об ошибке, если не задан ни один параметр.
    /// </summary>
    /// <returns>
    /// Текстовая строка с подробным описанием исключения.
    ///  Значение этого свойства может принимать одну из следующих форм:
    /// 
    ///         Условие
    /// 
    ///         Значение
    /// 
    ///         <paramref name="paramName" /> Является пустой ссылкой (<see langword="Nothing" /> в Visual Basic) или имеет нулевую длину.
    /// 
    ///         <paramref name="message" /> Строка, передаваемая в конструктор.
    /// 
    ///         <paramref name="paramName" /> Не является пустой ссылкой (<see langword="Nothing" /> в Visual Basic) и имеет длину больше нуля.
    /// 
    ///         <paramref name="message" /> Строки, добавленной с именем недопустимого параметра.
    ///       </returns>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        string message = base.Message;
        if (string.IsNullOrEmpty(this.m_paramName))
          return message;
        string resourceString = Environment.GetResourceString("Arg_ParamName_Name", (object) this.m_paramName);
        return message + Environment.NewLine + resourceString;
      }
    }

    /// <summary>
    ///   Возвращает имя параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <returns>Имя параметра.</returns>
    [__DynamicallyInvokable]
    public virtual string ParamName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_paramName;
      }
    }

    /// <summary>
    ///   Задает объекту <see cref="T:System.Runtime.Serialization.SerializationInfo" /> имя параметра и дополнительную информацию об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Объект <paramref name="info" /> является пустой ссылкой (<see langword="Nothing" /> в Visual Basic).
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      info.AddValue("ParamName", (object) this.m_paramName, typeof (string));
    }
  }
}
