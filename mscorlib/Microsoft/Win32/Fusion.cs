// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.Fusion
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32
{
  internal static class Fusion
  {
    [SecurityCritical]
    public static void ReadCache(ArrayList alAssems, string name, uint nFlag)
    {
      IAssemblyEnum ppEnum1 = (IAssemblyEnum) null;
      IAssemblyName ppName = (IAssemblyName) null;
      IAssemblyName ppEnum2 = (IAssemblyName) null;
      IApplicationContext ppAppCtx = (IApplicationContext) null;
      if (name != null)
      {
        int assemblyNameObject = Win32Native.CreateAssemblyNameObject(out ppEnum2, name, 1U, IntPtr.Zero);
        if (assemblyNameObject != 0)
          Marshal.ThrowExceptionForHR(assemblyNameObject);
      }
      int assemblyEnum = Win32Native.CreateAssemblyEnum(out ppEnum1, ppAppCtx, ppEnum2, nFlag, IntPtr.Zero);
      if (assemblyEnum != 0)
        Marshal.ThrowExceptionForHR(assemblyEnum);
      while (true)
      {
        string displayName;
        do
        {
          int nextAssembly = ppEnum1.GetNextAssembly(out ppAppCtx, out ppName, 0U);
          if (nextAssembly != 0)
          {
            if (nextAssembly < 0)
            {
              Marshal.ThrowExceptionForHR(nextAssembly);
              return;
            }
            goto label_10;
          }
          else
            displayName = Fusion.GetDisplayName(ppName, 0U);
        }
        while (displayName == null);
        alAssems.Add((object) displayName);
      }
label_10:;
    }

    [SecuritySafeCritical]
    private static unsafe string GetDisplayName(IAssemblyName aName, uint dwDisplayFlags)
    {
      uint pccDisplayName = 0;
      string str = (string) null;
      aName.GetDisplayName((IntPtr) 0, ref pccDisplayName, dwDisplayFlags);
      if (pccDisplayName > 0U)
      {
        IntPtr num = (IntPtr) 0;
        fixed (byte* numPtr = new byte[((int) pccDisplayName + 1) * 2])
        {
          num = new IntPtr((void*) numPtr);
          aName.GetDisplayName(num, ref pccDisplayName, dwDisplayFlags);
          str = Marshal.PtrToStringUni(num);
        }
      }
      return str;
    }
  }
}
