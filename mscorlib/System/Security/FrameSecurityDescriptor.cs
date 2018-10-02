// Decompiled with JetBrains decompiler
// Type: System.Security.FrameSecurityDescriptor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Security
{
  [Serializable]
  internal class FrameSecurityDescriptor
  {
    private PermissionSet m_assertions;
    private PermissionSet m_denials;
    private PermissionSet m_restriction;
    private PermissionSet m_DeclarativeAssertions;
    private PermissionSet m_DeclarativeDenials;
    private PermissionSet m_DeclarativeRestrictions;
    [SecurityCritical]
    [NonSerialized]
    private SafeAccessTokenHandle m_callerToken;
    [SecurityCritical]
    [NonSerialized]
    private SafeAccessTokenHandle m_impToken;
    private bool m_AssertFT;
    private bool m_assertAllPossible;
    private bool m_declSecComputed;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void IncrementOverridesCount();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void DecrementOverridesCount();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void IncrementAssertCount();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void DecrementAssertCount();

    internal FrameSecurityDescriptor()
    {
    }

    private PermissionSet CreateSingletonSet(IPermission perm)
    {
      PermissionSet permissionSet = new PermissionSet(false);
      permissionSet.AddPermission(perm.Copy());
      return permissionSet;
    }

    internal bool HasImperativeAsserts()
    {
      return this.m_assertions != null;
    }

    internal bool HasImperativeDenials()
    {
      return this.m_denials != null;
    }

    internal bool HasImperativeRestrictions()
    {
      return this.m_restriction != null;
    }

    [SecurityCritical]
    internal void SetAssert(IPermission perm)
    {
      this.m_assertions = this.CreateSingletonSet(perm);
      FrameSecurityDescriptor.IncrementAssertCount();
    }

    [SecurityCritical]
    internal void SetAssert(PermissionSet permSet)
    {
      this.m_assertions = permSet.Copy();
      this.m_AssertFT = this.m_AssertFT || this.m_assertions.IsUnrestricted();
      FrameSecurityDescriptor.IncrementAssertCount();
    }

    internal PermissionSet GetAssertions(bool fDeclarative)
    {
      if (!fDeclarative)
        return this.m_assertions;
      return this.m_DeclarativeAssertions;
    }

    [SecurityCritical]
    internal void SetAssertAllPossible()
    {
      this.m_assertAllPossible = true;
      FrameSecurityDescriptor.IncrementAssertCount();
    }

    internal bool GetAssertAllPossible()
    {
      return this.m_assertAllPossible;
    }

    [SecurityCritical]
    internal void SetDeny(IPermission perm)
    {
      this.m_denials = this.CreateSingletonSet(perm);
      FrameSecurityDescriptor.IncrementOverridesCount();
    }

    [SecurityCritical]
    internal void SetDeny(PermissionSet permSet)
    {
      this.m_denials = permSet.Copy();
      FrameSecurityDescriptor.IncrementOverridesCount();
    }

    internal PermissionSet GetDenials(bool fDeclarative)
    {
      if (!fDeclarative)
        return this.m_denials;
      return this.m_DeclarativeDenials;
    }

    [SecurityCritical]
    internal void SetPermitOnly(IPermission perm)
    {
      this.m_restriction = this.CreateSingletonSet(perm);
      FrameSecurityDescriptor.IncrementOverridesCount();
    }

    [SecurityCritical]
    internal void SetPermitOnly(PermissionSet permSet)
    {
      this.m_restriction = permSet.Copy();
      FrameSecurityDescriptor.IncrementOverridesCount();
    }

    internal PermissionSet GetPermitOnly(bool fDeclarative)
    {
      if (!fDeclarative)
        return this.m_restriction;
      return this.m_DeclarativeRestrictions;
    }

    [SecurityCritical]
    internal void SetTokenHandles(SafeAccessTokenHandle callerToken, SafeAccessTokenHandle impToken)
    {
      if (this.m_callerToken != null && !this.m_callerToken.IsInvalid)
        this.m_callerToken.Dispose();
      this.m_callerToken = callerToken;
      this.m_impToken = impToken;
    }

    [SecurityCritical]
    internal void RevertAssert()
    {
      if (this.m_assertions != null)
      {
        this.m_assertions = (PermissionSet) null;
        FrameSecurityDescriptor.DecrementAssertCount();
      }
      if (this.m_DeclarativeAssertions != null)
        this.m_AssertFT = this.m_DeclarativeAssertions.IsUnrestricted();
      else
        this.m_AssertFT = false;
    }

    [SecurityCritical]
    internal void RevertAssertAllPossible()
    {
      if (!this.m_assertAllPossible)
        return;
      this.m_assertAllPossible = false;
      FrameSecurityDescriptor.DecrementAssertCount();
    }

    [SecurityCritical]
    internal void RevertDeny()
    {
      if (!this.HasImperativeDenials())
        return;
      FrameSecurityDescriptor.DecrementOverridesCount();
      this.m_denials = (PermissionSet) null;
    }

    [SecurityCritical]
    internal void RevertPermitOnly()
    {
      if (!this.HasImperativeRestrictions())
        return;
      FrameSecurityDescriptor.DecrementOverridesCount();
      this.m_restriction = (PermissionSet) null;
    }

    [SecurityCritical]
    internal void RevertAll()
    {
      this.RevertAssert();
      this.RevertAssertAllPossible();
      this.RevertDeny();
      this.RevertPermitOnly();
    }

    [SecurityCritical]
    internal bool CheckDemand(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
    {
      bool flag = this.CheckDemand2(demand, permToken, rmh, false);
      if (flag)
        flag = this.CheckDemand2(demand, permToken, rmh, true);
      return flag;
    }

    [SecurityCritical]
    internal bool CheckDemand2(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh, bool fDeclarative)
    {
      if (this.GetPermitOnly(fDeclarative) != null)
        this.GetPermitOnly(fDeclarative).CheckDecoded(demand, permToken);
      if (this.GetDenials(fDeclarative) != null)
        this.GetDenials(fDeclarative).CheckDecoded(demand, permToken);
      if (this.GetAssertions(fDeclarative) != null)
        this.GetAssertions(fDeclarative).CheckDecoded(demand, permToken);
      bool flag1 = SecurityManager._SetThreadSecurity(false);
      try
      {
        PermissionSet permitOnly = this.GetPermitOnly(fDeclarative);
        if (permitOnly != null)
        {
          CodeAccessPermission permission = (CodeAccessPermission) permitOnly.GetPermission((IPermission) demand);
          if (permission == null)
          {
            if (!permitOnly.IsUnrestricted())
              throw new SecurityException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("Security_Generic"), (object) demand.GetType().AssemblyQualifiedName), (object) null, (object) permitOnly, SecurityRuntime.GetMethodInfo(rmh), (object) demand, (IPermission) demand);
          }
          else
          {
            bool flag2 = true;
            try
            {
              flag2 = !demand.CheckPermitOnly(permission);
            }
            catch (ArgumentException ex)
            {
            }
            if (flag2)
              throw new SecurityException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("Security_Generic"), (object) demand.GetType().AssemblyQualifiedName), (object) null, (object) permitOnly, SecurityRuntime.GetMethodInfo(rmh), (object) demand, (IPermission) demand);
          }
        }
        PermissionSet denials = this.GetDenials(fDeclarative);
        if (denials != null)
        {
          CodeAccessPermission permission = (CodeAccessPermission) denials.GetPermission((IPermission) demand);
          if (denials.IsUnrestricted())
            throw new SecurityException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("Security_Generic"), (object) demand.GetType().AssemblyQualifiedName), (object) denials, (object) null, SecurityRuntime.GetMethodInfo(rmh), (object) demand, (IPermission) demand);
          bool flag2 = true;
          try
          {
            flag2 = !demand.CheckDeny(permission);
          }
          catch (ArgumentException ex)
          {
          }
          if (flag2)
            throw new SecurityException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("Security_Generic"), (object) demand.GetType().AssemblyQualifiedName), (object) denials, (object) null, SecurityRuntime.GetMethodInfo(rmh), (object) demand, (IPermission) demand);
        }
        if (this.GetAssertAllPossible())
          return false;
        PermissionSet assertions = this.GetAssertions(fDeclarative);
        if (assertions != null)
        {
          CodeAccessPermission permission = (CodeAccessPermission) assertions.GetPermission((IPermission) demand);
          try
          {
            if (!assertions.IsUnrestricted())
            {
              if (!demand.CheckAssert(permission))
                goto label_35;
            }
            return false;
          }
          catch (ArgumentException ex)
          {
          }
        }
      }
      finally
      {
        if (flag1)
          SecurityManager._SetThreadSecurity(true);
      }
