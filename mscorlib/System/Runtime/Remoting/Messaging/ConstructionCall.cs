// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ConstructionCall
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Реализует <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> интерфейс, чтобы создать сообщение запроса, составляющее вызов конструктора удаленного объекта.
  /// </summary>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  public class ConstructionCall : MethodCall, IConstructionCallMessage, IMethodCallMessage, IMethodMessage, IMessage
  {
    internal Type _activationType;
    internal string _activationTypeName;
    internal IList _contextProperties;
    internal object[] _callSiteActivationAttributes;
    internal IActivator _activator;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> класс из массива заголовков удаленного взаимодействия.
    /// </summary>
    /// <param name="headers">
    ///   Массив заголовков удаленного взаимодействия, содержащих пары "ключ значение".
    ///    Этот массив используется для инициализации <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> поля для этих заголовков, которые принадлежат к пространству имен «http://schemas.microsoft.com/clr/soap/messageProperties».
    /// </param>
    public ConstructionCall(Header[] headers)
      : base(headers)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> класса путем копирования существующего сообщения.
    /// </summary>
    /// <param name="m">Сообщение удаленного взаимодействия.</param>
    public ConstructionCall(IMessage m)
      : base(m)
    {
    }

    internal ConstructionCall(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    [SecurityCritical]
    internal override bool FillSpecialHeader(string key, object value)
    {
      if (key != null)
      {
        if (key.Equals("__ActivationType"))
          this._activationType = (Type) null;
        else if (key.Equals("__ContextProperties"))
          this._contextProperties = (IList) value;
        else if (key.Equals("__CallSiteActivationAttributes"))
          this._callSiteActivationAttributes = (object[]) value;
        else if (key.Equals("__Activator"))
        {
          this._activator = (IActivator) value;
        }
        else
        {
          if (!key.Equals("__ActivationTypeName"))
            return base.FillSpecialHeader(key, value);
          this._activationTypeName = (string) value;
        }
      }
      return true;
    }

    /// <summary>
    ///   Возвращает атрибуты активации узла вызова для удаленного объекта.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> содержащий атрибуты активации узла вызова для удаленного объекта.
    /// </returns>
    public object[] CallSiteActivationAttributes
    {
      [SecurityCritical] get
      {
        return this._callSiteActivationAttributes;
      }
    }

    /// <summary>
    ///   Возвращает тип удаленного объекта, подлежащего активации.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Для активации удаленного объекта.
    /// </returns>
    public Type ActivationType
    {
      [SecurityCritical] get
      {
        if (this._activationType == (Type) null && this._activationTypeName != null)
          this._activationType = (Type) RemotingServices.InternalGetTypeFromQualifiedTypeName(this._activationTypeName, false);
        return this._activationType;
      }
    }

    /// <summary>
    ///   Возвращает полное имя типа для активации удаленного объекта.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.String" /> содержащий полное имя типа для активации удаленного объекта.
    /// </returns>
    public string ActivationTypeName
    {
      [SecurityCritical] get
      {
        return this._activationTypeName;
      }
    }

    /// <summary>
    ///   Возвращает список свойств, определяющих контекст, в котором создается удаленный объект.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Collections.IList" /> содержащий список свойств, которые определяют контекст, в котором создается удаленный объект.
    /// </returns>
    public IList ContextProperties
    {
      [SecurityCritical] get
      {
        if (this._contextProperties == null)
          this._contextProperties = (IList) new ArrayList();
        return this._contextProperties;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionary" /> интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IDictionary" /> Интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </returns>
    public override IDictionary Properties
    {
      [SecurityCritical] get
      {
        lock (this)
        {
          if (this.InternalProperties == null)
            this.InternalProperties = (IDictionary) new Hashtable();
          if (this.ExternalProperties == null)
            this.ExternalProperties = (IDictionary) new CCMDictionary((IConstructionCallMessage) this, this.InternalProperties);
          return this.ExternalProperties;
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает активатор, который активирует удаленные объекты.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.Activation.IActivator" /> Активирует удаленные объекты.
    /// </returns>
    public IActivator Activator
    {
      [SecurityCritical] get
      {
        return this._activator;
      }
      [SecurityCritical] set
      {
        this._activator = value;
      }
    }
  }
}
