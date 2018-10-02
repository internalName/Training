// Decompiled with JetBrains decompiler
// Type: System.Environment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Предоставляет сведения о текущей среде и платформе, а также необходимые для управления ими средства.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Environment
  {
    private const int MaxEnvVariableValueLength = 32767;
    private const int MaxSystemEnvVariableLength = 1024;
    private const int MaxUserEnvVariableLength = 255;
    private static volatile Environment.ResourceHelper m_resHelper;
    private const int MaxMachineNameLength = 256;
    private static object s_InternalSyncObject;
    private static volatile OperatingSystem m_os;
    private static volatile bool s_IsWindows8OrAbove;
    private static volatile bool s_CheckedOSWin8OrAbove;
    private static volatile bool s_WinRTSupported;
    private static volatile bool s_CheckedWinRT;
    private static volatile IntPtr processWinStation;
    private static volatile bool isUserNonInteractive;

    private static object InternalSyncObject
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)] get
      {
        if (Environment.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref Environment.s_InternalSyncObject, obj, (object) null);
        }
        return Environment.s_InternalSyncObject;
      }
    }

    /// <summary>
    ///   Возвращает время, истекшее с момента загрузки системы (в миллисекундах).
    /// </summary>
    /// <returns>
    ///   32-битовое целое число со знаком, содержащее время, истекшее с момента с последней загрузки системы (в миллисекундах).
    /// </returns>
    [__DynamicallyInvokable]
    public static extern int TickCount { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void _Exit(int exitCode);

    /// <summary>
    ///   Завершает этот процесс и возвращает код выхода операционной системе.
    /// </summary>
    /// <param name="exitCode">
    ///   Код выхода, возвращаемый операционной системе.
    ///    Чтобы указать, что процесс прошел успешно, следует использовать 0 (ноль).
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет достаточных разрешений на выполнение этой функции.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static void Exit(int exitCode)
    {
      Environment._Exit(exitCode);
    }

    /// <summary>Возвращает или задает код выхода из процесса.</summary>
    /// <returns>
    ///   32-битовое целое число со знаком, содержащее код выхода.
    ///    Значение по умолчанию 0 (нуль), что соответствует успешно выполненному процессу.
    /// </returns>
    public static extern int ExitCode { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] get; [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] set; }

    /// <summary>
    ///   Завершает процесс сразу после записи сообщения в журнал событий приложений Windows, после чего включает сообщение в отчет об ошибках, отправляемый в корпорацию Майкрософт.
    /// </summary>
    /// <param name="message">
    ///   Сообщение, в котором объясняется причина завершения процесса или содержится значение <see langword="null" />, если объяснение отсутствует.
    /// </param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void FailFast(string message);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void FailFast(string message, uint exitCode);

    /// <summary>
    ///   Завершает процесс сразу после записи сообщения в журнал событий приложений Windows, после чего включает сообщение и сведения об исключении в отчет об ошибках, отправляемый в корпорацию Майкрософт.
    /// </summary>
    /// <param name="message">
    ///   Сообщение, в котором объясняется причина завершения процесса или содержится значение <see langword="null" />, если объяснение отсутствует.
    /// </param>
    /// <param name="exception">
    ///   Исключение, представляющее ошибку, вызвавшую завершение процесса.
    ///    Обычно это исключение в блоке <see langword="catch" />.
    /// </param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void FailFast(string message, Exception exception);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void TriggerCodeContractFailure(ContractFailureKind failureKind, string message, string condition, string exceptionAsString);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetIsCLRHosted();

    internal static bool IsCLRHosted
    {
      [SecuritySafeCritical] get
      {
        return Environment.GetIsCLRHosted();
      }
    }

    /// <summary>Возвращает командную строку для данного процесса.</summary>
    /// <returns>Строка, содержащая аргументы командной строки.</returns>
    public static string CommandLine
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Demand();
        string s = (string) null;
        Environment.GetCommandLine(JitHelpers.GetStringHandleOnStack(ref s));
        return s;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetCommandLine(StringHandleOnStack retString);

    /// <summary>
    ///   Возвращает или задает полный путь к текущей рабочей папке.
    /// </summary>
    /// <returns>Строка, содержащая путь к каталогу.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Попытка задать пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка установить значение <see langword="null." />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Попытка установить локальный путь, который не удается найти.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует соответствующее разрешение.
    /// </exception>
    public static string CurrentDirectory
    {
      get
      {
        return Directory.GetCurrentDirectory();
      }
      set
      {
        Directory.SetCurrentDirectory(value);
      }
    }

    /// <summary>Возвращает полный путь к системному каталогу.</summary>
    /// <returns>Строка, содержащая путь к каталогу.</returns>
    public static string SystemDirectory
    {
      [SecuritySafeCritical] get
      {
        StringBuilder sb = new StringBuilder(260);
        if (Win32Native.GetSystemDirectory(sb, 260) == 0)
          __Error.WinIOError();
        string fullPath = sb.ToString();
        FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, fullPath, false, true);
        return fullPath;
      }
    }

    internal static string InternalWindowsDirectory
    {
      [SecurityCritical] get
      {
        StringBuilder sb = new StringBuilder(260);
        if (Win32Native.GetWindowsDirectory(sb, 260) == 0)
          __Error.WinIOError();
        return sb.ToString();
      }
    }

    /// <summary>
    ///   Замещает имя каждой переменной среды, внедренной в указанную строку, строчным эквивалентом значения переменной, а затем возвращает результирующую строку.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая либо не содержащая имена переменных среды.
    ///    Каждая переменная среды с двух сторон окружена знаками процента (%).
    /// </param>
    /// <returns>
    ///   Строка, в которой каждая переменная среды замещена ее значением.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ExpandEnvironmentVariables(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0 || AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        return name;
      int num1 = 100;
      StringBuilder lpDst = new StringBuilder(num1);
      bool flag1 = CodeAccessSecurityEngine.QuickCheckForAllDemands();
      string[] strArray = name.Split('%');
      StringBuilder stringBuilder = flag1 ? (StringBuilder) null : new StringBuilder();
      bool flag2 = false;
      for (int index = 1; index < strArray.Length - 1; ++index)
      {
        if (strArray[index].Length == 0 | flag2)
        {
          flag2 = false;
        }
        else
        {
          lpDst.Length = 0;
          string lpSrc = "%" + strArray[index] + "%";
          int num2 = Win32Native.ExpandEnvironmentStrings(lpSrc, lpDst, num1);
          if (num2 == 0)
            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
          while (num2 > num1)
          {
            num1 = num2;
            lpDst.Capacity = num1;
            lpDst.Length = 0;
            num2 = Win32Native.ExpandEnvironmentStrings(lpSrc, lpDst, num1);
            if (num2 == 0)
              Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
          }
          if (!flag1)
          {
            flag2 = lpDst.ToString() != lpSrc;
            if (flag2)
            {
              stringBuilder.Append(strArray[index]);
              stringBuilder.Append(';');
            }
          }
        }
      }
      if (!flag1)
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder.ToString()).Demand();
      lpDst.Length = 0;
      int num3 = Win32Native.ExpandEnvironmentStrings(name, lpDst, num1);
      if (num3 == 0)
        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
      while (num3 > num1)
      {
        num1 = num3;
        lpDst.Capacity = num1;
        lpDst.Length = 0;
        num3 = Win32Native.ExpandEnvironmentStrings(name, lpDst, num1);
        if (num3 == 0)
          Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
      }
      return lpDst.ToString();
    }

    /// <summary>
    ///   Возвращает имя NetBIOS данного локального компьютера.
    /// </summary>
    /// <returns>Строка, содержащая имя данного компьютера.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Не удается получить имя этого компьютера.
    /// </exception>
    public static string MachineName
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, "COMPUTERNAME").Demand();
        StringBuilder nameBuffer = new StringBuilder(256);
        int bufferSize = 256;
        if (Win32Native.GetComputerName(nameBuffer, ref bufferSize) == 0)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ComputerName"));
        return nameBuffer.ToString();
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetProcessorCount();

    /// <summary>Возвращает число процессоров на текущем компьютере.</summary>
    /// <returns>
    ///   32-битовое целое число со знаком, которое задает количество процессоров на текущем компьютере.
    ///    Значение по умолчанию отсутствует.
    ///    Если текущий компьютер содержит несколько групп процессоров, данное свойство возвращает число логических процессоров, доступных для использования средой CLR.
    /// </returns>
    [__DynamicallyInvokable]
    public static int ProcessorCount
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return Environment.GetProcessorCount();
      }
    }

    /// <summary>
    ///   Возвращает количество байтов на странице памяти операционной системы.
    /// </summary>
    /// <returns>Количество в байтах в странице памяти системы.</returns>
    public static int SystemPageSize
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(PermissionState.Unrestricted).Demand();
        Win32Native.SYSTEM_INFO lpSystemInfo = new Win32Native.SYSTEM_INFO();
        Win32Native.GetSystemInfo(ref lpSystemInfo);
        return lpSystemInfo.dwPageSize;
      }
    }

    /// <summary>
    ///   Возвращает строковый массив, содержащий аргументы командной строки для текущего процесса.
    /// </summary>
    /// <returns>
    ///   Массив строк, каждый элемент которого содержит аргумент командной строки.
    ///    Первым элементом является имя исполняемого файла. Последующие элементы, если они существуют, содержат аргументы командной строки.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Система не поддерживает аргументы командной строки.
    /// </exception>
    [SecuritySafeCritical]
    public static string[] GetCommandLineArgs()
    {
      new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Demand();
      return Environment.GetCommandLineArgsNative();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern string[] GetCommandLineArgsNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string nativeGetEnvironmentVariable(string variable);

    /// <summary>
    ///   Возвращает из текущего процесса значение переменной среды.
    /// </summary>
    /// <param name="variable">Имя переменной среды.</param>
    /// <returns>
    ///   Значение переменной среды, заданное параметром <paramref name="variable" /> или значение <see langword="null" />, если переменная среды не найдена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="variable" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение для выполнения этой операции.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string GetEnvironmentVariable(string variable)
    {
      if (variable == null)
        throw new ArgumentNullException(nameof (variable));
      if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        return (string) null;
      new EnvironmentPermission(EnvironmentPermissionAccess.Read, variable).Demand();
      StringBuilder stringBuilder = StringBuilderCache.Acquire(128);
      int environmentVariable = Win32Native.GetEnvironmentVariable(variable, stringBuilder, stringBuilder.Capacity);
      if (environmentVariable == 0 && Marshal.GetLastWin32Error() == 203)
      {
        StringBuilderCache.Release(stringBuilder);
        return (string) null;
      }
      for (; environmentVariable > stringBuilder.Capacity; environmentVariable = Win32Native.GetEnvironmentVariable(variable, stringBuilder, stringBuilder.Capacity))
      {
        stringBuilder.Capacity = environmentVariable;
        stringBuilder.Length = 0;
      }
      return StringBuilderCache.GetStringAndRelease(stringBuilder);
    }

    /// <summary>
    ///   Возвращает из текущего процесса или раздела реестра операционной системы Windows значение переменной среды для текущего пользователя или локального компьютера.
    /// </summary>
    /// <param name="variable">Имя переменной среды.</param>
    /// <param name="target">
    ///   Одно из значений <see cref="T:System.EnvironmentVariableTarget" />.
    /// </param>
    /// <returns>
    ///   Значение переменной среды, заданное параметрами <paramref name="variable" /> и <paramref name="target" /> или значение <see langword="null" />, если переменная среды не найдена.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="variable" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="target" /> не является допустимым значением <see cref="T:System.EnvironmentVariableTarget" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение для выполнения этой операции.
    /// </exception>
    [SecuritySafeCritical]
    public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
    {
      if (variable == null)
        throw new ArgumentNullException(nameof (variable));
      if (target == EnvironmentVariableTarget.Process)
        return Environment.GetEnvironmentVariable(variable);
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      if (target == EnvironmentVariableTarget.Machine)
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", false))
        {
          if (registryKey == null)
            return (string) null;
          return registryKey.GetValue(variable) as string;
        }
      }
      else if (target == EnvironmentVariableTarget.User)
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(nameof (Environment), false))
        {
          if (registryKey == null)
            return (string) null;
          return registryKey.GetValue(variable) as string;
        }
      }
      else
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) target));
    }

    [SecurityCritical]
    private static unsafe char[] GetEnvironmentCharArray()
    {
      char[] chArray = (char[]) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        char* chPtr1 = (char*) null;
        try
        {
          chPtr1 = Win32Native.GetEnvironmentStrings();
          if ((IntPtr) chPtr1 == IntPtr.Zero)
            throw new OutOfMemoryException();
          char* chPtr2 = chPtr1;
          while (*chPtr2 != char.MinValue || *(ushort*) ((IntPtr) chPtr2 + 2) != (ushort) 0)
            chPtr2 += 2;
          int charCount = (int) (chPtr2 - chPtr1 + 1L);
          chArray = new char[charCount];
          fixed (char* dmem = chArray)
            string.wstrcpy(dmem, chPtr1, charCount);
        }
        finally
        {
          if ((IntPtr) chPtr1 != IntPtr.Zero)
            Win32Native.FreeEnvironmentStrings(chPtr1);
        }
      }
      return chArray;
    }

    /// <summary>
    ///   Возвращает из текущего процесса имена всех переменных среды и их значения.
    /// </summary>
    /// <returns>
    ///   Словарь, в котором содержатся имена всех переменных среды и их значения; в противном случае, если переменные среды не найдены, — пустой словарь.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение для выполнения этой операции.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти буфера.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static IDictionary GetEnvironmentVariables()
    {
      if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        return (IDictionary) new Hashtable(0);
      bool flag1 = CodeAccessSecurityEngine.QuickCheckForAllDemands();
      StringBuilder stringBuilder = flag1 ? (StringBuilder) null : new StringBuilder();
      bool flag2 = true;
      char[] environmentCharArray = Environment.GetEnvironmentCharArray();
      Hashtable hashtable = new Hashtable(20);
      for (int index = 0; index < environmentCharArray.Length; ++index)
      {
        int startIndex1 = index;
        while (environmentCharArray[index] != '=' && environmentCharArray[index] != char.MinValue)
          ++index;
        if (environmentCharArray[index] != char.MinValue)
        {
          if (index - startIndex1 == 0)
          {
            while (environmentCharArray[index] != char.MinValue)
              ++index;
          }
          else
          {
            string str1 = new string(environmentCharArray, startIndex1, index - startIndex1);
            ++index;
            int startIndex2 = index;
            while (environmentCharArray[index] != char.MinValue)
              ++index;
            string str2 = new string(environmentCharArray, startIndex2, index - startIndex2);
            hashtable[(object) str1] = (object) str2;
            if (!flag1)
            {
              if (flag2)
                flag2 = false;
              else
                stringBuilder.Append(';');
              stringBuilder.Append(str1);
            }
          }
        }
      }
      if (!flag1)
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder.ToString()).Demand();
      return (IDictionary) hashtable;
    }

    internal static IDictionary GetRegistryKeyNameValuePairs(RegistryKey registryKey)
    {
      Hashtable hashtable = new Hashtable(20);
      if (registryKey != null)
      {
        foreach (string valueName in registryKey.GetValueNames())
        {
          string str = registryKey.GetValue(valueName, (object) "").ToString();
          hashtable.Add((object) valueName, (object) str);
        }
      }
      return (IDictionary) hashtable;
    }

    /// <summary>
    ///   Возвращает из текущего процесса или раздела реестра операционной системы Windows имена и значения всех переменных среды для текущего пользователя или локального компьютера.
    /// </summary>
    /// <param name="target">
    ///   Одно из значений <see cref="T:System.EnvironmentVariableTarget" />.
    /// </param>
    /// <returns>
    ///   Словарь, в котором содержатся имена всех переменных среды и их значения, извлеченные из источника, заданного параметром <paramref name="target" />; в противном случае, если переменные среды не найдены, — пустой словарь.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение для выполнения этой операции для указанного значения <paramref name="target" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="target" /> содержит недопустимое значение.
    /// </exception>
    [SecuritySafeCritical]
    public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
    {
      if (target == EnvironmentVariableTarget.Process)
        return Environment.GetEnvironmentVariables();
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      if (target == EnvironmentVariableTarget.Machine)
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", false))
          return Environment.GetRegistryKeyNameValuePairs(registryKey);
      }
      else if (target == EnvironmentVariableTarget.User)
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(nameof (Environment), false))
          return Environment.GetRegistryKeyNameValuePairs(registryKey);
      }
      else
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) target));
    }

    /// <summary>
    ///   Создает, изменяет или удаляет переменную среды, хранящуюся в текущем процессе.
    /// </summary>
    /// <param name="variable">Имя переменной среды.</param>
    /// <param name="value">
    ///   Значение, которое необходимо присвоить параметру <paramref name="variable" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="variable" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="variable" /> содержит строку нулевой длины, начальный шестнадцатеричный символ нуля (0x00) или знак равенства ("=").
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="variable" /> или <paramref name="value" /> больше или равна 32 767 символам.
    /// 
    ///   -или-
    /// 
    ///   Во время выполнения этой операции произошла ошибка.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение для выполнения этой операции.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void SetEnvironmentVariable(string variable, string value)
    {
      Environment.CheckEnvironmentVariableName(variable);
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      if (string.IsNullOrEmpty(value) || value[0] == char.MinValue)
        value = (string) null;
      else if (value.Length >= (int) short.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
      if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        throw new PlatformNotSupportedException();
      if (Win32Native.SetEnvironmentVariable(variable, value))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      switch (lastWin32Error)
      {
        case 203:
          break;
        case 206:
          throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
        default:
          throw new ArgumentException(Win32Native.GetMessage(lastWin32Error));
      }
    }

    private static void CheckEnvironmentVariableName(string variable)
    {
      if (variable == null)
        throw new ArgumentNullException(nameof (variable));
      if (variable.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), nameof (variable));
      if (variable[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_StringFirstCharIsZero"), nameof (variable));
      if (variable.Length >= (int) short.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
      if (variable.IndexOf('=') != -1)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalEnvVarName"));
    }

    /// <summary>
    ///   Создает, изменяет или удаляет переменную среды, хранящуюся в текущем процессе или разделе реестра операционной системы Windows, зарезервированном для текущего пользователя или локального компьютера.
    /// </summary>
    /// <param name="variable">Имя переменной среды.</param>
    /// <param name="value">
    ///   Значение, которое необходимо присвоить параметру <paramref name="variable" />.
    /// </param>
    /// <param name="target">
    ///   Одно из значений перечисления, указывающее местоположение переменной среды.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="variable" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="variable" /> содержит строку нулевой длины, начальный шестнадцатеричный символ нуля (0x00) или знак равенства ("=").
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="variable" /> составляет больше или ровно 32 767 символов.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="target" /> не является элементом перечисления <see cref="T:System.EnvironmentVariableTarget" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="target" /> — <see cref="F:System.EnvironmentVariableTarget.Machine" /> или <see cref="F:System.EnvironmentVariableTarget.User" />, а длина параметра <paramref name="variable" /> больше или равна 255.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="target" /> — <see cref="F:System.EnvironmentVariableTarget.Process" />, а длина параметра <paramref name="value" /> больше или равна 32 767 символам.
    /// 
    ///   -или-
    /// 
    ///   Во время выполнения этой операции произошла ошибка.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение для выполнения этой операции.
    /// </exception>
    [SecuritySafeCritical]
    public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
    {
      if (target == EnvironmentVariableTarget.Process)
      {
        Environment.SetEnvironmentVariable(variable, value);
      }
      else
      {
        Environment.CheckEnvironmentVariableName(variable);
        if (variable.Length >= 1024)
          throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarName"));
        new EnvironmentPermission(PermissionState.Unrestricted).Demand();
        if (string.IsNullOrEmpty(value) || value[0] == char.MinValue)
          value = (string) null;
        switch (target)
        {
          case EnvironmentVariableTarget.User:
            if (variable.Length >= (int) byte.MaxValue)
              throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(nameof (Environment), true))
            {
              if (registryKey != null)
              {
                if (value == null)
                {
                  registryKey.DeleteValue(variable, false);
                  break;
                }
                registryKey.SetValue(variable, (object) value);
                break;
              }
              break;
            }
          case EnvironmentVariableTarget.Machine:
            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
            {
              if (registryKey != null)
              {
                if (value == null)
                {
                  registryKey.DeleteValue(variable, false);
                  break;
                }
                registryKey.SetValue(variable, (object) value);
                break;
              }
              break;
            }
          default:
            throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) target));
        }
        int num = Win32Native.SendMessageTimeout(new IntPtr((int) ushort.MaxValue), 26, IntPtr.Zero, nameof (Environment), 0U, 1000U, IntPtr.Zero) == IntPtr.Zero ? 1 : 0;
      }
    }

    /// <summary>
    ///   Возвращает массив строк, содержащий имена логических дисков текущего компьютера.
    /// </summary>
    /// <returns>
    ///   Массив строк, в каждом элементе которого содержится имя логического диска.
    ///    Например, если первым логическим диском является жесткий диск компьютера, первым возвращаемым элементом будет "C:\".
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствуют необходимые разрешения.
    /// </exception>
    [SecuritySafeCritical]
    public static string[] GetLogicalDrives()
    {
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      int logicalDrives = Win32Native.GetLogicalDrives();
      if (logicalDrives == 0)
        __Error.WinIOError();
      uint num1 = (uint) logicalDrives;
      int length = 0;
      while (num1 != 0U)
      {
        if (((int) num1 & 1) != 0)
          ++length;
        num1 >>= 1;
      }
      string[] strArray = new string[length];
      char[] chArray = new char[3]{ 'A', ':', '\\' };
      uint num2 = (uint) logicalDrives;
      int num3 = 0;
      while (num2 != 0U)
      {
        if (((int) num2 & 1) != 0)
          strArray[num3++] = new string(chArray);
        num2 >>= 1;
        ++chArray[0];
      }
      return strArray;
    }

    /// <summary>
    ///   Возвращает строку, обозначающую в данной среде начало новой строки.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая "\r\n" для платформ, отличных от Unix, или строка, содержащая "\n" для платформ Unix.
    /// </returns>
    [__DynamicallyInvokable]
    public static string NewLine
    {
      [__DynamicallyInvokable] get
      {
        return "\r\n";
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Version" />, который описывает основной и дополнительный номера, а также номер построения и редакции среды CLR.
    /// </summary>
    /// <returns>Объект, содержащий версию среды CLR.</returns>
    public static Version Version
    {
      get
      {
        return new Version(4, 0, 30319, 42000);
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern long GetWorkingSet();

    /// <summary>
    ///   Возвращает объем физической памяти, сопоставленной контексту процесса.
    /// </summary>
    /// <returns>
    ///   Целое 64-битовое число со знаком, содержащее число байтов физической памяти, сопоставленное контексту процесса.
    /// </returns>
    public static long WorkingSet
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(PermissionState.Unrestricted).Demand();
        return Environment.GetWorkingSet();
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.OperatingSystem" />, который содержит идентификатор текущей платформы и номер версии.
    /// </summary>
    /// <returns>
    ///   Объект, который содержит идентификатор платформы и номер версии.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это свойство не смогло получить версию системы.
    /// 
    ///   -или-
    /// 
    ///   Полученный идентификатор платформы не является членом <see cref="T:System.PlatformID" />.
    /// </exception>
    public static OperatingSystem OSVersion
    {
      [SecuritySafeCritical] get
      {
        if (Environment.m_os == null)
        {
          Win32Native.OSVERSIONINFO osVer1 = new Win32Native.OSVERSIONINFO();
          if (!Environment.GetVersion(osVer1))
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GetVersion"));
          Win32Native.OSVERSIONINFOEX osVer2 = new Win32Native.OSVERSIONINFOEX();
          if (!Environment.GetVersionEx(osVer2))
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GetVersion"));
          Environment.m_os = new OperatingSystem(PlatformID.Win32NT, new Version(osVer1.MajorVersion, osVer1.MinorVersion, osVer1.BuildNumber, (int) osVer2.ServicePackMajor << 16 | (int) osVer2.ServicePackMinor), osVer1.CSDVersion);
        }
        return Environment.m_os;
      }
    }

    internal static bool IsWindows8OrAbove
    {
      get
      {
        if (!Environment.s_CheckedOSWin8OrAbove)
        {
          OperatingSystem osVersion = Environment.OSVersion;
          Environment.s_IsWindows8OrAbove = osVersion.Platform == PlatformID.Win32NT && (osVersion.Version.Major == 6 && osVersion.Version.Minor >= 2 || osVersion.Version.Major > 6);
          Environment.s_CheckedOSWin8OrAbove = true;
        }
        return Environment.s_IsWindows8OrAbove;
      }
    }

    internal static bool IsWinRTSupported
    {
      [SecuritySafeCritical] get
      {
        if (!Environment.s_CheckedWinRT)
        {
          Environment.s_WinRTSupported = Environment.WinRTSupported();
          Environment.s_CheckedWinRT = true;
        }
        return Environment.s_WinRTSupported;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool WinRTSupported();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool GetVersion(Win32Native.OSVERSIONINFO osVer);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool GetVersionEx(Win32Native.OSVERSIONINFOEX osVer);

    /// <summary>Возвращает текущие сведения о трассировке стека.</summary>
    /// <returns>
    ///   Строка, содержащая сведения о трассировке стека.
    ///    Это значение может быть равно <see cref="F:System.String.Empty" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string StackTrace
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        new EnvironmentPermission(PermissionState.Unrestricted).Demand();
        return Environment.GetStackTrace((Exception) null, true);
      }
    }

    internal static string GetStackTrace(Exception e, bool needFileInfo)
    {
      return (e != null ? new System.Diagnostics.StackTrace(e, needFileInfo) : new System.Diagnostics.StackTrace(needFileInfo)).ToString(System.Diagnostics.StackTrace.TraceFormat.Normal);
    }

    [SecuritySafeCritical]
    private static void InitResourceHelper()
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(Environment.InternalSyncObject, ref lockTaken);
        if (Environment.m_resHelper != null)
          return;
        Environment.ResourceHelper resourceHelper = new Environment.ResourceHelper("mscorlib");
        Thread.MemoryBarrier();
        Environment.m_resHelper = resourceHelper;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(Environment.InternalSyncObject);
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetResourceFromDefault(string key);

    internal static string GetResourceStringLocal(string key)
    {
      if (Environment.m_resHelper == null)
        Environment.InitResourceHelper();
      return Environment.m_resHelper.GetResourceString(key);
    }

    [SecuritySafeCritical]
    internal static string GetResourceString(string key)
    {
      return Environment.GetResourceFromDefault(key);
    }

    [SecuritySafeCritical]
    internal static string GetResourceString(string key, params object[] values)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString(key), values);
    }

    internal static string GetRuntimeResourceString(string key)
    {
      return Environment.GetResourceString(key);
    }

    internal static string GetRuntimeResourceString(string key, params object[] values)
    {
      return Environment.GetResourceString(key, values);
    }

    /// <summary>
    ///   Определяет, является ли текущий процесс 64-разрядным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если процесс является 64-разрядным; в противном случае —значение <see langword="false" />.
    /// </returns>
    public static bool Is64BitProcess
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Определяет, является ли текущая операционная система 64-разрядной.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если операционная система является 64-разрядной; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool Is64BitOperatingSystem
    {
      [SecuritySafeCritical] get
      {
        bool isWow64;
        return ((!Win32Native.DoesWin32MethodExist("kernel32.dll", "IsWow64Process") ? 0 : (Win32Native.IsWow64Process(Win32Native.GetCurrentProcess(), out isWow64) ? 1 : 0)) & (isWow64 ? 1 : 0)) != 0;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, выгружается ли текущий домен приложения или среда CLR завершает работу.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий домен приложения выгружается или среда CLR завершает работу; в противном случае — значение <see langword="false." />.
    /// </returns>
    [__DynamicallyInvokable]
    public static extern bool HasShutdownStarted { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool GetCompatibilityFlag(CompatibilityFlag flag);

    /// <summary>
    ///   Возвращает имя пользователя, который на данный момент выполнил вход в операционную систему Windows.
    /// </summary>
    /// <returns>
    ///   Имя пользователя, который на данный момент выполнил вход в операционную систему Windows.
    /// </returns>
    public static string UserName
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, nameof (UserName)).Demand();
        StringBuilder lpBuffer = new StringBuilder(256);
        int capacity = lpBuffer.Capacity;
        if (Win32Native.GetUserName(lpBuffer, ref capacity))
          return lpBuffer.ToString();
        return string.Empty;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, выполняется ли текущий процесс в режиме взаимодействия с пользователем.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий процесс выполняется в режиме взаимодействия с пользователем; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool UserInteractive
    {
      [SecuritySafeCritical] get
      {
        IntPtr processWindowStation = Win32Native.GetProcessWindowStation();
        if (processWindowStation != IntPtr.Zero && Environment.processWinStation != processWindowStation)
        {
          int lpnLengthNeeded = 0;
          Win32Native.USEROBJECTFLAGS userobjectflags = new Win32Native.USEROBJECTFLAGS();
          if (Win32Native.GetUserObjectInformation(processWindowStation, 1, userobjectflags, Marshal.SizeOf<Win32Native.USEROBJECTFLAGS>(userobjectflags), ref lpnLengthNeeded) && (userobjectflags.dwFlags & 1) == 0)
            Environment.isUserNonInteractive = true;
          Environment.processWinStation = processWindowStation;
        }
        return !Environment.isUserNonInteractive;
      }
    }

    /// <summary>
    ///   Возвращает путь к особой системной папке, указанной в заданном перечислении.
    /// </summary>
    /// <param name="folder">
    ///   Перечислимая константа, позволяющая определить особую системную папку.
    /// </param>
    /// <returns>
    ///   Путь к указанной особой системной папке, если эта папка физически существует на компьютере; в противном случае — пустая строка ("").
    /// 
    ///   Папка физически не существует, если она не была создана операционной системой, была удалена или является виртуальным каталогом, таким как "Мой компьютер", которому не сопоставлен физический путь.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="folder" /> не является членом <see cref="T:System.Environment.SpecialFolder" />.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Текущая платформа не поддерживается.
    /// </exception>
    [SecuritySafeCritical]
    public static string GetFolderPath(Environment.SpecialFolder folder)
    {
      if (!Enum.IsDefined(typeof (Environment.SpecialFolder), (object) folder))
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) folder));
      return Environment.InternalGetFolderPath(folder, Environment.SpecialFolderOption.None, false);
    }

    /// <summary>
    ///   Возвращает путь к особой системной папке, указанной в заданном перечислении, и использует заданный параметр для доступа к особым папкам.
    /// </summary>
    /// <param name="folder">
    ///   Перечислимая константа, позволяющая определить особую системную папку.
    /// </param>
    /// <param name="option">
    ///   Задает параметры, используемые для доступа к особой папке.
    /// </param>
    /// <returns>
    ///   Путь к указанной особой системной папке, если эта папка физически существует на компьютере; в противном случае — пустая строка ("").
    /// 
    ///   Папка физически не существует, если она не была создана операционной системой, была удалена или является виртуальным каталогом, таким как "Мой компьютер", которому не сопоставлен физический путь.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="folder" /> не является членом <see cref="T:System.Environment.SpecialFolder" />.
    /// </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   <see cref="T:System.PlatformNotSupportedException" />
    /// </exception>
    [SecuritySafeCritical]
    public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
    {
      if (!Enum.IsDefined(typeof (Environment.SpecialFolder), (object) folder))
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) folder));
      if (!Enum.IsDefined(typeof (Environment.SpecialFolderOption), (object) option))
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) option));
      return Environment.InternalGetFolderPath(folder, option, false);
    }

    [SecurityCritical]
    internal static string UnsafeGetFolderPath(Environment.SpecialFolder folder)
    {
      return Environment.InternalGetFolderPath(folder, Environment.SpecialFolderOption.None, true);
    }

    [SecurityCritical]
    private static string InternalGetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option, bool suppressSecurityChecks = false)
    {
      if (option == Environment.SpecialFolderOption.Create && !suppressSecurityChecks)
        new FileIOPermission(PermissionState.None)
        {
          AllFiles = FileIOPermissionAccess.Write
        }.Demand();
      StringBuilder lpszPath = new StringBuilder(260);
      int folderPath = Win32Native.SHGetFolderPath(IntPtr.Zero, (int) (folder | (Environment.SpecialFolder) option), IntPtr.Zero, 0, lpszPath);
      string empty;
      if (folderPath < 0)
      {
        if (folderPath == -2146233031)
          throw new PlatformNotSupportedException();
        empty = string.Empty;
      }
      else
        empty = lpszPath.ToString();
      if (!suppressSecurityChecks)
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, empty).Demand();
      return empty;
    }

    /// <summary>
    ///   Возвращает имя сетевого домена, связанное с текущим пользователем.
    /// </summary>
    /// <returns>
    ///   Имя сетевого домена, связанное с текущим пользователем.
    /// </returns>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Операционная система не поддерживает получение имени сетевого домена.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Не удалось получить имя сетевого домена.
    /// </exception>
    public static string UserDomainName
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, "UserDomain").Demand();
        byte[] sid = new byte[1024];
        int length1 = sid.Length;
        StringBuilder domainName = new StringBuilder(1024);
        uint capacity1 = (uint) domainName.Capacity;
        if (Win32Native.GetUserNameEx(2, domainName, ref capacity1) == (byte) 1)
        {
          string str = domainName.ToString();
          int length2 = str.IndexOf('\\');
          if (length2 != -1)
            return str.Substring(0, length2);
        }
        uint capacity2 = (uint) domainName.Capacity;
        int peUse;
        if (!Win32Native.LookupAccountName((string) null, Environment.UserName, sid, ref length1, domainName, ref capacity2, out peUse))
          throw new InvalidOperationException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
        return domainName.ToString();
      }
    }

    /// <summary>
    ///   Возвращает уникальный идентификатор текущего управляемого потока.
    /// </summary>
    /// <returns>
    ///   Целочисленное значение, представляющее уникальный идентификатор для этого управляемого потока.
    /// </returns>
    [__DynamicallyInvokable]
    public static int CurrentManagedThreadId
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return Thread.CurrentThread.ManagedThreadId;
      }
    }

    internal sealed class ResourceHelper
    {
      private string m_name;
      private ResourceManager SystemResMgr;
      private Stack currentlyLoading;
      internal bool resourceManagerInited;
      private int infinitelyRecursingCount;

      internal ResourceHelper(string name)
      {
        this.m_name = name;
      }

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
      internal string GetResourceString(string key)
      {
        if (key == null || key.Length == 0)
          return "[Resource lookup failed - null or empty resource name]";
        return this.GetResourceString(key, (CultureInfo) null);
      }

      [SecuritySafeCritical]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
      internal string GetResourceString(string key, CultureInfo culture)
      {
        if (key == null || key.Length == 0)
          return "[Resource lookup failed - null or empty resource name]";
        Environment.ResourceHelper.GetResourceStringUserData resourceStringUserData = new Environment.ResourceHelper.GetResourceStringUserData(this, key, culture);
        RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(new RuntimeHelpers.TryCode(this.GetResourceStringCode), new RuntimeHelpers.CleanupCode(this.GetResourceStringBackoutCode), (object) resourceStringUserData);
        return resourceStringUserData.m_retVal;
      }

      [SecuritySafeCritical]
      private void GetResourceStringCode(object userDataIn)
      {
        Environment.ResourceHelper.GetResourceStringUserData resourceStringUserData = (Environment.ResourceHelper.GetResourceStringUserData) userDataIn;
        Environment.ResourceHelper resourceHelper = resourceStringUserData.m_resourceHelper;
        string key = resourceStringUserData.m_key;
        CultureInfo culture = resourceStringUserData.m_culture;
        Monitor.Enter((object) resourceHelper, ref resourceStringUserData.m_lockWasTaken);
        if (resourceHelper.currentlyLoading != null && resourceHelper.currentlyLoading.Count > 0 && resourceHelper.currentlyLoading.Contains((object) key))
        {
          if (resourceHelper.infinitelyRecursingCount > 0)
          {
            resourceStringUserData.m_retVal = "[Resource lookup failed - infinite recursion or critical failure detected.]";
            return;
          }
          ++resourceHelper.infinitelyRecursingCount;
          string message = "Infinite recursion during resource lookup within mscorlib.  This may be a bug in mscorlib, or potentially in certain extensibility points such as assembly resolve events or CultureInfo names.  Resource name: " + key;
          Assert.Fail("[mscorlib recursive resource lookup bug]", message, -2146232797, System.Diagnostics.StackTrace.TraceFormat.NoResourceLookup);
          Environment.FailFast(message);
        }
        if (resourceHelper.currentlyLoading == null)
          resourceHelper.currentlyLoading = new Stack(4);
        if (!resourceHelper.resourceManagerInited)
        {
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
          }
          finally
          {
            RuntimeHelpers.RunClassConstructor(typeof (ResourceManager).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof (ResourceReader).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof (RuntimeResourceSet).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof (BinaryReader).TypeHandle);
            resourceHelper.resourceManagerInited = true;
          }
        }
        resourceHelper.currentlyLoading.Push((object) key);
        if (resourceHelper.SystemResMgr == null)
          resourceHelper.SystemResMgr = new ResourceManager(this.m_name, typeof (object).Assembly);
        string str = resourceHelper.SystemResMgr.GetString(key, (CultureInfo) null);
        resourceHelper.currentlyLoading.Pop();
        resourceStringUserData.m_retVal = str;
      }

      [PrePrepareMethod]
      private void GetResourceStringBackoutCode(object userDataIn, bool exceptionThrown)
      {
        Environment.ResourceHelper.GetResourceStringUserData resourceStringUserData = (Environment.ResourceHelper.GetResourceStringUserData) userDataIn;
        Environment.ResourceHelper resourceHelper = resourceStringUserData.m_resourceHelper;
        if (exceptionThrown && resourceStringUserData.m_lockWasTaken)
        {
          resourceHelper.SystemResMgr = (ResourceManager) null;
          resourceHelper.currentlyLoading = (Stack) null;
        }
        if (!resourceStringUserData.m_lockWasTaken)
          return;
        Monitor.Exit((object) resourceHelper);
      }

      internal class GetResourceStringUserData
      {
        public Environment.ResourceHelper m_resourceHelper;
        public string m_key;
        public CultureInfo m_culture;
        public string m_retVal;
        public bool m_lockWasTaken;

        public GetResourceStringUserData(Environment.ResourceHelper resourceHelper, string key, CultureInfo culture)
        {
          this.m_resourceHelper = resourceHelper;
          this.m_key = key;
          this.m_culture = culture;
        }
      }
    }

    /// <summary>
    ///   Указывает параметры, используемые для получения пути к особой папке.
    /// </summary>
    public enum SpecialFolderOption
    {
      None = 0,
      DoNotVerify = 16384, // 0x00004000
      Create = 32768, // 0x00008000
    }

    /// <summary>
    ///   Указывает перечислимые константы, используемые для получения путей к системным особым папкам.
    /// </summary>
    [ComVisible(true)]
    public enum SpecialFolder
    {
      Desktop = 0,
      Programs = 2,
      MyDocuments = 5,
      Personal = 5,
      Favorites = 6,
      Startup = 7,
      Recent = 8,
      SendTo = 9,
      StartMenu = 11, // 0x0000000B
      MyMusic = 13, // 0x0000000D
      MyVideos = 14, // 0x0000000E
      DesktopDirectory = 16, // 0x00000010
      MyComputer = 17, // 0x00000011
      NetworkShortcuts = 19, // 0x00000013
      Fonts = 20, // 0x00000014
      Templates = 21, // 0x00000015
      CommonStartMenu = 22, // 0x00000016
      CommonPrograms = 23, // 0x00000017
      CommonStartup = 24, // 0x00000018
      CommonDesktopDirectory = 25, // 0x00000019
      ApplicationData = 26, // 0x0000001A
      PrinterShortcuts = 27, // 0x0000001B
      LocalApplicationData = 28, // 0x0000001C
      InternetCache = 32, // 0x00000020
      Cookies = 33, // 0x00000021
      History = 34, // 0x00000022
      CommonApplicationData = 35, // 0x00000023
      Windows = 36, // 0x00000024
      System = 37, // 0x00000025
      ProgramFiles = 38, // 0x00000026
      MyPictures = 39, // 0x00000027
      UserProfile = 40, // 0x00000028
      SystemX86 = 41, // 0x00000029
      ProgramFilesX86 = 42, // 0x0000002A
      CommonProgramFiles = 43, // 0x0000002B
      CommonProgramFilesX86 = 44, // 0x0000002C
      CommonTemplates = 45, // 0x0000002D
      CommonDocuments = 46, // 0x0000002E
      CommonAdminTools = 47, // 0x0000002F
      AdminTools = 48, // 0x00000030
      CommonMusic = 53, // 0x00000035
      CommonPictures = 54, // 0x00000036
      CommonVideos = 55, // 0x00000037
      Resources = 56, // 0x00000038
      LocalizedResources = 57, // 0x00000039
      CommonOemLinks = 58, // 0x0000003A
      CDBurning = 59, // 0x0000003B
    }
  }
}
