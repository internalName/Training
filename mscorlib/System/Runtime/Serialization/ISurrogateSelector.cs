// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ISurrogateSelector
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>Указывает класс селектора суррогата сериализации.</summary>
  [ComVisible(true)]
  public interface ISurrogateSelector
  {
    /// <summary>
    ///   Задает следующий <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> для суррогатов, если у текущего экземпляра отсутствует суррогат для заданного типа и сборки в заданном контексте.
    /// </summary>
    /// <param name="selector">
    ///   Следующий проверяемый селектор суррогата.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    void ChainSelector(ISurrogateSelector selector);

    /// <summary>
    ///   Находит суррогат, который представляет тип заданного объекта, начиная с заданного селектора суррогата для заданного контекста сериализации.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Объекта (класса), для которого необходим суррогат.
    /// </param>
    /// <param name="context">
    ///   Контекст источника или назначения для текущей сериализации.
    /// </param>
    /// <param name="selector">
    ///   При возвращении данного метода содержит <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> хранящий ссылку на селектор суррогата, где был найден соответствующий суррогат.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Соответствующий суррогат для заданного типа в заданном контексте.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector);

    /// <summary>Возвращает следующий селектор суррогата в цепочку.</summary>
    /// <returns>
    ///   Следующий селектор суррогата в цепочке или <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    ISurrogateSelector GetNextSelector();
  }
}
