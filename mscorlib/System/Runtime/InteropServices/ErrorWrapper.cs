// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ErrorWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Инкапсулирует объекты, которые необходимо маршалировать, как <see langword="VT_ERROR" />.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ErrorWrapper
  {
    private int m_ErrorCode;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> класса со значением HRESULT ошибки.
    /// </summary>
    /// <param name="errorCode">Значение HRESULT ошибки.</param>
    [__DynamicallyInvokable]
    public ErrorWrapper(int errorCode)
    {
      this.m_ErrorCode = errorCode;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> класса object, содержащий значение HRESULT ошибки.
    /// </summary>
    /// <param name="errorCode">
    ///   Объект, содержащий значение HRESULT ошибки.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="errorCode" /> Параметр не <see cref="T:System.Int32" /> типа.
    /// </exception>
    [__DynamicallyInvokable]
    public ErrorWrapper(object errorCode)
    {
      if (!(errorCode is int))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt32"), nameof (errorCode));
      this.m_ErrorCode = (int) errorCode;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> класса со значением HRESULT, соответствующее исключению.
    /// </summary>
    /// <param name="e">Исключение, преобразуемое в код ошибки.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public ErrorWrapper(Exception e)
    {
      this.m_ErrorCode = Marshal.GetHRForException(e);
    }

    /// <summary>Возвращает код ошибки оболочки.</summary>
    /// <returns>Значение HRESULT ошибки.</returns>
    [__DynamicallyInvokable]
    public int ErrorCode
    {
      [__DynamicallyInvokable] get
      {
        return this.m_ErrorCode;
      }
    }
  }
}
