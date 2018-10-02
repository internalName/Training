// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.EnvironmentStringExpressionSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Util;

namespace System.Security.Permissions
{
  [Serializable]
  internal class EnvironmentStringExpressionSet : StringExpressionSet
  {
    public EnvironmentStringExpressionSet()
      : base(true, (string) null, false)
    {
    }

    public EnvironmentStringExpressionSet(string str)
      : base(true, str, false)
    {
    }

    protected override StringExpressionSet CreateNewEmpty()
    {
      return (StringExpressionSet) new EnvironmentStringExpressionSet();
    }

    protected override bool StringSubsetString(string left, string right, bool ignoreCase)
    {
      if (!ignoreCase)
        return string.Compare(left, right, StringComparison.Ordinal) == 0;
      return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
    }

    protected override string ProcessWholeString(string str)
    {
      return str;
    }

    protected override string ProcessSingleString(string str)
    {
      return str;
    }

    [SecuritySafeCritical]
    public override string ToString()
    {
      return this.UnsafeToString();
    }
  }
}
