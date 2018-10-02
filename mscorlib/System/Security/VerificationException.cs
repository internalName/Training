// Decompiled with JetBrains decompiler
// Type: System.Security.VerificationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
  /// <summary>
  ///   Исключение, которое создается, если в политику безопасности входит требование типобезопасности кода, а в ходе проверки невозможно определить, выполнено ли это требование в коде.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class VerificationException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.VerificationException" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public VerificationException()
      : base(Environment.GetResourceString("Verification_Exception"))
    {
      this.SetErrorCode(-2146233075);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.VerificationException" /> класса поясняющее сообщение.
    /// </summary>
    /// <param name="message">
    ///   Произошла сообщение с указанием причины исключения.
    /// </param>
    [__DynamicallyInvokable]
    public VerificationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233075);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.VerificationException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public VerificationException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233075);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.VerificationException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected VerificationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
