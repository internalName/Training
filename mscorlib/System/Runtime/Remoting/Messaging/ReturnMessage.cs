// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Содержит сообщение, возвращенное в ответ на вызов метода удаленного объекта.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage
  {
    internal object _ret;
    internal object _properties;
    internal string _URI;
    internal Exception _e;
    internal object[] _outArgs;
    internal int _outArgsCount;
    internal string _methodName;
    internal string _typeName;
    internal Type[] _methodSignature;
    internal bool _hasVarArgs;
    internal LogicalCallContext _callContext;
    internal ArgMapper _argMapper;
    internal MethodBase _methodBase;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> класс со всеми сведениями, вызывающей стороне возвращается после вызова метода.
    /// </summary>
    /// <param name="ret">
    ///   Объект, возвращенный вызванным методом, из которого текущий <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> был создан экземпляр.
    /// </param>
    /// <param name="outArgs">
    ///   Объекты, возвращенные из вызванного метода в качестве <see langword="out" /> параметров.
    /// </param>
    /// <param name="outArgsCount">
    ///   Количество <see langword="out" /> Параметры, возвращаемые из вызванного метода.
    /// </param>
    /// <param name="callCtx">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> Вызова метода.
    /// </param>
    /// <param name="mcm">
    ///   Исходный вызов метода для вызванного метода.
    /// </param>
    [SecurityCritical]
    public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
    {
      this._ret = ret;
      this._outArgs = outArgs;
      this._outArgsCount = outArgsCount;
      this._callContext = callCtx == null ? Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext : callCtx;
      if (mcm == null)
        return;
      this._URI = mcm.Uri;
      this._methodName = mcm.MethodName;
      this._methodSignature = (Type[]) null;
      this._typeName = mcm.TypeName;
      this._hasVarArgs = mcm.HasVarArgs;
      this._methodBase = mcm.MethodBase;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.
    /// </summary>
    /// <param name="e">
    ///   Исключение, инициированное при выполнении удаленно вызванного метода.
    /// </param>
    /// <param name="mcm">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> С помощью которого создается экземпляр <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> класса.
    /// </param>
    [SecurityCritical]
    public ReturnMessage(Exception e, IMethodCallMessage mcm)
    {
      this._e = ReturnMessage.IsCustomErrorEnabled() ? (Exception) new RemotingException(Environment.GetResourceString("Remoting_InternalError")) : e;
      this._callContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
      if (mcm == null)
        return;
      this._URI = mcm.Uri;
      this._methodName = mcm.MethodName;
      this._methodSignature = (Type[]) null;
      this._typeName = mcm.TypeName;
      this._hasVarArgs = mcm.HasVarArgs;
      this._methodBase = mcm.MethodBase;
    }

    /// <summary>
    ///   Возвращает или задает URI удаленного объекта, для которого был вызван удаленный метод.
    /// </summary>
    /// <returns>
    ///   URI удаленного объекта, для которого был вызван удаленный метод.
    /// </returns>
    public string Uri
    {
      [SecurityCritical] get
      {
        return this._URI;
      }
      set
      {
        this._URI = value;
      }
    }

    /// <summary>Возвращает имя вызванного метода.</summary>
    /// <returns>
    ///   Имя метода, текущий <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> исходит от.
    /// </returns>
    public string MethodName
    {
      [SecurityCritical] get
      {
        return this._methodName;
      }
    }

    /// <summary>
    ///   Возвращает имя типа, для которого был вызван удаленный метод.
    /// </summary>
    /// <returns>
    ///   Имя типа удаленного объекта, для которого был вызван удаленный метод.
    /// </returns>
    public string TypeName
    {
      [SecurityCritical] get
      {
        return this._typeName;
      }
    }

    /// <summary>
    ///   Получает или задает массив <see cref="T:System.Type" /> объектов, содержащих подпись метода.
    /// </summary>
    /// <returns>
    ///   Массив <see cref="T:System.Type" /> объектов, содержащих подпись метода.
    /// </returns>
    public object MethodSignature
    {
      [SecurityCritical] get
      {
        if (this._methodSignature == null && this._methodBase != (MethodBase) null)
          this._methodSignature = Message.GenerateMethodSignature(this._methodBase);
        return (object) this._methodSignature;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.MethodBase" /> вызванного метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.MethodBase" /> Вызванного метода.
    /// </returns>
    public MethodBase MethodBase
    {
      [SecurityCritical] get
      {
        return this._methodBase;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, принимает ли вызванный метод переменное число аргументов.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если вызванный метод принимает переменное число аргументов; в противном случае — <see langword="false" />.
    /// </returns>
    public bool HasVarArgs
    {
      [SecurityCritical] get
      {
        return this._hasVarArgs;
      }
    }

    /// <summary>Возвращает число аргументов вызванного метода.</summary>
    /// <returns>Число аргументов вызванного метода.</returns>
    public int ArgCount
    {
      [SecurityCritical] get
      {
        if (this._outArgs == null)
          return this._outArgsCount;
        return this._outArgs.Length;
      }
    }

    /// <summary>
    ///   Возвращает указанный аргумент, переданный удаленному методу во время вызова метода.
    /// </summary>
    /// <param name="argNum">
    ///   Отсчитываемый от нуля индекс запрошенного аргумента.
    /// </param>
    /// <returns>
    ///   Аргумент, переданный удаленному методу во время вызова метода.
    /// </returns>
    [SecurityCritical]
    public object GetArg(int argNum)
    {
      if (this._outArgs == null)
      {
        if (argNum < 0 || argNum >= this._outArgsCount)
          throw new ArgumentOutOfRangeException(nameof (argNum));
        return (object) null;
      }
      if (argNum < 0 || argNum >= this._outArgs.Length)
        throw new ArgumentOutOfRangeException(nameof (argNum));
      return this._outArgs[argNum];
    }

    /// <summary>Возвращает имя указанного аргумента метода.</summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс запрошенного имени аргумента.
    /// </param>
    /// <returns>Имя указанного аргумента метода.</returns>
    [SecurityCritical]
    public string GetArgName(int index)
    {
      if (this._outArgs == null)
      {
        if (index < 0 || index >= this._outArgsCount)
          throw new ArgumentOutOfRangeException(nameof (index));
      }
      else if (index < 0 || index >= this._outArgs.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      if (this._methodBase != (MethodBase) null)
        return InternalRemotingServices.GetReflectionCachedData(this._methodBase).Parameters[index].Name;
      return "__param" + (object) index;
    }

    /// <summary>
    ///   Возвращает указанный аргумент, переданный методу, вызванному на удаленный объект.
    /// </summary>
    /// <returns>
    ///   Аргумент, переданный методу, вызванному на удаленный объект.
    /// </returns>
    public object[] Args
    {
      [SecurityCritical] get
      {
        if (this._outArgs == null)
          return new object[this._outArgsCount];
        return this._outArgs;
      }
    }

    /// <summary>
    ///   Возвращает число <see langword="out" /> или <see langword="ref" /> аргументов вызванного метода.
    /// </summary>
    /// <returns>
    ///   Количество <see langword="out" /> или <see langword="ref" /> аргументов вызванного метода.
    /// </returns>
    public int OutArgCount
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.ArgCount;
      }
    }

    /// <summary>
    ///   Возвращает объект, переданный в качестве <see langword="out" /> или <see langword="ref" /> параметра во время удаленного вызова метода.
    /// </summary>
    /// <param name="argNum">
    ///   Отсчитываемый от нуля индекс запрошенного <see langword="out" /> или <see langword="ref" /> параметра.
    /// </param>
    /// <returns>
    ///   Объект, передаваемый в качестве <see langword="out" /> или <see langword="ref" /> параметра во время удаленного вызова метода.
    /// </returns>
    [SecurityCritical]
    public object GetOutArg(int argNum)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArg(argNum);
    }

    /// <summary>
    ///   Возвращает имя указанного <see langword="out" /> или <see langword="ref" /> параметра передается удаленному методу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс запрошенного аргумента.
    /// </param>
    /// <returns>
    ///   Строка, представляющая имя указанного <see langword="out" /> или <see langword="ref" /> параметра, или <see langword="null" /> если текущий метод не реализован.
    /// </returns>
    [SecurityCritical]
    public string GetOutArgName(int index)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArgName(index);
    }

    /// <summary>
    ///   Возвращает указанный объект, переданный как <see langword="out" /> или <see langword="ref" /> параметров вызванного метода.
    /// </summary>
    /// <returns>
    ///   Объект, передаваемый как <see langword="out" /> или <see langword="ref" /> параметров вызванного метода.
    /// </returns>
    public object[] OutArgs
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.Args;
      }
    }

    /// <summary>
    ///   Возвращает исключение, инициированное во время удаленного вызова метода.
    /// </summary>
    /// <returns>
    ///   Исключение, инициированное при вызове метода или <see langword="null" /> Если исключение не возникло во время вызова.
    /// </returns>
    public Exception Exception
    {
      [SecurityCritical] get
      {
        return this._e;
      }
    }

    /// <summary>Возвращает объект, возвращенный вызванным методом.</summary>
    /// <returns>Объект, возвращаемый вызываемым методом.</returns>
    public virtual object ReturnValue
    {
      [SecurityCritical] get
      {
        return this._ret;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionary" /> свойств, содержащихся в текущем <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IDictionary" /> Свойств, содержащихся в текущем <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.
    /// </returns>
    public virtual IDictionary Properties
    {
      [SecurityCritical] get
      {
        if (this._properties == null)
          this._properties = (object) new MRMDictionary((IMethodReturnMessage) this, (IDictionary) null);
        return (IDictionary) this._properties;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> вызванного метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> Вызванного метода.
    /// </returns>
    public LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return this.GetLogicalCallContext();
      }
    }

    [SecurityCritical]
    internal LogicalCallContext GetLogicalCallContext()
    {
      if (this._callContext == null)
        this._callContext = new LogicalCallContext();
      return this._callContext;
    }

    internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
    {
      LogicalCallContext callContext = this._callContext;
      this._callContext = ctx;
      return callContext;
    }

    internal bool HasProperties()
    {
      return this._properties != null;
    }

    [SecurityCritical]
    internal static bool IsCustomErrorEnabled()
    {
      object data = CallContext.GetData("__CustomErrorsEnabled");
      if (data != null)
        return (bool) data;
      return false;
    }
  }
}
