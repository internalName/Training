// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DesignerNamespaceResolveEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Предоставляет данные для события <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" />.
  /// </summary>
  [ComVisible(false)]
  public class DesignerNamespaceResolveEventArgs : EventArgs
  {
    private string _NamespaceName;
    private Collection<string> _ResolvedAssemblyFiles;

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
    ///   Возвращает коллекцию сборок пути к файлам; Если обработчик событий для <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> событие, вызываемое, возвращается пустая коллекция и обработчик событий отвечает за добавление файлов необходимые сборки.
    /// </summary>
    /// <returns>
    ///   Коллекция файлов сборки, которые определяют требуемым пространством имен.
    /// </returns>
    public Collection<string> ResolvedAssemblyFiles
    {
      get
      {
        return this._ResolvedAssemblyFiles;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DesignerNamespaceResolveEventArgs" />.
    /// </summary>
    /// <param name="namespaceName">
    ///   Имя пространства имен для разрешения.
    /// </param>
    public DesignerNamespaceResolveEventArgs(string namespaceName)
    {
      this._NamespaceName = namespaceName;
      this._ResolvedAssemblyFiles = new Collection<string>();
    }
  }
}
