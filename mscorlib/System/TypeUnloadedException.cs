// Decompiled with JetBrains decompiler
// Type: System.TypeUnloadedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее при попытке обращения к выгруженному классу.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class TypeUnloadedException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeUnloadedException" />.
    /// </summary>
    public TypeUnloadedException()
      : base(Environment.GetResourceString("Arg_TypeUnloadedException"))
    {
      this.SetErrorCode(-2146234349);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeUnloadedException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public TypeUnloadedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146234349);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeUnloadedException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public TypeUnloadedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146234349);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeUnloadedException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected TypeUnloadedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
