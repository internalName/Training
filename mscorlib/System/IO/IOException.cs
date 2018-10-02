// Decompiled with JetBrains decompiler
// Type: System.IO.IOException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
  /// <summary>
  ///   Исключение, которое выдается при возникновении ошибки ввода-вывода.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class IOException : SystemException
  {
    [NonSerialized]
    private string _maybeFullPath;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IOException" /> пустой строкой сообщения (""), значением COR_E_IO для HRESULT и пустой ссылкой для внутреннего исключения.
    /// </summary>
    [__DynamicallyInvokable]
    public IOException()
      : base(Environment.GetResourceString("Arg_IOException"))
    {
      this.SetErrorCode(-2146232800);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IOException" /> с заданным для строки сообщения значением <paramref name="message" />, HRESULT имеет значение COR_E_IO, а внутреннее исключение — <see langword="null" />.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public IOException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232800);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IOException" />, причем для его строки сообщения задается значение <paramref name="message" />, а свойство HRESULT задается пользователем.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="hresult">
    ///   Целое число, определяющее возникшую ошибку.
    /// </param>
    [__DynamicallyInvokable]
    public IOException(string message, int hresult)
      : base(message)
    {
      this.SetErrorCode(hresult);
    }

    internal IOException(string message, int hresult, string maybeFullPath)
      : base(message)
    {
      this.SetErrorCode(hresult);
      this._maybeFullPath = maybeFullPath;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IOException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public IOException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146232800);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.IOException" /> класс, содержащий указанную информацию сериализации и контекст.
    /// </summary>
    /// <param name="info">
    ///   Данные для сериализации или десериализации объекта.
    /// </param>
    /// <param name="context">Источник и назначение для объекта.</param>
    protected IOException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
