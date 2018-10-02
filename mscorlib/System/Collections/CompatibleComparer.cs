// Decompiled with JetBrains decompiler
// Type: System.Collections.CompatibleComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections
{
  [Serializable]
  internal class CompatibleComparer : IEqualityComparer
  {
    private IComparer _comparer;
    private IHashCodeProvider _hcp;

    internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
    {
      this._comparer = comparer;
      this._hcp = hashCodeProvider;
    }

    public int Compare(object a, object b)
    {
      if (a == b)
        return 0;
      if (a == null)
        return -1;
      if (b == null)
        return 1;
      if (this._comparer != null)
        return this._comparer.Compare(a, b);
      IComparable comparable = a as IComparable;
      if (comparable != null)
        return comparable.CompareTo(b);
      throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
    }

    public bool Equals(object a, object b)
    {
      return this.Compare(a, b) == 0;
    }

    public int GetHashCode(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (this._hcp != null)
        return this._hcp.GetHashCode(obj);
      return obj.GetHashCode();
    }

    internal IComparer Comparer
    {
      get
      {
        return this._comparer;
      }
    }

    internal IHashCodeProvider HashCodeProvider
    {
      get
      {
        return this._hcp;
      }
    }
  }
}
