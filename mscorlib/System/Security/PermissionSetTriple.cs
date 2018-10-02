﻿// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionSetTriple
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;

namespace System.Security
{
  [Serializable]
  internal sealed class PermissionSetTriple
  {
    private static volatile PermissionToken s_zoneToken;
    private static volatile PermissionToken s_urlToken;
    internal PermissionSet AssertSet;
    internal PermissionSet GrantSet;
    internal PermissionSet RefusedSet;

    internal PermissionSetTriple()
    {
      this.Reset();
    }

    internal PermissionSetTriple(PermissionSetTriple triple)
    {
      this.AssertSet = triple.AssertSet;
      this.GrantSet = triple.GrantSet;
      this.RefusedSet = triple.RefusedSet;
    }

    internal void Reset()
    {
      this.AssertSet = (PermissionSet) null;
      this.GrantSet = (PermissionSet) null;
      this.RefusedSet = (PermissionSet) null;
    }

    internal bool IsEmpty()
    {
      if (this.AssertSet == null && this.GrantSet == null)
        return this.RefusedSet == null;
      return false;
    }

    private PermissionToken ZoneToken
    {
      [SecurityCritical] get
      {
        if (PermissionSetTriple.s_zoneToken == null)
          PermissionSetTriple.s_zoneToken = PermissionToken.GetToken(typeof (ZoneIdentityPermission));
        return PermissionSetTriple.s_zoneToken;
      }
    }

    private PermissionToken UrlToken
    {
      [SecurityCritical] get
      {
        if (PermissionSetTriple.s_urlToken == null)
          PermissionSetTriple.s_urlToken = PermissionToken.GetToken(typeof (UrlIdentityPermission));
        return PermissionSetTriple.s_urlToken;
      }
    }

    [SecurityCritical]
    internal bool Update(PermissionSetTriple psTriple, out PermissionSetTriple retTriple)
    {
      retTriple = (PermissionSetTriple) null;
      retTriple = this.UpdateAssert(psTriple.AssertSet);
      if (psTriple.AssertSet != null && psTriple.AssertSet.IsUnrestricted())
        return true;
      this.UpdateGrant(psTriple.GrantSet);
      this.UpdateRefused(psTriple.RefusedSet);
      return false;
    }

    [SecurityCritical]
    internal PermissionSetTriple UpdateAssert(PermissionSet in_a)
    {
      PermissionSetTriple permissionSetTriple = (PermissionSetTriple) null;
      if (in_a != null)
      {
        if (in_a.IsSubsetOf(this.AssertSet))
          return (PermissionSetTriple) null;
        PermissionSet permissionSet;
        if (this.GrantSet != null)
        {
          permissionSet = in_a.Intersect(this.GrantSet);
        }
        else
        {
          this.GrantSet = new PermissionSet(true);
          permissionSet = in_a.Copy();
        }
        bool bFailedToCompress = false;
        if (this.RefusedSet != null)
          permissionSet = PermissionSet.RemoveRefusedPermissionSet(permissionSet, this.RefusedSet, out bFailedToCompress);
        if (!bFailedToCompress)
          bFailedToCompress = PermissionSet.IsIntersectingAssertedPermissions(permissionSet, this.AssertSet);
        if (bFailedToCompress)
        {
          permissionSetTriple = new PermissionSetTriple(this);
          this.Reset();
          this.GrantSet = permissionSetTriple.GrantSet.Copy();
        }
        if (this.AssertSet == null)
          this.AssertSet = permissionSet;
        else
          this.AssertSet.InplaceUnion(permissionSet);
      }
      return permissionSetTriple;
    }

    [SecurityCritical]
    internal void UpdateGrant(PermissionSet in_g, out ZoneIdentityPermission z, out UrlIdentityPermission u)
    {
      z = (ZoneIdentityPermission) null;
      u = (UrlIdentityPermission) null;
      if (in_g == null)
        return;
      if (this.GrantSet == null)
        this.GrantSet = in_g.Copy();
      else
        this.GrantSet.InplaceIntersect(in_g);
      z = (ZoneIdentityPermission) in_g.GetPermission(this.ZoneToken);
      u = (UrlIdentityPermission) in_g.GetPermission(this.UrlToken);
    }

