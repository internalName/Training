// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.BIND_OPTS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Сохраняет параметры, используемые при операции привязки моникера.
  /// </summary>
  [__DynamicallyInvokable]
  public struct BIND_OPTS
  {
    /// <summary>
    ///   Указывает размер в байтах, <see langword="BIND_OPTS" /> структуры.
    /// </summary>
    [__DynamicallyInvokable]
    public int cbStruct;
    /// <summary>Управляет аспектами операций привязки моникера.</summary>
    [__DynamicallyInvokable]
    public int grfFlags;
    /// <summary>
    ///   Представляет флаги, которые должны использоваться при открытии файла, содержащего объект, определенный моникером.
    /// </summary>
    [__DynamicallyInvokable]
    public int grfMode;
    /// <summary>
    ///   Указывает количество времени (часов время в миллисекундах, возвращенное функцией <see langword="GetTickCount" /> функции), вызывающий объект, указанный для завершения операции привязки.
    /// </summary>
    [__DynamicallyInvokable]
    public int dwTickCountDeadline;
  }
}
