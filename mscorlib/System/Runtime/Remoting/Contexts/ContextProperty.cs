// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.ContextProperty
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Содержит пары имя значение имени свойства и объект, представляющий свойство контекста.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ContextProperty
  {
    internal string _name;
    internal object _property;

    /// <summary>
    ///   Возвращает имя класса T:System.Runtime.Remoting.Contexts.ContextProperty.
    /// </summary>
    /// <returns>
    ///   Имя <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> класса.
    /// </returns>
    public virtual string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>
    ///   Возвращает объект, представляющий свойство контекста.
    /// </summary>
    /// <returns>Объект, представляющий свойство контекста.</returns>
    public virtual object Property
    {
      get
      {
        return this._property;
      }
    }

    internal ContextProperty(string name, object prop)
    {
      this._name = name;
      this._property = prop;
    }
  }
}
