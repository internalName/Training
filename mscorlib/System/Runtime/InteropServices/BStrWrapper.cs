// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.BStrWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Упаковывает данные типа <see langword="VT_BSTR" /> из управляемого в неуправляемый код.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class BStrWrapper
  {
    private string m_WrappedObject;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> с заданным <see cref="T:System.String" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект в оболочку и маршалируемый как <see langword="VT_BSTR" />.
    /// </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public BStrWrapper(string value)
    {
      this.m_WrappedObject = value;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> с заданным <see cref="T:System.Object" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект в оболочку и маршалируемый как <see langword="VT_BSTR" />.
    /// </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public BStrWrapper(object value)
    {
      this.m_WrappedObject = (string) value;
    }

    /// <summary>
    ///   Возвращает упакованный <see cref="T:System.String" /> объект для маршалинга как тип <see langword="VT_BSTR" />.
    /// </summary>
    /// <returns>
    ///   Объект, который является оболочкой для <see cref="T:System.Runtime.InteropServices.BStrWrapper" />.
    /// </returns>
    [__DynamicallyInvokable]
    public string WrappedObject
    {
      [__DynamicallyInvokable] get
      {
        return this.m_WrappedObject;
      }
    }
  }
}
