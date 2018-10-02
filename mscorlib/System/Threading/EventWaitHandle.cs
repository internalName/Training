// Decompiled with JetBrains decompiler
// Type: System.Threading.EventWaitHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>Представляет событие синхронизации потока.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class EventWaitHandle : WaitHandle
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.EventWaitHandle" /> класса, является ли дескриптор ожидания изначально сигнальным и ли сброс автоматически или вручную.
    /// </summary>
    /// <param name="initialState">
    ///   <see langword="true" /> для задания начального состояния сигнальным; <see langword="false" /> задать несигнальное.
    /// </param>
    /// <param name="mode">
    ///   Один из <see cref="T:System.Threading.EventResetMode" /> значений, который определяет, является ли событие сброс автоматически или вручную.
    /// </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public EventWaitHandle(bool initialState, EventResetMode mode)
      : this(initialState, mode, (string) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.EventWaitHandle" /> с указанием ли дескриптор ожидания изначально сигнальным, если в результате данного вызова, сбрасывается ли он автоматически или вручную и имя системного события синхронизации.
    /// </summary>
    /// <param name="initialState">
    ///   <see langword="true" /> для задания начального состояния сигнальным, если именованное событие создается в результате этого вызова; <see langword="false" /> задать несигнальное.
    /// </param>
    /// <param name="mode">
    ///   Один из <see cref="T:System.Threading.EventResetMode" /> значений, который определяет, является ли событие сброс автоматически или вручную.
    /// </param>
    /// <param name="name">
    ///   Имя события синхронизации во всей системе.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованное событие существует и имеет безопасность управления доступом, но пользователь не имеет <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />.
    /// </exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">
    ///   Именованное событие невозможно, возможно потому, что дескриптор ожидания другого типа имеет то же имя.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public EventWaitHandle(bool initialState, EventResetMode mode, string name)
    {
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      SafeWaitHandle handle;
      switch (mode)
      {
        case EventResetMode.AutoReset:
          handle = Win32Native.CreateEvent((Win32Native.SECURITY_ATTRIBUTES) null, false, initialState, name);
          break;
        case EventResetMode.ManualReset:
          handle = Win32Native.CreateEvent((Win32Native.SECURITY_ATTRIBUTES) null, true, initialState, name);
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag", (object) name));
      }
      if (handle.IsInvalid)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        handle.SetHandleAsInvalid();
        if (name != null && name.Length != 0 && 6 == lastWin32Error)
          throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        __Error.WinIOError(lastWin32Error, name);
      }
      this.SetHandleInternal(handle);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.EventWaitHandle" /> и указывает следующие сведения: передавать ли сигнал в дескриптор ожидания при первичном запуске в результате этого вызова; выполнять ли сброс автоматически или вручную; имя системного события синхронизации; логическая переменная для сохранения информации о том, было ли создано именованное системное событие.
    /// </summary>
    /// <param name="initialState">
    ///   Значение <see langword="true" /> обозначает, что именованное событие получит исходное сигнальное состояние, если такое событие создается в результате этого вызова; значение <see langword="false" /> устанавливает несигнальное состояние.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.Threading.EventResetMode" />, которое определяет, выполняется ли сброс события автоматически или вручную.
    /// </param>
    /// <param name="name">
    ///   Имя события синхронизации на уровне системы.
    /// </param>
    /// <param name="createdNew">
    ///   При завершении работы метода содержит значение <see langword="true" />, если было создано локальное событие (то есть если параметр <paramref name="name" /> имеет значение <see langword="null" /> или содержит пустую строку) или заданное именованное системное событие; или значение <see langword="false" />, если указанное именованное событие уже существовало.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованное событие уже существует и имеет настройки управления доступом, но пользователь не имеет прав <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />.
    /// </exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">
    ///   Именованное событие нельзя создать; вероятно, дескриптор ожидания другого типа имеет то же имя.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew)
      : this(initialState, mode, name, out createdNew, (EventWaitHandleSecurity) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.EventWaitHandle" /> с указанием следующих сведений: передавать ли сигнал в дескриптор ожидания при первичном запуске в результате этого вызова, выполнять ли сброс автоматически или вручную, имя системного события синхронизации, логическая переменная для сохранения информации о том, было ли создано именованное системное событие, правила управления доступом для именованного события, если оно создается.
    /// </summary>
    /// <param name="initialState">
    ///   Значение <see langword="true" /> обозначает, что именованное событие получит исходное сигнальное состояние, если такое событие создается в результате этого вызова; значение <see langword="false" /> устанавливает несигнальное состояние.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений <see cref="T:System.Threading.EventResetMode" />, которое определяет, выполняется ли сброс события автоматически или вручную.
    /// </param>
    /// <param name="name">
    ///   Имя события синхронизации на уровне системы.
    /// </param>
    /// <param name="createdNew">
    ///   При завершении работы метода содержит значение <see langword="true" />, если было создано локальное событие (то есть если параметр <paramref name="name" /> имеет значение <see langword="null" /> или содержит пустую строку) или заданное именованное системное событие; или значение <see langword="false" />, если указанное именованное событие уже существовало.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="eventSecurity">
    ///   Объект <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" />, определяющий настройки управления доступом для применения к именованному системному событию.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованное событие уже существует и имеет настройки управления доступом, но пользователь не имеет прав <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />.
    /// </exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">
    ///   Именованное событие нельзя создать; вероятно, дескриптор ожидания другого типа имеет то же имя.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    [SecurityCritical]
    public unsafe EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew, EventWaitHandleSecurity eventSecurity)
    {
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      Win32Native.SECURITY_ATTRIBUTES securityAttributes = (Win32Native.SECURITY_ATTRIBUTES) null;
      if (eventSecurity != null)
      {
        securityAttributes = new Win32Native.SECURITY_ATTRIBUTES();
        securityAttributes.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(securityAttributes);
        byte[] descriptorBinaryForm = eventSecurity.GetSecurityDescriptorBinaryForm();
        byte* pDest = stackalloc byte[descriptorBinaryForm.Length];
        Buffer.Memcpy(pDest, 0, descriptorBinaryForm, 0, descriptorBinaryForm.Length);
        securityAttributes.pSecurityDescriptor = pDest;
      }
      bool isManualReset;
      switch (mode)
      {
        case EventResetMode.AutoReset:
          isManualReset = false;
          break;
        case EventResetMode.ManualReset:
          isManualReset = true;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag", (object) name));
      }
      SafeWaitHandle handle = Win32Native.CreateEvent(securityAttributes, isManualReset, initialState, name);
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (handle.IsInvalid)
      {
        handle.SetHandleAsInvalid();
        if (name != null && name.Length != 0 && 6 == lastWin32Error)
          throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        __Error.WinIOError(lastWin32Error, name);
      }
      createdNew = lastWin32Error != 183;
      this.SetHandleInternal(handle);
    }

    [SecurityCritical]
    private EventWaitHandle(SafeWaitHandle handle)
    {
      this.SetHandleInternal(handle);
    }

    /// <summary>
    ///   Открывает указанный именованное событие, если он уже существует.
    /// </summary>
    /// <param name="name">
    ///   Имя системного события синхронизации, чтобы открыть.
    /// </param>
    /// <returns>
    ///   Объект, который представляет именованное системное событие.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">
    ///   Именованное событие не существует.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованное событие существует, но пользователь не имеет прав доступа, необходимых для его использования.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static EventWaitHandle OpenExisting(string name)
    {
      return EventWaitHandle.OpenExisting(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize);
    }

    /// <summary>
    ///   Открывает указанный именованный события синхронизации, если он уже существует, с требуемыми правами доступа.
    /// </summary>
    /// <param name="name">
    ///   Имя системного события синхронизации, чтобы открыть.
    /// </param>
    /// <param name="rights">
    ///   Битовая комбинация значений перечисления, которые определяют желаемые права доступа.
    /// </param>
    /// <returns>
    ///   Объект, который представляет именованное системное событие.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">
    ///   Именованное событие не существует.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованное событие существует, но пользователь не имеет требуемый доступ безопасности.
    /// </exception>
    [SecurityCritical]
    public static EventWaitHandle OpenExisting(string name, EventWaitHandleRights rights)
    {
      EventWaitHandle result;
      switch (EventWaitHandle.OpenExistingWorker(name, rights, out result))
      {
        case WaitHandle.OpenExistingResult.NameNotFound:
          throw new WaitHandleCannotBeOpenedException();
        case WaitHandle.OpenExistingResult.PathNotFound:
          __Error.WinIOError(3, "");
          return result;
        case WaitHandle.OpenExistingResult.NameInvalid:
          throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        default:
          return result;
      }
    }

    /// <summary>
    ///   Открывает указанное именованное событие синхронизации, если оно уже существует, и возвращает значение, указывающее, успешно ли выполнена операция.
    /// </summary>
    /// <param name="name">
    ///   Имя системного события синхронизации, которое нужно открыть.
    /// </param>
    /// <param name="result">
    ///   При возврате этого метода содержит объект <see cref="T:System.Threading.EventWaitHandle" />, представляющий именованное событие синхронизации, если вызов завершился успешно, или значение <see langword="null" />, если вызов завершился неудачно.
    ///    Этот параметр обрабатывается как неинициализированный.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если именованное событие синхронизации открылось успешно. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованное событие уже существует, но пользователь не имеет требуемых прав для безопасного доступа.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static bool TryOpenExisting(string name, out EventWaitHandle result)
    {
      return EventWaitHandle.OpenExistingWorker(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize, out result) == WaitHandle.OpenExistingResult.Success;
    }

    /// <summary>
    ///   Открывает заданное именованное событие синхронизации, если оно уже существует, с требуемыми правами доступа и возвращает значение, указывающее, успешно ли выполнена операция.
    /// </summary>
    /// <param name="name">
    ///   Имя системного события синхронизации, которое нужно открыть.
    /// </param>
    /// <param name="rights">
    ///   Битовая комбинация значений перечисления, которые определяют желаемые права доступа.
    /// </param>
    /// <param name="result">
    ///   При возврате этого метода содержит объект <see cref="T:System.Threading.EventWaitHandle" />, представляющий именованное событие синхронизации, если вызов завершился успешно, или значение <see langword="null" />, если вызов завершился неудачно.
    ///    Этот параметр обрабатывается как неинициализированный.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если именованное событие синхронизации открылось успешно. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованное событие уже существует, но пользователь не имеет требуемых прав для безопасного доступа.
    /// </exception>
    [SecurityCritical]
    public static bool TryOpenExisting(string name, EventWaitHandleRights rights, out EventWaitHandle result)
    {
      return EventWaitHandle.OpenExistingWorker(name, rights, out result) == WaitHandle.OpenExistingResult.Success;
    }

    [SecurityCritical]
    private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, EventWaitHandleRights rights, out EventWaitHandle result)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name), Environment.GetResourceString("ArgumentNull_WithParamName"));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      result = (EventWaitHandle) null;
      SafeWaitHandle handle = Win32Native.OpenEvent((int) rights, false, name);
      if (handle.IsInvalid)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (2 == lastWin32Error || 123 == lastWin32Error)
          return WaitHandle.OpenExistingResult.NameNotFound;
        if (3 == lastWin32Error)
          return WaitHandle.OpenExistingResult.PathNotFound;
        if (name != null && name.Length != 0 && 6 == lastWin32Error)
          return WaitHandle.OpenExistingResult.NameInvalid;
        __Error.WinIOError(lastWin32Error, "");
      }
      result = new EventWaitHandle(handle);
      return WaitHandle.OpenExistingResult.Success;
    }

    /// <summary>
    ///   Задает несигнальное состояние события, вызывая блокирование потоков.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если операция прошла успешно; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод <see cref="M:System.Threading.WaitHandle.Close" /> ранее вызывался для этого <see cref="T:System.Threading.EventWaitHandle" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Reset()
    {
      bool flag = Win32Native.ResetEvent(this.safeWaitHandle);
      if (!flag)
        __Error.WinIOError();
      return flag;
    }

    /// <summary>
    ///   Устанавливает сигнальное состояние события, что позволяет продолжить выполнение одному или нескольким ожидающим потокам.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если операция выполнена успешно; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод <see cref="M:System.Threading.WaitHandle.Close" /> ранее вызывался для этого <see cref="T:System.Threading.EventWaitHandle" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Set()
    {
      bool flag = Win32Native.SetEvent(this.safeWaitHandle);
      if (!flag)
        __Error.WinIOError();
      return flag;
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" />, представляющий параметры безопасности управления доступом для именованного системного события, представленного текущим объектом <see cref="T:System.Threading.EventWaitHandle" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" />, представляющий параметры безопасности управления доступом для именованного системного события.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Текущий объект <see cref="T:System.Threading.EventWaitHandle" /> представляет именованное системное событие, а пользователь не имеет <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ReadPermissions" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий объект <see cref="T:System.Threading.EventWaitHandle" /> представляет именованное системное событие и не был открыт с помощью <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ReadPermissions" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не поддерживается для Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод <see cref="M:System.Threading.WaitHandle.Close" /> ранее вызывался для этого <see cref="T:System.Threading.EventWaitHandle" />.
    /// </exception>
    [SecuritySafeCritical]
    public EventWaitHandleSecurity GetAccessControl()
    {
      return new EventWaitHandleSecurity(this.safeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>
    ///   Задает защиту управления доступом для именованного системного события.
    /// </summary>
    /// <param name="eventSecurity">
    ///   Объект <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" />, представляющий защиту управления доступом для применения к именованному системному событию.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="eventSecurity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь не имеет <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ChangePermissions" />.
    /// 
    ///   -или-
    /// 
    ///   Это событие не было открыто с помощью <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ChangePermissions" />.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Текущий объект <see cref="T:System.Threading.EventWaitHandle" /> не представляет именованное системное событие.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Метод <see cref="M:System.Threading.WaitHandle.Close" /> ранее вызывался для этого <see cref="T:System.Threading.EventWaitHandle" />.
    /// </exception>
    [SecuritySafeCritical]
    public void SetAccessControl(EventWaitHandleSecurity eventSecurity)
    {
      if (eventSecurity == null)
        throw new ArgumentNullException(nameof (eventSecurity));
      eventSecurity.Persist(this.safeWaitHandle);
    }
  }
}
