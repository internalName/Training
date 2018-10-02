// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ChannelDataStore
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
  ///   Хранит данные канала для каналов удаленного взаимодействия.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ChannelDataStore : IChannelDataStore
  {
    private string[] _channelURIs;
    private DictionaryEntry[] _extraData;

    private ChannelDataStore(string[] channelUrls, DictionaryEntry[] extraData)
    {
      this._channelURIs = channelUrls;
      this._extraData = extraData;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Channels.ChannelDataStore" /> класса URI, на который отображается текущий канал.
    /// </summary>
    /// <param name="channelURIs">
    ///   Массив URI, на который отображается текущий канал.
    /// </param>
    public ChannelDataStore(string[] channelURIs)
    {
      this._channelURIs = channelURIs;
      this._extraData = (DictionaryEntry[]) null;
    }

    [SecurityCritical]
    internal ChannelDataStore InternalShallowCopy()
    {
      return new ChannelDataStore(this._channelURIs, this._extraData);
    }

    /// <summary>
    ///   Возвращает или задает массив URI, на который отображается текущий канал.
    /// </summary>
    /// <returns>Массив URI, на который отображается текущий канал.</returns>
    public string[] ChannelUris
    {
      [SecurityCritical] get
      {
        return this._channelURIs;
      }
      set
      {
        this._channelURIs = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает объект данных, связанный с указанным ключом для используемого канала.
    /// </summary>
    /// <param name="key">Ключ, связанный с данным объектом.</param>
    /// <returns>Указанный объект данных для используемого канала.</returns>
    public object this[object key]
    {
      [SecurityCritical] get
      {
        foreach (DictionaryEntry dictionaryEntry in this._extraData)
        {
          if (dictionaryEntry.Key.Equals(key))
            return dictionaryEntry.Value;
        }
        return (object) null;
      }
      [SecurityCritical] set
      {
        if (this._extraData == null)
        {
          this._extraData = new DictionaryEntry[1];
          this._extraData[0] = new DictionaryEntry(key, value);
        }
        else
        {
          int length = this._extraData.Length;
          DictionaryEntry[] dictionaryEntryArray = new DictionaryEntry[length + 1];
          int index;
          for (index = 0; index < length; ++index)
            dictionaryEntryArray[index] = this._extraData[index];
          dictionaryEntryArray[index] = new DictionaryEntry(key, value);
          this._extraData = dictionaryEntryArray;
        }
      }
    }
  }
}
