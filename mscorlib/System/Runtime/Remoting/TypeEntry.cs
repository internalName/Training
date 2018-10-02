// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.TypeEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Реализует базовый класс, который содержит сведения о конфигурации, которые используются для активации экземпляра удаленного типа.
  /// </summary>
  [ComVisible(true)]
  public class TypeEntry
  {
    private string _typeName;
    private string _assemblyName;
    private RemoteAppEntry _cachedRemoteAppEntry;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.TypeEntry" />.
    /// </summary>
    protected TypeEntry()
    {
    }

    /// <summary>
    ///   Возвращает полное имя типа объекта, который настроен для удаленной активации типа.
    /// </summary>
    /// <returns>
    ///   Полный введите имя типа объекта, сконфигурированного типа удаленной активации.
    /// </returns>
    public string TypeName
    {
      get
      {
        return this._typeName;
      }
      set
      {
        this._typeName = value;
      }
    }

    /// <summary>
    ///   Возвращает имя сборки типа объекта, который настроен как тип удаленной активации.
    /// </summary>
    /// <returns>
    ///   Имя сборки типа объекта, который настроен как тип удаленной активации.
    /// </returns>
    public string AssemblyName
    {
      get
      {
        return this._assemblyName;
      }
      set
      {
        this._assemblyName = value;
      }
    }

    internal void CacheRemoteAppEntry(RemoteAppEntry entry)
    {
      this._cachedRemoteAppEntry = entry;
    }

    internal RemoteAppEntry GetRemoteAppEntry()
    {
      return this._cachedRemoteAppEntry;
    }
  }
}
