// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ImportedFromTypeLibAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, что типы, определенные в сборке, изначально были определены в библиотеке типов.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  public sealed class ImportedFromTypeLibAttribute : Attribute
  {
    internal string _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ImportedFromTypeLibAttribute" /> класс с именем файла исходной библиотеки типов.
    /// </summary>
    /// <param name="tlbFile">
    ///   Расположение файла исходной библиотеки типов.
    /// </param>
    public ImportedFromTypeLibAttribute(string tlbFile)
    {
      this._val = tlbFile;
    }

    /// <summary>Возвращает имя файла исходной библиотеки типов.</summary>
    /// <returns>Имя файла исходной библиотеки типов.</returns>
    public string Value
    {
      get
      {
        return this._val;
      }
    }
  }
}
