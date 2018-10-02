// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationTrustCollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Security.Policy
{
  /// <summary>
  ///   Представляет коллекцию объектов <see cref="T:System.Security.Policy.ApplicationTrust" />.
  ///    Этот класс не наследуется.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class ApplicationTrustCollection : ICollection, IEnumerable
  {
    private static Guid ClrPropertySet = new Guid("c989bb7a-8385-4715-98cf-a741a8edb823");
    private static object s_installReference = (object) null;
    private const string ApplicationTrustProperty = "ApplicationTrust";
    private const string InstallerIdentifier = "{60051b8f-4f12-400a-8e50-dd05ebd438d1}";
    private object m_appTrusts;
    private bool m_storeBounded;
    private Store m_pStore;

    private static StoreApplicationReference InstallReference
    {
      get
      {
        if (ApplicationTrustCollection.s_installReference == null)
          Interlocked.CompareExchange(ref ApplicationTrustCollection.s_installReference, (object) new StoreApplicationReference(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", (string) null), (object) null);
        return (StoreApplicationReference) ApplicationTrustCollection.s_installReference;
      }
    }

    private ArrayList AppTrusts
    {
      [SecurityCritical] get
      {
        if (this.m_appTrusts == null)
        {
          ArrayList arrayList = new ArrayList();
          if (this.m_storeBounded)
          {
            this.RefreshStorePointer();
            foreach (IDefinitionAppId installerDeployment in this.m_pStore.EnumInstallerDeployments(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", "ApplicationTrust", (IReferenceAppId) null))
            {
              foreach (StoreOperationMetadataProperty deploymentProperty in this.m_pStore.EnumInstallerDeploymentProperties(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", "ApplicationTrust", installerDeployment))
              {
                string xml = deploymentProperty.Value;
                if (xml != null && xml.Length > 0)
                {
                  SecurityElement element = SecurityElement.FromString(xml);
                  ApplicationTrust applicationTrust = new ApplicationTrust();
                  applicationTrust.FromXml(element);
                  arrayList.Add((object) applicationTrust);
                }
              }
            }
          }
          Interlocked.CompareExchange(ref this.m_appTrusts, (object) arrayList, (object) null);
        }
        return this.m_appTrusts as ArrayList;
      }
    }

    [SecurityCritical]
    internal ApplicationTrustCollection()
      : this(false)
    {
    }

    internal ApplicationTrustCollection(bool storeBounded)
    {
      this.m_storeBounded = storeBounded;
    }

    [SecurityCritical]
    private void RefreshStorePointer()
    {
      if (this.m_pStore != null)
        Marshal.ReleaseComObject((object) this.m_pStore.InternalStore);
      this.m_pStore = IsolationInterop.GetUserStore();
    }

    /// <summary>
    ///   Возвращает количество элементов, содержащихся в коллекции.
    /// </summary>
    /// <returns>Количество элементов в коллекции.</returns>
    public int Count
    {
      [SecuritySafeCritical] get
      {
        return this.AppTrusts.Count;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Policy.ApplicationTrust" /> объект, расположенный по указанному индексу в коллекции.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс объекта в коллекции.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.ApplicationTrust" /> Объект по указанному индексу в коллекции.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> — больше или равно количеству объектов в коллекции.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="index" /> является отрицательным числом.
    /// </exception>
    public ApplicationTrust this[int index]
    {
      [SecurityCritical] get
      {
        return this.AppTrusts[index] as ApplicationTrust;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Policy.ApplicationTrust" /> объекта для указанного приложения.
    /// </summary>
    /// <param name="appFullName">Полное имя приложения.</param>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.ApplicationTrust" /> Объекта для указанного приложения или <see langword="null" /> если объект не найден.
    /// </returns>
    public ApplicationTrust this[string appFullName]
    {
      [SecurityCritical] get
      {
        ApplicationTrustCollection applicationTrustCollection = this.Find(new ApplicationIdentity(appFullName), ApplicationVersionMatch.MatchExactVersion);
        if (applicationTrustCollection.Count > 0)
          return applicationTrustCollection[0];
        return (ApplicationTrust) null;
      }
    }

    [SecurityCritical]
    private void CommitApplicationTrust(ApplicationIdentity applicationIdentity, string trustXml)
    {
      StoreOperationMetadataProperty[] SetProperties = new StoreOperationMetadataProperty[1]
      {
        new StoreOperationMetadataProperty(ApplicationTrustCollection.ClrPropertySet, "ApplicationTrust", trustXml)
      };
      IEnumDefinitionIdentity definitionIdentity1 = applicationIdentity.Identity.EnumAppPath();
      IDefinitionIdentity[] DefinitionIdentity = new IDefinitionIdentity[1];
      IDefinitionIdentity definitionIdentity2 = (IDefinitionIdentity) null;
      if (definitionIdentity1.Next(1U, DefinitionIdentity) == 1U)
        definitionIdentity2 = DefinitionIdentity[0];
      IDefinitionAppId definition = IsolationInterop.AppIdAuthority.CreateDefinition();
      definition.SetAppPath(1U, new IDefinitionIdentity[1]
      {
        definitionIdentity2
      });
      definition.put_Codebase(applicationIdentity.CodeBase);
      using (StoreTransaction storeTransaction = new StoreTransaction())
      {
        storeTransaction.Add(new StoreOperationSetDeploymentMetadata(definition, ApplicationTrustCollection.InstallReference, SetProperties));
        this.RefreshStorePointer();
        this.m_pStore.Transact(storeTransaction.Operations);
      }
      this.m_appTrusts = (object) null;
    }

    /// <summary>Добавляет элемент в коллекцию.</summary>
    /// <param name="trust">
    ///   Добавляемый объект <see cref="T:System.Security.Policy.ApplicationTrust" />.
    /// </param>
    /// <returns>Индекс, в которой был вставлен новый элемент.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="trust" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> Свойства <see cref="T:System.Security.Policy.ApplicationTrust" /> указано в <paramref name="trust" /> — <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public int Add(ApplicationTrust trust)
    {
      if (trust == null)
        throw new ArgumentNullException(nameof (trust));
      if (trust.ApplicationIdentity == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
      if (!this.m_storeBounded)
        return this.AppTrusts.Add((object) trust);
      this.CommitApplicationTrust(trust.ApplicationIdentity, trust.ToXml().ToString());
      return -1;
    }

    /// <summary>
    ///   Копирует элементы указанного <see cref="T:System.Security.Policy.ApplicationTrust" /> массива в конец коллекции.
    /// </summary>
    /// <param name="trusts">
    ///   Массив объектов типа <see cref="T:System.Security.Policy.ApplicationTrust" /> содержит объекты для добавления в коллекцию.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="trusts" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> Свойства <see cref="T:System.Security.Policy.ApplicationTrust" /> указано в <paramref name="trust" /> — <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void AddRange(ApplicationTrust[] trusts)
    {
      if (trusts == null)
        throw new ArgumentNullException(nameof (trusts));
      int index1 = 0;
      try
      {
        for (; index1 < trusts.Length; ++index1)
          this.Add(trusts[index1]);
      }
      catch
      {
        for (int index2 = 0; index2 < index1; ++index2)
          this.Remove(trusts[index2]);
        throw;
      }
    }

    /// <summary>
    ///   Копирует элементы указанного <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> в конец коллекции.
    /// </summary>
    /// <param name="trusts">
    ///   Объект <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> содержит объекты для добавления в коллекцию.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="trusts" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> Свойства <see cref="T:System.Security.Policy.ApplicationTrust" /> указано в <paramref name="trust" /> — <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void AddRange(ApplicationTrustCollection trusts)
    {
      if (trusts == null)
        throw new ArgumentNullException(nameof (trusts));
      int num = 0;
      try
      {
        foreach (ApplicationTrust trust in trusts)
        {
          this.Add(trust);
          ++num;
        }
      }
      catch
      {
        for (int index = 0; index < num; ++index)
          this.Remove(trusts[index]);
        throw;
      }
    }

    /// <summary>
    ///   Получает доверия приложения в коллекции, соответствующие заданной идентификации приложения.
    /// </summary>
    /// <param name="applicationIdentity">
    ///   <see cref="T:System.ApplicationIdentity" /> Объект, описывающий приложения для поиска.
    /// </param>
    /// <param name="versionMatch">
    ///   Одно из значений <see cref="T:System.Security.Policy.ApplicationVersionMatch" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> Содержащий все сопоставления <see cref="T:System.Security.Policy.ApplicationTrust" /> объектов.
    /// </returns>
    [SecurityCritical]
    public ApplicationTrustCollection Find(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
    {
      ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection(false);
      foreach (ApplicationTrust trust in this)
      {
        if (CmsUtils.CompareIdentities(trust.ApplicationIdentity, applicationIdentity, versionMatch))
          applicationTrustCollection.Add(trust);
      }
      return applicationTrustCollection;
    }

    /// <summary>
    ///   Удаляет все объекты доверия приложения, соответствующих указанным критериям, из коллекции.
    /// </summary>
    /// <param name="applicationIdentity">
    ///   <see cref="T:System.ApplicationIdentity" /> Из <see cref="T:System.Security.Policy.ApplicationTrust" /> удаляемый объект.
    /// </param>
    /// <param name="versionMatch">
    ///   Одно из значений <see cref="T:System.Security.Policy.ApplicationVersionMatch" />.
    /// </param>
    [SecurityCritical]
    public void Remove(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
    {
      this.RemoveRange(this.Find(applicationIdentity, versionMatch));
    }

    /// <summary>Удаляет указанного доверия приложения из коллекции.</summary>
    /// <param name="trust">
    ///   Удаляемый объект <see cref="T:System.Security.Policy.ApplicationTrust" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="trust" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> Свойство <see cref="T:System.Security.Policy.ApplicationTrust" /> объекта, заданного параметром <paramref name="trust" /> — <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void Remove(ApplicationTrust trust)
    {
      if (trust == null)
        throw new ArgumentNullException(nameof (trust));
      if (trust.ApplicationIdentity == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
      if (this.m_storeBounded)
        this.CommitApplicationTrust(trust.ApplicationIdentity, (string) null);
      else
        this.AppTrusts.Remove((object) trust);
    }

    /// <summary>
    ///   Удаляет объекты доверия приложения в указанном массиве из коллекции.
    /// </summary>
    /// <param name="trusts">
    ///   Одномерный массив типа <see cref="T:System.Security.Policy.ApplicationTrust" /> содержащий элементы, удаляемые из текущей коллекции.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="trusts" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void RemoveRange(ApplicationTrust[] trusts)
    {
      if (trusts == null)
        throw new ArgumentNullException(nameof (trusts));
      int index1 = 0;
      try
      {
        for (; index1 < trusts.Length; ++index1)
          this.Remove(trusts[index1]);
      }
      catch
      {
        for (int index2 = 0; index2 < index1; ++index2)
          this.Add(trusts[index2]);
        throw;
      }
    }

    /// <summary>
    ///   Удаляет объекты доверия приложения в указанной коллекции из коллекции.
    /// </summary>
    /// <param name="trusts">
    ///   <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> Содержащий элементы, удаляемые из currentcollection.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="trusts" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void RemoveRange(ApplicationTrustCollection trusts)
    {
      if (trusts == null)
        throw new ArgumentNullException(nameof (trusts));
      int num = 0;
      try
      {
        foreach (ApplicationTrust trust in trusts)
        {
          this.Remove(trust);
          ++num;
        }
      }
      catch
      {
        for (int index = 0; index < num; ++index)
          this.Add(trusts[index]);
        throw;
      }
    }

    /// <summary>Удаляет все доверия приложения из коллекции.</summary>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> Свойство элемента в коллекции <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void Clear()
    {
      ArrayList appTrusts = this.AppTrusts;
      if (this.m_storeBounded)
      {
        foreach (ApplicationTrust applicationTrust in appTrusts)
        {
          if (applicationTrust.ApplicationIdentity == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
          this.CommitApplicationTrust(applicationTrust.ApplicationIdentity, (string) null);
        }
      }
      appTrusts.Clear();
    }

    /// <summary>
    ///   Возвращает объект, который может использоваться для итерации по коллекции.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.ApplicationTrustEnumerator" /> Может использоваться для итерации по коллекции.
    /// </returns>
    public ApplicationTrustEnumerator GetEnumerator()
    {
      return new ApplicationTrustEnumerator(this);
    }

    [SecuritySafeCritical]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new ApplicationTrustEnumerator(this);
    }

    [SecuritySafeCritical]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (index < 0 || index >= array.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (array.Length - index < this.Count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      for (int index1 = 0; index1 < this.Count; ++index1)
        array.SetValue((object) this[index1], index++);
    }

    /// <summary>
    ///   Копирует коллекцию в совместимый одномерный массив, начиная с указанного индекса массива назначения.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив типа <see cref="T:System.Security.Policy.ApplicationTrust" /> является целевым для элементов, копируемых из текущей коллекции.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> меньше нижней границы массива <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="array" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Число элементов в <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    public void CopyTo(ApplicationTrust[] array, int index)
    {
      ((ICollection) this).CopyTo((Array) array, index);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли доступ к коллекции синхронизированным (потокобезопасным).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="false" /> во всех случаях.
    /// </returns>
    public bool IsSynchronized
    {
      [SecuritySafeCritical] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает объект, который можно использовать для синхронизации доступа к коллекции.
    /// </summary>
    /// <returns>
    ///   Объект, используемый для синхронизации доступа к коллекции.
    /// </returns>
    public object SyncRoot
    {
      [SecuritySafeCritical] get
      {
        return (object) this;
      }
    }
  }
}
