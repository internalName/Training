// Decompiled with JetBrains decompiler
// Type: System.Resources.IResourceGroveler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Resources
{
  internal interface IResourceGroveler
  {
    ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark);

    bool HasNeutralResources(CultureInfo culture, string defaultResName);
  }
}
