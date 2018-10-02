// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.FUNCKIND
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCKIND" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Serializable]
  public enum FUNCKIND
  {
    FUNC_VIRTUAL,
    FUNC_PUREVIRTUAL,
    FUNC_NONVIRTUAL,
    FUNC_STATIC,
    FUNC_DISPATCH,
  }
}
