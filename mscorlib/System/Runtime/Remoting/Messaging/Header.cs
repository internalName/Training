// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.Header
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>Определяет данные по каналу для вызова.</summary>
  [ComVisible(true)]
  [Serializable]
  public class Header
  {
    /// <summary>
    ///   Содержит имя <see cref="T:System.Runtime.Remoting.Messaging.Header" />.
    /// </summary>
    public string Name;
    /// <summary>
    ///   Содержит значение для <see cref="T:System.Runtime.Remoting.Messaging.Header" />.
    /// </summary>
    public object Value;
    /// <summary>
    ///   Указывает, должна ли принимающая сторона распознавать данных по каналу.
    /// </summary>
    public bool MustUnderstand;
    /// <summary>
    ///   Указывает пространство имен XML, текущий <see cref="T:System.Runtime.Remoting.Messaging.Header" /> принадлежит.
    /// </summary>
    public string HeaderNamespace;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.Header" /> класса с заданными именем и значением.
    /// </summary>
    /// <param name="_Name">
    ///   Имя <see cref="T:System.Runtime.Remoting.Messaging.Header" />.
    /// </param>
    /// <param name="_Value">
    ///   Объект, содержащий значение для <see cref="T:System.Runtime.Remoting.Messaging.Header" />.
    /// </param>
    public Header(string _Name, object _Value)
      : this(_Name, _Value, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.Header" /> класса с заданным именем, значением и Дополнительные сведения о конфигурации.
    /// </summary>
    /// <param name="_Name">
    ///   Имя <see cref="T:System.Runtime.Remoting.Messaging.Header" />.
    /// </param>
    /// <param name="_Value">
    ///   Объект, содержащий значение для <see cref="T:System.Runtime.Remoting.Messaging.Header" />.
    /// </param>
    /// <param name="_MustUnderstand">
    ///   Указывает, должна ли принимающая сторона распознавать данных по каналу.
    /// </param>
    public Header(string _Name, object _Value, bool _MustUnderstand)
    {
      this.Name = _Name;
      this.Value = _Value;
      this.MustUnderstand = _MustUnderstand;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Messaging.Header" />.
    /// </summary>
    /// <param name="_Name">
    ///   Имя <see cref="T:System.Runtime.Remoting.Messaging.Header" />.
    /// </param>
    /// <param name="_Value">
    ///   Объект, содержащий значение <see cref="T:System.Runtime.Remoting.Messaging.Header" />.
    /// </param>
    /// <param name="_MustUnderstand">
    ///   Указывает, должна ли принимающая сторона распознавать данные по каналу.
    /// </param>
    /// <param name="_HeaderNamespace">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.Header" /> Пространство имен XML.
    /// </param>
    public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
    {
      this.Name = _Name;
      this.Value = _Value;
      this.MustUnderstand = _MustUnderstand;
      this.HeaderNamespace = _HeaderNamespace;
    }
  }
}
