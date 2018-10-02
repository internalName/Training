// Decompiled with JetBrains decompiler
// Type: System.Threading.Mutex
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Примитив синхронизации, который также может использоваться в межпроцессной синхронизации.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class Mutex : WaitHandle
  {
    private static bool dummyBool;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Mutex" /> логическим значением, указывающим, должен ли вызывающий поток быть изначальным владельцем мьютекса, иметь строку, являющуюся именем мьютекса, и логическое значение, которое при возврате метода показывает, предоставлено ли вызывающему потоку изначальное владение мьютексом.
    /// </summary>
    /// <param name="initiallyOwned">
    ///   Значение <see langword="true" /> для предоставления вызывающему потоку изначального владения именованным системным мьютексом, если этот мьютекс создан данным вызовом; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="name">
    ///   Имя <see cref="T:System.Threading.Mutex" />.
    ///    Если значение равно <see langword="null" />, у объекта <see cref="T:System.Threading.Mutex" /> нет имени.
    /// </param>
    /// <param name="createdNew">
    ///   При возврате из метода содержит логическое значение <see langword="true" />, если был создан локальный мьютекс (то есть, если параметр <paramref name="name" /> имеет значение <see langword="null" /> или содержит пустую строку) или был создан именованный системный мьютекс; значение <see langword="false" />, если указанный именованный системный мьютекс уже существует.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованный мьютекс существует, имеет безопасность управления доступом, но пользователь не имеет прав <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">
    ///   Именованный мьютекс нельзя создать; вероятно, дескриптор ожидания другого типа имеет то же имя.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public Mutex(bool initiallyOwned, string name, out bool createdNew)
      : this(initiallyOwned, name, out createdNew, (MutexSecurity) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Mutex" /> логическим значением, указывающим, должен ли вызывающий поток быть изначальным владельцем мьютекса, иметь строку, являющуюся именем мьютекса, и логическое значение, которое при возврате метода показывает, предоставлено ли вызывающему потоку изначальное владение мьютексом, а также безопасность управления доступом для применения к именованному мьютексу.
    /// </summary>
    /// <param name="initiallyOwned">
    ///   Значение <see langword="true" /> для предоставления вызывающему потоку изначального владения именованным системным мьютексом, если этот мьютекс создан данным вызовом; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="name">
    ///   Имя системного мьютекса.
    ///    Если значение равно <see langword="null" />, у объекта <see cref="T:System.Threading.Mutex" /> нет имени.
    /// </param>
    /// <param name="createdNew">
    ///   При возврате из метода содержит логическое значение <see langword="true" />, если был создан локальный мьютекс (то есть, если параметр <paramref name="name" /> имеет значение <see langword="null" /> или содержит пустую строку) или был создан именованный системный мьютекс; значение <see langword="false" />, если указанный именованный системный мьютекс уже существует.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="mutexSecurity">
    ///   Объект <see cref="T:System.Security.AccessControl.MutexSecurity" />, представляющий безопасность управления доступом для применения к именованному системному мьютексу.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованный мьютекс существует, имеет безопасность управления доступом, но пользователь не имеет прав <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />.
    /// </exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">
    ///   Именованный мьютекс нельзя создать; вероятно, дескриптор ожидания другого типа имеет то же имя.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public unsafe Mutex(bool initiallyOwned, string name, out bool createdNew, MutexSecurity mutexSecurity)
    {
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      Win32Native.SECURITY_ATTRIBUTES securityAttributes = (Win32Native.SECURITY_ATTRIBUTES) null;
      if (mutexSecurity != null)
      {
        securityAttributes = new Win32Native.SECURITY_ATTRIBUTES();
        securityAttributes.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(securityAttributes);
        byte[] descriptorBinaryForm = mutexSecurity.GetSecurityDescriptorBinaryForm();
        byte* pDest = stackalloc byte[descriptorBinaryForm.Length];
        Buffer.Memcpy(pDest, 0, descriptorBinaryForm, 0, descriptorBinaryForm.Length);
        securityAttributes.pSecurityDescriptor = pDest;
      }
      this.CreateMutexWithGuaranteedCleanup(initiallyOwned, name, out createdNew, securityAttributes);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal Mutex(bool initiallyOwned, string name, out bool createdNew, Win32Native.SECURITY_ATTRIBUTES secAttrs)
    {
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      this.CreateMutexWithGuaranteedCleanup(initiallyOwned, name, out createdNew, secAttrs);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal void CreateMutexWithGuaranteedCleanup(bool initiallyOwned, string name, out bool createdNew, Win32Native.SECURITY_ATTRIBUTES secAttrs)
    {
      RuntimeHelpers.CleanupCode backoutCode = new RuntimeHelpers.CleanupCode(this.MutexCleanupCode);
      Mutex.MutexCleanupInfo cleanupInfo = new Mutex.MutexCleanupInfo((SafeWaitHandle) null, false);
      Mutex.MutexTryCodeHelper mutexTryCodeHelper = new Mutex.MutexTryCodeHelper(initiallyOwned, cleanupInfo, name, secAttrs, this);
      RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(new RuntimeHelpers.TryCode(mutexTryCodeHelper.MutexTryCode), backoutCode, (object) cleanupInfo);
      createdNew = mutexTryCodeHelper.m_newMutex;
    }

    [SecurityCritical]
    [PrePrepareMethod]
    private void MutexCleanupCode(object userData, bool exceptionThrown)
    {
      Mutex.MutexCleanupInfo mutexCleanupInfo = (Mutex.MutexCleanupInfo) userData;
      if (this.hasThreadAffinity)
        return;
      if (mutexCleanupInfo.mutexHandle != null && !mutexCleanupInfo.mutexHandle.IsInvalid)
      {
        if (mutexCleanupInfo.inCriticalRegion)
          Win32Native.ReleaseMutex(mutexCleanupInfo.mutexHandle);
        mutexCleanupInfo.mutexHandle.Dispose();
      }
      if (!mutexCleanupInfo.inCriticalRegion)
        return;
      Thread.EndCriticalRegion();
      Thread.EndThreadAffinity();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Mutex" /> логическим значением, указывающим, должен ли вызывающий поток быть изначальным владельцем мьютекса, а также иметь строку, являющуюся именем мьютекса.
    /// </summary>
    /// <param name="initiallyOwned">
    ///   Значение <see langword="true" /> для предоставления вызывающему потоку изначального владения именованным системным мьютексом, если этот мьютекс создан данным вызовом; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="name">
    ///   Имя <see cref="T:System.Threading.Mutex" />.
    ///    Если значение равно <see langword="null" />, у объекта <see cref="T:System.Threading.Mutex" /> нет имени.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованный мьютекс существует, имеет безопасность управления доступом, но пользователь не имеет прав <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">
    ///   Именованный мьютекс нельзя создать; вероятно, дескриптор ожидания другого типа имеет то же имя.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина значения параметра <paramref name="name" /> превышает 260 символов.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public Mutex(bool initiallyOwned, string name)
      : this(initiallyOwned, name, out Mutex.dummyBool)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Mutex" /> логическим значением, указывающим, должен ли вызывающий поток быть изначальным владельцем мьютекса.
    /// </summary>
    /// <param name="initiallyOwned">
    ///   Значение <see langword="true" /> для предоставления вызывающему потоку изначального владения мьютексом; в противном случае — <see langword="false" />.
    /// </param>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public Mutex(bool initiallyOwned)
      : this(initiallyOwned, (string) null, out Mutex.dummyBool)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Mutex" /> стандартными свойствами.
    /// </summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public Mutex()
      : this(false, (string) null, out Mutex.dummyBool)
    {
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private Mutex(SafeWaitHandle handle)
    {
      this.SetHandleInternal(handle);
      this.hasThreadAffinity = true;
    }

    /// <summary>
    ///   Открывает указанный именованный мьютекс, если он уже существует.
    /// </summary>
    /// <param name="name">Имя системного мьютекса для открытия.</param>
    /// <returns>
    ///   Объект, представляющий именованный системный мьютекс.
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
    ///   Именованный мьютекс не существует.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованный мьютекс существует, но пользователь не имеет прав доступа, необходимых для его использования.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static Mutex OpenExisting(string name)
    {
      return Mutex.OpenExisting(name, MutexRights.Modify | MutexRights.Synchronize);
    }

    /// <summary>
    ///   Открывает указанный именованный мьютекс, если он уже существует, с требуемыми правами доступа.
    /// </summary>
    /// <param name="name">Имя системного мьютекса для открытия.</param>
    /// <param name="rights">
    ///   Битовая комбинация значений перечисления, которые определяют желаемые права доступа.
    /// </param>
    /// <returns>
    ///   Объект, представляющий именованный системный мьютекс.
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
    ///   Именованный мьютекс не существует.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка Win32.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Именованный мьютекс существует, но пользователь не имеет требуемый доступ безопасности.
    /// </exception>
    [SecurityCritical]
    public static Mutex OpenExisting(string name, MutexRights rights)
    {
      Mutex result;
      switch (Mutex.OpenExistingWorker(name, rights, out result))
      {
        case WaitHandle.OpenExistingResult.NameNotFound:
          throw new WaitHandleCannotBeOpenedException();
        case WaitHandle.OpenExistingResult.PathNotFound:
          __Error.WinIOError(3, name);
          return result;
        case WaitHandle.OpenExistingResult.NameInvalid:
          throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        default:
          return result;
      }
    }

    /// <summary>
    ///   Открывает указанный именованный мьютекс, если он уже существует, и возвращает значение, указывающее, успешно ли выполнена операция.
    /// </summary>
    /// <param name="name">Имя системного мьютекса для открытия.</param>
    /// <param name="result">
    ///   Когда выполнение этого метода завершается, содержит объект <see cref="T:System.Threading.Mutex" />, представляющий именованный мьютекс, если вызов завершился успешно, или значение <see langword="null" />, если произошел сбой вызова.
    ///    Этот параметр обрабатывается как неинициализированный.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если именованный мьютекс был успешно открыт; в противном случае — значение <see langword="false" />.
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
    ///   Именованный мьютекс существует, но у пользователя нет прав доступа, необходимых для его использования.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static bool TryOpenExisting(string name, out Mutex result)
    {
      return Mutex.OpenExistingWorker(name, MutexRights.Modify | MutexRights.Synchronize, out result) == WaitHandle.OpenExistingResult.Success;
    }

    /// <summary>
    ///   Открывает указанный именованный мьютекс, если он уже существует, с требуемыми правами доступа, и возвращает значение, указывающее, успешно ли выполнена операция.
    /// </summary>
    /// <param name="name">Имя системного мьютекса для открытия.</param>
    /// <param name="rights">
    ///   Битовая комбинация значений перечисления, которые определяют желаемые права доступа.
    /// </param>
    /// <param name="result">
    ///   Когда выполнение этого метода завершается, содержит объект <see cref="T:System.Threading.Mutex" />, представляющий именованный мьютекс, если вызов завершился успешно, или значение <see langword="null" />, если произошел сбой вызова.
    ///    Этот параметр обрабатывается как неинициализированный.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если именованный мьютекс был успешно открыт; в противном случае — значение <see langword="false" />.
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
    ///   Именованный мьютекс существует, но у пользователя нет прав доступа, необходимых для его использования.
    /// </exception>
    [SecurityCritical]
    public static bool TryOpenExisting(string name, MutexRights rights, out Mutex result)
    {
      return Mutex.OpenExistingWorker(name, rights, out result) == WaitHandle.OpenExistingResult.Success;
    }

    [SecurityCritical]
    private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, MutexRights rights, out Mutex result)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name), Environment.GetResourceString("ArgumentNull_WithParamName"));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      result = (Mutex) null;
      SafeWaitHandle handle = Win32Native.OpenMutex((int) rights, false, name);
      if (handle.IsInvalid)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (2 == lastWin32Error || 123 == lastWin32Error)
          return WaitHandle.OpenExistingResult.NameNotFound;
        if (3 == lastWin32Error)
          return WaitHandle.OpenExistingResult.PathNotFound;
        if (name != null && name.Length != 0 && 6 == lastWin32Error)
          return WaitHandle.OpenExistingResult.NameInvalid;
        __Error.WinIOError(lastWin32Error, name);
      }
      result = new Mutex(handle);
      return WaitHandle.OpenExistingResult.Success;
    }

    /// <summary>
    ///   Освобождает объект <see cref="T:System.Threading.Mutex" /> один раз.
    /// </summary>
    /// <exception cref="T:System.ApplicationException">
    ///   Вызывающий поток не является владельцем мьютекса.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий экземпляр уже удален.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public void ReleaseMutex()
    {
      if (!Win32Native.ReleaseMutex(this.safeWaitHandle))
        throw new ApplicationException(Environment.GetResourceString("Arg_SynchronizationLockException"));
      Thread.EndCriticalRegion();
      Thread.EndThreadAffinity();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static int CreateMutexHandle(bool initiallyOwned, string name, Win32Native.SECURITY_ATTRIBUTES securityAttribute, out SafeWaitHandle mutexHandle)
    {
      bool flag = false;
label_1:
      mutexHandle = Win32Native.CreateMutex(securityAttribute, initiallyOwned, name);
      int num = Marshal.GetLastWin32Error();
      if (mutexHandle.IsInvalid && num == 5)
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          try
          {
          }
          finally
          {
            Thread.BeginThreadAffinity();
            flag = true;
          }
          mutexHandle = Win32Native.OpenMutex(1048577, false, name);
          num = mutexHandle.IsInvalid ? Marshal.GetLastWin32Error() : 183;
        }
        finally
        {
          if (flag)
            Thread.EndThreadAffinity();
        }
        switch (num)
        {
          case 0:
            num = 183;
            break;
          case 2:
            goto label_1;
        }
      }
      return num;
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Security.AccessControl.MutexSecurity" />, представляющий безопасность управления доступом для именованного мьютекса.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.MutexSecurity" />, представляющий безопасность управления доступом для именованного мьютекса.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Текущий <see cref="T:System.Threading.Mutex" /> объект представляет именованный системный мьютекс, но пользователь не имеет <see cref="F:System.Security.AccessControl.MutexRights.ReadPermissions" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий <see cref="T:System.Threading.Mutex" /> объект представляет именованный системный мьютекс и не был открыт с <see cref="F:System.Security.AccessControl.MutexRights.ReadPermissions" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не поддерживается для Windows 98 или Windows Millennium Edition.
    /// </exception>
    [SecuritySafeCritical]
    public MutexSecurity GetAccessControl()
    {
      return new MutexSecurity(this.safeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>
    ///   Задает безопасность управления доступом для именованного системного мьютекса.
    /// </summary>
    /// <param name="mutexSecurity">
    ///   Объект <see cref="T:System.Security.AccessControl.MutexSecurity" />, представляющий безопасность управления доступом для применения к именованному системному мьютексу.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mutexSecurity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь не имеет <see cref="F:System.Security.AccessControl.MutexRights.ChangePermissions" />.
    /// 
    ///   -или-
    /// 
    ///   Объект взаимного исключения не был открыт с <see cref="F:System.Security.AccessControl.MutexRights.ChangePermissions" />.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Текущий <see cref="T:System.Threading.Mutex" /> объект не представляет именованный системный мьютекс.
    /// </exception>
    [SecuritySafeCritical]
    public void SetAccessControl(MutexSecurity mutexSecurity)
    {
      if (mutexSecurity == null)
        throw new ArgumentNullException(nameof (mutexSecurity));
      mutexSecurity.Persist(this.safeWaitHandle);
    }

    internal class MutexTryCodeHelper
    {
      private bool m_initiallyOwned;
      private Mutex.MutexCleanupInfo m_cleanupInfo;
      internal bool m_newMutex;
      private string m_name;
      [SecurityCritical]
      private Win32Native.SECURITY_ATTRIBUTES m_secAttrs;
      private Mutex m_mutex;

      [SecurityCritical]
      [PrePrepareMethod]
      internal MutexTryCodeHelper(bool initiallyOwned, Mutex.MutexCleanupInfo cleanupInfo, string name, Win32Native.SECURITY_ATTRIBUTES secAttrs, Mutex mutex)
      {
        this.m_initiallyOwned = initiallyOwned;
        this.m_cleanupInfo = cleanupInfo;
        this.m_name = name;
        this.m_secAttrs = secAttrs;
        this.m_mutex = mutex;
      }

      [SecurityCritical]
      [PrePrepareMethod]
      internal void MutexTryCode(object userData)
      {
        SafeWaitHandle mutexHandle = (SafeWaitHandle) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          if (this.m_initiallyOwned)
          {
            this.m_cleanupInfo.inCriticalRegion = true;
            Thread.BeginThreadAffinity();
            Thread.BeginCriticalRegion();
          }
        }
        int errorCode = 0;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          errorCode = Mutex.CreateMutexHandle(this.m_initiallyOwned, this.m_name, this.m_secAttrs, out mutexHandle);
        }
        if (mutexHandle.IsInvalid)
        {
          mutexHandle.SetHandleAsInvalid();
          if (this.m_name != null && this.m_name.Length != 0 && 6 == errorCode)
            throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) this.m_name));
          __Error.WinIOError(errorCode, this.m_name);
        }
        this.m_newMutex = errorCode != 183;
        this.m_mutex.SetHandleInternal(mutexHandle);
        this.m_mutex.hasThreadAffinity = true;
      }
    }

    internal class MutexCleanupInfo
    {
      [SecurityCritical]
      internal SafeWaitHandle mutexHandle;
      internal bool inCriticalRegion;

      [SecurityCritical]
      internal MutexCleanupInfo(SafeWaitHandle mutexHandle, bool inCriticalRegion)
      {
        this.mutexHandle = mutexHandle;
        this.inCriticalRegion = inCriticalRegion;
      }
    }
  }
}
