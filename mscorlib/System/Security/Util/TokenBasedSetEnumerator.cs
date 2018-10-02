// Decompiled with JetBrains decompiler
// Type: System.Security.Util.TokenBasedSetEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Util
{
  internal struct TokenBasedSetEnumerator
  {
    public object Current;
    public int Index;
    private TokenBasedSet _tb;

    public bool MoveNext()
    {
      if (this._tb == null)
        return false;
      return this._tb.MoveNext(ref this);
    }

    public void Reset()
    {
      this.Index = -1;
      this.Current = (object) null;
    }

    public TokenBasedSetEnumerator(TokenBasedSet tb)
    {
      this.Index = -1;
      this.Current = (object) null;
      this._tb = tb;
    }
  }
}
