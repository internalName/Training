// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageFile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace System.IO.IsolatedStorage
{
  /// <summary>
  ///   Предоставляет область изолированного хранилища, в которой содержатся файлы и каталоги.
  /// </summary>
  [ComVisible(true)]
  public sealed class IsolatedStorageFile : System.IO.IsolatedStorage.IsolatedStorage, IDisposable
  {
    private object m_internalLock = new object();
    private const int s_BlockSize = 1024;
    private const int s_DirSize = 1024;
    private const string s_name = "file.store";
    internal const string s_Files = "Files";
    internal const string s_AssemFiles = "AssemFiles";
    internal const string s_AppFiles = "AppFiles";
    internal const string s_IDFile = "identity.dat";
    internal const string s_InfoFile = "info.dat";
    internal const string s_AppInfoFile = "appinfo.dat";
    private static volatile string s_RootDirUser;
    private static volatile string s_RootDirMachine;
    private static volatile string s_RootDirRoaming;
    private static volatile string s_appDataDir;
    private static volatile FileIOPermission s_PermUser;
    private static volatile FileIOPermission s_PermMachine;
    private static volatile FileIOPermission s_PermRoaming;
    private static volatile IsolatedStorageFilePermission s_PermAdminUser;
    private FileIOPermission m_fiop;
    private string m_RootDir;
    private string m_InfoFile;
    private string m_SyncObjectName;
    [SecurityCritical]
    private SafeIsolatedStorageFileHandle m_handle;
    private bool m_closed;
    private bool m_bDisposed;
    private IsolatedStorageScope m_StoreScope;

    internal IsolatedStorageFile()
    {
    }

    /// <summary>
    ///   Получает изолированное хранение с областью действия пользователя, соответствующее удостоверению домена приложения и удостоверения сборки.
    /// </summary>
    /// <returns>
    ///   Объект, соответствующий <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />, основываясь на комбинации удостоверения домена приложения и удостоверения сборки.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Хранилище не удалось открыть.
    /// 
    ///   -или-
    /// 
    ///   Указанная сборка не имеет достаточно разрешений для создания изолированных хранилищ.
    /// 
    ///   -или-
    /// 
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// 
    ///   -или-
    /// 
    ///   Не удается определить разрешения для домена приложения.
    /// </exception>
    public static IsolatedStorageFile GetUserStoreForDomain()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, (Type) null, (Type) null);
    }

    /// <summary>
    ///   Получает изолированное хранение с областью действия пользователя, соответствующее удостоверения сборки вызывающего кода.
    /// </summary>
    /// <returns>
    ///   Объект, соответствующий области изолированного хранилища на основе удостоверения сборки вызывающего кода.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// 
    ///   -или-
    /// 
    ///   Не удается определить разрешения для вызывающей сборки.
    /// </exception>
    public static IsolatedStorageFile GetUserStoreForAssembly()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, (Type) null, (Type) null);
    }

    /// <summary>
    ///   Получает изолированное хранение с областью действия пользователя, соответствующее удостоверения приложения вызывающего кода.
    /// </summary>
    /// <returns>
    ///   Объект, соответствующий области изолированного хранилища на основе удостоверения сборки вызывающего кода.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// 
    ///   -или-
    /// 
    ///   Невозможно определить удостоверение приложения вызывающего объекта, поскольку <see cref="P:System.AppDomain.ActivationContext" /> Свойства, возвращаемого <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Не удается определить разрешения для домена приложения.
    /// </exception>
    public static IsolatedStorageFile GetUserStoreForApplication()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Application, (Type) null);
    }

    /// <summary>
    ///   Получает пользователя изолированное хранилище для использования приложениями в домене виртуального узла.
    /// </summary>
    /// <returns>
    ///   В файл изолированного хранилища, соответствующий области изолированного хранилища на основе удостоверения приложения вызывающего кода.
    /// </returns>
    [ComVisible(false)]
    public static IsolatedStorageFile GetUserStoreForSite()
    {
      throw new NotSupportedException(Environment.GetResourceString("IsolatedStorage_NotValidOnDesktop"));
    }

    /// <summary>
    ///   Получает изолированное хранение с областью действия компьютера, соответствующее удостоверению домена приложения и сборки.
    /// </summary>
    /// <returns>
    ///   Объект, соответствующий <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />, основываясь на комбинации удостоверения домена приложения и удостоверения сборки.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Хранилище не удалось открыть.
    /// 
    ///   -или-
    /// 
    ///   Указанная сборка не имеет достаточно разрешений для создания изолированных хранилищ.
    /// 
    ///   -или-
    /// 
    ///   Не удается определить разрешения для домена приложения.
    /// 
    ///   -или-
    /// 
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// </exception>
    public static IsolatedStorageFile GetMachineStoreForDomain()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine, (Type) null, (Type) null);
    }

    /// <summary>
    ///   Получает изолированное хранение с областью действия компьютера, соответствующее удостоверения сборки вызывающего кода.
    /// </summary>
    /// <returns>
    ///   Объект, соответствующий области изолированного хранилища на основе удостоверения сборки вызывающего кода.
    /// </returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    public static IsolatedStorageFile GetMachineStoreForAssembly()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine, (Type) null, (Type) null);
    }

    /// <summary>
    ///   Получает изолированное хранение с областью действия компьютера, соответствующее удостоверения приложения вызывающего кода.
    /// </summary>
    /// <returns>
    ///   Объект, соответствующий области изолированного хранилища на основе удостоверения приложения вызывающего кода.
    /// </returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Не удалось определить удостоверение приложения вызывающего объекта.
    /// 
    ///   -или-
    /// 
    ///   Не удалось определить набор предоставленных разрешений для домена приложения.
    /// 
    ///   -или-
    /// 
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    public static IsolatedStorageFile GetMachineStoreForApplication()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.Machine | IsolatedStorageScope.Application, (Type) null);
    }

    /// <summary>
    ///   Возвращает изолированное хранение, соответствующее области изолированного хранения, предоставляющей типы свидетельства домена и сборки приложения.
    /// </summary>
    /// <param name="scope">
    ///   Побитовое сочетание значений перечисления.
    /// </param>
    /// <param name="domainEvidenceType">
    ///   Тип <see cref="T:System.Security.Policy.Evidence" /> вы можете выбрать из списка <see cref="T:System.Security.Policy.Evidence" /> присутствующего в домене вызывающего приложения.
    ///   <see langword="null" /> позволяет <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> выберите объект свидетельства.
    /// </param>
    /// <param name="assemblyEvidenceType">
    ///   Тип <see cref="T:System.Security.Policy.Evidence" /> вы можете выбрать из списка <see cref="T:System.Security.Policy.Evidence" /> присутствующего в домене вызывающего приложения.
    ///   <see langword="null" /> позволяет <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> выберите объект свидетельства.
    /// </param>
    /// <returns>Объект, представляющий параметры.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="scope" /> Является недопустимым.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Предоставленный тип свидетельства отсутствует в списке свидетельства сборки.
    /// 
    ///   -или-
    /// 
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение перечисления <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, но не удается определить удостоверение приложения вызывающего объекта, так как <see cref="P:System.AppDomain.ActivationContext" /> для текущего домена приложения возвращается <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, но не удается определить разрешения для домена приложения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, но не удается определить разрешения для вызывающей сборки.
    /// </exception>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
    {
      if (domainEvidenceType != (Type) null)
        IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      isolatedStorageFile.InitStore(scope, domainEvidenceType, assemblyEvidenceType);
      isolatedStorageFile.Init(scope);
      return isolatedStorageFile;
    }

    internal void EnsureStoreIsValid()
    {
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
    }

    /// <summary>
    ///   Получает изолированное хранение, соответствующее объектам свидетельств сборки и домена приложения.
    /// </summary>
    /// <param name="scope">
    ///   Побитовое сочетание значений перечисления.
    /// </param>
    /// <param name="domainIdentity">
    ///   Объект, содержащий свидетельство для удостоверения домена приложения.
    /// </param>
    /// <param name="assemblyIdentity">
    ///   Объект, содержащий свидетельство для удостоверения сборки кода.
    /// </param>
    /// <returns>Объект, представляющий параметры.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Ни <paramref name="domainIdentity" /> ни <paramref name="assemblyIdentity" /> была передана в.
    ///    Это подтверждает, что используется правильный конструктор.
    /// 
    ///   -или-
    /// 
    ///   Либо <paramref name="domainIdentity" />, либо <paramref name="assemblyIdentity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="scope" /> Является недопустимым.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение перечисления <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, но не удается определить удостоверение приложения вызывающего объекта, так как <see cref="P:System.AppDomain.ActivationContext" /> для текущего домена приложения возвращается <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, но не удается определить разрешения для домена приложения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, но не удается определить разрешения для вызывающей сборки.
    /// </exception>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object domainIdentity, object assemblyIdentity)
    {
      if (assemblyIdentity == null)
        throw new ArgumentNullException(nameof (assemblyIdentity));
      if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope) && domainIdentity == null)
        throw new ArgumentNullException(nameof (domainIdentity));
      IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      isolatedStorageFile.InitStore(scope, domainIdentity, assemblyIdentity, (object) null);
      isolatedStorageFile.Init(scope);
      return isolatedStorageFile;
    }

    /// <summary>
    ///   Возвращает изолированное хранение, соответствующее указанный домен приложения и объектов свидетельств сборки и типы.
    /// </summary>
    /// <param name="scope">
    ///   Побитовое сочетание значений перечисления.
    /// </param>
    /// <param name="domainEvidence">
    ///   Объект, содержащий идентификатор домена приложения.
    /// </param>
    /// <param name="domainEvidenceType">
    ///   Тип удостоверения для выбора из свидетельства домена приложения.
    /// </param>
    /// <param name="assemblyEvidence">
    ///   Объект, содержащий идентификатор сборки кода.
    /// </param>
    /// <param name="assemblyEvidenceType">
    ///   Тип удостоверения для выбора из свидетельства сборки кода приложения.
    /// </param>
    /// <returns>Объект, представляющий параметры.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="domainEvidence" /> Или <paramref name="assemblyEvidence" /> identity не было передано в.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="scope" /> Является недопустимым.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение перечисления <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, но не удается определить удостоверение приложения вызывающего объекта, так как <see cref="P:System.AppDomain.ActivationContext" /> для текущего домена приложения возвращается <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, но не удается определить разрешения для домена приложения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, но не удается определить разрешения для вызывающей сборки.
    /// </exception>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Evidence domainEvidence, Type domainEvidenceType, Evidence assemblyEvidence, Type assemblyEvidenceType)
    {
      if (assemblyEvidence == null)
        throw new ArgumentNullException(nameof (assemblyEvidence));
      if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope) && domainEvidence == null)
        throw new ArgumentNullException(nameof (domainEvidence));
      IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      isolatedStorageFile.InitStore(scope, domainEvidence, domainEvidenceType, assemblyEvidence, assemblyEvidenceType, (Evidence) null, (Type) null);
      isolatedStorageFile.Init(scope);
      return isolatedStorageFile;
    }

    /// <summary>
    ///   Возвращает изолированное хранение, соответствующее области изоляции и объекту удостоверения приложения.
    /// </summary>
    /// <param name="scope">
    ///   Побитовое сочетание значений перечисления.
    /// </param>
    /// <param name="applicationEvidenceType">
    ///   Объект, содержащий идентификатор приложения.
    /// </param>
    /// <returns>Объект, представляющий параметры.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="applicationEvidence" />  Identity не было передано в.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="scope" /> Является недопустимым.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение перечисления <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, но не удается определить удостоверение приложения вызывающего объекта, так как <see cref="P:System.AppDomain.ActivationContext" /> для текущего домена приложения возвращается <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, но не удается определить разрешения для домена приложения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, но не удается определить разрешения для вызывающей сборки.
    /// </exception>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type applicationEvidenceType)
    {
      if (applicationEvidenceType != (Type) null)
        IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      isolatedStorageFile.InitStore(scope, applicationEvidenceType);
      isolatedStorageFile.Init(scope);
      return isolatedStorageFile;
    }

    /// <summary>
    ///   Возвращает изолированное хранение, соответствующее данному удостоверению приложения.
    /// </summary>
    /// <param name="scope">
    ///   Побитовое сочетание значений перечисления.
    /// </param>
    /// <param name="applicationIdentity">
    ///   Объект, содержащий свидетельство для удостоверения приложения.
    /// </param>
    /// <returns>Объект, представляющий параметры.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не предоставлены достаточные разрешения для изолированного хранилища.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="applicationIdentity" /> Identity не было передано в.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="scope" /> Является недопустимым.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Расположение изолированного хранилища не может инициализироваться.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение перечисления <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, но не удается определить удостоверение приложения вызывающего объекта, так как <see cref="P:System.AppDomain.ActivationContext" /> для текущего домена приложения возвращается <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, но не удается определить разрешения для домена приложения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="scope" /> содержит значение <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, но не удается определить разрешения для вызывающей сборки.
    /// </exception>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object applicationIdentity)
    {
      if (applicationIdentity == null)
        throw new ArgumentNullException(nameof (applicationIdentity));
      IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      isolatedStorageFile.InitStore(scope, (object) null, (object) null, applicationIdentity);
      isolatedStorageFile.Init(scope);
      return isolatedStorageFile;
    }

    /// <summary>
    ///   Возвращает значение, представляющее объем пространства, используемого для изолированного хранилища.
    /// </summary>
    /// <returns>
    ///   Пространство используется изолированного хранилища в байтах.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище было закрыто.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    public override long UsedSize
    {
      [SecuritySafeCritical] get
      {
        if (this.IsRoaming())
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined"));
        lock (this.m_internalLock)
        {
          if (this.m_bDisposed)
            throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.m_closed)
            throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.InvalidFileHandle)
            this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
          return (long) IsolatedStorageFile.GetUsage(this.m_handle);
        }
      }
    }

    /// <summary>Возвращает текущий размер изолированного хранилища.</summary>
    /// <returns>
    ///   Общее число байтов хранилища, в настоящее время использовать в области изолированного хранилища.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство недоступно.
    ///    Текущее хранилище имеет перемещаемую область действия или не открыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Размер текущего объекта не определено.
    /// </exception>
    [CLSCompliant(false)]
    [Obsolete("IsolatedStorageFile.CurrentSize has been deprecated because it is not CLS Compliant.  To get the current size use IsolatedStorageFile.UsedSize")]
    public override ulong CurrentSize
    {
      [SecuritySafeCritical] get
      {
        if (this.IsRoaming())
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined"));
        lock (this.m_internalLock)
        {
          if (this.m_bDisposed)
            throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.m_closed)
            throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.InvalidFileHandle)
            this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
          return IsolatedStorageFile.GetUsage(this.m_handle);
        }
      }
    }

    /// <summary>
    ///   Возвращает значение, показывающее объем свободного пространства, доступного для изолированного хранилища.
    /// </summary>
    /// <returns>
    ///   Свободного пространства, доступного для изолированного хранилища, в байтах.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище закрыто.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    [ComVisible(false)]
    public override long AvailableFreeSpace
    {
      [SecuritySafeCritical] get
      {
        if (this.IsRoaming())
          return long.MaxValue;
        long usage;
        lock (this.m_internalLock)
        {
          if (this.m_bDisposed)
            throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.m_closed)
            throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.InvalidFileHandle)
            this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
          usage = (long) IsolatedStorageFile.GetUsage(this.m_handle);
        }
        return this.Quota - usage;
      }
    }

    /// <summary>
    ///   Возвращает значение, представляющее максимальный объем пространства, доступного для изолированного хранилища.
    /// </summary>
    /// <returns>
    ///   Ограничение объема изолированного хранилища в байтах.
    /// </returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    [ComVisible(false)]
    public override long Quota
    {
      get
      {
        if (this.IsRoaming())
          return long.MaxValue;
        return base.Quota;
      }
      [SecuritySafeCritical] internal set
      {
        bool locked = false;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this.Lock(ref locked);
          lock (this.m_internalLock)
          {
            if (this.InvalidFileHandle)
              this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
            IsolatedStorageFile.SetQuota(this.m_handle, value);
          }
        }
        finally
        {
          if (locked)
            this.Unlock();
        }
        base.Quota = value;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, включено ли изолированное хранилище.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" /> во всех случаях.
    /// </returns>
    [ComVisible(false)]
    public static bool IsEnabled
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Получает значение, представляющее максимальный объем пространства, доступного для изолированного хранения в пределах, установленных квотой.
    /// </summary>
    /// <returns>
    ///   Ограничение объема изолированного хранилища в байтах.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство недоступно.
    ///   <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFile.MaximumSize" /> Невозможно определить без свидетельства создания сборки.
    ///    Не удалось определить свидетельство, при создании объекта.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Произошла ошибка изолированного хранилища.
    /// </exception>
    [CLSCompliant(false)]
    [Obsolete("IsolatedStorageFile.MaximumSize has been deprecated because it is not CLS Compliant.  To get the maximum size use IsolatedStorageFile.Quota")]
    public override ulong MaximumSize
    {
      get
      {
        if (this.IsRoaming())
          return (ulong) long.MaxValue;
        return base.MaximumSize;
      }
    }

    /// <summary>
    ///   Позволяет приложению явным образом запросить больший размер квоты в байтах.
    /// </summary>
    /// <param name="newQuotaSize">Запрошенный размер в байтах.</param>
    /// <returns>
    ///   <see langword="true" /> Если новая квота принята; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="newQuotaSize" /> меньше, чем текущий размер квоты.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="newQuotaSize" /> меньше нуля, или меньше или равно текущему размеру квоты.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище было закрыто.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущая область не предназначена для пользователя приложения.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public override bool IncreaseQuotaTo(long newQuotaSize)
    {
      if (newQuotaSize <= this.Quota)
        throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_OldQuotaLarger"));
      if (this.m_StoreScope != (IsolatedStorageScope.User | IsolatedStorageScope.Application))
        throw new NotSupportedException(Environment.GetResourceString("IsolatedStorage_OnlyIncreaseUserApplicationStore"));
      IsolatedStorageSecurityState quotaForApplication = IsolatedStorageSecurityState.CreateStateToIncreaseQuotaForApplication(newQuotaSize, this.Quota - this.AvailableFreeSpace);
      try
      {
        quotaForApplication.EnsureState();
      }
      catch (IsolatedStorageException ex)
      {
        return false;
      }
      this.Quota = newQuotaSize;
      return true;
    }

    [SecuritySafeCritical]
    internal void Reserve(ulong lReserve)
    {
      if (this.IsRoaming())
        return;
      ulong quota = (ulong) this.Quota;
      ulong plReserve = lReserve;
      lock (this.m_internalLock)
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.m_closed)
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.InvalidFileHandle)
          this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
        IsolatedStorageFile.Reserve(this.m_handle, quota, plReserve, false);
      }
    }

    internal void Unreserve(ulong lFree)
    {
      if (this.IsRoaming())
        return;
      ulong quota = (ulong) this.Quota;
      this.Unreserve(lFree, quota);
    }

    [SecuritySafeCritical]
    internal void Unreserve(ulong lFree, ulong quota)
    {
      if (this.IsRoaming())
        return;
      ulong plReserve = lFree;
      lock (this.m_internalLock)
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.m_closed)
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.InvalidFileHandle)
          this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
        IsolatedStorageFile.Reserve(this.m_handle, quota, plReserve, true);
      }
    }

    /// <summary>Удаляет файл в области изолированного хранилища.</summary>
    /// <param name="file">
    ///   Относительный путь файла для удаления из области изолированного хранилища.
    /// </param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Конечный файл открыт или путь неверен.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Путь к файлу <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void DeleteFile(string file)
    {
      if (file == null)
        throw new ArgumentNullException(nameof (file));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        long length;
        try
        {
          string fullPath = this.GetFullPath(file);
          length = LongPathFile.GetLength(fullPath);
          LongPathFile.Delete(fullPath);
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteFile"));
        }
        this.Unreserve(IsolatedStorageFile.RoundToBlockSize((ulong) length));
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
      CodeAccessPermission.RevertAll();
    }

    /// <summary>
    ///   Определяет, ссылается ли заданный путь на существующий файл в изолированном хранилище.
    /// </summary>
    /// <param name="path">Путь и имя файла для тестирования.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="path" /> ссылается на существующий файл в изолированном хранилище и не <see langword="null" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public bool FileExists(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string path1 = LongPath.NormalizePath(this.GetFullPath(path));
      if (path.EndsWith(Path.DirectorySeparatorChar.ToString() + ".", StringComparison.Ordinal))
        path1 = !path1.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) ? path1 + Path.DirectorySeparatorChar.ToString() + "." : path1 + ".";
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          path1
        }, false, false));
      }
      catch
      {
        return false;
      }
      bool flag = LongPathFile.Exists(path1);
      CodeAccessPermission.RevertAll();
      return flag;
    }

    /// <summary>
    ///   Определяет, ссылается ли заданный путь на существующий каталог в изолированном хранилище.
    /// </summary>
    /// <param name="path">Проверяемый путь.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="path" /> ссылается на существующий каталог в изолированном хранилище и не <see langword="null" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public bool DirectoryExists(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string fullPath = this.GetFullPath(path);
      string path1 = LongPath.NormalizePath(fullPath);
      if (fullPath.EndsWith(Path.DirectorySeparatorChar.ToString() + ".", StringComparison.Ordinal))
        path1 = !path1.EndsWith(Path.DirectorySeparatorChar) ? path1 + Path.DirectorySeparatorChar.ToString() + "." : path1 + ".";
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          path1
        }, false, false));
      }
      catch
      {
        return false;
      }
      bool flag = LongPathDirectory.Exists(path1);
      CodeAccessPermission.RevertAll();
      return flag;
    }

    /// <summary>Создает каталог в области изолированного хранилища.</summary>
    /// <param name="dir">
    ///   Относительный путь к папке для создания в области изолированного хранилища.
    /// </param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Текущий код не имеет достаточно разрешений для создания папки изолированного хранения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Путь к папке — <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void CreateDirectory(string dir)
    {
      if (dir == null)
        throw new ArgumentNullException(nameof (dir));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string fullPath1 = this.GetFullPath(dir);
      string fullPath2 = LongPath.NormalizePath(fullPath1);
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          fullPath2
        }, false, false));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_CreateDirectory"));
      }
      string[] create = this.DirectoriesToCreate(fullPath2);
      if (create == null || create.Length == 0)
      {
        if (!LongPathDirectory.Exists(fullPath1))
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_CreateDirectory"));
      }
      else
      {
        this.Reserve(1024UL * (ulong) create.Length);
        try
        {
          LongPathDirectory.CreateDirectory(create[create.Length - 1]);
        }
        catch
        {
          this.Unreserve(1024UL * (ulong) create.Length);
          try
          {
            LongPathDirectory.Delete(create[0], true);
          }
          catch
          {
          }
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_CreateDirectory"));
        }
        CodeAccessPermission.RevertAll();
      }
    }

    /// <summary>
    ///   Возвращает Дата и время создания указанного файла или каталога.
    /// </summary>
    /// <param name="path">
    ///   Путь к файлу или каталогу, для которых требуется получить сведения о дате и времени создания.
    /// </param>
    /// <returns>
    ///   Дата создания и время для заданного файла или каталога.
    ///    Значение представляется в формате местного времени.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " />представляет собой строку нулевой длины, содержит только пробелы или содержит один или несколько недопустимых символов, определяемых <see cref="M:System.IO.Path.GetInvalidPathChars" /> метод.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path " /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище было закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public DateTimeOffset GetCreationTime(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (path));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string path1 = LongPath.NormalizePath(this.GetFullPath(path));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          path1
        }, false, false));
      }
      catch
      {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero);
        dateTimeOffset = dateTimeOffset.ToLocalTime();
        return dateTimeOffset;
      }
      DateTimeOffset creationTime = LongPathFile.GetCreationTime(path1);
      CodeAccessPermission.RevertAll();
      return creationTime;
    }

    /// <summary>
    ///   Возвращает дату и время указанного файла или каталога последнего обращения к.
    /// </summary>
    /// <param name="path">
    ///   Путь к файлу или каталогу, для которых требуется получить сведения о дате и времени последнего доступа.
    /// </param>
    /// <returns>
    ///   Дата и время последнего обращения к указанному файлу или каталогу.
    ///    Значение представляется в формате местного времени.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " />представляет собой строку нулевой длины, содержит только пробелы или содержит один или несколько недопустимых символов, определяемых <see cref="M:System.IO.Path.GetInvalidPathChars" /> метод.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path " /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище было закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public DateTimeOffset GetLastAccessTime(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (path));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string path1 = LongPath.NormalizePath(this.GetFullPath(path));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          path1
        }, false, false));
      }
      catch
      {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero);
        dateTimeOffset = dateTimeOffset.ToLocalTime();
        return dateTimeOffset;
      }
      DateTimeOffset lastAccessTime = LongPathFile.GetLastAccessTime(path1);
      CodeAccessPermission.RevertAll();
      return lastAccessTime;
    }

    /// <summary>
    ///   Возвращает дату и время указанного файла или последней записи в каталог.
    /// </summary>
    /// <param name="path">
    ///   Путь к файлу или каталогу, для которых требуется получить сведения о дате и времени последней записи.
    /// </param>
    /// <returns>
    ///   Дата и время последней операции записи в указанный файл или каталог.
    ///    Значение представляется в формате местного времени.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path " />представляет собой строку нулевой длины, содержит только пробелы или содержит один или несколько недопустимых символов, определяемых <see cref="M:System.IO.Path.GetInvalidPathChars" /> метод.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path " /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище было закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public DateTimeOffset GetLastWriteTime(string path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (path.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (path));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string path1 = LongPath.NormalizePath(this.GetFullPath(path));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          path1
        }, false, false));
      }
      catch
      {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero);
        dateTimeOffset = dateTimeOffset.ToLocalTime();
        return dateTimeOffset;
      }
      DateTimeOffset lastWriteTime = LongPathFile.GetLastWriteTime(path1);
      CodeAccessPermission.RevertAll();
      return lastWriteTime;
    }

    /// <summary>Копирует существующий файл в новый файл.</summary>
    /// <param name="sourceFileName">Имя файла для копирования.</param>
    /// <param name="destinationFileName">
    ///   Имя конечного файла.
    ///    Это не может быть имя каталога или имя существующего файла.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="sourceFileName " />или<paramref name=" destinationFileName " />является строкой нулевой длины, содержит только пробелы или содержит один или несколько недопустимых символов, определяемых <see cref="M:System.IO.Path.GetInvalidPathChars" /> метод.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="sourceFileName " />или<paramref name=" destinationFileName " />— <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище было закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="sourceFileName " />не найден.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   <paramref name="sourceFileName " />не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="destinationFileName" /> существует.
    /// 
    ///   -или-
    /// 
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    [ComVisible(false)]
    public void CopyFile(string sourceFileName, string destinationFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName));
      if (destinationFileName == null)
        throw new ArgumentNullException(nameof (destinationFileName));
      if (sourceFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (sourceFileName));
      if (destinationFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (destinationFileName));
      this.CopyFile(sourceFileName, destinationFileName, false);
    }

    /// <summary>
    ///   Копирует существующий файл в новый файл и при необходимости перезаписывает существующий файл.
    /// </summary>
    /// <param name="sourceFileName">Имя файла для копирования.</param>
    /// <param name="destinationFileName">
    ///   Имя конечного файла.
    ///    Это не может быть имя каталога.
    /// </param>
    /// <param name="overwrite">
    ///   <see langword="true" />, если конечный файл можно перезаписать; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="sourceFileName " />или<paramref name=" destinationFileName " />является строкой нулевой длины, содержит только пробелы или содержит один или несколько недопустимых символов, определяемых <see cref="M:System.IO.Path.GetInvalidPathChars" /> метод.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="sourceFileName " />или<paramref name=" destinationFileName " />— <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище было закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="sourceFileName " />не найден.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   <paramref name="sourceFileName " />не найден.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// 
    ///   -или-
    /// 
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName));
      if (destinationFileName == null)
        throw new ArgumentNullException(nameof (destinationFileName));
      if (sourceFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (sourceFileName));
      if (destinationFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (destinationFileName));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string str1 = LongPath.NormalizePath(this.GetFullPath(sourceFileName));
      string str2 = LongPath.NormalizePath(this.GetFullPath(destinationFileName));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]
        {
          str1
        }, false, false));
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
        {
          str2
        }, false, false));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        long length;
        try
        {
          length = LongPathFile.GetLength(str1);
        }
        catch (FileNotFoundException ex)
        {
          throw new FileNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceFileName));
        }
        catch (DirectoryNotFoundException ex)
        {
          throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceFileName));
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
        }
        long num = 0;
        if (LongPathFile.Exists(str2))
        {
          try
          {
            num = LongPathFile.GetLength(str2);
          }
          catch
          {
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
          }
        }
        if (num < length)
          this.Reserve(IsolatedStorageFile.RoundToBlockSize((ulong) (length - num)));
        try
        {
          LongPathFile.Copy(str1, str2, overwrite);
        }
        catch (FileNotFoundException ex)
        {
          if (num < length)
            this.Unreserve(IsolatedStorageFile.RoundToBlockSize((ulong) (length - num)));
          throw new FileNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceFileName));
        }
        catch
        {
          if (num < length)
            this.Unreserve(IsolatedStorageFile.RoundToBlockSize((ulong) (length - num)));
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
        }
        if (!(num > length & overwrite))
          return;
        this.Unreserve(IsolatedStorageFile.RoundToBlockSizeFloor((ulong) (num - length)));
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
    }

    /// <summary>
    ///   Перемещает заданный файл в новое расположение и позволяет указать новое имя файла.
    /// </summary>
    /// <param name="sourceFileName">Имя перемещаемого файла.</param>
    /// <param name="destinationFileName">
    ///   Путь к новому местоположению файла.
    ///    Если имя файла, перемещенный файл будет иметь это имя.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="sourceFileName " />или<paramref name=" destinationFileName " />является строкой нулевой длины, содержит только пробелы или содержит один или несколько недопустимых символов, определяемых <see cref="M:System.IO.Path.GetInvalidPathChars" /> метод.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="sourceFileName " />или<paramref name=" destinationFileName " />— <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище было закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="sourceFileName" />.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void MoveFile(string sourceFileName, string destinationFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName));
      if (destinationFileName == null)
        throw new ArgumentNullException(nameof (destinationFileName));
      if (sourceFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (sourceFileName));
      if (destinationFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (destinationFileName));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string sourceFileName1 = LongPath.NormalizePath(this.GetFullPath(sourceFileName));
      string destFileName = LongPath.NormalizePath(this.GetFullPath(destinationFileName));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]
        {
          sourceFileName1
        }, false, false));
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
        {
          destFileName
        }, false, false));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      try
      {
        LongPathFile.Move(sourceFileName1, destFileName);
      }
      catch (FileNotFoundException ex)
      {
        throw new FileNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceFileName));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      CodeAccessPermission.RevertAll();
    }

    /// <summary>
    ///   Перемещает указанный каталог и его содержимое в новое расположение.
    /// </summary>
    /// <param name="sourceDirectoryName">Имя каталога.</param>
    /// <param name="destinationDirectoryName">
    ///   Путь к новому местоположению <paramref name="sourceDirectoryName" />.
    ///    Это не может быть путь к существующей папке.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="sourceFileName " />или<paramref name=" destinationFileName " />является строкой нулевой длины, содержит только пробелы или содержит один или несколько недопустимых символов, определяемых <see cref="M:System.IO.Path.GetInvalidPathChars" /> метод.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="sourceFileName " />или<paramref name=" destinationFileName " />— <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище было закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   <paramref name="sourceDirectoryName" /> не существует.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="destinationDirectoryName" /> уже существует.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="sourceDirectoryName" /> и <paramref name="destinationDirectoryName" /> см. в тот же каталог.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
    {
      if (sourceDirectoryName == null)
        throw new ArgumentNullException(nameof (sourceDirectoryName));
      if (destinationDirectoryName == null)
        throw new ArgumentNullException(nameof (destinationDirectoryName));
      if (sourceDirectoryName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (sourceDirectoryName));
      if (destinationDirectoryName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), nameof (destinationDirectoryName));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string sourceDirName = LongPath.NormalizePath(this.GetFullPath(sourceDirectoryName));
      string destDirName = LongPath.NormalizePath(this.GetFullPath(destinationDirectoryName));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]
        {
          sourceDirName
        }, false, false));
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
        {
          destDirName
        }, false, false));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      try
      {
        LongPathDirectory.Move(sourceDirName, destDirName);
      }
      catch (DirectoryNotFoundException ex)
      {
        throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceDirectoryName));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      CodeAccessPermission.RevertAll();
    }

    [SecurityCritical]
    private string[] DirectoriesToCreate(string fullPath)
    {
      List<string> stringList = new List<string>();
      int length = fullPath.Length;
      if (length >= 2 && (int) fullPath[length - 1] == (int) this.SeparatorExternal)
        --length;
      int rootLength = LongPath.GetRootLength(fullPath);
      while (rootLength < length)
      {
        ++rootLength;
        while (rootLength < length && (int) fullPath[rootLength] != (int) this.SeparatorExternal)
          ++rootLength;
        string path = fullPath.Substring(0, rootLength);
        if (!LongPathDirectory.InternalExists(path))
          stringList.Add(path);
      }
      if (stringList.Count != 0)
        return stringList.ToArray();
      return (string[]) null;
    }

    /// <summary>Удаляет каталог в области изолированного хранилища.</summary>
    /// <param name="dir">
    ///   Относительный путь к каталогу для удаления из области изолированного хранения.
    /// </param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Не удалось удалить папку.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Путь к папке — <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void DeleteDirectory(string dir)
    {
      if (dir == null)
        throw new ArgumentNullException(nameof (dir));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        try
        {
          string path = LongPath.NormalizePath(this.GetFullPath(dir));
          if (path.Equals(LongPath.NormalizePath(this.GetFullPath(".")), StringComparison.Ordinal))
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectory"));
          LongPathDirectory.Delete(path, false);
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectory"));
        }
        this.Unreserve(1024UL);
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
      CodeAccessPermission.RevertAll();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Demand(CodeAccessPermission permission)
    {
      permission.Demand();
    }

    /// <summary>
    ///   Перечисляет имена файлов в корне изолированного хранилища.
    /// </summary>
    /// <returns>
    ///   Массив относительных путей файлов в корневой папке изолированного хранилища.
    ///     Массив нулевой длины указывает, что нет файлов в корне.
    /// </returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Не удается определить пути к файлам из корня изолированного хранилища.
    /// </exception>
    [ComVisible(false)]
    public string[] GetFileNames()
    {
      return this.GetFileNames("*");
    }

    /// <summary>
    ///   Возвращает имена файлов, соответствующих шаблону поиска.
    /// </summary>
    /// <param name="searchPattern">
    ///   Шаблон поиска.
    ///    Как одиночные («?»)
    ///    и нескольких символов («*»), поддерживаются подстановочные знаки.
    /// </param>
    /// <returns>
    ///   Массив относительных путей файлов в изолированном хранилище область, соответствующие <paramref name="searchPattern" />.
    ///    Массив нулевой длины указывает на отсутствие соответствующих файлов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Путь к файлу, определяемое <paramref name="searchPattern" /> не найден.
    /// </exception>
    [SecuritySafeCritical]
    public string[] GetFileNames(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string[] fileDirectoryNames = IsolatedStorageFile.GetFileDirectoryNames(this.GetFullPath(searchPattern), searchPattern, true);
      CodeAccessPermission.RevertAll();
      return fileDirectoryNames;
    }

    /// <summary>
    ///   Перечисляет каталоги в корне изолированного хранилища.
    /// </summary>
    /// <returns>
    ///   Массив относительных путей к каталогам в корневой папке изолированного хранилища.
    ///    Массив нулевой длины указывает, что существуют каталоги в корне.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище закрыто.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Вызывающий объект не имеет разрешения на перечисление каталогов.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Не найдены один или несколько каталогов.
    /// </exception>
    [ComVisible(false)]
    public string[] GetDirectoryNames()
    {
      return this.GetDirectoryNames("*");
    }

    /// <summary>
    ///   Перечисляет каталоги в области изолированного хранилища, соответствующие заданному шаблону поиска.
    /// </summary>
    /// <param name="searchPattern">
    ///   Шаблон поиска.
    ///    Как одиночные («?»)
    ///    и нескольких символов («*»), поддерживаются подстановочные знаки.
    /// </param>
    /// <returns>
    ///   Массив относительных путей каталогов в изолированном хранилище область, соответствующие <paramref name="searchPattern" />.
    ///    Массив нулевой длины указывает на отсутствие соответствующих папок.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="searchPattern" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Изолированное хранилище закрыто.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Вызывающий объект не имеет разрешения на перечисление каталогов, разрешенных из <paramref name="searchPattern" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог или каталоги, заданные параметром <paramref name="searchPattern" /> не найдены.
    /// </exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// </exception>
    [SecuritySafeCritical]
    public string[] GetDirectoryNames(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException(nameof (searchPattern));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string[] fileDirectoryNames = IsolatedStorageFile.GetFileDirectoryNames(this.GetFullPath(searchPattern), searchPattern, false);
      CodeAccessPermission.RevertAll();
      return fileDirectoryNames;
    }

    private static string NormalizeSearchPattern(string searchPattern)
    {
      string searchPattern1 = searchPattern.TrimEnd(Path.TrimEndChars);
      Path.CheckSearchPattern(searchPattern1);
      return searchPattern1;
    }

    /// <summary>Открывает файл в заданном режиме.</summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений перечисления, задающее способ открытия файла.
    /// </param>
    /// <returns>
    ///   Файл, открытый в заданном режиме с доступом для чтения и записи и без предоставления общего доступа.
    /// </returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> Неправильный формат.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог в <paramref name="path" /> не существует.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    [ComVisible(false)]
    public IsolatedStorageFileStream OpenFile(string path, FileMode mode)
    {
      return new IsolatedStorageFileStream(path, mode, this);
    }

    /// <summary>
    ///   Открывает файл в заданном режиме с указанным чтения и записи.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений перечисления, задающее способ открытия файла.
    /// </param>
    /// <param name="access">
    ///   Одно из значений перечисления, которое указывает, будет ли файл открыт с чтения, записи или чтения и записи.
    /// </param>
    /// <returns>
    ///   Файл, открытый в заданном режиме доступа и без предоставления общего доступа.
    /// </returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> Неправильный формат.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог в <paramref name="path" /> не существует.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="F:System.IO.FileMode.Open" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    [ComVisible(false)]
    public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access)
    {
      return new IsolatedStorageFileStream(path, mode, access, this);
    }

    /// <summary>
    ///   Открывает файл в заданном режиме с указанным чтение и запись и совместное использование.
    /// </summary>
    /// <param name="path">
    ///   Относительный путь файла в изолированном хранилище.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений перечисления, задающее способ открытия или создания файла.
    /// </param>
    /// <param name="access">
    ///   Одно из значений перечисления, которое указывает, будет ли файл открыт с чтения, записи или чтения и записи
    /// </param>
    /// <param name="share">
    ///   Побитовое сочетание значений перечисления, задающее тип доступа других <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />   объекты имеют к этому файлу.
    /// </param>
    /// <returns>
    ///   Файл, который открыт в заданном режиме доступа и с указанными параметрами общего доступа.
    /// </returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> Неправильный формат.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог в <paramref name="path" /> не существует.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден, и <paramref name="mode" /> имеет значение <see cref="M:System.IO.FileInfo.Open(System.IO.FileMode)" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    [ComVisible(false)]
    public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
    {
      return new IsolatedStorageFileStream(path, mode, access, share, this);
    }

    /// <summary>Создает файл в изолированном хранилище.</summary>
    /// <param name="path">Относительный путь файла.</param>
    /// <returns>Новый файл изолированного хранилища.</returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Изолированное хранилище было удалено.
    /// 
    ///   -или-
    /// 
    ///   Изолированное хранилище отключено.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="path" /> Неправильный формат.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///   Каталог в <paramref name="path" /> не существует.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Изолированное хранилище был удален.
    /// </exception>
    [ComVisible(false)]
    public IsolatedStorageFileStream CreateFile(string path)
    {
      return new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, this);
    }

    /// <summary>
    ///   Удаление области изолированного хранилища и все ее содержимое.
    /// </summary>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Невозможно удалить изолированное хранилище.
    /// </exception>
    [SecuritySafeCritical]
    public override void Remove()
    {
      string str1 = (string) null;
      this.RemoveLogicalDir();
      this.Close();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(IsolatedStorageFile.GetRootDir(this.Scope));
      if (this.IsApp())
      {
        stringBuilder.Append(this.AppName);
        stringBuilder.Append(this.SeparatorExternal);
      }
      else
      {
        if (this.IsDomain())
        {
          stringBuilder.Append(this.DomainName);
          stringBuilder.Append(this.SeparatorExternal);
          str1 = stringBuilder.ToString();
        }
        stringBuilder.Append(this.AssemName);
        stringBuilder.Append(this.SeparatorExternal);
      }
      string str2 = stringBuilder.ToString();
      new FileIOPermission(FileIOPermissionAccess.AllAccess, str2).Assert();
      if (this.ContainsUnknownFiles(str2))
        return;
      try
      {
        LongPathDirectory.Delete(str2, true);
      }
      catch
      {
        return;
      }
      if (!this.IsDomain())
        return;
      CodeAccessPermission.RevertAssert();
      new FileIOPermission(FileIOPermissionAccess.AllAccess, str1).Assert();
      if (this.ContainsUnknownFiles(str1))
        return;
      try
      {
        LongPathDirectory.Delete(str1, true);
      }
      catch
      {
      }
    }

    [SecuritySafeCritical]
    private void RemoveLogicalDir()
    {
      this.m_fiop.Assert();
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        if (!Directory.Exists(this.RootDirectory))
          return;
        ulong lFree = this.IsRoaming() ? 0UL : (ulong) (this.Quota - this.AvailableFreeSpace);
        ulong quota = (ulong) this.Quota;
        try
        {
          LongPathDirectory.Delete(this.RootDirectory, true);
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectories"));
        }
        this.Unreserve(lFree, quota);
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
    }

    private bool ContainsUnknownFiles(string rootDir)
    {
      string[] fileDirectoryNames1;
      string[] fileDirectoryNames2;
      try
      {
        fileDirectoryNames1 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", true);
        fileDirectoryNames2 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", false);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectories"));
      }
      if (fileDirectoryNames2 != null && fileDirectoryNames2.Length != 0)
      {
        if (fileDirectoryNames2.Length > 1)
          return true;
        if (this.IsApp())
        {
          if (IsolatedStorageFile.NotAppFilesDir(fileDirectoryNames2[0]))
            return true;
        }
        else if (this.IsDomain())
        {
          if (IsolatedStorageFile.NotFilesDir(fileDirectoryNames2[0]))
            return true;
        }
        else if (IsolatedStorageFile.NotAssemFilesDir(fileDirectoryNames2[0]))
          return true;
      }
      if (fileDirectoryNames1 == null || fileDirectoryNames1.Length == 0)
        return false;
      if (this.IsRoaming())
        return fileDirectoryNames1.Length > 1 || IsolatedStorageFile.NotIDFile(fileDirectoryNames1[0]);
      return fileDirectoryNames1.Length > 2 || IsolatedStorageFile.NotIDFile(fileDirectoryNames1[0]) && IsolatedStorageFile.NotInfoFile(fileDirectoryNames1[0]) || fileDirectoryNames1.Length == 2 && IsolatedStorageFile.NotIDFile(fileDirectoryNames1[1]) && IsolatedStorageFile.NotInfoFile(fileDirectoryNames1[1]);
    }

    /// <summary>
    ///   Закрывает хранилище, открытое ранее с помощью <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(System.IO.IsolatedStorage.IsolatedStorageScope,System.Type,System.Type)" />, <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForAssembly" />, или <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForDomain" />.
    /// </summary>
    [SecuritySafeCritical]
    public void Close()
    {
      if (this.IsRoaming())
        return;
      lock (this.m_internalLock)
      {
        if (this.m_closed)
          return;
        this.m_closed = true;
        if (this.m_handle != null)
          this.m_handle.Dispose();
        GC.SuppressFinalize((object) this);
      }
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />.
    /// </summary>
    public void Dispose()
    {
      this.Close();
      this.m_bDisposed = true;
    }

    /// <summary>
    ///   Позволяет объекту попытаться освободить ресурсы и выполнить другие операции очистки, перед тем как он будет уничтожен во время сборки мусора.
    /// </summary>
    ~IsolatedStorageFile()
    {
      this.Dispose();
    }

    private static bool NotIDFile(string file)
    {
      return (uint) string.Compare(file, "identity.dat", StringComparison.Ordinal) > 0U;
    }

    private static bool NotInfoFile(string file)
    {
      if (string.Compare(file, "info.dat", StringComparison.Ordinal) != 0)
        return (uint) string.Compare(file, "appinfo.dat", StringComparison.Ordinal) > 0U;
      return false;
    }

    private static bool NotFilesDir(string dir)
    {
      return (uint) string.Compare(dir, "Files", StringComparison.Ordinal) > 0U;
    }

    internal static bool NotAssemFilesDir(string dir)
    {
      return (uint) string.Compare(dir, "AssemFiles", StringComparison.Ordinal) > 0U;
    }

    internal static bool NotAppFilesDir(string dir)
    {
      return (uint) string.Compare(dir, "AppFiles", StringComparison.Ordinal) > 0U;
    }

    /// <summary>
    ///   Удаляет указанный изолированной области хранилищ для всех удостоверений.
    /// </summary>
    /// <param name="scope">
    ///   Поразрядное сочетание значений <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />.
    /// </param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    ///   Невозможно удалить изолированное хранилище.
    /// </exception>
    [SecuritySafeCritical]
    public static void Remove(IsolatedStorageScope scope)
    {
      IsolatedStorageFile.VerifyGlobalScope(scope);
      IsolatedStorageFile.DemandAdminPermission();
      string rootDir = IsolatedStorageFile.GetRootDir(scope);
      new FileIOPermission(FileIOPermissionAccess.Write, rootDir).Assert();
      try
      {
        LongPathDirectory.Delete(rootDir, true);
        LongPathDirectory.CreateDirectory(rootDir);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectories"));
      }
    }

    /// <summary>
    ///   Возвращает перечислитель для <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> хранятся внутри области изолированного хранилища.
    /// </summary>
    /// <param name="scope">
    ///   Представляет <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> для которого возвращаются изолированные хранилища.
    ///   <see langword="User" /> и <see langword="User|Roaming" /> только <see langword="IsolatedStorageScope" /> Поддерживаемые сочетания.
    /// </param>
    /// <returns>
    ///   Перечислитель для <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> хранит в пределах области указанной изолированного хранилища.
    /// </returns>
    [SecuritySafeCritical]
    public static IEnumerator GetEnumerator(IsolatedStorageScope scope)
    {
      IsolatedStorageFile.VerifyGlobalScope(scope);
      IsolatedStorageFile.DemandAdminPermission();
      return (IEnumerator) new IsolatedStorageFileEnumerator(scope);
    }

    internal string RootDirectory
    {
      get
      {
        return this.m_RootDir;
      }
    }

    internal string GetFullPath(string path)
    {
      if (path == string.Empty)
        return this.RootDirectory;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.RootDirectory);
      if ((int) path[0] == (int) this.SeparatorExternal)
        stringBuilder.Append(path.Substring(1));
      else
        stringBuilder.Append(path);
      return stringBuilder.ToString();
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
    private static string GetDataDirectoryFromActivationContext()
    {
      if (IsolatedStorageFile.s_appDataDir == null)
      {
        ActivationContext activationContext = AppDomain.CurrentDomain.ActivationContext;
        if (activationContext == null)
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationMissingIdentity"));
        string dataDirectory = activationContext.DataDirectory;
        if (dataDirectory != null && dataDirectory[dataDirectory.Length - 1] != '\\')
          dataDirectory += "\\";
        IsolatedStorageFile.s_appDataDir = dataDirectory;
      }
      return IsolatedStorageFile.s_appDataDir;
    }

    [SecuritySafeCritical]
    internal void Init(IsolatedStorageScope scope)
    {
      IsolatedStorageFile.GetGlobalFileIOPerm(scope).Assert();
      this.m_StoreScope = scope;
      StringBuilder stringBuilder = new StringBuilder();
      if (System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope))
      {
        stringBuilder.Append(IsolatedStorageFile.GetRootDir(scope));
        if (IsolatedStorageFile.s_appDataDir == null)
        {
          stringBuilder.Append(this.AppName);
          stringBuilder.Append(this.SeparatorExternal);
        }
        try
        {
          LongPathDirectory.CreateDirectory(stringBuilder.ToString());
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
        }
        this.CreateIDFile(stringBuilder.ToString(), IsolatedStorageScope.Application);
        this.m_InfoFile = stringBuilder.ToString() + "appinfo.dat";
        stringBuilder.Append("AppFiles");
      }
      else
      {
        stringBuilder.Append(IsolatedStorageFile.GetRootDir(scope));
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          stringBuilder.Append(this.DomainName);
          stringBuilder.Append(this.SeparatorExternal);
          try
          {
            LongPathDirectory.CreateDirectory(stringBuilder.ToString());
            this.CreateIDFile(stringBuilder.ToString(), IsolatedStorageScope.Domain);
          }
          catch
          {
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
          }
          this.m_InfoFile = stringBuilder.ToString() + "info.dat";
        }
        stringBuilder.Append(this.AssemName);
        stringBuilder.Append(this.SeparatorExternal);
        try
        {
          LongPathDirectory.CreateDirectory(stringBuilder.ToString());
          this.CreateIDFile(stringBuilder.ToString(), IsolatedStorageScope.Assembly);
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
        }
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          stringBuilder.Append("Files");
        }
        else
        {
          this.m_InfoFile = stringBuilder.ToString() + "info.dat";
          stringBuilder.Append("AssemFiles");
        }
      }
      stringBuilder.Append(this.SeparatorExternal);
      string path = stringBuilder.ToString();
      try
      {
        LongPathDirectory.CreateDirectory(path);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
      }
      this.m_RootDir = path;
      this.m_fiop = new FileIOPermission(FileIOPermissionAccess.AllAccess, path);
      if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Application))
        return;
      this.UpdateQuotaFromInfoFile();
    }

    [SecurityCritical]
    private void UpdateQuotaFromInfoFile()
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        lock (this.m_internalLock)
        {
          if (this.InvalidFileHandle)
            this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
          long quota = 0;
          if (!IsolatedStorageFile.GetQuota(this.m_handle, out quota))
            return;
          base.Quota = quota;
        }
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
    }

    [SecuritySafeCritical]
    internal bool InitExistingStore(IsolatedStorageScope scope)
    {
      StringBuilder stringBuilder = new StringBuilder();
      this.m_StoreScope = scope;
      stringBuilder.Append(IsolatedStorageFile.GetRootDir(scope));
      if (System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope))
      {
        stringBuilder.Append(this.AppName);
        stringBuilder.Append(this.SeparatorExternal);
        this.m_InfoFile = stringBuilder.ToString() + "appinfo.dat";
        stringBuilder.Append("AppFiles");
      }
      else
      {
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          stringBuilder.Append(this.DomainName);
          stringBuilder.Append(this.SeparatorExternal);
          this.m_InfoFile = stringBuilder.ToString() + "info.dat";
        }
        stringBuilder.Append(this.AssemName);
        stringBuilder.Append(this.SeparatorExternal);
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          stringBuilder.Append("Files");
        }
        else
        {
          this.m_InfoFile = stringBuilder.ToString() + "info.dat";
          stringBuilder.Append("AssemFiles");
        }
      }
      stringBuilder.Append(this.SeparatorExternal);
      FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, stringBuilder.ToString());
      fileIoPermission.Assert();
      if (!LongPathDirectory.Exists(stringBuilder.ToString()))
        return false;
      this.m_RootDir = stringBuilder.ToString();
      this.m_fiop = fileIoPermission;
      if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
        this.UpdateQuotaFromInfoFile();
      return true;
    }

    protected override IsolatedStoragePermission GetPermission(PermissionSet ps)
    {
      if (ps == null)
        return (IsolatedStoragePermission) null;
      if (ps.IsUnrestricted())
        return (IsolatedStoragePermission) new IsolatedStorageFilePermission(PermissionState.Unrestricted);
      return (IsolatedStoragePermission) ps.GetPermission(typeof (IsolatedStorageFilePermission));
    }

    internal void UndoReserveOperation(ulong oldLen, ulong newLen)
    {
      oldLen = IsolatedStorageFile.RoundToBlockSize(oldLen);
      if (newLen <= oldLen)
        return;
      this.Unreserve(IsolatedStorageFile.RoundToBlockSize(newLen - oldLen));
    }

    internal void Reserve(ulong oldLen, ulong newLen)
    {
      oldLen = IsolatedStorageFile.RoundToBlockSize(oldLen);
      if (newLen <= oldLen)
        return;
      this.Reserve(IsolatedStorageFile.RoundToBlockSize(newLen - oldLen));
    }

    internal void ReserveOneBlock()
    {
      this.Reserve(1024UL);
    }

    internal void UnreserveOneBlock()
    {
      this.Unreserve(1024UL);
    }

    internal static ulong RoundToBlockSize(ulong num)
    {
      if (num < 1024UL)
        return 1024;
      ulong num1 = num % 1024UL;
      if (num1 != 0UL)
        num += 1024UL - num1;
      return num;
    }

    internal static ulong RoundToBlockSizeFloor(ulong num)
    {
      if (num < 1024UL)
        return 0;
      ulong num1 = num % 1024UL;
      num -= num1;
      return num;
    }

    [SecurityCritical]
    internal static string GetRootDir(IsolatedStorageScope scope)
    {
      if (System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(scope))
      {
        if (IsolatedStorageFile.s_RootDirRoaming == null)
        {
          string s = (string) null;
          IsolatedStorageFile.GetRootDir(scope, JitHelpers.GetStringHandleOnStack(ref s));
          IsolatedStorageFile.s_RootDirRoaming = s;
        }
        return IsolatedStorageFile.s_RootDirRoaming;
      }
      if (System.IO.IsolatedStorage.IsolatedStorage.IsMachine(scope))
      {
        if (IsolatedStorageFile.s_RootDirMachine == null)
          IsolatedStorageFile.InitGlobalsMachine(scope);
        return IsolatedStorageFile.s_RootDirMachine;
      }
      if (IsolatedStorageFile.s_RootDirUser == null)
        IsolatedStorageFile.InitGlobalsNonRoamingUser(scope);
      return IsolatedStorageFile.s_RootDirUser;
    }

    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    private static void InitGlobalsMachine(IsolatedStorageScope scope)
    {
      string s = (string) null;
      IsolatedStorageFile.GetRootDir(scope, JitHelpers.GetStringHandleOnStack(ref s));
      new FileIOPermission(FileIOPermissionAccess.AllAccess, s).Assert();
      string str = IsolatedStorageFile.GetMachineRandomDirectory(s);
      if (str == null)
      {
        Mutex mutexNotOwned = IsolatedStorageFile.CreateMutexNotOwned(s);
        if (!mutexNotOwned.WaitOne())
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
        try
        {
          str = IsolatedStorageFile.GetMachineRandomDirectory(s);
          if (str == null)
          {
            string randomFileName1 = Path.GetRandomFileName();
            string randomFileName2 = Path.GetRandomFileName();
            try
            {
              IsolatedStorageFile.CreateDirectoryWithDacl(s + randomFileName1);
              IsolatedStorageFile.CreateDirectoryWithDacl(s + randomFileName1 + "\\" + randomFileName2);
            }
            catch
            {
              throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
            }
            str = randomFileName1 + "\\" + randomFileName2;
          }
        }
        finally
        {
          mutexNotOwned.ReleaseMutex();
        }
      }
      IsolatedStorageFile.s_RootDirMachine = s + str + "\\";
    }

    [SecuritySafeCritical]
    private static void InitGlobalsNonRoamingUser(IsolatedStorageScope scope)
    {
      string s = (string) null;
      if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
      {
        s = IsolatedStorageFile.GetDataDirectoryFromActivationContext();
        if (s != null)
        {
          IsolatedStorageFile.s_RootDirUser = s;
          return;
        }
      }
      IsolatedStorageFile.GetRootDir(scope, JitHelpers.GetStringHandleOnStack(ref s));
      new FileIOPermission(FileIOPermissionAccess.AllAccess, s).Assert();
      bool bMigrateNeeded = false;
      string sOldStoreLocation = (string) null;
      string str = IsolatedStorageFile.GetRandomDirectory(s, out bMigrateNeeded, out sOldStoreLocation);
      if (str == null)
      {
        Mutex mutexNotOwned = IsolatedStorageFile.CreateMutexNotOwned(s);
        if (!mutexNotOwned.WaitOne())
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
        try
        {
          str = IsolatedStorageFile.GetRandomDirectory(s, out bMigrateNeeded, out sOldStoreLocation) ?? (!bMigrateNeeded ? IsolatedStorageFile.CreateRandomDirectory(s) : IsolatedStorageFile.MigrateOldIsoStoreDirectory(s, sOldStoreLocation));
        }
        finally
        {
          mutexNotOwned.ReleaseMutex();
        }
      }
      IsolatedStorageFile.s_RootDirUser = s + str + "\\";
    }

    internal bool Disposed
    {
      get
      {
        return this.m_bDisposed;
      }
    }

    private bool InvalidFileHandle
    {
      [SecuritySafeCritical] get
      {
        if (this.m_handle != null && !this.m_handle.IsClosed)
          return this.m_handle.IsInvalid;
        return true;
      }
    }

    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static string MigrateOldIsoStoreDirectory(string rootDir, string oldRandomDirectory)
    {
      string randomFileName1 = Path.GetRandomFileName();
      string randomFileName2 = Path.GetRandomFileName();
      string path = rootDir + randomFileName1;
      string destDirName = path + "\\" + randomFileName2;
      try
      {
        LongPathDirectory.CreateDirectory(path);
        LongPathDirectory.Move(rootDir + oldRandomDirectory, destDirName);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
      }
      return randomFileName1 + "\\" + randomFileName2;
    }

    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static string CreateRandomDirectory(string rootDir)
    {
      string str;
      string path;
      do
      {
        str = Path.GetRandomFileName() + "\\" + Path.GetRandomFileName();
        path = rootDir + str;
      }
      while (LongPathDirectory.Exists(path));
      try
      {
        LongPathDirectory.CreateDirectory(path);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
      }
      return str;
    }

    internal static string GetRandomDirectory(string rootDir, out bool bMigrateNeeded, out string sOldStoreLocation)
    {
      bMigrateNeeded = false;
      sOldStoreLocation = (string) null;
      string[] fileDirectoryNames1 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", false);
      for (int index1 = 0; index1 < fileDirectoryNames1.Length; ++index1)
      {
        if (fileDirectoryNames1[index1].Length == 12)
        {
          string[] fileDirectoryNames2 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + fileDirectoryNames1[index1] + "\\*", "*", false);
          for (int index2 = 0; index2 < fileDirectoryNames2.Length; ++index2)
          {
            if (fileDirectoryNames2[index2].Length == 12)
              return fileDirectoryNames1[index1] + "\\" + fileDirectoryNames2[index2];
          }
        }
      }
      for (int index = 0; index < fileDirectoryNames1.Length; ++index)
      {
        if (fileDirectoryNames1[index].Length == 24)
        {
          bMigrateNeeded = true;
          sOldStoreLocation = fileDirectoryNames1[index];
          return (string) null;
        }
      }
      return (string) null;
    }

    internal static string GetMachineRandomDirectory(string rootDir)
    {
      string[] fileDirectoryNames1 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", false);
      for (int index1 = 0; index1 < fileDirectoryNames1.Length; ++index1)
      {
        if (fileDirectoryNames1[index1].Length == 12)
        {
          string[] fileDirectoryNames2 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + fileDirectoryNames1[index1] + "\\*", "*", false);
          for (int index2 = 0; index2 < fileDirectoryNames2.Length; ++index2)
          {
            if (fileDirectoryNames2[index2].Length == 12)
              return fileDirectoryNames1[index1] + "\\" + fileDirectoryNames2[index2];
          }
        }
      }
      return (string) null;
    }

    [SecurityCritical]
    internal static Mutex CreateMutexNotOwned(string pathName)
    {
      return new Mutex(false, "Global\\" + IsolatedStorageFile.GetStrongHashSuitableForObjectName(pathName));
    }

    internal static string GetStrongHashSuitableForObjectName(string name)
    {
      MemoryStream memoryStream = new MemoryStream();
      new BinaryWriter((Stream) memoryStream).Write(name.ToUpper(CultureInfo.InvariantCulture));
      memoryStream.Position = 0L;
      return Path.ToBase32StringSuitableForDirName(new SHA1CryptoServiceProvider().ComputeHash((Stream) memoryStream));
    }

    private string GetSyncObjectName()
    {
      if (this.m_SyncObjectName == null)
        this.m_SyncObjectName = IsolatedStorageFile.GetStrongHashSuitableForObjectName(this.m_InfoFile);
      return this.m_SyncObjectName;
    }

    [SecuritySafeCritical]
    internal void Lock(ref bool locked)
    {
      locked = false;
      if (this.IsRoaming())
        return;
      lock (this.m_internalLock)
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.m_closed)
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.InvalidFileHandle)
          this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
        locked = IsolatedStorageFile.Lock(this.m_handle, true);
      }
    }

    [SecuritySafeCritical]
    internal void Unlock()
    {
      if (this.IsRoaming())
        return;
      lock (this.m_internalLock)
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.m_closed)
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.InvalidFileHandle)
          this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
        IsolatedStorageFile.Lock(this.m_handle, false);
      }
    }

    [SecurityCritical]
    internal static FileIOPermission GetGlobalFileIOPerm(IsolatedStorageScope scope)
    {
      if (System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(scope))
      {
        if (IsolatedStorageFile.s_PermRoaming == null)
          IsolatedStorageFile.s_PermRoaming = new FileIOPermission(FileIOPermissionAccess.AllAccess, IsolatedStorageFile.GetRootDir(scope));
        return IsolatedStorageFile.s_PermRoaming;
      }
      if (System.IO.IsolatedStorage.IsolatedStorage.IsMachine(scope))
      {
        if (IsolatedStorageFile.s_PermMachine == null)
          IsolatedStorageFile.s_PermMachine = new FileIOPermission(FileIOPermissionAccess.AllAccess, IsolatedStorageFile.GetRootDir(scope));
        return IsolatedStorageFile.s_PermMachine;
      }
      if (IsolatedStorageFile.s_PermUser == null)
        IsolatedStorageFile.s_PermUser = new FileIOPermission(FileIOPermissionAccess.AllAccess, IsolatedStorageFile.GetRootDir(scope));
      return IsolatedStorageFile.s_PermUser;
    }

    [SecurityCritical]
    private static void DemandAdminPermission()
    {
      if (IsolatedStorageFile.s_PermAdminUser == null)
        IsolatedStorageFile.s_PermAdminUser = new IsolatedStorageFilePermission(IsolatedStorageContainment.AdministerIsolatedStorageByUser, 0L, false);
      IsolatedStorageFile.s_PermAdminUser.Demand();
    }

    internal static void VerifyGlobalScope(IsolatedStorageScope scope)
    {
      if (scope != IsolatedStorageScope.User && scope != (IsolatedStorageScope.User | IsolatedStorageScope.Roaming) && scope != IsolatedStorageScope.Machine)
        throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Scope_U_R_M"));
    }

    [SecuritySafeCritical]
    internal void CreateIDFile(string path, IsolatedStorageScope scope)
    {
      try
      {
        using (FileStream fileStream = new FileStream(path + "identity.dat", FileMode.OpenOrCreate))
        {
          MemoryStream identityStream = this.GetIdentityStream(scope);
          byte[] buffer = identityStream.GetBuffer();
          fileStream.Write(buffer, 0, (int) identityStream.Length);
          identityStream.Close();
        }
      }
      catch
      {
      }
    }

    [SecuritySafeCritical]
    internal static string[] GetFileDirectoryNames(string path, string userSearchPattern, bool file)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path), Environment.GetResourceString("ArgumentNull_Path"));
      userSearchPattern = IsolatedStorageFile.NormalizeSearchPattern(userSearchPattern);
      if (userSearchPattern.Length == 0)
        return new string[0];
      bool flag = false;
      char ch = path[path.Length - 1];
      if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || ch == '.')
        flag = true;
      string path1 = LongPath.NormalizePath(path);
      if (flag && (int) path1[path1.Length - 1] != (int) ch)
        path1 += "\\*";
      string directoryName = LongPath.GetDirectoryName(path1);
      if (directoryName != null)
        directoryName += "\\";
      try
      {
        new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          directoryName == null ? path1 : directoryName
        }, false, false).Demand();
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      string[] array = new string[10];
      int newSize = 0;
      Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
      SafeFindHandle firstFile = Win32Native.FindFirstFile(Path.AddLongPathPrefix(path1), ref wiN32FindData);
      if (firstFile.IsInvalid)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (lastWin32Error == 2)
          return new string[0];
        __Error.WinIOError(lastWin32Error, userSearchPattern);
      }
      int num = 0;
      do
      {
        if (!file ? wiN32FindData.IsNormalDirectory : wiN32FindData.IsFile)
        {
          ++num;
          if (newSize == array.Length)
            Array.Resize<string>(ref array, 2 * array.Length);
          array[newSize++] = wiN32FindData.cFileName;
        }
      }
      while (Win32Native.FindNextFile(firstFile, ref wiN32FindData));
      int lastWin32Error1 = Marshal.GetLastWin32Error();
      firstFile.Close();
      if (lastWin32Error1 != 0 && lastWin32Error1 != 18)
        __Error.WinIOError(lastWin32Error1, userSearchPattern);
      if (!file && num == 1 && (wiN32FindData.dwFileAttributes & 16) != 0)
        return new string[1]{ wiN32FindData.cFileName };
      if (newSize == array.Length)
        return array;
      Array.Resize<string>(ref array, newSize);
      return array;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern ulong GetUsage(SafeIsolatedStorageFileHandle handle);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern SafeIsolatedStorageFileHandle Open(string infoFile, string syncName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void Reserve(SafeIsolatedStorageFileHandle handle, ulong plQuota, ulong plReserve, [MarshalAs(UnmanagedType.Bool)] bool fFree);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetRootDir(IsolatedStorageScope scope, StringHandleOnStack retRootDir);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool Lock(SafeIsolatedStorageFileHandle handle, [MarshalAs(UnmanagedType.Bool)] bool fLock);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void CreateDirectoryWithDacl(string path);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetQuota(SafeIsolatedStorageFileHandle scope, out long quota);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern void SetQuota(SafeIsolatedStorageFileHandle scope, long quota);
  }
}
