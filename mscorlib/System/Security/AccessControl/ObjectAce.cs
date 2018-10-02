// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ObjectAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Управляет доступом к объектам служб каталогов.
  ///    Этот класс представляет элемент управления доступом (ACE), связанный с объектом каталога.
  /// </summary>
  public sealed class ObjectAce : QualifiedAce
  {
    internal static readonly int AccessMaskWithObjectType = 315;
    private ObjectAceFlags _objectFlags;
    private Guid _objectAceType;
    private Guid _inheritedObjectAceType;
    private const int ObjectFlagsLength = 4;
    private const int GuidLength = 16;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.ObjectAce" />.
    /// </summary>
    /// <param name="aceFlags">
    ///   Наследование, распространение наследования и условия аудита для нового элемента управления доступом (ACE).
    /// </param>
    /// <param name="qualifier">
    ///   Функция нового элемента управления доступом.
    /// </param>
    /// <param name="accessMask">
    ///   Маска доступа элемента управления доступом.
    /// </param>
    /// <param name="sid">
    ///   Объект <see cref="T:System.Security.Principal.SecurityIdentifier" />, связанный с новым элементом управления доступом.
    /// </param>
    /// <param name="flags">
    ///   Содержат ли параметры <paramref name="type" /> и <paramref name="inheritedType" /> допустимые идентификаторы GUID объектов.
    /// </param>
    /// <param name="type">
    ///   Идентификатор GUID, определяющий тип объекта, к которому применяется новый элемент управления доступом.
    /// </param>
    /// <param name="inheritedType">
    ///   Идентификатор GUID, определяющий тип объекта, который может наследовать новый элемент управления доступом.
    /// </param>
    /// <param name="isCallback">
    ///   Значение <see langword="true" />, если новый элемент управления доступом является элементом управления доступом обратного вызова.
    /// </param>
    /// <param name="opaque">
    ///   Непрозрачные данные, связанные с новым элементом управления доступом.
    ///    Непрозрачные данные разрешены только для элементов управления доступом обратного вызова.
    ///    Длина этого массива не должна превышать значение, возвращаемое методом <see cref="M:System.Security.AccessControl.ObjectAce.MaxOpaqueLength(System.Boolean)" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр квалификатора содержит недопустимое значение или длина значения непрозрачного параметра больше значения, возвращаемого методом <see cref="M:System.Security.AccessControl.ObjectAce.MaxOpaqueLength(System.Boolean)" />.
    /// </exception>
    public ObjectAce(AceFlags aceFlags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, ObjectAceFlags flags, Guid type, Guid inheritedType, bool isCallback, byte[] opaque)
      : base(ObjectAce.TypeFromQualifier(isCallback, qualifier), aceFlags, accessMask, sid, opaque)
    {
      this._objectFlags = flags;
      this._objectAceType = type;
      this._inheritedObjectAceType = inheritedType;
    }

    private static AceType TypeFromQualifier(bool isCallback, AceQualifier qualifier)
    {
      switch (qualifier)
      {
        case AceQualifier.AccessAllowed:
          return !isCallback ? AceType.AccessAllowedObject : AceType.AccessAllowedCallbackObject;
        case AceQualifier.AccessDenied:
          return !isCallback ? AceType.AccessDeniedObject : AceType.AccessDeniedCallbackObject;
        case AceQualifier.SystemAudit:
          return !isCallback ? AceType.SystemAuditObject : AceType.SystemAuditCallbackObject;
        case AceQualifier.SystemAlarm:
          return !isCallback ? AceType.SystemAlarmObject : AceType.SystemAlarmCallbackObject;
        default:
          throw new ArgumentOutOfRangeException(nameof (qualifier), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      }
    }

    internal bool ObjectTypesMatch(ObjectAceFlags objectFlags, Guid objectType)
    {
      return (this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == (objectFlags & ObjectAceFlags.ObjectAceTypePresent) && ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None || this.ObjectAceType.Equals(objectType));
    }

    internal bool InheritedObjectTypesMatch(ObjectAceFlags objectFlags, Guid inheritedObjectType)
    {
      return (this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == (objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) && ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None || this.InheritedObjectAceType.Equals(inheritedObjectType));
    }

    internal static bool ParseBinaryForm(byte[] binaryForm, int offset, out AceQualifier qualifier, out int accessMask, out SecurityIdentifier sid, out ObjectAceFlags objectFlags, out Guid objectAceType, out Guid inheritedObjectAceType, out bool isCallback, out byte[] opaque)
    {
      byte[] b = new byte[16];
      GenericAce.VerifyHeader(binaryForm, offset);
      if (binaryForm.Length - offset >= 12 + SecurityIdentifier.MinBinaryLength)
      {
        AceType aceType = (AceType) binaryForm[offset];
        switch (aceType)
        {
          case AceType.AccessAllowedObject:
          case AceType.AccessDeniedObject:
          case AceType.SystemAuditObject:
          case AceType.SystemAlarmObject:
            isCallback = false;
            break;
          case AceType.AccessAllowedCallbackObject:
          case AceType.AccessDeniedCallbackObject:
          case AceType.SystemAuditCallbackObject:
          case AceType.SystemAlarmCallbackObject:
            isCallback = true;
            break;
          default:
            goto label_35;
        }
        switch (aceType)
        {
          case AceType.AccessAllowedObject:
          case AceType.AccessAllowedCallbackObject:
            qualifier = AceQualifier.AccessAllowed;
            break;
          case AceType.AccessDeniedObject:
          case AceType.AccessDeniedCallbackObject:
            qualifier = AceQualifier.AccessDenied;
            break;
          case AceType.SystemAuditObject:
          case AceType.SystemAuditCallbackObject:
            qualifier = AceQualifier.SystemAudit;
            break;
          case AceType.SystemAlarmObject:
          case AceType.SystemAlarmCallbackObject:
            qualifier = AceQualifier.SystemAlarm;
            break;
          default:
            goto label_35;
        }
        int num1 = offset + 4;
        int num2 = 0;
        accessMask = (int) binaryForm[num1 + 0] + ((int) binaryForm[num1 + 1] << 8) + ((int) binaryForm[num1 + 2] << 16) + ((int) binaryForm[num1 + 3] << 24);
        int num3 = num2 + 4;
        objectFlags = (ObjectAceFlags) ((int) binaryForm[num1 + num3 + 0] + ((int) binaryForm[num1 + num3 + 1] << 8) + ((int) binaryForm[num1 + num3 + 2] << 16) + ((int) binaryForm[num1 + num3 + 3] << 24));
        int num4 = num3 + 4;
        if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
        {
          for (int index = 0; index < 16; ++index)
            b[index] = binaryForm[num1 + num4 + index];
          num4 += 16;
        }
        else
        {
          for (int index = 0; index < 16; ++index)
            b[index] = (byte) 0;
        }
        objectAceType = new Guid(b);
        if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
        {
          for (int index = 0; index < 16; ++index)
            b[index] = binaryForm[num1 + num4 + index];
          num4 += 16;
        }
        else
        {
          for (int index = 0; index < 16; ++index)
            b[index] = (byte) 0;
        }
        inheritedObjectAceType = new Guid(b);
        sid = new SecurityIdentifier(binaryForm, num1 + num4);
        opaque = (byte[]) null;
        int num5 = ((int) binaryForm[offset + 3] << 8) + (int) binaryForm[offset + 2];
        if (num5 % 4 == 0)
        {
          int length = num5 - 4 - 4 - 4 - (int) (byte) sid.BinaryLength;
          if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
            length -= 16;
          if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
            length -= 16;
          if (length > 0)
          {
            opaque = new byte[length];
            for (int index = 0; index < length; ++index)
              opaque[index] = binaryForm[offset + num5 - length + index];
          }
          return true;
        }
      }
label_35:
      qualifier = AceQualifier.AccessAllowed;
      accessMask = 0;
      sid = (SecurityIdentifier) null;
      objectFlags = ObjectAceFlags.None;
      objectAceType = Guid.NewGuid();
      inheritedObjectAceType = Guid.NewGuid();
      isCallback = false;
      opaque = (byte[]) null;
      return false;
    }

    /// <summary>
    ///   Возвращает или задает флаги, которые указывают, содержат ли свойства <see cref="P:System.Security.AccessControl.ObjectAce.ObjectAceType" /> и <see cref="P:System.Security.AccessControl.ObjectAce.InheritedObjectAceType" /> значения, идентифицирующие допустимые типы объектов.
    /// </summary>
    /// <returns>
    ///   Один или несколько членов перечисления <see cref="T:System.Security.AccessControl.ObjectAceFlags" />, сочетаемые с помощью логической операции ИЛИ.
    /// </returns>
    public ObjectAceFlags ObjectAceFlags
    {
      get
      {
        return this._objectFlags;
      }
      set
      {
        this._objectFlags = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает идентификатор GUID типа объекта, связанный с данным объектом <see cref="T:System.Security.AccessControl.ObjectAce" />.
    /// </summary>
    /// <returns>
    ///   Идентификатор GUID типа объекта, связанный с данным объектом <see cref="T:System.Security.AccessControl.ObjectAce" />.
    /// </returns>
    public Guid ObjectAceType
    {
      get
      {
        return this._objectAceType;
      }
      set
      {
        this._objectAceType = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает идентификатор GUID типа объекта, который может наследовать элемент управления доступом (ACE), представляемый данным объектом <see cref="T:System.Security.AccessControl.ObjectAce" />.
    /// </summary>
    /// <returns>
    ///   GUID типа объекта, который может наследовать элемент управления доступом (ACE), представляемый данным объектом <see cref="T:System.Security.AccessControl.ObjectAce" />.
    /// </returns>
    public Guid InheritedObjectAceType
    {
      get
      {
        return this._inheritedObjectAceType;
      }
      set
      {
        this._inheritedObjectAceType = value;
      }
    }

    /// <summary>
    ///   Возвращает длину в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.ObjectAce" />.
    ///    Эту длину необходимо использовать перед маршалингом списка управления доступом в двоичный массив с помощью метода <see cref="M:System.Security.AccessControl.ObjectAce.GetBinaryForm(System.Byte[],System.Int32)" />.
    /// </summary>
    /// <returns>
    ///   Длина в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.ObjectAce" />.
    /// </returns>
    public override int BinaryLength
    {
      get
      {
        return 12 + (((this._objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None ? 16 : 0) + ((this._objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None ? 16 : 0)) + this.SecurityIdentifier.BinaryLength + this.OpaqueLength;
      }
    }

    /// <summary>
    ///   Возвращает максимально допустимую длину (в байтах) большого двоичного объекта непрозрачных данных для элементов управления доступом (ACE) обратного вызова.
    /// </summary>
    /// <param name="isCallback">
    ///   Значение true, если <see cref="T:System.Security.AccessControl.ObjectAce" /> является типом ACE обратного вызова.
    /// </param>
    /// <returns>
    ///   Максимально допустимая длина (в байтах) большого двоичного объекта непрозрачных данных для элементов управления доступом (ACE) обратного вызова.
    /// </returns>
    public static int MaxOpaqueLength(bool isCallback)
    {
      return 65491 - SecurityIdentifier.MaxBinaryLength;
    }

    internal override int MaxOpaqueLengthInternal
    {
      get
      {
        return ObjectAce.MaxOpaqueLength(this.IsCallback);
      }
    }

    /// <summary>
    ///   Маршалирует содержимое объекта <see cref="T:System.Security.AccessControl.ObjectAce" /> в указанный массив байтов, начиная с указанной позиции.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, в который маршалируется содержимое объекта <see cref="T:System.Security.AccessControl.ObjectAce" />.
    /// </param>
    /// <param name="offset">
    ///   Позиция, с которой начинается маршалинг.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="offset" /> является отрицательным или слишком велико, чтобы разрешить копирование всего <see cref="T:System.Security.AccessControl.ObjectAce" /> в <paramref name="array" />.
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
      binaryForm[num1 + num3 + 0] = (byte) this.ObjectAceFlags;
      binaryForm[num1 + num3 + 1] = (byte) ((uint) this.ObjectAceFlags >> 8);
      binaryForm[num1 + num3 + 2] = (byte) ((uint) this.ObjectAceFlags >> 16);
      binaryForm[num1 + num3 + 3] = (byte) ((uint) this.ObjectAceFlags >> 24);
      int num4 = num3 + 4;
      Guid guid;
      if ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
      {
        guid = this.ObjectAceType;
        guid.ToByteArray().CopyTo((Array) binaryForm, num1 + num4);
        num4 += 16;
      }
      if ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
      {
        guid = this.InheritedObjectAceType;
        guid.ToByteArray().CopyTo((Array) binaryForm, num1 + num4);
        num4 += 16;
      }
      this.SecurityIdentifier.GetBinaryForm(binaryForm, num1 + num4);
      int num5 = num4 + this.SecurityIdentifier.BinaryLength;
      if (this.GetOpaque() == null)
        return;
      if (this.OpaqueLength > this.MaxOpaqueLengthInternal)
        throw new SystemException();
      this.GetOpaque().CopyTo((Array) binaryForm, num1 + num5);
    }
  }
}
