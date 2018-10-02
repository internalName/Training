// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.GenericAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет запись управления доступом (ACE) и является базовым классом для всех других классов элементов управления ДОСТУПОМ.
  /// </summary>
  public abstract class GenericAce
  {
    private readonly AceType _type;
    private AceFlags _flags;
    internal ushort _indexInAcl;
    internal const int HeaderLength = 4;

    internal void MarshalHeader(byte[] binaryForm, int offset)
    {
      int binaryLength = this.BinaryLength;
      if (binaryForm == null)
        throw new ArgumentNullException(nameof (binaryForm));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < this.BinaryLength)
        throw new ArgumentOutOfRangeException(nameof (binaryForm), Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      if (binaryLength > (int) ushort.MaxValue)
        throw new SystemException();
      binaryForm[offset + 0] = (byte) this.AceType;
      binaryForm[offset + 1] = (byte) this.AceFlags;
      binaryForm[offset + 2] = (byte) binaryLength;
      binaryForm[offset + 3] = (byte) (binaryLength >> 8);
    }

    internal GenericAce(AceType type, AceFlags flags)
    {
      this._type = type;
      this._flags = flags;
    }

    internal static AceFlags AceFlagsFromAuditFlags(AuditFlags auditFlags)
    {
      AceFlags aceFlags = AceFlags.None;
      if ((auditFlags & AuditFlags.Success) != AuditFlags.None)
        aceFlags |= AceFlags.SuccessfulAccess;
      if ((auditFlags & AuditFlags.Failure) != AuditFlags.None)
        aceFlags |= AceFlags.FailedAccess;
      if (aceFlags == AceFlags.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), nameof (auditFlags));
      return aceFlags;
    }

    internal static AceFlags AceFlagsFromInheritanceFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      AceFlags aceFlags = AceFlags.None;
      if ((inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
        aceFlags |= AceFlags.ContainerInherit;
      if ((inheritanceFlags & InheritanceFlags.ObjectInherit) != InheritanceFlags.None)
        aceFlags |= AceFlags.ObjectInherit;
      if (aceFlags != AceFlags.None)
      {
        if ((propagationFlags & PropagationFlags.NoPropagateInherit) != PropagationFlags.None)
          aceFlags |= AceFlags.NoPropagateInherit;
        if ((propagationFlags & PropagationFlags.InheritOnly) != PropagationFlags.None)
          aceFlags |= AceFlags.InheritOnly;
      }
      return aceFlags;
    }

    internal static void VerifyHeader(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException(nameof (binaryForm));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < 4)
        throw new ArgumentOutOfRangeException(nameof (binaryForm), Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      if (((int) binaryForm[offset + 3] << 8) + (int) binaryForm[offset + 2] > binaryForm.Length - offset)
        throw new ArgumentOutOfRangeException(nameof (binaryForm), Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.AccessControl.GenericAce" /> объект с указанным двоичным данным.
    /// </summary>
    /// <param name="binaryForm">
    ///   Двоичные данные, из которого будет создан новый <see cref="T:System.Security.AccessControl.GenericAce" /> объекта.
    /// </param>
    /// <param name="offset">
    ///   Позиция, с которой начинается распаковка.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.GenericAce" />, создаваемый с помощью данного метода.
    /// </returns>
    public static GenericAce CreateFromBinaryForm(byte[] binaryForm, int offset)
    {
      GenericAce.VerifyHeader(binaryForm, offset);
      AceType type = (AceType) binaryForm[offset];
      GenericAce genericAce;
      switch (type)
      {
        case AceType.AccessAllowed:
        case AceType.AccessDenied:
        case AceType.SystemAudit:
        case AceType.SystemAlarm:
        case AceType.AccessAllowedCallback:
        case AceType.AccessDeniedCallback:
        case AceType.SystemAuditCallback:
        case AceType.SystemAlarmCallback:
          AceQualifier qualifier1;
          int accessMask1;
          SecurityIdentifier sid1;
          bool isCallback1;
          byte[] opaque1;
          if (CommonAce.ParseBinaryForm(binaryForm, offset, out qualifier1, out accessMask1, out sid1, out isCallback1, out opaque1))
          {
            genericAce = (GenericAce) new CommonAce((AceFlags) binaryForm[offset + 1], qualifier1, accessMask1, sid1, isCallback1, opaque1);
            break;
          }
          goto label_15;
        case AceType.AccessAllowedCompound:
          int accessMask2;
          CompoundAceType compoundAceType;
          SecurityIdentifier sid2;
          if (CompoundAce.ParseBinaryForm(binaryForm, offset, out accessMask2, out compoundAceType, out sid2))
          {
            genericAce = (GenericAce) new CompoundAce((AceFlags) binaryForm[offset + 1], accessMask2, compoundAceType, sid2);
            break;
          }
          goto label_15;
        case AceType.AccessAllowedObject:
        case AceType.AccessDeniedObject:
        case AceType.SystemAuditObject:
        case AceType.SystemAlarmObject:
        case AceType.AccessAllowedCallbackObject:
        case AceType.AccessDeniedCallbackObject:
        case AceType.SystemAuditCallbackObject:
        case AceType.SystemAlarmCallbackObject:
          AceQualifier qualifier2;
          int accessMask3;
          SecurityIdentifier sid3;
          ObjectAceFlags objectFlags;
          Guid objectAceType;
          Guid inheritedObjectAceType;
          bool isCallback2;
          byte[] opaque2;
          if (ObjectAce.ParseBinaryForm(binaryForm, offset, out qualifier2, out accessMask3, out sid3, out objectFlags, out objectAceType, out inheritedObjectAceType, out isCallback2, out opaque2))
          {
            genericAce = (GenericAce) new ObjectAce((AceFlags) binaryForm[offset + 1], qualifier2, accessMask3, sid3, objectFlags, objectAceType, inheritedObjectAceType, isCallback2, opaque2);
            break;
          }
          goto label_15;
        default:
          AceFlags flags = (AceFlags) binaryForm[offset + 1];
          byte[] opaque3 = (byte[]) null;
          int num = (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8);
          if (num % 4 == 0)
          {
            int length = num - 4;
            if (length > 0)
            {
              opaque3 = new byte[length];
              for (int index = 0; index < length; ++index)
                opaque3[index] = binaryForm[offset + num - length + index];
            }
            genericAce = (GenericAce) new CustomAce(type, flags, opaque3);
            break;
          }
          goto label_15;
      }
      if ((genericAce is ObjectAce || (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8) == genericAce.BinaryLength) && (!(genericAce is ObjectAce) || (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8) == genericAce.BinaryLength || (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8) - 32 == genericAce.BinaryLength))
        return genericAce;
label_15:
      throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAceBinaryForm"), nameof (binaryForm));
    }

    /// <summary>
    ///   Возвращает тип из этой записи управления доступом (ACE).
    /// </summary>
    /// <returns>Тип данного элемента управления ДОСТУПОМ.</returns>
    public AceType AceType
    {
      get
      {
        return this._type;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Security.AccessControl.AceFlags" /> связанный с этим <see cref="T:System.Security.AccessControl.GenericAce" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.AccessControl.AceFlags" /> Связанный с этим <see cref="T:System.Security.AccessControl.GenericAce" /> объекта.
    /// </returns>
    public AceFlags AceFlags
    {
      get
      {
        return this._flags;
      }
      set
      {
        this._flags = value;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, которое определяет, наследуется ли задано явным образом этой записи управления доступом (ACE).
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот элемент управления ДОСТУПОМ наследуется; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsInherited
    {
      get
      {
        return (uint) (this.AceFlags & AceFlags.Inherited) > 0U;
      }
    }

    /// <summary>
    ///   Получает флаги, определяющие свойства наследования для этой записи управления доступом (ACE).
    /// </summary>
    /// <returns>
    ///   Флаги, определяющие свойства наследования данного элемента управления доступом.
    /// </returns>
    public InheritanceFlags InheritanceFlags
    {
      get
      {
        InheritanceFlags inheritanceFlags = InheritanceFlags.None;
        if ((this.AceFlags & AceFlags.ContainerInherit) != AceFlags.None)
          inheritanceFlags |= InheritanceFlags.ContainerInherit;
        if ((this.AceFlags & AceFlags.ObjectInherit) != AceFlags.None)
          inheritanceFlags |= InheritanceFlags.ObjectInherit;
        return inheritanceFlags;
      }
    }

    /// <summary>
    ///   Получает флаги, определяющие свойства распространения наследования из этой записи управления доступом (ACE).
    /// </summary>
    /// <returns>
    ///   Флаги, определяющие свойства распространения наследования данного элемента управления доступом.
    /// </returns>
    public PropagationFlags PropagationFlags
    {
      get
      {
        PropagationFlags propagationFlags = PropagationFlags.None;
        if ((this.AceFlags & AceFlags.InheritOnly) != AceFlags.None)
          propagationFlags |= PropagationFlags.InheritOnly;
        if ((this.AceFlags & AceFlags.NoPropagateInherit) != AceFlags.None)
          propagationFlags |= PropagationFlags.NoPropagateInherit;
        return propagationFlags;
      }
    }

    /// <summary>
    ///   Возвращает сведения об аудите, связанные с этой записи управления доступом (ACE).
    /// </summary>
    /// <returns>
    ///   Данные аудита, связанных с этой записи управления доступом (ACE).
    /// </returns>
    public AuditFlags AuditFlags
    {
      get
      {
        AuditFlags auditFlags = AuditFlags.None;
        if ((this.AceFlags & AceFlags.SuccessfulAccess) != AceFlags.None)
          auditFlags |= AuditFlags.Success;
        if ((this.AceFlags & AceFlags.FailedAccess) != AceFlags.None)
          auditFlags |= AuditFlags.Failure;
        return auditFlags;
      }
    }

    /// <summary>
    ///   Возвращает длину в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.GenericAce" />.
    ///    Эту длину необходимо использовать перед маршалингом списка управления доступом в двоичный массив с помощью метода <see cref="M:System.Security.AccessControl.GenericAce.GetBinaryForm(System.Byte[],System.Int32)" />.
    /// </summary>
    /// <returns>
    ///   Длина в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.GenericAce" />.
    /// </returns>
    public abstract int BinaryLength { get; }

    /// <summary>
    ///   Маршалирует содержимое объекта <see cref="T:System.Security.AccessControl.GenericAce" /> в указанный массив байтов, начиная с указанной позиции.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, в который маршалируется содержимое объекта <see cref="T:System.Security.AccessControl.GenericAce" />.
    /// </param>
    /// <param name="offset">
    ///   Позиция, с которой начинается маршалинг.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="offset" /> является отрицательным или слишком велико, чтобы разрешить копирование всего <see cref="T:System.Security.AccessControl.GenericAcl" /> в <paramref name="array" />.
    /// </exception>
    public abstract void GetBinaryForm(byte[] binaryForm, int offset);

    /// <summary>
    ///   Создает глубокую копию из этой записи управления доступом (ACE).
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.AccessControl.GenericAce" /> Объект, который создает этот метод.
    /// </returns>
    public GenericAce Copy()
    {
      byte[] binaryForm = new byte[this.BinaryLength];
      this.GetBinaryForm(binaryForm, 0);
      return GenericAce.CreateFromBinaryForm(binaryForm, 0);
    }

    /// <summary>
    ///   Определяет, является ли указанный <see cref="T:System.Security.AccessControl.GenericAce" /> объект равен текущему объекту <see cref="T:System.Security.AccessControl.GenericAce" /> объекта.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.Security.AccessControl.GenericAce" /> Объект, сравниваемый с текущим <see cref="T:System.Security.AccessControl.GenericAce" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный <see cref="T:System.Security.AccessControl.GenericAce" /> объект равен текущему объекту <see cref="T:System.Security.AccessControl.GenericAce" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    public override sealed bool Equals(object o)
    {
      if (o == null)
        return false;
      GenericAce genericAce = o as GenericAce;
      if (genericAce == (GenericAce) null || this.AceType != genericAce.AceType || this.AceFlags != genericAce.AceFlags)
        return false;
      int binaryLength1 = this.BinaryLength;
      int binaryLength2 = genericAce.BinaryLength;
      if (binaryLength1 != binaryLength2)
        return false;
      byte[] binaryForm1 = new byte[binaryLength1];
      byte[] binaryForm2 = new byte[binaryLength2];
      this.GetBinaryForm(binaryForm1, 0);
      genericAce.GetBinaryForm(binaryForm2, 0);
      for (int index = 0; index < binaryForm1.Length; ++index)
      {
        if ((int) binaryForm1[index] != (int) binaryForm2[index])
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Служит хэш-функцией для <see cref="T:System.Security.AccessControl.GenericAce" /> класса.
    ///   <see cref="M:System.Security.AccessControl.GenericAce.GetHashCode" /> Метод подходит для использования в алгоритмах и структурах данных хеширования как хэш-таблицы.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Security.AccessControl.GenericAce" />.
    /// </returns>
    public override sealed int GetHashCode()
    {
      int binaryLength = this.BinaryLength;
      byte[] binaryForm = new byte[binaryLength];
      this.GetBinaryForm(binaryForm, 0);
      int num1 = 0;
      int index = 0;
      while (index < binaryLength)
      {
        int num2 = (int) binaryForm[index] + ((int) binaryForm[index + 1] << 8) + ((int) binaryForm[index + 2] << 16) + ((int) binaryForm[index + 3] << 24);
        num1 ^= num2;
        index += 4;
      }
      return num1;
    }

    /// <summary>
    ///   Определяет, является ли указанный <see cref="T:System.Security.AccessControl.GenericAce" /> объекты считаются равными.
    /// </summary>
    /// <param name="left">
    ///   Первый из сравниваемых объектов <see cref="T:System.Security.AccessControl.GenericAce" />.
    /// </param>
    /// <param name="right">
    ///   Второй экземпляр <see cref="T:System.Security.AccessControl.GenericAce" /> для сравнения.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если два объекта <see cref="T:System.Security.AccessControl.GenericAce" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(GenericAce left, GenericAce right)
    {
      object obj1 = (object) left;
      object obj2 = (object) right;
      if (obj1 == null && obj2 == null)
        return true;
      if (obj1 == null || obj2 == null)
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет, является ли указанный <see cref="T:System.Security.AccessControl.GenericAce" /> объекты считаются неравными.
    /// </summary>
    /// <param name="left">
    ///   Первый из сравниваемых объектов <see cref="T:System.Security.AccessControl.GenericAce" />.
    /// </param>
    /// <param name="right">
    ///   Второй экземпляр <see cref="T:System.Security.AccessControl.GenericAce" /> для сравнения.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если два <see cref="T:System.Security.AccessControl.GenericAce" /> объекты равны, в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(GenericAce left, GenericAce right)
    {
      return !(left == right);
    }
  }
}
