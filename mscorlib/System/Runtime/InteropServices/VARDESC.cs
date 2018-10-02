// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.VARDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.VARDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct VARDESC
  {
    /// <summary>Указывает идентификатор элемента переменной.</summary>
    public int memid;
    /// <summary>
    ///   Это поле зарезервировано для будущего использования.
    /// </summary>
    public string lpstrSchema;
    /// <summary>Содержит тип переменной.</summary>
    public ELEMDESC elemdescVar;
    /// <summary>Определяет свойства переменной.</summary>
    public short wVarFlags;
    /// <summary>Определяет способ маршалинга переменной.</summary>
    public VarEnum varkind;

    /// <summary>
    ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC.DESCUNION" />.
    /// </summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct DESCUNION
    {
      /// <summary>
      ///   Указывает смещение данной переменной в пределах экземпляра.
      /// </summary>
      [FieldOffset(0)]
      public int oInst;
      /// <summary>Описывает символьную константу.</summary>
      [FieldOffset(0)]
      public IntPtr lpvarValue;
    }
  }
}
