// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ResourceConsumptionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Runtime.Versioning
{
  /// <summary>
  ///   Указывает ресурс, потребляемый членом класса.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
  [Conditional("RESOURCE_ANNOTATION_WORK")]
  public sealed class ResourceConsumptionAttribute : Attribute
  {
    private ResourceScope _consumptionScope;
    private ResourceScope _resourceScope;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> класса, указывая область использования ресурса.
    /// </summary>
    /// <param name="resourceScope">
    ///   <see cref="T:System.Runtime.Versioning.ResourceScope" /> Потребляемого ресурса.
    /// </param>
    public ResourceConsumptionAttribute(ResourceScope resourceScope)
    {
      this._resourceScope = resourceScope;
      this._consumptionScope = this._resourceScope;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> класса объем потребленных ресурсов и область действия потребления.
    /// </summary>
    /// <param name="resourceScope">
    ///   <see cref="T:System.Runtime.Versioning.ResourceScope" /> Потребляемого ресурса.
    /// </param>
    /// <param name="consumptionScope">
    ///   <see cref="T:System.Runtime.Versioning.ResourceScope" /> Использования участником.
    /// </param>
    public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
    {
      this._resourceScope = resourceScope;
      this._consumptionScope = consumptionScope;
    }

    /// <summary>Возвращает область видимости потребляемого ресурса.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Versioning.ResourceScope" /> объект, указывающий ресурс область использования элемента.
    /// </returns>
    public ResourceScope ResourceScope
    {
      get
      {
        return this._resourceScope;
      }
    }

    /// <summary>Возвращает область потребления для данного члена.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Versioning.ResourceScope" /> объект, задающий области ресурсов, используемый этим членом.
    /// </returns>
    public ResourceScope ConsumptionScope
    {
      get
      {
        return this._consumptionScope;
      }
    }
  }
}
