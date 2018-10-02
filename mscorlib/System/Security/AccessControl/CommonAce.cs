// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CommonAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>Представляет элемент управления доступом.</summary>
  public sealed class CommonAce : QualifiedAce
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.CommonAce" />.
    /// </summary>
    /// <param name="flags">
    ///   Флаги, определяющие параметры наследования и распространения наследования, а также условия аудита нового элемента управления доступом.
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
    /// <param name="isCallback">
    ///   <see langword="true" /> Чтобы указать, что новый элемент управления ДОСТУПОМ обратного вызова типа ACE.
    /// </param>
    /// <param name="opaque">
    ///   Непрозрачные данные, связанные с новым элементом управления доступом.
    ///    Непрозрачные данные разрешены только для элементов управления доступом обратного вызова.
    ///    Длина этого массива не должно быть больше, чем значение, возвращаемое <see cref="M:System.Security.AccessControl.CommonAce.MaxOpaqueLength(System.Boolean)" /> метод.
    /// </param>
    public CommonAce(AceFlags flags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, bool isCallback, byte[] opaque)
      : base(CommonAce.TypeFromQualifier(isCallback, qualifier), flags, accessMask, sid, opaque)
    {
    }

    private static AceType TypeFromQualifier(bool isCallback, AceQualifier qualifier)
    {
      switch (qualifier)
      {
        case AceQualifier.AccessAllowed:
          return !isCallback ? AceType.AccessAllowed : AceType.AccessAllowedCallback;
        case AceQualifier.AccessDenied:
          return !isCallback ? AceType.AccessDenied : AceType.AccessDeniedCallback;
        case AceQualifier.SystemAudit:
          return !isCallback ? AceType.SystemAudit : AceType.SystemAuditCallback;
        case AceQualifier.SystemAlarm:
          return !isCallback ? AceType.SystemAlarm : AceType.SystemAlarmCallback;
        default:
          throw new ArgumentOutOfRangeException(nameof (qualifier), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      }
    }

    internal static bool ParseBinaryForm(byte[] binaryForm, int offset, out AceQualifier qualifier, out int accessMask, out SecurityIdentifier sid, out bool isCallback, out byte[] opaque)
    {
      GenericAce.VerifyHeader(binaryForm, offset);
      if (binaryForm.Length - offset >= 8 + SecurityIdentifier.MinBinaryLength)
      {
        AceType aceType = (AceType) binaryForm[offset];
        switch (aceType)
        {
          case AceType.AccessAllowed:
          case AceType.AccessDenied:
          case AceType.SystemAudit:
          case AceType.SystemAlarm:
            isCallback = false;
            break;
          case AceType.AccessAllowedCallback:
          case AceType.AccessDeniedCallback:
          case AceType.SystemAuditCallback:
          case AceType.SystemAlarmCallback:
            isCallback = true;
            break;
          default:
            goto label_15;
        }
        switch (aceType)
        {
          case AceType.AccessAllowed:
          case AceType.AccessAllowedCallback:
            qualifier = AceQualifier.AccessAllowed;
            break;
          case AceType.AccessDenied:
          case AceType.AccessDeniedCallback:
            qualifier = AceQualifier.AccessDenied;
            break;
          case AceType.SystemAudit:
          case AceType.SystemAuditCallback:
            qualifier = AceQualifier.SystemAudit;
            break;
          case AceType.SystemAlarm:
          case AceType.SystemAlarmCallback:
            qualifier = AceQualifier.SystemAlarm;
            break;
          default:
            goto label_15;
        }
        int num1 = offset + 4;
        int num2 = 0;
        accessMask = (int) binaryForm[num1 + 0] + ((int) binaryForm[num1 + 1] << 8) + ((int) binaryForm[num1 + 2] << 16) + ((int) binaryForm[num1 + 3] << 24);
        int num3 = num2 + 4;
        sid = new SecurityIdentifier(binaryForm, num1 + num3);
        opaque = (byte[]) null;
        int num4 = ((int) binaryForm[offset + 3] << 8) + (int) binaryForm[offset + 2];
        if (num4 % 4 == 0)
        {
          int length = num4 - 4 - 4 - (int) (byte) sid.BinaryLength;
          if (length > 0)
          {
            opaque = new byte[length];
            for (int index = 0; index < length; ++index)
              opaque[index] = binaryForm[offset + num4 - length + index];
          }
          return true;
        }
      }
label_15:
      qualifier = AceQualifier.AccessAllowed;
      accessMask = 0;
      sid = (SecurityIdentifier) null;
      isCallback = false;
      opaque = (byte[]) null;
      return false;
    }

    /// <summary>
    ///   Возвращает длину в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.CommonAce" />.
    ///    Используйте эту длину с <see cref="M:System.Security.AccessControl.CommonAce.GetBinaryForm(System.Byte[],System.Int32)" /> метод перед маршалингом списка управления Доступом в двоичный массив.
    /// </summary>
    /// <returns>
    ///   Длина в байтах двоичного представления текущего объекта <see cref="T:System.Security.AccessControl.CommonAce" />.
    /// </returns>
    public override int BinaryLength
    {
      get
      {
        return 8 + this.SecurityIdentifier.BinaryLength + this.OpaqueLength;
      }
    }

    /// <summary>
    ///   Возвращает максимально допустимую длину большого двоичного объекта непрозрачных данных для элементов управления доступом обратного вызова.
    /// </summary>
    /// <param name="isCallback">
    ///   <see langword="true" /> Чтобы указать, что <see cref="T:System.Security.AccessControl.CommonAce" /> объект является элементом управления ДОСТУПОМ обратного.
    /// </param>
    /// <returns>
    ///   Допустимая длина большого двоичного объекта непрозрачных данных.
    /// </returns>
    public static int MaxOpaqueLength(bool isCallback)
    {
      return 65527 - SecurityIdentifier.MaxBinaryLength;
    }

    internal override int MaxOpaqueLengthInternal
    {
      get
      {
        return CommonAce.MaxOpaqueLength(this.IsCallback);
      }
    }

    /// <summary>
    ///   Маршалирует содержимое объекта <see cref="T:System.Security.AccessControl.CommonAce" /> в указанный массив байтов, начиная с указанной позиции.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, в котором содержимое <see cref="T:System.Security.AccessControl.CommonAce" /> маршалинге объекта.
    /// </param>
    /// <param name="offset">
    ///   Позиция, с которой начинается маршалинг.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> отрицательное или слишком большое, чтобы разрешить весь <see cref="T:System.Security.AccessControl.CommonAce" /> необходимо скопировать в <paramref name="binaryForm" /> массива.
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
      this.SecurityIdentifier.GetBinaryForm(binaryForm, num1 + num3);
      int num4 = num3 + this.SecurityIdentifier.BinaryLength;
      if (this.GetOpaque() == null)
        return;
      if (this.OpaqueLength > this.MaxOpaqueLengthInternal)
        throw new SystemException();
      this.GetOpaque().CopyTo((Array) binaryForm, num1 + num4);
    }
  }
}
