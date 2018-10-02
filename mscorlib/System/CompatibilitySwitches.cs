// Decompiled with JetBrains decompiler
// Type: System.CompatibilitySwitches
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System
{
  [FriendAccessAllowed]
  internal static class CompatibilitySwitches
  {
    private static bool s_AreSwitchesSet;
    private static bool s_isNetFx40TimeSpanLegacyFormatMode;
    private static bool s_isNetFx40LegacySecurityPolicy;
    private static bool s_isNetFx45LegacyManagedDeflateStream;

    public static bool IsCompatibilityBehaviorDefined
    {
      get
      {
        return CompatibilitySwitches.s_AreSwitchesSet;
      }
    }

    private static bool IsCompatibilitySwitchSet(string compatibilitySwitch)
    {
      bool? nullable = AppDomain.CurrentDomain.IsCompatibilitySwitchSet(compatibilitySwitch);
      if (nullable.HasValue)
        return nullable.Value;
      return false;
    }

    internal static void InitializeSwitches()
    {
      CompatibilitySwitches.s_isNetFx40TimeSpanLegacyFormatMode = CompatibilitySwitches.IsCompatibilitySwitchSet("NetFx40_TimeSpanLegacyFormatMode");
      CompatibilitySwitches.s_isNetFx40LegacySecurityPolicy = CompatibilitySwitches.IsCompatibilitySwitchSet("NetFx40_LegacySecurityPolicy");
      CompatibilitySwitches.s_isNetFx45LegacyManagedDeflateStream = CompatibilitySwitches.IsCompatibilitySwitchSet("NetFx45_LegacyManagedDeflateStream");
      CompatibilitySwitches.s_AreSwitchesSet = true;
    }

    public static bool IsAppEarlierThanSilverlight4
    {
      get
      {
        return false;
      }
    }

    public static bool IsAppEarlierThanWindowsPhone8
    {
      get
      {
        return false;
      }
    }

    public static bool IsAppEarlierThanWindowsPhoneMango
    {
      get
      {
        return false;
      }
    }

    public static bool IsNetFx40TimeSpanLegacyFormatMode
    {
      get
      {
        return CompatibilitySwitches.s_isNetFx40TimeSpanLegacyFormatMode;
      }
    }

    public static bool IsNetFx40LegacySecurityPolicy
    {
      get
      {
        return CompatibilitySwitches.s_isNetFx40LegacySecurityPolicy;
      }
    }

    public static bool IsNetFx45LegacyManagedDeflateStream
    {
      get
      {
        return CompatibilitySwitches.s_isNetFx45LegacyManagedDeflateStream;
      }
    }
  }
}
