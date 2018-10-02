// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreAssemblyFileEnumeration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal class StoreAssemblyFileEnumeration : IEnumerator
  {
    private IEnumSTORE_ASSEMBLY_FILE _enum;
    private bool _fValid;
    private STORE_ASSEMBLY_FILE _current;

    public StoreAssemblyFileEnumeration(IEnumSTORE_ASSEMBLY_FILE pI)
    {
      this._enum = pI;
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this;
    }

    private STORE_ASSEMBLY_FILE GetCurrent()
    {
      if (!this._fValid)
        throw new InvalidOperationException();
      return this._current;
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.GetCurrent();
      }
    }

    public STORE_ASSEMBLY_FILE Current
    {
      get
      {
        return this.GetCurrent();
      }
    }

    [SecuritySafeCritical]
    public bool MoveNext()
    {
      STORE_ASSEMBLY_FILE[] rgelt = new STORE_ASSEMBLY_FILE[1];
      uint num = this._enum.Next(1U, rgelt);
      if (num == 1U)
        this._current = rgelt[0];
      return this._fValid = num == 1U;
    }

    [SecuritySafeCritical]
    public void Reset()
    {
      this._fValid = false;
      this._enum.Reset();
    }
  }
}
