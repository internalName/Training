// Decompiled with JetBrains decompiler
// Type: System.Resources.NeutralResourcesLanguageAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>
  ///   Сообщает диспетчеру ресурсов приложения по умолчанию языка и региональных параметров.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class NeutralResourcesLanguageAttribute : Attribute
  {
    private string _culture;
    private UltimateResourceFallbackLocation _fallbackLoc;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" />.
    /// </summary>
    /// <param name="cultureName">
    ///   Имя языка и региональных параметров, в записаны нейтральные ресурсы текущей сборки.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="cultureName" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public NeutralResourcesLanguageAttribute(string cultureName)
    {
      if (cultureName == null)
        throw new ArgumentNullException(nameof (cultureName));
      this._culture = cultureName;
      this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> класса резервного расположения указанного ресурса ultimate.
    /// </summary>
    /// <param name="cultureName">
    ///   Имя языка и региональных параметров, в записаны нейтральные ресурсы текущей сборки.
    /// </param>
    /// <param name="location">
    ///   Одно из значений перечисления, которое указывает расположение, из которого извлекаются нейтральные резервные ресурсы.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="cultureName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="location" /> не является членом <see cref="T:System.Resources.UltimateResourceFallbackLocation" />.
    /// </exception>
    public NeutralResourcesLanguageAttribute(string cultureName, UltimateResourceFallbackLocation location)
    {
      if (cultureName == null)
        throw new ArgumentNullException(nameof (cultureName));
      if (!Enum.IsDefined(typeof (UltimateResourceFallbackLocation), (object) location))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidNeutralResourcesLanguage_FallbackLoc", (object) location));
      this._culture = cultureName;
      this._fallbackLoc = location;
    }

    /// <summary>Возвращает имя языка и региональных параметров.</summary>
    /// <returns>
    ///   Имя языка и региональных параметров по умолчанию для главной сборки.
    /// </returns>
    [__DynamicallyInvokable]
    public string CultureName
    {
      [__DynamicallyInvokable] get
      {
        return this._culture;
      }
    }

    /// <summary>
    ///   Возвращает расположение класса <see cref="T:System.Resources.ResourceManager" />, применяемого для извлечения нейтральных ресурсов с помощью процесса использования резервных ресурсов.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее на расположение (главной или вспомогательной сборки), из которого следует извлекать нейтральные ресурсы.
    /// </returns>
    public UltimateResourceFallbackLocation Location
    {
      get
      {
        return this._fallbackLoc;
      }
    }
  }
}
