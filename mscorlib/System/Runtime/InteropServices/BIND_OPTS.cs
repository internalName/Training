// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.BIND_OPTS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.BIND_OPTS" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.BIND_OPTS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  public struct BIND_OPTS
  {
    /// <summary>
    ///   Указывает размер <see langword="BIND_OPTS" /> структуры в байтах.
    /// </summary>
    public int cbStruct;
    /// <summary>Управляет аспектами операций привязки моникера.</summary>
    public int grfFlags;
    /// <summary>
    ///   Флаги, которые должны использоваться при открытии файла, содержащего объект, определенный моникером.
    /// </summary>
    public int grfMode;
    /// <summary>
    ///   Указывает количество времени (часов время в миллисекундах, возвращенное функцией <see langword="GetTickCount" /> функция) вызывающим объектом для завершения операции привязки.
    /// </summary>
    public int dwTickCountDeadline;
  }
}
