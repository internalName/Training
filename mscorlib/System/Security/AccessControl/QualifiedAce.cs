// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.QualifiedAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет элемент управления входа ДОСТУПОМ, содержащий квалификатор.
  ///    Квалификатор, представленного <see cref="T:System.Security.AccessControl.AceQualifier" /> объекта, указывает ли запись ACE доступ, отказывает в доступе, вызов системного аудита или системного оповещения.
  ///   <see cref="T:System.Security.AccessControl.QualifiedAce" /> — Абстрактный базовый класс для <see cref="T:System.Security.AccessControl.CommonAce" /> и <see cref="T:System.Security.AccessControl.ObjectAce" /> классы.
  /// </summary>
  public abstract class QualifiedAce : KnownAce
  {
    private readonly bool _isCallback;
    private readonly AceQualifier _qualifier;
    private byte[] _opaque;

    private AceQualifier QualifierFromType(AceType type, out bool isCallback)
    {
      switch (type)
      {
        case AceType.AccessAllowed:
          isCallback = false;
          return AceQualifier.AccessAllowed;
        case AceType.AccessDenied:
          isCallback = false;
          return AceQualifier.AccessDenied;
        case AceType.SystemAudit:
          isCallback = false;
          return AceQualifier.SystemAudit;
        case AceType.SystemAlarm:
          isCallback = false;
          return AceQualifier.SystemAlarm;
        case AceType.AccessAllowedObject:
          isCallback = false;
          return AceQualifier.AccessAllowed;
        case AceType.AccessDeniedObject:
          isCallback = false;
          return AceQualifier.AccessDenied;
        case AceType.SystemAuditObject:
          isCallback = false;
          return AceQualifier.SystemAudit;
        case AceType.SystemAlarmObject:
          isCallback = false;
          return AceQualifier.SystemAlarm;
        case AceType.AccessAllowedCallback:
          isCallback = true;
          return AceQualifier.AccessAllowed;
        case AceType.AccessDeniedCallback:
          isCallback = true;
          return AceQualifier.AccessDenied;
        case AceType.AccessAllowedCallbackObject:
          isCallback = true;
          return AceQualifier.AccessAllowed;
        case AceType.AccessDeniedCallbackObject:
          isCallback = true;
          return AceQualifier.AccessDenied;
        case AceType.SystemAuditCallback:
          isCallback = true;
          return AceQualifier.SystemAudit;
        case AceType.SystemAlarmCallback:
          isCallback = true;
          return AceQualifier.SystemAlarm;
        case AceType.SystemAuditCallbackObject:
          isCallback = true;
          return AceQualifier.SystemAudit;
        case AceType.SystemAlarmCallbackObject:
          isCallback = true;
          return AceQualifier.SystemAlarm;
        default:
          throw new SystemException();
      }
    }

    internal QualifiedAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier sid, byte[] opaque)
      : base(type, flags, accessMask, sid)
    {
      this._qualifier = this.QualifierFromType(type, out this._isCallback);
      this.SetOpaque(opaque);
    }

    /// <summary>
    ///   Возвращает значение, указывающее ли запись ACE доступ, отказывает в доступе, вызов системного аудита или системного оповещения.
    /// </summary>
    /// <returns>
    ///   Значение, указывающее, разрешает ли запись ACE доступ, отказывает в доступе, вызов системного аудита или системного оповещения.
    /// </returns>
    public AceQualifier AceQualifier
    {
      get
      {
        return this._qualifier;
      }
    }

    /// <summary>
    ///   Указывает, является ли это <see cref="T:System.Security.AccessControl.QualifiedAce" /> объект содержит данные обратного вызова.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот <see cref="T:System.Security.AccessControl.QualifiedAce" /> объект содержит данные обратного вызова; в противном случае — значение false.
    /// </returns>
    public bool IsCallback
    {
      get
      {
        return this._isCallback;
      }
    }

    internal abstract int MaxOpaqueLengthInternal { get; }

    /// <summary>
    ///   Возвращает длину данных непрозрачный обратного вызова, связанный с этим <see cref="T:System.Security.AccessControl.QualifiedAce" /> объекта.
    ///    Данное свойство допустимо только для записи управления доступом (ACE) обратного вызова.
    /// </summary>
    /// <returns>Длина непрозрачных данных обратного вызова.</returns>
    public int OpaqueLength
    {
      get
      {
        if (this._opaque != null)
          return this._opaque.Length;
        return 0;
      }
    }

    /// <summary>
    ///   Возвращает данные непрозрачный обратного вызова, связанный с этим <see cref="T:System.Security.AccessControl.QualifiedAce" /> объекта.
    /// </summary>
    /// <returns>
    ///   Массив байтовых значений, представляющий данные непрозрачный обратного вызова, связанный с этим <see cref="T:System.Security.AccessControl.QualifiedAce" /> объекта.
    /// </returns>
    public byte[] GetOpaque()
    {
      return this._opaque;
    }

    /// <summary>
    ///   Задает данные непрозрачный обратного вызова, связанный с этим <see cref="T:System.Security.AccessControl.QualifiedAce" /> объекта.
    /// </summary>
    /// <param name="opaque">
    ///   Массив байтовых значений, представляющий непрозрачных данных обратного вызова для этой <see cref="T:System.Security.AccessControl.QualifiedAce" /> объекта.
    /// </param>
    public void SetOpaque(byte[] opaque)
    {
      if (opaque != null)
      {
        if (opaque.Length > this.MaxOpaqueLengthInternal)
          throw new ArgumentOutOfRangeException(nameof (opaque), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLength"), (object) 0, (object) this.MaxOpaqueLengthInternal));
        if (opaque.Length % 4 != 0)
          throw new ArgumentOutOfRangeException(nameof (opaque), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple"), (object) 4));
      }
      this._opaque = opaque;
    }
  }
}
