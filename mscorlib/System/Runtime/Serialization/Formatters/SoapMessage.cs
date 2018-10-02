// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.SoapMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>
  ///   Содержит имена и типы параметров, необходимых при сериализации SOAP RPC (удаленный вызов процедуры).
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class SoapMessage : ISoapMessage
  {
    internal string[] paramNames;
    internal object[] paramValues;
    internal Type[] paramTypes;
    internal string methodName;
    internal string xmlNameSpace;
    internal Header[] headers;

    /// <summary>
    ///   Возвращает или задает имена параметров вызванного метода.
    /// </summary>
    /// <returns>Имена параметров вызванного метода.</returns>
    public string[] ParamNames
    {
      get
      {
        return this.paramNames;
      }
      set
      {
        this.paramNames = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значения параметров вызванного метода.
    /// </summary>
    /// <returns>Значения параметров вызванного метода.</returns>
    public object[] ParamValues
    {
      get
      {
        return this.paramValues;
      }
      set
      {
        this.paramValues = value;
      }
    }

    /// <summary>
    ///   Это свойство зарезервировано.
    ///    Используйте <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamNames" /> или <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamValues" /> свойства вместо.
    /// </summary>
    /// <returns>Типы параметров вызванного метода.</returns>
    public Type[] ParamTypes
    {
      get
      {
        return this.paramTypes;
      }
      set
      {
        this.paramTypes = value;
      }
    }

    /// <summary>Возвращает или задает имя вызванного метода.</summary>
    /// <returns>Имя вызванного метода.</returns>
    public string MethodName
    {
      get
      {
        return this.methodName;
      }
      set
      {
        this.methodName = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя пространства имен XML, в котором расположен объект, содержащий вызванный метод.
    /// </summary>
    /// <returns>
    ///   Имя пространства имен XML, где находится объект, содержащий вызванный метод.
    /// </returns>
    public string XmlNameSpace
    {
      get
      {
        return this.xmlNameSpace;
      }
      set
      {
        this.xmlNameSpace = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает данные по каналу, вызванного метода.
    /// </summary>
    /// <returns>Данные по каналу, вызванного метода.</returns>
    public Header[] Headers
    {
      get
      {
        return this.headers;
      }
      set
      {
        this.headers = value;
      }
    }
  }
}
