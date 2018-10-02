// Decompiled with JetBrains decompiler
// Type: System.Resources.MissingManifestResourceException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
  /// <summary>
  ///   Исключение, которое создается, если главная сборка не содержит ресурсов для нейтрального языка и региональных параметров и соответствующей вспомогательной сборки отсутствует.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MissingManifestResourceException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.MissingManifestResourceException" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public MissingManifestResourceException()
      : base(Environment.GetResourceString("Arg_MissingManifestResourceException"))
    {
      this.SetErrorCode(-2146233038);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.MissingManifestResourceException" /> указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public MissingManifestResourceException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233038);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.MissingManifestResourceException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public MissingManifestResourceException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233038);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.MissingManifestResourceException" /> из сериализованных данных.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении исключения.
    /// </param>
    protected MissingManifestResourceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
