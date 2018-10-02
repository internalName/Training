// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComCompatibleVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает клиенту COM, что все классы текущей версии сборки совместимы с классами в более ранней версии сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  public sealed class ComCompatibleVersionAttribute : Attribute
  {
    internal int _major;
    internal int _minor;
    internal int _build;
    internal int _revision;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ComCompatibleVersionAttribute" /> класса с основной номер версии, дополнительный номер версии, построения и редакции сборки.
    /// </summary>
    /// <param name="major">Основной номер версии сборки.</param>
    /// <param name="minor">Дополнительный номер версии сборки.</param>
    /// <param name="build">Номер построения сборки.</param>
    /// <param name="revision">Номер редакции сборки.</param>
    public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
    {
      this._major = major;
      this._minor = minor;
      this._build = build;
      this._revision = revision;
    }

    /// <summary>Возвращает основной номер версии сборки.</summary>
    /// <returns>Основной номер версии сборки.</returns>
    public int MajorVersion
    {
      get
      {
        return this._major;
      }
    }

    /// <summary>Возвращает дополнительный номер версии сборки.</summary>
    /// <returns>Дополнительный номер версии сборки.</returns>
    public int MinorVersion
    {
      get
      {
        return this._minor;
      }
    }

    /// <summary>Возвращает номер построения сборки.</summary>
    /// <returns>Номер построения сборки.</returns>
    public int BuildNumber
    {
      get
      {
        return this._build;
      }
    }

    /// <summary>Получает номер редакции сборки.</summary>
    /// <returns>Номер редакции сборки.</returns>
    public int RevisionNumber
    {
      get
      {
        return this._revision;
      }
    }
  }
}
