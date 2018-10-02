// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Предоставляет события для разрешения запросов только для отражения типов для типов, предоставляемых файлы метаданных Windows, а также методы для разрешения.
  /// </summary>
  public static class WindowsRuntimeMetadata
  {
    /// <summary>
    ///   Находит файлы метаданных Windows для указанного пространства имен, заданным в указанных местоположениях для поиска.
    /// </summary>
    /// <param name="namespaceName">Пространство имен для решения.</param>
    /// <param name="packageGraphFilePaths">
    ///   Пути для поиска файлов метаданных Windows приложения или <see langword="null" /> Поиск только файлы метаданных Windows из установки операционной системы.
    /// </param>
    /// <returns>
    ///   Перечислимый список строк, представляющих файлы метаданных Windows, которые определяют <paramref name="namespaceName" />.
    /// </returns>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Версия операционной системы не поддерживает Среда выполнения Windows.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="namespaceName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IEnumerable<string> ResolveNamespace(string namespaceName, IEnumerable<string> packageGraphFilePaths)
    {
      return WindowsRuntimeMetadata.ResolveNamespace(namespaceName, (string) null, packageGraphFilePaths);
    }

    /// <summary>
    ///   Находит файлы метаданных Windows для указанного пространства имен, заданным в указанных местоположениях для поиска.
    /// </summary>
    /// <param name="namespaceName">Пространство имен для решения.</param>
    /// <param name="windowsSdkFilePath">
    ///   Путь для поиска файлов метаданных Windows, предоставляемые в SDK или <see langword="null" /> для поиска файлов метаданных Windows из установки операционной системы.
    /// </param>
    /// <param name="packageGraphFilePaths">
    ///   Приложение пути для поиска файлов метаданных Windows.
    /// </param>
    /// <returns>
    ///   Перечислимый список строк, представляющих файлы метаданных Windows, которые определяют <paramref name="namespaceName" />.
    /// </returns>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   Версия операционной системы не поддерживает Среда выполнения Windows.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="namespaceName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IEnumerable<string> ResolveNamespace(string namespaceName, string windowsSdkFilePath, IEnumerable<string> packageGraphFilePaths)
    {
      if (namespaceName == null)
        throw new ArgumentNullException(nameof (namespaceName));
      string[] packageGraphFilePaths1 = (string[]) null;
      if (packageGraphFilePaths != null)
      {
        List<string> stringList = new List<string>(packageGraphFilePaths);
        packageGraphFilePaths1 = new string[stringList.Count];
        int index = 0;
        foreach (string str in stringList)
        {
          packageGraphFilePaths1[index] = str;
          ++index;
        }
      }
      string[] o = (string[]) null;
      WindowsRuntimeMetadata.nResolveNamespace(namespaceName, windowsSdkFilePath, packageGraphFilePaths1, packageGraphFilePaths1 == null ? 0 : packageGraphFilePaths1.Length, JitHelpers.GetObjectHandleOnStack<string[]>(ref o));
      return (IEnumerable<string>) o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void nResolveNamespace(string namespaceName, string windowsSdkFilePath, string[] packageGraphFilePaths, int cPackageGraphFilePaths, ObjectHandleOnStack retFileNames);

    /// <summary>
    ///   Возникает при сбое разрешения файла метаданных Windows в контекст, поддерживающий только отражение.
    /// </summary>
    public static event EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve;

    internal static RuntimeAssembly[] OnReflectionOnlyNamespaceResolveEvent(AppDomain appDomain, RuntimeAssembly assembly, string namespaceName)
    {
      // ISSUE: reference to a compiler-generated field
      EventHandler<NamespaceResolveEventArgs> namespaceResolve = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
      if (namespaceResolve != null)
      {
        Delegate[] invocationList = namespaceResolve.GetInvocationList();
        int length = invocationList.Length;
        for (int index1 = 0; index1 < length; ++index1)
        {
          NamespaceResolveEventArgs e = new NamespaceResolveEventArgs(namespaceName, (Assembly) assembly);
          ((EventHandler<NamespaceResolveEventArgs>) invocationList[index1])((object) appDomain, e);
          Collection<Assembly> resolvedAssemblies = e.ResolvedAssemblies;
          if (resolvedAssemblies.Count > 0)
          {
            RuntimeAssembly[] runtimeAssemblyArray = new RuntimeAssembly[resolvedAssemblies.Count];
            int index2 = 0;
            foreach (Assembly asm in resolvedAssemblies)
            {
              runtimeAssemblyArray[index2] = AppDomain.GetRuntimeAssembly(asm);
              ++index2;
            }
            return runtimeAssemblyArray;
          }
        }
      }
      return (RuntimeAssembly[]) null;
    }

    /// <summary>
    ///   Возникает при сбое разрешения файла метаданных Windows в среде разработки.
    /// </summary>
    public static event EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve;

    internal static string[] OnDesignerNamespaceResolveEvent(AppDomain appDomain, string namespaceName)
    {
      // ISSUE: reference to a compiler-generated field
      EventHandler<DesignerNamespaceResolveEventArgs> namespaceResolve = WindowsRuntimeMetadata.DesignerNamespaceResolve;
      if (namespaceResolve != null)
      {
        Delegate[] invocationList = namespaceResolve.GetInvocationList();
        int length = invocationList.Length;
        for (int index1 = 0; index1 < length; ++index1)
        {
          DesignerNamespaceResolveEventArgs e = new DesignerNamespaceResolveEventArgs(namespaceName);
          ((EventHandler<DesignerNamespaceResolveEventArgs>) invocationList[index1])((object) appDomain, e);
          Collection<string> resolvedAssemblyFiles = e.ResolvedAssemblyFiles;
          if (resolvedAssemblyFiles.Count > 0)
          {
            string[] strArray = new string[resolvedAssemblyFiles.Count];
            int index2 = 0;
            foreach (string str in resolvedAssemblyFiles)
            {
              if (string.IsNullOrEmpty(str))
                throw new ArgumentException(Environment.GetResourceString("Arg_EmptyOrNullString"), "DesignerNamespaceResolveEventArgs.ResolvedAssemblyFiles");
              strArray[index2] = str;
              ++index2;
            }
            return strArray;
          }
        }
      }
      return (string[]) null;
    }
  }
}
