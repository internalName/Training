// Decompiled with JetBrains decompiler
// Type: System.Reflection.Assembly
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection
{
  /// <summary>
  ///   Представляет сборку, которая является модулем с возможностью многократного использования, поддержкой версий и встроенным механизмом описания общеязыковой исполняющей среды.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Assembly))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class Assembly : _Assembly, IEvidenceFactory, ICustomAttributeProvider, ISerializable
  {
    /// <summary>
    ///   Создает имя типа, который определяется отображаемым именем его сборки.
    /// </summary>
    /// <param name="assemblyName">Отображаемое имя сборки.</param>
    /// <param name="typeName">Полное имя типа.</param>
    /// <returns>
    ///   Полное имя типа, дополненное отображаемым именем сборки.
    /// </returns>
    public static string CreateQualifiedName(string assemblyName, string typeName)
    {
      return typeName + ", " + assemblyName;
    }

    /// <summary>
    ///   Возвращает текущую загруженную сборку, в которой определяется заданный тип.
    /// </summary>
    /// <param name="type">
    ///   Объект, представляющий тип в сборке, которая будет возвращена.
    /// </param>
    /// <returns>Сборка, в которой определяется заданный тип.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    public static Assembly GetAssembly(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      Module module = type.Module;
      if (module == (Module) null)
        return (Assembly) null;
      return module.Assembly;
    }

    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    /// <param name="left">
    ///   Сборка, сравниваемая со значением <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   Сборка, сравниваемая со значением <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(Assembly left, Assembly right)
    {
      if ((object) left == (object) right)
        return true;
      if ((object) left == null || (object) right == null || (left is RuntimeAssembly || right is RuntimeAssembly))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    /// <param name="left">
    ///   Сборка, сравниваемая со значением <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   Сборка, сравниваемая со значением <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(Assembly left, Assembly right)
    {
      return !(left == right);
    }

    /// <summary>Определяет равенство сборки и заданного объекта.</summary>
    /// <param name="o">Объект, сравниваемый с данным экземпляром.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="o" /> равно данному экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      return base.Equals(o);
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>Загружает сборку с заданным именем или путем.</summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего манифест сборки, либо путь к нему.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл <paramref name="assemblyFile" /> не найден, или модуль, который вы пытаетесь загрузить, не указывает расширение имени файла.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой; например, это 32-разрядная сборка в 64-разрядном процессе.
    ///    Дополнительные сведения см. в разделе исключений.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   База кода, которая не начинается с "file://", была указана без требуемого <see cref="T:System.Net.WebPermission" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="assemblyFile" /> представляет собой пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина имени сборки больше числа знаков, заданных в MAX_PATH.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadFrom(string assemblyFile)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, (Evidence) null, (byte[]) null, AssemblyHashAlgorithm.None, false, false, ref stackMark);
    }

    /// <summary>
    ///   Загружает сборку с заданным путем в контекст, предназначенный только для отражения.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Путь к файлу, содержащему манифест сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл <paramref name="assemblyFile" /> не найден, или модуль, который вы пытаетесь загрузить, не указывает расширение имени файла.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Файл <paramref name="assemblyFile" /> найден, но его не удается загрузить.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Файл <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   База кода, которая не начинается с "file://", была указана без требуемого разрешения <see cref="T:System.Net.WebPermission" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина имени сборки больше числа знаков, заданных в MAX_PATH.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="assemblyFile" /> является пустой строкой ("").
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, (Evidence) null, (byte[]) null, AssemblyHashAlgorithm.None, true, false, ref stackMark);
    }

    /// <summary>
    ///   Загружает сборку, заданную именем файла или путем к этому файлу и предоставленным свидетельством безопасности.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего манифест сборки, либо путь к нему.
    /// </param>
    /// <param name="securityEvidence">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл <paramref name="assemblyFile" /> не найден, или модуль, который вы пытаетесь загрузить, не указывает расширение имени файла.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить найденный файл.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="securityEvidence" /> не является неоднозначным и определяется как недопустимый.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой (например, это 32-разрядная сборка в 64-разрядном процессе).
    ///    Дополнительные сведения см. в разделе исключений.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   База кода, которая не начинается с "file://", была указана без требуемого <see cref="T:System.Net.WebPermission" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="assemblyFile" /> представляет собой пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина имени сборки больше числа знаков, заданных в MAX_PATH.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, securityEvidence, (byte[]) null, AssemblyHashAlgorithm.None, false, false, ref stackMark);
    }

    /// <summary>
    ///   Загружает сборку с заданным именем файла сборки или путем, свидетельством безопасности, хэш-значением и хэш-алгоритмом.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего манифест сборки, либо путь к нему.
    /// </param>
    /// <param name="securityEvidence">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <param name="hashValue">Значение вычисленного хэш-кода.</param>
    /// <param name="hashAlgorithm">
    ///   Алгоритм хэширования, используемый для хэширования файлов и генерации строгого имени.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл <paramref name="assemblyFile" /> не найден, или модуль, который вы пытаетесь загрузить, не указывает расширение имени файла.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить найденный файл.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="securityEvidence" /> не является неоднозначным и определяется как недопустимый.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой (например, это 32-разрядная сборка в 64-разрядном процессе).
    ///    Дополнительные сведения см. в разделе исключений.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   База кода, которая не начинается с "file://", была указана без требуемого <see cref="T:System.Net.WebPermission" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="assemblyFile" /> представляет собой пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина имени сборки больше числа знаков, заданных в MAX_PATH.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, securityEvidence, hashValue, hashAlgorithm, false, false, ref stackMark);
    }

    /// <summary>
    ///   Загружает сборку с заданным именем файла сборки или путем, хэш-значением и хэш-алгоритмом.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего манифест сборки, либо путь к нему.
    /// </param>
    /// <param name="hashValue">Значение вычисленного хэш-кода.</param>
    /// <param name="hashAlgorithm">
    ///   Алгоритм хэширования, используемый для хэширования файлов и генерации строгого имени.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Параметр <paramref name="assemblyFile" /> не найден, или модуль, который вы пытаетесь загрузить, не указывает расширение имени файла.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой; например, это 32-разрядная сборка в 64-разрядном процессе.
    ///    Дополнительные сведения см. в разделе исключений.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   База кода, которая не начинается с "file://", была указана без требуемого <see cref="T:System.Net.WebPermission" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="assemblyFile" /> представляет собой пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина имени сборки больше числа знаков, заданных в MAX_PATH.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, (Evidence) null, hashValue, hashAlgorithm, false, false, ref stackMark);
    }

    /// <summary>
    ///   Загружает сборку в контекст, из которого производится загрузка, обходя некоторые проверки безопасности.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего манифест сборки, либо путь к нему.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл <paramref name="assemblyFile" /> не найден, или модуль, который вы пытаетесь загрузить, не указывает расширение имени файла.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимым именем сборки
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   База кода, которая не начинается с "file://", была указана без требуемого <see cref="T:System.Net.WebPermission" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="assemblyFile" /> представляет собой пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Длина имени сборки больше числа знаков, заданных в MAX_PATH.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly UnsafeLoadFrom(string assemblyFile)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, (Evidence) null, (byte[]) null, AssemblyHashAlgorithm.None, false, true, ref stackMark);
    }

    /// <summary>Загружает сборку, заданную длинной формой ее имени.</summary>
    /// <param name="assemblyString">Длинная форма имени сборки.</param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyString" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="assemblyString" /> представляет собой строку нулевой длины.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyString" /> не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyString" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyString" /> была скомпилирована в более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(string assemblyString)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, (Evidence) null, ref stackMark, false);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static Type GetType_Compat(string assemblyString, string typeName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      RuntimeAssembly assemblyFromResolveEvent;
      AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, false, out assemblyFromResolveEvent);
      if ((Assembly) assemblyFromResolveEvent == (Assembly) null)
      {
        if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
          return Type.GetType(typeName + ", " + assemblyString, true, false);
        assemblyFromResolveEvent = RuntimeAssembly.InternalLoadAssemblyName(assemblyName, (Evidence) null, (RuntimeAssembly) null, ref stackMark, true, false, false);
      }
      return assemblyFromResolveEvent.GetType(typeName, true, false);
    }

    /// <summary>
    ///   Загружает сборку с заданным отображаемым именем в контекст, предназначенный только для отражения.
    /// </summary>
    /// <param name="assemblyString">
    ///   Отображаемое имя сборки, возвращаемое свойством <see cref="P:System.Reflection.AssemblyName.FullName" />.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyString" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="assemblyString" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyString" /> не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   <paramref name="assemblyString" /> найдена, но не может быть загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyString" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyString" /> был скомпилирован в более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly ReflectionOnlyLoad(string assemblyString)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, (Evidence) null, ref stackMark, true);
    }

    /// <summary>
    ///   Загружает сборку с заданным отображаемым именем. Сборка загружается в домен вызывающего объекта с использованием переданного основания.
    /// </summary>
    /// <param name="assemblyString">Отображаемое имя сборки.</param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyString" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyString" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyString" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyString" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// 
    ///   -или-
    /// 
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(string assemblyString, Evidence assemblySecurity)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, assemblySecurity, ref stackMark, false);
    }

    /// <summary>
    ///   Загружает сборку с заданным именем <see cref="T:System.Reflection.AssemblyName" />.
    /// </summary>
    /// <param name="assemblyRef">
    ///   Объект, описывающий загружаемую сборку.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyRef" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyRef" /> не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.IO.IOException" />.
    /// 
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyRef" /> не является допустимой сборкой.
    ///    -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyRef" /> была скомпилирована в более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(AssemblyName assemblyRef)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, (Evidence) null, (RuntimeAssembly) null, ref stackMark, true, false, false);
    }

    /// <summary>
    ///   Загружает сборку с заданным именем <see cref="T:System.Reflection.AssemblyName" />.
    ///    Сборка загружена в домен вызывающего объекта с использованием переданного свидетельства.
    /// </summary>
    /// <param name="assemblyRef">
    ///   Объект, описывающий загружаемую сборку.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyRef" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyRef" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyRef" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyRef" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, assemblySecurity, (RuntimeAssembly) null, ref stackMark, true, false, false);
    }

    /// <summary>
    ///   Загружает сборку из каталога приложения или из глобального кэша сборок с использованием частичного имени.
    /// </summary>
    /// <param name="partialName">Отображаемое имя сборки.</param>
    /// <returns>
    ///   Загруженная сборка.
    ///    Если значение <paramref name="partialName" /> не найдено, этот метод возвращает значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="partialName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="partialName" /> была скомпилирована в более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadWithPartialName(string partialName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.LoadWithPartialNameInternal(partialName, (Evidence) null, ref stackMark);
    }

    /// <summary>
    ///   Загружает сборку из каталога приложения или из глобального кэша сборок с использованием частичного имени.
    ///    Сборка загружена в домен вызывающего объекта с использованием переданного свидетельства.
    /// </summary>
    /// <param name="partialName">Отображаемое имя сборки.</param>
    /// <param name="securityEvidence">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>
    ///   Загруженная сборка.
    ///    Если значение <paramref name="partialName" /> не найдено, этот метод возвращает значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными наборами свидетельств.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="partialName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="partialName" /> была скомпилирована в более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.LoadWithPartialNameInternal(partialName, securityEvidence, ref stackMark);
    }

    /// <summary>
    ///   Загружает сборку с образом в формате COFF, содержащим порожденную сборку.
    ///    Сборка загружается в домен приложения вызывающего объекта.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив байтов, который является образом в формате COFF, содержащим созданную сборку.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rawAssembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawAssembly" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="rawAssembly" /> была скомпилирована в более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(byte[] rawAssembly)
    {
      AppDomain.CheckLoadByteArraySupported();
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, (byte[]) null, (Evidence) null, ref stackMark, false, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Загружает сборку из образа в формате COFF, содержащего порожденную сборку.
    ///    Сборка загружается в контекст, предназначенный только для отражения, который относится к домену приложения вызывающего объекта.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив байтов, который является образом в формате COFF, содержащим созданную сборку.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rawAssembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawAssembly" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="rawAssembly" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   <paramref name="rawAssembly" /> не удается загрузить.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
    {
      AppDomain.CheckReflectionOnlyLoadSupported();
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, (byte[]) null, (Evidence) null, ref stackMark, true, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Загружает сборку с образом в формате COFF, содержащим выпущенную сборку и (дополнительно) символы для сборки.
    ///    Сборка загружается в домен приложения вызывающего объекта.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив байтов, который является образом в формате COFF, содержащим созданную сборку.
    /// </param>
    /// <param name="rawSymbolStore">
    ///   Массив байтов, содержащий необработанные байты, которые представляют символы для сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rawAssembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawAssembly" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="rawAssembly" /> была скомпилирована в более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
    {
      AppDomain.CheckLoadByteArraySupported();
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, (Evidence) null, ref stackMark, false, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Загружает сборку с образом в формате COFF, содержащим выпущенную сборку и необязательно содержащим символы и задающим источник для контекста безопасности.
    ///    Сборка загружается в домен приложения вызывающего объекта.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив байтов, который является образом в формате COFF, содержащим созданную сборку.
    /// </param>
    /// <param name="rawSymbolStore">
    ///   Массив байтов, содержащий необработанные байты, которые представляют символы для сборки.
    /// </param>
    /// <param name="securityContextSource">
    ///   Источник контекста безопасности.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rawAssembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawAssembly" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="rawAssembly" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="securityContextSource" /> не является одним из значений перечисления.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
    {
      AppDomain.CheckLoadByteArraySupported();
      switch (securityContextSource)
      {
        case SecurityContextSource.CurrentAppDomain:
        case SecurityContextSource.CurrentAssembly:
          StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
          return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, (Evidence) null, ref stackMark, false, false, securityContextSource);
        default:
          throw new ArgumentOutOfRangeException(nameof (securityContextSource));
      }
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static Assembly LoadImageSkipIntegrityCheck(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
    {
      AppDomain.CheckLoadByteArraySupported();
      switch (securityContextSource)
      {
        case SecurityContextSource.CurrentAppDomain:
        case SecurityContextSource.CurrentAssembly:
          StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
          return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, (Evidence) null, ref stackMark, false, true, securityContextSource);
        default:
          throw new ArgumentOutOfRangeException(nameof (securityContextSource));
      }
    }

    /// <summary>
    ///   Загружает сборку с образом в формате COFF, содержащим выпущенную сборку и (необязательно) символы и свидетельство для сборки.
    ///    Сборка загружается в домен приложения вызывающего объекта.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив байтов, который является образом в формате COFF, содержащим созданную сборку.
    /// </param>
    /// <param name="rawSymbolStore">
    ///   Массив байтов, содержащий необработанные байты, которые представляют символы для сборки.
    /// </param>
    /// <param name="securityEvidence">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rawAssembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawAssembly" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="rawAssembly" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="securityEvidence" /> не является <see langword="null" />.
    ///     По умолчанию устаревшая политика CAS не включена в .NET Framework 4; если она не включена, параметр <paramref name="securityEvidence" /> должен иметь значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
    public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
    {
      AppDomain.CheckLoadByteArraySupported();
      if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
      {
        Zone hostEvidence = securityEvidence.GetHostEvidence<Zone>();
        if (hostEvidence == null || hostEvidence.SecurityZone != SecurityZone.MyComputer)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      }
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, securityEvidence, ref stackMark, false, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Загружает содержимое файла сборки, находящегося по указанному пути.
    /// </summary>
    /// <param name="path">Полный путь к загружаемому файлу.</param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="path" /> не является абсолютным путем.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Параметр <paramref name="path" /> представляет собой пустую строку ("") или не существует.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="path" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="path" /> была скомпилирована в более поздней версии.
    /// </exception>
    [SecuritySafeCritical]
    public static Assembly LoadFile(string path)
    {
      AppDomain.CheckLoadFileSupported();
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, path).Demand();
      return (Assembly) RuntimeAssembly.nLoadFile(path, (Evidence) null);
    }

    /// <summary>
    ///   Загружает сборку с заданным путем в домен вызывающего объекта с использованием переданного свидетельства.
    /// </summary>
    /// <param name="path">Полный путь к файлу сборки.</param>
    /// <param name="securityEvidence">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент <paramref name="path" /> не является абсолютным путем.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Параметр <paramref name="path" /> представляет собой пустую строку ("") или не существует.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="path" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="path" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="securityEvidence" /> не является <see langword="null" />.
    ///    По умолчанию устаревшая политика CAS не включена в .NET Framework 4; если она не включена, параметр <paramref name="securityEvidence" /> должен иметь значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFile which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
    public static Assembly LoadFile(string path, Evidence securityEvidence)
    {
      AppDomain.CheckLoadFileSupported();
      if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, path).Demand();
      return (Assembly) RuntimeAssembly.nLoadFile(path, securityEvidence);
    }

    /// <summary>
    ///   Получает сборку, которая содержит выполняемый в текущий момент код.
    /// </summary>
    /// <returns>
    ///   Сборка, содержащая выполняемый в текущий момент код.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly GetExecutingAssembly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.GetExecutingAssembly(ref stackMark);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.Assembly" /> метода, вызвавшего текущий выполняемый метод.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="Assembly" /> метода, вызвавшего выполняющийся в текущий момент метод.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly GetCallingAssembly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCallersCaller;
      return (Assembly) RuntimeAssembly.GetExecutingAssembly(ref stackMark);
    }

    /// <summary>
    ///   Получает процесс, исполняемый в домене приложения по умолчанию.
    ///    В других доменах приложений это первый исполняемый процесс, который был выполнен методом <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />.
    /// </summary>
    /// <returns>
    ///   Сборка, представляющая собой исполняемый файл процесса в домене приложения по умолчанию или первый исполняемый файл, выполненный методом <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />.
    ///    Может возвратить значение <see langword="null" /> при вызове из неуправляемого кода.
    /// </returns>
    [SecuritySafeCritical]
    public static Assembly GetEntryAssembly()
    {
      return (AppDomain.CurrentDomain.DomainManager ?? new AppDomainManager()).EntryAssembly;
    }

    /// <summary>
    ///   Возникает, если загрузчик классов общеязыковой среды выполнения не может обработать ссылку на внутренний модуль сборки, используя обычные средства.
    /// </summary>
    public virtual event ModuleResolveEventHandler ModuleResolve
    {
      [SecurityCritical] add
      {
        throw new NotImplementedException();
      }
      [SecurityCritical] remove
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Получает первоначально заданное расположение сборки, например в объекте <see cref="T:System.Reflection.AssemblyName" />.
    /// </summary>
    /// <returns>Первоначально заданное расположение сборки.</returns>
    public virtual string CodeBase
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Получает универсальный код доступа (URI), предоставляющий базовый код, включая escape-символы.
    /// </summary>
    /// <returns>Универсальный код доступа (URI) с escape-символами.</returns>
    public virtual string EscapedCodeBase
    {
      [SecuritySafeCritical] get
      {
        return AssemblyName.EscapeCodeBase(this.CodeBase);
      }
    }

    /// <summary>
    ///   Получает имя <see cref="T:System.Reflection.AssemblyName" /> для этой сборки.
    /// </summary>
    /// <returns>
    ///   Объект, содержащий полностью проанализированное отображаемое имя для этой сборки.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual AssemblyName GetName()
    {
      return this.GetName(false);
    }

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Reflection.AssemblyName" /> для этой сборки, база кода устанавливается в соответствии с параметром <paramref name="copiedName" />.
    /// </summary>
    /// <param name="copiedName">
    ///   Значение <see langword="true" />, чтобы значение свойства <see cref="P:System.Reflection.Assembly.CodeBase" /> указывало на расположение сборки после создания ее теневой копии; значение <see langword="false" />, если значение свойства <see cref="P:System.Reflection.Assembly.CodeBase" /> должно указывать на первоначальное расположение.
    /// </param>
    /// <returns>
    ///   Объект, содержащий полностью проанализированное отображаемое имя для этой сборки.
    /// </returns>
    public virtual AssemblyName GetName(bool copiedName)
    {
      throw new NotImplementedException();
    }

    /// <summary>Получает отображаемое имя сборки.</summary>
    /// <returns>Отображаемое имя сборки.</returns>
    [__DynamicallyInvokable]
    public virtual string FullName
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>Получает точку входа для этой сборки.</summary>
    /// <returns>
    ///   Объект, представляющий точку входа этой сборки.
    ///    Если точка входа не найдена (например, сборка является DLL-библиотекой), возвращается значение <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo EntryPoint
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    Type _Assembly.GetType()
    {
      return this.GetType();
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" /> с указанным именем в экземпляре сборки.
    /// </summary>
    /// <param name="name">Полное имя типа.</param>
    /// <returns>
    ///   Объект, представляющий указанный класс, или <see langword="null" />, если класс не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> недопустим.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Для <paramref name="name" /> требуется зависимая сборка, которую не удалось найти.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.IO.IOException" />.
    /// 
    ///   Для <paramref name="name" /> требуется зависимая сборка, которая была найдена, но ее не удалось загрузить.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения, а для <paramref name="name" /> требуется зависимая сборка, которая не была предварительно загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Для <paramref name="name" /> требуется зависимая сборка, однако файл не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="name" /> требуется зависимая сборка, которая была скомпилирована для версии среды выполнения более поздней, чем текущая загруженная версия.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type GetType(string name)
    {
      return this.GetType(name, false, false);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" /> с заданным именем в экземпляре сборки и может вызывать исключение, если тип не найден.
    /// </summary>
    /// <param name="name">Полное имя типа.</param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" /> для создания исключения, если тип не найден, в обратном случае — значение <see langword="false" />, в результате чего будет возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>Объект, представляющий указанный класс.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="name" /> превышает 1024 символа.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="throwOnError" /> является <see langword="true" />, не удается найти тип.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Для <paramref name="name" /> требуется зависимая сборка, которую не удается найти.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Для <paramref name="name" /> требуется зависимая сборка, которая была найдена, но ее не удалось загрузить.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения, а для <paramref name="name" /> требуется зависимая сборка, которая не была предварительно загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Для <paramref name="name" /> требуется зависимая сборка, однако файл не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="name" /> требуется зависимая сборка, которая была скомпилирована для версии среды выполнения более поздней, чем текущая загруженная версия.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type GetType(string name, bool throwOnError)
    {
      return this.GetType(name, throwOnError, false);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" /> с заданным именем в экземпляре сборки, с возможностями игнорировать регистр и вызвать исключение, если тип не найден.
    /// </summary>
    /// <param name="name">Полное имя типа.</param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" /> для создания исключения, если тип не найден, в обратном случае — значение <see langword="false" />, в результате чего будет возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы игнорировать регистр имени типа, в обратном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Объект, представляющий указанный класс.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="name" /> превышает 1024 символа.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="throwOnError" /> является <see langword="true" />, не удается найти тип.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Для <paramref name="name" /> требуется зависимая сборка, которую не удается найти.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Для <paramref name="name" /> требуется зависимая сборка, которая была найдена, но ее не удалось загрузить.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения, а для <paramref name="name" /> требуется зависимая сборка, которая не была предварительно загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Для <paramref name="name" /> требуется зависимая сборка, однако файл не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="name" /> требуется зависимая сборка, которая была скомпилирована для версии среды выполнения более поздней, чем текущая загруженная версия.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Получает коллекцию открытых типов, определенных в этой сборке и видимых за ее пределами.
    /// </summary>
    /// <returns>
    ///   Коллекция открытых типов, определенных в этой сборке и видимых за ее пределами.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<Type> ExportedTypes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<Type>) this.GetExportedTypes();
      }
    }

    /// <summary>
    ///   Получает открытые типы, определенные в этой сборке и видимые за ее пределами.
    /// </summary>
    /// <returns>
    ///   Массив, представляющий типы, определенные в сборке и видимые за ее пределами.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Сборка является динамической.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type[] GetExportedTypes()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Получает коллекцию типов, определенных в этой сборке.
    /// </summary>
    /// <returns>Коллекция типов, определенных в этой сборке.</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<TypeInfo> DefinedTypes
    {
      [__DynamicallyInvokable] get
      {
        Type[] types = this.GetTypes();
        TypeInfo[] typeInfoArray = new TypeInfo[types.Length];
        for (int index = 0; index < types.Length; ++index)
        {
          TypeInfo typeInfo = types[index].GetTypeInfo();
          if ((Type) typeInfo == (Type) null)
            throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoTypeInfo", (object) types[index].FullName));
          typeInfoArray[index] = typeInfo;
        }
        return (IEnumerable<TypeInfo>) typeInfoArray;
      }
    }

    /// <summary>Получает типы, определенные в этой сборке.</summary>
    /// <returns>
    ///   Массив, содержащий все типы, определенные в этой сборке.
    /// </returns>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">
    ///   Сборка содержит один или несколько типов, которые не удается загрузить.
    ///    Массив, возвращаемый свойством <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> этого исключения, содержит объект <see cref="T:System.Type" /> для каждого типа, который был загружен, и объект <see langword="null" /> для каждого типа, который не удалось загрузить, тогда как свойство <see cref="P:System.Reflection.ReflectionTypeLoadException.LoaderExceptions" /> содержит исключение для каждого типа, который не удалось загрузить.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type[] GetTypes()
    {
      Module[] modules = this.GetModules(false);
      int length1 = modules.Length;
      int length2 = 0;
      Type[][] typeArray1 = new Type[length1][];
      for (int index = 0; index < length1; ++index)
      {
        typeArray1[index] = modules[index].GetTypes();
        length2 += typeArray1[index].Length;
      }
      int destinationIndex = 0;
      Type[] typeArray2 = new Type[length2];
      for (int index = 0; index < length1; ++index)
      {
        int length3 = typeArray1[index].Length;
        Array.Copy((Array) typeArray1[index], 0, (Array) typeArray2, destinationIndex, length3);
        destinationIndex += length3;
      }
      return typeArray2;
    }

    /// <summary>
    ///   Загружает из сборки указанный ресурс манифеста с учетом ограничения области действия пространства имен по типу.
    /// </summary>
    /// <param name="type">
    ///   Тип, пространством имен которого ограничена область действия имени ресурса манифеста.
    /// </param>
    /// <param name="name">
    ///   Имя запрашиваемого ресурса манифеста, зависящее от регистра.
    /// </param>
    /// <returns>
    ///   Ресурс манифеста; или значение <see langword="null" />, если при компиляции не были заданы ресурсы или ресурс не является видимым для вызывающего объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> представляет собой пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить найденный файл.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="name" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="name" /> не является допустимой сборкой.
    /// </exception>
    /// <exception cref="T:System.NotImplementedException">
    ///   Длина ресурса больше <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Stream GetManifestResourceStream(Type type, string name)
    {
      throw new NotImplementedException();
    }

    /// <summary>Загружает указанный ресурс манифеста из сборки.</summary>
    /// <param name="name">
    ///   Имя запрашиваемого ресурса манифеста, зависящее от регистра.
    /// </param>
    /// <returns>
    ///   Ресурс манифеста; или значение <see langword="null" />, если при компиляции не были заданы ресурсы или ресурс не является видимым для вызывающего объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> представляет собой пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.IO.IOException" />.
    /// 
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="name" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="name" /> не является допустимой сборкой.
    /// </exception>
    /// <exception cref="T:System.NotImplementedException">
    ///   Длина ресурса больше <see cref="F:System.Int64.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Stream GetManifestResourceStream(string name)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Получает сопутствующую сборку для указанной культуры.
    /// </summary>
    /// <param name="culture">
    ///   Заданные язык и региональные параметры.
    /// </param>
    /// <returns>Указанная вспомогательная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти сборку.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Вспомогательная сборка с соответствующим именем файла была найдена, но параметр <see langword="CultureInfo" /> не соответствует указанному.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Вспомогательная сборка не является допустимой сборкой.
    /// </exception>
    public virtual Assembly GetSatelliteAssembly(CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Получает указанную версию вспомогательной сборки для указанной культуры.
    /// </summary>
    /// <param name="culture">
    ///   Заданные язык и региональные параметры.
    /// </param>
    /// <param name="version">Версия вспомогательной сборки.</param>
    /// <returns>Указанная вспомогательная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Вспомогательная сборка с соответствующим именем файла была найдена, но <see langword="CultureInfo" /> или версия не соответствуют указанным.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти сборку.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Вспомогательная сборка не является допустимой сборкой.
    /// </exception>
    public virtual Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
    {
      throw new NotImplementedException();
    }

    /// <summary>Получает свидетельство для этой сборки.</summary>
    /// <returns>Свидетельство для этой сборки.</returns>
    public virtual Evidence Evidence
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>Получает набор разрешений текущей сборки.</summary>
    /// <returns>Набор разрешений текущей сборки.</returns>
    public virtual PermissionSet PermissionSet
    {
      [SecurityCritical] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, загружена ли текущая сборка с полным доверием.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущая сборка загружена с полным доверием; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsFullyTrusted
    {
      [SecuritySafeCritical] get
      {
        return this.PermissionSet.IsUnrestricted();
      }
    }

    /// <summary>
    ///   Получает значение, указывающее набор правил безопасности, которые применяются средой CLR к данной сборке.
    /// </summary>
    /// <returns>
    ///   Набор правил безопасности, которые применяются средой CLR к данной сборке.
    /// </returns>
    public virtual SecurityRuleSet SecurityRuleSet
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Получает сведения сериализации со всеми данными, необходимыми для повторного создания экземпляра этой сборки.
    /// </summary>
    /// <param name="info">
    ///   Объект, для которого будут заполнены сведения о сериализации.
    /// </param>
    /// <param name="context">Контекст назначения сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает модуль, содержащий манифест текущей сборки.
    /// </summary>
    /// <returns>Модуль, содержащий манифест текущей сборки.</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual Module ManifestModule
    {
      [__DynamicallyInvokable] get
      {
        RuntimeAssembly runtimeAssembly = this as RuntimeAssembly;
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly.ManifestModule;
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Получает коллекцию, содержащую пользовательские атрибуты этой сборки.
    /// </summary>
    /// <returns>
    ///   Коллекция, содержащая пользовательские атрибуты этой сборки.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<CustomAttributeData> CustomAttributes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<CustomAttributeData>) this.GetCustomAttributesData();
      }
    }

    /// <summary>
    ///   Получает все настраиваемые атрибуты для этой сборки.
    /// </summary>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов типа <see cref="T:System.Reflection.Assembly" />.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты для этой сборки.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual object[] GetCustomAttributes(bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Получает настраиваемые атрибуты для этой сборки как заданные по типу.
    /// </summary>
    /// <param name="attributeType">
    ///   Тип, для которого должны быть возвращены настраиваемые атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов типа <see cref="T:System.Reflection.Assembly" />.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты для этой сборки, заданные параметром <paramref name="attributeType" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является типом времени выполнения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>Указывает, применен ли конкретный атрибут к сборке.</summary>
    /// <param name="attributeType">
    ///   Тип атрибута, который нужно проверить для данной сборки.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если атрибут был применен к сборке; в обратном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> использует недопустимый тип.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает сведения об атрибутах, примененных к текущему объекту <see cref="T:System.Reflection.Assembly" />; сведения представляют собой объекты <see cref="T:System.Reflection.CustomAttributeData" />.
    /// </summary>
    /// <returns>
    ///   Универсальный список объектов <see cref="T:System.Reflection.CustomAttributeData" />, представляющих данные об атрибутах, которые были применены к текущей сборке.
    /// </returns>
    public virtual IList<CustomAttributeData> GetCustomAttributesData()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Boolean" />, которое указывает, была ли эта сборка загружена в контекст, предназначенный только для отражения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если сборка была загружена в контекст, предназначенный только для отражения вместо контекста выполнения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public virtual bool ReflectionOnly
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Загружает модуль, внутренний для этой сборки, с образом в формате COFF, содержащим включенный модуль или файл ресурсов.
    /// </summary>
    /// <param name="moduleName">
    ///   Имя модуля.
    ///    Эта строка должна соответствовать имени файла в манифесте этой сборки.
    /// </param>
    /// <param name="rawModule">
    ///   Массив байтов, который является COFF-образом, содержащим передаваемый модуль или ресурс.
    /// </param>
    /// <returns>Загруженный модуль.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="moduleName" /> или <paramref name="rawModule" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="moduleName" /> не соответствует записи файла в манифесте этой сборки.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawModule" /> не является допустимым модулем.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить найденный файл.
    /// </exception>
    public Module LoadModule(string moduleName, byte[] rawModule)
    {
      return this.LoadModule(moduleName, rawModule, (byte[]) null);
    }

    /// <summary>
    ///   Загружает модуль, внутренний для этой сборки, с образом в формате COFF, содержащим включенный модуль или файл ресурсов.
    ///    Также загружаются необработанные байты, представляющие собой символы для модуля.
    /// </summary>
    /// <param name="moduleName">
    ///   Имя модуля.
    ///    Эта строка должна соответствовать имени файла в манифесте этой сборки.
    /// </param>
    /// <param name="rawModule">
    ///   Массив байтов, который является COFF-образом, содержащим передаваемый модуль или ресурс.
    /// </param>
    /// <param name="rawSymbolStore">
    ///   Массив байтов, содержащий необработанные байты, представляющие собой символы для модуля.
    ///    Для файла ресурсов должно быть задано значение <see langword="null" />.
    /// </param>
    /// <returns>Загруженный модуль.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="moduleName" /> или <paramref name="rawModule" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="moduleName" /> не соответствует записи файла в манифесте этой сборки.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawModule" /> не является допустимым модулем.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить найденный файл.
    /// </exception>
    public virtual Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   С помощью поиска с учетом регистра находит заданный тип в этой сборке и создает его экземпляр, используя абстрактный метод.
    /// </summary>
    /// <param name="typeName">
    ///   <see cref="P:System.Type.FullName" /> искомого типа.
    /// </param>
    /// <returns>
    ///   Экземпляр указанного типа, созданный с использованием конструктора по умолчанию; или <see langword="null" />, если <paramref name="typeName" /> не найден.
    ///    Тип разрешается с использованием связывателя по умолчанию, не задавая языка и региональных параметров, а также атрибутов активации, при этом для объекта <see cref="T:System.Reflection.BindingFlags" /> задано значение <see langword="Public" /> или <see langword="Instance" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение <paramref name="typeName" /> является пустой строкой ("") или строкой, начинающейся с нуль-символа.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, которую не удалось найти.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, которая была найдена, но ее не удалось загрузить.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения, а для <paramref name="typeName" /> требуется зависимая сборка, которая не была предварительно загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, однако файл не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, которая была скомпилирована для версии среды выполнения более поздней, чем текущая загруженная версия.
    /// </exception>
    public object CreateInstance(string typeName)
    {
      return this.CreateInstance(typeName, false, BindingFlags.Instance | BindingFlags.Public, (Binder) null, (object[]) null, (CultureInfo) null, (object[]) null);
    }

    /// <summary>
    ///   При помощи необязательного поиска с учетом регистра находит заданный тип в этой сборке и создает его экземпляр, используя абстрактный метод.
    /// </summary>
    /// <param name="typeName">
    ///   <see cref="P:System.Type.FullName" /> искомого типа.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы игнорировать регистр имени типа, в обратном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Экземпляр указанного типа, созданный с использованием конструктора по умолчанию; или <see langword="null" />, если <paramref name="typeName" /> не найден.
    ///    Тип разрешается с использованием связывателя по умолчанию, не задавая языка и региональных параметров, а также атрибутов активации, при этом для объекта <see cref="T:System.Reflection.BindingFlags" /> задано значение <see langword="Public" /> или <see langword="Instance" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение <paramref name="typeName" /> является пустой строкой ("") или строкой, начинающейся с нуль-символа.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, которую не удается найти.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, которая была найдена, но ее не удалось загрузить.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения, а для <paramref name="typeName" /> требуется зависимая сборка, которая не была предварительно загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, однако файл не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, которая была скомпилирована для версии среды выполнения более поздней, чем текущая загруженная версия.
    /// </exception>
    public object CreateInstance(string typeName, bool ignoreCase)
    {
      return this.CreateInstance(typeName, ignoreCase, BindingFlags.Instance | BindingFlags.Public, (Binder) null, (object[]) null, (CultureInfo) null, (object[]) null);
    }

    /// <summary>
    ///   Находит в сборке указанный тип и создает его экземпляр с использованием системного активатора при помощи необязательного поиска с учетом регистра и с заданными аргументами, культурой, а также атрибутами привязки и активации.
    /// </summary>
    /// <param name="typeName">
    ///   <see cref="P:System.Type.FullName" /> искомого типа.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы игнорировать регистр имени типа, в обратном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение является сочетанием одноразрядных флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив, содержащий аргументы, передаваемые конструктору.
    ///    Этот массив аргументов должен по числу, порядку и типу аргументов соответствовать параметрам вызываемого конструктора.
    ///    Если нужен конструктор по умолчанию, <paramref name="args" /> должен быть пустым массивом или <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр объекта <see langword="CultureInfo" />, используемого для управления приведением типов.
    ///    Если значение этого объекта — <see langword="null" />, для текущего потока используется <see langword="CultureInfo" />.
    ///    (Например, необходимо преобразовывать объект <see langword="String" />, представляющий 1000, в значение <see langword="Double" />, поскольку при разных языках и региональных параметрах 1000 представляется по-разному.)
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    ///     Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Экземпляр указанного типа или <see langword="null" />, если <paramref name="typeName" /> не найден.
    ///    Предоставленные аргументы используются для разрешения типа и привязки конструктора, который используется для создания экземпляра.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение <paramref name="typeName" /> является пустой строкой ("") или строкой, начинающейся с нуль-символа.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Непустой массив атрибутов активации передан в тип, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, которую не удалось найти.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, которая была найдена, но ее не удалось загрузить.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения, а для <paramref name="typeName" /> требуется зависимая сборка, которая не была предварительно загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, однако файл не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="typeName" /> требуется зависимая сборка, которая была скомпилирована для версии среды выполнения более поздней, чем текущая загруженная версия.
    /// </exception>
    public virtual object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      Type type = this.GetType(typeName, false, ignoreCase);
      if (type == (Type) null)
        return (object) null;
      return Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
    }

    /// <summary>
    ///   Получает коллекцию, содержащую модули в этой сборке.
    /// </summary>
    /// <returns>Коллекция, содержащая модули в этой сборке.</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<Module> Modules
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<Module>) this.GetLoadedModules(true);
      }
    }

    /// <summary>
    ///   Получает все загруженные модули, являющиеся частью этой сборки.
    /// </summary>
    /// <returns>Массив модулей.</returns>
    public Module[] GetLoadedModules()
    {
      return this.GetLoadedModules(false);
    }

    /// <summary>
    ///   Получает все загруженные модули, входящие в эту сборку, с заданием возможности включения модулей ресурсов.
    /// </summary>
    /// <param name="getResourceModules">
    ///   Значение <see langword="true" />, если необходимо включать модули ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Массив модулей.</returns>
    public virtual Module[] GetLoadedModules(bool getResourceModules)
    {
      throw new NotImplementedException();
    }

    /// <summary>Получает все модули, являющиеся частью этой сборки.</summary>
    /// <returns>Массив модулей.</returns>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   В загружаемом модуле не указано расширение имени файла.
    /// </exception>
    [__DynamicallyInvokable]
    public Module[] GetModules()
    {
      return this.GetModules(false);
    }

    /// <summary>
    ///   Получает все загруженные модули, входящие в эту сборку, с указанием возможности включения модулей ресурсов.
    /// </summary>
    /// <param name="getResourceModules">
    ///   Значение <see langword="true" />, если необходимо включать модули ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Массив модулей.</returns>
    public virtual Module[] GetModules(bool getResourceModules)
    {
      throw new NotImplementedException();
    }

    /// <summary>Получает указанный модуль этой сборки.</summary>
    /// <param name="name">Имя запрашиваемого модуля.</param>
    /// <returns>
    ///   Запрашиваемый модуль или значение <see langword="null" />, если модуль не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> представляет собой пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить найденный файл.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="name" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="name" /> не является допустимой сборкой.
    /// </exception>
    public virtual Module GetModule(string name)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.IO.FileStream" /> для указанного файла из таблицы файлов манифеста данной сборки.
    /// </summary>
    /// <param name="name">
    ///   Имя указанного файла.
    ///    Не должно содержать путь к файлу.
    /// </param>
    /// <returns>
    ///   Поток, содержащий указанный файл или <see langword="null" />, если файл не найден.
    /// </returns>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> представляет собой пустую строку ("").
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="name" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="name" /> не является допустимой сборкой.
    /// </exception>
    public virtual FileStream GetFile(string name)
    {
      throw new NotImplementedException();
    }

    /// <summary>Получает файлы в таблице файлов манифеста сборки.</summary>
    /// <returns>Массив потоков, содержащий файлы.</returns>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Файл не является допустимой сборкой.
    /// </exception>
    public virtual FileStream[] GetFiles()
    {
      return this.GetFiles(false);
    }

    /// <summary>
    ///   Получает файлы из таблицы манифеста сборки с указанием включать или не включать модули ресурсов.
    /// </summary>
    /// <param name="getResourceModules">
    ///   Значение <see langword="true" />, если необходимо включать модули ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Массив потоков, содержащий файлы.</returns>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Не удалось загрузить файл, который был найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Файл не является допустимой сборкой.
    /// </exception>
    public virtual FileStream[] GetFiles(bool getResourceModules)
    {
      throw new NotImplementedException();
    }

    /// <summary>Возвращает имена всех ресурсов в этой сборке.</summary>
    /// <returns>Массив, который содержит имена всех ресурсов.</returns>
    [__DynamicallyInvokable]
    public virtual string[] GetManifestResourceNames()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает объекты <see cref="T:System.Reflection.AssemblyName" /> для всех сборок, на которые ссылается данная сборка.
    /// </summary>
    /// <returns>
    ///   Массив, содержащий полностью проанализированные отображаемые имена всех сборок, на которые ссылается данная сборка.
    /// </returns>
    public virtual AssemblyName[] GetReferencedAssemblies()
    {
      throw new NotImplementedException();
    }

    /// <summary>Возвращает сведения о сохранении заданного ресурса.</summary>
    /// <param name="resourceName">
    ///   Имя ресурса, зависящее от регистра.
    /// </param>
    /// <returns>
    ///   Объект со сведениями о топологии ресурса или <see langword="null" />, если ресурс не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="resourceName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="resourceName" /> представляет собой пустую строку ("").
    /// </exception>
    [__DynamicallyInvokable]
    public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает полное имя сборки, также называемое отображаемым именем.
    /// </summary>
    /// <returns>
    ///   Полное имя сборки или имя класса, если полное имя сборки не может быть определено.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.FullName ?? base.ToString();
    }

    /// <summary>
    ///   Получает полный путь либо UNC для расположения загруженного файла, содержащего манифест.
    /// </summary>
    /// <returns>
    ///   Местоположение загруженного файла, содержащего манифест.
    ///    Если для загруженного файла был создан снимок состояния, местонахождение является местонахождением файла после теневого копирования.
    ///    Если сборка загружается из массива байтов, например, при использовании метода перегрузки <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" />, возвращаемое значение является пустой строкой ("").
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущая сборка является динамической сборкой, представленной объектом <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.
    /// </exception>
    public virtual string Location
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Получает строку, представляющую версию общеязыковой среды выполнения (CLR), сохраненной в файле, содержащем манифест.
    /// </summary>
    /// <returns>
    ///   Имя папки версии среды CLR.
    ///    Указанный путь не является полным.
    /// </returns>
    [ComVisible(false)]
    public virtual string ImageRuntimeVersion
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, была ли сборка загружена из глобального кэша сборок.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если сборка была загружена из глобального кэша сборок, в обратном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool GlobalAssemblyCache
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Получает контекст хост-сайта, с которым была загружена сборка.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Int64" />, которое указывает контекст узла, с которым была загружена сборка, при его наличии.
    /// </returns>
    [ComVisible(false)]
    public virtual long HostContext
    {
      get
      {
        RuntimeAssembly runtimeAssembly = this as RuntimeAssembly;
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly.HostContext;
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Получает значение, определяющее, была ли текущая сборка создана динамически в текущем процессе с помощью отражения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущая сборка была создана динамически в текущем процессе; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsDynamic
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }
  }
}
