// Decompiled with JetBrains decompiler
// Type: System.NotSupportedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается, если вызываемый метод не поддерживается, или при попытке чтения, поиска или записи в поток, который не поддерживает вызванную функцию.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class NotSupportedException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.NotSupportedException" /> классе и задайте <see cref="P:System.Exception.Message" /> Свойства нового экземпляра системное сообщение, описывающее ошибку.
    ///    Это сообщение учитывает текущую культуру системы.
    /// </summary>
    [__DynamicallyInvokable]
    public NotSupportedException()
      : base(Environment.GetResourceString("Arg_NotSupportedException"))
    {
      this.SetErrorCode(-2146233067);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotSupportedException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public NotSupportedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233067);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotSupportedException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем NULL, текущее исключение возникло в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public NotSupportedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233067);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotSupportedException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected NotSupportedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
