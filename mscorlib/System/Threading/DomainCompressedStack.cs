// Decompiled with JetBrains decompiler
// Type: System.Threading.DomainCompressedStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
  [Serializable]
  internal sealed class DomainCompressedStack
  {
    private PermissionListSet m_pls;
    private bool m_bHaltConstruction;

    internal PermissionListSet PLS
    {
      get
      {
        return this.m_pls;
      }
    }

    internal bool ConstructionHalted
    {
      get
      {
        return this.m_bHaltConstruction;
      }
    }

    [SecurityCritical]
    private static DomainCompressedStack CreateManagedObject(IntPtr unmanagedDCS)
    {
      DomainCompressedStack domainCompressedStack = new DomainCompressedStack();
      domainCompressedStack.m_pls = PermissionListSet.CreateCompressedState(unmanagedDCS, out domainCompressedStack.m_bHaltConstruction);
      return domainCompressedStack;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetDescCount(IntPtr dcs);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void GetDomainPermissionSets(IntPtr dcs, out PermissionSet granted, out PermissionSet refused);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool GetDescriptorInfo(IntPtr dcs, int index, out PermissionSet granted, out PermissionSet refused, out Assembly assembly, out FrameSecurityDescriptor fsd);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IgnoreDomain(IntPtr dcs);
  }
}
