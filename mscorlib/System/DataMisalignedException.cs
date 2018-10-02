// Decompiled with JetBrains decompiler
// Type: System.DataMisalignedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выбрасывается, когда единица данных считывается или записывается по адресу, не кратному размеру данных.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DataMisalignedException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DataMisalignedException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public DataMisalignedException()
      : base(Environment.GetResourceString("Arg_DataMisalignedException"))
    {
      this.SetErrorCode(-2146233023);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.DataMisalignedException" />, используя указанное сообщение об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Объект <see cref="T:System.String" /> описывающий ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public DataMisalignedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233023);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.DataMisalignedException" /> класса, используя указанное сообщение об ошибке и базового исключения.
    /// </summary>
    /// <param name="message">
    ///   Объект <see cref="T:System.String" /> описывающий ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое стало причиной текущего исключения <see cref="T:System.DataMisalignedException" />.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public DataMisalignedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233023);
    }

    internal DataMisalignedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
