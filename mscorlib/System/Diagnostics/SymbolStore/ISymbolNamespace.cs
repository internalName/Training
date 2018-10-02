// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolNamespace
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Представляет пространство имен в хранилище символов.
  /// </summary>
  [ComVisible(true)]
  public interface ISymbolNamespace
  {
    /// <summary>Возвращает текущее пространство имен.</summary>
    /// <returns>Текущее пространство имен.</returns>
    string Name { get; }

    /// <summary>
    ///   Возвращает дочерние элементы текущего пространства имен.
    /// </summary>
    /// <returns>Дочерние элементы текущего пространства имен.</returns>
    ISymbolNamespace[] GetNamespaces();

    /// <summary>
    ///   Возвращает все переменные, определенные в глобальной области в текущем пространстве имен.
    /// </summary>
    /// <returns>
    ///   Переменные, определенные в глобальной области в текущем пространстве имен.
    /// </returns>
    ISymbolVariable[] GetVariables();
  }
}
