// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PolicyException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
  /// <summary>
  ///   Исключение, которое вызывается, когда политика запрещает запуск кода.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class PolicyException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.PolicyException" /> стандартными свойствами.
    /// </summary>
    public PolicyException()
      : base(Environment.GetResourceString("Policy_Default"))
    {
      this.HResult = -2146233322;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.PolicyException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public PolicyException(string message)
      : base(message)
    {
      this.HResult = -2146233322;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.PolicyException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="exception">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="exception" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public PolicyException(string message, Exception exception)
      : base(message, exception)
    {
      this.HResult = -2146233322;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.PolicyException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected PolicyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    internal PolicyException(string message, int hresult)
      : base(message)
    {
      this.HResult = hresult;
    }

    internal PolicyException(string message, int hresult, Exception exception)
      : base(message, exception)
    {
      this.HResult = hresult;
    }
  }
}
