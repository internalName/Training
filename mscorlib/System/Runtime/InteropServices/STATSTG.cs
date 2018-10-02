// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.STATSTG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.STATSTG" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.STATSTG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct STATSTG
  {
    /// <summary>
    ///   Указатель нулем строку, содержащую имя объекта, описанного этой структурой.
    /// </summary>
    public string pwcsName;
    /// <summary>
    ///   Указывает тип объекта хранилища, являющегося одним из значений из <see langword="STGTY" /> перечисления.
    /// </summary>
    public int type;
    /// <summary>
    ///   Указывает размер в байтах для потока или массива байтов.
    /// </summary>
    public long cbSize;
    /// <summary>
    ///   Указывает время последнего изменения для этого хранилища, потока или массива байтов.
    /// </summary>
    public FILETIME mtime;
    /// <summary>
    ///   Указывает время создания для этого хранилища, потока или массива байтов.
    /// </summary>
    public FILETIME ctime;
    /// <summary>
    ///   Указывает время последнего доступа для данного хранилища, потока или массива байтов
    /// </summary>
    public FILETIME atime;
    /// <summary>
    ///   Указывает режим доступа, который был указан при открытии объекта.
    /// </summary>
    public int grfMode;
    /// <summary>
    ///   Указывает типы региональных блокировок, поддерживаемые потоком или массивом байтов.
    /// </summary>
    public int grfLocksSupported;
    /// <summary>
    ///   Указывает идентификатор класса для объекта хранилища.
    /// </summary>
    public Guid clsid;
    /// <summary>
    ///   Показывает текущие биты состояния объекта хранилища (значение чаще всего устанавливается <see langword="IStorage::SetStateBits" /> метод).
    /// </summary>
    public int grfStateBits;
    /// <summary>Зарезервировано для будущего использования.</summary>
    public int reserved;
  }
}
