// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityRuntime
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Security
{
  internal class SecurityRuntime
  {
    internal const bool StackContinue = true;
    internal const bool StackHalt = false;

    private SecurityRuntime()
    {
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern FrameSecurityDescriptor GetSecurityObjectForFrame(ref StackCrawlMark stackMark, bool create);

    [SecurityCritical]
    internal static MethodInfo GetMethodInfo(RuntimeMethodHandleInternal rmh)
    {
      if (rmh.IsNullHandle())
        return (MethodInfo) null;
      PermissionSet.s_fullTrust.Assert();
      return RuntimeType.GetMethodBase(RuntimeMethodHandle.GetDeclaringType(rmh), rmh) as MethodInfo;
    }

    [SecurityCritical]
    private static bool FrameDescSetHelper(FrameSecurityDescriptor secDesc, PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
    {
      return secDesc.CheckSetDemand(demandSet, out alteredDemandSet, rmh);
    }

    [SecurityCritical]
    private static bool FrameDescHelper(FrameSecurityDescriptor secDesc, IPermission demandIn, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
    {
      return secDesc.CheckDemand((CodeAccessPermission) demandIn, permToken, rmh);
    }

    [SecurityCritical]
    private static bool CheckDynamicMethodSetHelper(DynamicResolver dynamicResolver, PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
    {
      CompressedStack securityContext = dynamicResolver.GetSecurityContext();
      try
      {
        return securityContext.CheckSetDemandWithModificationNoHalt(demandSet, out alteredDemandSet, rmh);
      }
      catch (SecurityException ex)
      {
        throw new SecurityException(Environment.GetResourceString("Security_AnonymouslyHostedDynamicMethodCheckFailed"), (Exception) ex);
      }
    }

    [SecurityCritical]
    private static bool CheckDynamicMethodHelper(DynamicResolver dynamicResolver, IPermission demandIn, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
    {
      CompressedStack securityContext = dynamicResolver.GetSecurityContext();
      try
      {
        return securityContext.CheckDemandNoHalt((CodeAccessPermission) demandIn, permToken, rmh);
      }
      catch (SecurityException ex)
      {
        throw new SecurityException(Environment.GetResourceString("Security_AnonymouslyHostedDynamicMethodCheckFailed"), (Exception) ex);
      }
    }

    [SecurityCritical]
    internal static void Assert(PermissionSet permSet, ref StackCrawlMark stackMark)
    {
      FrameSecurityDescriptor securityDescriptor = CodeAccessSecurityEngine.CheckNReturnSO(CodeAccessSecurityEngine.AssertPermissionToken, (CodeAccessPermission) CodeAccessSecurityEngine.AssertPermission, ref stackMark, 1);
      if (securityDescriptor == null)
      {
        Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      }
      else
      {
        if (securityDescriptor.HasImperativeAsserts())
          throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
        securityDescriptor.SetAssert(permSet);
      }
    }

    [SecurityCritical]
    internal static void AssertAllPossible(ref StackCrawlMark stackMark)
    {
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
      if (securityObjectForFrame == null)
      {
        Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      }
      else
      {
        if (securityObjectForFrame.GetAssertAllPossible())
          throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
        securityObjectForFrame.SetAssertAllPossible();
      }
    }

    [SecurityCritical]
    internal static void Deny(PermissionSet permSet, ref StackCrawlMark stackMark)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_CasDeny"));
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
      if (securityObjectForFrame == null)
      {
        Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      }
      else
      {
        if (securityObjectForFrame.HasImperativeDenials())
          throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
        securityObjectForFrame.SetDeny(permSet);
      }
    }

    [SecurityCritical]
    internal static void PermitOnly(PermissionSet permSet, ref StackCrawlMark stackMark)
    {
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
      if (securityObjectForFrame == null)
      {
        Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      }
      else
      {
        if (securityObjectForFrame.HasImperativeRestrictions())
          throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
        securityObjectForFrame.SetPermitOnly(permSet);
      }
    }

    [SecurityCritical]
    internal static void RevertAssert(ref StackCrawlMark stackMark)
    {
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
      if (securityObjectForFrame == null)
        throw new InvalidOperationException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      securityObjectForFrame.RevertAssert();
    }

    [SecurityCritical]
    internal static void RevertDeny(ref StackCrawlMark stackMark)
    {
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
      if (securityObjectForFrame == null)
        throw new InvalidOperationException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      securityObjectForFrame.RevertDeny();
    }

    [SecurityCritical]
    internal static void RevertPermitOnly(ref StackCrawlMark stackMark)
    {
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
      if (securityObjectForFrame == null)
        throw new InvalidOperationException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      securityObjectForFrame.RevertPermitOnly();
    }

    [SecurityCritical]
    internal static void RevertAll(ref StackCrawlMark stackMark)
    {
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
      if (securityObjectForFrame == null)
        throw new InvalidOperationException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      securityObjectForFrame.RevertAll();
    }
  }
}
