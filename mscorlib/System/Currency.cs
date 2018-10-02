// Decompiled with JetBrains decompiler
// Type: System.Currency
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  [Serializable]
  internal struct Currency
  {
    internal long m_value;

    public Currency(Decimal value)
    {
      this.m_value = Decimal.ToCurrency(value).m_value;
    }

    internal Currency(long value, int ignored)
    {
      this.m_value = value;
    }

    public static Currency FromOACurrency(long cy)
    {
      return new Currency(cy, 0);
    }

    public long ToOACurrency()
    {
      return this.m_value;
    }

    [SecuritySafeCritical]
    public static Decimal ToDecimal(Currency c)
    {
      Decimal result = new Decimal();
      Currency.FCallToDecimal(ref result, c);
      return result;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallToDecimal(ref Decimal result, Currency c);
  }
}
