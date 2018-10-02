// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DefaultDllImportSearchPathsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает пути, используемые для поиска DLL, предоставляющих вызываемые для платформы функции.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public sealed class DefaultDllImportSearchPathsAttribute : Attribute
  {
    internal DllImportSearchPath _paths;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.DefaultDllImportSearchPathsAttribute" /> класса, задание пути для поиска для целей платформа вызывает.
    /// </summary>
    /// <param name="paths">
    ///   Побитовое сочетание значений перечисления, которые указывают на пути, LoadLibraryEx вызывает функцию поиска в течение платформы.
    /// </param>
    [__DynamicallyInvokable]
    public DefaultDllImportSearchPathsAttribute(DllImportSearchPath paths)
    {
      this._paths = paths;
    }

    /// <summary>
    ///   Возвращает побитовое сочетание значений перечисления, которые указывают на пути, LoadLibraryEx вызывает функцию поиска в течение платформы.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание значений перечисления, задающее путей поиска для платформы вызывает.
    /// </returns>
    [__DynamicallyInvokable]
    public DllImportSearchPath Paths
    {
      [__DynamicallyInvokable] get
      {
        return this._paths;
      }
    }
  }
}
