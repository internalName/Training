// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.CompatibilitySwitch
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.Versioning
{
  public static class CompatibilitySwitch
  {
    [SecurityCritical]
    public static bool IsEnabled(string compatibilitySwitchName)
    {
      return CompatibilitySwitch.IsEnabledInternalCall(compatibilitySwitchName, true);
    }

    [SecurityCritical]
    public static string GetValue(string compatibilitySwitchName)
    {
      return CompatibilitySwitch.GetValueInternalCall(compatibilitySwitchName, true);
    }

    [SecurityCritical]
    internal static bool IsEnabledInternal(string compatibilitySwitchName)
    {
      return CompatibilitySwitch.IsEnabledInternalCall(compatibilitySwitchName, false);
    }

    [SecurityCritical]
    internal static string GetValueInternal(string compatibilitySwitchName)
    {
      return CompatibilitySwitch.GetValueInternalCall(compatibilitySwitchName, false);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetAppContextOverridesInternalCall();

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsEnabledInternalCall(string compatibilitySwitchName, bool onlyDB);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern string GetValueInternalCall(string compatibilitySwitchName, bool onlyDB);
  }
}
