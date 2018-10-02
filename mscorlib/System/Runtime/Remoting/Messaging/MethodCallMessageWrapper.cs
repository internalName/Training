// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MethodCallMessageWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Реализует <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> интерфейс для создания сообщения запроса, который действует как вызов метода удаленного объекта.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class MethodCallMessageWrapper : InternalMessageWrapper, IMethodCallMessage, IMethodMessage, IMessage
  {
    private IMethodCallMessage _msg;
    private IDictionary _properties;
    private ArgMapper _argMapper;
    private object[] _args;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.MethodCallMessageWrapper" /> класса путем заключения <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> интерфейса.
    /// </summary>
    /// <param name="msg">
    ///   Сообщение, действующее как исходящий вызов метода удаленного объекта.
    /// </param>
    public MethodCallMessageWrapper(IMethodCallMessage msg)
      : base((IMessage) msg)
    {
      this._msg = msg;
      this._args = this._msg.Args;
    }

    /// <summary>
    ///   Возвращает универсальный код ресурса (URI) удаленного объекта, для которого осуществляется вызов метода.
    /// </summary>
    /// <returns>URI удаленного объекта.</returns>
    public virtual string Uri
    {
      [SecurityCritical] get
      {
        return this._msg.Uri;
      }
      set
      {
        this._msg.Properties[(object) Message.UriKey] = (object) value;
      }
    }

    /// <summary>Возвращает имя вызванного метода.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащая имя вызываемого метода.
    /// </returns>
    public virtual string MethodName
    {
      [SecurityCritical] get
      {
        return this._msg.MethodName;
      }
    }

    /// <summary>
    ///   Возвращает полное имя типа удаленного объекта, для которого осуществляется вызов метода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий полное имя типа удаленного объекта, для которого осуществляется вызов метода.
    /// </returns>
    public virtual string TypeName
    {
      [SecurityCritical] get
      {
        return this._msg.TypeName;
      }
    }

    /// <summary>Возвращает объект, содержащий подпись метода.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Object" /> содержащий сигнатуру метода.
    /// </returns>
    public virtual object MethodSignature
    {
      [SecurityCritical] get
      {
        return this._msg.MethodSignature;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> для текущего вызова метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> Для текущего вызова метода.
    /// </returns>
    public virtual LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return this._msg.LogicalCallContext;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.MethodBase" /> вызванного метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.MethodBase" /> Вызванного метода.
    /// </returns>
    public virtual MethodBase MethodBase
    {
      [SecurityCritical] get
      {
        return this._msg.MethodBase;
      }
    }

    /// <summary>Возвращает число аргументов, передаваемых в метод.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Int32" /> представляющий количество аргументов, передаваемых в метод.
    /// </returns>
    public virtual int ArgCount
    {
      [SecurityCritical] get
      {
        if (this._args != null)
          return this._args.Length;
        return 0;
      }
    }

    /// <summary>
    ///   Возвращает имя аргумента метода по указанному индексу.
    /// </summary>
    /// <param name="index">Индекс запрошенного аргумента.</param>
    /// <returns>Имя аргумента метода.</returns>
    [SecurityCritical]
    public virtual string GetArgName(int index)
    {
      return this._msg.GetArgName(index);
    }

    /// <summary>
    ///   Возвращает аргумент метода в виде объекта по указанному индексу.
    /// </summary>
    /// <param name="argNum">Индекс запрошенного аргумента.</param>
    /// <returns>Аргумент метода в виде объекта.</returns>
    [SecurityCritical]
    public virtual object GetArg(int argNum)
    {
      return this._args[argNum];
    }

    /// <summary>Возвращает массив аргументов, передаваемых в метод.</summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> представляющий аргументы, переданные в метод.
    /// </returns>
    public virtual object[] Args
    {
      [SecurityCritical] get
      {
        return this._args;
      }
      set
      {
        this._args = value;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, принимает ли метод переменное число аргументов.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если метод может принимать переменное число аргументов; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool HasVarArgs
    {
      [SecurityCritical] get
      {
        return this._msg.HasVarArgs;
      }
    }

    /// <summary>
    ///   Возвращает число аргументов в вызове метода, которые не помечены как <see langword="out" /> параметров.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Int32" /> представляющий число аргументов в вызове метода, которые не помечены как <see langword="out" /> параметров.
    /// </returns>
    public virtual int InArgCount
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, false);
        return this._argMapper.ArgCount;
      }
    }

    /// <summary>
    ///   Возвращает аргумент метода по указанному индексу, который не помечен как <see langword="out" /> параметр.
    /// </summary>
    /// <param name="argNum">Индекс запрошенного аргумента.</param>
    /// <returns>
    ///   Аргумент метода, который не помечен как <see langword="out" /> параметр.
    /// </returns>
    [SecurityCritical]
    public virtual object GetInArg(int argNum)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, false);
      return this._argMapper.GetArg(argNum);
    }

    /// <summary>
    ///   Возвращает имя аргумента метода по указанному индексу, который не помечен как  параметр.
    /// </summary>
    /// <param name="index">Индекс запрошенного аргумента.</param>
    /// <returns>
    ///   Имя аргумента метода, который не помечен как  параметр.
    /// </returns>
    [SecurityCritical]
    public virtual string GetInArgName(int index)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, false);
      return this._argMapper.GetArgName(index);
    }

    /// <summary>
    ///   Возвращает массив аргументов в вызове метода, которые не помечены как <see langword="out" /> параметров.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> представляющий аргументы в вызове метода, которые не помечены как <see langword="out" /> параметров.
    /// </returns>
    public virtual object[] InArgs
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, false);
        return this._argMapper.Args;
      }
    }

    /// <summary>
    ///   <see cref="T:System.Collections.IDictionary" /> Представляющий коллекцию свойств сообщения удаленного взаимодействия.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IDictionary" /> Интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </returns>
    public virtual IDictionary Properties
    {
      [SecurityCritical] get
      {
        if (this._properties == null)
          this._properties = (IDictionary) new MethodCallMessageWrapper.MCMWrapperDictionary((IMethodCallMessage) this, this._msg.Properties);
        return this._properties;
      }
    }

    private class MCMWrapperDictionary : Hashtable
    {
      private IMethodCallMessage _mcmsg;
      private IDictionary _idict;

      public MCMWrapperDictionary(IMethodCallMessage msg, IDictionary idict)
      {
        this._mcmsg = msg;
        this._idict = idict;
      }

      public override object this[object key]
      {
        [SecuritySafeCritical] get
        {
          switch (key as string)
          {
            case "__Uri":
              return (object) this._mcmsg.Uri;
            case "__MethodName":
              return (object) this._mcmsg.MethodName;
            case "__MethodSignature":
              return this._mcmsg.MethodSignature;
            case "__TypeName":
              return (object) this._mcmsg.TypeName;
            case "__Args":
              return (object) this._mcmsg.Args;
            default:
              return this._idict[key];
          }
        }
        [SecuritySafeCritical] set
        {
          switch (key as string)
          {
            case "__MethodName":
            case "__MethodSignature":
            case "__TypeName":
            case "__Args":
              throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
            case null:
              break;
            default:
              this._idict[key] = value;
              break;
          }
        }
      }
    }
  }
}
