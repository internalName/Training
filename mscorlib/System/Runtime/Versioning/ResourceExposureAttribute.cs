// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ResourceExposureAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Runtime.Versioning
{
  /// <summary>
  ///   Указывает доступность ресурса для члена класса.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
  [Conditional("RESOURCE_ANNOTATION_WORK")]
  public sealed class ResourceExposureAttribute : Attribute
  {
    private ResourceScope _resourceExposureLevel;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Versioning.ResourceExposureAttribute" /> класса с уровнем указанной уязвимости.
    /// </summary>
    /// <param name="exposureLevel">Область видимости ресурса.</param>
    public ResourceExposureAttribute(ResourceScope exposureLevel)
    {
      this._resourceExposureLevel = exposureLevel;
    }

    /// <summary>Возвращает область доступности ресурса.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Versioning.ResourceScope" />.
    /// </returns>
    public ResourceScope ResourceExposureLevel
    {
      get
      {
        return this._resourceExposureLevel;
      }
    }
  }
}
