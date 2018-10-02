// Decompiled with JetBrains decompiler
// Type: System.EntryPointNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, выбрасываемое, когда попытка загрузки класса завершается неудачей из-за отсутствия метода входа.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class EntryPointNotFoundException : TypeLoadException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.EntryPointNotFoundException" />.
    /// </summary>
    public EntryPointNotFoundException()
      : base(Environment.GetResourceString("Arg_EntryPointNotFoundException"))
    {
      this.SetErrorCode(-2146233053);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.EntryPointNotFoundException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public EntryPointNotFoundException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233053);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.EntryPointNotFoundException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="inner" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public EntryPointNotFoundException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233053);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.EntryPointNotFoundException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
