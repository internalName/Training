// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ICustomQueryInterface
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Дает разработчикам возможность создавать пользовательские управляемые реализации IUnknown::QueryInterface(REFIID riid, void **ppvObject) метод.
  /// </summary>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public interface ICustomQueryInterface
  {
    /// <summary>
    ///   Возвращает интерфейс в соответствии с идентификатором указанного интерфейса.
    /// </summary>
    /// <param name="iid">
    ///   Идентификатор GUID запрашиваемого интерфейса.
    /// </param>
    /// <param name="ppv">
    ///   Ссылка на запрашиваемый интерфейс при возвращении данного метода.
    /// </param>
    /// <returns>
    ///   Одно из значений перечисления, указывающее, является ли пользовательская реализация IUnknown::QueryInterface был использован.
    /// </returns>
    [SecurityCritical]
    CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv);
  }
}
