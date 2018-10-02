// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DispatchWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Инкапсулирует объекты, которые необходимо маршалировать, как <see langword="VT_DISPATCH" />.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DispatchWrapper
  {
    private object m_WrappedObject;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.DispatchWrapper" /> класса Инкапсулируемый объект.
    /// </summary>
    /// <param name="obj">
    ///   Необходимо заключить и преобразовать в объект <see cref="F:System.Runtime.InteropServices.VarEnum.VT_DISPATCH" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="obj" /> не является классом или массивом.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="obj" /> не поддерживает <see langword="IDispatch" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="obj" /> Параметр был отмечен атрибутом <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" />  атрибута, которое было передано значение <see langword="false" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="obj" /> Параметр наследует от типа, отмеченного <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" />  атрибута, которое было передано значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public DispatchWrapper(object obj)
    {
      if (obj != null)
        Marshal.Release(Marshal.GetIDispatchForObject(obj));
      this.m_WrappedObject = obj;
    }

    /// <summary>
    ///   Возвращает объект, перезаписанный <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.
    /// </summary>
    /// <returns>
    ///   Объект, перезаписанный <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.
    /// </returns>
    [__DynamicallyInvokable]
    public object WrappedObject
    {
      [__DynamicallyInvokable] get
      {
        return this.m_WrappedObject;
      }
    }
  }
}
