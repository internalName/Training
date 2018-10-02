// Decompiled with JetBrains decompiler
// Type: System.Exception
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System
{
  /// <summary>
  ///   Представляет ошибки, происходящие во время выполнения приложения.
  /// 
  ///   Исходный код .NET Framework для этого типа см. в указанном источнике.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Exception))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class Exception : ISerializable, _Exception
  {
    [OptionalField]
    private static object s_EDILock = new object();
    private string _className;
    private MethodBase _exceptionMethod;
    private string _exceptionMethodString;
    internal string _message;
    private IDictionary _data;
    private Exception _innerException;
    private string _helpURL;
    private object _stackTrace;
    [OptionalField]
    private object _watsonBuckets;
    private string _stackTraceString;
    private string _remoteStackTraceString;
    private int _remoteStackIndex;
    private object _dynamicMethods;
    internal int _HResult;
    private string _source;
    private IntPtr _xptrs;
    private int _xcode;
    [OptionalField]
    private UIntPtr _ipForWatsonBuckets;
    [OptionalField(VersionAdded = 4)]
    private SafeSerializationManager _safeSerializationManager;
    private const int _COMPlusExceptionCode = -532462766;

    private void Init()
    {
      this._message = (string) null;
      this._stackTrace = (object) null;
      this._dynamicMethods = (object) null;
      this.HResult = -2146233088;
      this._xcode = -532462766;
      this._xptrs = (IntPtr) 0;
      this._watsonBuckets = (object) null;
      this._ipForWatsonBuckets = UIntPtr.Zero;
      this._safeSerializationManager = new SafeSerializationManager();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Exception" />.
    /// </summary>
    [__DynamicallyInvokable]
    public Exception()
    {
      this.Init();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Exception" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public Exception(string message)
    {
      this.Init();
      this._message = message;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Exception" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, вызвавшее текущее исключение, или пустая ссылка (<see langword="Nothing" /> в Visual Basic), если внутреннее исключение не задано.
    /// </param>
    [__DynamicallyInvokable]
    public Exception(string message, Exception innerException)
    {
      this.Init();
      this._message = message;
      this._innerException = innerException;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Exception" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Имя класса — <see langword="null" />, или <see cref="P:System.Exception.HResult" /> равно нулю (0).
    /// </exception>
    [SecuritySafeCritical]
    protected Exception(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this._className = info.GetString("ClassName");
      this._message = info.GetString(nameof (Message));
      this._data = (IDictionary) info.GetValueNoThrow(nameof (Data), typeof (IDictionary));
      this._innerException = (Exception) info.GetValue(nameof (InnerException), typeof (Exception));
      this._helpURL = info.GetString("HelpURL");
      this._stackTraceString = info.GetString("StackTraceString");
      this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
      this._remoteStackIndex = info.GetInt32("RemoteStackIndex");
      this._exceptionMethodString = (string) info.GetValue("ExceptionMethod", typeof (string));
      this.HResult = info.GetInt32(nameof (HResult));
      this._source = info.GetString(nameof (Source));
      this._watsonBuckets = info.GetValueNoThrow(nameof (WatsonBuckets), typeof (byte[]));
      this._safeSerializationManager = info.GetValueNoThrow("SafeSerializationManager", typeof (SafeSerializationManager)) as SafeSerializationManager;
      if (this._className == null || this.HResult == 0)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      if (context.State != StreamingContextStates.CrossAppDomain)
        return;
      this._remoteStackTraceString += this._stackTraceString;
      this._stackTraceString = (string) null;
    }

    /// <summary>Получает сообщение, описывающее текущее исключение.</summary>
    /// <returns>
    ///   Сообщение об ошибке с объяснением причин исключения или пустая строка ("").
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string Message
    {
      [__DynamicallyInvokable] get
      {
        if (this._message != null)
          return this._message;
        if (this._className == null)
          this._className = this.GetClassName();
        return Environment.GetResourceString("Exception_WasThrown", (object) this._className);
      }
    }

    /// <summary>
    ///   Возвращает коллекцию пар ключ/значение, предоставляющие дополнительные сведения об исключении, определяемые пользователем.
    /// </summary>
    /// <returns>
    ///   Объект, который реализует интерфейс <see cref="T:System.Collections.IDictionary" /> и содержит коллекцию заданных пользователем пар «ключ — значение».
    ///    По умолчанию является пустой коллекцией.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual IDictionary Data
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this._data == null)
          this._data = !Exception.IsImmutableAgileException(this) ? (IDictionary) new ListDictionaryInternal() : (IDictionary) new EmptyReadOnlyDictionaryInternal();
        return this._data;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsImmutableAgileException(Exception e);

    [FriendAccessAllowed]
    internal void AddExceptionDataForRestrictedErrorInfo(string restrictedError, string restrictedErrorReference, string restrictedCapabilitySid, object restrictedErrorObject, bool hasrestrictedLanguageErrorObject = false)
    {
      IDictionary data = this.Data;
      if (data == null)
        return;
      data.Add((object) "RestrictedDescription", (object) restrictedError);
      data.Add((object) "RestrictedErrorReference", (object) restrictedErrorReference);
      data.Add((object) "RestrictedCapabilitySid", (object) restrictedCapabilitySid);
      data.Add((object) "__RestrictedErrorObject", restrictedErrorObject == null ? (object) (Exception.__RestrictedErrorObject) null : (object) new Exception.__RestrictedErrorObject(restrictedErrorObject));
      data.Add((object) "__HasRestrictedLanguageErrorObject", (object) hasrestrictedLanguageErrorObject);
    }

    internal bool TryGetRestrictedLanguageErrorObject(out object restrictedErrorObject)
    {
      restrictedErrorObject = (object) null;
      if (this.Data == null || !this.Data.Contains((object) "__HasRestrictedLanguageErrorObject"))
        return false;
      if (this.Data.Contains((object) "__RestrictedErrorObject"))
      {
        Exception.__RestrictedErrorObject restrictedErrorObject1 = this.Data[(object) "__RestrictedErrorObject"] as Exception.__RestrictedErrorObject;
        if (restrictedErrorObject1 != null)
          restrictedErrorObject = restrictedErrorObject1.RealErrorObject;
      }
      return (bool) this.Data[(object) "__HasRestrictedLanguageErrorObject"];
    }

    private string GetClassName()
    {
      if (this._className == null)
        this._className = this.GetType().ToString();
      return this._className;
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает исключение <see cref="T:System.Exception" />, которое является корневой причиной одного или нескольких последующих исключений.
    /// </summary>
    /// <returns>
    ///   В цепочке исключений создается первое исключение.
    ///    Если значением свойства <see cref="P:System.Exception.InnerException" /> текущего исключения является пустая ссылка (<see langword="Nothing" /> в Visual Basic), это свойство возвращает текущее исключение.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Exception GetBaseException()
    {
      Exception innerException = this.InnerException;
      Exception exception = this;
      for (; innerException != null; innerException = innerException.InnerException)
        exception = innerException;
      return exception;
    }

    /// <summary>
    ///   Возвращает экземпляр класса <see cref="T:System.Exception" />, который вызвал текущее исключение.
    /// </summary>
    /// <returns>
    ///   Объект, описывающий ошибку, которая вызвала текущее исключение.
    ///    Свойство <see cref="P:System.Exception.InnerException" /> возвращает то же значение, что было передано в конструктор <see cref="M:System.Exception.#ctor(System.String,System.Exception)" />, или значение <see langword="null" />, если конструктору не было передано значение внутреннего исключения.
    ///    Это свойство доступно только для чтения.
    /// </returns>
    [__DynamicallyInvokable]
    public Exception InnerException
    {
      [__DynamicallyInvokable] get
      {
        return this._innerException;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IRuntimeMethodInfo GetMethodFromStackTrace(object stackTrace);

    [SecuritySafeCritical]
    private MethodBase GetExceptionMethodFromStackTrace()
    {
      IRuntimeMethodInfo methodFromStackTrace = Exception.GetMethodFromStackTrace(this._stackTrace);
      if (methodFromStackTrace == null)
        return (MethodBase) null;
      return RuntimeType.GetMethodBase(methodFromStackTrace);
    }

    /// <summary>Возвращает метод, создавший текущее исключение.</summary>
    /// <returns>
    ///   Метод <see cref="T:System.Reflection.MethodBase" />, вызвавший текущее исключение.
    /// </returns>
    public MethodBase TargetSite
    {
      [SecuritySafeCritical] get
      {
        return this.GetTargetSiteInternal();
      }
    }

    [SecurityCritical]
    private MethodBase GetTargetSiteInternal()
    {
      if (this._exceptionMethod != (MethodBase) null)
        return this._exceptionMethod;
      if (this._stackTrace == null)
        return (MethodBase) null;
      this._exceptionMethod = this._exceptionMethodString == null ? this.GetExceptionMethodFromStackTrace() : this.GetExceptionMethodFromString();
      return this._exceptionMethod;
    }

    /// <summary>
    ///   Получает строковое представление непосредственных кадров в стеке вызова.
    /// </summary>
    /// <returns>
    ///   Строка, описывающая непосредственные фреймы стека вызова.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string StackTrace
    {
      [__DynamicallyInvokable] get
      {
        return this.GetStackTrace(true);
      }
    }

    private string GetStackTrace(bool needFileInfo)
    {
      string stackTrace1 = this._stackTraceString;
      string stackTrace2 = this._remoteStackTraceString;
      if (!needFileInfo)
      {
        stackTrace1 = this.StripFileInfo(stackTrace1, false);
        stackTrace2 = this.StripFileInfo(stackTrace2, true);
      }
      if (stackTrace1 != null)
        return stackTrace2 + stackTrace1;
      if (this._stackTrace == null)
        return stackTrace2;
      string stackTrace3 = Environment.GetStackTrace(this, needFileInfo);
      return stackTrace2 + stackTrace3;
    }

    [FriendAccessAllowed]
    internal void SetErrorCode(int hr)
    {
      this.HResult = hr;
    }

    /// <summary>
    ///   Получает или задает ссылку на файл справки, связанный с этим исключением.
    /// </summary>
    /// <returns>URN или URL-адрес.</returns>
    [__DynamicallyInvokable]
    public virtual string HelpLink
    {
      [__DynamicallyInvokable] get
      {
        return this._helpURL;
      }
      [__DynamicallyInvokable] set
      {
        this._helpURL = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя приложения или объекта, вызывавшего ошибку.
    /// </summary>
    /// <returns>Имя приложения или объекта, вызвавшего ошибку.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Объект должен быть объектом среды выполнения <see cref="N:System.Reflection" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual string Source
    {
      [__DynamicallyInvokable] get
      {
        if (this._source == null)
        {
          System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(this, true);
          if (stackTrace.FrameCount > 0)
          {
            Module module = stackTrace.GetFrame(0).GetMethod().Module;
            RuntimeModule runtimeModule = module as RuntimeModule;
            if ((Module) runtimeModule == (Module) null)
            {
              ModuleBuilder moduleBuilder = module as ModuleBuilder;
              if (!((Module) moduleBuilder != (Module) null))
                throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
              runtimeModule = (RuntimeModule) moduleBuilder.InternalModule;
            }
            this._source = runtimeModule.GetRuntimeAssembly().GetSimpleName();
          }
        }
        return this._source;
      }
      [__DynamicallyInvokable] set
      {
        this._source = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает строковое представление текущего исключения.
    /// </summary>
    /// <returns>Строковое представление текущего исключения.</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ToString(true, true);
    }

    private string ToString(bool needFileLineInfo, bool needMessage)
    {
      string str1 = needMessage ? this.Message : (string) null;
      string str2 = str1 == null || str1.Length <= 0 ? this.GetClassName() : this.GetClassName() + ": " + str1;
      if (this._innerException != null)
        str2 = str2 + " ---> " + this._innerException.ToString(needFileLineInfo, needMessage) + Environment.NewLine + "   " + Environment.GetResourceString("Exception_EndOfInnerExceptionStack");
      string stackTrace = this.GetStackTrace(needFileLineInfo);
      if (stackTrace != null)
        str2 = str2 + Environment.NewLine + stackTrace;
      return str2;
    }

    [SecurityCritical]
    private string GetExceptionMethodString()
    {
      MethodBase targetSiteInternal = this.GetTargetSiteInternal();
      if (targetSiteInternal == (MethodBase) null)
        return (string) null;
      if (targetSiteInternal is DynamicMethod.RTDynamicMethod)
        return (string) null;
      char ch = '\n';
      StringBuilder stringBuilder = new StringBuilder();
      if ((object) (targetSiteInternal as ConstructorInfo) != null)
      {
        RuntimeConstructorInfo runtimeConstructorInfo = (RuntimeConstructorInfo) targetSiteInternal;
        Type reflectedType = runtimeConstructorInfo.ReflectedType;
        stringBuilder.Append(1);
        stringBuilder.Append(ch);
        stringBuilder.Append(runtimeConstructorInfo.Name);
        if (reflectedType != (Type) null)
        {
          stringBuilder.Append(ch);
          stringBuilder.Append(reflectedType.Assembly.FullName);
          stringBuilder.Append(ch);
          stringBuilder.Append(reflectedType.FullName);
        }
        stringBuilder.Append(ch);
        stringBuilder.Append(runtimeConstructorInfo.ToString());
      }
      else
      {
        RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo) targetSiteInternal;
        Type declaringType = runtimeMethodInfo.DeclaringType;
        stringBuilder.Append(8);
        stringBuilder.Append(ch);
        stringBuilder.Append(runtimeMethodInfo.Name);
        stringBuilder.Append(ch);
        stringBuilder.Append(runtimeMethodInfo.Module.Assembly.FullName);
        stringBuilder.Append(ch);
        if (declaringType != (Type) null)
        {
          stringBuilder.Append(declaringType.FullName);
          stringBuilder.Append(ch);
        }
        stringBuilder.Append(runtimeMethodInfo.ToString());
      }
      return stringBuilder.ToString();
    }

    [SecurityCritical]
    private MethodBase GetExceptionMethodFromString()
    {
      string[] strArray = this._exceptionMethodString.Split(new char[2]
      {
        char.MinValue,
        '\n'
      });
      if (strArray.Length != 5)
        throw new SerializationException();
      SerializationInfo info = new SerializationInfo(typeof (MemberInfoSerializationHolder), (IFormatterConverter) new FormatterConverter());
      info.AddValue("MemberType", (object) int.Parse(strArray[0], (IFormatProvider) CultureInfo.InvariantCulture), typeof (int));
      info.AddValue("Name", (object) strArray[1], typeof (string));
      info.AddValue("AssemblyName", (object) strArray[2], typeof (string));
      info.AddValue("ClassName", (object) strArray[3]);
      info.AddValue("Signature", (object) strArray[4]);
      StreamingContext context = new StreamingContext(StreamingContextStates.All);
      try
      {
        return (MethodBase) new MemberInfoSerializationHolder(info, context).GetRealObject(context);
      }
      catch (SerializationException ex)
      {
        return (MethodBase) null;
      }
    }

    /// <summary>
    ///   Возникает, когда исключение сериализовано для создания объекта состояния исключения, содержащего сериализованные данные об исключении.
    /// </summary>
    protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState
    {
      add
      {
        this._safeSerializationManager.SerializeObjectState += value;
      }
      remove
      {
        this._safeSerializationManager.SerializeObjectState -= value;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе задает объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> со сведениями об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значением параметра <paramref name="info" /> является пустая ссылка (<see langword="Nothing" /> в Visual Basic).
    /// </exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      string str = this._stackTraceString;
      if (this._stackTrace != null)
      {
        if (str == null)
          str = Environment.GetStackTrace(this, true);
        if (this._exceptionMethod == (MethodBase) null)
          this._exceptionMethod = this.GetExceptionMethodFromStackTrace();
      }
      if (this._source == null)
        this._source = this.Source;
      info.AddValue("ClassName", (object) this.GetClassName(), typeof (string));
      info.AddValue("Message", (object) this._message, typeof (string));
      info.AddValue("Data", (object) this._data, typeof (IDictionary));
      info.AddValue("InnerException", (object) this._innerException, typeof (Exception));
      info.AddValue("HelpURL", (object) this._helpURL, typeof (string));
      info.AddValue("StackTraceString", (object) str, typeof (string));
      info.AddValue("RemoteStackTraceString", (object) this._remoteStackTraceString, typeof (string));
      info.AddValue("RemoteStackIndex", (object) this._remoteStackIndex, typeof (int));
      info.AddValue("ExceptionMethod", (object) this.GetExceptionMethodString(), typeof (string));
      info.AddValue("HResult", this.HResult);
      info.AddValue("Source", (object) this._source, typeof (string));
      info.AddValue("WatsonBuckets", this._watsonBuckets, typeof (byte[]));
      if (this._safeSerializationManager == null || !this._safeSerializationManager.IsActive)
        return;
      info.AddValue("SafeSerializationManager", (object) this._safeSerializationManager, typeof (SafeSerializationManager));
      this._safeSerializationManager.CompleteSerialization((object) this, info, context);
    }

    internal Exception PrepForRemoting()
    {
      string str;
      if (this._remoteStackIndex == 0)
        str = Environment.NewLine + "Server stack trace: " + Environment.NewLine + this.StackTrace + Environment.NewLine + Environment.NewLine + "Exception rethrown at [" + (object) this._remoteStackIndex + "]: " + Environment.NewLine;
      else
        str = this.StackTrace + Environment.NewLine + Environment.NewLine + "Exception rethrown at [" + (object) this._remoteStackIndex + "]: " + Environment.NewLine;
      this._remoteStackTraceString = str;
      ++this._remoteStackIndex;
      return this;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      this._stackTrace = (object) null;
      this._ipForWatsonBuckets = UIntPtr.Zero;
      if (this._safeSerializationManager == null)
        this._safeSerializationManager = new SafeSerializationManager();
      else
        this._safeSerializationManager.CompleteDeserialization((object) this);
    }

    internal void InternalPreserveStackTrace()
    {
      string stackTrace;
      if (AppDomain.IsAppXModel())
      {
        stackTrace = this.GetStackTrace(true);
        string source = this.Source;
      }
      else
        stackTrace = this.StackTrace;
      if (stackTrace != null && stackTrace.Length > 0)
        this._remoteStackTraceString = stackTrace + Environment.NewLine;
      this._stackTrace = (object) null;
      this._stackTraceString = (string) null;
    }

    internal UIntPtr IPForWatsonBuckets
    {
      get
      {
        return this._ipForWatsonBuckets;
      }
    }

    internal object WatsonBuckets
    {
      get
      {
        return this._watsonBuckets;
      }
    }

    internal string RemoteStackTrace
    {
      get
      {
        return this._remoteStackTraceString;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void PrepareForForeignExceptionRaise();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetStackTracesDeepCopy(Exception exception, out object currentStackTrace, out object dynamicMethodArray);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SaveStackTracesFromDeepCopy(Exception exception, object currentStackTrace, object dynamicMethodArray);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object CopyStackTrace(object currentStackTrace);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object CopyDynamicMethods(object currentDynamicMethods);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern string StripFileInfo(string stackTrace, bool isRemoteStackTrace);

    [SecuritySafeCritical]
    internal object DeepCopyStackTrace(object currentStackTrace)
    {
      if (currentStackTrace != null)
        return Exception.CopyStackTrace(currentStackTrace);
      return (object) null;
    }

    [SecuritySafeCritical]
    internal object DeepCopyDynamicMethods(object currentDynamicMethods)
    {
      if (currentDynamicMethods != null)
        return Exception.CopyDynamicMethods(currentDynamicMethods);
      return (object) null;
    }

    [SecuritySafeCritical]
    internal void GetStackTracesDeepCopy(out object currentStackTrace, out object dynamicMethodArray)
    {
      Exception.GetStackTracesDeepCopy(this, out currentStackTrace, out dynamicMethodArray);
    }

    [SecuritySafeCritical]
    internal void RestoreExceptionDispatchInfo(ExceptionDispatchInfo exceptionDispatchInfo)
    {
      if (Exception.IsImmutableAgileException(this))
        return;
      try
      {
      }
      finally
      {
        object currentStackTrace = exceptionDispatchInfo.BinaryStackTraceArray == null ? (object) null : this.DeepCopyStackTrace(exceptionDispatchInfo.BinaryStackTraceArray);
        object dynamicMethodArray = exceptionDispatchInfo.DynamicMethodArray == null ? (object) null : this.DeepCopyDynamicMethods(exceptionDispatchInfo.DynamicMethodArray);
        lock (Exception.s_EDILock)
        {
          this._watsonBuckets = exceptionDispatchInfo.WatsonBuckets;
          this._ipForWatsonBuckets = exceptionDispatchInfo.IPForWatsonBuckets;
          this._remoteStackTraceString = exceptionDispatchInfo.RemoteStackTrace;
          Exception.SaveStackTracesFromDeepCopy(this, currentStackTrace, dynamicMethodArray);
        }
        this._stackTraceString = (string) null;
        Exception.PrepareForForeignExceptionRaise();
      }
    }

    /// <summary>
    ///   Возвращает или задает HRESULT — кодированное числовое значение, присвоенное определенному исключению.
    /// </summary>
    /// <returns>Значение HRESULT.</returns>
    [__DynamicallyInvokable]
    public int HResult
    {
      [__DynamicallyInvokable] get
      {
        return this._HResult;
      }
      [__DynamicallyInvokable] protected set
      {
        this._HResult = value;
      }
    }

    [SecurityCritical]
    internal virtual string InternalToString()
    {
      try
      {
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy).Assert();
      }
      catch
      {
      }
      return this.ToString(true, true);
    }

    /// <summary>
    ///   Возвращает тип среды выполнения текущего экземпляра.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий точный тип среды выполнения текущего экземпляра.
    /// </returns>
    [__DynamicallyInvokable]
    public new Type GetType()
    {
      return base.GetType();
    }

    internal bool IsTransient
    {
      [SecuritySafeCritical] get
      {
        return Exception.nIsTransient(this._HResult);
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nIsTransient(int hr);

    [SecuritySafeCritical]
    internal static string GetMessageFromNativeResources(Exception.ExceptionMessageKind kind)
    {
      string s = (string) null;
      Exception.GetMessageFromNativeResources(kind, JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetMessageFromNativeResources(Exception.ExceptionMessageKind kind, StringHandleOnStack retMesg);

    [Serializable]
    internal class __RestrictedErrorObject
    {
      [NonSerialized]
      private object _realErrorObject;

      internal __RestrictedErrorObject(object errorObject)
      {
        this._realErrorObject = errorObject;
      }

      public object RealErrorObject
      {
        get
        {
          return this._realErrorObject;
        }
      }
    }

    internal enum ExceptionMessageKind
    {
      ThreadAbort = 1,
      ThreadInterrupted = 2,
      OutOfMemory = 3,
    }
  }
}
