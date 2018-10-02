// Decompiled with JetBrains decompiler
// Type: System.NullReferenceException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее при попытке разыменования указателя NULL на объект.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class NullReferenceException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.NullReferenceException" /> класса, задание <see cref="P:System.Exception.Message" /> свойств нового экземпляра системным сообщением, содержащим описание ошибки, такие как «значение «null» найдено где требовалось экземпляр объекта.»
    ///    Это сообщение учитывает текущую культуру системы.
    /// </summary>
    [__DynamicallyInvokable]
    public NullReferenceException()
      : base(Environment.GetResourceString("Arg_NullReferenceException"))
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NullReferenceException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public NullReferenceException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NullReferenceException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public NullReferenceException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NullReferenceException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected NullReferenceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
