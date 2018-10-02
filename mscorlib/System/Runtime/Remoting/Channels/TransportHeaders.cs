// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.TransportHeaders
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Хранит коллекцию заголовков, используемых в канале приемников.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class TransportHeaders : ITransportHeaders
  {
    private ArrayList _headerList;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Channels.TransportHeaders" />.
    /// </summary>
    public TransportHeaders()
    {
      this._headerList = new ArrayList(6);
    }

    /// <summary>
    ///   Возвращает или задает транспортный заголовок, связанный с указанным ключом.
    /// </summary>
    /// <param name="key">
    ///   <see cref="T:System.String" /> Которой связан запрашиваемый заголовок.
    /// </param>
    /// <returns>
    ///   Транспортный заголовок, связанный с заданным ключом, или <see langword="null" /> если ключ не найден.
    /// </returns>
    public object this[object key]
    {
      [SecurityCritical] get
      {
        string strB = (string) key;
        foreach (DictionaryEntry header in this._headerList)
        {
          if (string.Compare((string) header.Key, strB, StringComparison.OrdinalIgnoreCase) == 0)
            return header.Value;
        }
        return (object) null;
      }
      [SecurityCritical] set
      {
        if (key == null)
          return;
        string strB = (string) key;
        for (int index = this._headerList.Count - 1; index >= 0; --index)
        {
          if (string.Compare((string) ((DictionaryEntry) this._headerList[index]).Key, strB, StringComparison.OrdinalIgnoreCase) == 0)
          {
            this._headerList.RemoveAt(index);
            break;
          }
        }
        if (value == null)
          return;
        this._headerList.Add((object) new DictionaryEntry(key, value));
      }
    }

    /// <summary>
    ///   Возвращает перечислитель хранящихся транспортных заголовков.
    /// </summary>
    /// <returns>Перечислитель хранящихся транспортных заголовков.</returns>
    [SecurityCritical]
    public IEnumerator GetEnumerator()
    {
      return this._headerList.GetEnumerator();
    }
  }
}
