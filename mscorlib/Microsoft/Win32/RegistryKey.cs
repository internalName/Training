// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryKey
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.Win32
{
  /// <summary>
  ///   Представляет узел уровня раздела в реестре Windows.
  ///    Этот класс является инкапсуляцией реестра.
  /// </summary>
  [ComVisible(true)]
  public sealed class RegistryKey : MarshalByRefObject, IDisposable
  {
    internal static readonly IntPtr HKEY_CLASSES_ROOT = new IntPtr(int.MinValue);
    internal static readonly IntPtr HKEY_CURRENT_USER = new IntPtr(-2147483647);
    internal static readonly IntPtr HKEY_LOCAL_MACHINE = new IntPtr(-2147483646);
    internal static readonly IntPtr HKEY_USERS = new IntPtr(-2147483645);
    internal static readonly IntPtr HKEY_PERFORMANCE_DATA = new IntPtr(-2147483644);
    internal static readonly IntPtr HKEY_CURRENT_CONFIG = new IntPtr(-2147483643);
    internal static readonly IntPtr HKEY_DYN_DATA = new IntPtr(-2147483642);
    private static readonly string[] hkeyNames = new string[7]
    {
      nameof (HKEY_CLASSES_ROOT),
      nameof (HKEY_CURRENT_USER),
      nameof (HKEY_LOCAL_MACHINE),
      nameof (HKEY_USERS),
      nameof (HKEY_PERFORMANCE_DATA),
      nameof (HKEY_CURRENT_CONFIG),
      nameof (HKEY_DYN_DATA)
    };
    private const int STATE_DIRTY = 1;
    private const int STATE_SYSTEMKEY = 2;
    private const int STATE_WRITEACCESS = 4;
    private const int STATE_PERF_DATA = 8;
    private const int MaxKeyLength = 255;
    private const int MaxValueLength = 16383;
    [SecurityCritical]
    private volatile SafeRegistryHandle hkey;
    private volatile int state;
    private volatile string keyName;
    private volatile bool remoteKey;
    private volatile RegistryKeyPermissionCheck checkMode;
    private volatile RegistryView regView;
    private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
    private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
    private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

    [SecurityCritical]
    private RegistryKey(SafeRegistryHandle hkey, bool writable, RegistryView view)
      : this(hkey, writable, false, false, false, view)
    {
    }

    [SecurityCritical]
    private RegistryKey(SafeRegistryHandle hkey, bool writable, bool systemkey, bool remoteKey, bool isPerfData, RegistryView view)
    {
      this.hkey = hkey;
      this.keyName = "";
      this.remoteKey = remoteKey;
      this.regView = view;
      if (systemkey)
        this.state |= 2;
      if (writable)
        this.state |= 4;
      if (isPerfData)
        this.state |= 8;
      RegistryKey.ValidateKeyView(view);
    }

    /// <summary>
    ///   Если содержимое раздела было изменено, следует закрыть раздел и записать его на диск.
    /// </summary>
    public void Close()
    {
      this.Dispose(true);
    }

    [SecuritySafeCritical]
    private void Dispose(bool disposing)
    {
      if (this.hkey == null)
        return;
      if (!this.IsSystemKey())
      {
        try
        {
          this.hkey.Dispose();
        }
        catch (IOException ex)
        {
        }
        finally
        {
          this.hkey = (SafeRegistryHandle) null;
        }
      }
      else
      {
        if (!disposing || !this.IsPerfDataKey())
          return;
        SafeRegistryHandle.RegCloseKey(RegistryKey.HKEY_PERFORMANCE_DATA);
      }
    }

    /// <summary>
    ///   Записывает в реестр все атрибуты заданного открытого раздела реестра.
    /// </summary>
    [SecuritySafeCritical]
    public void Flush()
    {
      if (this.hkey == null || !this.IsDirty())
        return;
      Win32Native.RegFlushKey(this.hkey);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:Microsoft.Win32.RegistryKey" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Создает новый подраздел или открывает существующий для доступа на запись.
    /// </summary>
    /// <param name="subkey">
    ///   Имя или путь создаваемого или открываемого подраздела.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <returns>
    ///   Созданный подраздел или <see langword="null" /> в случае сбоя операции.
    ///    Если в качестве значения <paramref name="subkey" /> задана строка нулевой длины, возвращается текущий объект <see cref="T:Microsoft.Win32.RegistryKey" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="subkey" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У пользователя нет разрешений, необходимых для создания или открытия раздела реестра.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> На которой вызывается этот метод закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Не может быть записан; например, он не был открыт в качестве ключа для записи, или пользователь не имеет необходимых прав доступа.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Уровень вложенности превышает 510.
    /// 
    ///   -или-
    /// 
    ///   Произошла системная ошибка, например удаление раздела или попытка создать раздел в корне <see cref="F:Microsoft.Win32.Registry.LocalMachine" />.
    /// </exception>
    public RegistryKey CreateSubKey(string subkey)
    {
      return this.CreateSubKey(subkey, this.checkMode);
    }

    /// <summary>
    ///   Создает новый подраздел или открывает существующий доступом на запись, используя заданные параметры проверки разрешений.
    /// </summary>
    /// <param name="subkey">
    ///   Имя или путь создаваемого или открываемого подраздела.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="permissionCheck">
    ///   Одно из значений перечисления, определяющее, с какими правами открывается раздел: только для чтения или для чтения и записи.
    /// </param>
    /// <returns>
    ///   Созданный подраздел или <see langword="null" /> в случае сбоя операции.
    ///    Если в качестве значения <paramref name="subkey" /> задана строка нулевой длины, возвращается текущий объект <see cref="T:Microsoft.Win32.RegistryKey" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="subkey" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У пользователя нет разрешений, необходимых для создания или открытия раздела реестра.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="permissionCheck" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> На которой вызывается этот метод закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Не может быть записан; например, он не был открыт в качестве ключа для записи, или пользователь не имеет необходимых прав доступа.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Уровень вложенности превышает 510.
    /// 
    ///   -или-
    /// 
    ///   Произошла системная ошибка, например удаление раздела или попытка создать раздел в корне <see cref="F:Microsoft.Win32.Registry.LocalMachine" />.
    /// </exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
    {
      return this.CreateSubKeyInternal(subkey, permissionCheck, (object) null, RegistryOptions.None);
    }

    /// <summary>
    ///   Создает подраздел или подраздел с доступом на запись, используя параметры проверки и реестра указанное разрешение.
    /// </summary>
    /// <param name="subkey">
    ///   Имя или путь создаваемого или открываемого подраздела.
    /// </param>
    /// <param name="permissionCheck">
    ///   Одно из значений перечисления, определяющее, с какими правами открывается раздел: только для чтения или для чтения и записи.
    /// </param>
    /// <param name="options">
    ///   Параметр реестра, чтобы использовать; Например, который создает временный ключ.
    /// </param>
    /// <returns>
    ///   Созданный подраздел или <see langword="null" /> в случае сбоя операции.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="subkey " /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий <see cref="T:Microsoft.Win32.RegistryKey" /> объект закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Текущий <see cref="T:Microsoft.Win32.RegistryKey" /> объекта не может быть записан; например, он не был открыт в качестве ключа для записи или пользователь не имеет соответствующие права доступа.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Уровень вложенности превышает 510.
    /// 
    ///   -или-
    /// 
    ///   Произошла системная ошибка, например при удалении ключа или при попытке создать ключ в <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> корневой.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У пользователя нет разрешений, необходимых для создания или открытия раздела реестра.
    /// </exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options)
    {
      return this.CreateSubKeyInternal(subkey, permissionCheck, (object) null, options);
    }

    /// <summary>
    ///   Создает новый подраздел или открывает существующий с указанным доступом.
    /// 
    ///   Доступно начиная с версии .NET Framework 4.6
    /// </summary>
    /// <param name="subkey">
    ///   Имя или путь создаваемого или открываемого подраздела.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="writable">
    ///   Значение <see langword="true" /> указывает, что новый подраздел доступен для записи; в противном случае значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Созданный подраздел или <see langword="null" /> в случае сбоя операции.
    ///    Если в качестве значения <paramref name="subkey" /> задана строка нулевой длины, возвращается текущий объект <see cref="T:Microsoft.Win32.RegistryKey" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="subkey" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У пользователя нет разрешений, необходимых для создания или открытия раздела реестра.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Невозможно выполнить запись в текущий объект <see cref="T:Microsoft.Win32.RegistryKey" />; например, он не был открыт как раздел, доступный для записи, или у пользователя нет нужных прав доступа.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Уровень вложенности превышает 510.
    /// 
    ///   -или-
    /// 
    ///   Произошла системная ошибка, например удаление раздела или попытка создать раздел в корне <see cref="F:Microsoft.Win32.Registry.LocalMachine" />.
    /// </exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, bool writable)
    {
      return this.CreateSubKeyInternal(subkey, writable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree, (object) null, RegistryOptions.None);
    }

    /// <summary>
    ///   Создает новый подраздел или открывает существующий с указанным доступом.
    /// 
    ///   Доступно начиная с версии .NET Framework 4.6
    /// </summary>
    /// <param name="subkey">
    ///   Имя или путь создаваемого или открываемого подраздела.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="writable">
    ///   Значение <see langword="true" /> указывает, что новый подраздел доступен для записи; в противном случае значение <see langword="false" />.
    /// </param>
    /// <param name="options">Параметр реестра для использования.</param>
    /// <returns>
    ///   Созданный подраздел или <see langword="null" /> в случае сбоя операции.
    ///    Если в качестве значения <paramref name="subkey" /> задана строка нулевой длины, возвращается текущий объект <see cref="T:Microsoft.Win32.RegistryKey" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="subkey" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не задает допустимый параметр.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У пользователя нет разрешений, необходимых для создания или открытия раздела реестра.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Невозможно выполнить запись в текущий объект <see cref="T:Microsoft.Win32.RegistryKey" />; например, он не был открыт как раздел, доступный для записи, или у пользователя нет нужных прав доступа.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Уровень вложенности превышает 510.
    /// 
    ///   -или-
    /// 
    ///   Произошла системная ошибка, например удаление раздела или попытка создать раздел в корне <see cref="F:Microsoft.Win32.Registry.LocalMachine" />.
    /// </exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, bool writable, RegistryOptions options)
    {
      return this.CreateSubKeyInternal(subkey, writable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree, (object) null, options);
    }

    /// <summary>
    ///   Создает новый подраздел или открывает существующий доступом на запись, используя указанное разрешение проверьте параметр и реестром безопасности.
    /// </summary>
    /// <param name="subkey">
    ///   Имя или путь создаваемого или открываемого подраздела.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="permissionCheck">
    ///   Одно из значений перечисления, определяющее, с какими правами открывается раздел: только для чтения или для чтения и записи.
    /// </param>
    /// <param name="registrySecurity">
    ///   Безопасность управления доступом для нового раздела.
    /// </param>
    /// <returns>
    ///   Созданный подраздел или <see langword="null" /> в случае сбоя операции.
    ///    Если в качестве значения <paramref name="subkey" /> задана строка нулевой длины, возвращается текущий объект <see cref="T:Microsoft.Win32.RegistryKey" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="subkey" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У пользователя нет разрешений, необходимых для создания или открытия раздела реестра.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="permissionCheck" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> На которой вызывается этот метод закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Невозможно выполнить запись в текущий объект <see cref="T:Microsoft.Win32.RegistryKey" />; например, он не был открыт как раздел, доступный для записи, или у пользователя нет нужных прав доступа.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Уровень вложенности превышает 510.
    /// 
    ///   -или-
    /// 
    ///   Произошла системная ошибка, например удаление раздела или попытка создать раздел в корне <see cref="F:Microsoft.Win32.Registry.LocalMachine" />.
    /// </exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistrySecurity registrySecurity)
    {
      return this.CreateSubKeyInternal(subkey, permissionCheck, (object) registrySecurity, RegistryOptions.None);
    }

    /// <summary>
    ///   Создает подраздел или подраздел с доступом на запись, используя заданные параметры проверки разрешений, параметры реестра и безопасности реестра.
    /// </summary>
    /// <param name="subkey">
    ///   Имя или путь создаваемого или открываемого подраздела.
    /// </param>
    /// <param name="permissionCheck">
    ///   Одно из значений перечисления, определяющее, с какими правами открывается раздел: только для чтения или для чтения и записи.
    /// </param>
    /// <param name="registryOptions">
    ///   Параметр реестра для использования.
    /// </param>
    /// <param name="registrySecurity">
    ///   Безопасность управления доступом для нового раздела.
    /// </param>
    /// <returns>
    ///   Созданный подраздел или <see langword="null" /> в случае сбоя операции.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="subkey " /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий <see cref="T:Microsoft.Win32.RegistryKey" /> объект закрыт.
    ///    Невозможно получить доступ к закрытым ключам.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Текущий <see cref="T:Microsoft.Win32.RegistryKey" /> объекта не может быть записан; например, он не был открыт в качестве ключа для записи или пользователь не имеет соответствующие права доступа.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Уровень вложенности превышает 510.
    /// 
    ///   -или-
    /// 
    ///   Произошла системная ошибка, например при удалении ключа или при попытке создать ключ в <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> корневой.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У пользователя нет разрешений, необходимых для создания или открытия раздела реестра.
    /// </exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions registryOptions, RegistrySecurity registrySecurity)
    {
      return this.CreateSubKeyInternal(subkey, permissionCheck, (object) registrySecurity, registryOptions);
    }

    [SecuritySafeCritical]
    [ComVisible(false)]
    private unsafe RegistryKey CreateSubKeyInternal(string subkey, RegistryKeyPermissionCheck permissionCheck, object registrySecurityObj, RegistryOptions registryOptions)
    {
      RegistryKey.ValidateKeyOptions(registryOptions);
      RegistryKey.ValidateKeyName(subkey);
      RegistryKey.ValidateKeyMode(permissionCheck);
      this.EnsureWriteable();
      subkey = RegistryKey.FixupName(subkey);
      if (!this.remoteKey)
      {
        RegistryKey registryKey = this.InternalOpenSubKey(subkey, permissionCheck != RegistryKeyPermissionCheck.ReadSubTree);
        if (registryKey != null)
        {
          this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubKeyWritePermission, subkey, false, RegistryKeyPermissionCheck.Default);
          this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubTreePermission, subkey, false, permissionCheck);
          registryKey.checkMode = permissionCheck;
          return registryKey;
        }
      }
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubKeyCreatePermission, subkey, false, RegistryKeyPermissionCheck.Default);
      Win32Native.SECURITY_ATTRIBUTES securityAttributes = (Win32Native.SECURITY_ATTRIBUTES) null;
      RegistrySecurity registrySecurity = (RegistrySecurity) registrySecurityObj;
      if (registrySecurity != null)
      {
        securityAttributes = new Win32Native.SECURITY_ATTRIBUTES();
        securityAttributes.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(securityAttributes);
        byte[] descriptorBinaryForm = registrySecurity.GetSecurityDescriptorBinaryForm();
        byte* pDest = stackalloc byte[descriptorBinaryForm.Length];
        Buffer.Memcpy(pDest, 0, descriptorBinaryForm, 0, descriptorBinaryForm.Length);
        securityAttributes.pSecurityDescriptor = pDest;
      }
      int lpdwDisposition = 0;
      SafeRegistryHandle hkResult = (SafeRegistryHandle) null;
      int keyEx = Win32Native.RegCreateKeyEx(this.hkey, subkey, 0, (string) null, (int) registryOptions, (int) ((RegistryView) RegistryKey.GetRegistryKeyAccess(permissionCheck != RegistryKeyPermissionCheck.ReadSubTree) | this.regView), securityAttributes, out hkResult, out lpdwDisposition);
      if (keyEx == 0 && !hkResult.IsInvalid)
      {
        RegistryKey registryKey = new RegistryKey(hkResult, permissionCheck != RegistryKeyPermissionCheck.ReadSubTree, false, this.remoteKey, false, this.regView);
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubTreePermission, subkey, false, permissionCheck);
        registryKey.checkMode = permissionCheck;
        registryKey.keyName = subkey.Length != 0 ? this.keyName + "\\" + subkey : this.keyName;
        return registryKey;
      }
      if (keyEx != 0)
        this.Win32Error(keyEx, this.keyName + "\\" + subkey);
      return (RegistryKey) null;
    }

    /// <summary>Удаляет заданный подраздел.</summary>
    /// <param name="subkey">
    ///   Имя удаляемого подраздела.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="subkey" /> Содержит дочерние подразделы
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="subkey" /> Параметр не указан допустимый раздел реестра
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="subkey" /> равно <see langword="null" />
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для удаления раздела.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    public void DeleteSubKey(string subkey)
    {
      this.DeleteSubKey(subkey, true);
    }

    /// <summary>
    ///   Удаляет указанный подраздел и определяет, нужно ли исключение, если подраздел не найден.
    /// </summary>
    /// <param name="subkey">
    ///   Имя удаляемого подраздела.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="throwOnMissingSubKey">
    ///   Указывает, должно ли вызываться исключение, если заданный подраздел найти невозможно.
    ///    Если этот аргумент равен <see langword="true" /> а заданный подраздел не существует, возникает исключение.
    ///    Если этот аргумент равен <see langword="false" /> а заданный подраздел не существует, никакие действия не выполняются.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="subkey" /> содержит дочерние подразделы.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="subkey" /> не указан допустимый раздел реестра, и <paramref name="throwOnMissingSubKey" /> является <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="subkey" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для удаления раздела.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    [SecuritySafeCritical]
    public void DeleteSubKey(string subkey, bool throwOnMissingSubKey)
    {
      RegistryKey.ValidateKeyName(subkey);
      this.EnsureWriteable();
      subkey = RegistryKey.FixupName(subkey);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubKeyWritePermission, subkey, false, RegistryKeyPermissionCheck.Default);
      RegistryKey registryKey = this.InternalOpenSubKey(subkey, false);
      if (registryKey != null)
      {
        try
        {
          if (registryKey.InternalSubKeyCount() > 0)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_RegRemoveSubKey);
        }
        finally
        {
          registryKey.Close();
        }
        int errorCode;
        try
        {
          errorCode = Win32Native.RegDeleteKeyEx(this.hkey, subkey, (int) this.regView, 0);
        }
        catch (EntryPointNotFoundException ex)
        {
          errorCode = Win32Native.RegDeleteKey(this.hkey, subkey);
        }
        switch (errorCode)
        {
          case 0:
            break;
          case 2:
            if (!throwOnMissingSubKey)
              break;
            ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
            break;
          default:
            this.Win32Error(errorCode, (string) null);
            break;
        }
      }
      else
      {
        if (!throwOnMissingSubKey)
          return;
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
      }
    }

    /// <summary>
    ///   Удаляет подраздел и все дочерние подразделы рекурсивно.
    /// </summary>
    /// <param name="subkey">
    ///   Удаляемый подраздел.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="subkey" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Попытка удаления корневого куста реестра.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="subkey" /> не соответствует допустимому подключу реестра.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для удаления раздела.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    public void DeleteSubKeyTree(string subkey)
    {
      this.DeleteSubKeyTree(subkey, true);
    }

    /// <summary>
    ///   Удаляет указанный подраздел и все дочерние подразделы рекурсивно и указывает, нужно ли исключение, если подраздел не найден.
    /// </summary>
    /// <param name="subkey">
    ///   Имя удаляемого подраздела.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="throwOnMissingSubKey">
    ///   Указывает, должно ли вызываться исключение, если заданный подраздел найти невозможно.
    ///    Если этот аргумент равен <see langword="true" /> а заданный подраздел не существует, возникает исключение.
    ///    Если этот аргумент равен <see langword="false" /> а заданный подраздел не существует, никакие действия не выполняются.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Предпринята попытка удаления корневого куста дерева.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="subkey" /> не указан раздел реестра и <paramref name="throwOnMissingSubKey" /> — <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="subkey" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для удаления раздела.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void DeleteSubKeyTree(string subkey, bool throwOnMissingSubKey)
    {
      RegistryKey.ValidateKeyName(subkey);
      if (subkey.Length == 0 && this.IsSystemKey())
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegKeyDelHive);
      this.EnsureWriteable();
      subkey = RegistryKey.FixupName(subkey);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubTreeWritePermission, subkey, false, RegistryKeyPermissionCheck.Default);
      RegistryKey registryKey = this.InternalOpenSubKey(subkey, true);
      if (registryKey != null)
      {
        try
        {
          if (registryKey.InternalSubKeyCount() > 0)
          {
            foreach (string subKeyName in registryKey.InternalGetSubKeyNames())
              registryKey.DeleteSubKeyTreeInternal(subKeyName);
          }
        }
        finally
        {
          registryKey.Close();
        }
        int errorCode;
        try
        {
          errorCode = Win32Native.RegDeleteKeyEx(this.hkey, subkey, (int) this.regView, 0);
        }
        catch (EntryPointNotFoundException ex)
        {
          errorCode = Win32Native.RegDeleteKey(this.hkey, subkey);
        }
        if (errorCode == 0)
          return;
        this.Win32Error(errorCode, (string) null);
      }
      else
      {
        if (!throwOnMissingSubKey)
          return;
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
      }
    }

    [SecurityCritical]
    private void DeleteSubKeyTreeInternal(string subkey)
    {
      RegistryKey registryKey = this.InternalOpenSubKey(subkey, true);
      if (registryKey != null)
      {
        try
        {
          if (registryKey.InternalSubKeyCount() > 0)
          {
            foreach (string subKeyName in registryKey.InternalGetSubKeyNames())
              registryKey.DeleteSubKeyTreeInternal(subKeyName);
          }
        }
        finally
        {
          registryKey.Close();
        }
        int errorCode;
        try
        {
          errorCode = Win32Native.RegDeleteKeyEx(this.hkey, subkey, (int) this.regView, 0);
        }
        catch (EntryPointNotFoundException ex)
        {
          errorCode = Win32Native.RegDeleteKey(this.hkey, subkey);
        }
        if (errorCode == 0)
          return;
        this.Win32Error(errorCode, (string) null);
      }
      else
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
    }

    /// <summary>Удаляет заданное значение из этого раздела.</summary>
    /// <param name="name">Имя удаляемого параметра.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> не является допустимой ссылкой на значение.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для удаления значения.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Обрабатывается только для чтения.
    /// </exception>
    public void DeleteValue(string name)
    {
      this.DeleteValue(name, true);
    }

    /// <summary>
    ///   Удаляет указанное значение из данного раздела и определяет, нужно ли создавать исключение, если значение на найдено.
    /// </summary>
    /// <param name="name">Имя удаляемого параметра.</param>
    /// <param name="throwOnMissingValue">
    ///   Показывает, должно ли вызываться исключение, если заданное значение найти невозможно.
    ///    Если этот аргумент равен <see langword="true" /> и заданное значение не существует, возникает исключение.
    ///    Если этот аргумент равен <see langword="false" /> и заданное значение не существует, никакие действия не выполняются.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> не является допустимой ссылкой на значение и <paramref name="throwOnMissingValue" /> — <see langword="true" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для удаления значения.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Обрабатывается только для чтения.
    /// </exception>
    [SecuritySafeCritical]
    public void DeleteValue(string name, bool throwOnMissingValue)
    {
      this.EnsureWriteable();
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueWritePermission, name, false, RegistryKeyPermissionCheck.Default);
      switch (Win32Native.RegDeleteValue(this.hkey, name))
      {
        case 2:
        case 206:
          if (!throwOnMissingValue)
            break;
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyValueAbsent);
          break;
      }
    }

    [SecurityCritical]
    internal static RegistryKey GetBaseKey(IntPtr hKey)
    {
      return RegistryKey.GetBaseKey(hKey, RegistryView.Default);
    }

    [SecurityCritical]
    internal static RegistryKey GetBaseKey(IntPtr hKey, RegistryView view)
    {
      int index = (int) hKey & 268435455;
      bool flag = hKey == RegistryKey.HKEY_PERFORMANCE_DATA;
      return new RegistryKey(new SafeRegistryHandle(hKey, flag), true, true, false, flag, view)
      {
        checkMode = RegistryKeyPermissionCheck.Default,
        keyName = RegistryKey.hkeyNames[index]
      };
    }

    /// <summary>
    ///   Открывает новую <see cref="T:Microsoft.Win32.RegistryKey" /> , представляющий запрошенный раздел на локальном компьютере в указанном представлении.
    /// </summary>
    /// <param name="hKey">
    ///   Раздел HKEY, который необходимо открыть.
    /// </param>
    /// <param name="view">
    ///   Представление реестра для использования.
    /// </param>
    /// <returns>Запрошенный раздел реестра.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="hKey" />или <paramref name="view" /> является недопустимым.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для выполнения этого действия.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
    {
      RegistryKey.ValidateKeyView(view);
      RegistryKey.CheckUnmanagedCodePermission();
      return RegistryKey.GetBaseKey((IntPtr) ((int) hKey), view);
    }

    /// <summary>
    ///   Открывает новый <see cref="T:Microsoft.Win32.RegistryKey" /> представляющий запрошенный раздел на удаленном компьютере.
    /// </summary>
    /// <param name="hKey">
    ///   Открываемый раздел HKEY из <see cref="T:Microsoft.Win32.RegistryHive" /> перечисления.
    /// </param>
    /// <param name="machineName">Удаленный компьютер.</param>
    /// <returns>Запрошенный раздел реестра.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="hKey" /> недопустим.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="machineName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="machineName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для выполнения этой операции.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
    {
      return RegistryKey.OpenRemoteBaseKey(hKey, machineName, RegistryView.Default);
    }

    /// <summary>
    ///   Открывает новый раздел реестра, который представляет запрошенный раздел на удаленном компьютере в указанном представлении.
    /// </summary>
    /// <param name="hKey">
    ///   Открываемый раздел HKEY из <see cref="T:Microsoft.Win32.RegistryHive" /> перечисления...
    /// </param>
    /// <param name="machineName">Удаленный компьютер.</param>
    /// <param name="view">
    ///   Представление реестра для использования.
    /// </param>
    /// <returns>Запрошенный раздел реестра.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="hKey" /> или <paramref name="view" /> является недопустимым.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <paramref name="machineName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="machineName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="machineName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет необходимых разрешений для выполнения этой операции.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName, RegistryView view)
    {
      if (machineName == null)
        throw new ArgumentNullException(nameof (machineName));
      int index = (int) (hKey & (RegistryHive) 268435455);
      if (index < 0 || index >= RegistryKey.hkeyNames.Length || ((long) hKey & 4294967280L) != 2147483648L)
        throw new ArgumentException(Environment.GetResourceString("Arg_RegKeyOutOfRange"));
      RegistryKey.ValidateKeyView(view);
      RegistryKey.CheckUnmanagedCodePermission();
      SafeRegistryHandle result = (SafeRegistryHandle) null;
      int errorCode = Win32Native.RegConnectRegistry(machineName, new SafeRegistryHandle(new IntPtr((int) hKey), false), out result);
      switch (errorCode)
      {
        case 0:
          if (result.IsInvalid)
            throw new ArgumentException(Environment.GetResourceString("Arg_RegKeyNoRemoteConnect", (object) machineName));
          return new RegistryKey(result, true, false, true, (IntPtr) ((int) hKey) == RegistryKey.HKEY_PERFORMANCE_DATA, view)
          {
            checkMode = RegistryKeyPermissionCheck.Default,
            keyName = RegistryKey.hkeyNames[index]
          };
        case 1114:
          throw new ArgumentException(Environment.GetResourceString("Arg_DllInitFailure"));
        default:
          RegistryKey.Win32ErrorStatic(errorCode, (string) null);
          goto case 0;
      }
    }

    /// <summary>
    ///   Возвращает заданный подраздел и указывает, является ли доступ на запись для применения к ключу.
    /// </summary>
    /// <param name="name">Имя или путь к разделу, чтобы открыть.</param>
    /// <param name="writable">
    ///   Значение <see langword="true" /> при необходимости записи доступа к ключу.
    /// </param>
    /// <returns>
    ///   Подраздел, в запросе или <see langword="null" /> при сбое операции.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для доступа к разделу реестра в заданном режиме.
    /// </exception>
    [SecuritySafeCritical]
    public RegistryKey OpenSubKey(string name, bool writable)
    {
      RegistryKey.ValidateKeyName(name);
      this.EnsureNotDisposed();
      name = RegistryKey.FixupName(name);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckOpenSubKeyWithWritablePermission, name, writable, RegistryKeyPermissionCheck.Default);
      SafeRegistryHandle hkResult = (SafeRegistryHandle) null;
      int num = Win32Native.RegOpenKeyEx(this.hkey, name, 0, (int) ((RegistryView) RegistryKey.GetRegistryKeyAccess(writable) | this.regView), out hkResult);
      if (num == 0 && !hkResult.IsInvalid)
        return new RegistryKey(hkResult, writable, false, this.remoteKey, false, this.regView)
        {
          checkMode = this.GetSubKeyPermissonCheck(writable),
          keyName = this.keyName + "\\" + name
        };
      if (num == 5 || num == 1346)
        ThrowHelper.ThrowSecurityException(ExceptionResource.Security_RegistryPermission);
      return (RegistryKey) null;
    }

    /// <summary>
    ///   Возвращает заданный подраздел с доступом для чтения или для чтения и записи.
    /// </summary>
    /// <param name="name">
    ///   Имя или путь создаваемого или открываемого подраздела.
    /// </param>
    /// <param name="permissionCheck">
    ///   Одно из значений перечисления, определяющее, с какими правами открывается раздел: только для чтения или для чтения и записи.
    /// </param>
    /// <returns>
    ///   Запрошенный подраздел или <see langword="null" /> при сбое операции.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> равно <see langword="null" />
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="permissionCheck" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для чтения раздела реестра.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck)
    {
      RegistryKey.ValidateKeyMode(permissionCheck);
      return this.InternalOpenSubKey(name, permissionCheck, RegistryKey.GetRegistryKeyAccess(permissionCheck));
    }

    /// <summary>
    ///   Возвращает подраздел с указанным именем.
    /// 
    ///   Доступно начиная с версии .NET Framework 4.6
    /// </summary>
    /// <param name="name">
    ///   Имя или путь создаваемого или открываемого подраздела.
    /// </param>
    /// <param name="rights">Права для раздела реестра.</param>
    /// <returns>
    ///   Подраздел, в запросе или <see langword="null" /> при сбое операции.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для доступа к разделу реестра в заданном режиме.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public RegistryKey OpenSubKey(string name, RegistryRights rights)
    {
      return this.InternalOpenSubKey(name, this.checkMode, (int) rights);
    }

    /// <summary>
    ///   Возвращает заданный подраздел с доступом для чтения или для чтения и записи с запросом заданных прав доступа.
    /// </summary>
    /// <param name="name">
    ///   Имя или путь создаваемого или открываемого подраздела.
    /// </param>
    /// <param name="permissionCheck">
    ///   Одно из значений перечисления, определяющее, с какими правами открывается раздел: только для чтения или для чтения и записи.
    /// </param>
    /// <param name="rights">
    ///   Побитовое сочетание значений перечисления, которое определяет требуемый доступ безопасности.
    /// </param>
    /// <returns>
    ///   Запрошенный подраздел или <see langword="null" /> при сбое операции.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> равно <see langword="null" />
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="permissionCheck" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   <paramref name="rights" /> содержит недопустимый прав доступа к реестру.
    /// 
    ///   -или-
    /// 
    ///   Пользователь не имеет запрошенные разрешения.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights)
    {
      return this.InternalOpenSubKey(name, permissionCheck, (int) rights);
    }

    [SecurityCritical]
    private RegistryKey InternalOpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, int rights)
    {
      RegistryKey.ValidateKeyName(name);
      RegistryKey.ValidateKeyMode(permissionCheck);
      RegistryKey.ValidateKeyRights(rights);
      this.EnsureNotDisposed();
      name = RegistryKey.FixupName(name);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckOpenSubKeyPermission, name, false, permissionCheck);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubTreePermission, name, false, permissionCheck);
      SafeRegistryHandle hkResult = (SafeRegistryHandle) null;
      int num = Win32Native.RegOpenKeyEx(this.hkey, name, 0, (int) ((RegistryView) rights | this.regView), out hkResult);
      if (num == 0 && !hkResult.IsInvalid)
        return new RegistryKey(hkResult, permissionCheck == RegistryKeyPermissionCheck.ReadWriteSubTree, false, this.remoteKey, false, this.regView)
        {
          keyName = this.keyName + "\\" + name,
          checkMode = permissionCheck
        };
      if (num == 5 || num == 1346)
        ThrowHelper.ThrowSecurityException(ExceptionResource.Security_RegistryPermission);
      return (RegistryKey) null;
    }

    [SecurityCritical]
    internal RegistryKey InternalOpenSubKey(string name, bool writable)
    {
      RegistryKey.ValidateKeyName(name);
      this.EnsureNotDisposed();
      SafeRegistryHandle hkResult = (SafeRegistryHandle) null;
      if (Win32Native.RegOpenKeyEx(this.hkey, name, 0, (int) ((RegistryView) RegistryKey.GetRegistryKeyAccess(writable) | this.regView), out hkResult) != 0 || hkResult.IsInvalid)
        return (RegistryKey) null;
      return new RegistryKey(hkResult, writable, false, this.remoteKey, false, this.regView)
      {
        keyName = this.keyName + "\\" + name
      };
    }

    /// <summary>Возвращает подраздел с доступом только для чтения.</summary>
    /// <param name="name">
    ///   Имя или путь к разделу, чтобы открыть только для чтения.
    /// </param>
    /// <returns>
    ///   Подраздел, в запросе или <see langword="null" /> при сбое операции.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> равно <see langword="null" />
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для чтения раздела реестра.
    /// </exception>
    public RegistryKey OpenSubKey(string name)
    {
      return this.OpenSubKey(name, false);
    }

    /// <summary>
    ///   Возвращает количество подразделов для текущего раздела.
    /// </summary>
    /// <returns>Количество подразделов для текущего раздела.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь имеет разрешение на чтение для ключа.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла системная ошибка, например текущий ключ был удален.
    /// </exception>
    public int SubKeyCount
    {
      [SecuritySafeCritical] get
      {
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckKeyReadPermission, (string) null, false, RegistryKeyPermissionCheck.Default);
        return this.InternalSubKeyCount();
      }
    }

    /// <summary>
    ///   Возвращает представление, который использовался для создания раздела реестра.
    /// </summary>
    /// <returns>
    ///   Представление, которое использовалось для создания раздела реестра.
    /// 
    ///   -или-
    /// 
    ///   <see cref="F:Microsoft.Win32.RegistryView.Default" />, если представление не использовался.
    /// </returns>
    [ComVisible(false)]
    public RegistryView View
    {
      [SecuritySafeCritical] get
      {
        this.EnsureNotDisposed();
        return this.regView;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:Microsoft.Win32.SafeHandles.SafeRegistryHandle" /> ключ, представляющий реестра текущего <see cref="T:Microsoft.Win32.RegistryKey" /> инкапсулирует объект.
    /// </summary>
    /// <returns>Дескриптор раздела реестра.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Закрыт раздел реестра.
    ///    Невозможно получить доступ к закрытым ключам.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла системная ошибка, например при удалении текущего ключа.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения на чтение ключа.
    /// </exception>
    [ComVisible(false)]
    public SafeRegistryHandle Handle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.EnsureNotDisposed();
        int errorCode = 6;
        if (!this.IsSystemKey())
          return this.hkey;
        IntPtr hKey = (IntPtr) 0;
        switch (this.keyName)
        {
          case "HKEY_CLASSES_ROOT":
            hKey = RegistryKey.HKEY_CLASSES_ROOT;
            break;
          case "HKEY_CURRENT_CONFIG":
            hKey = RegistryKey.HKEY_CURRENT_CONFIG;
            break;
          case "HKEY_CURRENT_USER":
            hKey = RegistryKey.HKEY_CURRENT_USER;
            break;
          case "HKEY_DYN_DATA":
            hKey = RegistryKey.HKEY_DYN_DATA;
            break;
          case "HKEY_LOCAL_MACHINE":
            hKey = RegistryKey.HKEY_LOCAL_MACHINE;
            break;
          case "HKEY_PERFORMANCE_DATA":
            hKey = RegistryKey.HKEY_PERFORMANCE_DATA;
            break;
          case "HKEY_USERS":
            hKey = RegistryKey.HKEY_USERS;
            break;
          default:
            this.Win32Error(errorCode, (string) null);
            break;
        }
        SafeRegistryHandle hkResult;
        int num = Win32Native.RegOpenKeyEx(hKey, (string) null, 0, (int) ((RegistryView) RegistryKey.GetRegistryKeyAccess(this.IsWritable()) | this.regView), out hkResult);
        if (num == 0 && !hkResult.IsInvalid)
          return hkResult;
        this.Win32Error(num, (string) null);
        throw new IOException(Win32Native.GetMessage(num), num);
      }
    }

    /// <summary>
    ///   Создает раздел реестра на базе указанного дескриптора.
    /// </summary>
    /// <param name="handle">Дескриптор раздела реестра.</param>
    /// <returns>Раздел реестра.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="handle" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для выполнения этого действия.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static RegistryKey FromHandle(SafeRegistryHandle handle)
    {
      return RegistryKey.FromHandle(handle, RegistryView.Default);
    }

    /// <summary>
    ///   Создает раздел реестра из указанного дескриптора и параметров представления реестра.
    /// </summary>
    /// <param name="handle">Дескриптор раздела реестра.</param>
    /// <param name="view">
    ///   Представление реестра для использования.
    /// </param>
    /// <returns>Раздел реестра.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="view" /> недопустим.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="handle" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для выполнения этого действия.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static RegistryKey FromHandle(SafeRegistryHandle handle, RegistryView view)
    {
      if (handle == null)
        throw new ArgumentNullException(nameof (handle));
      RegistryKey.ValidateKeyView(view);
      return new RegistryKey(handle, true, view);
    }

    [SecurityCritical]
    internal int InternalSubKeyCount()
    {
      this.EnsureNotDisposed();
      int lpcSubKeys = 0;
      int lpcValues = 0;
      int errorCode = Win32Native.RegQueryInfoKey(this.hkey, (StringBuilder) null, (int[]) null, IntPtr.Zero, ref lpcSubKeys, (int[]) null, (int[]) null, ref lpcValues, (int[]) null, (int[]) null, (int[]) null, (int[]) null);
      if (errorCode != 0)
        this.Win32Error(errorCode, (string) null);
      return lpcSubKeys;
    }

    /// <summary>
    ///   Возвращает массив строк, который содержит все имена подразделов.
    /// </summary>
    /// <returns>
    ///   Массив строк, который содержит имена подразделов для текущего раздела.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для чтения из раздела.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла системная ошибка, например текущий ключ был удален.
    /// </exception>
    [SecuritySafeCritical]
    public string[] GetSubKeyNames()
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckKeyReadPermission, (string) null, false, RegistryKeyPermissionCheck.Default);
      return this.InternalGetSubKeyNames();
    }

    [SecurityCritical]
    internal unsafe string[] InternalGetSubKeyNames()
    {
      this.EnsureNotDisposed();
      int length1 = this.InternalSubKeyCount();
      string[] strArray = new string[length1];
      if (length1 > 0)
      {
        char[] chArray = new char[256];
        fixed (char* lpName = &chArray[0])
        {
          for (int dwIndex = 0; dwIndex < length1; ++dwIndex)
          {
            int length2 = chArray.Length;
            int errorCode = Win32Native.RegEnumKeyEx(this.hkey, dwIndex, lpName, ref length2, (int[]) null, (StringBuilder) null, (int[]) null, (long[]) null);
            if (errorCode != 0)
              this.Win32Error(errorCode, (string) null);
            strArray[dwIndex] = new string(lpName);
          }
        }
      }
      return strArray;
    }

    /// <summary>Возвращает число значений в разделе.</summary>
    /// <returns>Число пар "имя/значение" в разделе.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь имеет разрешение на чтение для ключа.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла системная ошибка, например текущий ключ был удален.
    /// </exception>
    public int ValueCount
    {
      [SecuritySafeCritical] get
      {
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckKeyReadPermission, (string) null, false, RegistryKeyPermissionCheck.Default);
        return this.InternalValueCount();
      }
    }

    [SecurityCritical]
    internal int InternalValueCount()
    {
      this.EnsureNotDisposed();
      int lpcValues = 0;
      int lpcSubKeys = 0;
      int errorCode = Win32Native.RegQueryInfoKey(this.hkey, (StringBuilder) null, (int[]) null, IntPtr.Zero, ref lpcSubKeys, (int[]) null, (int[]) null, ref lpcValues, (int[]) null, (int[]) null, (int[]) null, (int[]) null);
      if (errorCode != 0)
        this.Win32Error(errorCode, (string) null);
      return lpcValues;
    }

    /// <summary>
    ///   Возвращает массив строк, содержащий все имена значений, связанных с этим разделом.
    /// </summary>
    /// <returns>
    ///   Массив строк, который содержит имена значений для текущего раздела.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для чтения из раздела реестра.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла системная ошибка; например текущий ключ был удален.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe string[] GetValueNames()
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckKeyReadPermission, (string) null, false, RegistryKeyPermissionCheck.Default);
      this.EnsureNotDisposed();
      int length1 = this.InternalValueCount();
      string[] strArray = new string[length1];
      if (length1 > 0)
      {
        char[] chArray = new char[16384];
        fixed (char* lpValueName = &chArray[0])
        {
          for (int dwIndex = 0; dwIndex < length1; ++dwIndex)
          {
            int length2 = chArray.Length;
            int errorCode = Win32Native.RegEnumValue(this.hkey, dwIndex, lpValueName, ref length2, IntPtr.Zero, (int[]) null, (byte[]) null, (int[]) null);
            if (errorCode != 0 && (!this.IsPerfDataKey() || errorCode != 234))
              this.Win32Error(errorCode, (string) null);
            strArray[dwIndex] = new string(lpValueName);
          }
        }
      }
      return strArray;
    }

    /// <summary>
    ///   Возвращает значение, связанное с заданным именем.
    ///    Возвращает <see langword="null" /> Если пара имя значение не существует в реестре.
    /// </summary>
    /// <param name="name">
    ///   Имя извлекаемого значения.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <returns>
    ///   Значение, связанное с <paramref name="name" />, или <see langword="null" /> Если <paramref name="name" /> не найден.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для чтения из раздела реестра.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение, помечен для удаления.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    [SecuritySafeCritical]
    public object GetValue(string name)
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueReadPermission, name, false, RegistryKeyPermissionCheck.Default);
      return this.InternalGetValue(name, (object) null, false, true);
    }

    /// <summary>
    ///   Возвращает значение, связанное с заданным именем.
    ///    Если имя не найдено, возвращает предоставленное значение по умолчанию.
    /// </summary>
    /// <param name="name">
    ///   Имя извлекаемого значения.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="defaultValue">
    ///   Значение, возвращаемое, если <paramref name="name" /> не существует.
    /// </param>
    /// <returns>
    ///   Значение, связанное с <paramref name="name" />, ни с одним внедренных слева свернуты, переменные среды или <paramref name="defaultValue" /> Если <paramref name="name" /> не найден.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для чтения из раздела реестра.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение, помечен для удаления.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    [SecuritySafeCritical]
    public object GetValue(string name, object defaultValue)
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueReadPermission, name, false, RegistryKeyPermissionCheck.Default);
      return this.InternalGetValue(name, defaultValue, false, true);
    }

    /// <summary>
    ///   Возвращает значение, связанное с заданным именем и параметрами извлечения.
    ///    Если имя не найдено, возвращает предоставленное значение по умолчанию.
    /// </summary>
    /// <param name="name">
    ///   Имя извлекаемого значения.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <param name="defaultValue">
    ///   Значение, возвращаемое, если <paramref name="name" /> не существует.
    /// </param>
    /// <param name="options">
    ///   Одно из значений перечисления, определяющее дополнительную обработку возвращаемого значения.
    /// </param>
    /// <returns>
    ///   Значение, связанное с <paramref name="name" />, обработанные согласно указанному <paramref name="options" />, или <paramref name="defaultValue" /> Если <paramref name="name" /> не найден.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для чтения из раздела реестра.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение, помечен для удаления.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" />не является допустимым <see cref="T:Microsoft.Win32.RegistryValueOptions" /> значения; например, недопустимое значение приводится к <see cref="T:Microsoft.Win32.RegistryValueOptions" />.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public object GetValue(string name, object defaultValue, RegistryValueOptions options)
    {
      switch (options)
      {
        case RegistryValueOptions.None:
        case RegistryValueOptions.DoNotExpandEnvironmentNames:
          bool doNotExpand = options == RegistryValueOptions.DoNotExpandEnvironmentNames;
          this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueReadPermission, name, false, RegistryKeyPermissionCheck.Default);
          return this.InternalGetValue(name, defaultValue, doNotExpand, true);
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options), nameof (options));
      }
    }

    [SecurityCritical]
    internal object InternalGetValue(string name, object defaultValue, bool doNotExpand, bool checkSecurity)
    {
      if (checkSecurity)
        this.EnsureNotDisposed();
      object obj = defaultValue;
      int lpType = 0;
      int lpcbData1 = 0;
      int num1 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, (byte[]) null, ref lpcbData1);
      if (num1 != 0)
      {
        if (this.IsPerfDataKey())
        {
          int length = 65000;
          int lpcbData2 = length;
          byte[] lpData;
          int errorCode;
          for (lpData = new byte[length]; 234 == (errorCode = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData, ref lpcbData2)); lpData = new byte[length])
          {
            if (length == int.MaxValue)
              this.Win32Error(errorCode, name);
            else if (length > 1073741823)
              length = int.MaxValue;
            else
              length *= 2;
            lpcbData2 = length;
          }
          if (errorCode != 0)
            this.Win32Error(errorCode, name);
          return (object) lpData;
        }
        if (num1 != 234)
          return obj;
      }
      if (lpcbData1 < 0)
        lpcbData1 = 0;
      int num2;
      switch (lpType)
      {
        case 0:
        case 3:
        case 5:
          byte[] lpData1 = new byte[lpcbData1];
          num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData1, ref lpcbData1);
          obj = (object) lpData1;
          break;
        case 1:
          if (lpcbData1 % 2 == 1)
          {
            try
            {
              checked { ++lpcbData1; }
            }
            catch (OverflowException ex)
            {
              throw new IOException(Environment.GetResourceString("Arg_RegGetOverflowBug"), (Exception) ex);
            }
          }
          char[] lpData2 = new char[lpcbData1 / 2];
          num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData2, ref lpcbData1);
          obj = lpData2.Length == 0 || lpData2[lpData2.Length - 1] != char.MinValue ? (object) new string(lpData2) : (object) new string(lpData2, 0, lpData2.Length - 1);
          break;
        case 2:
          if (lpcbData1 % 2 == 1)
          {
            try
            {
              checked { ++lpcbData1; }
            }
            catch (OverflowException ex)
            {
              throw new IOException(Environment.GetResourceString("Arg_RegGetOverflowBug"), (Exception) ex);
            }
          }
          char[] lpData3 = new char[lpcbData1 / 2];
          num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData3, ref lpcbData1);
          obj = lpData3.Length == 0 || lpData3[lpData3.Length - 1] != char.MinValue ? (object) new string(lpData3) : (object) new string(lpData3, 0, lpData3.Length - 1);
          if (!doNotExpand)
          {
            obj = (object) Environment.ExpandEnvironmentVariables((string) obj);
            break;
          }
          break;
        case 4:
          if (lpcbData1 <= 4)
          {
            int lpData4 = 0;
            num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, ref lpData4, ref lpcbData1);
            obj = (object) lpData4;
            break;
          }
          goto case 11;
        case 7:
          if (lpcbData1 % 2 == 1)
          {
            try
            {
              checked { ++lpcbData1; }
            }
            catch (OverflowException ex)
            {
              throw new IOException(Environment.GetResourceString("Arg_RegGetOverflowBug"), (Exception) ex);
            }
          }
          char[] lpData5 = new char[lpcbData1 / 2];
          int num3 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData5, ref lpcbData1);
          if (lpData5.Length != 0)
          {
            if (lpData5[lpData5.Length - 1] != char.MinValue)
            {
              try
              {
                char[] chArray = new char[checked (lpData5.Length + 1)];
                for (int index = 0; index < lpData5.Length; ++index)
                  chArray[index] = lpData5[index];
                chArray[chArray.Length - 1] = char.MinValue;
                lpData5 = chArray;
              }
              catch (OverflowException ex)
              {
                throw new IOException(Environment.GetResourceString("Arg_RegGetOverflowBug"), (Exception) ex);
              }
              lpData5[lpData5.Length - 1] = char.MinValue;
            }
          }
          IList<string> stringList = (IList<string>) new List<string>();
          int startIndex = 0;
          int index1;
          for (int length = lpData5.Length; num3 == 0 && startIndex < length; startIndex = index1 + 1)
          {
            index1 = startIndex;
            while (index1 < length && lpData5[index1] != char.MinValue)
              ++index1;
            if (index1 < length)
            {
              if (index1 - startIndex > 0)
                stringList.Add(new string(lpData5, startIndex, index1 - startIndex));
              else if (index1 != length - 1)
                stringList.Add(string.Empty);
            }
            else
              stringList.Add(new string(lpData5, startIndex, length - startIndex));
          }
          obj = (object) new string[stringList.Count];
          stringList.CopyTo((string[]) obj, 0);
          break;
        case 11:
          if (lpcbData1 <= 8)
          {
            long lpData4 = 0;
            num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, ref lpData4, ref lpcbData1);
            obj = (object) lpData4;
            break;
          }
          goto case 0;
      }
      return obj;
    }

    /// <summary>
    ///   Возвращает тип данных реестра для значения, связанного с заданным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя значения, для которого возвращается тип данных реестра.
    ///    В этой строке не учитывается регистр знаков.
    /// </param>
    /// <returns>
    ///   Тип данных реестра значение, связанное с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для чтения из раздела реестра.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   , Содержащий указанное значение не существует.
    /// 
    ///   -или-
    /// 
    ///   Пара имя значение, заданная <paramref name="name" /> не существует.
    /// 
    ///   Это исключение не вызывается в Windows 95, Windows 98 или Windows Millennium Edition.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Пользователь имеет необходимые права.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public RegistryValueKind GetValueKind(string name)
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueReadPermission, name, false, RegistryKeyPermissionCheck.Default);
      this.EnsureNotDisposed();
      int lpType = 0;
      int lpcbData = 0;
      int errorCode = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, (byte[]) null, ref lpcbData);
      if (errorCode != 0)
        this.Win32Error(errorCode, (string) null);
      if (lpType == 0)
        return RegistryValueKind.None;
      if (!Enum.IsDefined(typeof (RegistryValueKind), (object) lpType))
        return RegistryValueKind.Unknown;
      return (RegistryValueKind) lpType;
    }

    private bool IsDirty()
    {
      return (uint) (this.state & 1) > 0U;
    }

    private bool IsSystemKey()
    {
      return (uint) (this.state & 2) > 0U;
    }

    private bool IsWritable()
    {
      return (uint) (this.state & 4) > 0U;
    }

    private bool IsPerfDataKey()
    {
      return (uint) (this.state & 8) > 0U;
    }

    /// <summary>Возвращает имя раздела.</summary>
    /// <returns>Абсолютное (полное) имя раздела.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    public string Name
    {
      [SecuritySafeCritical] get
      {
        this.EnsureNotDisposed();
        return this.keyName;
      }
    }

    private void SetDirty()
    {
      this.state |= 1;
    }

    /// <summary>Задает указанную пару "имя-значение".</summary>
    /// <param name="name">Имя значения для хранения.</param>
    /// <param name="value">Для сохранения данных.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" />имеет неподдерживаемый тип данных.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Доступно только для чтения и не может быть записан; например, ключ не был открыт с доступом на запись.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Объект представляет корневой узел, а операционной системой является Windows Millennium Edition или Windows 98.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для создания или изменения реестра.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Объект представляет корневой узел, а операционной системой является Windows 2000, Windows XP или Windows Server 2003.
    /// </exception>
    public void SetValue(string name, object value)
    {
      this.SetValue(name, value, RegistryValueKind.Unknown);
    }

    /// <summary>
    ///   Устанавливает значение пары "имя-значение" в разделе реестра, используя заданный тип данных реестра.
    /// </summary>
    /// <param name="name">Имя параметра для сохранения.</param>
    /// <param name="value">Для сохранения данных.</param>
    /// <param name="valueKind">
    ///   Тип данных реестра, используемый для хранения данных.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип <paramref name="value" /> не соответствует типу данных реестра, определяемое <paramref name="valueKind" />, поэтому данные не удалось правильно преобразовать.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Доступно только для чтения и не может быть записан; например, ключ не был открыт с доступом на запись.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Объект представляет корневой узел, а операционной системой является Windows Millennium Edition или Windows 98.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для создания или изменения реестра.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Объект представляет корневой узел, а операционной системой является Windows 2000, Windows XP или Windows Server 2003.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public unsafe void SetValue(string name, object value, RegistryValueKind valueKind)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if (name != null && name.Length > 16383)
        throw new ArgumentException(Environment.GetResourceString("Arg_RegValStrLenBug"));
      if (!Enum.IsDefined(typeof (RegistryValueKind), (object) valueKind))
        throw new ArgumentException(Environment.GetResourceString("Arg_RegBadKeyKind"), nameof (valueKind));
      this.EnsureWriteable();
      if (!this.remoteKey && this.ContainsRegistryValue(name))
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueWritePermission, name, false, RegistryKeyPermissionCheck.Default);
      else
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueCreatePermission, name, false, RegistryKeyPermissionCheck.Default);
      if (valueKind == RegistryValueKind.Unknown)
        valueKind = this.CalculateValueKind(value);
      int errorCode = 0;
      try
      {
        switch (valueKind)
        {
          case RegistryValueKind.None:
          case RegistryValueKind.Binary:
            byte[] lpData1 = (byte[]) value;
            errorCode = Win32Native.RegSetValueEx(this.hkey, name, 0, valueKind == RegistryValueKind.None ? RegistryValueKind.Unknown : RegistryValueKind.Binary, lpData1, lpData1.Length);
            break;
          case RegistryValueKind.String:
          case RegistryValueKind.ExpandString:
            string lpData2 = value.ToString();
            errorCode = Win32Native.RegSetValueEx(this.hkey, name, 0, valueKind, lpData2, checked (lpData2.Length * 2 + 2));
            break;
          case RegistryValueKind.DWord:
            int int32 = Convert.ToInt32(value, (IFormatProvider) CultureInfo.InvariantCulture);
            errorCode = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.DWord, ref int32, 4);
            break;
          case RegistryValueKind.MultiString:
            string[] strArray = (string[]) ((Array) value).Clone();
            int num = 0;
            for (int index = 0; index < strArray.Length; ++index)
            {
              if (strArray[index] == null)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetStrArrNull);
              checked { num += (strArray[index].Length + 1) * 2; }
            }
            int cbData = checked (num + 2);
            byte[] lpData3 = new byte[cbData];
            fixed (byte* numPtr = lpData3)
            {
              IntPtr dest = new IntPtr((void*) numPtr);
              for (int index = 0; index < strArray.Length; ++index)
              {
                string.InternalCopy(strArray[index], dest, checked (strArray[index].Length * 2));
                dest = new IntPtr((long) dest + (long) checked (strArray[index].Length * 2));
                *(short*) dest.ToPointer() = (short) 0;
                dest = new IntPtr((long) dest + 2L);
              }
              *(short*) dest.ToPointer() = (short) 0;
              dest = new IntPtr((long) dest + 2L);
              errorCode = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.MultiString, lpData3, cbData);
              break;
            }
          case RegistryValueKind.QWord:
            long int64 = Convert.ToInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
            errorCode = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.QWord, ref int64, 8);
            break;
        }
      }
      catch (OverflowException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
      }
      catch (InvalidOperationException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
      }
      catch (FormatException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
      }
      if (errorCode == 0)
        this.SetDirty();
      else
        this.Win32Error(errorCode, (string) null);
    }

    private RegistryValueKind CalculateValueKind(object value)
    {
      if (value is int)
        return RegistryValueKind.DWord;
      if (!(value is Array))
        return RegistryValueKind.String;
      if (value is byte[])
        return RegistryValueKind.Binary;
      if (value is string[])
        return RegistryValueKind.MultiString;
      throw new ArgumentException(Environment.GetResourceString("Arg_RegSetBadArrType", (object) value.GetType().Name));
    }

    /// <summary>Возвращает строковое представление этого раздела.</summary>
    /// <returns>
    ///   Строка, представляющая раздел.
    ///    Если указанный ключ является недопустимым (отсутствует) затем <see langword="null" /> возвращается.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Доступ к закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    [SecuritySafeCritical]
    public override string ToString()
    {
      this.EnsureNotDisposed();
      return this.keyName;
    }

    /// <summary>
    ///   Возвращает безопасность элемента управления доступом для текущего раздела реестра.
    /// </summary>
    /// <returns>
    ///   Объект, описывающий разрешения управления доступом для раздела реестра, представленного текущим <see cref="T:Microsoft.Win32.RegistryKey" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет необходимых разрешений.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий ключ был удален.
    /// </exception>
    public RegistrySecurity GetAccessControl()
    {
      return this.GetAccessControl(AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>
    ///   Возвращает заданные разделы безопасности элемента управления доступом для текущего раздела реестра.
    /// </summary>
    /// <param name="includeSections">
    ///   Побитовая комбинация значений перечисления, указывающее тип для получения сведений о безопасности.
    /// </param>
    /// <returns>
    ///   Объект, описывающий разрешения управления доступом для раздела реестра, представленного текущим <see cref="T:Microsoft.Win32.RegistryKey" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет необходимых разрешений.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий ключ был удален.
    /// </exception>
    [SecuritySafeCritical]
    public RegistrySecurity GetAccessControl(AccessControlSections includeSections)
    {
      this.EnsureNotDisposed();
      return new RegistrySecurity(this.hkey, this.keyName, includeSections);
    }

    /// <summary>
    ///   Применяет безопасность управления доступом Windows к существующему разделу реестра.
    /// </summary>
    /// <param name="registrySecurity">
    ///   Безопасность управления доступом для применения к текущему подразделу.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Текущий <see cref="T:Microsoft.Win32.RegistryKey" /> объект представляет собой ключ с помощью безопасности управления доступом и вызывающий объект не имеет <see cref="F:System.Security.AccessControl.RegistryRights.ChangePermissions" /> прав.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="registrySecurity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Управлении закрыт (к закрытым ключам доступ отсутствует).
    /// </exception>
    [SecuritySafeCritical]
    public void SetAccessControl(RegistrySecurity registrySecurity)
    {
      this.EnsureWriteable();
      if (registrySecurity == null)
        throw new ArgumentNullException(nameof (registrySecurity));
      registrySecurity.Persist(this.hkey, this.keyName);
    }

    [SecuritySafeCritical]
    internal void Win32Error(int errorCode, string str)
    {
      switch (errorCode)
      {
        case 2:
          throw new IOException(Environment.GetResourceString("Arg_RegKeyNotFound"), errorCode);
        case 5:
          if (str != null)
            throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_RegistryKeyGeneric_Key", (object) str));
          throw new UnauthorizedAccessException();
        case 6:
          if (!this.IsPerfDataKey())
          {
            this.hkey.SetHandleAsInvalid();
            this.hkey = (SafeRegistryHandle) null;
            break;
          }
          break;
      }
      throw new IOException(Win32Native.GetMessage(errorCode), errorCode);
    }

    [SecuritySafeCritical]
    internal static void Win32ErrorStatic(int errorCode, string str)
    {
      if (errorCode != 5)
        throw new IOException(Win32Native.GetMessage(errorCode), errorCode);
      if (str != null)
        throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_RegistryKeyGeneric_Key", (object) str));
      throw new UnauthorizedAccessException();
    }

    internal static string FixupName(string name)
    {
      if (name.IndexOf('\\') == -1)
        return name;
      StringBuilder path = new StringBuilder(name);
      RegistryKey.FixupPath(path);
      int index = path.Length - 1;
      if (index >= 0 && path[index] == '\\')
        path.Length = index;
      return path.ToString();
    }

    private static void FixupPath(StringBuilder path)
    {
      int length = path.Length;
      bool flag = false;
      char maxValue = char.MaxValue;
      for (int index = 1; index < length - 1; ++index)
      {
        if (path[index] == '\\')
        {
          ++index;
          while (index < length && path[index] == '\\')
          {
            path[index] = maxValue;
            ++index;
            flag = true;
          }
        }
      }
      if (!flag)
        return;
      int index1 = 0;
      int index2 = 0;
      while (index1 < length)
      {
        if ((int) path[index1] == (int) maxValue)
        {
          ++index1;
        }
        else
        {
          path[index2] = path[index1];
          ++index1;
          ++index2;
        }
      }
      path.Length += index2 - index1;
    }

    private void GetSubKeyReadPermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read;
      path = this.keyName + "\\" + subkeyName + "\\.";
    }

    private void GetSubKeyWritePermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Write;
      path = this.keyName + "\\" + subkeyName + "\\.";
    }

    private void GetSubKeyCreatePermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Create;
      path = this.keyName + "\\" + subkeyName + "\\.";
    }

    private void GetSubTreeReadPermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read;
      path = this.keyName + "\\" + subkeyName + "\\";
    }

    private void GetSubTreeWritePermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Write;
      path = this.keyName + "\\" + subkeyName + "\\";
    }

    private void GetSubTreeReadWritePermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read | RegistryPermissionAccess.Write;
      path = this.keyName + "\\" + subkeyName;
    }

    private void GetValueReadPermission(string valueName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read;
      path = this.keyName + "\\" + valueName;
    }

    private void GetValueWritePermission(string valueName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Write;
      path = this.keyName + "\\" + valueName;
    }

    private void GetValueCreatePermission(string valueName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Create;
      path = this.keyName + "\\" + valueName;
    }

    private void GetKeyReadPermission(out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read;
      path = this.keyName + "\\.";
    }

    [SecurityCritical]
    private void CheckPermission(RegistryKey.RegistryInternalCheck check, string item, bool subKeyWritable, RegistryKeyPermissionCheck subKeyCheck)
    {
      bool flag = false;
      RegistryPermissionAccess access = RegistryPermissionAccess.NoAccess;
      string path = (string) null;
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
        return;
      switch (check)
      {
        case RegistryKey.RegistryInternalCheck.CheckSubKeyWritePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetSubKeyWritePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubKeyReadPermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          flag = true;
          this.GetSubKeyReadPermission(item, out access, out path);
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubKeyCreatePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetSubKeyCreatePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubTreeReadPermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetSubTreeReadPermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubTreeWritePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetSubTreeWritePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubTreeReadWritePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          flag = true;
          this.GetSubTreeReadWritePermission(item, out access, out path);
          break;
        case RegistryKey.RegistryInternalCheck.CheckValueWritePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetValueWritePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckValueCreatePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetValueCreatePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckValueReadPermission:
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetValueReadPermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckKeyReadPermission:
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetKeyReadPermission(out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubTreePermission:
          switch (subKeyCheck)
          {
            case RegistryKeyPermissionCheck.ReadSubTree:
              if (this.checkMode == RegistryKeyPermissionCheck.Default)
              {
                if (this.remoteKey)
                {
                  RegistryKey.CheckUnmanagedCodePermission();
                  break;
                }
                flag = true;
                this.GetSubTreeReadPermission(item, out access, out path);
                break;
              }
              break;
            case RegistryKeyPermissionCheck.ReadWriteSubTree:
              if (this.checkMode != RegistryKeyPermissionCheck.ReadWriteSubTree)
              {
                if (this.remoteKey)
                {
                  RegistryKey.CheckUnmanagedCodePermission();
                  break;
                }
                flag = true;
                this.GetSubTreeReadWritePermission(item, out access, out path);
                break;
              }
              break;
          }
        case RegistryKey.RegistryInternalCheck.CheckOpenSubKeyWithWritablePermission:
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            if (this.remoteKey)
            {
              RegistryKey.CheckUnmanagedCodePermission();
              break;
            }
            flag = true;
            this.GetSubKeyReadPermission(item, out access, out path);
            break;
          }
          if (subKeyWritable && this.checkMode == RegistryKeyPermissionCheck.ReadSubTree)
          {
            if (this.remoteKey)
            {
              RegistryKey.CheckUnmanagedCodePermission();
              break;
            }
            flag = true;
            this.GetSubTreeReadWritePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckOpenSubKeyPermission:
          if (subKeyCheck == RegistryKeyPermissionCheck.Default && this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            if (this.remoteKey)
            {
              RegistryKey.CheckUnmanagedCodePermission();
              break;
            }
            flag = true;
            this.GetSubKeyReadPermission(item, out access, out path);
            break;
          }
          break;
      }
      if (!flag)
        return;
      new RegistryPermission(access, path).Demand();
    }

    [SecurityCritical]
    private static void CheckUnmanagedCodePermission()
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
    }

    [SecurityCritical]
    private bool ContainsRegistryValue(string name)
    {
      int lpType = 0;
      int lpcbData = 0;
      return Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, (byte[]) null, ref lpcbData) == 0;
    }

    [SecurityCritical]
    private void EnsureNotDisposed()
    {
      if (this.hkey != null)
        return;
      ThrowHelper.ThrowObjectDisposedException(this.keyName, ExceptionResource.ObjectDisposed_RegKeyClosed);
    }

    [SecurityCritical]
    private void EnsureWriteable()
    {
      this.EnsureNotDisposed();
      if (this.IsWritable())
        return;
      ThrowHelper.ThrowUnauthorizedAccessException(ExceptionResource.UnauthorizedAccess_RegistryNoWrite);
    }

    private static int GetRegistryKeyAccess(bool isWritable)
    {
      return isWritable ? 131103 : 131097;
    }

    private static int GetRegistryKeyAccess(RegistryKeyPermissionCheck mode)
    {
      int num = 0;
      switch (mode)
      {
        case RegistryKeyPermissionCheck.Default:
        case RegistryKeyPermissionCheck.ReadSubTree:
          num = 131097;
          break;
        case RegistryKeyPermissionCheck.ReadWriteSubTree:
          num = 131103;
          break;
      }
      return num;
    }

    private RegistryKeyPermissionCheck GetSubKeyPermissonCheck(bool subkeyWritable)
    {
      if (this.checkMode == RegistryKeyPermissionCheck.Default)
        return this.checkMode;
      return subkeyWritable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree;
    }

    private static void ValidateKeyName(string name)
    {
      if (name == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.name);
      int num = name.IndexOf("\\", StringComparison.OrdinalIgnoreCase);
      int startIndex = 0;
      for (; num != -1; num = name.IndexOf("\\", startIndex, StringComparison.OrdinalIgnoreCase))
      {
        if (num - startIndex > (int) byte.MaxValue)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegKeyStrLenBug);
        startIndex = num + 1;
      }
      if (name.Length - startIndex <= (int) byte.MaxValue)
        return;
      ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegKeyStrLenBug);
    }

    private static void ValidateKeyMode(RegistryKeyPermissionCheck mode)
    {
      switch (mode)
      {
        case RegistryKeyPermissionCheck.Default:
        case RegistryKeyPermissionCheck.ReadSubTree:
        case RegistryKeyPermissionCheck.ReadWriteSubTree:
          break;
        default:
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidRegistryKeyPermissionCheck, ExceptionArgument.mode);
          break;
      }
    }

    private static void ValidateKeyOptions(RegistryOptions options)
    {
      switch (options)
      {
        case RegistryOptions.None:
        case RegistryOptions.Volatile:
          break;
        default:
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidRegistryOptionsCheck, ExceptionArgument.options);
          break;
      }
    }

    private static void ValidateKeyView(RegistryView view)
    {
      if (view == RegistryView.Default || view == RegistryView.Registry32 || view == RegistryView.Registry64)
        return;
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidRegistryViewCheck, ExceptionArgument.view);
    }

    private static void ValidateKeyRights(int rights)
    {
      if ((rights & -983104) == 0)
        return;
      ThrowHelper.ThrowSecurityException(ExceptionResource.Security_RegistryPermission);
    }

    private enum RegistryInternalCheck
    {
      CheckSubKeyWritePermission,
      CheckSubKeyReadPermission,
      CheckSubKeyCreatePermission,
      CheckSubTreeReadPermission,
      CheckSubTreeWritePermission,
      CheckSubTreeReadWritePermission,
      CheckValueWritePermission,
      CheckValueCreatePermission,
      CheckValueReadPermission,
      CheckKeyReadPermission,
      CheckSubTreePermission,
      CheckOpenSubKeyWithWritablePermission,
      CheckOpenSubKeyPermission,
    }
  }
}
