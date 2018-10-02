// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Log
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
  internal static class Log
  {
    internal static Hashtable m_Hashtable = new Hashtable();
    private static volatile bool m_fConsoleDeviceEnabled = false;
    private static object locker = new object();
    public static readonly LogSwitch GlobalSwitch = new LogSwitch("Global", "Global Switch for this log");
    private static LogMessageEventHandler _LogMessageEventHandler;
    private static volatile LogSwitchLevelHandler _LogSwitchLevelHandler;

    static Log()
    {
      Log.GlobalSwitch.MinimumLevel = LoggingLevels.ErrorLevel;
    }

    public static void AddOnLogMessage(LogMessageEventHandler handler)
    {
      lock (Log.locker)
        Log._LogMessageEventHandler += handler;
    }

    public static void RemoveOnLogMessage(LogMessageEventHandler handler)
    {
      lock (Log.locker)
        Log._LogMessageEventHandler -= handler;
    }

    public static void AddOnLogSwitchLevel(LogSwitchLevelHandler handler)
    {
      lock (Log.locker)
        Log._LogSwitchLevelHandler += handler;
    }

    public static void RemoveOnLogSwitchLevel(LogSwitchLevelHandler handler)
    {
      lock (Log.locker)
        Log._LogSwitchLevelHandler -= handler;
    }

    internal static void InvokeLogSwitchLevelHandlers(LogSwitch ls, LoggingLevels newLevel)
    {
      LogSwitchLevelHandler switchLevelHandler = Log._LogSwitchLevelHandler;
      if (switchLevelHandler == null)
        return;
      switchLevelHandler(ls, newLevel);
    }

    public static bool IsConsoleEnabled
    {
      get
      {
        return Log.m_fConsoleDeviceEnabled;
      }
      set
      {
        Log.m_fConsoleDeviceEnabled = value;
      }
    }

    public static void LogMessage(LoggingLevels level, string message)
    {
      Log.LogMessage(level, Log.GlobalSwitch, message);
    }

    public static void LogMessage(LoggingLevels level, LogSwitch logswitch, string message)
    {
      if (logswitch == null)
        throw new ArgumentNullException("LogSwitch");
      if (level < LoggingLevels.TraceLevel0)
        throw new ArgumentOutOfRangeException(nameof (level), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (!logswitch.CheckLevel(level))
        return;
      Debugger.Log((int) level, logswitch.strName, message);
      if (!Log.m_fConsoleDeviceEnabled)
        return;
      Console.Write(message);
    }

    public static void Trace(LogSwitch logswitch, string message)
    {
      Log.LogMessage(LoggingLevels.TraceLevel0, logswitch, message);
    }

    public static void Trace(string switchname, string message)
    {
      Log.LogMessage(LoggingLevels.TraceLevel0, LogSwitch.GetSwitch(switchname), message);
    }

    public static void Trace(string message)
    {
      Log.LogMessage(LoggingLevels.TraceLevel0, Log.GlobalSwitch, message);
    }

    public static void Status(LogSwitch logswitch, string message)
    {
      Log.LogMessage(LoggingLevels.StatusLevel0, logswitch, message);
    }

    public static void Status(string switchname, string message)
    {
      Log.LogMessage(LoggingLevels.StatusLevel0, LogSwitch.GetSwitch(switchname), message);
    }

    public static void Status(string message)
    {
      Log.LogMessage(LoggingLevels.StatusLevel0, Log.GlobalSwitch, message);
    }

    public static void Warning(LogSwitch logswitch, string message)
    {
      Log.LogMessage(LoggingLevels.WarningLevel, logswitch, message);
    }

    public static void Warning(string switchname, string message)
    {
      Log.LogMessage(LoggingLevels.WarningLevel, LogSwitch.GetSwitch(switchname), message);
    }

    public static void Warning(string message)
    {
      Log.LogMessage(LoggingLevels.WarningLevel, Log.GlobalSwitch, message);
    }

    public static void Error(LogSwitch logswitch, string message)
    {
      Log.LogMessage(LoggingLevels.ErrorLevel, logswitch, message);
    }

    public static void Error(string switchname, string message)
    {
      Log.LogMessage(LoggingLevels.ErrorLevel, LogSwitch.GetSwitch(switchname), message);
    }

    public static void Error(string message)
    {
      Log.LogMessage(LoggingLevels.ErrorLevel, Log.GlobalSwitch, message);
    }

    public static void Panic(string message)
    {
      Log.LogMessage(LoggingLevels.PanicLevel, Log.GlobalSwitch, message);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void AddLogSwitch(LogSwitch logSwitch);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ModifyLogSwitch(int iNewLevel, string strSwitchName, string strParentName);
  }
}
