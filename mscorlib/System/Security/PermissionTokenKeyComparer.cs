// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionTokenKeyComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;

namespace System.Security
{
  [Serializable]
  internal sealed class PermissionTokenKeyComparer : IEqualityComparer
  {
    private Comparer _caseSensitiveComparer;
    private TextInfo _info;

    public PermissionTokenKeyComparer()
    {
      this._caseSensitiveComparer = new Comparer(CultureInfo.InvariantCulture);
      this._info = CultureInfo.InvariantCulture.TextInfo;
    }

    [SecuritySafeCritical]
    public int Compare(object a, object b)
    {
      string strLeft = a as string;
      string strRight = b as string;
      if (strLeft == null || strRight == null)
        return this._caseSensitiveComparer.Compare(a, b);
      int num = this._caseSensitiveComparer.Compare(a, b);
      if (num == 0 || SecurityManager.IsSameType(strLeft, strRight))
        return 0;
      return num;
    }

    public bool Equals(object a, object b)
    {
      if (a == b)
        return true;
      if (a == null || b == null)
        return false;
      return this.Compare(a, b) == 0;
    }

    public int GetHashCode(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      string str = obj as string;
      if (str == null)
        return obj.GetHashCode();
      int num1 = str.IndexOf(',');
      if (num1 == -1)
        num1 = str.Length;
      int num2 = 0;
      for (int index = 0; index < num1; ++index)
        num2 = num2 << 7 ^ (int) str[index] ^ num2 >> 25;
      return num2;
    }
  }
}
