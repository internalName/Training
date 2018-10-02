// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CompoundAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет составной элемент управления доступом (ACE).
  /// </summary>
  public sealed class CompoundAce : KnownAce
  {
    private CompoundAceType _compoundAceType;
    private const int AceTypeLength = 4;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.CompoundAce" />.
    /// </summary>
    /// <param name="flags">
    ///   Содержит флаги, определяющие параметры наследования и распространения наследования, а также условия для нового элемента управления запись ДОСТУПОМ аудита.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа элемента управления доступом.
    /// </param>
    /// <param name="compoundAceType">
    ///   Значение из перечисления <see cref="T:System.Security.AccessControl.CompoundAceType" />.
    /// </param>
    /// <param name="sid">
    ///   Объект <see cref="T:System.Security.Principal.SecurityIdentifier" />, связанный с новым элементом управления доступом.
    /// </param>
    public CompoundAce(AceFlags flags, int accessMask, CompoundAceType compoundAceType, SecurityIdentifier sid)
      : base(AceType.AccessAllowedCompound, flags, accessMask, sid)
    {
      this._compoundAceType = compoundAceType;
    }

    internal static bool ParseBinaryForm(byte[] binaryForm, int offset, out int accessMask, out CompoundAceType compoundAceType, out SecurityIdentifier sid)
    {
      GenericAce.VerifyHeader(binaryForm, offset);
      if (binaryForm.Length - offset >= 12 + SecurityIdentifier.MinBinaryLength)
      {
        int num1 = offset + 4;
        int num2 = 0;
        accessMask = (int) binaryForm[num1 + 0] + ((int) binaryForm[num1 + 1] << 8) + ((int) binaryForm[num1 + 2] << 16) + ((int) binaryForm[num1 + 3] << 24);
        int num3 = num2 + 4;
        compoundAceType = (CompoundAceType) ((int) binaryForm[num1 + num3 + 0] + ((int) binaryForm[num1 + num3 + 1] << 8));
        int num4 = num3 + 4;
        sid = new SecurityIdentifier(binaryForm, num1 + num4);
        return true;
      }
      accessMask = 0;
      compoundAceType = (CompoundAceType) 0;
      sid = (SecurityIdentifier) null;
      return false;
    }

    /// <summary>
    ///   Получает или задает тип этого объекта <see cref="T:System.Security.AccessControl.CompoundAce" />.
    /// </summary>
    /// <returns>
    ///   Тип этого объекта <see cref="T:System.Security.AccessControl.CompoundAce" />.
    /// </returns>
    public CompoundAceType CompoundAceType
    {
      get
      {
        return this._compoundAceType;
      }
      set
      {
        this._compoundAceType = value;
      }
    }

    /// <summary>
    ///   Возвращает длину в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.CompoundAce" />.
    ///    Эту длину необходимо использовать перед маршалингом списка управления доступом в двоичный массив с помощью метода <see cref="M:System.Security.AccessControl.CompoundAce.GetBinaryForm(System.Byte[],System.Int32)" />.
    /// </summary>
    /// <returns>
    ///   Длина в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.CompoundAce" />.
    /// </returns>
    public override int BinaryLength
    {
      get
      {
        return 12 + this.SecurityIdentifier.BinaryLength;
      }
    }

    /// <summary>
    ///   Маршалирует содержимое объекта <see cref="T:System.Security.AccessControl.CompoundAce" /> в указанный массив байтов, начиная с указанной позиции.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, в который маршалируется содержимое объекта <see cref="T:System.Security.AccessControl.CompoundAce" />.
    /// </param>
    /// <param name="offset">
    ///   Позиция, с которой начинается маршалинг.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="offset" /> является отрицательным или слишком велико, чтобы разрешить копирование всего <see cref="T:System.Security.AccessControl.CompoundAce" /> в <paramref name="array" />.
    /// </exception>
    public override void GetBinaryForm(byte[] binaryForm, int offset)
    {
      this.MarshalHeader(binaryForm, offset);
      int num1 = offset + 4;
      int num2 = 0;
      binaryForm[num1 + 0] = (byte) this.AccessMask;
      binaryForm[num1 + 1] = (byte) (this.AccessMask >> 8);
      binaryForm[num1 + 2] = (byte) (this.AccessMask >> 16);
      binaryForm[num1 + 3] = (byte) (this.AccessMask >> 24);
      int num3 = num2 + 4;
      binaryForm[num1 + num3 + 0] = (byte) (ushort) this.CompoundAceType;
      binaryForm[num1 + num3 + 1] = (byte) ((uint) (ushort) this.CompoundAceType >> 8);
      binaryForm[num1 + num3 + 2] = (byte) 0;
      binaryForm[num1 + num3 + 3] = (byte) 0;
      int num4 = num3 + 4;
      this.SecurityIdentifier.GetBinaryForm(binaryForm, num1 + num4);
    }
  }
}
