// Decompiled with JetBrains decompiler
// Type: System.AccessViolationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее при попытке чтения или записи в защищенную область памяти.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class AccessViolationException : SystemException
  {
    private IntPtr _ip;
    private IntPtr _target;
    private int _accessType;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AccessViolationException" /> с системным сообщением, содержащим описание ошибки.
    /// </summary>
    public AccessViolationException()
      : base(Environment.GetResourceString("Arg_AccessViolationException"))
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AccessViolationException" /> с использованием заданного сообщения, содержащего описание ошибки.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    public AccessViolationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AccessViolationException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public AccessViolationException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AccessViolationException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Класс <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected AccessViolationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
