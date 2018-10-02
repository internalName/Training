// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageSecurityState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.IO.IsolatedStorage
{
  /// <summary>
  ///   Предоставляет параметры для управления размером квоты для изолированного хранилища.
  /// </summary>
  [SecurityCritical]
  public class IsolatedStorageSecurityState : SecurityState
  {
    private long m_UsedSize;
    private long m_Quota;
    private IsolatedStorageSecurityOptions m_Options;

    internal static IsolatedStorageSecurityState CreateStateToIncreaseQuotaForApplication(long newQuota, long usedSize)
    {
      return new IsolatedStorageSecurityState()
      {
        m_Options = IsolatedStorageSecurityOptions.IncreaseQuotaForApplication,
        m_Quota = newQuota,
        m_UsedSize = usedSize
      };
    }

    [SecurityCritical]
    private IsolatedStorageSecurityState()
    {
    }

    /// <summary>
    ///   Возвращает параметр для управления безопасностью изолированного хранилища.
    /// </summary>
    /// <returns>
    ///   Параметр для увеличения размера квоты изолированного хранилища.
    /// </returns>
    public IsolatedStorageSecurityOptions Options
    {
      get
      {
        return this.m_Options;
      }
    }

    /// <summary>
    ///   Возвращает текущий размер использования в изолированном хранилище.
    /// </summary>
    /// <returns>Текущий размер использования в байтах.</returns>
    public long UsedSize
    {
      get
      {
        return this.m_UsedSize;
      }
    }

    /// <summary>
    ///   Возвращает или задает текущий размер квоты для изолированного хранилища.
    /// </summary>
    /// <returns>Текущий размер квоты в байтах.</returns>
    public long Quota
    {
      get
      {
        return this.m_Quota;
      }
      set
      {
        this.m_Quota = value;
      }
    }

    /// <summary>
    ///   Гарантирует, что состояние, которое представлено <see cref="T:System.IO.IsolatedStorage.IsolatedStorageSecurityState" />, доступно на узле.
    /// </summary>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Состояние недоступно.
    /// </exception>
    [SecurityCritical]
    public override void EnsureState()
    {
      if (!this.IsStateAvailable())
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
    }
  }
}
