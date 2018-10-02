// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyNameProxy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Предоставляет дистанционной версии <see langword="AssemblyName" />.
  /// </summary>
  [ComVisible(true)]
  public class AssemblyNameProxy : MarshalByRefObject
  {
    /// <summary>
    ///   Возвращает <see langword="AssemblyName" /> для заданного файла.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Файл сборки, для которого нужно получить <see langword="AssemblyName" />.
    /// </param>
    /// <returns>
    ///   <see langword="AssemblyName" /> Объект, представляющий данный файл.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="assemblyFile" /> пуст.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyFile" /> не найден.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// </exception>
    public AssemblyName GetAssemblyName(string assemblyFile)
    {
      return AssemblyName.GetAssemblyName(assemblyFile);
    }
  }
}
