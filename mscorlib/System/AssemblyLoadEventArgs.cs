// Decompiled with JetBrains decompiler
// Type: System.AssemblyLoadEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Предоставляет данные для события <see cref="E:System.AppDomain.AssemblyLoad" />.
  /// </summary>
  [ComVisible(true)]
  public class AssemblyLoadEventArgs : EventArgs
  {
    private Assembly _LoadedAssembly;

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.Assembly" /> представляющий текущую загруженную сборку.
    /// </summary>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Reflection.Assembly" /> представляющий текущую загруженную сборку.
    /// </returns>
    public Assembly LoadedAssembly
    {
      get
      {
        return this._LoadedAssembly;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AssemblyLoadEventArgs" /> с использованием указанного объекта <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    /// <param name="loadedAssembly">
    ///   Экземпляр, представляющий текущую загруженную сборку.
    /// </param>
    public AssemblyLoadEventArgs(Assembly loadedAssembly)
    {
      this._LoadedAssembly = loadedAssembly;
    }
  }
}
