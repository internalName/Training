// Decompiled with JetBrains decompiler
// Type: System.Console
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
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Предоставляет стандартные потоки для консольных приложений: входной, выходной и поток сообщений об ошибках.
  ///    Этот класс не наследуется.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  public static class Console
  {
    private static readonly UnicodeEncoding StdConUnicodeEncoding = new UnicodeEncoding(false, false);
    private static volatile bool _isOutTextWriterRedirected = false;
    private static volatile bool _isErrorTextWriterRedirected = false;
    private static volatile Encoding _inputEncoding = (Encoding) null;
    private static volatile Encoding _outputEncoding = (Encoding) null;
    private static volatile bool _stdInRedirectQueried = false;
    private static volatile bool _stdOutRedirectQueried = false;
    private static volatile bool _stdErrRedirectQueried = false;
    private const int DefaultConsoleBufferSize = 256;
    private const short AltVKCode = 18;
    private const int NumberLockVKCode = 144;
    private const int CapsLockVKCode = 20;
    private const int MinBeepFrequency = 37;
    private const int MaxBeepFrequency = 32767;
    private const int MaxConsoleTitleLength = 24500;
    private static volatile TextReader _in;
    private static volatile TextWriter _out;
    private static volatile TextWriter _error;
    private static volatile ConsoleCancelEventHandler _cancelCallbacks;
    private static volatile Console.ControlCHooker _hooker;
    [SecurityCritical]
    private static Win32Native.InputRecord _cachedInputRecord;
    private static volatile bool _haveReadDefaultColors;
    private static volatile byte _defaultColors;
    private static bool _isStdInRedirected;
    private static bool _isStdOutRedirected;
    private static bool _isStdErrRedirected;
    private static volatile object s_InternalSyncObject;
    private static volatile object s_ReadKeySyncObject;
    private static volatile IntPtr _consoleInputHandle;
    private static volatile IntPtr _consoleOutputHandle;

    private static object InternalSyncObject
    {
      get
      {
        if (Console.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref Console.s_InternalSyncObject, obj, (object) null);
        }
        return Console.s_InternalSyncObject;
      }
    }

    private static object ReadKeySyncObject
    {
      get
      {
        if (Console.s_ReadKeySyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref Console.s_ReadKeySyncObject, obj, (object) null);
        }
        return Console.s_ReadKeySyncObject;
      }
    }

    private static IntPtr ConsoleInputHandle
    {
      [SecurityCritical] get
      {
        if (Console._consoleInputHandle == IntPtr.Zero)
          Console._consoleInputHandle = Win32Native.GetStdHandle(-10);
        return Console._consoleInputHandle;
      }
    }

    private static IntPtr ConsoleOutputHandle
    {
      [SecurityCritical] get
      {
        if (Console._consoleOutputHandle == IntPtr.Zero)
          Console._consoleOutputHandle = Win32Native.GetStdHandle(-11);
        return Console._consoleOutputHandle;
      }
    }

    [SecuritySafeCritical]
    private static bool IsHandleRedirected(IntPtr ioHandle)
    {
      if ((Win32Native.GetFileType(new SafeFileHandle(ioHandle, false)) & 2) != 2)
        return true;
      int mode;
      return !Win32Native.GetConsoleMode(ioHandle, out mode);
    }

    /// <summary>
    ///   Получает значение, показывающее, был ли перенаправлены ли входные данные от стандартного входного потока.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если входные данные перенаправляются; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool IsInputRedirected
    {
      [SecuritySafeCritical] get
      {
        if (Console._stdInRedirectQueried)
          return Console._isStdInRedirected;
        lock (Console.InternalSyncObject)
        {
          if (Console._stdInRedirectQueried)
            return Console._isStdInRedirected;
          Console._isStdInRedirected = Console.IsHandleRedirected(Console.ConsoleInputHandle);
          Console._stdInRedirectQueried = true;
          return Console._isStdInRedirected;
        }
      }
    }

    /// <summary>
    ///   Получает значение, показывающее, был ли перенаправлены выходные данные от стандартного выходного потока.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если выходные данные перенаправляются; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool IsOutputRedirected
    {
      [SecuritySafeCritical] get
      {
        if (Console._stdOutRedirectQueried)
          return Console._isStdOutRedirected;
        lock (Console.InternalSyncObject)
        {
          if (Console._stdOutRedirectQueried)
            return Console._isStdOutRedirected;
          Console._isStdOutRedirected = Console.IsHandleRedirected(Console.ConsoleOutputHandle);
          Console._stdOutRedirectQueried = true;
          return Console._isStdOutRedirected;
        }
      }
    }

    /// <summary>
    ///   Получает значение, показывающее, был ли перенаправлен выходной поток ошибок от стандартного потока ошибок.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если выходные сообщения об ошибках перенаправляются; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool IsErrorRedirected
    {
      [SecuritySafeCritical] get
      {
        if (Console._stdErrRedirectQueried)
          return Console._isStdErrRedirected;
        lock (Console.InternalSyncObject)
        {
          if (Console._stdErrRedirectQueried)
            return Console._isStdErrRedirected;
          Console._isStdErrRedirected = Console.IsHandleRedirected(Win32Native.GetStdHandle(-12));
          Console._stdErrRedirectQueried = true;
          return Console._isStdErrRedirected;
        }
      }
    }

    /// <summary>Возвращает стандартный входной поток.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.IO.TextReader" />, представляющий стандартный входной поток.
    /// </returns>
    public static TextReader In
    {
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)] get
      {
        if (Console._in == null)
        {
          lock (Console.InternalSyncObject)
          {
            if (Console._in == null)
            {
              Stream stream = Console.OpenStandardInput(256);
              TextReader textReader;
              if (stream == Stream.Null)
              {
                textReader = (TextReader) StreamReader.Null;
              }
              else
              {
                Encoding inputEncoding = Console.InputEncoding;
                textReader = TextReader.Synchronized((TextReader) new StreamReader(stream, inputEncoding, false, 256, true));
              }
              Thread.MemoryBarrier();
              Console._in = textReader;
            }
          }
        }
        return Console._in;
      }
    }

    /// <summary>Возвращает стандартный выходной поток.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.IO.TextWriter" />, представляющий стандартный выходной поток.
    /// </returns>
    public static TextWriter Out
    {
      [HostProtection(SecurityAction.LinkDemand, UI = true)] get
      {
        if (Console._out == null)
          Console.InitializeStdOutError(true);
        return Console._out;
      }
    }

    /// <summary>
    ///   Возвращает стандартный выходной поток сообщений об ошибках.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.IO.TextWriter" />, предоставляющий стандартный поток вывода ошибок.
    /// </returns>
    public static TextWriter Error
    {
      [HostProtection(SecurityAction.LinkDemand, UI = true)] get
      {
        if (Console._error == null)
          Console.InitializeStdOutError(false);
        return Console._error;
      }
    }

    [SecuritySafeCritical]
    private static void InitializeStdOutError(bool stdout)
    {
      lock (Console.InternalSyncObject)
      {
        if (stdout && Console._out != null || !stdout && Console._error != null)
          return;
        Stream stream = !stdout ? Console.OpenStandardError(256) : Console.OpenStandardOutput(256);
        TextWriter textWriter;
        if (stream == Stream.Null)
        {
          textWriter = TextWriter.Synchronized((TextWriter) StreamWriter.Null);
        }
        else
        {
          Encoding outputEncoding = Console.OutputEncoding;
          textWriter = TextWriter.Synchronized((TextWriter) new StreamWriter(stream, outputEncoding, 256, true)
          {
            HaveWrittenPreamble = true,
            AutoFlush = true
          });
        }
        if (stdout)
          Console._out = textWriter;
        else
          Console._error = textWriter;
      }
    }

    private static bool IsStandardConsoleUnicodeEncoding(Encoding encoding)
    {
      UnicodeEncoding unicodeEncoding = encoding as UnicodeEncoding;
      if (unicodeEncoding == null || Console.StdConUnicodeEncoding.CodePage != unicodeEncoding.CodePage)
        return false;
      return Console.StdConUnicodeEncoding.bigEndian == unicodeEncoding.bigEndian;
    }

    private static bool GetUseFileAPIs(int handleType)
    {
      switch (handleType)
      {
        case -12:
          if (Console.IsStandardConsoleUnicodeEncoding(Console.OutputEncoding))
            return Console.IsErrorRedirected;
          return true;
        case -11:
          if (Console.IsStandardConsoleUnicodeEncoding(Console.OutputEncoding))
            return Console.IsOutputRedirected;
          return true;
        case -10:
          if (Console.IsStandardConsoleUnicodeEncoding(Console.InputEncoding))
            return Console.IsInputRedirected;
          return true;
        default:
          return true;
      }
    }

    [SecuritySafeCritical]
    private static Stream GetStandardFile(int stdHandleName, FileAccess access, int bufferSize)
    {
      SafeFileHandle safeFileHandle = new SafeFileHandle(Win32Native.GetStdHandle(stdHandleName), false);
      if (safeFileHandle.IsInvalid)
      {
        safeFileHandle.SetHandleAsInvalid();
        return Stream.Null;
      }
      if (stdHandleName != -10 && !Console.ConsoleHandleIsWritable(safeFileHandle))
        return Stream.Null;
      bool useFileApIs = Console.GetUseFileAPIs(stdHandleName);
      return (Stream) new __ConsoleStream(safeFileHandle, access, useFileApIs);
    }

    [SecuritySafeCritical]
    private static unsafe bool ConsoleHandleIsWritable(SafeFileHandle outErrHandle)
    {
      byte num = 65;
      int numBytesWritten;
      return (uint) Win32Native.WriteFile(outErrHandle, &num, 0, out numBytesWritten, IntPtr.Zero) > 0U;
    }

    /// <summary>
    ///   Возвращает или задает кодировку консоли, используемую при чтении входных данных.
    /// </summary>
    /// <returns>Кодировка консоли, используемая при чтении ввода.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства в операции задания — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Во время выполнения этой операции произошла ошибка.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У приложения нет разрешения на выполнение этой операции.
    /// </exception>
    public static Encoding InputEncoding
    {
      [SecuritySafeCritical] get
      {
        if (Console._inputEncoding != null)
          return Console._inputEncoding;
        lock (Console.InternalSyncObject)
        {
          if (Console._inputEncoding != null)
            return Console._inputEncoding;
          Console._inputEncoding = Encoding.GetEncoding((int) Win32Native.GetConsoleCP());
          return Console._inputEncoding;
        }
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        lock (Console.InternalSyncObject)
        {
          if (!Console.IsStandardConsoleUnicodeEncoding(value) && !Win32Native.SetConsoleCP((uint) value.CodePage))
            __Error.WinIOError();
          Console._inputEncoding = (Encoding) value.Clone();
          Console._in = (TextReader) null;
        }
      }
    }

    /// <summary>
    ///   Получает или задает кодировку консоли, используемую при записи выходных данных.
    /// </summary>
    /// <returns>Кодировка консоли, используемая при записи вывода.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение свойства в операции задания — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Во время выполнения этой операции произошла ошибка.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У приложения нет разрешения на выполнение этой операции.
    /// </exception>
    public static Encoding OutputEncoding
    {
      [SecuritySafeCritical] get
      {
        if (Console._outputEncoding != null)
          return Console._outputEncoding;
        lock (Console.InternalSyncObject)
        {
          if (Console._outputEncoding != null)
            return Console._outputEncoding;
          Console._outputEncoding = Encoding.GetEncoding((int) Win32Native.GetConsoleOutputCP());
          return Console._outputEncoding;
        }
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        lock (Console.InternalSyncObject)
        {
          if (Console._out != null && !Console._isOutTextWriterRedirected)
          {
            Console._out.Flush();
            Console._out = (TextWriter) null;
          }
          if (Console._error != null && !Console._isErrorTextWriterRedirected)
          {
            Console._error.Flush();
            Console._error = (TextWriter) null;
          }
          if (!Console.IsStandardConsoleUnicodeEncoding(value) && !Win32Native.SetConsoleOutputCP((uint) value.CodePage))
            __Error.WinIOError();
          Console._outputEncoding = (Encoding) value.Clone();
        }
      }
    }

    /// <summary>
    ///   Воспроизводит звуковой сигнал через динамик консоли.
    /// </summary>
    /// <exception cref="T:System.Security.HostProtectionException">
    ///   Этот метод выполнен на сервере, например SQL Server, который не разрешает доступ к интерфейсу пользователя.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Beep()
    {
      Console.Beep(800, 200);
    }

    /// <summary>
    ///   Воспроизводит звуковой сигнал заданной частоты и длительности через динамик консоли.
    /// </summary>
    /// <param name="frequency">
    ///   Частота сигнала в диапазоне от 37 до 32767 Гц.
    /// </param>
    /// <param name="duration">
    ///   Длительность сигнала в миллисекундах.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="frequency" /> меньше 37 или больше 32767 Гц.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="duration" /> меньше или равно нулю.
    /// </exception>
    /// <exception cref="T:System.Security.HostProtectionException">
    ///   Этот метод выполнен на сервере, например SQL Server, который не разрешает доступ к консоли.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Beep(int frequency, int duration)
    {
      if (frequency < 37 || frequency > (int) short.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (frequency), (object) frequency, Environment.GetResourceString("ArgumentOutOfRange_BeepFrequency", (object) 37, (object) (int) short.MaxValue));
      if (duration <= 0)
        throw new ArgumentOutOfRangeException(nameof (duration), (object) duration, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      Win32Native.Beep(frequency, duration);
    }

    /// <summary>
    ///   Удаляет из буфера консоли и ее окна отображаемую информацию.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [SecuritySafeCritical]
    public static void Clear()
    {
      Win32Native.COORD coord = new Win32Native.COORD();
      IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
      if (consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE)
        throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
      int num1 = (int) bufferInfo.dwSize.X * (int) bufferInfo.dwSize.Y;
      int num2 = 0;
      if (!Win32Native.FillConsoleOutputCharacter(consoleOutputHandle, ' ', num1, coord, out num2))
        __Error.WinIOError();
      num2 = 0;
      if (!Win32Native.FillConsoleOutputAttribute(consoleOutputHandle, bufferInfo.wAttributes, num1, coord, out num2))
        __Error.WinIOError();
      if (Win32Native.SetConsoleCursorPosition(consoleOutputHandle, coord))
        return;
      __Error.WinIOError();
    }

    [SecurityCritical]
    private static Win32Native.Color ConsoleColorToColorAttribute(ConsoleColor color, bool isBackground)
    {
      if ((color & ~ConsoleColor.White) != ConsoleColor.Black)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"));
      Win32Native.Color color1 = (Win32Native.Color) color;
      if (isBackground)
        color1 = (Win32Native.Color) ((int) color1 << 4);
      return color1;
    }

    [SecurityCritical]
    private static ConsoleColor ColorAttributeToConsoleColor(Win32Native.Color c)
    {
      if ((c & Win32Native.Color.BackgroundMask) != Win32Native.Color.Black)
        c = (Win32Native.Color) ((int) c >> 4);
      return (ConsoleColor) c;
    }

    /// <summary>Возвращает или задает цвет фона консоли.</summary>
    /// <returns>
    ///   Значение из перечисления , задающее фоновый цвет консоли, то есть цвет, на фоне которого выводятся символы.
    ///    Значением по умолчанию является Black.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Цвет, указанный в операции SET, не является допустимым членом <see cref="T:System.ConsoleColor" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static ConsoleColor BackgroundColor
    {
      [SecuritySafeCritical] get
      {
        bool succeeded;
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out succeeded);
        if (!succeeded)
          return ConsoleColor.Black;
        return Console.ColorAttributeToConsoleColor((Win32Native.Color) ((int) bufferInfo.wAttributes & 240));
      }
      [SecuritySafeCritical] set
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        Win32Native.Color colorAttribute = Console.ConsoleColorToColorAttribute(value, true);
        bool succeeded;
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out succeeded);
        if (!succeeded)
          return;
        Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, (short) ((int) (ushort) ((int) bufferInfo.wAttributes & -241) | (int) (ushort) colorAttribute));
      }
    }

    /// <summary>Возвращает или задает цвет фона консоли.</summary>
    /// <returns>
    ///   Значение из перечисления <see cref="T:System.ConsoleColor" />, задающее цвет переднего плана консоли, то есть цвет, которым выводятся символы.
    ///    По умолчанию задано значение Gray.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Цвет, указанный в операции SET, не является допустимым членом <see cref="T:System.ConsoleColor" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static ConsoleColor ForegroundColor
    {
      [SecuritySafeCritical] get
      {
        bool succeeded;
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out succeeded);
        if (!succeeded)
          return ConsoleColor.Gray;
        return Console.ColorAttributeToConsoleColor((Win32Native.Color) ((int) bufferInfo.wAttributes & 15));
      }
      [SecuritySafeCritical] set
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        Win32Native.Color colorAttribute = Console.ConsoleColorToColorAttribute(value, false);
        bool succeeded;
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out succeeded);
        if (!succeeded)
          return;
        Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, (short) ((int) (ushort) ((int) bufferInfo.wAttributes & -16) | (int) (ushort) colorAttribute));
      }
    }

    /// <summary>
    ///   Устанавливает для цветов фона и текста консоли их значения по умолчанию.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [SecuritySafeCritical]
    public static void ResetColor()
    {
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      bool succeeded;
      Console.GetBufferInfo(false, out succeeded);
      if (!succeeded)
        return;
      Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, (short) Console._defaultColors);
    }

    /// <summary>
    ///   Копирует заданную исходную область буфера экрана в заданную область назначения.
    /// </summary>
    /// <param name="sourceLeft">
    ///   Крайний слева столбец исходной области.
    /// </param>
    /// <param name="sourceTop">
    ///   Самая верхняя строка исходной области.
    /// </param>
    /// <param name="sourceWidth">
    ///   Общее число столбцов в исходной области.
    /// </param>
    /// <param name="sourceHeight">
    ///   Общее число строк в исходной области.
    /// </param>
    /// <param name="targetLeft">
    ///   Крайний слева столбец области назначения.
    /// </param>
    /// <param name="targetTop">
    ///   Самая верхняя строка области назначения.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Один или несколько параметров имеют значение меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="sourceLeft" /> или <paramref name="targetLeft" /> больше или равен <see cref="P:System.Console.BufferWidth" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="sourceTop" /> или <paramref name="targetTop" /> больше или равен <see cref="P:System.Console.BufferHeight" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> больше или равно <see cref="P:System.Console.BufferHeight" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> больше или равно <see cref="P:System.Console.BufferWidth" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
    {
      Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, Console.BackgroundColor);
    }

    /// <summary>
    ///   Копирует заданную исходную область буфера экрана в заданную область назначения.
    /// </summary>
    /// <param name="sourceLeft">
    ///   Крайний слева столбец исходной области.
    /// </param>
    /// <param name="sourceTop">
    ///   Самая верхняя строка исходной области.
    /// </param>
    /// <param name="sourceWidth">
    ///   Общее число столбцов в исходной области.
    /// </param>
    /// <param name="sourceHeight">
    ///   Общее число строк в исходной области.
    /// </param>
    /// <param name="targetLeft">
    ///   Крайний слева столбец области назначения.
    /// </param>
    /// <param name="targetTop">
    ///   Самая верхняя строка области назначения.
    /// </param>
    /// <param name="sourceChar">
    ///   Символ, используемый для заполнения исходной области.
    /// </param>
    /// <param name="sourceForeColor">
    ///   Цвет переднего плана, используемый для заполнения исходной области.
    /// </param>
    /// <param name="sourceBackColor">
    ///   Цвет фона, используемый для заполнения исходной области.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Один или несколько параметров имеют значение меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="sourceLeft" /> или <paramref name="targetLeft" /> больше или равен <see cref="P:System.Console.BufferWidth" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="sourceTop" /> или <paramref name="targetTop" /> больше или равен <see cref="P:System.Console.BufferHeight" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> больше или равно <see cref="P:System.Console.BufferHeight" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> больше или равно <see cref="P:System.Console.BufferWidth" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Один или оба параметра цвет не является членом <see cref="T:System.ConsoleColor" /> перечисления.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
    {
      if (sourceForeColor < ConsoleColor.Black || sourceForeColor > ConsoleColor.White)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), nameof (sourceForeColor));
      if (sourceBackColor < ConsoleColor.Black || sourceBackColor > ConsoleColor.White)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), nameof (sourceBackColor));
      Win32Native.COORD dwSize = Console.GetBufferInfo().dwSize;
      if (sourceLeft < 0 || sourceLeft > (int) dwSize.X)
        throw new ArgumentOutOfRangeException(nameof (sourceLeft), (object) sourceLeft, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (sourceTop < 0 || sourceTop > (int) dwSize.Y)
        throw new ArgumentOutOfRangeException(nameof (sourceTop), (object) sourceTop, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (sourceWidth < 0 || sourceWidth > (int) dwSize.X - sourceLeft)
        throw new ArgumentOutOfRangeException(nameof (sourceWidth), (object) sourceWidth, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (sourceHeight < 0 || sourceTop > (int) dwSize.Y - sourceHeight)
        throw new ArgumentOutOfRangeException(nameof (sourceHeight), (object) sourceHeight, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (targetLeft < 0 || targetLeft > (int) dwSize.X)
        throw new ArgumentOutOfRangeException(nameof (targetLeft), (object) targetLeft, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (targetTop < 0 || targetTop > (int) dwSize.Y)
        throw new ArgumentOutOfRangeException(nameof (targetTop), (object) targetTop, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (sourceWidth == 0 || sourceHeight == 0)
        return;
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      Win32Native.CHAR_INFO[] charInfoArray = new Win32Native.CHAR_INFO[sourceWidth * sourceHeight];
      dwSize.X = (short) sourceWidth;
      dwSize.Y = (short) sourceHeight;
      Win32Native.COORD bufferCoord = new Win32Native.COORD();
      Win32Native.SMALL_RECT readRegion = new Win32Native.SMALL_RECT();
      readRegion.Left = (short) sourceLeft;
      readRegion.Right = (short) (sourceLeft + sourceWidth - 1);
      readRegion.Top = (short) sourceTop;
      readRegion.Bottom = (short) (sourceTop + sourceHeight - 1);
      bool flag;
      fixed (Win32Native.CHAR_INFO* pBuffer = charInfoArray)
        flag = Win32Native.ReadConsoleOutput(Console.ConsoleOutputHandle, pBuffer, dwSize, bufferCoord, ref readRegion);
      if (!flag)
        __Error.WinIOError();
      Win32Native.COORD coord = new Win32Native.COORD();
      coord.X = (short) sourceLeft;
      short wColorAttribute = (short) (Console.ConsoleColorToColorAttribute(sourceBackColor, true) | Console.ConsoleColorToColorAttribute(sourceForeColor, false));
      for (int index = sourceTop; index < sourceTop + sourceHeight; ++index)
      {
        coord.Y = (short) index;
        int num;
        if (!Win32Native.FillConsoleOutputCharacter(Console.ConsoleOutputHandle, sourceChar, sourceWidth, coord, out num))
          __Error.WinIOError();
        if (!Win32Native.FillConsoleOutputAttribute(Console.ConsoleOutputHandle, wColorAttribute, sourceWidth, coord, out num))
          __Error.WinIOError();
      }
      Win32Native.SMALL_RECT writeRegion = new Win32Native.SMALL_RECT();
      writeRegion.Left = (short) targetLeft;
      writeRegion.Right = (short) (targetLeft + sourceWidth);
      writeRegion.Top = (short) targetTop;
      writeRegion.Bottom = (short) (targetTop + sourceHeight);
      fixed (Win32Native.CHAR_INFO* buffer = charInfoArray)
        Win32Native.WriteConsoleOutput(Console.ConsoleOutputHandle, buffer, dwSize, bufferCoord, ref writeRegion);
    }

    [SecurityCritical]
    private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo()
    {
      bool succeeded;
      return Console.GetBufferInfo(true, out succeeded);
    }

    [SecuritySafeCritical]
    private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo(bool throwOnNoConsole, out bool succeeded)
    {
      succeeded = false;
      IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
      if (consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE)
      {
        if (!throwOnNoConsole)
          return new Win32Native.CONSOLE_SCREEN_BUFFER_INFO();
        throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
      }
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo;
      if (!Win32Native.GetConsoleScreenBufferInfo(consoleOutputHandle, out lpConsoleScreenBufferInfo))
      {
        bool screenBufferInfo = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-12), out lpConsoleScreenBufferInfo);
        if (!screenBufferInfo)
          screenBufferInfo = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-10), out lpConsoleScreenBufferInfo);
        if (!screenBufferInfo)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          if (lastWin32Error == 6 && !throwOnNoConsole)
            return new Win32Native.CONSOLE_SCREEN_BUFFER_INFO();
          __Error.WinIOError(lastWin32Error, (string) null);
        }
      }
      if (!Console._haveReadDefaultColors)
      {
        Console._defaultColors = (byte) ((uint) lpConsoleScreenBufferInfo.wAttributes & (uint) byte.MaxValue);
        Console._haveReadDefaultColors = true;
      }
      succeeded = true;
      return lpConsoleScreenBufferInfo;
    }

    /// <summary>Возвращает или задает высоту буферной области.</summary>
    /// <returns>Текущая высота буферной области в строках.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение в операции задания меньше или равно нулю.
    /// 
    ///   -или-
    /// 
    ///   Значение в операции задания больше или равно <see cref="F:System.Int16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Значение в операции задания меньше суммы <see cref="P:System.Console.WindowTop" /> и <see cref="P:System.Console.WindowHeight" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static int BufferHeight
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().dwSize.Y;
      }
      set
      {
        Console.SetBufferSize(Console.BufferWidth, value);
      }
    }

    /// <summary>Возвращает или задает ширину буферной области.</summary>
    /// <returns>Текущая ширина буферной области в столбцах.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение в операции задания меньше или равно нулю.
    /// 
    ///   -или-
    /// 
    ///   Значение в операции задания больше или равно <see cref="F:System.Int16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Значение в операции задания меньше суммы <see cref="P:System.Console.WindowLeft" /> и <see cref="P:System.Console.WindowWidth" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static int BufferWidth
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().dwSize.X;
      }
      set
      {
        Console.SetBufferSize(value, Console.BufferHeight);
      }
    }

    /// <summary>
    ///   Устанавливает заданные значения высоты и ширины буферной области экрана.
    /// </summary>
    /// <param name="width">Ширина области буфера в столбцах.</param>
    /// <param name="height">Высота области буфера в строках.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="height" /> или <paramref name="width" /> меньше или равно нулю.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="height" /> или <paramref name="width" /> больше или равен <see cref="F:System.Int16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="width" /> меньше <see cref="P:System.Console.WindowLeft" /> + <see cref="P:System.Console.WindowWidth" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="height" /> меньше <see cref="P:System.Console.WindowTop" /> + <see cref="P:System.Console.WindowHeight" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [SecuritySafeCritical]
    public static void SetBufferSize(int width, int height)
    {
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      Win32Native.SMALL_RECT srWindow = Console.GetBufferInfo().srWindow;
      if (width < (int) srWindow.Right + 1 || width >= (int) short.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (width), (object) width, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
      if (height < (int) srWindow.Bottom + 1 || height >= (int) short.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (height), (object) height, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
      if (Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, new Win32Native.COORD()
      {
        X = (short) width,
        Y = (short) height
      }))
        return;
      __Error.WinIOError();
    }

    /// <summary>Возвращает или задает высоту области окна консоли.</summary>
    /// <returns>Высота окна консоли измеряется строками.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение свойства <see cref="P:System.Console.WindowWidth" /> или <see cref="P:System.Console.WindowHeight" /> не больше 0.
    /// 
    ///   -или-
    /// 
    ///   Сумма значений свойств <see cref="P:System.Console.WindowHeight" /> и <see cref="P:System.Console.WindowTop" /> больше или равна <see cref="F:System.Int16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Значение свойства <see cref="P:System.Console.WindowWidth" /> или <see cref="P:System.Console.WindowHeight" /> больше наибольшей возможной ширины или высоты окна для текущего шрифта консоли и разрешения экрана.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка при чтении или записи данных.
    /// </exception>
    public static int WindowHeight
    {
      [SecuritySafeCritical] get
      {
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
        return (int) bufferInfo.srWindow.Bottom - (int) bufferInfo.srWindow.Top + 1;
      }
      set
      {
        Console.SetWindowSize(Console.WindowWidth, value);
      }
    }

    /// <summary>Возвращает или задает ширину окна консоли.</summary>
    /// <returns>Ширина окна консоли измеряется столбцами.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение свойства <see cref="P:System.Console.WindowWidth" /> или <see cref="P:System.Console.WindowHeight" /> больше или равно 0.
    /// 
    ///   -или-
    /// 
    ///   Сумма значений свойств <see cref="P:System.Console.WindowHeight" /> и <see cref="P:System.Console.WindowTop" /> больше или равна <see cref="F:System.Int16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Значение свойства <see cref="P:System.Console.WindowWidth" /> или <see cref="P:System.Console.WindowHeight" /> больше наибольшей возможной ширины или высоты окна для текущего шрифта консоли и разрешения экрана.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка при чтении или записи данных.
    /// </exception>
    public static int WindowWidth
    {
      [SecuritySafeCritical] get
      {
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
        return (int) bufferInfo.srWindow.Right - (int) bufferInfo.srWindow.Left + 1;
      }
      set
      {
        Console.SetWindowSize(value, Console.WindowHeight);
      }
    }

    /// <summary>
    ///   Устанавливает заданные значения высоты и ширины окна консоли.
    /// </summary>
    /// <param name="width">
    ///   Ширина окна консоли измеряется столбцами.
    /// </param>
    /// <param name="height">
    ///   Высота окна консоли измеряется строками.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="width" /> или <paramref name="height" /> меньше или равно нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="width" /> плюс <see cref="P:System.Console.WindowLeft" /> или <paramref name="height" /> плюс <see cref="P:System.Console.WindowTop" /> больше или равно <see cref="F:System.Int16.MaxValue" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="width" /> или <paramref name="height" /> больше наибольшей возможной ширины или высоты окна для текущего разрешения экрана и шрифта консоли.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe void SetWindowSize(int width, int height)
    {
      if (width <= 0)
        throw new ArgumentOutOfRangeException(nameof (width), (object) width, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (height <= 0)
        throw new ArgumentOutOfRangeException(nameof (height), (object) height, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
      bool flag = false;
      Win32Native.COORD size = new Win32Native.COORD();
      size.X = bufferInfo.dwSize.X;
      size.Y = bufferInfo.dwSize.Y;
      if ((int) bufferInfo.dwSize.X < (int) bufferInfo.srWindow.Left + width)
      {
        if ((int) bufferInfo.srWindow.Left >= (int) short.MaxValue - width)
          throw new ArgumentOutOfRangeException(nameof (width), Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowBufferSize"));
        size.X = (short) ((int) bufferInfo.srWindow.Left + width);
        flag = true;
      }
      if ((int) bufferInfo.dwSize.Y < (int) bufferInfo.srWindow.Top + height)
      {
        if ((int) bufferInfo.srWindow.Top >= (int) short.MaxValue - height)
          throw new ArgumentOutOfRangeException(nameof (height), Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowBufferSize"));
        size.Y = (short) ((int) bufferInfo.srWindow.Top + height);
        flag = true;
      }
      if (flag && !Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, size))
        __Error.WinIOError();
      Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
      srWindow.Bottom = (short) ((int) srWindow.Top + height - 1);
      srWindow.Right = (short) ((int) srWindow.Left + width - 1);
      if (Win32Native.SetConsoleWindowInfo(Console.ConsoleOutputHandle, true, &srWindow))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (flag)
        Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, bufferInfo.dwSize);
      Win32Native.COORD consoleWindowSize = Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle);
      if (width > (int) consoleWindowSize.X)
        throw new ArgumentOutOfRangeException(nameof (width), (object) width, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowSize_Size", (object) consoleWindowSize.X));
      if (height > (int) consoleWindowSize.Y)
        throw new ArgumentOutOfRangeException(nameof (height), (object) height, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowSize_Size", (object) consoleWindowSize.Y));
      __Error.WinIOError(lastWin32Error, string.Empty);
    }

    /// <summary>
    ///   Возвращает максимальное число столбцов окна консоли с учетом текущего шрифта и разрешения экрана.
    /// </summary>
    /// <returns>
    ///   Максимально возможная ширина окна консоли измеряется в столбцах.
    /// </returns>
    public static int LargestWindowWidth
    {
      [SecuritySafeCritical] get
      {
        return (int) Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle).X;
      }
    }

    /// <summary>
    ///   Возвращает максимальное число строк окна консоли с учетом текущего шрифта и разрешения экрана.
    /// </summary>
    /// <returns>
    ///   Максимально возможная высота окна консоли измеряется в строках.
    /// </returns>
    public static int LargestWindowHeight
    {
      [SecuritySafeCritical] get
      {
        return (int) Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle).Y;
      }
    }

    /// <summary>
    ///   Возвращает или задает позицию левого края области окна консоли относительно буфера экрана.
    /// </summary>
    /// <returns>
    ///   Позиция левого края области окна консоли измеряется столбцами.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   В операции над множеством назначаемое значение меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   В результате назначения сумма <see cref="P:System.Console.WindowLeft" /> и <see cref="P:System.Console.WindowWidth" /> превысит <see cref="P:System.Console.BufferWidth" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка при чтении или записи данных.
    /// </exception>
    public static int WindowLeft
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().srWindow.Left;
      }
      set
      {
        Console.SetWindowPosition(value, Console.WindowTop);
      }
    }

    /// <summary>
    ///   Возвращает или задает позицию верхнего края области окна консоли относительно буфера экрана.
    /// </summary>
    /// <returns>
    ///   Позиция верхнего края области окна консоли измеряется строками.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   В операции задания назначаемое значение меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   В результате назначения сумма <see cref="P:System.Console.WindowTop" /> и <see cref="P:System.Console.WindowHeight" /> превысит <see cref="P:System.Console.BufferHeight" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка при чтении или записи данных.
    /// </exception>
    public static int WindowTop
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().srWindow.Top;
      }
      set
      {
        Console.SetWindowPosition(Console.WindowLeft, value);
      }
    }

    /// <summary>
    ///   Задает позицию окна консоли относительно буфера экрана.
    /// </summary>
    /// <param name="left">
    ///   Позиция столбца верхнего левого угла окна консоли.
    /// </param>
    /// <param name="top">
    ///   Позиция строки верхнего левого угла окна консоли.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="left" /> или <paramref name="top" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Сумма <paramref name="left" /> и <see cref="P:System.Console.WindowWidth" /> больше значения <see cref="P:System.Console.BufferWidth" />.
    /// 
    ///   -или-
    /// 
    ///   Сумма <paramref name="top" /> и <see cref="P:System.Console.WindowHeight" /> больше значения <see cref="P:System.Console.BufferHeight" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe void SetWindowPosition(int left, int top)
    {
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
      Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
      int num1 = left + (int) srWindow.Right - (int) srWindow.Left + 1;
      if (left < 0 || num1 > (int) bufferInfo.dwSize.X || num1 < 0)
        throw new ArgumentOutOfRangeException(nameof (left), (object) left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowPos"));
      int num2 = top + (int) srWindow.Bottom - (int) srWindow.Top + 1;
      if (top < 0 || num2 > (int) bufferInfo.dwSize.Y || num2 < 0)
        throw new ArgumentOutOfRangeException(nameof (top), (object) top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowPos"));
      srWindow.Bottom -= (short) ((int) srWindow.Top - top);
      srWindow.Right -= (short) ((int) srWindow.Left - left);
      srWindow.Left = (short) left;
      srWindow.Top = (short) top;
      if (Win32Native.SetConsoleWindowInfo(Console.ConsoleOutputHandle, true, &srWindow))
        return;
      __Error.WinIOError();
    }

    /// <summary>
    ///   Возвращает или задает позицию столбца курсора в буферной области.
    /// </summary>
    /// <returns>Текущая позиция курсора в столбцах.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение в операции задания меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение в операции задания больше или равно <see cref="P:System.Console.BufferWidth" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static int CursorLeft
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().dwCursorPosition.X;
      }
      set
      {
        Console.SetCursorPosition(value, Console.CursorTop);
      }
    }

    /// <summary>
    ///   Возвращает или задает позицию строки курсора в буферной области.
    /// </summary>
    /// <returns>Текущая позиция курсора в строках.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение в операции задания меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение в операции задания больше или равно <see cref="P:System.Console.BufferHeight" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static int CursorTop
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().dwCursorPosition.Y;
      }
      set
      {
        Console.SetCursorPosition(Console.CursorLeft, value);
      }
    }

    /// <summary>Устанавливает положение курсора.</summary>
    /// <param name="left">
    ///   Позиция столбца курсора.
    ///    Столбцы нумеруются как слева направо, начинается с 0.
    /// </param>
    /// <param name="top">
    ///   Позиция строки курсора.
    ///    Строки пронумерованы сверху вниз, начиная с 0.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="left" /> или <paramref name="top" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="left" /> больше или равно <see cref="P:System.Console.BufferWidth" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="top" /> больше или равно <see cref="P:System.Console.BufferHeight" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [SecuritySafeCritical]
    public static void SetCursorPosition(int left, int top)
    {
      if (left < 0 || left >= (int) short.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (left), (object) left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (top < 0 || top >= (int) short.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (top), (object) top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      if (Win32Native.SetConsoleCursorPosition(Console.ConsoleOutputHandle, new Win32Native.COORD()
      {
        X = (short) left,
        Y = (short) top
      }))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
      if (left < 0 || left >= (int) bufferInfo.dwSize.X)
        throw new ArgumentOutOfRangeException(nameof (left), (object) left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (top < 0 || top >= (int) bufferInfo.dwSize.Y)
        throw new ArgumentOutOfRangeException(nameof (top), (object) top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      __Error.WinIOError(lastWin32Error, string.Empty);
    }

    /// <summary>
    ///   Возвращает или задает высоту курсора в символьной ячейке.
    /// </summary>
    /// <returns>
    ///   Размер курсора, выраженный как процент от высоты символьной ячейки.
    ///    Данное свойство принимает значения в диапазоне от 1 до 100.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение, указанное в операции задания, меньше 1 или больше 100.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static int CursorSize
    {
      [SecuritySafeCritical] get
      {
        Win32Native.CONSOLE_CURSOR_INFO cci;
        if (!Win32Native.GetConsoleCursorInfo(Console.ConsoleOutputHandle, out cci))
          __Error.WinIOError();
        return cci.dwSize;
      }
      [SecuritySafeCritical] set
      {
        if (value < 1 || value > 100)
          throw new ArgumentOutOfRangeException(nameof (value), (object) value, Environment.GetResourceString("ArgumentOutOfRange_CursorSize"));
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
        Win32Native.CONSOLE_CURSOR_INFO cci;
        if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out cci))
          __Error.WinIOError();
        cci.dwSize = value;
        if (Win32Native.SetConsoleCursorInfo(consoleOutputHandle, ref cci))
          return;
        __Error.WinIOError();
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, видим ли курсор.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если курсор видим; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешений на выполнение этого действия.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static bool CursorVisible
    {
      [SecuritySafeCritical] get
      {
        Win32Native.CONSOLE_CURSOR_INFO cci;
        if (!Win32Native.GetConsoleCursorInfo(Console.ConsoleOutputHandle, out cci))
          __Error.WinIOError();
        return cci.bVisible;
      }
      [SecuritySafeCritical] set
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
        Win32Native.CONSOLE_CURSOR_INFO cci;
        if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out cci))
          __Error.WinIOError();
        cci.bVisible = value;
        if (Win32Native.SetConsoleCursorInfo(consoleOutputHandle, ref cci))
          return;
        __Error.WinIOError();
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Ansi)]
    private static extern int GetTitleNative(StringHandleOnStack outTitle, out int outTitleLength);

    /// <summary>
    ///   Возвращает или задает заголовок для отображения в строке заголовка консоли.
    /// </summary>
    /// <returns>
    ///   Строка для отображения в строке заголовка консоли.
    ///    Максимальная длина строки заголовка — 24500 символов.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   В операции get длина полученного заголовка превышает 24 500 символов.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   В операции set длина указанного заголовка превышает 24 500 символов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   В операции set указанный заголовок имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    public static string Title
    {
      [SecuritySafeCritical] get
      {
        string s = (string) null;
        int outTitleLength = -1;
        int titleNative = Console.GetTitleNative(JitHelpers.GetStringHandleOnStack(ref s), out outTitleLength);
        if (titleNative != 0)
          __Error.WinIOError(titleNative, string.Empty);
        if (outTitleLength > 24500)
          throw new InvalidOperationException(Environment.GetResourceString("ArgumentOutOfRange_ConsoleTitleTooLong"));
        return s;
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (value.Length > 24500)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_ConsoleTitleTooLong"));
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        if (Win32Native.SetConsoleTitle(value))
          return;
        __Error.WinIOError();
      }
    }

    /// <summary>
    ///   Получает следующий нажатый пользователем символ или функциональную клавишу.
    ///    Нажатая клавиша отображается в окне консоли.
    /// </summary>
    /// <returns>
    ///   Объект, описывающий константу <see cref="T:System.ConsoleKey" /> и символ Юникода (при наличии), соответствующий нажатой клавише консоли.
    ///    Этот объект <see cref="T:System.ConsoleKeyInfo" /> также описывает в битовой комбинации значений <see cref="T:System.ConsoleModifiers" />, нажимались ли клавиши-модификаторы (одна или несколько) Shift, Alt или Ctrl одновременно с клавишей консоли.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство <see cref="P:System.Console.In" /> перенаправлено из потока, отличного от консоли.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static ConsoleKeyInfo ReadKey()
    {
      return Console.ReadKey(false);
    }

    [SecurityCritical]
    private static bool IsAltKeyDown(Win32Native.InputRecord ir)
    {
      return (uint) (ir.keyEvent.controlKeyState & 3) > 0U;
    }

    [SecurityCritical]
    private static bool IsKeyDownEvent(Win32Native.InputRecord ir)
    {
      if (ir.eventType == (short) 1)
        return ir.keyEvent.keyDown;
      return false;
    }

    [SecurityCritical]
    private static bool IsModKey(Win32Native.InputRecord ir)
    {
      short virtualKeyCode = ir.keyEvent.virtualKeyCode;
      if ((virtualKeyCode < (short) 16 || virtualKeyCode > (short) 18) && (virtualKeyCode != (short) 20 && virtualKeyCode != (short) 144))
        return virtualKeyCode == (short) 145;
      return true;
    }

    /// <summary>
    ///   Получает следующий нажатый пользователем символ или функциональную клавишу.
    ///    Нажатая клавиша может быть отображена в окне консоли.
    /// </summary>
    /// <param name="intercept">
    ///   Определяет, следует ли отображать нажатую клавишу в окне консоли.
    ///    Значение <see langword="true" />, чтобы не отображать нажатую клавишу; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект, описывающий константу <see cref="T:System.ConsoleKey" /> и символ Юникода (при наличии), соответствующий нажатой клавише консоли.
    ///    Этот объект <see cref="T:System.ConsoleKeyInfo" /> также описывает в битовой комбинации значений <see cref="T:System.ConsoleModifiers" />, нажимались ли клавиши-модификаторы (одна или несколько) Shift, Alt или Ctrl одновременно с клавишей консоли.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство <see cref="P:System.Console.In" /> перенаправлено из потока, отличного от консоли.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static ConsoleKeyInfo ReadKey(bool intercept)
    {
      int numEventsRead = -1;
      Win32Native.InputRecord buffer;
      lock (Console.ReadKeySyncObject)
      {
        if (Console._cachedInputRecord.eventType == (short) 1)
        {
          buffer = Console._cachedInputRecord;
          if (Console._cachedInputRecord.keyEvent.repeatCount == (short) 0)
            Console._cachedInputRecord.eventType = (short) -1;
          else
            --Console._cachedInputRecord.keyEvent.repeatCount;
        }
        else
        {
          while (Win32Native.ReadConsoleInput(Console.ConsoleInputHandle, out buffer, 1, out numEventsRead) && numEventsRead != 0)
          {
            short virtualKeyCode = buffer.keyEvent.virtualKeyCode;
            if ((Console.IsKeyDownEvent(buffer) || virtualKeyCode == (short) 18) && (buffer.keyEvent.uChar != char.MinValue || !Console.IsModKey(buffer)))
            {
              ConsoleKey consoleKey = (ConsoleKey) virtualKeyCode;
              if (!Console.IsAltKeyDown(buffer) || (consoleKey < ConsoleKey.NumPad0 || consoleKey > ConsoleKey.NumPad9) && (consoleKey != ConsoleKey.Clear && consoleKey != ConsoleKey.Insert) && (consoleKey < ConsoleKey.PageUp || consoleKey > ConsoleKey.DownArrow))
              {
                if (buffer.keyEvent.repeatCount > (short) 1)
                {
                  --buffer.keyEvent.repeatCount;
                  Console._cachedInputRecord = buffer;
                  goto label_14;
                }
                else
                  goto label_14;
              }
            }
          }
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConsoleReadKeyOnFile"));
        }
      }
label_14:
      Console.ControlKeyState controlKeyState = (Console.ControlKeyState) buffer.keyEvent.controlKeyState;
      bool shift = (uint) (controlKeyState & Console.ControlKeyState.ShiftPressed) > 0U;
      bool alt = (uint) (controlKeyState & (Console.ControlKeyState.RightAltPressed | Console.ControlKeyState.LeftAltPressed)) > 0U;
      bool control = (uint) (controlKeyState & (Console.ControlKeyState.RightCtrlPressed | Console.ControlKeyState.LeftCtrlPressed)) > 0U;
      ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo(buffer.keyEvent.uChar, (ConsoleKey) buffer.keyEvent.virtualKeyCode, shift, alt, control);
      if (!intercept)
        Console.Write(buffer.keyEvent.uChar);
      return consoleKeyInfo;
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, доступно ли нажатие клавиши во входном потоке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если нажатие клавиши доступно; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Стандартный ввод перенаправляется в файл вместо клавиатуры.
    /// </exception>
    public static bool KeyAvailable
    {
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)] get
      {
        if (Console._cachedInputRecord.eventType == (short) 1)
          return true;
        Win32Native.InputRecord buffer = new Win32Native.InputRecord();
        int numEventsRead = 0;
        while (true)
        {
          do
          {
            if (!Win32Native.PeekConsoleInput(Console.ConsoleInputHandle, out buffer, 1, out numEventsRead))
            {
              int lastWin32Error = Marshal.GetLastWin32Error();
              if (lastWin32Error == 6)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConsoleKeyAvailableOnFile"));
              __Error.WinIOError(lastWin32Error, "stdin");
            }
            if (numEventsRead == 0)
              return false;
            if (Console.IsKeyDownEvent(buffer) && !Console.IsModKey(buffer))
              goto label_12;
          }
          while (Win32Native.ReadConsoleInput(Console.ConsoleInputHandle, out buffer, 1, out numEventsRead));
          __Error.WinIOError();
        }
label_12:
        return true;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, включен или отключен режим NUM LOCK клавиатуры.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если клавиша NUM LOCK включена; значение <see langword="false" />, если клавиша NUM LOCK выключена.
    /// </returns>
    public static bool NumberLock
    {
      [SecuritySafeCritical] get
      {
        return ((int) Win32Native.GetKeyState(144) & 1) == 1;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, включен или отключен режим CAPS LOCK клавиатуры.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если режим CAPS LOCK включен; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool CapsLock
    {
      [SecuritySafeCritical] get
      {
        return ((int) Win32Native.GetKeyState(20) & 1) == 1;
      }
    }

    /// <summary>
    ///   Получает или задает значение, указывающее, интерпретируется ли комбинация клавиши-модификатора <see cref="F:System.ConsoleModifiers.Control" /> и клавиши консоли <see cref="F:System.ConsoleKey.C" /> (Ctrl+C) как обычный ввод или как прерывание, которое обрабатывается операционной системой.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если сочетание клавиш CTRL+C интерпретируется как обычный ввод; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Не удалось получить или задать режим ввода входного буфера консоли.
    /// </exception>
    public static bool TreatControlCAsInput
    {
      [SecuritySafeCritical] get
      {
        IntPtr consoleInputHandle = Console.ConsoleInputHandle;
        if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
          throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
        int mode = 0;
        if (!Win32Native.GetConsoleMode(consoleInputHandle, out mode))
          __Error.WinIOError();
        return (mode & 1) == 0;
      }
      [SecuritySafeCritical] set
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        IntPtr consoleInputHandle = Console.ConsoleInputHandle;
        if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
          throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
        int mode1 = 0;
        Win32Native.GetConsoleMode(consoleInputHandle, out mode1);
        int mode2 = !value ? mode1 | 1 : mode1 & -2;
        if (Win32Native.SetConsoleMode(consoleInputHandle, mode2))
          return;
        __Error.WinIOError();
      }
    }

    private static bool BreakEvent(int controlType)
    {
      if (controlType != 0 && controlType != 1)
        return false;
      ConsoleCancelEventHandler cancelCallbacks = Console._cancelCallbacks;
      if (cancelCallbacks == null)
        return false;
      Console.ControlCDelegateData controlCdelegateData = new Console.ControlCDelegateData(controlType == 0 ? ConsoleSpecialKey.ControlC : ConsoleSpecialKey.ControlBreak, cancelCallbacks);
      if (!ThreadPool.QueueUserWorkItem(new WaitCallback(Console.ControlCDelegate), (object) controlCdelegateData))
        return false;
      TimeSpan timeout = new TimeSpan(0, 0, 30);
      controlCdelegateData.CompletionEvent.WaitOne(timeout, false);
      if (!controlCdelegateData.DelegateStarted)
        return false;
      controlCdelegateData.CompletionEvent.WaitOne();
      controlCdelegateData.CompletionEvent.Close();
      return controlCdelegateData.Cancel;
    }

    private static void ControlCDelegate(object data)
    {
      Console.ControlCDelegateData controlCdelegateData = (Console.ControlCDelegateData) data;
      try
      {
        controlCdelegateData.DelegateStarted = true;
        ConsoleCancelEventArgs e = new ConsoleCancelEventArgs(controlCdelegateData.ControlKey);
        controlCdelegateData.CancelCallbacks((object) null, e);
        controlCdelegateData.Cancel = e.Cancel;
      }
      finally
      {
        controlCdelegateData.CompletionEvent.Set();
      }
    }

    /// <summary>
    ///   Возникает при одновременном нажатии клавиши-модификатора <see cref="F:System.ConsoleModifiers.Control" /> (Ctrl) и либо клавиши консоли <see cref="F:System.ConsoleKey.C" /> (C), либо клавиши Break (Ctrl+C или Ctrl+Break).
    /// </summary>
    public static event ConsoleCancelEventHandler CancelKeyPress
    {
      [SecuritySafeCritical] add
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        lock (Console.InternalSyncObject)
        {
          Console._cancelCallbacks += value;
          if (Console._hooker != null)
            return;
          Console._hooker = new Console.ControlCHooker();
          Console._hooker.Hook();
        }
      }
      [SecuritySafeCritical] remove
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        lock (Console.InternalSyncObject)
        {
          Console._cancelCallbacks -= value;
          if (Console._hooker == null || Console._cancelCallbacks != null)
            return;
          Console._hooker.Unhook();
        }
      }
    }

    /// <summary>Получает стандартный поток сообщений об ошибках.</summary>
    /// <returns>Стандартный поток сообщений об ошибках.</returns>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardError()
    {
      return Console.OpenStandardError(256);
    }

    /// <summary>
    ///   Получает стандартный поток сообщений об ошибках, для которого установлен заданный размер буфера.
    /// </summary>
    /// <param name="bufferSize">Размер буфера внутреннего потока.</param>
    /// <returns>Стандартный поток сообщений об ошибках.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="bufferSize" /> не больше нуля.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardError(int bufferSize)
    {
      if (bufferSize < 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return Console.GetStandardFile(-12, FileAccess.Write, bufferSize);
    }

    /// <summary>Получает стандартный входной поток.</summary>
    /// <returns>Стандартный входной поток.</returns>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardInput()
    {
      return Console.OpenStandardInput(256);
    }

    /// <summary>
    ///   Получает стандартный входной поток, для которого установлен заданный размер буфера.
    /// </summary>
    /// <param name="bufferSize">Размер буфера внутреннего потока.</param>
    /// <returns>Стандартный входной поток.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="bufferSize" /> меньше или равно нулю.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardInput(int bufferSize)
    {
      if (bufferSize < 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return Console.GetStandardFile(-10, FileAccess.Read, bufferSize);
    }

    /// <summary>Получает стандартный выходной поток.</summary>
    /// <returns>Стандартный выходной поток.</returns>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardOutput()
    {
      return Console.OpenStandardOutput(256);
    }

    /// <summary>
    ///   Получает стандартный выходной поток, для которого установлен заданный размер буфера.
    /// </summary>
    /// <param name="bufferSize">Размер буфера внутреннего потока.</param>
    /// <returns>Стандартный выходной поток.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="bufferSize" /> не больше нуля.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardOutput(int bufferSize)
    {
      if (bufferSize < 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return Console.GetStandardFile(-11, FileAccess.Write, bufferSize);
    }

    /// <summary>
    ///   Присваивает свойству <see cref="P:System.Console.In" /> указанный объект <see cref="T:System.IO.TextReader" />.
    /// </summary>
    /// <param name="newIn">
    ///   Поток, являющийся новым стандартным входным потоком.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="newIn" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void SetIn(TextReader newIn)
    {
      if (newIn == null)
        throw new ArgumentNullException(nameof (newIn));
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      newIn = TextReader.Synchronized(newIn);
      lock (Console.InternalSyncObject)
        Console._in = newIn;
    }

    /// <summary>
    ///   Присваивает свойству <see cref="P:System.Console.Out" /> указанный объект <see cref="T:System.IO.TextWriter" />.
    /// </summary>
    /// <param name="newOut">
    ///   Поток, являющийся новым стандартным выходным потоком.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="newOut" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void SetOut(TextWriter newOut)
    {
      if (newOut == null)
        throw new ArgumentNullException(nameof (newOut));
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      Console._isOutTextWriterRedirected = true;
      newOut = TextWriter.Synchronized(newOut);
      lock (Console.InternalSyncObject)
        Console._out = newOut;
    }

    /// <summary>
    ///   Присваивает свойству <see cref="P:System.Console.Error" /> указанный объект <see cref="T:System.IO.TextWriter" />.
    /// </summary>
    /// <param name="newError">
    ///   Поток, являющийся новым стандартным потоком сообщений об ошибках.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="newError" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void SetError(TextWriter newError)
    {
      if (newError == null)
        throw new ArgumentNullException(nameof (newError));
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      Console._isErrorTextWriterRedirected = true;
      newError = TextWriter.Synchronized(newError);
      lock (Console.InternalSyncObject)
        Console._error = newError;
    }

    /// <summary>
    ///   Читает следующий символ из стандартного входного потока.
    /// </summary>
    /// <returns>
    ///   Следующий символ из входного потока или значение минус единица (-1), если доступных для чтения символов не осталось.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static int Read()
    {
      return Console.In.Read();
    }

    /// <summary>
    ///   Считывает следующую строку символов из стандартного входного потока.
    /// </summary>
    /// <returns>
    ///   Следующая строка символов из входного потока или значение <see langword="null" />, если больше нет доступных строк.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти для выделения буфера для возвращаемой строки.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Количество символов в следующей строке символов больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static string ReadLine()
    {
      return Console.In.ReadLine();
    }

    /// <summary>
    ///   Записывает текущий признак конца строки в стандартный выходной поток.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine()
    {
      Console.Out.WriteLine();
    }

    /// <summary>
    ///   Записывает текстовое представление заданного логического значения с текущим признаком конца строки в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(bool value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает заданный знак Юникода, за которым следует текущий признак конца строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(char value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает заданный массив знаков Юникода, за которым следует текущий признак конца строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="buffer">Массив знаков Юникода.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(char[] buffer)
    {
      Console.Out.WriteLine(buffer);
    }

    /// <summary>
    ///   Записывает заданный подмассив знаков Юникода, за которым следует текущий признак конца строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="buffer">Массив знаков Юникода.</param>
    /// <param name="index">
    ///   Начальная позиция в массиве <paramref name="buffer" />.
    /// </param>
    /// <param name="count">Количество символов для записи.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="index" /> и <paramref name="count" /> указывает позицию, которая не находится в <paramref name="buffer" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(char[] buffer, int index, int count)
    {
      Console.Out.WriteLine(buffer, index, count);
    }

    /// <summary>
    ///   Записывает текстовое представление указанного значения <see cref="T:System.Decimal" />, за которым следует текущий знак завершения строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(Decimal value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного значения двойной точности с плавающей запятой, за которым следует признак конца строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(double value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного значения одинарной точности с плавающей запятой, за которым следует признак конца строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(float value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного 32-битового целого числа со знаком, за которым следует текущий знак завершения строки, в стандартный поток вывода.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(int value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного 32-битового целого числа без знака, за которым следует текущий признак конца строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(uint value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного 64-битового целого числа со знаком, за которым следует текущий знак завершения строки, в стандартный поток вывода.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(long value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного 64-битового целого числа без знака, за которым следует текущий признак конца строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(ulong value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного объекта, за которым следует текущий признак конца строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(object value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает заданное строковое значение, за которым следует текущий признак конца строки, в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного объекта, за которым следует текущий признак конца строки, в стандартный выходной поток с использованием заданных сведений о форматировании.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимая спецификация формата в <paramref name="format" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, object arg0)
    {
      Console.Out.WriteLine(format, arg0);
    }

    /// <summary>
    ///   Записывает текстовые представления заданных объектов, за которыми следует текущий признак конца строки, в стандартный выходной поток с использованием заданных сведений о форматировании.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Первый объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <param name="arg1">
    ///   Второй объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимая спецификация формата в <paramref name="format" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, object arg0, object arg1)
    {
      Console.Out.WriteLine(format, arg0, arg1);
    }

    /// <summary>
    ///   Записывает текстовые представления заданных объектов, за которыми следует текущий признак конца строки, в стандартный выходной поток с использованием заданных сведений о форматировании.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Первый объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <param name="arg1">
    ///   Второй объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <param name="arg2">
    ///   Третий объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимая спецификация формата в <paramref name="format" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, object arg0, object arg1, object arg2)
    {
      Console.Out.WriteLine(format, arg0, arg1, arg2);
    }

    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
    {
      ArgIterator argIterator = new ArgIterator(__arglist);
      int length = argIterator.GetRemainingCount() + 4;
      object[] objArray = new object[length];
      objArray[0] = arg0;
      objArray[1] = arg1;
      objArray[2] = arg2;
      objArray[3] = arg3;
      for (int index = 4; index < length; ++index)
        objArray[index] = TypedReference.ToObject(argIterator.GetNextArg());
      Console.Out.WriteLine(format, objArray);
    }

    /// <summary>
    ///   Записывает текстовые представления заданного массива объектов, за которым следует текущий признак конца строки, в стандартный выходной поток с использованием заданных сведений о форматировании.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg">
    ///   Массив объектов для записи с использованием <paramref name="format" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="format" /> или <paramref name="arg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимая спецификация формата в <paramref name="format" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, params object[] arg)
    {
      if (arg == null)
        Console.Out.WriteLine(format, (object) null, (object) null);
      else
        Console.Out.WriteLine(format, arg);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного объекта в стандартный выходной поток, используя заданные сведения о форматировании.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимая спецификация формата в <paramref name="format" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, object arg0)
    {
      Console.Out.Write(format, arg0);
    }

    /// <summary>
    ///   Записывает текстовые представления заданных объектов в стандартный выходной поток, используя заданные сведения о форматировании.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Первый объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <param name="arg1">
    ///   Второй объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимая спецификация формата в <paramref name="format" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, object arg0, object arg1)
    {
      Console.Out.Write(format, arg0, arg1);
    }

    /// <summary>
    ///   Записывает текстовые представления заданных объектов в стандартный выходной поток, используя заданные сведения о форматировании.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Первый объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <param name="arg1">
    ///   Второй объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <param name="arg2">
    ///   Третий объект для записи с использованием <paramref name="format" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимая спецификация формата в <paramref name="format" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, object arg0, object arg1, object arg2)
    {
      Console.Out.Write(format, arg0, arg1, arg2);
    }

    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
    {
      ArgIterator argIterator = new ArgIterator(__arglist);
      int length = argIterator.GetRemainingCount() + 4;
      object[] objArray = new object[length];
      objArray[0] = arg0;
      objArray[1] = arg1;
      objArray[2] = arg2;
      objArray[3] = arg3;
      for (int index = 4; index < length; ++index)
        objArray[index] = TypedReference.ToObject(argIterator.GetNextArg());
      Console.Out.Write(format, objArray);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного массива объектов в стандартный выходной поток, используя заданные сведения о форматировании.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg">
    ///   Массив объектов для записи с использованием <paramref name="format" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="format" /> или <paramref name="arg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимая спецификация формата в <paramref name="format" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, params object[] arg)
    {
      if (arg == null)
        Console.Out.Write(format, (object) null, (object) null);
      else
        Console.Out.Write(format, arg);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного логического значения в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(bool value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает значение заданного знака Юникода в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(char value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает заданный массив знаков Юникода в стандартный выходной поток.
    /// </summary>
    /// <param name="buffer">Массив знаков Юникода.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(char[] buffer)
    {
      Console.Out.Write(buffer);
    }

    /// <summary>
    ///   Записывает заданный дочерний массив знаков Юникода в стандартный выходной поток.
    /// </summary>
    /// <param name="buffer">Массив знаков Юникода.</param>
    /// <param name="index">
    ///   Начальная позиция в массиве <paramref name="buffer" />.
    /// </param>
    /// <param name="count">Количество символов для записи.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="index" /> и <paramref name="count" /> указывает позицию, которая не находится в <paramref name="buffer" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(char[] buffer, int index, int count)
    {
      Console.Out.Write(buffer, index, count);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного значения двойной точности с плавающей запятой в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(double value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного значения <see cref="T:System.Decimal" /> в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(Decimal value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного значения одинарной точности с плавающей запятой в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(float value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного 32-битового целого числа со знаком в стандартный поток вывода.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(int value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного 32-битового целого числа без знака в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(uint value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного 64-битового целого числа со знаком в стандартный поток вывода.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(long value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного 64-битового целого числа без знака в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(ulong value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает текстовое представление заданного объекта в стандартный выходной поток.
    /// </summary>
    /// <param name="value">
    ///   Записываемое значение или <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(object value)
    {
      Console.Out.Write(value);
    }

    /// <summary>
    ///   Записывает заданное строковое значение в стандартный выходной поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string value)
    {
      Console.Out.Write(value);
    }

    [Flags]
    internal enum ControlKeyState
    {
      RightAltPressed = 1,
      LeftAltPressed = 2,
      RightCtrlPressed = 4,
      LeftCtrlPressed = 8,
      ShiftPressed = 16, // 0x00000010
      NumLockOn = 32, // 0x00000020
      ScrollLockOn = 64, // 0x00000040
      CapsLockOn = 128, // 0x00000080
      EnhancedKey = 256, // 0x00000100
    }

    internal sealed class ControlCHooker : CriticalFinalizerObject
    {
      private bool _hooked;
      [SecurityCritical]
      private Win32Native.ConsoleCtrlHandlerRoutine _handler;

      [SecurityCritical]
      internal ControlCHooker()
      {
        this._handler = new Win32Native.ConsoleCtrlHandlerRoutine(Console.BreakEvent);
      }

      ~ControlCHooker()
      {
        this.Unhook();
      }

      [SecuritySafeCritical]
      internal void Hook()
      {
        if (this._hooked)
          return;
        if (!Win32Native.SetConsoleCtrlHandler(this._handler, true))
          __Error.WinIOError();
        this._hooked = true;
      }

      [SecuritySafeCritical]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      internal void Unhook()
      {
        if (!this._hooked)
          return;
        if (!Win32Native.SetConsoleCtrlHandler(this._handler, false))
          __Error.WinIOError();
        this._hooked = false;
      }
    }

    private sealed class ControlCDelegateData
    {
      internal ConsoleSpecialKey ControlKey;
      internal bool Cancel;
      internal bool DelegateStarted;
      internal ManualResetEvent CompletionEvent;
      internal ConsoleCancelEventHandler CancelCallbacks;

      internal ControlCDelegateData(ConsoleSpecialKey controlKey, ConsoleCancelEventHandler cancelCallbacks)
      {
        this.ControlKey = controlKey;
        this.CancelCallbacks = cancelCallbacks;
        this.CompletionEvent = new ManualResetEvent(false);
      }
    }
  }
}
