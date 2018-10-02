// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ITypeLibConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет набор служб, преобразующих управляемую сборку в библиотеку типов COM и наоборот.
  /// </summary>
  [Guid("F1C3BF78-C3E4-11d3-88E7-00902754C43A")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface ITypeLibConverter
  {
    /// <summary>Преобразует библиотеку типов COM в сборку.</summary>
    /// <param name="typeLib">
    ///   Объект, реализующий интерфейс <see langword="ITypeLib" />.
    /// </param>
    /// <param name="asmFileName">Имя файла итоговой сборки.</param>
    /// <param name="flags">
    ///   Объект <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> показывающее специальные параметры.
    /// </param>
    /// <param name="notifySink">
    ///   <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> интерфейс, реализованный вызывающим объектом.
    /// </param>
    /// <param name="publicKey">
    ///   A <see langword="byte" /> массив, содержащий открытый ключ.
    /// </param>
    /// <param name="keyPair">
    ///   Объект <see cref="T:System.Reflection.StrongNameKeyPair" /> объект, содержащий пару открытого и закрытого криптографических ключей.
    /// </param>
    /// <param name="asmNamespace">
    ///   Пространство имен для итоговой сборки.
    /// </param>
    /// <param name="asmVersion">
    ///   Версия итоговой сборки.
    ///    Если <see langword="null" />, используется версия библиотеки типов.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> Объект, содержащий преобразованную библиотеку типов.
    /// </returns>
    AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion);

    /// <summary>Преобразует сборку в библиотеку COM-типов.</summary>
    /// <param name="assembly">Сборка для преобразования.</param>
    /// <param name="typeLibName">
    ///   Имя файла итоговой библиотеки типов.
    /// </param>
    /// <param name="flags">
    ///   Объект <see cref="T:System.Runtime.InteropServices.TypeLibExporterFlags" /> показывающее специальные параметры.
    /// </param>
    /// <param name="notifySink">
    ///   <see cref="T:System.Runtime.InteropServices.ITypeLibExporterNotifySink" /> Интерфейс, реализованный вызывающим объектом.
    /// </param>
    /// <returns>
    ///   Объект, реализующий интерфейс <see langword="ITypeLib" />.
    /// </returns>
    [return: MarshalAs(UnmanagedType.Interface)]
    object ConvertAssemblyToTypeLib(Assembly assembly, string typeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

    /// <summary>
    ///   Возвращает имя и базу кода основной сборки взаимодействия для указанной библиотеки типов.
    /// </summary>
    /// <param name="g">Идентификатор GUID библиотеки типов.</param>
    /// <param name="major">
    ///   Основной номер версии библиотеки типов.
    /// </param>
    /// <param name="minor">
    ///   Дополнительный номер версии библиотеки типов.
    /// </param>
    /// <param name="lcid">Идентификатор LCID библиотеки типов.</param>
    /// <param name="asmName">
    ///   При удачном возвращении имя основной сборки взаимодействия, связанной с <paramref name="g" />.
    /// </param>
    /// <param name="asmCodeBase">
    ///   При удачном возвращении база кода основной сборки взаимодействия связано с <paramref name="g" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если основная сборка взаимодействия был найден в реестре; в противном случае <see langword="false" />.
    /// </returns>
    bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase);

    /// <summary>Преобразует библиотеку типов COM в сборку.</summary>
    /// <param name="typeLib">
    ///   Объект, реализующий интерфейс <see langword="ITypeLib" />.
    /// </param>
    /// <param name="asmFileName">Имя файла итоговой сборки.</param>
    /// <param name="flags">
    ///   Объект <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> показывающее специальные параметры.
    /// </param>
    /// <param name="notifySink">
    ///   <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> интерфейс, реализованный вызывающим объектом.
    /// </param>
    /// <param name="publicKey">
    ///   A <see langword="byte" /> массив, содержащий открытый ключ.
    /// </param>
    /// <param name="keyPair">
    ///   Объект <see cref="T:System.Reflection.StrongNameKeyPair" /> объект, содержащий пару открытого и закрытого криптографических ключей.
    /// </param>
    /// <param name="unsafeInterfaces">
    ///   Если <see langword="true" />, для интерфейса необходимы проверки времени компоновки для <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> разрешение.
    ///    Если <see langword="false" />, интерфейсов необходимы проверки во время выполнения, для которых требуется стека становятся более ресурсоемкими, помогают обеспечить большую защищенность.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> Объект, содержащий преобразованную библиотеку типов.
    /// </returns>
    AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces);
  }
}
