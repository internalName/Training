// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IntrospectiveSortUtilities
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Versioning;

namespace System.Collections.Generic
{
  internal static class IntrospectiveSortUtilities
  {
    internal const int IntrosortSizeThreshold = 16;
    internal const int QuickSortDepthThreshold = 32;

    internal static int FloorLog2(int n)
    {
      int num = 0;
      while (n >= 1)
      {
        ++num;
        n /= 2;
      }
      return num;
    }

    internal static void ThrowOrIgnoreBadComparer(object comparer)
    {
      if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", comparer));
    }
  }
}
