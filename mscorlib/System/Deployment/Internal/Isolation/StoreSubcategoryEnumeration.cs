// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreSubcategoryEnumeration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal class StoreSubcategoryEnumeration : IEnumerator
  {
    private IEnumSTORE_CATEGORY_SUBCATEGORY _enum;
    private bool _fValid;
    private STORE_CATEGORY_SUBCATEGORY _current;

    public StoreSubcategoryEnumeration(IEnumSTORE_CATEGORY_SUBCATEGORY pI)
    {
      this._enum = pI;
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this;
    }

    private STORE_CATEGORY_SUBCATEGORY GetCurrent()
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

    public STORE_CATEGORY_SUBCATEGORY Current
    {
      get
      {
        return this.GetCurrent();
      }
    }

    [SecuritySafeCritical]
    public bool MoveNext()
    {
      STORE_CATEGORY_SUBCATEGORY[] rgElements = new STORE_CATEGORY_SUBCATEGORY[1];
      uint num = this._enum.Next(1U, rgElements);
      if (num == 1U)
        this._current = rgElements[0];
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
