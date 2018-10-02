// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MethodReturnMessageWrapper
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
  ///   Реализует <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> интерфейс, чтобы создать сообщение, которое выступает в качестве ответа на вызов метода удаленного объекта.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class MethodReturnMessageWrapper : InternalMessageWrapper, IMethodReturnMessage, IMethodMessage, IMessage
  {
    private IMethodReturnMessage _msg;
    private IDictionary _properties;
    private ArgMapper _argMapper;
    private object[] _args;
    private object _returnValue;
    private Exception _exception;

    /// <summary>
    ///   Создает оболочку для <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> для создания <see cref="T:System.Runtime.Remoting.Messaging.MethodReturnMessageWrapper" />.
    /// </summary>
    /// <param name="msg">
    ///   Сообщение, действующее как исходящий вызов метода удаленного объекта.
    /// </param>
    public MethodReturnMessageWrapper(IMethodReturnMessage msg)
      : base((IMessage) msg)
    {
      this._msg = msg;
      this._args = this._msg.Args;
      this._returnValue = this._msg.ReturnValue;
      this._exception = this._msg.Exception;
    }

    /// <summary>
    ///   Возвращает универсальный код ресурса (URI) удаленного объекта, для которого осуществляется вызов метода.
    /// </summary>
    /// <returns>URI удаленного объекта.</returns>
    public string Uri
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
    ///   Возвращает флаг, указывающий, принимает ли метод переменное число аргументов.
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
    ///   Возвращает число аргументов в вызове метода, которые помечены как <see langword="ref" /> Параметры или <see langword="out" /> Параметры.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Int32" /> представляющий число аргументов в вызове метода, которые помечены как <see langword="ref" /> Параметры или <see langword="out" /> Параметры.
    /// </returns>
    public virtual int OutArgCount
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.ArgCount;
      }
    }

    /// <summary>
    ///   Возвращает указанный аргумент, отмеченный как <see langword="ref" /> параметр или <see langword="out" /> параметра.
    /// </summary>
    /// <param name="argNum">Индекс запрошенного аргумента.</param>
    /// <returns>
    ///   Указанный аргумент, помеченный как <see langword="ref" /> параметр или <see langword="out" /> параметра.
    /// </returns>
    [SecurityCritical]
    public virtual object GetOutArg(int argNum)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArg(argNum);
    }

    /// <summary>
    ///   Возвращает имя указанного аргумента, помеченного как <see langword="ref" /> параметр или <see langword="out" /> параметра.
    /// </summary>
    /// <param name="index">Индекс запрошенного аргумента.</param>
    /// <returns>
    ///   Имя аргумента или <see langword="null" /> если текущий метод не реализован.
    /// </returns>
    [SecurityCritical]
    public virtual string GetOutArgName(int index)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArgName(index);
    }

    /// <summary>
    ///   Возвращает массив аргументов в вызове метода, которые помечены как <see langword="ref" /> Параметры или <see langword="out" /> Параметры.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> представляющий аргументы в вызове метода, которые помечены как <see langword="ref" /> Параметры или <see langword="out" /> Параметры.
    /// </returns>
    public virtual object[] OutArgs
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.Args;
      }
    }

    /// <summary>
    ///   Возвращает исключение, инициированное во время вызова метода, или <see langword="null" /> если метод не выдал исключение.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Exception" /> Исключение при вызове метода или <see langword="null" /> Если метод не выдал исключение.
    /// </returns>
    public virtual Exception Exception
    {
      [SecurityCritical] get
      {
        return this._exception;
      }
      set
      {
        this._exception = value;
      }
    }

    /// <summary>Получает возвращаемое значение вызова метода.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Object" /> представляющий возвращаемое значение вызова метода.
    /// </returns>
    public virtual object ReturnValue
    {
      [SecurityCritical] get
      {
        return this._returnValue;
      }
      set
      {
        this._returnValue = value;
      }
    }

    /// <summary>
    ///   <see cref="T:System.Collections.IDictionary" /> Интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IDictionary" /> Интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </returns>
    public virtual IDictionary Properties
    {
      [SecurityCritical] get
      {
        if (this._properties == null)
          this._properties = (IDictionary) new MethodReturnMessageWrapper.MRMWrapperDictionary((IMethodReturnMessage) this, this._msg.Properties);
        return this._properties;
      }
    }

    private class MRMWrapperDictionary : Hashtable
    {
      private IMethodReturnMessage _mrmsg;
      private IDictionary _idict;

      public MRMWrapperDictionary(IMethodReturnMessage msg, IDictionary idict)
      {
        this._mrmsg = msg;
        this._idict = idict;
      }

      public override object this[object key]
      {
        [SecuritySafeCritical] get
        {
          switch (key as string)
          {
            case "__Uri":
              return (object) this._mrmsg.Uri;
            case "__MethodName":
              return (object) this._mrmsg.MethodName;
            case "__MethodSignature":
              return this._mrmsg.MethodSignature;
            case "__TypeName":
              return (object) this._mrmsg.TypeName;
            case "__Return":
              return this._mrmsg.ReturnValue;
            case "__OutArgs":
              return (object) this._mrmsg.OutArgs;
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
            case "__Return":
            case "__OutArgs":
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
