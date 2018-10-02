﻿// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreDeploymentMetadataPropertyEnumeration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal class StoreDeploymentMetadataPropertyEnumeration : IEnumerator
  {
    private IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY _enum;
    private bool _fValid;
    private StoreOperationMetadataProperty _current;

    public StoreDeploymentMetadataPropertyEnumeration(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY pI)
    {
      this._enum = pI;
    }

    private StoreOperationMetadataProperty GetCurrent()
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

    public StoreOperationMetadataProperty Current
    {
      get
      {
        return this.GetCurrent();
      }
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this;
    }

    [SecuritySafeCritical]
    public bool MoveNext()
    {
      StoreOperationMetadataProperty[] AppIds = new StoreOperationMetadataProperty[1];
      uint num = this._enum.Next(1U, AppIds);
      if (num == 1U)
        this._current = AppIds[0];
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
