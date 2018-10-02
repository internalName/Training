// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.RemotingSurrogateSelector
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Выбирает суррогат удаленного взаимодействия, который может использоваться для сериализации объекта, производного от <see cref="T:System.MarshalByRefObject" />.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class RemotingSurrogateSelector : ISurrogateSelector
  {
    private static Type s_IMethodCallMessageType = typeof (IMethodCallMessage);
    private static Type s_IMethodReturnMessageType = typeof (IMethodReturnMessage);
    private static Type s_ObjRefType = typeof (ObjRef);
    private RemotingSurrogate _remotingSurrogate = new RemotingSurrogate();
    private ObjRefSurrogate _objRefSurrogate = new ObjRefSurrogate();
    private object _rootObj;
    private ISurrogateSelector _next;
    private ISerializationSurrogate _messageSurrogate;
    private MessageSurrogateFilter _filter;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" />.
    /// </summary>
    public RemotingSurrogateSelector()
    {
      this._messageSurrogate = (ISerializationSurrogate) new MessageSurrogate(this);
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Remoting.Messaging.MessageSurrogateFilter" /> делегат для текущего экземпляра <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.Messaging.MessageSurrogateFilter" /> Делегирования для текущего экземпляра <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" />.
    /// </returns>
    public MessageSurrogateFilter Filter
    {
      set
      {
        this._filter = value;
      }
      get
      {
        return this._filter;
      }
    }

    /// <summary>Устанавливает объект в корне графа объектов.</summary>
    /// <param name="obj">Объект в корне графа объектов.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetRootObject(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      this._rootObj = obj;
      (this._messageSurrogate as SoapMessageSurrogate)?.SetRootObject(this._rootObj);
    }

    /// <summary>Возвращает объект в корне графа объектов.</summary>
    /// <returns>Объект в корне графа объектов.</returns>
    public object GetRootObject()
    {
      return this._rootObj;
    }

    /// <summary>
    ///   Добавляет указанный <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> селектор в цепочку.
    /// </summary>
    /// <param name="selector">
    ///   Следующий <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> для проверки.
    /// </param>
    [SecurityCritical]
    public virtual void ChainSelector(ISurrogateSelector selector)
    {
      this._next = selector;
    }

    /// <summary>
    ///   Возвращает соответствующий суррогат для заданного типа в заданном контексте.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Для которого запрашивается суррогат.
    /// </param>
    /// <param name="context">
    ///   Источник или назначение сериализации.
    /// </param>
    /// <param name="ssout">
    ///   При возвращении данного метода содержит <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> подходящим для указанного типа объекта.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Соответствующий суррогат для заданного типа в заданном контексте.
    /// </returns>
    [SecurityCritical]
    public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector ssout)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (type.IsMarshalByRef)
      {
        ssout = (ISurrogateSelector) this;
        return (ISerializationSurrogate) this._remotingSurrogate;
      }
      if (RemotingSurrogateSelector.s_IMethodCallMessageType.IsAssignableFrom(type) || RemotingSurrogateSelector.s_IMethodReturnMessageType.IsAssignableFrom(type))
      {
        ssout = (ISurrogateSelector) this;
        return this._messageSurrogate;
      }
      if (RemotingSurrogateSelector.s_ObjRefType.IsAssignableFrom(type))
      {
        ssout = (ISurrogateSelector) this;
        return (ISerializationSurrogate) this._objRefSurrogate;
      }
      if (this._next != null)
        return this._next.GetSurrogate(type, context, out ssout);
      ssout = (ISurrogateSelector) null;
      return (ISerializationSurrogate) null;
    }

    /// <summary>
    ///   Возвращает следующий <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> в цепочке суррогатных селекторов.
    /// </summary>
    /// <returns>
    ///   Следующий <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> в цепочке суррогатных селекторов.
    /// </returns>
    [SecurityCritical]
    public virtual ISurrogateSelector GetNextSelector()
    {
      return this._next;
    }

    /// <summary>
    ///   Настраивает текущий селектор суррогата для использования формата SOAP.
    /// </summary>
    public virtual void UseSoapFormat()
    {
      this._messageSurrogate = (ISerializationSurrogate) new SoapMessageSurrogate(this);
      ((SoapMessageSurrogate) this._messageSurrogate).SetRootObject(this._rootObj);
    }
  }
}
