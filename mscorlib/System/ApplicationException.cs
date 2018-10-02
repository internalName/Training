// Decompiled with JetBrains decompiler
// Type: System.ApplicationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Служит базовым классом для определяемых приложением исключений.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class ApplicationException : Exception
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException" />.
    /// </summary>
    public ApplicationException()
      : base(Environment.GetResourceString("Arg_ApplicationException"))
    {
      this.SetErrorCode(-2146232832);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public ApplicationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232832);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем NULL, текущее исключение возникло в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public ApplicationException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146232832);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected ApplicationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
