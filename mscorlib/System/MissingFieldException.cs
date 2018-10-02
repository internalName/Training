// Decompiled with JetBrains decompiler
// Type: System.MissingFieldException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается при попытке динамического доступа к несуществующему полю.
  ///    Если поле в библиотеке классов было удалено или переименовано, перекомпилируйте все сборки, ссылающиеся на эту библиотеку.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MissingFieldException : MissingMemberException, ISerializable
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingFieldException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public MissingFieldException()
      : base(Environment.GetResourceString("Arg_MissingFieldException"))
    {
      this.SetErrorCode(-2146233071);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingFieldException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    /// </param>
    [__DynamicallyInvokable]
    public MissingFieldException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233071);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingFieldException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="inner" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public MissingFieldException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233071);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingFieldException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected MissingFieldException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Возвращает текстовую строку, содержащую подпись отсутствующего поля, имя класса и имя поля.
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
        return Environment.GetResourceString("MissingField_Name", (object) ((this.Signature != null ? MissingMemberException.FormatSignature(this.Signature) + " " : "") + this.ClassName + "." + this.MemberName));
      }
    }

    private MissingFieldException(string className, string fieldName, byte[] signature)
    {
      this.ClassName = className;
      this.MemberName = fieldName;
      this.Signature = signature;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.MissingFieldException" /> класса с заданным именем класса и имя поля.
    /// </summary>
    /// <param name="className">
    ///   Имя класса, в котором была произведена попытка доступа к несуществующему полю.
    /// </param>
    /// <param name="fieldName">
    ///   Имя поля, которое будет недоступна.
    /// </param>
    public MissingFieldException(string className, string fieldName)
    {
      this.ClassName = className;
      this.MemberName = fieldName;
    }
  }
}
