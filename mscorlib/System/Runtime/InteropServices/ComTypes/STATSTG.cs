// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.STATSTG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Содержит статистические данные об открытом объекте хранилища, потока или массива байтов.
  /// </summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct STATSTG
  {
    /// <summary>
    ///   Представляет указатель нулем строку, содержащую имя объекта, описанного этой структурой.
    /// </summary>
    [__DynamicallyInvokable]
    public string pwcsName;
    /// <summary>
    ///   Указывает тип объекта хранилища, являющегося одним из значений из <see langword="STGTY" /> перечисления.
    /// </summary>
    [__DynamicallyInvokable]
    public int type;
    /// <summary>Размер в байтах для потока или массива байтов.</summary>
    [__DynamicallyInvokable]
    public long cbSize;
    /// <summary>
    ///   Указывает время последнего изменения для этого хранилища, потока или массива байтов.
    /// </summary>
    [__DynamicallyInvokable]
    public FILETIME mtime;
    /// <summary>
    ///   Указывает время создания для этого хранилища, потока или массива байтов.
    /// </summary>
    [__DynamicallyInvokable]
    public FILETIME ctime;
    /// <summary>
    ///   Указывает время последнего доступа для этого хранилища, потока или массива байтов.
    /// </summary>
    [__DynamicallyInvokable]
    public FILETIME atime;
    /// <summary>
    ///   Указывает режим доступа, который был указан при открытии объекта.
    /// </summary>
    [__DynamicallyInvokable]
    public int grfMode;
    /// <summary>
    ///   Указывает типы региональных блокировок, поддерживаемые потоком или массивом байтов.
    /// </summary>
    [__DynamicallyInvokable]
    public int grfLocksSupported;
    /// <summary>
    ///   Указывает идентификатор класса для объекта хранилища.
    /// </summary>
    [__DynamicallyInvokable]
    public Guid clsid;
    /// <summary>
    ///   Показывает текущие биты состояния объекта хранилища (значение чаще всего устанавливается <see langword="IStorage::SetStateBits" /> метод).
    /// </summary>
    [__DynamicallyInvokable]
    public int grfStateBits;
    /// <summary>Зарезервировано для будущего использования.</summary>
    [__DynamicallyInvokable]
    public int reserved;
  }
}
