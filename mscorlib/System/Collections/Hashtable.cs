// Decompiled with JetBrains decompiler
// Type: System.Collections.Hashtable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
  /// <summary>
  ///   Представляет коллекцию пар «ключ-значение», которые упорядочены по хэш-коду ключа.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  [DebuggerTypeProxy(typeof (Hashtable.HashtableDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class Hashtable : IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
  {
    internal const int HashPrime = 101;
    private const int InitialSize = 3;
    private const string LoadFactorName = "LoadFactor";
    private const string VersionName = "Version";
    private const string ComparerName = "Comparer";
    private const string HashCodeProviderName = "HashCodeProvider";
    private const string HashSizeName = "HashSize";
    private const string KeysName = "Keys";
    private const string ValuesName = "Values";
    private const string KeyComparerName = "KeyComparer";
    private Hashtable.bucket[] buckets;
    private int count;
    private int occupancy;
    private int loadsize;
    private float loadFactor;
    private volatile int version;
    private volatile bool isWriterInProgress;
    private ICollection keys;
    private ICollection values;
    private IEqualityComparer _keycomparer;
    private object _syncRoot;

    /// <summary>
    ///   Получает или задает объект, который может распределять хэш-коды.
    /// </summary>
    /// <returns>Объект, который может распределять хэш-коды.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойству присвоено значение, но хэш-таблица была создана с помощью <see cref="T:System.Collections.IEqualityComparer" />.
    /// </exception>
    [Obsolete("Please use EqualityComparer property.")]
    protected IHashCodeProvider hcp
    {
      get
      {
        if (this._keycomparer is CompatibleComparer)
          return ((CompatibleComparer) this._keycomparer).HashCodeProvider;
        if (this._keycomparer == null)
          return (IHashCodeProvider) null;
        throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
      }
      set
      {
        if (this._keycomparer is CompatibleComparer)
        {
          this._keycomparer = (IEqualityComparer) new CompatibleComparer(((CompatibleComparer) this._keycomparer).Comparer, value);
        }
        else
        {
          if (this._keycomparer != null)
            throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
          this._keycomparer = (IEqualityComparer) new CompatibleComparer((IComparer) null, value);
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Collections.IComparer" /> для <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IComparer" /> Для <see cref="T:System.Collections.Hashtable" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойству присвоено значение, но хэш-таблица была создана с помощью <see cref="T:System.Collections.IEqualityComparer" />.
    /// </exception>
    [Obsolete("Please use KeyComparer properties.")]
    protected IComparer comparer
    {
      get
      {
        if (this._keycomparer is CompatibleComparer)
          return ((CompatibleComparer) this._keycomparer).Comparer;
        if (this._keycomparer == null)
          return (IComparer) null;
        throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
      }
      set
      {
        if (this._keycomparer is CompatibleComparer)
        {
          CompatibleComparer keycomparer = (CompatibleComparer) this._keycomparer;
          this._keycomparer = (IEqualityComparer) new CompatibleComparer(value, keycomparer.HashCodeProvider);
        }
        else
        {
          if (this._keycomparer != null)
            throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
          this._keycomparer = (IEqualityComparer) new CompatibleComparer(value, (IHashCodeProvider) null);
        }
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IEqualityComparer" /> для <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IEqualityComparer" /> Для <see cref="T:System.Collections.Hashtable" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойству присвоено значение, но хэш-таблица была создана с помощью <see cref="T:System.Collections.IHashCodeProvider" /> и <see cref="T:System.Collections.IComparer" />.
    /// </exception>
    protected IEqualityComparer EqualityComparer
    {
      get
      {
        return this._keycomparer;
      }
    }

    internal Hashtable(bool trash)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с исходной емкостью, показателем загрузки, поставщиком хэш-кода и компаратор.
    /// </summary>
    public Hashtable()
      : this(0, 1f)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с использованием указанной начальной емкостью и по умолчанию показателем загрузки, поставщиком хэш-кода и компаратор.
    /// </summary>
    /// <param name="capacity">
    ///   Приблизительное количество элементов, <see cref="T:System.Collections.Hashtable" /> может первоначально содержать объект.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// </exception>
    public Hashtable(int capacity)
      : this(capacity, 1f)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с указанной исходной емкостью и показателем загрузки и по умолчанию поставщиком хэш-кода и компаратор.
    /// </summary>
    /// <param name="capacity">
    ///   Приблизительное количество элементов, <see cref="T:System.Collections.Hashtable" /> может первоначально содержать объект.
    /// </param>
    /// <param name="loadFactor">
    ///   Число в диапазоне от 0,1 до 1,0, умноженное на значение по умолчанию, обеспечивающее наилучшую производительность.
    ///    Результат определяет максимальное отношение количества элементов к количеству сегментов.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="loadFactor" /> меньше 0,1.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="loadFactor" /> больше, чем 1.0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="capacity" /> вызывает переполнение.
    /// </exception>
    public Hashtable(int capacity, float loadFactor)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if ((double) loadFactor < 0.100000001490116 || (double) loadFactor > 1.0)
        throw new ArgumentOutOfRangeException(nameof (loadFactor), Environment.GetResourceString("ArgumentOutOfRange_HashtableLoadFactor", (object) 0.1, (object) 1.0));
      this.loadFactor = 0.72f * loadFactor;
      double num = (double) capacity / (double) this.loadFactor;
      if (num > (double) int.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Arg_HTCapacityOverflow"));
      int length = num > 3.0 ? HashHelpers.GetPrime((int) num) : 3;
      this.buckets = new Hashtable.bucket[length];
      this.loadsize = (int) ((double) this.loadFactor * (double) length);
      this.isWriterInProgress = false;
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с указанной начальной емкостью, показателем загрузки, поставщиком хэш-кода и компаратор.
    /// </summary>
    /// <param name="capacity">
    ///   Приблизительное количество элементов, <see cref="T:System.Collections.Hashtable" /> может первоначально содержать объект.
    /// </param>
    /// <param name="loadFactor">
    ///   Число в диапазоне от 0,1 до 1,0, умноженное на значение по умолчанию, обеспечивающее наилучшую производительность.
    ///    Результат определяет максимальное отношение количества элементов к количеству сегментов.
    /// </param>
    /// <param name="hcp">
    ///   <see cref="T:System.Collections.IHashCodeProvider" /> Объект, предоставляющий хэш-коды для всех ключей в <see cref="T:System.Collections.Hashtable" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования поставщика хэш-кода по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" />.
    /// </param>
    /// <param name="comparer">
    ///   <see cref="T:System.Collections.IComparer" /> Объект, используемый для определения равенства двух ключей.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> Чтобы использовать функцию сравнения по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="loadFactor" /> меньше 0,1.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="loadFactor" /> больше, чем 1.0.
    /// </exception>
    [Obsolete("Please use Hashtable(int, float, IEqualityComparer) instead.")]
    public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
      : this(capacity, loadFactor)
    {
      if (hcp == null && comparer == null)
        this._keycomparer = (IEqualityComparer) null;
      else
        this._keycomparer = (IEqualityComparer) new CompatibleComparer(comparer, hcp);
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с использованием указанной начальной емкостью, показателем загрузки и <see cref="T:System.Collections.IEqualityComparer" /> объекта.
    /// </summary>
    /// <param name="capacity">
    ///   Приблизительное количество элементов, <see cref="T:System.Collections.Hashtable" /> может первоначально содержать объект.
    /// </param>
    /// <param name="loadFactor">
    ///   Число в диапазоне от 0,1 до 1,0, умноженное на значение по умолчанию, обеспечивающее наилучшую производительность.
    ///    Результат определяет максимальное отношение количества элементов к количеству сегментов.
    /// </param>
    /// <param name="equalityComparer">
    ///   <see cref="T:System.Collections.IEqualityComparer" /> Объект, определяющий поставщик хэш-кода и компаратор для использования с <see cref="T:System.Collections.Hashtable" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования поставщика хэш-кода по умолчанию и функцию сравнения по умолчанию.
    ///    Хеш-кода является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" /> и блок сравнения по умолчанию является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="loadFactor" /> меньше 0,1.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="loadFactor" /> больше, чем 1.0.
    /// </exception>
    public Hashtable(int capacity, float loadFactor, IEqualityComparer equalityComparer)
      : this(capacity, loadFactor)
    {
      this._keycomparer = equalityComparer;
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с начальной емкостью по умолчанию и показателем загрузки и указанным хеш-кода и компаратор.
    /// </summary>
    /// <param name="hcp">
    ///   <see cref="T:System.Collections.IHashCodeProvider" /> Объект, предоставляющий хэш-коды для всех ключей в <see cref="T:System.Collections.Hashtable" /> объекта.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования поставщика хэш-кода по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" />.
    /// </param>
    /// <param name="comparer">
    ///   <see cref="T:System.Collections.IComparer" /> Объект, используемый для определения равенства двух ключей.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> Чтобы использовать функцию сравнения по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    [Obsolete("Please use Hashtable(IEqualityComparer) instead.")]
    public Hashtable(IHashCodeProvider hcp, IComparer comparer)
      : this(0, 1f, hcp, comparer)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с по умолчанию исходной емкостью и показателем загрузки и указанным <see cref="T:System.Collections.IEqualityComparer" /> объекта.
    /// </summary>
    /// <param name="equalityComparer">
    ///   <see cref="T:System.Collections.IEqualityComparer" /> Объект, определяющий поставщик хэш-кода и компаратор для использования с <see cref="T:System.Collections.Hashtable" /> объекта.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования поставщика хэш-кода по умолчанию и функцию сравнения по умолчанию.
    ///    Хеш-кода является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" /> и блок сравнения по умолчанию является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    public Hashtable(IEqualityComparer equalityComparer)
      : this(0, 1f, equalityComparer)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с использованием указанной начальной емкостью, поставщиком хэш-кода, сравнения и по умолчанию показателем загрузки.
    /// </summary>
    /// <param name="capacity">
    ///   Приблизительное количество элементов, <see cref="T:System.Collections.Hashtable" /> может первоначально содержать объект.
    /// </param>
    /// <param name="hcp">
    ///   <see cref="T:System.Collections.IHashCodeProvider" /> Объект, предоставляющий хэш-коды для всех ключей в <see cref="T:System.Collections.Hashtable" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования поставщика хэш-кода по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" />.
    /// </param>
    /// <param name="comparer">
    ///   <see cref="T:System.Collections.IComparer" /> Объект, используемый для определения равенства двух ключей.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> Чтобы использовать функцию сравнения по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// </exception>
    [Obsolete("Please use Hashtable(int, IEqualityComparer) instead.")]
    public Hashtable(int capacity, IHashCodeProvider hcp, IComparer comparer)
      : this(capacity, 1f, hcp, comparer)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с указанной исходной емкостью и <see cref="T:System.Collections.IEqualityComparer" />, и по умолчанию показателем загрузки.
    /// </summary>
    /// <param name="capacity">
    ///   Приблизительное количество элементов, <see cref="T:System.Collections.Hashtable" /> может первоначально содержать объект.
    /// </param>
    /// <param name="equalityComparer">
    ///   <see cref="T:System.Collections.IEqualityComparer" /> Объект, определяющий поставщик хэш-кода и компаратор для использования с <see cref="T:System.Collections.Hashtable" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования поставщика хэш-кода по умолчанию и функцию сравнения по умолчанию.
    ///    Хеш-кода является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" /> и блок сравнения по умолчанию является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// </exception>
    public Hashtable(int capacity, IEqualityComparer equalityComparer)
      : this(capacity, 1f, equalityComparer)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Hashtable" /> класса путем копирования элементов из указанного словаря в новый <see cref="T:System.Collections.Hashtable" /> объекта.
    ///    Новый <see cref="T:System.Collections.Hashtable" /> объект обладает начальной емкостью, равной количеству скопированных элементов и использует по умолчанию показателем загрузки, поставщиком хэш-кода и компаратор.
    /// </summary>
    /// <param name="d">
    ///   <see cref="T:System.Collections.IDictionary" /> Скопировать в новый объект <see cref="T:System.Collections.Hashtable" /> объект.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    public Hashtable(IDictionary d)
      : this(d, 1f)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Hashtable" /> класса путем копирования элементов из указанного словаря в новый <see cref="T:System.Collections.Hashtable" /> объекта.
    ///    Новый <see cref="T:System.Collections.Hashtable" /> объект обладает начальной емкостью, равной количеству скопированных элементов и обладает указанным показателем загрузки и по умолчанию поставщиком хэш-кода и объектом сравнения.
    /// </summary>
    /// <param name="d">
    ///   <see cref="T:System.Collections.IDictionary" /> Скопировать в новый объект <see cref="T:System.Collections.Hashtable" /> объект.
    /// </param>
    /// <param name="loadFactor">
    ///   Число в диапазоне от 0,1 до 1,0, умноженное на значение по умолчанию, обеспечивающее наилучшую производительность.
    ///    Результат определяет максимальное отношение количества элементов к количеству сегментов.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="loadFactor" /> меньше 0,1.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="loadFactor" /> больше, чем 1.0.
    /// </exception>
    public Hashtable(IDictionary d, float loadFactor)
      : this(d, loadFactor, (IEqualityComparer) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Hashtable" /> класса путем копирования элементов из указанного словаря в новый <see cref="T:System.Collections.Hashtable" /> объекта.
    ///    Новый <see cref="T:System.Collections.Hashtable" /> объект обладает начальной емкостью, равной количеству скопированных элементов и использует по умолчанию показателем загрузки и указанным хеш-кода и объектом сравнения.
    ///    Этот API устарел.
    ///    Кроме того, в разделе <see cref="M:System.Collections.Hashtable.#ctor(System.Collections.IDictionary,System.Collections.IEqualityComparer)" />.
    /// </summary>
    /// <param name="d">
    ///   <see cref="T:System.Collections.IDictionary" /> Скопировать в новый объект <see cref="T:System.Collections.Hashtable" /> объект.
    /// </param>
    /// <param name="hcp">
    ///   <see cref="T:System.Collections.IHashCodeProvider" /> Объект, предоставляющий хэш-коды для всех ключей в <see cref="T:System.Collections.Hashtable" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования поставщика хэш-кода по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" />.
    /// </param>
    /// <param name="comparer">
    ///   <see cref="T:System.Collections.IComparer" /> Объект, используемый для определения равенства двух ключей.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> Чтобы использовать функцию сравнения по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    [Obsolete("Please use Hashtable(IDictionary, IEqualityComparer) instead.")]
    public Hashtable(IDictionary d, IHashCodeProvider hcp, IComparer comparer)
      : this(d, 1f, hcp, comparer)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Hashtable" /> класса путем копирования элементов из указанного словаря в новый <see cref="T:System.Collections.Hashtable" /> объекта.
    ///    Новый <see cref="T:System.Collections.Hashtable" /> объект обладает начальной емкостью, равной количеству скопированных элементов и использует по умолчанию показателем загрузки и указанным <see cref="T:System.Collections.IEqualityComparer" /> объекта.
    /// </summary>
    /// <param name="d">
    ///   <see cref="T:System.Collections.IDictionary" /> Скопировать в новый объект <see cref="T:System.Collections.Hashtable" /> объект.
    /// </param>
    /// <param name="equalityComparer">
    ///   <see cref="T:System.Collections.IEqualityComparer" /> Объект, определяющий поставщик хэш-кода и компаратор для использования с <see cref="T:System.Collections.Hashtable" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования поставщика хэш-кода по умолчанию и функцию сравнения по умолчанию.
    ///    Хеш-кода является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" /> и блок сравнения по умолчанию является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    public Hashtable(IDictionary d, IEqualityComparer equalityComparer)
      : this(d, 1f, equalityComparer)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Hashtable" /> класса путем копирования элементов из указанного словаря в новый <see cref="T:System.Collections.Hashtable" /> объекта.
    ///    Новый <see cref="T:System.Collections.Hashtable" /> объект обладает начальной емкостью, равной количеству скопированных элементов и использует заданному, поставщика хэш-кода и функцию сравнения.
    /// </summary>
    /// <param name="d">
    ///   <see cref="T:System.Collections.IDictionary" /> Объект для копирования в новую <see cref="T:System.Collections.Hashtable" /> объекта.
    /// </param>
    /// <param name="loadFactor">
    ///   Число в диапазоне от 0,1 до 1,0, умноженное на значение по умолчанию, обеспечивающее наилучшую производительность.
    ///    Результат определяет максимальное отношение количества элементов к количеству сегментов.
    /// </param>
    /// <param name="hcp">
    ///   <see cref="T:System.Collections.IHashCodeProvider" /> Объект, предоставляющий хэш-коды для всех ключей в <see cref="T:System.Collections.Hashtable" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />для использования поставщика хэш-кода по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" />.
    /// </param>
    /// <param name="comparer">
    ///   <see cref="T:System.Collections.IComparer" /> Объект, который используется для определения равенства двух ключей.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />Чтобы использовать функцию сравнения по умолчанию, который является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="loadFactor" />меньше, чем 0,1.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="loadFactor" />больше, чем 1.0.
    /// </exception>
    [Obsolete("Please use Hashtable(IDictionary, float, IEqualityComparer) instead.")]
    public Hashtable(IDictionary d, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
      : this(d != null ? d.Count : 0, loadFactor, hcp, comparer)
    {
      if (d == null)
        throw new ArgumentNullException(nameof (d), Environment.GetResourceString("ArgumentNull_Dictionary"));
      IDictionaryEnumerator enumerator = d.GetEnumerator();
      while (enumerator.MoveNext())
        this.Add(enumerator.Key, enumerator.Value);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Hashtable" /> класса путем копирования элементов из указанного словаря в новый <see cref="T:System.Collections.Hashtable" /> объекта.
    ///    Новый <see cref="T:System.Collections.Hashtable" /> объект обладает начальной емкостью, равной количеству скопированных элементов и использует указанным показателем загрузки и <see cref="T:System.Collections.IEqualityComparer" /> объекта.
    /// </summary>
    /// <param name="d">
    ///   <see cref="T:System.Collections.IDictionary" /> Скопировать в новый объект <see cref="T:System.Collections.Hashtable" /> объект.
    /// </param>
    /// <param name="loadFactor">
    ///   Число в диапазоне от 0,1 до 1,0, умноженное на значение по умолчанию, обеспечивающее наилучшую производительность.
    ///    Результат определяет максимальное отношение количества элементов к количеству сегментов.
    /// </param>
    /// <param name="equalityComparer">
    ///   <see cref="T:System.Collections.IEqualityComparer" /> Объект, определяющий поставщик хэш-кода и компаратор для использования с <see cref="T:System.Collections.Hashtable" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования поставщика хэш-кода по умолчанию и функцию сравнения по умолчанию.
    ///    Хеш-кода является реализацией каждого ключа <see cref="M:System.Object.GetHashCode" /> и блок сравнения по умолчанию является реализацией каждого ключа <see cref="M:System.Object.Equals(System.Object)" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="loadFactor" /> меньше 0,1.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="loadFactor" /> больше, чем 1.0.
    /// </exception>
    public Hashtable(IDictionary d, float loadFactor, IEqualityComparer equalityComparer)
      : this(d != null ? d.Count : 0, loadFactor, equalityComparer)
    {
      if (d == null)
        throw new ArgumentNullException(nameof (d), Environment.GetResourceString("ArgumentNull_Dictionary"));
      IDictionaryEnumerator enumerator = d.GetEnumerator();
      while (enumerator.MoveNext())
        this.Add(enumerator.Key, enumerator.Value);
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Hashtable" /> с использованием заданного сериализуемого класса <see cref="T:System.Runtime.Serialization.SerializationInfo" /> и <see cref="T:System.Runtime.Serialization.StreamingContext" /> объектов.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект, содержащий сведения, необходимые для сериализации <see cref="T:System.Collections.Hashtable" /> объекта.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий исходный объект и объект назначения для сериализованного потока, связанного с коллекцией <see cref="T:System.Collections.Hashtable" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    protected Hashtable(SerializationInfo info, StreamingContext context)
    {
      HashHelpers.SerializationInfoTable.Add((object) this, info);
    }

    private uint InitHash(object key, int hashsize, out uint seed, out uint incr)
    {
      uint num = (uint) (this.GetHash(key) & int.MaxValue);
      seed = num;
      incr = 1U + seed * 101U % (uint) (hashsize - 1);
      return num;
    }

    /// <summary>
    ///   Добавляет элемент с указанными ключом и значением в словарь <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <param name="key">Ключ добавляемого элемента.</param>
    /// <param name="value">
    ///   Добавляемое значение элемента.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент с таким ключом уже существует в <see cref="T:System.Collections.Hashtable" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Hashtable" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.Hashtable" /> Имеет фиксированный размер.
    /// </exception>
    public virtual void Add(object key, object value)
    {
      this.Insert(key, value, true);
    }

    /// <summary>
    ///   Удаляет все элементы из коллекции <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Hashtable" /> доступен только для чтения.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public virtual void Clear()
    {
      if (this.count == 0 && this.occupancy == 0)
        return;
      Thread.BeginCriticalRegion();
      this.isWriterInProgress = true;
      for (int index = 0; index < this.buckets.Length; ++index)
      {
        this.buckets[index].hash_coll = 0;
        this.buckets[index].key = (object) null;
        this.buckets[index].val = (object) null;
      }
      this.count = 0;
      this.occupancy = 0;
      this.UpdateVersion();
      this.isWriterInProgress = false;
      Thread.EndCriticalRegion();
    }

    /// <summary>
    ///   Создает неполную копию объекта <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <returns>
    ///   Неполная копия <see cref="T:System.Collections.Hashtable" />.
    /// </returns>
    public virtual object Clone()
    {
      Hashtable.bucket[] buckets = this.buckets;
      Hashtable hashtable = new Hashtable(this.count, this._keycomparer);
      hashtable.version = this.version;
      hashtable.loadFactor = this.loadFactor;
      hashtable.count = 0;
      int length = buckets.Length;
      while (length > 0)
      {
        --length;
        object key = buckets[length].key;
        if (key != null && key != buckets)
          hashtable[key] = buckets[length].val;
      }
      return (object) hashtable;
    }

    /// <summary>
    ///   Определяет, содержит ли объект <see cref="T:System.Collections.Hashtable" /> указанный ключ.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который требуется найти в <see cref="T:System.Collections.Hashtable" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <see cref="T:System.Collections.Hashtable" /> содержит элемент с указанным ключом, в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual bool Contains(object key)
    {
      return this.ContainsKey(key);
    }

    /// <summary>
    ///   Определяет, содержит ли объект <see cref="T:System.Collections.Hashtable" /> указанный ключ.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который требуется найти в <see cref="T:System.Collections.Hashtable" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <see cref="T:System.Collections.Hashtable" /> содержит элемент с указанным ключом, в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual bool ContainsKey(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
      Hashtable.bucket[] buckets = this.buckets;
      uint seed;
      uint incr;
      uint num1 = this.InitHash(key, buckets.Length, out seed, out incr);
      int num2 = 0;
      int index = (int) (seed % (uint) buckets.Length);
      Hashtable.bucket bucket;
      do
      {
        bucket = buckets[index];
        if (bucket.key == null)
          return false;
        if ((long) (bucket.hash_coll & int.MaxValue) == (long) num1 && this.KeyEquals(bucket.key, key))
          return true;
        index = (int) (((long) index + (long) incr) % (long) (uint) buckets.Length);
      }
      while (bucket.hash_coll < 0 && ++num2 < buckets.Length);
      return false;
    }

    /// <summary>
    ///   Определяет, содержит ли коллекция <see cref="T:System.Collections.Hashtable" /> указанное значение.
    /// </summary>
    /// <param name="value">
    ///   Значение, которое требуется найти в словаре <see cref="T:System.Collections.Hashtable" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Collections.Hashtable" /> содержит элемент с указанным значением параметра <paramref name="value" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool ContainsValue(object value)
    {
      if (value == null)
      {
        int length = this.buckets.Length;
        while (--length >= 0)
        {
          if (this.buckets[length].key != null && this.buckets[length].key != this.buckets && this.buckets[length].val == null)
            return true;
        }
      }
      else
      {
        int length = this.buckets.Length;
        while (--length >= 0)
        {
          object val = this.buckets[length].val;
          if (val != null && val.Equals(value))
            return true;
        }
      }
      return false;
    }

    private void CopyKeys(Array array, int arrayIndex)
    {
      Hashtable.bucket[] buckets = this.buckets;
      int length = buckets.Length;
      while (--length >= 0)
      {
        object key = buckets[length].key;
        if (key != null && key != this.buckets)
          array.SetValue(key, arrayIndex++);
      }
    }

    private void CopyEntries(Array array, int arrayIndex)
    {
      Hashtable.bucket[] buckets = this.buckets;
      int length = buckets.Length;
      while (--length >= 0)
      {
        object key = buckets[length].key;
        if (key != null && key != this.buckets)
        {
          DictionaryEntry dictionaryEntry = new DictionaryEntry(key, buckets[length].val);
          array.SetValue((object) dictionaryEntry, arrayIndex++);
        }
      }
    }

    /// <summary>
    ///   Копирует <see cref="T:System.Collections.Hashtable" /> элементы в одномерном массиве <see cref="T:System.Array" /> экземпляр по указанному индексу.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" /> который является конечным <see cref="T:System.Collections.DictionaryEntry" /> объекты из <see cref="T:System.Collections.Hashtable" />.
    ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
    /// </param>
    /// <param name="arrayIndex">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="arrayIndex" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="array" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.Hashtable" /> больше, чем свободное пространство от <paramref name="arrayIndex" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Тип источника <see cref="T:System.Collections.Hashtable" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    public virtual void CopyTo(Array array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array), Environment.GetResourceString("ArgumentNull_Array"));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (arrayIndex), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - arrayIndex < this.Count)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
      this.CopyEntries(array, arrayIndex);
    }

    internal virtual KeyValuePairs[] ToKeyValuePairsArray()
    {
      KeyValuePairs[] keyValuePairsArray = new KeyValuePairs[this.count];
      int num = 0;
      Hashtable.bucket[] buckets = this.buckets;
      int length = buckets.Length;
      while (--length >= 0)
      {
        object key = buckets[length].key;
        if (key != null && key != this.buckets)
          keyValuePairsArray[num++] = new KeyValuePairs(key, buckets[length].val);
      }
      return keyValuePairsArray;
    }

    private void CopyValues(Array array, int arrayIndex)
    {
      Hashtable.bucket[] buckets = this.buckets;
      int length = buckets.Length;
      while (--length >= 0)
      {
        object key = buckets[length].key;
        if (key != null && key != this.buckets)
          array.SetValue(buckets[length].val, arrayIndex++);
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, связанное с указанным ключом.
    /// </summary>
    /// <param name="key">
    ///   Задаваемое или получаемое значение ключа.
    /// </param>
    /// <returns>
    ///   Значение, связанное с указанным ключом.
    ///    Если указанный ключ не найден, при попытке его получения возвращается <see langword="null" />, и попытке задания создается новый элемент, используя указанный ключ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Свойство задано, и список <see cref="T:System.Collections.Hashtable" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Задано свойство <paramref name="key" /> не существует в коллекции, а <see cref="T:System.Collections.Hashtable" /> имеет фиксированный размер.
    /// </exception>
    public virtual object this[object key]
    {
      get
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
        Hashtable.bucket[] buckets = this.buckets;
        uint seed;
        uint incr;
        uint num1 = this.InitHash(key, buckets.Length, out seed, out incr);
        int num2 = 0;
        int index = (int) (seed % (uint) buckets.Length);
        Hashtable.bucket bucket;
        do
        {
          int num3 = 0;
          int version;
          do
          {
            version = this.version;
            bucket = buckets[index];
            if (++num3 % 8 == 0)
              Thread.Sleep(1);
          }
          while (this.isWriterInProgress || version != this.version);
          if (bucket.key == null)
            return (object) null;
          if ((long) (bucket.hash_coll & int.MaxValue) == (long) num1 && this.KeyEquals(bucket.key, key))
            return bucket.val;
          index = (int) (((long) index + (long) incr) % (long) (uint) buckets.Length);
        }
        while (bucket.hash_coll < 0 && ++num2 < buckets.Length);
        return (object) null;
      }
      set
      {
        this.Insert(key, value, false);
      }
    }

    private void expand()
    {
      this.rehash(HashHelpers.ExpandPrime(this.buckets.Length), false);
    }

    private void rehash()
    {
      this.rehash(this.buckets.Length, false);
    }

    private void UpdateVersion()
    {
      ++this.version;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private void rehash(int newsize, bool forceNewHashCode)
    {
      this.occupancy = 0;
      Hashtable.bucket[] newBuckets = new Hashtable.bucket[newsize];
      for (int index = 0; index < this.buckets.Length; ++index)
      {
        Hashtable.bucket bucket = this.buckets[index];
        if (bucket.key != null && bucket.key != this.buckets)
        {
          int hashcode = (forceNewHashCode ? this.GetHash(bucket.key) : bucket.hash_coll) & int.MaxValue;
          this.putEntry(newBuckets, bucket.key, bucket.val, hashcode);
        }
      }
      Thread.BeginCriticalRegion();
      this.isWriterInProgress = true;
      this.buckets = newBuckets;
      this.loadsize = (int) ((double) this.loadFactor * (double) newsize);
      this.UpdateVersion();
      this.isWriterInProgress = false;
      Thread.EndCriticalRegion();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new Hashtable.HashtableEnumerator(this, 3);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionaryEnumerator" /> проходящий по <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <returns>
    ///   Интерфейс <see cref="T:System.Collections.IDictionaryEnumerator" /> для <see cref="T:System.Collections.Hashtable" />.
    /// </returns>
    public virtual IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) new Hashtable.HashtableEnumerator(this, 3);
    }

    /// <summary>Возвращает хэш-код указанного ключа.</summary>
    /// <param name="key">
    ///   <see cref="T:System.Object" /> Для которого должен быть возвращен хэш-код.
    /// </param>
    /// <returns>
    ///   Хэш-код для <paramref name="key" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    protected virtual int GetHash(object key)
    {
      if (this._keycomparer != null)
        return this._keycomparer.GetHashCode(key);
      return key.GetHashCode();
    }

    /// <summary>
    ///   Получает значение, указывающее, является ли объект <see cref="T:System.Collections.Hashtable" /> доступным только для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если коллекция <see cref="T:System.Collections.Hashtable" /> доступна только для чтения; в противном случае — значение <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, имеет ли список <see cref="T:System.Collections.Hashtable" /> фиксированный размер.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если словарь <see cref="T:System.Collections.Hashtable" /> имеет фиксированный размер; в противном случае — значение <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public virtual bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли доступ к коллекции <see cref="T:System.Collections.Hashtable" /> синхронизированным (потокобезопасным).
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если доступ к классу <see cref="T:System.Collections.Hashtable" /> является синхронизированным (потокобезопасным); в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Сравнивает указанный <see cref="T:System.Object" /> с указанным ключом в <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <param name="item">
    ///   <see cref="T:System.Object" /> Для сравнения с <paramref name="key" />.
    /// </param>
    /// <param name="key">
    ///   Ключ в <see cref="T:System.Collections.Hashtable" /> для сравнения с <paramref name="item" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="item" /> и <paramref name="key" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="item" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    protected virtual bool KeyEquals(object item, object key)
    {
      if (this.buckets == item)
        return false;
      if (item == key)
        return true;
      if (this._keycomparer != null)
        return this._keycomparer.Equals(item, key);
      if (item != null)
        return item.Equals(key);
      return false;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.ICollection" /> с ключами в <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.ICollection" /> С ключами в <see cref="T:System.Collections.Hashtable" />.
    /// </returns>
    public virtual ICollection Keys
    {
      get
      {
        if (this.keys == null)
          this.keys = (ICollection) new Hashtable.KeyCollection(this);
        return this.keys;
      }
    }

    /// <summary>
    ///   Возвращает интерфейс <see cref="T:System.Collections.ICollection" />, содержащий значения из <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <returns>
    ///   Коллекция <see cref="T:System.Collections.ICollection" />, содержащая значения из словаря <see cref="T:System.Collections.Hashtable" />.
    /// </returns>
    public virtual ICollection Values
    {
      get
      {
        if (this.values == null)
          this.values = (ICollection) new Hashtable.ValueCollection(this);
        return this.values;
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private void Insert(object key, object nvalue, bool add)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
      if (this.count >= this.loadsize)
        this.expand();
      else if (this.occupancy > this.loadsize && this.count > 100)
        this.rehash();
      uint seed;
      uint incr;
      uint num1 = this.InitHash(key, this.buckets.Length, out seed, out incr);
      int num2 = 0;
      int index1 = -1;
      int index2 = (int) (seed % (uint) this.buckets.Length);
      do
      {
        if (index1 == -1 && this.buckets[index2].key == this.buckets && this.buckets[index2].hash_coll < 0)
          index1 = index2;
        if (this.buckets[index2].key == null || this.buckets[index2].key == this.buckets && ((long) this.buckets[index2].hash_coll & 2147483648L) == 0L)
        {
          if (index1 != -1)
            index2 = index1;
          Thread.BeginCriticalRegion();
          this.isWriterInProgress = true;
          this.buckets[index2].val = nvalue;
          this.buckets[index2].key = key;
          this.buckets[index2].hash_coll |= (int) num1;
          ++this.count;
          this.UpdateVersion();
          this.isWriterInProgress = false;
          Thread.EndCriticalRegion();
          if (num2 <= 100 || !HashHelpers.IsWellKnownEqualityComparer((object) this._keycomparer) || this._keycomparer != null && this._keycomparer is RandomizedObjectEqualityComparer)
            return;
          this._keycomparer = HashHelpers.GetRandomizedEqualityComparer((object) this._keycomparer);
          this.rehash(this.buckets.Length, true);
          return;
        }
        if ((long) (this.buckets[index2].hash_coll & int.MaxValue) == (long) num1 && this.KeyEquals(this.buckets[index2].key, key))
        {
          if (add)
            throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", this.buckets[index2].key, key));
          Thread.BeginCriticalRegion();
          this.isWriterInProgress = true;
          this.buckets[index2].val = nvalue;
          this.UpdateVersion();
          this.isWriterInProgress = false;
          Thread.EndCriticalRegion();
          if (num2 <= 100 || !HashHelpers.IsWellKnownEqualityComparer((object) this._keycomparer) || this._keycomparer != null && this._keycomparer is RandomizedObjectEqualityComparer)
            return;
          this._keycomparer = HashHelpers.GetRandomizedEqualityComparer((object) this._keycomparer);
          this.rehash(this.buckets.Length, true);
          return;
        }
        if (index1 == -1 && this.buckets[index2].hash_coll >= 0)
        {
          this.buckets[index2].hash_coll |= int.MinValue;
          ++this.occupancy;
        }
        index2 = (int) (((long) index2 + (long) incr) % (long) (uint) this.buckets.Length);
      }
      while (++num2 < this.buckets.Length);
      if (index1 == -1)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HashInsertFailed"));
      Thread.BeginCriticalRegion();
      this.isWriterInProgress = true;
      this.buckets[index1].val = nvalue;
      this.buckets[index1].key = key;
      this.buckets[index1].hash_coll |= (int) num1;
      ++this.count;
      this.UpdateVersion();
      this.isWriterInProgress = false;
      Thread.EndCriticalRegion();
      if (this.buckets.Length <= 100 || !HashHelpers.IsWellKnownEqualityComparer((object) this._keycomparer) || this._keycomparer != null && this._keycomparer is RandomizedObjectEqualityComparer)
        return;
      this._keycomparer = HashHelpers.GetRandomizedEqualityComparer((object) this._keycomparer);
      this.rehash(this.buckets.Length, true);
    }

    private void putEntry(Hashtable.bucket[] newBuckets, object key, object nvalue, int hashcode)
    {
      uint num1 = (uint) hashcode;
      uint num2 = 1U + num1 * 101U % (uint) (newBuckets.Length - 1);
      int index;
      for (index = (int) (num1 % (uint) newBuckets.Length); newBuckets[index].key != null && newBuckets[index].key != this.buckets; index = (int) (((long) index + (long) num2) % (long) (uint) newBuckets.Length))
      {
        if (newBuckets[index].hash_coll >= 0)
        {
          newBuckets[index].hash_coll |= int.MinValue;
          ++this.occupancy;
        }
      }
      newBuckets[index].val = nvalue;
      newBuckets[index].key = key;
      newBuckets[index].hash_coll |= hashcode;
    }

    /// <summary>
    ///   Удаляет элемент с указанным ключом из <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется удалить.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Hashtable" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.Hashtable" /> имеет фиксированный размер.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public virtual void Remove(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
      uint seed;
      uint incr;
      uint num1 = this.InitHash(key, this.buckets.Length, out seed, out incr);
      int num2 = 0;
      int index = (int) (seed % (uint) this.buckets.Length);
      Hashtable.bucket bucket;
      do
      {
        bucket = this.buckets[index];
        if ((long) (bucket.hash_coll & int.MaxValue) == (long) num1 && this.KeyEquals(bucket.key, key))
        {
          Thread.BeginCriticalRegion();
          this.isWriterInProgress = true;
          this.buckets[index].hash_coll &= int.MinValue;
          this.buckets[index].key = this.buckets[index].hash_coll == 0 ? (object) null : (object) this.buckets;
          this.buckets[index].val = (object) null;
          --this.count;
          this.UpdateVersion();
          this.isWriterInProgress = false;
          Thread.EndCriticalRegion();
          break;
        }
        index = (int) (((long) index + (long) incr) % (long) (uint) this.buckets.Length);
      }
      while (bucket.hash_coll < 0 && ++num2 < this.buckets.Length);
    }

    /// <summary>
    ///   Получает объект, с помощью которого можно синхронизировать доступ к коллекции <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <returns>
    ///   Объект, который может использоваться для синхронизации доступа к <see cref="T:System.Collections.Hashtable" />.
    /// </returns>
    public virtual object SyncRoot
    {
      get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    /// <summary>
    ///   Возвращает число пар "ключ-значение", содержащихся в словаре <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <returns>
    ///   Число пар "ключ-значение", содержащихся в словаре <see cref="T:System.Collections.Hashtable" />.
    /// </returns>
    public virtual int Count
    {
      get
      {
        return this.count;
      }
    }

    /// <summary>
    ///   Возвращает синхронизированной (потокобезопасной) обертки для <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <param name="table">
    ///   Коллекция <see cref="T:System.Collections.Hashtable" />, которую требуется синхронизировать.
    /// </param>
    /// <returns>
    ///   Синхронизированная (потокобезопасная) оболочка для <see cref="T:System.Collections.Hashtable" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="table" /> имеет значение <see langword="null" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static Hashtable Synchronized(Hashtable table)
    {
      if (table == null)
        throw new ArgumentNullException(nameof (table));
      return (Hashtable) new Hashtable.SyncHashtable(table);
    }

    /// <summary>
    ///   Реализует <see cref="T:System.Runtime.Serialization.ISerializable" /> интерфейс и возвращает данные, необходимые для сериализации <see cref="T:System.Collections.Hashtable" />.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, который содержит сведения, требуемые для сериализации коллекции <see cref="T:System.Collections.Hashtable" />.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий исходный объект и объект назначения для сериализованного потока, связанного с коллекцией <see cref="T:System.Collections.Hashtable" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Коллекция была изменена.
    /// </exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      lock (this.SyncRoot)
      {
        int version = this.version;
        info.AddValue("LoadFactor", this.loadFactor);
        info.AddValue("Version", this.version);
        IEqualityComparer forSerialization = (IEqualityComparer) HashHelpers.GetEqualityComparerForSerialization((object) this._keycomparer);
        if (forSerialization == null)
        {
          info.AddValue("Comparer", (object) null, typeof (IComparer));
          info.AddValue("HashCodeProvider", (object) null, typeof (IHashCodeProvider));
        }
        else if (forSerialization is CompatibleComparer)
        {
          CompatibleComparer compatibleComparer = forSerialization as CompatibleComparer;
          info.AddValue("Comparer", (object) compatibleComparer.Comparer, typeof (IComparer));
          info.AddValue("HashCodeProvider", (object) compatibleComparer.HashCodeProvider, typeof (IHashCodeProvider));
        }
        else
          info.AddValue("KeyComparer", (object) forSerialization, typeof (IEqualityComparer));
        info.AddValue("HashSize", this.buckets.Length);
        object[] objArray1 = new object[this.count];
        object[] objArray2 = new object[this.count];
        this.CopyKeys((Array) objArray1, 0);
        this.CopyValues((Array) objArray2, 0);
        info.AddValue("Keys", (object) objArray1, typeof (object[]));
        info.AddValue("Values", (object) objArray2, typeof (object[]));
        if (this.version != version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
      }
    }

    /// <summary>
    ///   Реализует интерфейс <see cref="T:System.Runtime.Serialization.ISerializable" /> и вызывает событие десериализации при завершении десериализации.
    /// </summary>
    /// <param name="sender">Источник события десериализации.</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> Объект, связанный с текущим <see cref="T:System.Collections.Hashtable" /> является недопустимым.
    /// </exception>
    public virtual void OnDeserialization(object sender)
    {
      if (this.buckets != null)
        return;
      SerializationInfo serializationInfo;
      HashHelpers.SerializationInfoTable.TryGetValue((object) this, out serializationInfo);
      if (serializationInfo == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidOnDeser"));
      int length = 0;
      IComparer comparer = (IComparer) null;
      IHashCodeProvider hashCodeProvider = (IHashCodeProvider) null;
      object[] objArray1 = (object[]) null;
      object[] objArray2 = (object[]) null;
      SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
      while (enumerator.MoveNext())
      {
        switch (enumerator.Name)
        {
          case "Comparer":
            comparer = (IComparer) serializationInfo.GetValue("Comparer", typeof (IComparer));
            continue;
          case "HashCodeProvider":
            hashCodeProvider = (IHashCodeProvider) serializationInfo.GetValue("HashCodeProvider", typeof (IHashCodeProvider));
            continue;
          case "HashSize":
            length = serializationInfo.GetInt32("HashSize");
            continue;
          case "KeyComparer":
            this._keycomparer = (IEqualityComparer) serializationInfo.GetValue("KeyComparer", typeof (IEqualityComparer));
            continue;
          case "Keys":
            objArray1 = (object[]) serializationInfo.GetValue("Keys", typeof (object[]));
            continue;
          case "LoadFactor":
            this.loadFactor = serializationInfo.GetSingle("LoadFactor");
            continue;
          case "Values":
            objArray2 = (object[]) serializationInfo.GetValue("Values", typeof (object[]));
            continue;
          default:
            continue;
        }
      }
      this.loadsize = (int) ((double) this.loadFactor * (double) length);
      if (this._keycomparer == null && (comparer != null || hashCodeProvider != null))
        this._keycomparer = (IEqualityComparer) new CompatibleComparer(comparer, hashCodeProvider);
      this.buckets = new Hashtable.bucket[length];
      if (objArray1 == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_MissingKeys"));
      if (objArray2 == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_MissingValues"));
      if (objArray1.Length != objArray2.Length)
        throw new SerializationException(Environment.GetResourceString("Serialization_KeyValueDifferentSizes"));
      for (int index = 0; index < objArray1.Length; ++index)
      {
        if (objArray1[index] == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_NullKey"));
        this.Insert(objArray1[index], objArray2[index], true);
      }
      this.version = serializationInfo.GetInt32("Version");
      HashHelpers.SerializationInfoTable.Remove((object) this);
    }

    private struct bucket
    {
      public object key;
      public object val;
      public int hash_coll;
    }

    [Serializable]
    private class KeyCollection : ICollection, IEnumerable
    {
      private Hashtable _hashtable;

      internal KeyCollection(Hashtable hashtable)
      {
        this._hashtable = hashtable;
      }

      public virtual void CopyTo(Array array, int arrayIndex)
      {
        if (array == null)
          throw new ArgumentNullException(nameof (array));
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (arrayIndex < 0)
          throw new ArgumentOutOfRangeException(nameof (arrayIndex), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - arrayIndex < this._hashtable.count)
          throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
        this._hashtable.CopyKeys(array, arrayIndex);
      }

      public virtual IEnumerator GetEnumerator()
      {
        return (IEnumerator) new Hashtable.HashtableEnumerator(this._hashtable, 1);
      }

      public virtual bool IsSynchronized
      {
        get
        {
          return this._hashtable.IsSynchronized;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this._hashtable.SyncRoot;
        }
      }

      public virtual int Count
      {
        get
        {
          return this._hashtable.count;
        }
      }
    }

    [Serializable]
    private class ValueCollection : ICollection, IEnumerable
    {
      private Hashtable _hashtable;

      internal ValueCollection(Hashtable hashtable)
      {
        this._hashtable = hashtable;
      }

      public virtual void CopyTo(Array array, int arrayIndex)
      {
        if (array == null)
          throw new ArgumentNullException(nameof (array));
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (arrayIndex < 0)
          throw new ArgumentOutOfRangeException(nameof (arrayIndex), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - arrayIndex < this._hashtable.count)
          throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
        this._hashtable.CopyValues(array, arrayIndex);
      }

      public virtual IEnumerator GetEnumerator()
      {
        return (IEnumerator) new Hashtable.HashtableEnumerator(this._hashtable, 2);
      }

      public virtual bool IsSynchronized
      {
        get
        {
          return this._hashtable.IsSynchronized;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this._hashtable.SyncRoot;
        }
      }

      public virtual int Count
      {
        get
        {
          return this._hashtable.count;
        }
      }
    }

    [Serializable]
    private class SyncHashtable : Hashtable, IEnumerable
    {
      protected Hashtable _table;

      internal SyncHashtable(Hashtable table)
        : base(false)
      {
        this._table = table;
      }

      internal SyncHashtable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._table = (Hashtable) info.GetValue("ParentTable", typeof (Hashtable));
        if (this._table == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      }

      [SecurityCritical]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        lock (this._table.SyncRoot)
          info.AddValue("ParentTable", (object) this._table, typeof (Hashtable));
      }

      public override int Count
      {
        get
        {
          return this._table.Count;
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return this._table.IsReadOnly;
        }
      }

      public override bool IsFixedSize
      {
        get
        {
          return this._table.IsFixedSize;
        }
      }

      public override bool IsSynchronized
      {
        get
        {
          return true;
        }
      }

      public override object this[object key]
      {
        get
        {
          return this._table[key];
        }
        set
        {
          lock (this._table.SyncRoot)
            this._table[key] = value;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._table.SyncRoot;
        }
      }

      public override void Add(object key, object value)
      {
        lock (this._table.SyncRoot)
          this._table.Add(key, value);
      }

      public override void Clear()
      {
        lock (this._table.SyncRoot)
          this._table.Clear();
      }

      public override bool Contains(object key)
      {
        return this._table.Contains(key);
      }

      public override bool ContainsKey(object key)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
        return this._table.ContainsKey(key);
      }

      public override bool ContainsValue(object key)
      {
        lock (this._table.SyncRoot)
          return this._table.ContainsValue(key);
      }

      public override void CopyTo(Array array, int arrayIndex)
      {
        lock (this._table.SyncRoot)
          this._table.CopyTo(array, arrayIndex);
      }

      public override object Clone()
      {
        lock (this._table.SyncRoot)
          return (object) Hashtable.Synchronized((Hashtable) this._table.Clone());
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) this._table.GetEnumerator();
      }

      public override IDictionaryEnumerator GetEnumerator()
      {
        return this._table.GetEnumerator();
      }

      public override ICollection Keys
      {
        get
        {
          lock (this._table.SyncRoot)
            return this._table.Keys;
        }
      }

      public override ICollection Values
      {
        get
        {
          lock (this._table.SyncRoot)
            return this._table.Values;
        }
      }

      public override void Remove(object key)
      {
        lock (this._table.SyncRoot)
          this._table.Remove(key);
      }

      public override void OnDeserialization(object sender)
      {
      }

      internal override KeyValuePairs[] ToKeyValuePairsArray()
      {
        return this._table.ToKeyValuePairsArray();
      }
    }

    [Serializable]
    private class HashtableEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
    {
      private Hashtable hashtable;
      private int bucket;
      private int version;
      private bool current;
      private int getObjectRetType;
      private object currentKey;
      private object currentValue;
      internal const int Keys = 1;
      internal const int Values = 2;
      internal const int DictEntry = 3;

      internal HashtableEnumerator(Hashtable hashtable, int getObjRetType)
      {
        this.hashtable = hashtable;
        this.bucket = hashtable.buckets.Length;
        this.version = hashtable.version;
        this.current = false;
        this.getObjectRetType = getObjRetType;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual object Key
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          return this.currentKey;
        }
      }

      public virtual bool MoveNext()
      {
        if (this.version != this.hashtable.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        while (this.bucket > 0)
        {
          --this.bucket;
          object key = this.hashtable.buckets[this.bucket].key;
          if (key != null && key != this.hashtable.buckets)
          {
            this.currentKey = key;
            this.currentValue = this.hashtable.buckets[this.bucket].val;
            this.current = true;
            return true;
          }
        }
        this.current = false;
        return false;
      }

      public virtual DictionaryEntry Entry
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return new DictionaryEntry(this.currentKey, this.currentValue);
        }
      }

      public virtual object Current
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          if (this.getObjectRetType == 1)
            return this.currentKey;
          if (this.getObjectRetType == 2)
            return this.currentValue;
          return (object) new DictionaryEntry(this.currentKey, this.currentValue);
        }
      }

      public virtual object Value
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return this.currentValue;
        }
      }

      public virtual void Reset()
      {
        if (this.version != this.hashtable.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.current = false;
        this.bucket = this.hashtable.buckets.Length;
        this.currentKey = (object) null;
        this.currentValue = (object) null;
      }
    }

    internal class HashtableDebugView
    {
      private Hashtable hashtable;

      public HashtableDebugView(Hashtable hashtable)
      {
        if (hashtable == null)
          throw new ArgumentNullException(nameof (hashtable));
        this.hashtable = hashtable;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public KeyValuePairs[] Items
      {
        get
        {
          return this.hashtable.ToKeyValuePairsArray();
        }
      }
    }
  }
}
