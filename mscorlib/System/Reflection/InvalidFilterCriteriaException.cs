// Decompiled with JetBrains decompiler
// Type: System.Reflection.InvalidFilterCriteriaException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>
  ///   Исключение, которое возникает в методе <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" />, если для данного типа фильтра используются недопустимые критерии.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class InvalidFilterCriteriaException : ApplicationException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> со стандартными свойствами.
    /// </summary>
    public InvalidFilterCriteriaException()
      : base(Environment.GetResourceString("Arg_InvalidFilterCriteriaException"))
    {
      this.SetErrorCode(-2146232831);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> с помощью заданного параметра HRESULT и строки сообщения.
    /// </summary>
    /// <param name="message">Текст сообщения исключения.</param>
    public InvalidFilterCriteriaException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232831);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public InvalidFilterCriteriaException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232831);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> класс, содержащий указанную информацию сериализации и контекст.
    /// </summary>
    /// <param name="info">
    ///   Объект <see langword="SerializationInfo" /> содержащий сведения, необходимые для сериализации данного экземпляра.
    /// </param>
    /// <param name="context">
    ///   Объект <see langword="StreamingContext" /> содержащий источник и назначение сериализованного потока, связанного с данным экземпляром.
    /// </param>
    protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
