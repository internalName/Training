// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.RuntimeInformation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет сведения об установке среды выполнения .NET.
  /// </summary>
  public static class RuntimeInformation
  {
    private static string s_osDescription = (string) null;
    private static object s_osLock = new object();
    private static object s_processLock = new object();
    private static Architecture? s_osArch = new Architecture?();
    private static Architecture? s_processArch = new Architecture?();
    private const string FrameworkName = ".NET Framework";
    private static string s_frameworkDescription;

    /// <summary>
    ///   Возвращает строку, указывающую имя установки .NET, в которой выполняется приложение.
    /// </summary>
    /// <returns>
    ///   Имя установки .NET, в которой выполняется приложение.
    /// </returns>
    public static string FrameworkDescription
    {
      get
      {
        if (RuntimeInformation.s_frameworkDescription == null)
          RuntimeInformation.s_frameworkDescription = string.Format("{0} {1}", (object) ".NET Framework", (object) ((AssemblyFileVersionAttribute) typeof (object).GetTypeInfo().Assembly.GetCustomAttribute(typeof (AssemblyFileVersionAttribute))).Version);
        return RuntimeInformation.s_frameworkDescription;
      }
    }

    /// <summary>
    ///   Указывает, выполняется ли текущее приложение на указанной платформе.
    /// </summary>
    /// <param name="osPlatform">Платформа.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее приложение выполняется на указанной платформе; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool IsOSPlatform(OSPlatform osPlatform)
    {
      return OSPlatform.Windows == osPlatform;
    }

    /// <summary>
    ///   Получает строку, указывающую имя операционной системы, в которой выполняется приложение.
    /// </summary>
    /// <returns>
    ///   Имя операционной системы, в которой выполняется приложение.
    /// </returns>
    public static string OSDescription
    {
      [SecuritySafeCritical] get
      {
        if (RuntimeInformation.s_osDescription == null)
          RuntimeInformation.s_osDescription = RuntimeInformation.RtlGetVersion();
        return RuntimeInformation.s_osDescription;
      }
    }

    /// <summary>
    ///   Получает архитектуру платформы, в которой выполняется текущее приложение.
    /// </summary>
    /// <returns>
    ///   Архитектура платформы, в которой выполняется текущее приложение.
    /// </returns>
    public static Architecture OSArchitecture
    {
      [SecuritySafeCritical] get
      {
        lock (RuntimeInformation.s_osLock)
        {
          if (!RuntimeInformation.s_osArch.HasValue)
          {
            Win32Native.SYSTEM_INFO lpSystemInfo;
            Win32Native.GetNativeSystemInfo(out lpSystemInfo);
            RuntimeInformation.s_osArch = new Architecture?(RuntimeInformation.GetArchitecture(lpSystemInfo.wProcessorArchitecture));
          }
        }
        return RuntimeInformation.s_osArch.Value;
      }
    }

    /// <summary>
    ///   Получает архитектуру процесса выполняющегося приложения.
    /// </summary>
    /// <returns>Архитектура процесса выполняющегося приложения.</returns>
    public static Architecture ProcessArchitecture
    {
      [SecuritySafeCritical] get
      {
        lock (RuntimeInformation.s_processLock)
        {
          if (!RuntimeInformation.s_processArch.HasValue)
          {
            Win32Native.SYSTEM_INFO lpSystemInfo = new Win32Native.SYSTEM_INFO();
            Win32Native.GetSystemInfo(ref lpSystemInfo);
            RuntimeInformation.s_processArch = new Architecture?(RuntimeInformation.GetArchitecture(lpSystemInfo.wProcessorArchitecture));
          }
        }
        return RuntimeInformation.s_processArch.Value;
      }
    }

    private static Architecture GetArchitecture(ushort wProcessorArchitecture)
    {
      Architecture architecture = Architecture.X86;
      switch (wProcessorArchitecture)
      {
        case 0:
          architecture = Architecture.X86;
          break;
        case 5:
          architecture = Architecture.Arm;
          break;
        case 9:
          architecture = Architecture.X64;
          break;
        case 12:
          architecture = Architecture.Arm64;
          break;
      }
      return architecture;
    }

    [SecuritySafeCritical]
    private static string RtlGetVersion()
    {
      Win32Native.RTL_OSVERSIONINFOEX lpVersionInformation = new Win32Native.RTL_OSVERSIONINFOEX();
      lpVersionInformation.dwOSVersionInfoSize = (uint) Marshal.SizeOf<Win32Native.RTL_OSVERSIONINFOEX>(lpVersionInformation);
      if (Win32Native.RtlGetVersion(out lpVersionInformation) != 0)
        return "Microsoft Windows";
      return string.Format("{0} {1}.{2}.{3} {4}", (object) "Microsoft Windows", (object) lpVersionInformation.dwMajorVersion, (object) lpVersionInformation.dwMinorVersion, (object) lpVersionInformation.dwBuildNumber, (object) lpVersionInformation.szCSDVersion);
    }
  }
}
