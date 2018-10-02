// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TYPELIBATTR
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.TYPELIBATTR" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPELIBATTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Serializable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct TYPELIBATTR
  {
    /// <summary>
    ///   Представляет глобальный уникальный идентификатор библиотеки типов.
    /// </summary>
    public Guid guid;
    /// <summary>Представляет код языка библиотеки типов.</summary>
    public int lcid;
    /// <summary>
    ///   Представляет целевую аппаратную платформу библиотеки типов.
    /// </summary>
    public SYSKIND syskind;
    /// <summary>
    ///   Представляет основной номер версии библиотеки типов.
    /// </summary>
    public short wMajorVerNum;
    /// <summary>
    ///   Представляет дополнительный номер версии библиотеки типов.
    /// </summary>
    public short wMinorVerNum;
    /// <summary>Представляет флаги библиотеки.</summary>
    public LIBFLAGS wLibFlags;
  }
}
