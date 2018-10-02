// Decompiled with JetBrains decompiler
// Type: System.Reflection.ManifestResourceInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Предоставляет доступ к ресурсам манифеста, которые являются XML-файлами, описывающими зависимости приложения.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public class ManifestResourceInfo
  {
    private Assembly _containingAssembly;
    private string _containingFileName;
    private ResourceLocation _resourceLocation;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.ManifestResourceInfo" /> класса для ресурсов, хранящихся в указанной сборки и файла и который имеет указанное расположение.
    /// </summary>
    /// <param name="containingAssembly">
    ///   Сборка, содержащая ресурс манифеста.
    /// </param>
    /// <param name="containingFileName">
    ///   Имя файла, содержащего ресурс манифеста, если файл не является таким же, как файл манифеста.
    /// </param>
    /// <param name="resourceLocation">
    ///   Побитовое сочетание значений перечисления, предоставляющий сведения о расположении ресурса манифеста.
    /// </param>
    [__DynamicallyInvokable]
    public ManifestResourceInfo(Assembly containingAssembly, string containingFileName, ResourceLocation resourceLocation)
    {
      this._containingAssembly = containingAssembly;
      this._containingFileName = containingFileName;
      this._resourceLocation = resourceLocation;
    }

    /// <summary>
    ///   Получает для ресурса манифеста содержащую его сборку.
    /// </summary>
    /// <returns>Сборка, содержащая ресурс манифеста.</returns>
    [__DynamicallyInvokable]
    public virtual Assembly ReferencedAssembly
    {
      [__DynamicallyInvokable] get
      {
        return this._containingAssembly;
      }
    }

    /// <summary>
    ///   Возвращает имя файла, содержащего ресурс манифеста, если он не является таким же, как файл манифеста.
    /// </summary>
    /// <returns>Имя файла ресурса манифеста.</returns>
    [__DynamicallyInvokable]
    public virtual string FileName
    {
      [__DynamicallyInvokable] get
      {
        return this._containingFileName;
      }
    }

    /// <summary>Получает расположение ресурса манифеста.</summary>
    /// <returns>
    ///   Побитовое сочетание <see cref="T:System.Reflection.ResourceLocation" /> флагов, указывающих расположение ресурса манифеста.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual ResourceLocation ResourceLocation
    {
      [__DynamicallyInvokable] get
      {
        return this._resourceLocation;
      }
    }
  }
}
