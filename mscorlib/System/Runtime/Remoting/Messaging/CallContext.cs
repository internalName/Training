// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.CallContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Предоставляет набор свойств, которые переносятся с ветви кода.
  ///    Этот класс не наследуется.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  public sealed class CallContext
  {
    private CallContext()
    {
    }

    internal static LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      LogicalCallContext logicalCallContext = executionContext.LogicalCallContext;
      executionContext.LogicalCallContext = callCtx;
      return logicalCallContext;
    }

    /// <summary>Очищает область данных с указанным именем.</summary>
    /// <param name="name">Имя области данных пустая.</param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void FreeNamedDataSlot(string name)
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      executionContext.LogicalCallContext.FreeNamedDataSlot(name);
      executionContext.IllogicalCallContext.FreeNamedDataSlot(name);
    }

    /// <summary>
    ///   Извлекает объект, связанный с указанным именем из логического контекста вызова.
    /// </summary>
    /// <param name="name">
    ///   Имя элемента в логическом контексте вызова.
    /// </param>
    /// <returns>
    ///   Объект в логическом контексте вызова с заданным именем.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static object LogicalGetData(string name)
    {
      return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.GetData(name);
    }

    private static object IllogicalGetData(string name)
    {
      return Thread.CurrentThread.GetExecutionContextReader().IllogicalCallContext.GetData(name);
    }

    internal static IPrincipal Principal
    {
      [SecurityCritical] get
      {
        return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.Principal;
      }
      [SecurityCritical] set
      {
        Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Principal = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает контекст хоста, связанный с текущим потоком.
    /// </summary>
    /// <returns>Контекст хоста, связанный с текущим потоком.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static object HostContext
    {
      [SecurityCritical] get
      {
        ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
        return executionContextReader.IllogicalCallContext.HostContext ?? executionContextReader.LogicalCallContext.HostContext;
      }
      [SecurityCritical] set
      {
        ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
        if (value is ILogicalThreadAffinative)
        {
          executionContext.IllogicalCallContext.HostContext = (object) null;
          executionContext.LogicalCallContext.HostContext = value;
        }
        else
        {
          executionContext.IllogicalCallContext.HostContext = value;
          executionContext.LogicalCallContext.HostContext = (object) null;
        }
      }
    }

    /// <summary>
    ///   Извлекает объект, связанный с указанным именем из <see cref="T:System.Runtime.Remoting.Messaging.CallContext" />.
    /// </summary>
    /// <param name="name">Имя элемента в контексте вызова.</param>
    /// <returns>
    ///   Объект в контексте вызова, связанный с указанным именем.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static object GetData(string name)
    {
      return CallContext.LogicalGetData(name) ?? CallContext.IllogicalGetData(name);
    }

    /// <summary>
    ///   Сохраняет заданный объект и связывает его с заданным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя, с которым связывается новый элемент в контексте вызова.
    /// </param>
    /// <param name="data">Объект, сохраняемый в контексте вызова.</param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void SetData(string name, object data)
    {
      if (data is ILogicalThreadAffinative)
      {
        CallContext.LogicalSetData(name, data);
      }
      else
      {
        ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
        executionContext.LogicalCallContext.FreeNamedDataSlot(name);
        executionContext.IllogicalCallContext.SetData(name, data);
      }
    }

    /// <summary>
    ///   Сохраняет заданный объект в логическом контексте вызова и связывает его с заданным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя, с которым связывается новый элемент в логическом контексте вызова.
    /// </param>
    /// <param name="data">
    ///   Объект, сохраняемый в логическом контексте вызова, этот объект должен поддерживать сериализацию.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void LogicalSetData(string name, object data)
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      executionContext.IllogicalCallContext.FreeNamedDataSlot(name);
      executionContext.LogicalCallContext.SetData(name, data);
    }

    /// <summary>
    ///   Возвращает заголовки, которые отправляются вместе с вызовом метода.
    /// </summary>
    /// <returns>
    ///   Заголовки, которые отправляются вместе с вызовом метода.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static Header[] GetHeaders()
    {
      return Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.InternalGetHeaders();
    }

    /// <summary>
    ///   Задает заголовки, которые отправляются вместе с вызовом метода.
    /// </summary>
    /// <param name="headers">
    ///   A <see cref="T:System.Runtime.Remoting.Messaging.Header" /> массива заголовков, которые необходимо отправить вместе с вызовом метода.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void SetHeaders(Header[] headers)
    {
      Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.InternalSetHeaders(headers);
    }
  }
}
