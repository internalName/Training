// Decompiled with JetBrains decompiler
// Type: System.Globalization.CultureTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>
  ///   Определяет типы списков языков и региональных параметров, которые можно получить с помощью метода <see cref="M:System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes)" />.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum CultureTypes
  {
    NeutralCultures = 1,
    SpecificCultures = 2,
    InstalledWin32Cultures = 4,
    AllCultures = InstalledWin32Cultures | SpecificCultures | NeutralCultures, // 0x00000007
    UserCustomCulture = 8,
    ReplacementCultures = 16, // 0x00000010
    [Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")] WindowsOnlyCultures = 32, // 0x00000020
    [Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")] FrameworkCultures = 64, // 0x00000040
  }
}
