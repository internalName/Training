// Decompiled with JetBrains decompiler
// Type: System.IO.DriveNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
  /// <summary>
  ///   Исключение вызывается при попытке доступа к недоступному диску или данным совместного использования.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class DriveNotFoundException : IOException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.DriveNotFoundException" /> класса со строкой сообщения, установленной на системного сообщения и значением HRESULT, равным COR_E_DIRECTORYNOTFOUND.
    /// </summary>
    public DriveNotFoundException()
      : base(Environment.GetResourceString("Arg_DriveNotFoundException"))
    {
      this.SetErrorCode(-2147024893);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.DriveNotFoundException" /> класса с заданной строкой сообщения и значением HRESULT, равным COR_E_DIRECTORYNOTFOUND.
    /// </summary>
    /// <param name="message">
    ///   Объект <see cref="T:System.String" /> описывающий ошибку.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    public DriveNotFoundException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024893);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.DriveNotFoundException" /> класса с указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public DriveNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024893);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.DriveNotFoundException" /> класс, содержащий указанную информацию сериализации и контекст.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> содержащий сериализованные данные объекта о возникающем исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" /> содержащий контекстные сведения об источнике или назначении исключения.
    /// </param>
    protected DriveNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
