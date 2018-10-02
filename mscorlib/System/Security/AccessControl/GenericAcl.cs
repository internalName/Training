// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.GenericAcl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет список управления доступом и является базовым классом для классов <see cref="T:System.Security.AccessControl.CommonAcl" />, <see cref="T:System.Security.AccessControl.DiscretionaryAcl" />, <see cref="T:System.Security.AccessControl.RawAcl" /> и <see cref="T:System.Security.AccessControl.SystemAcl" />.
  /// </summary>
  public abstract class GenericAcl : ICollection, IEnumerable
  {
    /// <summary>
    ///   Уровень редакции текущего объекта <see cref="T:System.Security.AccessControl.GenericAcl" />.
    ///    Это значение возвращается свойством <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> для списков управления доступом (ACL), которые связаны с объектами служб каталогов.
    /// </summary>
    public static readonly byte AclRevision = 2;
    /// <summary>
    ///   Уровень редакции текущего объекта <see cref="T:System.Security.AccessControl.GenericAcl" />.
    ///    Это значение возвращается свойством <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> для списков управления доступом (ACL), которые связаны с объектами служб каталогов.
    /// </summary>
    public static readonly byte AclRevisionDS = 4;
    /// <summary>
    ///   Максимально допустимая двоичная длина объекта <see cref="T:System.Security.AccessControl.GenericAcl" />.
    /// </summary>
    public static readonly int MaxBinaryLength = (int) ushort.MaxValue;
    internal const int HeaderLength = 8;

    /// <summary>
    ///   Возвращает уровень редакции объекта <see cref="T:System.Security.AccessControl.GenericAcl" />.
    /// </summary>
    /// <returns>
    ///   Байтовое значение, определяющее уровень редакции объекта <see cref="T:System.Security.AccessControl.GenericAcl" />.
    /// </returns>
    public abstract byte Revision { get; }

    /// <summary>
    ///   Возвращает длину в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.GenericAcl" />.
    ///    Эту длину необходимо использовать перед маршалингом списка управления доступом в двоичный массив с помощью метода <see cref="M:System.Security.AccessControl.GenericAcl.GetBinaryForm(System.Byte[],System.Int32)" />.
    /// </summary>
    /// <returns>
    ///   Длина в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.GenericAcl" />.
    /// </returns>
    public abstract int BinaryLength { get; }

    /// <summary>
    ///   Возвращает или задает объект <see cref="T:System.Security.AccessControl.GenericAce" /> с заданным индексом.
    /// </summary>
    /// <param name="index">
    ///   Индекс (с нуля) возвращаемого или задаваемого <see cref="T:System.Security.AccessControl.GenericAce" />.
    /// </param>
    /// <returns>
    ///   Параметр <see cref="T:System.Security.AccessControl.GenericAce" /> по указанному индексу.
    /// </returns>
    public abstract GenericAce this[int index] { get; set; }

    /// <summary>
    ///   Маршалирует содержимое объекта <see cref="T:System.Security.AccessControl.GenericAcl" /> в указанный массив байтов, начиная с указанной позиции.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, в который маршалируется содержимое объекта <see cref="T:System.Security.AccessControl.GenericAcl" />.
    /// </param>
    /// <param name="offset">
    ///   Позиция, с которой начинается маршалинг.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="offset" /> является отрицательным или слишком велико, чтобы разрешить копирование всего <see cref="T:System.Security.AccessControl.GenericAcl" /> в <paramref name="array" />.
    /// </exception>
    public abstract void GetBinaryForm(byte[] binaryForm, int offset);

    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (array.Rank != 1)
        throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < this.Count)
        throw new ArgumentOutOfRangeException(nameof (array), Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      for (int index1 = 0; index1 < this.Count; ++index1)
        array.SetValue((object) this[index1], index + index1);
    }

    /// <summary>
    ///   Копирует каждый <see cref="T:System.Security.AccessControl.GenericAce" /> текущего объекта <see cref="T:System.Security.AccessControl.GenericAcl" /> в указанный массив.
    /// </summary>
    /// <param name="array">
    ///   Массив, в котором размещаются копии объектов <see cref="T:System.Security.AccessControl.GenericAce" />, содержащихся в текущем <see cref="T:System.Security.AccessControl.GenericAcl" />.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс <paramref name="array" />, указывающий начало копирования.
    /// </param>
    public void CopyTo(GenericAce[] array, int index)
    {
      ((ICollection) this).CopyTo((Array) array, index);
    }

    /// <summary>
    ///   Возвращает количество элементов управления доступом в текущем объекте <see cref="T:System.Security.AccessControl.GenericAcl" />.
    /// </summary>
    /// <returns>
    ///   Количество элементов управления доступом в текущем объекте <see cref="T:System.Security.AccessControl.GenericAcl" />.
    /// </returns>
    public abstract int Count { get; }

    /// <summary>
    ///   Данное свойство всегда имеет значение <see langword="false" />.
    ///    Оно реализовано только потому, что необходимо для реализации интерфейса <see cref="T:System.Collections.ICollection" />.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="false" />.
    /// </returns>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Данное свойство всегда возвращает значение <see langword="null" />.
    ///    Оно реализовано только потому, что необходимо для реализации интерфейса <see cref="T:System.Collections.ICollection" />.
    /// </summary>
    /// <returns>
    ///   Всегда возвращает значение <see langword="null" />.
    /// </returns>
    public virtual object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new AceEnumerator(this);
    }

    /// <summary>
    ///   Извлекает объект, который можно использовать для итерации по записям управления доступом (ACE) в списке управления доступом (ACL).
    /// </summary>
    /// <returns>Объект перечислителя.</returns>
    public AceEnumerator GetEnumerator()
    {
      return ((IEnumerable) this).GetEnumerator() as AceEnumerator;
    }
  }
}
