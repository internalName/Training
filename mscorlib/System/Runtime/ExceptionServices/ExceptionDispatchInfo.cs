// Decompiled with JetBrains decompiler
// Type: System.Runtime.ExceptionServices.ExceptionDispatchInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.ExceptionServices
{
  /// <summary>
  ///   Представляет исключение, состояние которого регистрируется в определенной точке кода.
  /// </summary>
  [__DynamicallyInvokable]
  public sealed class ExceptionDispatchInfo
  {
    private Exception m_Exception;
    private string m_remoteStackTrace;
    private object m_stackTrace;
    private object m_dynamicMethods;
    private UIntPtr m_IPForWatsonBuckets;
    private object m_WatsonBuckets;

    private ExceptionDispatchInfo(Exception exception)
    {
      this.m_Exception = exception;
      this.m_remoteStackTrace = exception.RemoteStackTrace;
      object currentStackTrace;
      object dynamicMethodArray;
      this.m_Exception.GetStackTracesDeepCopy(out currentStackTrace, out dynamicMethodArray);
      this.m_stackTrace = currentStackTrace;
      this.m_dynamicMethods = dynamicMethodArray;
      this.m_IPForWatsonBuckets = exception.IPForWatsonBuckets;
      this.m_WatsonBuckets = exception.WatsonBuckets;
    }

    internal UIntPtr IPForWatsonBuckets
    {
      get
      {
        return this.m_IPForWatsonBuckets;
      }
    }

    internal object WatsonBuckets
    {
      get
      {
        return this.m_WatsonBuckets;
      }
    }

    internal object BinaryStackTraceArray
    {
      get
      {
        return this.m_stackTrace;
      }
    }

    internal object DynamicMethodArray
    {
      get
      {
        return this.m_dynamicMethods;
      }
    }

    internal string RemoteStackTrace
    {
      get
      {
        return this.m_remoteStackTrace;
      }
    }

    /// <summary>
    ///   Создает <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> представляющий указанное исключение в текущей точке в коде.
    /// </summary>
    /// <param name="source">
    ///   Исключение, состояние которого регистрируется и представленный возвращаемого объекта.
    /// </param>
    /// <returns>
    ///   Объект, представляющий указанное исключение в текущей точке в коде.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static ExceptionDispatchInfo Capture(Exception source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source), Environment.GetResourceString("ArgumentNull_Obj"));
      return new ExceptionDispatchInfo(source);
    }

    /// <summary>
    ///   Возвращает исключение, представленный текущим экземпляром.
    /// </summary>
    /// <returns>
    ///   Исключение, которое представляется текущим экземпляром.
    /// </returns>
    [__DynamicallyInvokable]
    public Exception SourceException
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Exception;
      }
    }

    /// <summary>
    ///   Создает исключение, которое представляется текущим <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> объекта после восстановления состояния, который был сохранен, когда исключение было зафиксировано.
    /// </summary>
    [__DynamicallyInvokable]
    public void Throw()
    {
      this.m_Exception.RestoreExceptionDispatchInfo(this);
      throw this.m_Exception;
    }
  }
}
