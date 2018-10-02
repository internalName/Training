// Decompiled with JetBrains decompiler
// Type: System.MissingMethodException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается при попытке динамического доступа к несуществующему методу.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MissingMethodException : MissingMemberException, ISerializable
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingMethodException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public MissingMethodException()
      : base(Environment.GetResourceString("Arg_MissingMethodException"))
    {
      this.SetErrorCode(-2146233069);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingMethodException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    /// </param>
    [__DynamicallyInvokable]
    public MissingMethodException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233069);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingMethodException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="inner" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public MissingMethodException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233069);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingMethodException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected MissingMethodException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Возвращает текстовую строку, содержащую имя класса, имя метода и сигнатуру отсутствующего метода.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>Строка сообщения об ошибке.</returns>
    [__DynamicallyInvokable]
    public override string Message
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this.ClassName == null)
          return base.Message;
        return Environment.GetResourceString("MissingMethod_Name", (object) (this.ClassName + "." + this.MemberName + (this.Signature != null ? " " + MissingMemberException.FormatSignature(this.Signature) : "")));
      }
    }

    private MissingMethodException(string className, string methodName, byte[] signature)
    {
      this.ClassName = className;
      this.MemberName = methodName;
      this.Signature = signature;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.MissingMethodException" /> класса с заданным именем класса и имя метода.
    /// </summary>
    /// <param name="className">
    ///   Имя класса, в котором была произведена попытка доступа к несуществующему методу.
    /// </param>
    /// <param name="methodName">
    ///   Имя метода, который будет недоступна.
    /// </param>
    public MissingMethodException(string className, string methodName)
    {
      this.ClassName = className;
      this.MemberName = methodName;
    }
  }
}
