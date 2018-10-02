// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionTokenFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Threading;

namespace System.Security
{
  internal class PermissionTokenFactory
  {
    private volatile int m_size;
    private volatile int m_index;
    private volatile Hashtable m_tokenTable;
    private volatile Hashtable m_handleTable;
    private volatile Hashtable m_indexTable;
    private volatile PermissionToken[] m_builtIn;
    private const string s_unrestrictedPermissionInferfaceName = "System.Security.Permissions.IUnrestrictedPermission";

    internal PermissionTokenFactory(int size)
    {
      this.m_builtIn = new PermissionToken[17];
      this.m_size = size;
      this.m_index = 17;
      this.m_tokenTable = (Hashtable) null;
      this.m_handleTable = new Hashtable(size);
      this.m_indexTable = new Hashtable(size);
    }

    [SecuritySafeCritical]
    internal PermissionToken FindToken(Type cls)
    {
      IntPtr num = cls.TypeHandle.Value;
      PermissionToken permissionToken1 = (PermissionToken) this.m_handleTable[(object) num];
      if (permissionToken1 != null)
        return permissionToken1;
      if (this.m_tokenTable == null)
        return (PermissionToken) null;
      PermissionToken permissionToken2 = (PermissionToken) this.m_tokenTable[(object) cls.AssemblyQualifiedName];
      if (permissionToken2 != null)
      {
        lock (this)
          this.m_handleTable.Add((object) num, (object) permissionToken2);
      }
      return permissionToken2;
    }

    internal PermissionToken FindTokenByIndex(int i)
    {
      return i >= 17 ? (PermissionToken) this.m_indexTable[(object) i] : this.BuiltInGetToken(i, (IPermission) null, (Type) null);
    }

    [SecuritySafeCritical]
    internal PermissionToken GetToken(Type cls, IPermission perm)
    {
      IntPtr num = cls.TypeHandle.Value;
      object obj = this.m_handleTable[(object) num];
      if (obj == null)
      {
        string assemblyQualifiedName = cls.AssemblyQualifiedName;
        obj = this.m_tokenTable != null ? this.m_tokenTable[(object) assemblyQualifiedName] : (object) null;
        if (obj == null)
        {
          lock (this)
          {
            if (this.m_tokenTable != null)
              obj = this.m_tokenTable[(object) assemblyQualifiedName];
            else
              this.m_tokenTable = new Hashtable(this.m_size, 1f, (IEqualityComparer) new PermissionTokenKeyComparer());
            if (obj == null)
            {
              obj = perm == null ? (!(cls.GetInterface("System.Security.Permissions.IUnrestrictedPermission") != (Type) null) ? (object) new PermissionToken(this.m_index++, PermissionTokenType.Normal, assemblyQualifiedName) : (object) new PermissionToken(this.m_index++, PermissionTokenType.IUnrestricted, assemblyQualifiedName)) : (object) new PermissionToken(this.m_index++, PermissionTokenType.IUnrestricted, assemblyQualifiedName);
              this.m_tokenTable.Add((object) assemblyQualifiedName, obj);
              this.m_indexTable.Add((object) (this.m_index - 1), obj);
              PermissionToken.s_tokenSet.SetItem(((PermissionToken) obj).m_index, obj);
            }
            if (!this.m_handleTable.Contains((object) num))
              this.m_handleTable.Add((object) num, obj);
          }
        }
        else
        {
          lock (this)
          {
            if (!this.m_handleTable.Contains((object) num))
              this.m_handleTable.Add((object) num, obj);
          }
        }
      }
      if ((((PermissionToken) obj).m_type & PermissionTokenType.DontKnow) != (PermissionTokenType) 0)
      {
        if (perm != null)
        {
          ((PermissionToken) obj).m_type = PermissionTokenType.IUnrestricted;
          ((PermissionToken) obj).m_strTypeName = perm.GetType().AssemblyQualifiedName;
        }
        else
        {
          if (cls.GetInterface("System.Security.Permissions.IUnrestrictedPermission") != (Type) null)
            ((PermissionToken) obj).m_type = PermissionTokenType.IUnrestricted;
          else
            ((PermissionToken) obj).m_type = PermissionTokenType.Normal;
          ((PermissionToken) obj).m_strTypeName = cls.AssemblyQualifiedName;
        }
      }
      return (PermissionToken) obj;
    }

    internal PermissionToken GetToken(string typeStr)
    {
      object obj = this.m_tokenTable != null ? this.m_tokenTable[(object) typeStr] : (object) null;
      if (obj == null)
      {
        lock (this)
        {
          if (this.m_tokenTable != null)
            obj = this.m_tokenTable[(object) typeStr];
          else
            this.m_tokenTable = new Hashtable(this.m_size, 1f, (IEqualityComparer) new PermissionTokenKeyComparer());
          if (obj == null)
          {
            obj = (object) new PermissionToken(this.m_index++, PermissionTokenType.DontKnow, typeStr);
            this.m_tokenTable.Add((object) typeStr, obj);
            this.m_indexTable.Add((object) (this.m_index - 1), obj);
            PermissionToken.s_tokenSet.SetItem(((PermissionToken) obj).m_index, obj);
          }
        }
      }
      return (PermissionToken) obj;
    }

    internal PermissionToken BuiltInGetToken(int index, IPermission perm, Type cls)
    {
      PermissionToken permissionToken = Volatile.Read<PermissionToken>(ref this.m_builtIn[index]);
      if (permissionToken == null)
      {
        lock (this)
        {
          permissionToken = this.m_builtIn[index];
          if (permissionToken == null)
          {
            PermissionTokenType permissionTokenType = PermissionTokenType.DontKnow;
            if (perm != null)
              permissionTokenType = PermissionTokenType.IUnrestricted;
            else if (cls != (Type) null)
              permissionTokenType = PermissionTokenType.IUnrestricted;
            permissionToken = new PermissionToken(index, permissionTokenType | PermissionTokenType.BuiltIn, (string) null);
            Volatile.Write<PermissionToken>(ref this.m_builtIn[index], permissionToken);
            PermissionToken.s_tokenSet.SetItem(permissionToken.m_index, (object) permissionToken);
          }
        }
      }
      if ((permissionToken.m_type & PermissionTokenType.DontKnow) != (PermissionTokenType) 0)
      {
        permissionToken.m_type = PermissionTokenType.BuiltIn;
        if (perm != null)
          permissionToken.m_type |= PermissionTokenType.IUnrestricted;
        else if (cls != (Type) null)
          permissionToken.m_type |= PermissionTokenType.IUnrestricted;
        else
          permissionToken.m_type |= PermissionTokenType.DontKnow;
      }
      return permissionToken;
    }
  }
}
