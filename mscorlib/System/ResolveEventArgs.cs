// Decompiled with JetBrains decompiler
// Type: System.ResolveEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Предоставляет данные для загрузчика события разрешения, такие как <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, <see cref="E:System.AppDomain.ReflectionOnlyAssemblyResolve" />, и <see cref="E:System.AppDomain.AssemblyResolve" /> события.
  /// </summary>
  [ComVisible(true)]
  public class ResolveEventArgs : EventArgs
  {
    private string _Name;
    private Assembly _RequestingAssembly;

    /// <summary>Возвращает имя элемента для разрешения.</summary>
    /// <returns>Имя элемента.</returns>
    public string Name
    {
      get
      {
        return this._Name;
      }
    }

    /// <summary>
    ///   Получает сборку, зависимость которой необходимо разрешить.
    /// </summary>
    /// <returns>
    ///   Сборки, запрошенный элемент с указанным <see cref="P:System.ResolveEventArgs.Name" /> свойство.
    /// </returns>
    public Assembly RequestingAssembly
    {
      get
      {
        return this._RequestingAssembly;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ResolveEventArgs" /> класс, указав имя разрешаемого элемента.
    /// </summary>
    /// <param name="name">Имя разрешаемого элемента.</param>
    public ResolveEventArgs(string name)
    {
      this._Name = name;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ResolveEventArgs" /> класс, указав имя элемента для разрешения и сборку, зависимость которой необходимо разрешить.
    /// </summary>
    /// <param name="name">Имя разрешаемого элемента.</param>
    /// <param name="requestingAssembly">
    ///   Сборка, зависимость которой необходимо разрешить.
    /// </param>
    public ResolveEventArgs(string name, Assembly requestingAssembly)
    {
      this._Name = name;
      this._RequestingAssembly = requestingAssembly;
    }
  }
}
