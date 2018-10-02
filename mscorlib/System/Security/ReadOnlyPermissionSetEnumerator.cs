// Decompiled with JetBrains decompiler
// Type: System.Security.ReadOnlyPermissionSetEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security
{
  internal sealed class ReadOnlyPermissionSetEnumerator : IEnumerator
  {
    private IEnumerator m_permissionSetEnumerator;

    internal ReadOnlyPermissionSetEnumerator(IEnumerator permissionSetEnumerator)
    {
      this.m_permissionSetEnumerator = permissionSetEnumerator;
    }

    public object Current
    {
      get
      {
        IPermission current = this.m_permissionSetEnumerator.Current as IPermission;
        if (current == null)
          return (object) null;
        return (object) current.Copy();
      }
    }

    public bool MoveNext()
    {
      return this.m_permissionSetEnumerator.MoveNext();
    }

    public void Reset()
    {
      this.m_permissionSetEnumerator.Reset();
    }
  }
}
