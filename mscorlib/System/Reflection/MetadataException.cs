// Decompiled with JetBrains decompiler
// Type: System.Reflection.MetadataException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Reflection
{
  internal class MetadataException : Exception
  {
    private int m_hr;

    internal MetadataException(int hr)
    {
      this.m_hr = hr;
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "MetadataException HResult = {0:x}.", (object) this.m_hr);
    }
  }
}
