// Decompiled with JetBrains decompiler
// Type: System.Resources.SatelliteContractVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>
  ///   Указывает, что <see cref="T:System.Resources.ResourceManager" /> объект попросить для конкретной версии вспомогательной сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class SatelliteContractVersionAttribute : Attribute
  {
    private string _version;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.SatelliteContractVersionAttribute" />.
    /// </summary>
    /// <param name="version">
    ///   Строка, указывающая версию вспомогательных сборок для загрузки.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="version" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public SatelliteContractVersionAttribute(string version)
    {
      if (version == null)
        throw new ArgumentNullException(nameof (version));
      this._version = version;
    }

    /// <summary>
    ///   Возвращает версию вспомогательных сборок, содержащих требуемые ресурсы.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая версию вспомогательных сборок, содержащих требуемые ресурсы.
    /// </returns>
    [__DynamicallyInvokable]
    public string Version
    {
      [__DynamicallyInvokable] get
      {
        return this._version;
      }
    }
  }
}
