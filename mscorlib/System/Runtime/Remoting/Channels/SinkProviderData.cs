// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.SinkProviderData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Хранит данные поставщика приемника для поставщиков приемников.
  /// </summary>
  [ComVisible(true)]
  public class SinkProviderData
  {
    private Hashtable _properties = new Hashtable((IEqualityComparer) StringComparer.InvariantCultureIgnoreCase);
    private ArrayList _children = new ArrayList();
    private string _name;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" />.
    /// </summary>
    /// <param name="name">
    ///   Имя поставщика приемника, данные в текущем <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> объект, связанный с.
    /// </param>
    public SinkProviderData(string name)
    {
      this._name = name;
    }

    /// <summary>
    ///   Возвращает имя поставщика приемника, данные в текущем <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> объект, связанный с.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> с именем узла XML, данные в текущем <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> объект, связанный с.
    /// </returns>
    public string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>
    ///   Получает словарь, через какие свойства приемника обращение к поставщику.
    /// </summary>
    /// <returns>
    ///   Словарь, через какие свойства приемника обращение к поставщику.
    /// </returns>
    public IDictionary Properties
    {
      get
      {
        return (IDictionary) this._properties;
      }
    }

    /// <summary>
    ///   Получает список дочерних <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> узлов.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Collections.IList" /> дочерних <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> узлов.
    /// </returns>
    public IList Children
    {
      get
      {
        return (IList) this._children;
      }
    }
  }
}