    [SecurityCritical]
    internal void UpdateGrant(PermissionSet in_g)
    {
      if (in_g == null)
        return;
      if (this.GrantSet == null)
        this.GrantSet = in_g.Copy();
      else
        this.GrantSet.InplaceIntersect(in_g);
    }

    internal void UpdateRefused(PermissionSet in_r)
    {
      if (in_r == null)
        return;
      if (this.RefusedSet == null)
        this.RefusedSet = in_r.Copy();
      else
        this.RefusedSet.InplaceUnion(in_r);
    }

    [SecurityCritical]
    private static bool CheckAssert(PermissionSet pSet, CodeAccessPermission demand, PermissionToken permToken)
    {
      if (pSet != null)
      {
        pSet.CheckDecoded(demand, permToken);
        CodeAccessPermission permission = (CodeAccessPermission) pSet.GetPermission((IPermission) demand);
        try
        {
          if (!pSet.IsUnrestricted())
          {
            if (!demand.CheckAssert(permission))
              goto label_6;
          }
          return false;
        }
        catch (ArgumentException ex)
        {
        }
      }
label_6:
      return true;
    }

    [SecurityCritical]
    private static bool CheckAssert(PermissionSet assertPset, PermissionSet demandSet, out PermissionSet newDemandSet)
    {
      newDemandSet = (PermissionSet) null;
      if (assertPset != null)
      {
        assertPset.CheckDecoded(demandSet);
        if (demandSet.CheckAssertion(assertPset))
          return false;
        PermissionSet.RemoveAssertedPermissionSet(demandSet, assertPset, out newDemandSet);
      }
      return true;
    }

    [SecurityCritical]
    internal bool CheckDemand(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
    {
      if (!PermissionSetTriple.CheckAssert(this.AssertSet, demand, permToken))
        return false;
      CodeAccessSecurityEngine.CheckHelper(this.GrantSet, this.RefusedSet, demand, permToken, rmh, (object) null, SecurityAction.Demand, true);
      return true;
    }

    [SecurityCritical]
    internal bool CheckSetDemand(PermissionSet demandSet, out PermissionSet alteredDemandset, RuntimeMethodHandleInternal rmh)
    {
      alteredDemandset = (PermissionSet) null;
      if (!PermissionSetTriple.CheckAssert(this.AssertSet, demandSet, out alteredDemandset))
        return false;
      if (alteredDemandset != null)
        demandSet = alteredDemandset;
      CodeAccessSecurityEngine.CheckSetHelper(this.GrantSet, this.RefusedSet, demandSet, rmh, (object) null, SecurityAction.Demand, true);
      return true;
    }

    [SecurityCritical]
    internal bool CheckDemandNoThrow(CodeAccessPermission demand, PermissionToken permToken)
    {
      return CodeAccessSecurityEngine.CheckHelper(this.GrantSet, this.RefusedSet, demand, permToken, RuntimeMethodHandleInternal.EmptyHandle, (object) null, SecurityAction.Demand, false);
    }

    [SecurityCritical]
    internal bool CheckSetDemandNoThrow(PermissionSet demandSet)
    {
      return CodeAccessSecurityEngine.CheckSetHelper(this.GrantSet, this.RefusedSet, demandSet, RuntimeMethodHandleInternal.EmptyHandle, (object) null, SecurityAction.Demand, false);
    }

    [SecurityCritical]
    internal bool CheckFlags(ref int flags)
    {
      if (this.AssertSet != null)
      {
        int specialFlags = SecurityManager.GetSpecialFlags(this.AssertSet, (PermissionSet) null);
        if ((flags & specialFlags) != 0)
          flags &= ~specialFlags;
      }
      return (SecurityManager.GetSpecialFlags(this.GrantSet, this.RefusedSet) & flags) == flags;
    }
  }
}