label_35:
      return true;
    }

    [SecurityCritical]
    internal bool CheckSetDemand(PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
    {
      PermissionSet alteredDemandSet1 = (PermissionSet) null;
      PermissionSet alteredDemandSet2 = (PermissionSet) null;
      bool flag = this.CheckSetDemand2(demandSet, out alteredDemandSet1, rmh, false);
      if (alteredDemandSet1 != null)
        demandSet = alteredDemandSet1;
      if (flag)
        flag = this.CheckSetDemand2(demandSet, out alteredDemandSet2, rmh, true);
      alteredDemandSet = alteredDemandSet2 == null ? (alteredDemandSet1 == null ? (PermissionSet) null : alteredDemandSet1) : alteredDemandSet2;
      return flag;
    }

    [SecurityCritical]
    internal bool CheckSetDemand2(PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh, bool fDeclarative)
    {
      alteredDemandSet = (PermissionSet) null;
      if (demandSet == null || demandSet.IsEmpty())
        return false;
      if (this.GetPermitOnly(fDeclarative) != null)
        this.GetPermitOnly(fDeclarative).CheckDecoded(demandSet);
      if (this.GetDenials(fDeclarative) != null)
        this.GetDenials(fDeclarative).CheckDecoded(demandSet);
      if (this.GetAssertions(fDeclarative) != null)
        this.GetAssertions(fDeclarative).CheckDecoded(demandSet);
      bool flag1 = SecurityManager._SetThreadSecurity(false);
      try
      {
        PermissionSet permitOnly = this.GetPermitOnly(fDeclarative);
        if (permitOnly != null)
        {
          IPermission firstPermThatFailed = (IPermission) null;
          bool flag2 = true;
          try
          {
            flag2 = !demandSet.CheckPermitOnly(permitOnly, out firstPermThatFailed);
          }
          catch (ArgumentException ex)
          {
          }
          if (flag2)
            throw new SecurityException(Environment.GetResourceString("Security_GenericNoType"), (object) null, (object) permitOnly, SecurityRuntime.GetMethodInfo(rmh), (object) demandSet, firstPermThatFailed);
        }
        PermissionSet denials = this.GetDenials(fDeclarative);
        if (denials != null)
        {
          IPermission firstPermThatFailed = (IPermission) null;
          bool flag2 = true;
          try
          {
            flag2 = !demandSet.CheckDeny(denials, out firstPermThatFailed);
          }
          catch (ArgumentException ex)
          {
          }
          if (flag2)
            throw new SecurityException(Environment.GetResourceString("Security_GenericNoType"), (object) denials, (object) null, SecurityRuntime.GetMethodInfo(rmh), (object) demandSet, firstPermThatFailed);
        }
        if (this.GetAssertAllPossible())
          return false;
        PermissionSet assertions = this.GetAssertions(fDeclarative);
        if (assertions != null)
        {
          if (demandSet.CheckAssertion(assertions))
            return false;
          if (!assertions.IsUnrestricted())
            PermissionSet.RemoveAssertedPermissionSet(demandSet, assertions, out alteredDemandSet);
        }
      }
      finally
      {
        if (flag1)
          SecurityManager._SetThreadSecurity(true);
      }
      return true;
    }
  }
}
