// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceFallbackManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Resources
{
  internal class ResourceFallbackManager : IEnumerable<CultureInfo>, IEnumerable
  {
    private CultureInfo m_startingCulture;
    private CultureInfo m_neutralResourcesCulture;
    private bool m_useParents;

    internal ResourceFallbackManager(CultureInfo startingCulture, CultureInfo neutralResourcesCulture, bool useParents)
    {
      this.m_startingCulture = startingCulture == null ? CultureInfo.CurrentUICulture : startingCulture;
      this.m_neutralResourcesCulture = neutralResourcesCulture;
      this.m_useParents = useParents;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IEnumerator<CultureInfo> GetEnumerator()
    {
      bool reachedNeutralResourcesCulture = false;
      CultureInfo currentCulture = this.m_startingCulture;
      while (this.m_neutralResourcesCulture == null || !(currentCulture.Name == this.m_neutralResourcesCulture.Name))
      {
        yield return currentCulture;
        currentCulture = currentCulture.Parent;
        if (!this.m_useParents || currentCulture.HasInvariantCultureName)
          goto label_4;
      }
      yield return CultureInfo.InvariantCulture;
      reachedNeutralResourcesCulture = true;
label_4:
      if (this.m_useParents && !this.m_startingCulture.HasInvariantCultureName && !reachedNeutralResourcesCulture)
        yield return CultureInfo.InvariantCulture;
    }
  }
}
