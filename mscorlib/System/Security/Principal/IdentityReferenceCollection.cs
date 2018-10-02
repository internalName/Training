// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IdentityReferenceCollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Principal
{
  /// <summary>
  ///   Представляет коллекцию объектов <see cref="T:System.Security.Principal.IdentityReference" /> и обеспечивает средства преобразования наборов объектов, производных от <see cref="T:System.Security.Principal.IdentityReference" />, в типы, производные от <see cref="T:System.Security.Principal.IdentityReference" />.
  /// </summary>
  [ComVisible(false)]
  public class IdentityReferenceCollection : ICollection<IdentityReference>, IEnumerable<IdentityReference>, IEnumerable
  {
    private List<IdentityReference> _Identities;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> нулевыми элементами в коллекции.
    /// </summary>
    public IdentityReferenceCollection()
      : this(0)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.IdentityReferenceCollection" />, используя заданный исходный размер.
    /// </summary>
    /// <param name="capacity">
    ///   Исходное число элементов в коллекции.
    ///    Значение параметра <paramref name="capacity" /> служит только в качестве подсказки; необязательно создается максимальное число элементов.
    /// </param>
    public IdentityReferenceCollection(int capacity)
    {
      this._Identities = new List<IdentityReference>(capacity);
    }

    /// <summary>
    ///   Копирует коллекцию <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> в массив <see cref="T:System.Security.Principal.IdentityReferenceCollection" />, начиная с указанного индекса.
    /// </summary>
    /// <param name="array">
    ///   Объект массива <see cref="T:System.Security.Principal.IdentityReferenceCollection" />, в который должна быть скопирована коллекция <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </param>
    /// <param name="offset">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, который обозначает позицию для копирования коллекции <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </param>
    public void CopyTo(IdentityReference[] array, int offset)
    {
      this._Identities.CopyTo(0, array, offset, this.Count);
    }

    /// <summary>
    ///   Возвращает число элементов в коллекции <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </summary>
    /// <returns>
    ///   Число объектов <see cref="T:System.Security.Principal.IdentityReference" /> в коллекции <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </returns>
    public int Count
    {
      get
      {
        return this._Identities.Count;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее на то, доступна ли коллекция <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> только для чтения.
    /// </summary>
    /// <returns>
    ///   Всегда возвращает значение <see langword="false" />.
    /// </returns>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Добавляет объект <see cref="T:System.Security.Principal.IdentityReference" /> в коллекцию <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </summary>
    /// <param name="identity">
    ///   Объект <see cref="T:System.Security.Principal.IdentityReference" /> для добавления в коллекцию.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    public void Add(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException(nameof (identity));
      this._Identities.Add(identity);
    }

    /// <summary>
    ///   Удаляет указанный объект <see cref="T:System.Security.Principal.IdentityReference" /> из коллекции.
    /// </summary>
    /// <param name="identity">
    ///   Удаляемый объект <see cref="T:System.Security.Principal.IdentityReference" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если заданный объект был удален из коллекции.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool Remove(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException(nameof (identity));
      if (!this.Contains(identity))
        return false;
      this._Identities.Remove(identity);
      return true;
    }

    /// <summary>
    ///   Удаляет все объекты <see cref="T:System.Security.Principal.IdentityReference" /> из коллекции <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </summary>
    public void Clear()
    {
      this._Identities.Clear();
    }

    /// <summary>
    ///   Указывает, содержит ли коллекция <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> заданный объект <see cref="T:System.Security.Principal.IdentityReference" />.
    /// </summary>
    /// <param name="identity">
    ///   Объект <see cref="T:System.Security.Principal.IdentityReference" /> для проверки.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если коллекция содержит заданный объект.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool Contains(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException(nameof (identity));
      return this._Identities.Contains(identity);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    /// <summary>
    ///   Возвращает перечислитель, который может использоваться для выполнения итерации по коллекции <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </summary>
    /// <returns>
    ///   Перечислитель для коллекции <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </returns>
    public IEnumerator<IdentityReference> GetEnumerator()
    {
      return (IEnumerator<IdentityReference>) new IdentityReferenceEnumerator(this);
    }

    /// <summary>
    ///   Устанавливает или возвращает узел по заданному индексу коллекции <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс в коллекции <see cref="T:System.Security.Principal.IdentityReferenceCollection" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Principal.IdentityReference" />, содержащийся в коллекции по указанному индексу.
    ///    Если значение параметра <paramref name="index" /> больше или равно числу узлов в коллекции, возвращается значение <see langword="null" />.
    /// </returns>
    public IdentityReference this[int index]
    {
      get
      {
        return this._Identities[index];
      }
      set
      {
        if (value == (IdentityReference) null)
          throw new ArgumentNullException(nameof (value));
        this._Identities[index] = value;
      }
    }

    internal List<IdentityReference> Identities
    {
      get
      {
        return this._Identities;
      }
    }

    /// <summary>
    ///   Преобразует объекты коллекции в указанный тип.
    ///    Этот метод вызывается так же, как метод <see cref="M:System.Security.Principal.IdentityReferenceCollection.Translate(System.Type,System.Boolean)" /> со значением <see langword="false" /> второго параметра. Это означает, что для элементов, преобразование которых завершается неудачно, исключения выдаваться не будут.
    /// </summary>
    /// <param name="targetType">
    ///   Тип, в который преобразуются элементы коллекции.
    /// </param>
    /// <returns>
    ///   Коллекция <see cref="T:System.Security.Principal.IdentityReferenceCollection" />, представляющая преобразованное содержимое исходной коллекции.
    /// </returns>
    public IdentityReferenceCollection Translate(Type targetType)
    {
      return this.Translate(targetType, false);
    }

    /// <summary>
    ///   Преобразует объекты коллекции в указанный тип и использует заданную отказоустойчивость для обработки или игнорирования ошибок, связанных с типом, не имеющим сопоставление преобразования.
    /// </summary>
    /// <param name="targetType">
    ///   Тип, в который преобразуются элементы коллекции.
    /// </param>
    /// <param name="forceSuccess">
    ///   Логическое значение, определяющее способ обработки ошибок преобразования.
    /// 
    ///   Если параметр <paramref name="forceSuccess" /> имеет значение <see langword="true" />, ошибки преобразования из-за необнаружения сопоставления во время преобразования приводят к сбою преобразования и вызову исключений.
    /// 
    ///   Если параметр <paramref name="forceSuccess" /> имеет значение <see langword="false" />, типы, которые не удалось преобразовать из-за необнаружения сопоставления во время преобразования, копируются в возвращаемую коллекцию без преобразования.
    /// </param>
    /// <returns>
    ///   Коллекция <see cref="T:System.Security.Principal.IdentityReferenceCollection" />, представляющая преобразованное содержимое исходной коллекции.
    /// </returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
    public IdentityReferenceCollection Translate(Type targetType, bool forceSuccess)
    {
      if (targetType == (Type) null)
        throw new ArgumentNullException(nameof (targetType));
      if (!targetType.IsSubclassOf(typeof (IdentityReference)))
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), nameof (targetType));
      if (this.Identities.Count == 0)
        return new IdentityReferenceCollection();
      int capacity1 = 0;
      int capacity2 = 0;
      for (int index = 0; index < this.Identities.Count; ++index)
      {
        Type type = this.Identities[index].GetType();
        if (!(type == targetType))
        {
          if (type == typeof (SecurityIdentifier))
          {
            ++capacity1;
          }
          else
          {
            if (!(type == typeof (NTAccount)))
              throw new SystemException();
            ++capacity2;
          }
        }
      }
      bool flag = false;
      IdentityReferenceCollection sourceSids = (IdentityReferenceCollection) null;
      IdentityReferenceCollection sourceAccounts = (IdentityReferenceCollection) null;
      if (capacity1 == this.Count)
      {
        flag = true;
        sourceSids = this;
      }
      else if (capacity1 > 0)
        sourceSids = new IdentityReferenceCollection(capacity1);
      if (capacity2 == this.Count)
      {
        flag = true;
        sourceAccounts = this;
      }
      else if (capacity2 > 0)
        sourceAccounts = new IdentityReferenceCollection(capacity2);
      IdentityReferenceCollection referenceCollection1 = (IdentityReferenceCollection) null;
      if (!flag)
      {
        referenceCollection1 = new IdentityReferenceCollection(this.Identities.Count);
        for (int index = 0; index < this.Identities.Count; ++index)
        {
          IdentityReference identity = this[index];
          Type type = identity.GetType();
          if (!(type == targetType))
          {
            if (type == typeof (SecurityIdentifier))
            {
              sourceSids.Add(identity);
            }
            else
            {
              if (!(type == typeof (NTAccount)))
                throw new SystemException();
              sourceAccounts.Add(identity);
            }
          }
        }
      }
      bool someFailed = false;
      IdentityReferenceCollection referenceCollection2 = (IdentityReferenceCollection) null;
      IdentityReferenceCollection referenceCollection3 = (IdentityReferenceCollection) null;
      if (capacity1 > 0)
      {
        referenceCollection2 = SecurityIdentifier.Translate(sourceSids, targetType, out someFailed);
        if (flag && !(forceSuccess & someFailed))
          referenceCollection1 = referenceCollection2;
      }
      if (capacity2 > 0)
      {
        referenceCollection3 = NTAccount.Translate(sourceAccounts, targetType, out someFailed);
        if (flag && !(forceSuccess & someFailed))
          referenceCollection1 = referenceCollection3;
      }
      if (forceSuccess & someFailed)
      {
        IdentityReferenceCollection unmappedIdentities = new IdentityReferenceCollection();
        if (referenceCollection2 != null)
        {
          foreach (IdentityReference identity in referenceCollection2)
          {
            if (identity.GetType() != targetType)
              unmappedIdentities.Add(identity);
          }
        }
        if (referenceCollection3 != null)
        {
          foreach (IdentityReference identity in referenceCollection3)
          {
            if (identity.GetType() != targetType)
              unmappedIdentities.Add(identity);
          }
        }
        throw new IdentityNotMappedException(Environment.GetResourceString("IdentityReference_IdentityNotMapped"), unmappedIdentities);
      }
      if (!flag)
      {
        int num1 = 0;
        int num2 = 0;
        referenceCollection1 = new IdentityReferenceCollection(this.Identities.Count);
        for (int index = 0; index < this.Identities.Count; ++index)
        {
          IdentityReference identity = this[index];
          Type type = identity.GetType();
          if (type == targetType)
            referenceCollection1.Add(identity);
          else if (type == typeof (SecurityIdentifier))
          {
            referenceCollection1.Add(referenceCollection2[num1++]);
          }
          else
          {
            if (!(type == typeof (NTAccount)))
              throw new SystemException();
            referenceCollection1.Add(referenceCollection3[num2++]);
          }
        }
      }
      return referenceCollection1;
    }
  }
}
