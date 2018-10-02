// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TCEAdapterGen.NameSpaceExtractor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.TCEAdapterGen
{
  internal static class NameSpaceExtractor
  {
    private static char NameSpaceSeperator = '.';

    public static string ExtractNameSpace(string FullyQualifiedTypeName)
    {
      int length = FullyQualifiedTypeName.LastIndexOf(NameSpaceExtractor.NameSpaceSeperator);
      if (length == -1)
        return "";
      return FullyQualifiedTypeName.Substring(0, length);
    }
  }
}
