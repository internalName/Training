// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.Win32
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  internal static class Win32
  {
    internal const int TRUE = 1;

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    internal static int ConvertSdToSddl(byte[] binaryForm, int requestedRevision, SecurityInfos si, out string resultSddl)
    {
      uint resultStringLength = 0;
      IntPtr resultString;
      if (1 != Win32Native.ConvertSdToStringSd(binaryForm, (uint) requestedRevision, (uint) si, out resultString, ref resultStringLength))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        resultSddl = (string) null;
        if (lastWin32Error == 8)
          throw new OutOfMemoryException();
        return lastWin32Error;
      }
      resultSddl = Marshal.PtrToStringUni(resultString);
      Win32Native.LocalFree(resultString);
      return 0;
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static int GetSecurityInfo(ResourceType resourceType, string name, SafeHandle handle, AccessControlSections accessControlSections, out RawSecurityDescriptor resultSd)
    {
      resultSd = (RawSecurityDescriptor) null;
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      SecurityInfos securityInfos = (SecurityInfos) 0;
      Privilege privilege = (Privilege) null;
      if ((accessControlSections & AccessControlSections.Owner) != AccessControlSections.None)
        securityInfos |= SecurityInfos.Owner;
      if ((accessControlSections & AccessControlSections.Group) != AccessControlSections.None)
        securityInfos |= SecurityInfos.Group;
      if ((accessControlSections & AccessControlSections.Access) != AccessControlSections.None)
        securityInfos |= SecurityInfos.DiscretionaryAcl;
      if ((accessControlSections & AccessControlSections.Audit) != AccessControlSections.None)
      {
        securityInfos |= SecurityInfos.SystemAcl;
        privilege = new Privilege("SeSecurityPrivilege");
      }
      RuntimeHelpers.PrepareConstrainedRegions();
      IntPtr securityDescriptor;
      int num;
      try
      {
        if (privilege != null)
        {
          try
          {
            privilege.Enable();
          }
          catch (PrivilegeNotHeldException ex)
          {
          }
        }
        IntPtr sidOwner;
        IntPtr sidGroup;
        IntPtr dacl;
        IntPtr sacl;
        if (name != null)
        {
          num = (int) Win32Native.GetSecurityInfoByName(name, (uint) resourceType, (uint) securityInfos, out sidOwner, out sidGroup, out dacl, out sacl, out securityDescriptor);
        }
        else
        {
          if (handle == null)
            throw new SystemException();
          if (handle.IsInvalid)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSafeHandle"), nameof (handle));
          num = (int) Win32Native.GetSecurityInfoByHandle(handle, (uint) resourceType, (uint) securityInfos, out sidOwner, out sidGroup, out dacl, out sacl, out securityDescriptor);
        }
        if (num == 0 && IntPtr.Zero.Equals((object) securityDescriptor))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoSecurityDescriptor"));
        if (num == 1300 || num == 1314)
          throw new PrivilegeNotHeldException("SeSecurityPrivilege");
        if (num == 5 || num == 1347)
          throw new UnauthorizedAccessException();
        if (num != 0)
          goto label_33;
      }
      catch
      {
        privilege?.Revert();
        throw;
      }
      finally
      {
        privilege?.Revert();
      }
      uint descriptorLength = Win32Native.GetSecurityDescriptorLength(securityDescriptor);
      byte[] numArray = new byte[(int) descriptorLength];
      Marshal.Copy(securityDescriptor, numArray, 0, (int) descriptorLength);
      Win32Native.LocalFree(securityDescriptor);
      resultSd = new RawSecurityDescriptor(numArray, 0);
      return 0;
label_33:
      if (num == 8)
        throw new OutOfMemoryException();
      return num;
    }

    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static int SetSecurityInfo(ResourceType type, string name, SafeHandle handle, SecurityInfos securityInformation, SecurityIdentifier owner, SecurityIdentifier group, GenericAcl sacl, GenericAcl dacl)
    {
      byte[] numArray1 = (byte[]) null;
      byte[] numArray2 = (byte[]) null;
      byte[] numArray3 = (byte[]) null;
      byte[] numArray4 = (byte[]) null;
      Privilege privilege = (Privilege) null;
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      if (owner != (SecurityIdentifier) null)
      {
        numArray1 = new byte[owner.BinaryLength];
        owner.GetBinaryForm(numArray1, 0);
      }
      if (group != (SecurityIdentifier) null)
      {
        numArray2 = new byte[group.BinaryLength];
        group.GetBinaryForm(numArray2, 0);
      }
      if (dacl != null)
      {
        numArray4 = new byte[dacl.BinaryLength];
        dacl.GetBinaryForm(numArray4, 0);
      }
      if (sacl != null)
      {
        numArray3 = new byte[sacl.BinaryLength];
        sacl.GetBinaryForm(numArray3, 0);
      }
      if ((securityInformation & SecurityInfos.SystemAcl) != (SecurityInfos) 0)
        privilege = new Privilege("SeSecurityPrivilege");
      RuntimeHelpers.PrepareConstrainedRegions();
      int num;
      try
      {
        if (privilege != null)
        {
          try
          {
            privilege.Enable();
          }
          catch (PrivilegeNotHeldException ex)
          {
          }
        }
        if (name != null)
        {
          num = (int) Win32Native.SetSecurityInfoByName(name, (uint) type, (uint) securityInformation, numArray1, numArray2, numArray4, numArray3);
        }
        else
        {
          if (handle == null)
            throw new InvalidProgramException();
          if (handle.IsInvalid)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSafeHandle"), nameof (handle));
          num = (int) Win32Native.SetSecurityInfoByHandle(handle, (uint) type, (uint) securityInformation, numArray1, numArray2, numArray4, numArray3);
        }
        if (num == 1300 || num == 1314)
          throw new PrivilegeNotHeldException("SeSecurityPrivilege");
        if (num == 5 || num == 1347)
          throw new UnauthorizedAccessException();
        if (num != 0)
          goto label_33;
      }
      catch
      {
        privilege?.Revert();
        throw;
      }
      finally
      {
        privilege?.Revert();
      }
      return 0;
label_33:
      if (num == 8)
        throw new OutOfMemoryException();
      return num;
    }
  }
}
