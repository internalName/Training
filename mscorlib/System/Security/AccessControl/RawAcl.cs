// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RawAcl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.AccessControl
{
  /// <summary>Представляет список управления доступом (ACL).</summary>
  public sealed class RawAcl : GenericAcl
  {
    private byte _revision;
    private ArrayList _aces;

    private static void VerifyHeader(byte[] binaryForm, int offset, out byte revision, out int count, out int length)
    {
      if (binaryForm == null)
        throw new ArgumentNullException(nameof (binaryForm));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset >= 8)
      {
        revision = binaryForm[offset + 0];
        length = (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8);
        count = (int) binaryForm[offset + 4] + ((int) binaryForm[offset + 5] << 8);
        if (length <= binaryForm.Length - offset)
          return;
      }
      throw new ArgumentOutOfRangeException(nameof (binaryForm), Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
    }

    private void MarshalHeader(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException(nameof (binaryForm));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this.BinaryLength > GenericAcl.MaxBinaryLength)
        throw new InvalidOperationException(Environment.GetResourceString("AccessControl_AclTooLong"));
      if (binaryForm.Length - offset < this.BinaryLength)
        throw new ArgumentOutOfRangeException(nameof (binaryForm), Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      binaryForm[offset + 0] = this.Revision;
      binaryForm[offset + 1] = (byte) 0;
      binaryForm[offset + 2] = (byte) this.BinaryLength;
      binaryForm[offset + 3] = (byte) (this.BinaryLength >> 8);
      binaryForm[offset + 4] = (byte) this.Count;
      binaryForm[offset + 5] = (byte) (this.Count >> 8);
      binaryForm[offset + 6] = (byte) 0;
      binaryForm[offset + 7] = (byte) 0;
    }

    internal void SetBinaryForm(byte[] binaryForm, int offset)
    {
      int count;
      int length;
      RawAcl.VerifyHeader(binaryForm, offset, out this._revision, out count, out length);
      int num1 = length + offset;
      offset += 8;
      this._aces = new ArrayList(count);
      int num2 = 8;
      for (int index = 0; index < count; ++index)
      {
        GenericAce fromBinaryForm = GenericAce.CreateFromBinaryForm(binaryForm, offset);
        int binaryLength = fromBinaryForm.BinaryLength;
        if (num2 + binaryLength > GenericAcl.MaxBinaryLength)
          throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAclBinaryForm"), nameof (binaryForm));
        this._aces.Add((object) fromBinaryForm);
        if (binaryLength % 4 != 0)
          throw new SystemException();
        num2 += binaryLength;
        if ((int) this._revision == (int) GenericAcl.AclRevisionDS)
          offset += (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8);
        else
          offset += binaryLength;
        if (offset > num1)
          throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAclBinaryForm"), nameof (binaryForm));
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RawAcl" /> класса указанный номер редакции.
    /// </summary>
    /// <param name="revision">
    ///   Номер редакции нового доступом список управления (ACL).
    /// </param>
    /// <param name="capacity">
    ///   Число записей управления доступом (ACE) это <see cref="T:System.Security.AccessControl.RawAcl" /> может содержать объект.
    ///    Это число будет использоваться только в качестве подсказки.
    /// </param>
    public RawAcl(byte revision, int capacity)
    {
      this._revision = revision;
      this._aces = new ArrayList(capacity);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RawAcl" /> класс из указанного двоичной форме.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтовых значений, представляющий список управления доступом (ACL).
    /// </param>
    /// <param name="offset">
    ///   Смещение в <paramref name="binaryForm" /> параметр, с которой начинается распаковка данных.
    /// </param>
    public RawAcl(byte[] binaryForm, int offset)
    {
      this.SetBinaryForm(binaryForm, offset);
    }

    /// <summary>
    ///   Возвращает уровень редакции объекта <see cref="T:System.Security.AccessControl.RawAcl" />.
    /// </summary>
    /// <returns>
    ///   Байтовое значение, определяющее уровень редакции объекта <see cref="T:System.Security.AccessControl.RawAcl" />.
    /// </returns>
    public override byte Revision
    {
      get
      {
        return this._revision;
      }
    }

    /// <summary>
    ///   Возвращает количество элементов управления доступом в текущем объекте <see cref="T:System.Security.AccessControl.RawAcl" />.
    /// </summary>
    /// <returns>
    ///   Количество элементов управления доступом в текущем объекте <see cref="T:System.Security.AccessControl.RawAcl" />.
    /// </returns>
    public override int Count
    {
      get
      {
        return this._aces.Count;
      }
    }

    /// <summary>
    ///   Возвращает длину в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.RawAcl" />.
    ///    Эту длину необходимо использовать перед маршалингом списка управления доступом в двоичный массив с помощью метода <see cref="M:System.Security.AccessControl.RawAcl.GetBinaryForm(System.Byte[],System.Int32)" />.
    /// </summary>
    /// <returns>
    ///   Длина в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.RawAcl" />.
    /// </returns>
    public override int BinaryLength
    {
      get
      {
        int num = 8;
        for (int index = 0; index < this.Count; ++index)
        {
          GenericAce ace = this._aces[index] as GenericAce;
          num += ace.BinaryLength;
        }
        return num;
      }
    }

    /// <summary>
    ///   Маршалирует содержимое объекта <see cref="T:System.Security.AccessControl.RawAcl" /> в указанный массив байтов, начиная с указанной позиции.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, в который маршалируется содержимое объекта <see cref="T:System.Security.AccessControl.RawAcl" />.
    /// </param>
    /// <param name="offset">
    ///   Позиция, с которой начинается маршалинг.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="offset" /> является отрицательным или слишком велико, чтобы обеспечить копирование всего <see cref="T:System.Security.AccessControl.RawAcl" /> в <paramref name="array" />.
    /// </exception>
    public override void GetBinaryForm(byte[] binaryForm, int offset)
    {
      this.MarshalHeader(binaryForm, offset);
      offset += 8;
      for (int index = 0; index < this.Count; ++index)
      {
        GenericAce ace = this._aces[index] as GenericAce;
        ace.GetBinaryForm(binaryForm, offset);
        int binaryLength = ace.BinaryLength;
        if (binaryLength % 4 != 0)
          throw new SystemException();
        offset += binaryLength;
      }
    }

    /// <summary>
    ///   Возвращает или задает запись управления доступом (ACE) по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс получаемого или задаваемого элемента управления доступом.
    /// </param>
    /// <returns>Элемент управления ДОСТУПОМ по указанному индексу.</returns>
    public override GenericAce this[int index]
    {
      get
      {
        return this._aces[index] as GenericAce;
      }
      set
      {
        if (value == (GenericAce) null)
          throw new ArgumentNullException(nameof (value));
        if (value.BinaryLength % 4 != 0)
          throw new SystemException();
        if (this.BinaryLength - (index < this._aces.Count ? (this._aces[index] as GenericAce).BinaryLength : 0) + value.BinaryLength > GenericAcl.MaxBinaryLength)
          throw new OverflowException(Environment.GetResourceString("AccessControl_AclTooLong"));
        this._aces[index] = (object) value;
      }
    }

    /// <summary>
    ///   Вставляет указанный элемент управления доступом (ACE) по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Позиция, в которую добавляется новый элемент управления ДОСТУПОМ.
    ///    Укажите значение <see cref="P:System.Security.AccessControl.RawAcl.Count" /> Свойства для вставки записи управления ДОСТУПОМ в конце <see cref="T:System.Security.AccessControl.RawAcl" /> объекта.
    /// </param>
    /// <param name="ace">Вставляемый элемент управления ДОСТУПОМ.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="offset" /> является отрицательным или слишком велико, чтобы обеспечить копирование всего <see cref="T:System.Security.AccessControl.GenericAcl" /> в <paramref name="array" />.
    /// </exception>
    public void InsertAce(int index, GenericAce ace)
    {
      if (ace == (GenericAce) null)
        throw new ArgumentNullException(nameof (ace));
      if (this.BinaryLength + ace.BinaryLength > GenericAcl.MaxBinaryLength)
        throw new OverflowException(Environment.GetResourceString("AccessControl_AclTooLong"));
      this._aces.Insert(index, (object) ace);
    }

    /// <summary>
    ///   Удаляет запись управления доступом (ACE) в указанном расположении.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс удаляемого элемента управления доступом.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="index" /> параметра превышает значение <see cref="P:System.Security.AccessControl.RawAcl.Count" /> Свойства минус один или является отрицательным.
    /// </exception>
    public void RemoveAce(int index)
    {
      GenericAce ace = this._aces[index] as GenericAce;
      this._aces.RemoveAt(index);
    }
  }
}
