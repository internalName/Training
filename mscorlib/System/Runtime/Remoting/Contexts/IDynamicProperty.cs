// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IDynamicProperty
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Указывает, что реализующее свойство должно быть зарегистрировано во время выполнения посредством <see cref="M:System.Runtime.Remoting.Contexts.Context.RegisterDynamicProperty(System.Runtime.Remoting.Contexts.IDynamicProperty,System.ContextBoundObject,System.Runtime.Remoting.Contexts.Context)" /> метод.
  /// </summary>
  [ComVisible(true)]
  public interface IDynamicProperty
  {
    /// <summary>Возвращает имя динамического свойства.</summary>
    /// <returns>Имя динамического свойства.</returns>
    string Name { [SecurityCritical] get; }
  }
}
