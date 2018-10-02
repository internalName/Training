// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyFileVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Дает компилятору указание использовать определенный номер версии для ресурса версии файла Win32.
  ///    Версия файла Win32 не обязательно должна совпадать с номером версии сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyFileVersionAttribute : Attribute
  {
    private string _version;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AssemblyFileVersionAttribute" /> класс, указав версию файла.
    /// </summary>
    /// <param name="version">Версия файла.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="version" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public AssemblyFileVersionAttribute(string version)
    {
      if (version == null)
        throw new ArgumentNullException(nameof (version));
      this._version = version;
    }

    /// <summary>Возвращает имя ресурса версии файла Win32.</summary>
    /// <returns>Строка, содержащая имя ресурса версии файла.</returns>
    [__DynamicallyInvokable]
    public string Version
    {
      [__DynamicallyInvokable] get
      {
        return this._version;
      }
    }
  }
}
