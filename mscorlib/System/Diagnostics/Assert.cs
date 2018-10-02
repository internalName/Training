// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Assert
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
  internal static class Assert
  {
    private static AssertFilter Filter = (AssertFilter) new DefaultFilter();
    internal const int COR_E_FAILFAST = -2146232797;

    internal static void Check(bool condition, string conditionString, string message)
    {
      if (condition)
        return;
      Assert.Fail(conditionString, message, (string) null, -2146232797);
    }

    internal static void Check(bool condition, string conditionString, string message, int exitCode)
    {
      if (condition)
        return;
      Assert.Fail(conditionString, message, (string) null, exitCode);
    }

    internal static void Fail(string conditionString, string message)
    {
      Assert.Fail(conditionString, message, (string) null, -2146232797);
    }

    internal static void Fail(string conditionString, string message, string windowTitle, int exitCode)
    {
      Assert.Fail(conditionString, message, windowTitle, exitCode, StackTrace.TraceFormat.Normal, 0);
    }

    internal static void Fail(string conditionString, string message, int exitCode, StackTrace.TraceFormat stackTraceFormat)
    {
      Assert.Fail(conditionString, message, (string) null, exitCode, stackTraceFormat, 0);
    }

    [SecuritySafeCritical]
    internal static void Fail(string conditionString, string message, string windowTitle, int exitCode, StackTrace.TraceFormat stackTraceFormat, int numStackFramesToSkip)
    {
      StackTrace location = new StackTrace(numStackFramesToSkip, true);
      switch (Assert.Filter.AssertFailure(conditionString, message, location, stackTraceFormat, windowTitle))
      {
        case AssertFilters.FailDebug:
          if (Debugger.IsAttached)
          {
            Debugger.Break();
            break;
          }
          if (Debugger.Launch())
            break;
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DebuggerLaunchFailed"));
        case AssertFilters.FailTerminate:
          if (Debugger.IsAttached)
          {
            Environment._Exit(exitCode);
            break;
          }
          Environment.FailFast(message, (uint) exitCode);
          break;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int ShowDefaultAssertDialog(string conditionString, string message, string stackTrace, string windowTitle);
  }
}
