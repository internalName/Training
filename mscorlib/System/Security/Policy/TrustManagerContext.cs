// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.TrustManagerContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Представляет контекст для диспетчера доверия, которые следует учитывать при принятии решения о запуске приложения и при установке параметров безопасности на новом <see cref="T:System.AppDomain" /> для запуска приложения.
  /// </summary>
  [ComVisible(true)]
  public class TrustManagerContext
  {
    private bool m_ignorePersistedDecision;
    private TrustManagerUIContext m_uiContext;
    private bool m_noPrompt;
    private bool m_keepAlive;
    private bool m_persist;
    private ApplicationIdentity m_appId;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.TrustManagerContext" />.
    /// </summary>
    public TrustManagerContext()
      : this(TrustManagerUIContext.Run)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.TrustManagerContext" />, используя указанный объект <see cref="T:System.Security.Policy.TrustManagerUIContext" />.
    /// </summary>
    /// <param name="uiContext">
    ///   Один из <see cref="T:System.Security.Policy.TrustManagerUIContext" /> значения, которое указывает тип пользовательского интерфейса диспетчера доверия для использования.
    /// </param>
    public TrustManagerContext(TrustManagerUIContext uiContext)
    {
      this.m_ignorePersistedDecision = false;
      this.m_uiContext = uiContext;
      this.m_keepAlive = false;
      this.m_persist = true;
    }

    /// <summary>
    ///   Возвращает или задает тип пользовательского интерфейса, диспетчер доверия следует отобразить.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Policy.TrustManagerUIContext" />.
    ///    Значение по умолчанию — <see cref="F:System.Security.Policy.TrustManagerUIContext.Run" />.
    /// </returns>
    public virtual TrustManagerUIContext UIContext
    {
      get
      {
        return this.m_uiContext;
      }
      set
      {
        this.m_uiContext = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, должен ли диспетчер доверия запросить пользователя для решений о доверии.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> не запрашивать пользователя; <see langword="false" /> для запроса пользователя.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public virtual bool NoPrompt
    {
      get
      {
        return this.m_noPrompt;
      }
      set
      {
        this.m_noPrompt = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, должен ли диспетчер безопасности приложения игнорировать любые сохраненные решения и вызвать диспетчер доверия.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> для вызова доверия manager; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IgnorePersistedDecision
    {
      get
      {
        return this.m_ignorePersistedDecision;
      }
      set
      {
        this.m_ignorePersistedDecision = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, было ли диспетчер доверия сохранить в кэше состояние для этого приложения, чтобы упростить будущие запросы на определение доверия к приложению.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Чтобы кэшировать данные состояния; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public virtual bool KeepAlive
    {
      get
      {
        return this.m_keepAlive;
      }
      set
      {
        this.m_keepAlive = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, следует ли сохранить ответ пользователя в диалоговом окне согласия.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Чтобы кэшировать данные состояния; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </returns>
    public virtual bool Persist
    {
      get
      {
        return this.m_persist;
      }
      set
      {
        this.m_persist = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает идентификацию предыдущей идентификации приложения.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.ApplicationIdentity" /> Объект, представляющий предыдущий <see cref="T:System.ApplicationIdentity" />.
    /// </returns>
    public virtual ApplicationIdentity PreviousApplicationIdentity
    {
      get
      {
        return this.m_appId;
      }
      set
      {
        this.m_appId = value;
      }
    }
  }
}
