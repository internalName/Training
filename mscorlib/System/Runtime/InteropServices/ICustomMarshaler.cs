// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ICustomMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет настраиваемые оболочки, обрабатывающих вызовы методов.
  /// </summary>
  [ComVisible(true)]
  public interface ICustomMarshaler
  {
    /// <summary>Преобразует неуправляемые данные в управляемые.</summary>
    /// <param name="pNativeData">
    ///   Указатель на неуправляемые данные, который следует упаковать.
    /// </param>
    /// <returns>
    ///   Объект, представляющий управляемое представление данных COM.
    /// </returns>
    object MarshalNativeToManaged(IntPtr pNativeData);

    /// <summary>
    ///   Преобразует управляемые данные, неуправляемые данные.
    /// </summary>
    /// <param name="ManagedObj">
    ///   Управляемый объект для преобразования.
    /// </param>
    /// <returns>
    ///   Указатель на COM-представление управляемого объекта.
    /// </returns>
    IntPtr MarshalManagedToNative(object ManagedObj);

    /// <summary>
    ///   Выполняет необходимую очистку неуправляемых данных, если они больше не нужны.
    /// </summary>
    /// <param name="pNativeData">
    ///   Указатель на неуправляемые данные будут уничтожены.
    /// </param>
    void CleanUpNativeData(IntPtr pNativeData);

    /// <summary>
    ///   Выполняет необходимую очистку управляемых данных, если они больше не нужны.
    /// </summary>
    /// <param name="ManagedObj">
    ///   Управляемый объект будет уничтожен.
    /// </param>
    void CleanUpManagedData(object ManagedObj);

    /// <summary>
    ///   Возвращает размер собственных данных для маршалинга.
    /// </summary>
    /// <returns>Размер в байтах собственных данных.</returns>
    int GetNativeDataSize();
  }
}
