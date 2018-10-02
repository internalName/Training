// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationTrustEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Представляет перечислитель для объектов <see cref="T:System.Security.Policy.ApplicationTrust" /> в коллекции <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.
  /// </summary>
  [ComVisible(true)]
  public sealed class ApplicationTrustEnumerator : IEnumerator
  {
    [SecurityCritical]
    private ApplicationTrustCollection m_trusts;
    private int m_current;

    private ApplicationTrustEnumerator()
    {
    }

    [SecurityCritical]
    internal ApplicationTrustEnumerator(ApplicationTrustCollection trusts)
    {
      this.m_trusts = trusts;
      this.m_current = -1;
    }

    /// <summary>
    ///   Возвращает текущую <see cref="T:System.Security.Policy.ApplicationTrust" /> объекта в <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> коллекции.
    /// </summary>
    /// <returns>
    ///   Текущий <see cref="T:System.Security.Policy.ApplicationTrust" /> в <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.
    /// </returns>
    public ApplicationTrust Current
    {
      [SecuritySafeCritical] get
      {
        return this.m_trusts[this.m_current];
      }
    }

    object IEnumerator.Current
    {
      [SecuritySafeCritical] get
      {
        return (object) this.m_trusts[this.m_current];
      }
    }

    /// <summary>
    ///   Переходит к следующему элементу в <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> коллекции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если перечислитель был успешно перемещен к следующему элементу; значение <see langword="false" />, если перечислитель достиг конца коллекции.
    /// </returns>
    [SecuritySafeCritical]
    public bool MoveNext()
    {
      if (this.m_current == this.m_trusts.Count - 1)
        return false;
      ++this.m_current;
      return true;
    }

    /// <summary>
    ///   Устанавливает перечислитель в начало <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> коллекции.
    /// </summary>
    public void Reset()
    {
      this.m_current = -1;
    }
  }
}
