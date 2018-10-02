// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.GenericSecurityDescriptor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет дескриптор безопасности.
  ///    Дескриптор безопасности включает владельца, основную группу, список управления доступом на уровне пользователей (DACL) и системный список управления доступом управления (SACL).
  /// </summary>
  public abstract class GenericSecurityDescriptor
  {
    internal const int HeaderLength = 20;
    internal const int OwnerFoundAt = 4;
    internal const int GroupFoundAt = 8;
    internal const int SaclFoundAt = 12;
    internal const int DaclFoundAt = 16;

    private static void MarshalInt(byte[] binaryForm, int offset, int number)
    {
      binaryForm[offset + 0] = (byte) number;
      binaryForm[offset + 1] = (byte) (number >> 8);
      binaryForm[offset + 2] = (byte) (number >> 16);
      binaryForm[offset + 3] = (byte) (number >> 24);
    }

    internal static int UnmarshalInt(byte[] binaryForm, int offset)
    {
      return (int) binaryForm[offset + 0] + ((int) binaryForm[offset + 1] << 8) + ((int) binaryForm[offset + 2] << 16) + ((int) binaryForm[offset + 3] << 24);
    }

    internal abstract GenericAcl GenericSacl { get; }

    internal abstract GenericAcl GenericDacl { get; }

    private bool IsCraftedAefaDacl
    {
      get
      {
        if (this.GenericDacl is DiscretionaryAcl)
          return (this.GenericDacl as DiscretionaryAcl).EveryOneFullAccessForNullDacl;
        return false;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, может ли дескриптор безопасности, связанный с этим объектом <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />, быть преобразован в формат языка определения дескрипторов безопасности (SDDL).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если дескриптор безопасности, связанный с этим объектом <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />, может быть преобразован в формат языка определения дескрипторов безопасности (SDDL). В противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool IsSddlConversionSupported()
    {
      return true;
    }

    /// <summary>
    ///   Получает уровень редакции объекта <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </summary>
    /// <returns>
    ///   Байтовое значение, определяющее уровень редакции объекта <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </returns>
    public static byte Revision
    {
      get
      {
        return 1;
      }
    }

    /// <summary>
    ///   Возвращает значения, которые определяют поведение <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> объекта.
    /// </summary>
    /// <returns>
    ///   Одно или несколько значений перечисления <see cref="T:System.Security.AccessControl.ControlFlags" />, объединенных с помощью логической операции ИЛИ.
    /// </returns>
    public abstract ControlFlags ControlFlags { get; }

    /// <summary>
    ///   Возвращает или задает владельца объекта, связанного с этим объектом <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </summary>
    /// <returns>
    ///   Владелец объекта, связанного с этим объектом <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </returns>
    public abstract SecurityIdentifier Owner { get; set; }

    /// <summary>
    ///   Возвращает или задает основную группу для этого объекта <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </summary>
    /// <returns>
    ///   Основная группа для этого объекта <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </returns>
    public abstract SecurityIdentifier Group { get; set; }

    /// <summary>
    ///   Возвращает длину в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    ///    Эту длину необходимо использовать перед маршалингом списка управления доступом в двоичный массив с помощью метода <see cref="M:System.Security.AccessControl.GenericSecurityDescriptor.GetBinaryForm(System.Byte[],System.Int32)" />.
    /// </summary>
    /// <returns>
    ///   Длина в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </returns>
    public int BinaryLength
    {
      get
      {
        int num = 20;
        if (this.Owner != (SecurityIdentifier) null)
          num += this.Owner.BinaryLength;
        if (this.Group != (SecurityIdentifier) null)
          num += this.Group.BinaryLength;
        if ((this.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && this.GenericSacl != null)
          num += this.GenericSacl.BinaryLength;
        if ((this.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && this.GenericDacl != null && !this.IsCraftedAefaDacl)
          num += this.GenericDacl.BinaryLength;
        return num;
      }
    }

    /// <summary>
    ///   Возвращает представление на языке определения дескриптора безопасности (SDDL) указанных разделов дескриптора безопасности, который представляет этот объект <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </summary>
    /// <param name="includeSections">
    ///   Указывает, какие следует получить разделы дескриптора безопасности (правила доступа, правила аудита, основная группа, владелец).
    /// </param>
    /// <returns>
    ///   Представление на языке SDDL указанных разделов дескриптора безопасности, связанных с этим объектом <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </returns>
    [SecuritySafeCritical]
    public string GetSddlForm(AccessControlSections includeSections)
    {
      byte[] binaryForm = new byte[this.BinaryLength];
      this.GetBinaryForm(binaryForm, 0);
      SecurityInfos si = (SecurityInfos) 0;
      if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
        si |= SecurityInfos.Owner;
      if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
        si |= SecurityInfos.Group;
      if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
        si |= SecurityInfos.SystemAcl;
      if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
        si |= SecurityInfos.DiscretionaryAcl;
      string resultSddl;
      switch (Win32.ConvertSdToSddl(binaryForm, 1, si, out resultSddl))
      {
        case 0:
          return resultSddl;
        case 87:
        case 1305:
          throw new InvalidOperationException();
        default:
          throw new InvalidOperationException();
      }
    }

    /// <summary>
    ///   Возвращает массив значений байтов, представляющих сведения, содержащиеся в этом объекте <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, в который маршалируется содержимое объекта <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.
    /// </param>
    /// <param name="offset">
    ///   Позиция, с которой начинается маршалинг.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="offset" /> является отрицательным или слишком велико, чтобы разрешить копирование всего <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> в <paramref name="array" />.
    /// </exception>
    public void GetBinaryForm(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException(nameof (binaryForm));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < this.BinaryLength)
        throw new ArgumentOutOfRangeException(nameof (binaryForm), Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      int num1 = offset;
      int binaryLength = this.BinaryLength;
      byte num2 = !(this is RawSecurityDescriptor) || (this.ControlFlags & ControlFlags.RMControlValid) == ControlFlags.None ? (byte) 0 : (this as RawSecurityDescriptor).ResourceManagerControl;
      int controlFlags = (int) this.ControlFlags;
      if (this.IsCraftedAefaDacl)
        controlFlags &= -5;
      binaryForm[offset + 0] = GenericSecurityDescriptor.Revision;
      binaryForm[offset + 1] = num2;
      binaryForm[offset + 2] = (byte) controlFlags;
      binaryForm[offset + 3] = (byte) (controlFlags >> 8);
      int offset1 = offset + 4;
      int offset2 = offset + 8;
      int offset3 = offset + 12;
      int offset4 = offset + 16;
      offset += 20;
      if (this.Owner != (SecurityIdentifier) null)
      {
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset1, offset - num1);
        this.Owner.GetBinaryForm(binaryForm, offset);
        offset += this.Owner.BinaryLength;
      }
      else
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset1, 0);
      if (this.Group != (SecurityIdentifier) null)
      {
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset2, offset - num1);
        this.Group.GetBinaryForm(binaryForm, offset);
        offset += this.Group.BinaryLength;
      }
      else
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset2, 0);
      if ((this.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && this.GenericSacl != null)
      {
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset3, offset - num1);
        this.GenericSacl.GetBinaryForm(binaryForm, offset);
        offset += this.GenericSacl.BinaryLength;
      }
      else
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset3, 0);
      if ((this.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && this.GenericDacl != null && !this.IsCraftedAefaDacl)
      {
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset4, offset - num1);
        this.GenericDacl.GetBinaryForm(binaryForm, offset);
        offset += this.GenericDacl.BinaryLength;
      }
      else
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset4, 0);
    }
  }
}
