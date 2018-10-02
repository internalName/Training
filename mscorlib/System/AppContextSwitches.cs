// Decompiled with JetBrains decompiler
// Type: System.AppContextSwitches
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System
{
  internal static class AppContextSwitches
  {
    private static int _noAsyncCurrentCulture;
    private static int _throwExceptionIfDisposedCancellationTokenSource;
    private static int _preserveEventListnerObjectIdentity;
    private static int _useLegacyPathHandling;
    private static int _blockLongPaths;
    private static int _cloneActor;
    private static int _doNotAddrOfCspParentWindowHandle;
    private static int _ignorePortablePDBsInStackTraces;
    private static int _useNewMaxArraySize;

    public static bool NoAsyncCurrentCulture
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchNoAsyncCurrentCulture, ref AppContextSwitches._noAsyncCurrentCulture);
      }
    }

    public static bool ThrowExceptionIfDisposedCancellationTokenSource
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchThrowExceptionIfDisposedCancellationTokenSource, ref AppContextSwitches._throwExceptionIfDisposedCancellationTokenSource);
      }
    }

    public static bool PreserveEventListnerObjectIdentity
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchPreserveEventListnerObjectIdentity, ref AppContextSwitches._preserveEventListnerObjectIdentity);
      }
    }

    public static bool UseLegacyPathHandling
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchUseLegacyPathHandling, ref AppContextSwitches._useLegacyPathHandling);
      }
    }

    public static bool BlockLongPaths
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchBlockLongPaths, ref AppContextSwitches._blockLongPaths);
      }
    }

    public static bool SetActorAsReferenceWhenCopyingClaimsIdentity
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchSetActorAsReferenceWhenCopyingClaimsIdentity, ref AppContextSwitches._cloneActor);
      }
    }

    public static bool DoNotAddrOfCspParentWindowHandle
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchDoNotAddrOfCspParentWindowHandle, ref AppContextSwitches._doNotAddrOfCspParentWindowHandle);
      }
    }

    public static bool IgnorePortablePDBsInStackTraces
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchIgnorePortablePDBsInStackTraces, ref AppContextSwitches._ignorePortablePDBsInStackTraces);
      }
    }

    public static bool UseNewMaxArraySize
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchUseNewMaxArraySize, ref AppContextSwitches._useNewMaxArraySize);
      }
    }

    private static bool DisableCaching { get; set; }

    static AppContextSwitches()
    {
      bool isEnabled;
      if (!AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out isEnabled))
        return;
      AppContextSwitches.DisableCaching = isEnabled;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
    {
      if (switchValue < 0)
        return false;
      if (switchValue > 0)
        return true;
      return AppContextSwitches.GetCachedSwitchValueInternal(switchName, ref switchValue);
    }

    private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
    {
      bool isEnabled;
      AppContext.TryGetSwitch(switchName, out isEnabled);
      if (AppContextSwitches.DisableCaching)
        return isEnabled;
      switchValue = isEnabled ? 1 : -1;
      return isEnabled;
    }
  }
}
