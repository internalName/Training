// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.RegistrationServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет набор служб для регистрации и отмены регистрации управляемых сборок для использования из COM.
  /// </summary>
  [Guid("475E398F-8AFA-43a7-A3BE-F4EF8D6787C9")]
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  public class RegistrationServices : IRegistrationServices
  {
    private static Guid s_ManagedCategoryGuid = new Guid("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}");
    private const string strManagedCategoryGuid = "{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}";
    private const string strDocStringPrefix = "";
    private const string strManagedTypeThreadingModel = "Both";
    private const string strComponentCategorySubKey = "Component Categories";
    private const string strManagedCategoryDescription = ".NET Category";
    private const string strImplementedCategoriesSubKey = "Implemented Categories";
    private const string strMsCorEEFileName = "mscoree.dll";
    private const string strRecordRootName = "Record";
    private const string strClsIdRootName = "CLSID";
    private const string strTlbRootName = "TypeLib";

    /// <summary>
    ///   Регистрирует классы в управляемой сборке для поддержки создания из COM.
    /// </summary>
    /// <param name="assembly">Регистрируемая сборка.</param>
    /// <param name="flags">
    ///   <see cref="T:System.Runtime.InteropServices.AssemblyRegistrationFlags" /> Значение, указывающее, специальные параметры, используемые при регистрации <paramref name="assembly" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="assembly" /> содержит типы, которые были успешно зарегистрирован; в противном случае <see langword="false" /> Если сборка не содержит подходящих типов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Полное имя <paramref name="assembly" /> — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Метод, помеченный атрибутом <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> не <see langword="static" />.
    /// 
    ///   -или-
    /// 
    ///   Существует более одного метода, помеченного <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> на данном уровне иерархии.
    /// 
    ///   -или-
    /// 
    ///   Подпись метода, помеченного <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> является недопустимым.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Функция пользовательской регистрации (отмеченные <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> атрибут) вызывает исключение.
    /// </exception>
    [SecurityCritical]
    public virtual bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException(nameof (assembly));
      if (assembly.ReflectionOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsmLoadedForReflectionOnly"));
      RuntimeAssembly assembly1 = assembly as RuntimeAssembly;
      if ((Assembly) assembly1 == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      string fullName = assembly.FullName;
      if (fullName == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoAsmName"));
      string strAsmCodeBase = (string) null;
      if ((flags & AssemblyRegistrationFlags.SetCodeBase) != AssemblyRegistrationFlags.None)
      {
        strAsmCodeBase = assembly1.GetCodeBase(false);
        if (strAsmCodeBase == null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoAsmCodeBase"));
      }
      Type[] registrableTypesInAssembly = this.GetRegistrableTypesInAssembly(assembly);
      int length1 = registrableTypesInAssembly.Length;
      string strAsmVersion = assembly1.GetVersion().ToString();
      string imageRuntimeVersion = assembly.ImageRuntimeVersion;
      for (int index = 0; index < length1; ++index)
      {
        if (this.IsRegisteredAsValueType(registrableTypesInAssembly[index]))
          this.RegisterValueType(registrableTypesInAssembly[index], fullName, strAsmVersion, strAsmCodeBase, imageRuntimeVersion);
        else if (this.TypeRepresentsComType(registrableTypesInAssembly[index]))
          this.RegisterComImportedType(registrableTypesInAssembly[index], fullName, strAsmVersion, strAsmCodeBase, imageRuntimeVersion);
        else
          this.RegisterManagedType(registrableTypesInAssembly[index], fullName, strAsmVersion, strAsmCodeBase, imageRuntimeVersion);
        this.CallUserDefinedRegistrationMethod(registrableTypesInAssembly[index], true);
      }
      object[] customAttributes = assembly.GetCustomAttributes(typeof (PrimaryInteropAssemblyAttribute), false);
      int length2 = customAttributes.Length;
      for (int index = 0; index < length2; ++index)
        this.RegisterPrimaryInteropAssembly(assembly1, strAsmCodeBase, (PrimaryInteropAssemblyAttribute) customAttributes[index]);
      return registrableTypesInAssembly.Length != 0 || length2 > 0;
    }

    /// <summary>Отменяет регистрацию классов в управляемой сборке.</summary>
    /// <param name="assembly">Сборка для отмены регистрации.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="assembly" /> содержит типы, которые были успешно удалена; в противном случае <see langword="false" /> Если сборка не содержит подходящих типов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Полное имя <paramref name="assembly" /> — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Метод, помеченный атрибутом <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> не <see langword="static" />.
    /// 
    ///   -или-
    /// 
    ///   Существует более одного метода, помеченного <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> на данном уровне иерархии.
    /// 
    ///   -или-
    /// 
    ///   Подпись метода, помеченного <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> является недопустимым.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Функция пользовательской пользовательских отмены регистрации (отмеченные <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" />  атрибута) вызывает исключение.
    /// </exception>
    [SecurityCritical]
    public virtual bool UnregisterAssembly(Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException(nameof (assembly));
      if (assembly.ReflectionOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsmLoadedForReflectionOnly"));
      RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
      if ((Assembly) runtimeAssembly == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      bool flag = true;
      Type[] registrableTypesInAssembly = this.GetRegistrableTypesInAssembly(assembly);
      int length1 = registrableTypesInAssembly.Length;
      string strAsmVersion = runtimeAssembly.GetVersion().ToString();
      for (int index = 0; index < length1; ++index)
      {
        this.CallUserDefinedRegistrationMethod(registrableTypesInAssembly[index], false);
        if (this.IsRegisteredAsValueType(registrableTypesInAssembly[index]))
        {
          if (!this.UnregisterValueType(registrableTypesInAssembly[index], strAsmVersion))
            flag = false;
        }
        else if (this.TypeRepresentsComType(registrableTypesInAssembly[index]))
        {
          if (!this.UnregisterComImportedType(registrableTypesInAssembly[index], strAsmVersion))
            flag = false;
        }
        else if (!this.UnregisterManagedType(registrableTypesInAssembly[index], strAsmVersion))
          flag = false;
      }
      object[] customAttributes = assembly.GetCustomAttributes(typeof (PrimaryInteropAssemblyAttribute), false);
      int length2 = customAttributes.Length;
      if (flag)
      {
        for (int index = 0; index < length2; ++index)
          this.UnregisterPrimaryInteropAssembly(assembly, (PrimaryInteropAssemblyAttribute) customAttributes[index]);
      }
      return registrableTypesInAssembly.Length != 0 || length2 > 0;
    }

    /// <summary>
    ///   Получает список классов в сборке, которая будет зарегистрирована с помощью вызова <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterAssembly(System.Reflection.Assembly,System.Runtime.InteropServices.AssemblyRegistrationFlags)" />.
    /// </summary>
    /// <param name="assembly">Сборка для поиска классов.</param>
    /// <returns>
    ///   A <see cref="T:System.Type" /> массив, содержащий список классов в <paramref name="assembly" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assembly" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual Type[] GetRegistrableTypesInAssembly(Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException(nameof (assembly));
      if (!(assembly is RuntimeAssembly))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), nameof (assembly));
      Type[] exportedTypes = assembly.GetExportedTypes();
      int length = exportedTypes.Length;
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < length; ++index)
      {
        Type type = exportedTypes[index];
        if (this.TypeRequiresRegistration(type))
          arrayList.Add((object) type);
      }
      Type[] typeArray = new Type[arrayList.Count];
      arrayList.CopyTo((Array) typeArray);
      return typeArray;
    }

    /// <summary>
    ///   Извлекает идентификатор ProgID COM для указанного типа.
    /// </summary>
    /// <param name="type">
    ///   Тип, соответствующий код ProgID, запрашивается.
    /// </param>
    /// <returns>Идентификатор ProgID для указанного типа.</returns>
    [SecurityCritical]
    public virtual string GetProgIdForType(Type type)
    {
      return Marshal.GenerateProgIdForType(type);
    }

    /// <summary>
    ///   Регистрирует указанный тип в COM, используя указанный идентификатор GUID.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Регистрируемый для использования из COM.
    /// </param>
    /// <param name="g">
    ///   <see cref="T:System.Guid" /> Используемый для регистрации указанного типа.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="type" /> Не удается создать параметр.
    /// </exception>
    [SecurityCritical]
    public virtual void RegisterTypeForComClients(Type type, ref Guid g)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (type as RuntimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (type));
      if (!this.TypeRequiresRegistration(type))
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustBeComCreatable"), nameof (type));
      RegistrationServices.RegisterTypeForComClientsNative(type, ref g);
    }

    /// <summary>
    ///   Возвращает идентификатор GUID категории COM, содержащей управляемые классы.
    /// </summary>
    /// <returns>
    ///   Идентификатор GUID категории COM, содержащей управляемые классы.
    /// </returns>
    public virtual Guid GetManagedCategoryGuid()
    {
      return RegistrationServices.s_ManagedCategoryGuid;
    }

    /// <summary>Определяет, требуется ли указанный тип регистрации.</summary>
    /// <param name="type">
    ///   Проверяемый тип требования к регистрации COM.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если этот тип должен быть зарегистрирован для использования из COM; в противном случае <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    public virtual bool TypeRequiresRegistration(Type type)
    {
      return RegistrationServices.TypeRequiresRegistrationHelper(type);
    }

    /// <summary>
    ///   Указывает, является ли тип помечен атрибутом <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />, или является производным от типа, отмеченного <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> и использует тот же GUID в качестве родительской.
    /// </summary>
    /// <param name="type">
    ///   Тип, для модели COM тип которого проверяется.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если тип помечен атрибутом <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />, или является производным от типа, отмеченного <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> и использует тот же GUID как родительский элемент; в противном случае <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public virtual bool TypeRepresentsComType(Type type)
    {
      if (!type.IsCOMObject)
        return false;
      if (type.IsImport)
        return true;
      Type baseComImportType = this.GetBaseComImportType(type);
      return Marshal.GenerateGuidForType(type) == Marshal.GenerateGuidForType(baseComImportType);
    }

    /// <summary>
    ///   Регистрирует указанный тип в COM, используя заданный контекст выполнения и тип подключения.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Объект, регистрируемый для использования из COM.
    /// </param>
    /// <param name="classContext">
    ///   Один из <see cref="T:System.Runtime.InteropServices.RegistrationClassContext" /> значений, которое указывает контекст, в котором будет запущен исполняемый код.
    /// </param>
    /// <param name="flags">
    ///   Один из <see cref="T:System.Runtime.InteropServices.RegistrationConnectionType" /> значений, указывающих способ подключения к объекту класса.
    /// </param>
    /// <returns>Целое число, представляющее значение файла cookie.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="type" /> Не удается создать параметр.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    public virtual int RegisterTypeForComClients(Type type, RegistrationClassContext classContext, RegistrationConnectionType flags)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (type as RuntimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (type));
      if (!this.TypeRequiresRegistration(type))
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustBeComCreatable"), nameof (type));
      return RegistrationServices.RegisterTypeForComClientsExNative(type, classContext, flags);
    }

    /// <summary>
    ///   Удаляет ссылки на тип, зарегистрированный с помощью метода <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" />.
    /// </summary>
    /// <param name="cookie">
    ///   Значение файла cookie, возвращенное предыдущим вызовом перегрузки метода <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" />.
    /// </param>
    [SecurityCritical]
    [ComVisible(false)]
    public virtual void UnregisterTypeForComClients(int cookie)
    {
      RegistrationServices.CoRevokeClassObject(cookie);
    }

    [SecurityCritical]
    internal static bool TypeRequiresRegistrationHelper(Type type)
    {
      if (!type.IsClass && !type.IsValueType || type.IsAbstract || !type.IsValueType && type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, (Binder) null, new Type[0], (ParameterModifier[]) null) == (ConstructorInfo) null)
        return false;
      return Marshal.IsTypeVisibleFromCom(type);
    }

    [SecurityCritical]
    private void RegisterValueType(Type type, string strAsmName, string strAsmVersion, string strAsmCodeBase, string strRuntimeVersion)
    {
      string subkey = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("Record"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey(subkey))
        {
          using (RegistryKey subKey3 = subKey2.CreateSubKey(strAsmVersion))
          {
            subKey3.SetValue("Class", (object) type.FullName);
            subKey3.SetValue("Assembly", (object) strAsmName);
            subKey3.SetValue("RuntimeVersion", (object) strRuntimeVersion);
            if (strAsmCodeBase == null)
              return;
            subKey3.SetValue("CodeBase", (object) strAsmCodeBase);
          }
        }
      }
    }

    [SecurityCritical]
    private void RegisterManagedType(Type type, string strAsmName, string strAsmVersion, string strAsmCodeBase, string strRuntimeVersion)
    {
      string str = type.FullName ?? "";
      string subkey = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      string progIdForType = this.GetProgIdForType(type);
      if (progIdForType != string.Empty)
      {
        using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey(progIdForType))
        {
          subKey1.SetValue("", (object) str);
          using (RegistryKey subKey2 = subKey1.CreateSubKey("CLSID"))
            subKey2.SetValue("", (object) subkey);
        }
      }
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("CLSID"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey(subkey))
        {
          subKey2.SetValue("", (object) str);
          using (RegistryKey subKey3 = subKey2.CreateSubKey("InprocServer32"))
          {
            subKey3.SetValue("", (object) "mscoree.dll");
            subKey3.SetValue("ThreadingModel", (object) "Both");
            subKey3.SetValue("Class", (object) type.FullName);
            subKey3.SetValue("Assembly", (object) strAsmName);
            subKey3.SetValue("RuntimeVersion", (object) strRuntimeVersion);
            if (strAsmCodeBase != null)
              subKey3.SetValue("CodeBase", (object) strAsmCodeBase);
            using (RegistryKey subKey4 = subKey3.CreateSubKey(strAsmVersion))
            {
              subKey4.SetValue("Class", (object) type.FullName);
              subKey4.SetValue("Assembly", (object) strAsmName);
              subKey4.SetValue("RuntimeVersion", (object) strRuntimeVersion);
              if (strAsmCodeBase != null)
                subKey4.SetValue("CodeBase", (object) strAsmCodeBase);
            }
            if (progIdForType != string.Empty)
            {
              using (RegistryKey subKey4 = subKey2.CreateSubKey("ProgId"))
                subKey4.SetValue("", (object) progIdForType);
            }
          }
          using (RegistryKey subKey3 = subKey2.CreateSubKey("Implemented Categories"))
          {
            using (subKey3.CreateSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}"))
              ;
          }
        }
      }
      this.EnsureManagedCategoryExists();
    }

    [SecurityCritical]
    private void RegisterComImportedType(Type type, string strAsmName, string strAsmVersion, string strAsmCodeBase, string strRuntimeVersion)
    {
      string subkey = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("CLSID"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey(subkey))
        {
          using (RegistryKey subKey3 = subKey2.CreateSubKey("InprocServer32"))
          {
            subKey3.SetValue("Class", (object) type.FullName);
            subKey3.SetValue("Assembly", (object) strAsmName);
            subKey3.SetValue("RuntimeVersion", (object) strRuntimeVersion);
            if (strAsmCodeBase != null)
              subKey3.SetValue("CodeBase", (object) strAsmCodeBase);
            using (RegistryKey subKey4 = subKey3.CreateSubKey(strAsmVersion))
            {
              subKey4.SetValue("Class", (object) type.FullName);
              subKey4.SetValue("Assembly", (object) strAsmName);
              subKey4.SetValue("RuntimeVersion", (object) strRuntimeVersion);
              if (strAsmCodeBase == null)
                return;
              subKey4.SetValue("CodeBase", (object) strAsmCodeBase);
            }
          }
        }
      }
    }

    [SecurityCritical]
    private bool UnregisterValueType(Type type, string strAsmVersion)
    {
      bool flag = true;
      string str = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("Record", true))
      {
        if (registryKey1 != null)
        {
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey(str, true))
          {
            if (registryKey2 != null)
            {
              using (RegistryKey registryKey3 = registryKey2.OpenSubKey(strAsmVersion, true))
              {
                if (registryKey3 != null)
                {
                  registryKey3.DeleteValue("Assembly", false);
                  registryKey3.DeleteValue("Class", false);
                  registryKey3.DeleteValue("CodeBase", false);
                  registryKey3.DeleteValue("RuntimeVersion", false);
                  if (registryKey3.SubKeyCount == 0)
                  {
                    if (registryKey3.ValueCount == 0)
                      registryKey2.DeleteSubKey(strAsmVersion);
                  }
                }
              }
              if (registryKey2.SubKeyCount != 0)
                flag = false;
              if (registryKey2.SubKeyCount == 0)
              {
                if (registryKey2.ValueCount == 0)
                  registryKey1.DeleteSubKey(str);
              }
            }
          }
          if (registryKey1.SubKeyCount == 0)
          {
            if (registryKey1.ValueCount == 0)
              Registry.ClassesRoot.DeleteSubKey("Record");
          }
        }
      }
      return flag;
    }

    [SecurityCritical]
    private bool UnregisterManagedType(Type type, string strAsmVersion)
    {
      bool flag = true;
      string str = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      string progIdForType = this.GetProgIdForType(type);
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("CLSID", true))
      {
        if (registryKey1 != null)
        {
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey(str, true))
          {
            if (registryKey2 != null)
            {
              using (RegistryKey registryKey3 = registryKey2.OpenSubKey("InprocServer32", true))
              {
                if (registryKey3 != null)
                {
                  using (RegistryKey registryKey4 = registryKey3.OpenSubKey(strAsmVersion, true))
                  {
                    if (registryKey4 != null)
                    {
                      registryKey4.DeleteValue("Assembly", false);
                      registryKey4.DeleteValue("Class", false);
                      registryKey4.DeleteValue("RuntimeVersion", false);
                      registryKey4.DeleteValue("CodeBase", false);
                      if (registryKey4.SubKeyCount == 0)
                      {
                        if (registryKey4.ValueCount == 0)
                          registryKey3.DeleteSubKey(strAsmVersion);
                      }
                    }
                  }
                  if (registryKey3.SubKeyCount != 0)
                    flag = false;
                  if (flag)
                  {
                    registryKey3.DeleteValue("", false);
                    registryKey3.DeleteValue("ThreadingModel", false);
                  }
                  registryKey3.DeleteValue("Assembly", false);
                  registryKey3.DeleteValue("Class", false);
                  registryKey3.DeleteValue("RuntimeVersion", false);
                  registryKey3.DeleteValue("CodeBase", false);
                  if (registryKey3.SubKeyCount == 0)
                  {
                    if (registryKey3.ValueCount == 0)
                      registryKey2.DeleteSubKey("InprocServer32");
                  }
                }
              }
              if (flag)
              {
                registryKey2.DeleteValue("", false);
                if (progIdForType != string.Empty)
                {
                  using (RegistryKey registryKey3 = registryKey2.OpenSubKey("ProgId", true))
                  {
                    if (registryKey3 != null)
                    {
                      registryKey3.DeleteValue("", false);
                      if (registryKey3.SubKeyCount == 0)
                      {
                        if (registryKey3.ValueCount == 0)
                          registryKey2.DeleteSubKey("ProgId");
                      }
                    }
                  }
                }
                using (RegistryKey registryKey3 = registryKey2.OpenSubKey("Implemented Categories", true))
                {
                  if (registryKey3 != null)
                  {
                    using (RegistryKey registryKey4 = registryKey3.OpenSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}", true))
                    {
                      if (registryKey4 != null)
                      {
                        if (registryKey4.SubKeyCount == 0)
                        {
                          if (registryKey4.ValueCount == 0)
                            registryKey3.DeleteSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}");
                        }
                      }
                    }
                    if (registryKey3.SubKeyCount == 0)
                    {
                      if (registryKey3.ValueCount == 0)
                        registryKey2.DeleteSubKey("Implemented Categories");
                    }
                  }
                }
              }
              if (registryKey2.SubKeyCount == 0)
              {
                if (registryKey2.ValueCount == 0)
                  registryKey1.DeleteSubKey(str);
              }
            }
          }
          if (registryKey1.SubKeyCount == 0 && registryKey1.ValueCount == 0)
            Registry.ClassesRoot.DeleteSubKey("CLSID");
        }
        if (flag)
        {
          if (progIdForType != string.Empty)
          {
            using (RegistryKey registryKey2 = Registry.ClassesRoot.OpenSubKey(progIdForType, true))
            {
              if (registryKey2 != null)
              {
                registryKey2.DeleteValue("", false);
                using (RegistryKey registryKey3 = registryKey2.OpenSubKey("CLSID", true))
                {
                  if (registryKey3 != null)
                  {
                    registryKey3.DeleteValue("", false);
                    if (registryKey3.SubKeyCount == 0)
                    {
                      if (registryKey3.ValueCount == 0)
                        registryKey2.DeleteSubKey("CLSID");
                    }
                  }
                }
                if (registryKey2.SubKeyCount == 0)
                {
                  if (registryKey2.ValueCount == 0)
                    Registry.ClassesRoot.DeleteSubKey(progIdForType);
                }
              }
            }
          }
        }
      }
      return flag;
    }

    [SecurityCritical]
    private bool UnregisterComImportedType(Type type, string strAsmVersion)
    {
      bool flag = true;
      string str = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("CLSID", true))
      {
        if (registryKey1 != null)
        {
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey(str, true))
          {
            if (registryKey2 != null)
            {
              using (RegistryKey registryKey3 = registryKey2.OpenSubKey("InprocServer32", true))
              {
                if (registryKey3 != null)
                {
                  registryKey3.DeleteValue("Assembly", false);
                  registryKey3.DeleteValue("Class", false);
                  registryKey3.DeleteValue("RuntimeVersion", false);
                  registryKey3.DeleteValue("CodeBase", false);
                  using (RegistryKey registryKey4 = registryKey3.OpenSubKey(strAsmVersion, true))
                  {
                    if (registryKey4 != null)
                    {
                      registryKey4.DeleteValue("Assembly", false);
                      registryKey4.DeleteValue("Class", false);
                      registryKey4.DeleteValue("RuntimeVersion", false);
                      registryKey4.DeleteValue("CodeBase", false);
                      if (registryKey4.SubKeyCount == 0)
                      {
                        if (registryKey4.ValueCount == 0)
                          registryKey3.DeleteSubKey(strAsmVersion);
                      }
                    }
                  }
                  if (registryKey3.SubKeyCount != 0)
                    flag = false;
                  if (registryKey3.SubKeyCount == 0)
                  {
                    if (registryKey3.ValueCount == 0)
                      registryKey2.DeleteSubKey("InprocServer32");
                  }
                }
              }
              if (registryKey2.SubKeyCount == 0)
              {
                if (registryKey2.ValueCount == 0)
                  registryKey1.DeleteSubKey(str);
              }
            }
          }
          if (registryKey1.SubKeyCount == 0)
          {
            if (registryKey1.ValueCount == 0)
              Registry.ClassesRoot.DeleteSubKey("CLSID");
          }
        }
      }
      return flag;
    }

    [SecurityCritical]
    private void RegisterPrimaryInteropAssembly(RuntimeAssembly assembly, string strAsmCodeBase, PrimaryInteropAssemblyAttribute attr)
    {
      if (assembly.GetPublicKey().Length == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_PIAMustBeStrongNamed"));
      string subkey1 = "{" + Marshal.GetTypeLibGuidForAssembly((Assembly) assembly).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      string subkey2 = attr.MajorVersion.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture) + "." + attr.MinorVersion.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture);
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("TypeLib"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey(subkey1))
        {
          using (RegistryKey subKey3 = subKey2.CreateSubKey(subkey2))
          {
            subKey3.SetValue("PrimaryInteropAssemblyName", (object) assembly.FullName);
            if (strAsmCodeBase == null)
              return;
            subKey3.SetValue("PrimaryInteropAssemblyCodeBase", (object) strAsmCodeBase);
          }
        }
      }
    }

    [SecurityCritical]
    private void UnregisterPrimaryInteropAssembly(Assembly assembly, PrimaryInteropAssemblyAttribute attr)
    {
      string str1 = "{" + Marshal.GetTypeLibGuidForAssembly(assembly).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      int num = attr.MajorVersion;
      string str2 = num.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture);
      string str3 = ".";
      num = attr.MinorVersion;
      string str4 = num.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture);
      string str5 = str2 + str3 + str4;
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("TypeLib", true))
      {
        if (registryKey1 == null)
          return;
        using (RegistryKey registryKey2 = registryKey1.OpenSubKey(str1, true))
        {
          if (registryKey2 != null)
          {
            using (RegistryKey registryKey3 = registryKey2.OpenSubKey(str5, true))
            {
              if (registryKey3 != null)
              {
                registryKey3.DeleteValue("PrimaryInteropAssemblyName", false);
                registryKey3.DeleteValue("PrimaryInteropAssemblyCodeBase", false);
                if (registryKey3.SubKeyCount == 0)
                {
                  if (registryKey3.ValueCount == 0)
                    registryKey2.DeleteSubKey(str5);
                }
              }
            }
            if (registryKey2.SubKeyCount == 0)
            {
              if (registryKey2.ValueCount == 0)
                registryKey1.DeleteSubKey(str1);
            }
          }
        }
        if (registryKey1.SubKeyCount != 0 || registryKey1.ValueCount != 0)
          return;
        Registry.ClassesRoot.DeleteSubKey("TypeLib");
      }
    }

    private void EnsureManagedCategoryExists()
    {
      if (RegistrationServices.ManagedCategoryExists())
        return;
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("Component Categories"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}"))
          subKey2.SetValue("0", (object) ".NET Category");
      }
    }

    private static bool ManagedCategoryExists()
    {
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("Component Categories", RegistryKeyPermissionCheck.ReadSubTree))
      {
        if (registryKey1 == null)
          return false;
        using (RegistryKey registryKey2 = registryKey1.OpenSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}", RegistryKeyPermissionCheck.ReadSubTree))
        {
          if (registryKey2 == null)
            return false;
          object obj = registryKey2.GetValue("0");
          if (obj == null || obj.GetType() != typeof (string))
            return false;
          if ((string) obj != ".NET Category")
            return false;
        }
      }
      return true;
    }

    [SecurityCritical]
    private void CallUserDefinedRegistrationMethod(Type type, bool bRegister)
    {
      bool flag = false;
      Type attributeType = !bRegister ? typeof (ComUnregisterFunctionAttribute) : typeof (ComRegisterFunctionAttribute);
      for (Type type1 = type; !flag && type1 != (Type) null; type1 = type1.BaseType)
      {
        MethodInfo[] methods = type1.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        int length = methods.Length;
        for (int index = 0; index < length; ++index)
        {
          MethodInfo methodInfo = methods[index];
          if (methodInfo.GetCustomAttributes(attributeType, true).Length != 0)
          {
            if (!methodInfo.IsStatic)
            {
              if (bRegister)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NonStaticComRegFunction", (object) methodInfo.Name, (object) type1.Name));
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NonStaticComUnRegFunction", (object) methodInfo.Name, (object) type1.Name));
            }
            ParameterInfo[] parameters1 = methodInfo.GetParameters();
            if (methodInfo.ReturnType != typeof (void) || parameters1 == null || parameters1.Length != 1 || parameters1[0].ParameterType != typeof (string) && parameters1[0].ParameterType != typeof (Type))
            {
              if (bRegister)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InvalidComRegFunctionSig", (object) methodInfo.Name, (object) type1.Name));
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InvalidComUnRegFunctionSig", (object) methodInfo.Name, (object) type1.Name));
            }
            if (flag)
            {
              if (bRegister)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MultipleComRegFunctions", (object) type1.Name));
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MultipleComUnRegFunctions", (object) type1.Name));
            }
            object[] parameters2 = new object[1]
            {
              !(parameters1[0].ParameterType == typeof (string)) ? (object) type : (object) ("HKEY_CLASSES_ROOT\\CLSID\\{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}")
            };
            methodInfo.Invoke((object) null, parameters2);
            flag = true;
          }
        }
      }
    }

    private Type GetBaseComImportType(Type type)
    {
      while (type != (Type) null && !type.IsImport)
        type = type.BaseType;
      return type;
    }

    private bool IsRegisteredAsValueType(Type type)
    {
      return type.IsValueType;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void RegisterTypeForComClientsNative(Type type, ref Guid g);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int RegisterTypeForComClientsExNative(Type t, RegistrationClassContext clsContext, RegistrationConnectionType flags);

    [DllImport("ole32.dll", CharSet = CharSet.Auto, PreserveSig = false)]
    private static extern void CoRevokeClassObject(int cookie);
  }
}
