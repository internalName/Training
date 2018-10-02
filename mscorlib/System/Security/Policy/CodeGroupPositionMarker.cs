// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.CodeGroupPositionMarker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Policy
{
  internal class CodeGroupPositionMarker
  {
    internal int elementIndex;
    internal int groupIndex;
    internal SecurityElement element;

    internal CodeGroupPositionMarker(int elementIndex, int groupIndex, SecurityElement element)
    {
      this.elementIndex = elementIndex;
      this.groupIndex = groupIndex;
      this.element = element;
    }
  }
}
