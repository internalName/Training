// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.BaseChannelWithProperties
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
  ///   Предоставляет базовую реализацию для каналов, чтобы предоставить интерфейс словаря к его свойствам.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public abstract class BaseChannelWithProperties : BaseChannelObjectWithProperties
  {
    /// <summary>
    ///   Указывает высший приемник в стеке приемников канала.
    /// </summary>
    protected IChannelSinkBase SinksWithProperties;

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionary" /> свойств канала, связанных с объектом текущего канала.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Collections.IDictionary" /> свойств канала, связанных с объектом текущего канала.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public override IDictionary Properties
    {
      [SecurityCritical] get
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) this);
        if (this.SinksWithProperties != null)
        {
          IServerChannelSink serverChannelSink = this.SinksWithProperties as IServerChannelSink;
          if (serverChannelSink != null)
          {
            for (; serverChannelSink != null; serverChannelSink = serverChannelSink.NextChannelSink)
            {
              IDictionary properties = serverChannelSink.Properties;
              if (properties != null)
                arrayList.Add((object) properties);
            }
          }
          else
          {
            for (IClientChannelSink clientChannelSink = (IClientChannelSink) this.SinksWithProperties; clientChannelSink != null; clientChannelSink = clientChannelSink.NextChannelSink)
            {
              IDictionary properties = clientChannelSink.Properties;
              if (properties != null)
                arrayList.Add((object) properties);
            }
          }
        }
        return (IDictionary) new AggregateDictionary((ICollection) arrayList);
      }
    }
  }
}
