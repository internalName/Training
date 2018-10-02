// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.NamespaceResolveEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Предоставляет данные для события <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" />.
  /// </summary>
  [ComVisible(false)]
  public class NamespaceResolveEventArgs : EventArgs
  {
    private string _NamespaceName;
    private Assembly _RequestingAssembly;
    private Collection<Assembly> _ResolvedAssemblies;

    /// <summary>Возвращает имя пространства имен для разрешения.</summary>
    /// <returns>Имя пространства имен для разрешения.</returns>
    public string NamespaceName
    {
      get
      {
        return this._NamespaceName;
      }
    }

    /// <summary>
    ///   Возвращает имя сборки, зависимость которой необходимо разрешить.
    /// </summary>
    /// <returns>
    ///   Имя сборки, зависимость которой необходимо разрешить.
    /// </returns>
    public Assembly RequestingAssembly
    {
      get
      {
        return this._RequestingAssembly;
      }
    }

    /// <summary>
    ///   Возвращает коллекцию сборок; Если обработчик событий для <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" /> событие, вызываемое, возвращается пустая коллекция и обработчик событий отвечает за добавление необходимые сборки.
    /// </summary>
    /// <returns>
    ///   Коллекция сборок, которые определяют требуемым пространством имен.
    /// </returns>
    public Collection<Assembly> ResolvedAssemblies
    {
      get
      {
        return this._ResolvedAssemblies;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.WindowsRuntime.NamespaceResolveEventArgs" /> указанием пространства имен для разрешения и сборку, зависимость которой необходимо разрешить.
    /// </summary>
    /// <param name="namespaceName">Пространство имен для решения.</param>
    /// <param name="requestingAssembly">
    ///   Сборка, зависимость которой необходимо разрешить.
    /// </param>
    public NamespaceResolveEventArgs(string namespaceName, Assembly requestingAssembly)
    {
      this._NamespaceName = namespaceName;
      this._RequestingAssembly = requestingAssembly;
      this._ResolvedAssemblies = new Collection<Assembly>();
    }
  }
}
