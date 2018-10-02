// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.FUNCDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCDESC" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  public struct FUNCDESC
  {
    /// <summary>Определяет идентификатор члена функции.</summary>
    public int memid;
    /// <summary>
    ///   Хранит число ошибок, которое функция может возвратить в 16-разрядной системе.
    /// </summary>
    public IntPtr lprgscode;
    /// <summary>
    ///   Указывает размер <see cref="F:System.Runtime.InteropServices.FUNCDESC.cParams" />.
    /// </summary>
    public IntPtr lprgelemdescParam;
    /// <summary>
    ///   Указывает, является ли функция виртуальной, статической или диспетчеризации.
    /// </summary>
    public FUNCKIND funckind;
    /// <summary>Указывает тип функции свойства.</summary>
    public INVOKEKIND invkind;
    /// <summary>Задает соглашение о вызовах функции.</summary>
    public CALLCONV callconv;
    /// <summary>Подсчитывает общее количество параметров.</summary>
    public short cParams;
    /// <summary>Подсчитывает число необязательных параметров.</summary>
    public short cParamsOpt;
    /// <summary>
    ///   Задает смещение в VTBL для <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_VIRTUAL" />.
    /// </summary>
    public short oVft;
    /// <summary>
    ///   Подсчитывает число разрешенных возвращаемых значений.
    /// </summary>
    public short cScodes;
    /// <summary>Содержит возвращаемый тип функции.</summary>
    public ELEMDESC elemdescFunc;
    /// <summary>
    ///   Указывает <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> функции.
    /// </summary>
    public short wFuncFlags;
  }
}
