// Decompiled with JetBrains decompiler
// Type: System.Security.Util.Config
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security.Util
{
  internal static class Config
  {
    private static volatile string m_machineConfig;
    private static volatile string m_userConfig;

    [SecurityCritical]
    private static void GetFileLocales()
    {
      if (Config.m_machineConfig == null)
      {
        string s = (string) null;
        Config.GetMachineDirectory(JitHelpers.GetStringHandleOnStack(ref s));
        Config.m_machineConfig = s;
      }
      if (Config.m_userConfig != null)
        return;
      string s1 = (string) null;
      Config.GetUserDirectory(JitHelpers.GetStringHandleOnStack(ref s1));
      Config.m_userConfig = s1;
    }

    internal static string MachineDirectory
    {
      [SecurityCritical] get
      {
        Config.GetFileLocales();
        return Config.m_machineConfig;
      }
    }

    internal static string UserDirectory
    {
      [SecurityCritical] get
      {
        Config.GetFileLocales();
        return Config.m_userConfig;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int SaveDataByte(string path, [In] byte[] data, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool RecoverData(ConfigId id);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetQuickCache(ConfigId id, QuickCacheEntryType quickCacheFlags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool GetCacheEntry(ConfigId id, int numKey, [In] byte[] key, int keyLength, ObjectHandleOnStack retData);

    [SecurityCritical]
    internal static bool GetCacheEntry(ConfigId id, int numKey, byte[] key, out byte[] data)
    {
      byte[] o = (byte[]) null;
      bool cacheEntry = Config.GetCacheEntry(id, numKey, key, key.Length, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      data = o;
      return cacheEntry;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddCacheEntry(ConfigId id, int numKey, [In] byte[] key, int keyLength, byte[] data, int dataLength);

    [SecurityCritical]
    internal static void AddCacheEntry(ConfigId id, int numKey, byte[] key, byte[] data)
    {
      Config.AddCacheEntry(id, numKey, key, key.Length, data, data.Length);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void ResetCacheData(ConfigId id);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetMachineDirectory(StringHandleOnStack retDirectory);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetUserDirectory(StringHandleOnStack retDirectory);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool WriteToEventLog(string message);
  }
}
