// Decompiled with JetBrains decompiler
// Type: System.BCLDebug
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System
{
  internal static class BCLDebug
  {
    internal static volatile bool m_registryChecked = false;
    internal static volatile bool m_loggingNotEnabled = false;
    private static readonly SwitchStructure[] switches = new SwitchStructure[14]
    {
      new SwitchStructure("NLS", 1),
      new SwitchStructure("SER", 2),
      new SwitchStructure("DYNIL", 4),
      new SwitchStructure("REMOTE", 8),
      new SwitchStructure("BINARY", 16),
      new SwitchStructure("SOAP", 32),
      new SwitchStructure("REMOTINGCHANNELS", 64),
      new SwitchStructure("CACHE", 128),
      new SwitchStructure("RESMGRFILEFORMAT", 256),
      new SwitchStructure("PERF", 512),
      new SwitchStructure("CORRECTNESS", 1024),
      new SwitchStructure("MEMORYFAILPOINT", 2048),
      new SwitchStructure("DATETIME", 4096),
      new SwitchStructure("INTEROP", 8192)
    };
    private static readonly LogLevel[] levelConversions = new LogLevel[11]
    {
      LogLevel.Panic,
      LogLevel.Error,
      LogLevel.Error,
      LogLevel.Warning,
      LogLevel.Warning,
      LogLevel.Status,
      LogLevel.Status,
      LogLevel.Trace,
      LogLevel.Trace,
      LogLevel.Trace,
      LogLevel.Trace
    };
    internal static bool m_perfWarnings;
    internal static bool m_correctnessWarnings;
    internal static bool m_safeHandleStackTraces;
    internal static volatile PermissionSet m_MakeConsoleErrorLoggingWork;

    [Conditional("_DEBUG")]
    public static void Assert(bool condition, string message)
    {
    }

    [Conditional("_LOGGING")]
    [SecuritySafeCritical]
    public static void Log(string message)
    {
      if (AppDomain.CurrentDomain.IsUnloadingForcedFinalize())
        return;
      if (!BCLDebug.m_registryChecked)
        BCLDebug.CheckRegistry();
      Log.Trace(message);
      Log.Trace(Environment.NewLine);
    }

    [Conditional("_LOGGING")]
    [SecuritySafeCritical]
    public static void Log(string switchName, string message)
    {
      if (AppDomain.CurrentDomain.IsUnloadingForcedFinalize())
        return;
      if (!BCLDebug.m_registryChecked)
        BCLDebug.CheckRegistry();
      try
      {
        LogSwitch logswitch = LogSwitch.GetSwitch(switchName);
        if (logswitch == null)
          return;
        Log.Trace(logswitch, message);
        Log.Trace(logswitch, Environment.NewLine);
      }
      catch
      {
        Log.Trace("Exception thrown in logging." + Environment.NewLine);
        Log.Trace("Switch was: " + (switchName == null ? "<null>" : switchName) + Environment.NewLine);
        Log.Trace("Message was: " + (message == null ? "<null>" : message) + Environment.NewLine);
      }
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetRegistryLoggingValues(out bool loggingEnabled, out bool logToConsole, out int logLevel, out bool perfWarnings, out bool correctnessWarnings, out bool safeHandleStackTraces);

    [SecuritySafeCritical]
    private static void CheckRegistry()
    {
      if (AppDomain.CurrentDomain.IsUnloadingForcedFinalize() || BCLDebug.m_registryChecked)
        return;
      BCLDebug.m_registryChecked = true;
      bool loggingEnabled;
      bool logToConsole;
      int logLevel;
      int registryLoggingValues = BCLDebug.GetRegistryLoggingValues(out loggingEnabled, out logToConsole, out logLevel, out BCLDebug.m_perfWarnings, out BCLDebug.m_correctnessWarnings, out BCLDebug.m_safeHandleStackTraces);
      if (!loggingEnabled)
        BCLDebug.m_loggingNotEnabled = true;
      if (!loggingEnabled)
        return;
      if (BCLDebug.levelConversions == null)
        return;
      try
      {
        int levelConversion = (int) BCLDebug.levelConversions[logLevel];
        if (registryLoggingValues <= 0)
          return;
        for (int index = 0; index < BCLDebug.switches.Length; ++index)
        {
          if ((BCLDebug.switches[index].value & registryLoggingValues) != 0)
            new LogSwitch(BCLDebug.switches[index].name, BCLDebug.switches[index].name, Log.GlobalSwitch).MinimumLevel = (LoggingLevels) levelConversion;
        }
        Log.GlobalSwitch.MinimumLevel = (LoggingLevels) levelConversion;
        Log.IsConsoleEnabled = logToConsole;
      }
      catch
      {
      }
    }

    [SecuritySafeCritical]
    internal static bool CheckEnabled(string switchName)
    {
      if (AppDomain.CurrentDomain.IsUnloadingForcedFinalize())
        return false;
      if (!BCLDebug.m_registryChecked)
        BCLDebug.CheckRegistry();
      LogSwitch logSwitch = LogSwitch.GetSwitch(switchName);
      if (logSwitch == null)
        return false;
      return logSwitch.MinimumLevel <= LoggingLevels.TraceLevel0;
    }

    [SecuritySafeCritical]
    private static bool CheckEnabled(string switchName, LogLevel level, out LogSwitch logSwitch)
    {
      if (AppDomain.CurrentDomain.IsUnloadingForcedFinalize())
      {
        logSwitch = (LogSwitch) null;
        return false;
      }
      logSwitch = LogSwitch.GetSwitch(switchName);
      if (logSwitch == null)
        return false;
      return logSwitch.MinimumLevel <= (LoggingLevels) level;
    }

    [Conditional("_LOGGING")]
    [SecuritySafeCritical]
    public static void Log(string switchName, LogLevel level, params object[] messages)
    {
      if (AppDomain.CurrentDomain.IsUnloadingForcedFinalize())
        return;
      if (!BCLDebug.m_registryChecked)
        BCLDebug.CheckRegistry();
      LogSwitch logSwitch;
      if (!BCLDebug.CheckEnabled(switchName, level, out logSwitch))
        return;
      StringBuilder sb = StringBuilderCache.Acquire(16);
      for (int index = 0; index < messages.Length; ++index)
      {
        string str;
        try
        {
          str = messages[index] != null ? messages[index].ToString() : "<null>";
        }
        catch
        {
          str = "<unable to convert>";
        }
        sb.Append(str);
      }
      Log.LogMessage((LoggingLevels) level, logSwitch, StringBuilderCache.GetStringAndRelease(sb));
    }

    [Conditional("_LOGGING")]
    public static void Trace(string switchName, params object[] messages)
    {
      LogSwitch logSwitch;
      if (BCLDebug.m_loggingNotEnabled || !BCLDebug.CheckEnabled(switchName, LogLevel.Trace, out logSwitch))
        return;
      StringBuilder sb = StringBuilderCache.Acquire(16);
      for (int index = 0; index < messages.Length; ++index)
      {
        string str;
        try
        {
          str = messages[index] != null ? messages[index].ToString() : "<null>";
        }
        catch
        {
          str = "<unable to convert>";
        }
        sb.Append(str);
      }
      sb.Append(Environment.NewLine);
      Log.LogMessage(LoggingLevels.TraceLevel0, logSwitch, StringBuilderCache.GetStringAndRelease(sb));
    }

    [Conditional("_LOGGING")]
    public static void Trace(string switchName, string format, params object[] messages)
    {
      LogSwitch logSwitch;
      if (BCLDebug.m_loggingNotEnabled || !BCLDebug.CheckEnabled(switchName, LogLevel.Trace, out logSwitch))
        return;
      StringBuilder sb = StringBuilderCache.Acquire(16);
      sb.AppendFormat(format, messages);
      sb.Append(Environment.NewLine);
      Log.LogMessage(LoggingLevels.TraceLevel0, logSwitch, StringBuilderCache.GetStringAndRelease(sb));
    }

    [Conditional("_LOGGING")]
    public static void DumpStack(string switchName)
    {
      if (!BCLDebug.m_registryChecked)
        BCLDebug.CheckRegistry();
      LogSwitch logSwitch;
      if (!BCLDebug.CheckEnabled(switchName, LogLevel.Trace, out logSwitch))
        return;
      StackTrace stackTrace = new StackTrace();
      Log.LogMessage(LoggingLevels.TraceLevel0, logSwitch, stackTrace.ToString());
    }

    [SecuritySafeCritical]
    [Conditional("_DEBUG")]
    internal static void ConsoleError(string msg)
    {
      if (AppDomain.CurrentDomain.IsUnloadingForcedFinalize())
        return;
      if (BCLDebug.m_MakeConsoleErrorLoggingWork == null)
      {
        PermissionSet permissionSet = new PermissionSet();
        permissionSet.AddPermission((IPermission) new EnvironmentPermission(PermissionState.Unrestricted));
        permissionSet.AddPermission((IPermission) new FileIOPermission(FileIOPermissionAccess.AllAccess, Path.GetFullPath(".")));
        BCLDebug.m_MakeConsoleErrorLoggingWork = permissionSet;
      }
      BCLDebug.m_MakeConsoleErrorLoggingWork.Assert();
      using (TextWriter textWriter = (TextWriter) File.AppendText("ConsoleErrors.log"))
        textWriter.WriteLine(msg);
    }

    [Conditional("_DEBUG")]
    [SecuritySafeCritical]
    internal static void Perf(bool expr, string msg)
    {
      if (AppDomain.CurrentDomain.IsUnloadingForcedFinalize())
        return;
      if (!BCLDebug.m_registryChecked)
        BCLDebug.CheckRegistry();
      if (!BCLDebug.m_perfWarnings)
        return;
      int num = expr ? 1 : 0;
      Assert.Check(expr, "BCL Perf Warning: Your perf may be less than perfect because...", msg);
    }

    [Conditional("_DEBUG")]
    internal static void Correctness(bool expr, string msg)
    {
    }

    [SecuritySafeCritical]
    internal static bool CorrectnessEnabled()
    {
      if (AppDomain.CurrentDomain.IsUnloadingForcedFinalize())
        return false;
      if (!BCLDebug.m_registryChecked)
        BCLDebug.CheckRegistry();
      return BCLDebug.m_correctnessWarnings;
    }

    internal static bool SafeHandleStackTracesEnabled
    {
      get
      {
        return false;
      }
    }
  }
}
