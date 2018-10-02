// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ConstructionResponse
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Реализует <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" /> интерфейс, чтобы создать сообщение, которое отвечает на вызов для создания удаленного объекта.
  /// </summary>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> класс из массива заголовков удаленного взаимодействия и сообщение запроса.
    /// </summary>
    /// <param name="h">
    ///   Массив заголовков удаленного взаимодействия, содержащих пары "ключ значение".
    ///    Этот массив используется для инициализации <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> поля для этих заголовков, которые принадлежат к пространству имен «http://schemas.microsoft.com/clr/soap/messageProperties».
    /// </param>
    /// <param name="mcm">
    ///   Сообщение запроса, составляющее вызов конструктора удаленного объекта.
    /// </param>
    public ConstructionResponse(Header[] h, IMethodCallMessage mcm)
      : base(h, mcm)
    {
    }

    internal ConstructionResponse(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
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
            this.ExternalProperties = (IDictionary) new CRMDictionary((IConstructionReturnMessage) this, this.InternalProperties);
          return this.ExternalProperties;
        }
      }
    }
  }
}
