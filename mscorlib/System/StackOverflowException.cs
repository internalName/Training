// Decompiled with JetBrains decompiler
// Type: System.StackOverflowException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выбрасывается при переполнении стека выполнения из-за чрезмерного количества вложенных вызовов метода.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class StackOverflowException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.StackOverflowException" />, устанавливая в качестве значения свойства нового экземпляра <see cref="P:System.Exception.Message" /> системное сообщение с описанием ошибки, например: "Запрашиваемая операция вызывает переполнение стека".
    ///    Это сообщение учитывает текущую культуру системы.
    /// </summary>
    public StackOverflowException()
      : base(Environment.GetResourceString("Arg_StackOverflowException"))
    {
      this.SetErrorCode(-2147023895);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.StackOverflowException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое параметра message должно быть понятным пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    public StackOverflowException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147023895);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.StackOverflowException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public StackOverflowException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147023895);
    }

    internal StackOverflowException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
