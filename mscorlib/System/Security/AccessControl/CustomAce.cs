// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CustomAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет элемент управления входа (ДОСТУПОМ), не определенный одним из членов <see cref="T:System.Security.AccessControl.AceType" /> перечисления.
  /// </summary>
  public sealed class CustomAce : GenericAce
  {
    /// <summary>
    ///   Возвращает максимально допустимую длину большого двоичного объекта непрозрачных данных для этой <see cref="T:System.Security.AccessControl.CustomAce" /> объекта.
    /// </summary>
    public static readonly int MaxOpaqueLength = 65531;
    private byte[] _opaque;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.CustomAce" />.
    /// </summary>
    /// <param name="type">
    ///   Тип нового доступа к записи управления (ACE).
    ///    Это значение должно быть больше, чем <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" />.
    /// </param>
    /// <param name="flags">
    ///   Флаги, определяющие параметры наследования и распространения наследования, и условия для нового элемента управления ДОСТУПОМ аудита.
    /// </param>
    /// <param name="opaque">
    ///   Массив байтовых значений, содержащий данные для нового элемента управления ДОСТУПОМ.
    ///    Это значение может быть равно <see langword="null" />.
    ///    Длина этого массива не должно быть больше, чем значение <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> поля и должно быть кратно 4.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="type" /> не больше значения параметра <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" /> или длина <paramref name="opaque" /> массив является либо больше, чем значение <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> поле или не кратно 4.
    /// </exception>
    public CustomAce(AceType type, AceFlags flags, byte[] opaque)
      : base(type, flags)
    {
      if (type <= AceType.SystemAlarmCallbackObject)
        throw new ArgumentOutOfRangeException(nameof (type), Environment.GetResourceString("ArgumentOutOfRange_InvalidUserDefinedAceType"));
      this.SetOpaque(opaque);
    }

    /// <summary>
    ///   Возвращает длину непрозрачные данные, связанные с этим <see cref="T:System.Security.AccessControl.CustomAce" /> объекта.
    /// </summary>
    /// <returns>Длина непрозрачных данных обратного вызова.</returns>
    public int OpaqueLength
    {
      get
      {
        if (this._opaque == null)
          return 0;
        return this._opaque.Length;
      }
    }

    /// <summary>
    ///   Возвращает длину в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.CustomAce" />.
    ///    Эту длину необходимо использовать перед маршалингом списка управления доступом в двоичный массив с помощью метода <see cref="M:System.Security.AccessControl.CustomAce.GetBinaryForm(System.Byte[],System.Int32)" />.
    /// </summary>
    /// <returns>
    ///   Длина в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.CustomAce" />.
    /// </returns>
    public override int BinaryLength
    {
      get
      {
        return 4 + this.OpaqueLength;
      }
    }

    /// <summary>
    ///   Возвращает непрозрачные данные, связанные с этим <see cref="T:System.Security.AccessControl.CustomAce" /> объекта.
    /// </summary>
    /// <returns>
    ///   Массив байтовых значений, представляющий непрозрачные данные, связанные с этим <see cref="T:System.Security.AccessControl.CustomAce" /> объекта.
    /// </returns>
    public byte[] GetOpaque()
    {
      return this._opaque;
    }

    /// <summary>
    ///   Задает данные непрозрачный обратного вызова, связанный с этим <see cref="T:System.Security.AccessControl.CustomAce" /> объекта.
    /// </summary>
    /// <param name="opaque">
    ///   Массив байтовых значений, представляющий непрозрачных данных обратного вызова для этой <see cref="T:System.Security.AccessControl.CustomAce" /> объекта.
    /// </param>
    public void SetOpaque(byte[] opaque)
    {
      if (opaque != null)
      {
        if (opaque.Length > CustomAce.MaxOpaqueLength)
          throw new ArgumentOutOfRangeException(nameof (opaque), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLength"), (object) 0, (object) CustomAce.MaxOpaqueLength));
        if (opaque.Length % 4 != 0)
          throw new ArgumentOutOfRangeException(nameof (opaque), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple"), (object) 4));
      }
      this._opaque = opaque;
    }

    /// <summary>
    ///   Маршалирует содержимое объекта <see cref="T:System.Security.AccessControl.CustomAce" /> в указанный массив байтов, начиная с указанной позиции.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, в который маршалируется содержимое объекта <see cref="T:System.Security.AccessControl.CustomAce" />.
    /// </param>
    /// <param name="offset">
    ///   Позиция, с которой начинается маршалинг.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="offset" /> является отрицательным или слишком велико, чтобы разрешить копирование всего <see cref="T:System.Security.AccessControl.CustomAce" /> в <paramref name="array" />.
    /// </exception>
    public override void GetBinaryForm(byte[] binaryForm, int offset)
    {
      this.MarshalHeader(binaryForm, offset);
      offset += 4;
      if (this.OpaqueLength == 0)
        return;
      if (this.OpaqueLength > CustomAce.MaxOpaqueLength)
        throw new SystemException();
      this.GetOpaque().CopyTo((Array) binaryForm, offset);
    }
  }
}
