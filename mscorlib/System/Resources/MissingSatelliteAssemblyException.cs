// Decompiled with JetBrains decompiler
// Type: System.Resources.MissingSatelliteAssemblyException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
  /// <summary>
  ///   Исключение, возникающее при отсутствии вспомогательной сборки для ресурсов культуры по умолчанию.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class MissingSatelliteAssemblyException : SystemException
  {
    private string _cultureName;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> стандартными свойствами.
    /// </summary>
    public MissingSatelliteAssemblyException()
      : base(Environment.GetResourceString("MissingSatelliteAssembly_Default"))
    {
      this.SetErrorCode(-2146233034);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public MissingSatelliteAssemblyException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233034);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> класса с указанным сообщением об ошибке и именем нейтрального языка и региональных параметров.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="cultureName">
    ///   Имя нейтрального языка и региональных параметров.
    /// </param>
    public MissingSatelliteAssemblyException(string message, string cultureName)
      : base(message)
    {
      this.SetErrorCode(-2146233034);
      this._cultureName = cultureName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public MissingSatelliteAssemblyException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233034);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> из сериализованных данных.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении исключения.
    /// </param>
    protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Возвращает имя языка и региональных параметров по умолчанию.
    /// </summary>
    /// <returns>Имя языка и региональных параметров по умолчанию.</returns>
    public string CultureName
    {
      get
      {
        return this._cultureName;
      }
    }
  }
}
