// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IdentityReferenceEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  [ComVisible(false)]
  internal class IdentityReferenceEnumerator : IEnumerator<IdentityReference>, IDisposable, IEnumerator
  {
    private int _Current;
    private readonly IdentityReferenceCollection _Collection;

    internal IdentityReferenceEnumerator(IdentityReferenceCollection collection)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      this._Collection = collection;
      this._Current = -1;
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this._Collection.Identities[this._Current];
      }
    }

    public IdentityReference Current
    {
      get
      {
        return ((IEnumerator) this).Current as IdentityReference;
      }
    }

    public bool MoveNext()
    {
      ++this._Current;
      return this._Current < this._Collection.Count;
    }

    public void Reset()
    {
      this._Current = -1;
    }

    public void Dispose()
    {
    }
  }
}
