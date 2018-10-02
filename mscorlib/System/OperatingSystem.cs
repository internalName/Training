// Decompiled with JetBrains decompiler
// Type: System.OperatingSystem
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Предоставляет информацию об операционной системе, например версию и идентификатор платформы.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class OperatingSystem : ICloneable, ISerializable
  {
    private Version _version;
    private PlatformID _platform;
    private string _servicePack;
    private string _versionString;

    private OperatingSystem()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.OperatingSystem" />, используя указанное значение идентификатора платформы и объект версии.
    /// </summary>
    /// <param name="platform">
    ///   Одно из значений <see cref="T:System.PlatformID" />, указывающих платформу операционной системы.
    /// </param>
    /// <param name="version">
    ///   Объект <see cref="T:System.Version" />, указывающий версию операционной системы.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="version" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="platform" /> не <see cref="T:System.PlatformID" /> значение перечисления.
    /// </exception>
    public OperatingSystem(PlatformID platform, Version version)
      : this(platform, version, (string) null)
    {
    }

    internal OperatingSystem(PlatformID platform, Version version, string servicePack)
    {
      switch (platform)
      {
        case PlatformID.Win32S:
        case PlatformID.Win32Windows:
        case PlatformID.Win32NT:
        case PlatformID.WinCE:
        case PlatformID.Unix:
        case PlatformID.Xbox:
        case PlatformID.MacOSX:
          if ((object) version == null)
            throw new ArgumentNullException(nameof (version));
          this._platform = platform;
          this._version = (Version) version.Clone();
          this._servicePack = servicePack;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) platform), nameof (platform));
      }
    }

    private OperatingSystem(SerializationInfo info, StreamingContext context)
    {
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        if (!(name == nameof (_version)))
        {
          if (!(name == nameof (_platform)))
          {
            if (name == nameof (_servicePack))
              this._servicePack = info.GetString(nameof (_servicePack));
          }
          else
            this._platform = (PlatformID) info.GetValue(nameof (_platform), typeof (PlatformID));
        }
        else
          this._version = (Version) info.GetValue(nameof (_version), typeof (Version));
      }
      if (this._version == (Version) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_MissField", (object) nameof (_version)));
    }

    /// <summary>
    ///   Заполняет объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> данными, необходимыми для десериализации данного экземпляра.
    /// </summary>
    /// <param name="info">
    ///   Объект, который требуется заполнить информацией о сериализации.
    /// </param>
    /// <param name="context">
    ///   Место для хранения и извлечения сериализованных данных.
    ///    Зарезервировано для будущего использования.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("_version", (object) this._version);
      info.AddValue("_platform", (object) this._platform);
      info.AddValue("_servicePack", (object) this._servicePack);
    }

    /// <summary>
    ///   Возвращает значение перечисления <see cref="T:System.PlatformID" />, идентифицирующее данную платформу операционной системы.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.PlatformID" />.
    /// </returns>
    public PlatformID Platform
    {
      get
      {
        return this._platform;
      }
    }

    /// <summary>
    ///   Возвращает версию пакета обновления, представленную этим объектом <see cref="T:System.OperatingSystem" />.
    /// </summary>
    /// <returns>
    ///   Версия пакета обновления, если пакеты обновления поддерживаются и хотя бы один из них установлен; в противном случае — пустая строка ("").
    /// </returns>
    public string ServicePack
    {
      get
      {
        if (this._servicePack == null)
          return string.Empty;
        return this._servicePack;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Version" />, идентифицирующий данную операционную систему.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Version" />, в котором указан основной и дополнительный номера версии, номер сборки и номер редакции операционной системы.
    /// </returns>
    public Version Version
    {
      get
      {
        return this._version;
      }
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.OperatingSystem" />, идентичный данному экземпляру.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.OperatingSystem" />, который является копией данного экземпляра.
    /// </returns>
    public object Clone()
    {
      return (object) new OperatingSystem(this._platform, this._version, this._servicePack);
    }

    /// <summary>
    ///   Преобразует значение этого объекта <see cref="T:System.OperatingSystem" /> в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>
    ///   Строковое представление значений, возвращаемых свойствами <see cref="P:System.OperatingSystem.Platform" />, <see cref="P:System.OperatingSystem.Version" /> и <see cref="P:System.OperatingSystem.ServicePack" />.
    /// </returns>
    public override string ToString()
    {
      return this.VersionString;
    }

    /// <summary>
    ///   Возвращает объединенную строку, в которой представлен идентификатор платформы, версия и пакет обновления, установленный в данный момент в операционной системе.
    /// </summary>
    /// <returns>
    ///   Строковое представление значений, возвращаемых свойствами <see cref="P:System.OperatingSystem.Platform" />, <see cref="P:System.OperatingSystem.Version" /> и <see cref="P:System.OperatingSystem.ServicePack" />.
    /// </returns>
    public string VersionString
    {
      get
      {
        if (this._versionString != null)
          return this._versionString;
        string str;
        switch (this._platform)
        {
          case PlatformID.Win32S:
            str = "Microsoft Win32S ";
            break;
          case PlatformID.Win32Windows:
            str = this._version.Major > 4 || this._version.Major == 4 && this._version.Minor > 0 ? "Microsoft Windows 98 " : "Microsoft Windows 95 ";
            break;
          case PlatformID.Win32NT:
            str = "Microsoft Windows NT ";
            break;
          case PlatformID.WinCE:
            str = "Microsoft Windows CE ";
            break;
          case PlatformID.MacOSX:
            str = "Mac OS X ";
            break;
          default:
            str = "<unknown> ";
            break;
        }
        this._versionString = !string.IsNullOrEmpty(this._servicePack) ? str + this._version.ToString(3) + " " + this._servicePack : str + this._version.ToString();
        return this._versionString;
      }
    }
  }
}
