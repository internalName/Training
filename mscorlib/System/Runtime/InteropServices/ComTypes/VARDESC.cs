// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.VARDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>Описывает переменную, константу или данные-член.</summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct VARDESC
  {
    /// <summary>Указывает идентификатор элемента переменной.</summary>
    [__DynamicallyInvokable]
    public int memid;
    /// <summary>
    ///   Это поле зарезервировано для будущего использования.
    /// </summary>
    [__DynamicallyInvokable]
    public string lpstrSchema;
    /// <summary>Содержит сведения о переменной.</summary>
    [__DynamicallyInvokable]
    public VARDESC.DESCUNION desc;
    /// <summary>Содержит тип переменной.</summary>
    [__DynamicallyInvokable]
    public ELEMDESC elemdescVar;
    /// <summary>Определяет свойства переменной.</summary>
    [__DynamicallyInvokable]
    public short wVarFlags;
    /// <summary>Определяет способ маршалинга переменной.</summary>
    [__DynamicallyInvokable]
    public VARKIND varkind;

    /// <summary>Содержит сведения о переменной.</summary>
    [__DynamicallyInvokable]
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct DESCUNION
    {
      /// <summary>
      ///   Указывает смещение данной переменной в пределах экземпляра.
      /// </summary>
      [__DynamicallyInvokable]
      [FieldOffset(0)]
      public int oInst;
      /// <summary>Описывает символьную константу.</summary>
      [FieldOffset(0)]
      public IntPtr lpvarValue;
    }
  }
}
