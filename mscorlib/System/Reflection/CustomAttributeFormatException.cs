// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeFormatException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>
  ///   Это исключение выдается при неправильном двоичном формате настраиваемого атрибута.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class CustomAttributeFormatException : FormatException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.CustomAttributeFormatException" /> класс со свойствами по умолчанию.
    /// </summary>
    public CustomAttributeFormatException()
      : base(Environment.GetResourceString("Arg_CustomAttributeFormatException"))
    {
      this.SetErrorCode(-2146232827);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.CustomAttributeFormatException" /> указанным сообщением.
    /// </summary>
    /// <param name="message">
    ///   Сообщение, указывающее причину данного исключения.
    /// </param>
    public CustomAttributeFormatException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232827);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.CustomAttributeFormatException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public CustomAttributeFormatException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232827);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.CustomAttributeFormatException" /> класс, содержащий указанную информацию сериализации и контекст.
    /// </summary>
    /// <param name="info">
    ///   Данные для сериализации или десериализации пользовательского атрибута.
    /// </param>
    /// <param name="context">
    ///   Источник и назначение пользовательского атрибута.
    /// </param>
    protected CustomAttributeFormatException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
