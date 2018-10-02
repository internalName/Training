// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RawSecurityDescriptor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет дескриптор безопасности.
  ///    Дескриптор безопасности включает владельца, основную группу, список управления доступом на уровне пользователей (DACL) и системный список управления доступом управления (SACL).
  /// </summary>
  public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
  {
    private SecurityIdentifier _owner;
    private SecurityIdentifier _group;
    private ControlFlags _flags;
    private RawAcl _sacl;
    private RawAcl _dacl;
    private byte _rmControl;

    internal override GenericAcl GenericSacl
    {
      get
      {
        return (GenericAcl) this._sacl;
      }
    }

    internal override GenericAcl GenericDacl
    {
      get
      {
        return (GenericAcl) this._dacl;
      }
    }

    private void CreateFromParts(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
    {
      this.SetFlags(flags);
      this.Owner = owner;
      this.Group = group;
      this.SystemAcl = systemAcl;
      this.DiscretionaryAcl = discretionaryAcl;
      this.ResourceManagerControl = (byte) 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> с использованием указанных значений.
    /// </summary>
    /// <param name="flags">
    ///   Флаги, определяющие поведение нового <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="owner">
    ///   Владельца для нового <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="group">
    ///   Основной группы для нового <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="systemAcl">
    ///   Список управления доступом системы (SACL) для нового <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="discretionaryAcl">
    ///   Список (ДОСТУПОМ) для нового <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </param>
    public RawSecurityDescriptor(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
    {
      this.CreateFromParts(flags, owner, group, systemAcl, discretionaryAcl);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> класс из указанной строки языка определения дескрипторов безопасности (SDDL).
    /// </summary>
    /// <param name="sddlForm">
    ///   Строка SDDL, из которой будет создан новый <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </param>
    [SecuritySafeCritical]
    public RawSecurityDescriptor(string sddlForm)
      : this(RawSecurityDescriptor.BinaryFormFromSddlForm(sddlForm), 0)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> класс из указанного массива значений типа byte.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтовых значений, из которого будет создан новый <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </param>
    /// <param name="offset">
    ///   Смещение в  <paramref name="binaryForm" /> массиве, с которого начинается копирование.
    /// </param>
    public RawSecurityDescriptor(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException(nameof (binaryForm));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < 20)
        throw new ArgumentOutOfRangeException(nameof (binaryForm), Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      if ((int) binaryForm[offset + 0] != (int) GenericSecurityDescriptor.Revision)
        throw new ArgumentOutOfRangeException(nameof (binaryForm), Environment.GetResourceString("AccessControl_InvalidSecurityDescriptorRevision"));
      byte num1 = binaryForm[offset + 1];
      ControlFlags flags = (ControlFlags) ((int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8));
      if ((flags & ControlFlags.SelfRelative) == ControlFlags.None)
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidSecurityDescriptorSelfRelativeForm"), nameof (binaryForm));
      int num2 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 4);
      SecurityIdentifier owner = num2 == 0 ? (SecurityIdentifier) null : new SecurityIdentifier(binaryForm, offset + num2);
      int num3 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 8);
      SecurityIdentifier group = num3 == 0 ? (SecurityIdentifier) null : new SecurityIdentifier(binaryForm, offset + num3);
      int num4 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 12);
      RawAcl systemAcl = (flags & ControlFlags.SystemAclPresent) == ControlFlags.None || num4 == 0 ? (RawAcl) null : new RawAcl(binaryForm, offset + num4);
      int num5 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 16);
      RawAcl discretionaryAcl = (flags & ControlFlags.DiscretionaryAclPresent) == ControlFlags.None || num5 == 0 ? (RawAcl) null : new RawAcl(binaryForm, offset + num5);
      this.CreateFromParts(flags, owner, group, systemAcl, discretionaryAcl);
      if ((flags & ControlFlags.RMControlValid) == ControlFlags.None)
        return;
      this.ResourceManagerControl = num1;
    }

    [SecurityCritical]
    private static byte[] BinaryFormFromSddlForm(string sddlForm)
    {
      if (sddlForm == null)
        throw new ArgumentNullException(nameof (sddlForm));
      IntPtr resultSd = IntPtr.Zero;
      uint resultSdLength = 0;
      byte[] destination = (byte[]) null;
      try
      {
        if (1 != Win32Native.ConvertStringSdToSd(sddlForm, (uint) GenericSecurityDescriptor.Revision, out resultSd, ref resultSdLength))
        {
          switch (Marshal.GetLastWin32Error())
          {
            case 0:
              break;
            case 8:
              throw new OutOfMemoryException();
            case 87:
            case 1305:
            case 1336:
            case 1338:
              throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidSDSddlForm"), nameof (sddlForm));
            case 1337:
              throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidSidInSDDLString"), nameof (sddlForm));
            default:
              throw new SystemException();
          }
        }
        destination = new byte[(int) resultSdLength];
        Marshal.Copy(resultSd, destination, 0, (int) resultSdLength);
      }
      finally
      {
        if (resultSd != IntPtr.Zero)
          Win32Native.LocalFree(resultSd);
      }
      return destination;
    }

    /// <summary>
    ///   Возвращает значения, которые определяют поведение <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </summary>
    /// <returns>
    ///   Одно или несколько значений перечисления <see cref="T:System.Security.AccessControl.ControlFlags" />, объединенных с помощью логической операции ИЛИ.
    /// </returns>
    public override ControlFlags ControlFlags
    {
      get
      {
        return this._flags;
      }
    }

    /// <summary>
    ///   Возвращает или задает владельца объекта, связанного с этим объектом <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" />.
    /// </summary>
    /// <returns>
    ///   Владелец объекта, связанного с этим объектом <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" />.
    /// </returns>
    public override SecurityIdentifier Owner
    {
      get
      {
        return this._owner;
      }
      set
      {
        this._owner = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает основную группу для этого объекта <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" />.
    /// </summary>
    /// <returns>
    ///   Основная группа для этого объекта <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" />.
    /// </returns>
    public override SecurityIdentifier Group
    {
      get
      {
        return this._group;
      }
      set
      {
        this._group = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает список управления доступом (SACL) системы для этого <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    ///    SACL содержит правила аудита.
    /// </summary>
    /// <returns>
    ///   Системный список управления ДОСТУПОМ для данного <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </returns>
    public RawAcl SystemAcl
    {
      get
      {
        return this._sacl;
      }
      set
      {
        this._sacl = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает список (ДОСТУПОМ) для этого <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    ///    Список DACL содержит правила доступа.
    /// </summary>
    /// <returns>
    ///   Список DACL для этого <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </returns>
    public RawAcl DiscretionaryAcl
    {
      get
      {
        return this._dacl;
      }
      set
      {
        this._dacl = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение в байтах, представляющий управляющие биты диспетчера ресурсов, связанных с данным <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </summary>
    /// <returns>
    ///   Байтовое значение, представляющее диспетчера ресурсов управления bits, связанный с этим <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта.
    /// </returns>
    public byte ResourceManagerControl
    {
      get
      {
        return this._rmControl;
      }
      set
      {
        this._rmControl = value;
      }
    }

    /// <summary>
    ///   Наборы <see cref="P:System.Security.AccessControl.RawSecurityDescriptor.ControlFlags" /> это свойство <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> объекта с указанным значением.
    /// </summary>
    /// <param name="flags">
    ///   Одно или несколько значений перечисления <see cref="T:System.Security.AccessControl.ControlFlags" />, объединенных с помощью логической операции ИЛИ.
    /// </param>
    public void SetFlags(ControlFlags flags)
    {
      this._flags = flags | ControlFlags.SelfRelative;
    }
  }
}
