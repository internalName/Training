// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.SoapFault
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>
  ///   Содержит сведения об ошибках и состоянии в сообщении SOAP.
  ///    Этот класс не наследуется.
  /// </summary>
  [SoapType(Embedded = true)]
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapFault : ISerializable
  {
    private string faultCode;
    private string faultString;
    private string faultActor;
    [SoapField(Embedded = true)]
    private object detail;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> со значениями по умолчанию.
    /// </summary>
    public SoapFault()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> класс, устанавливая свойства для указанного значения.
    /// </summary>
    /// <param name="faultCode">
    ///   Код ошибки для нового экземпляра <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    ///    Код ошибки определяет тип произошедшей ошибки.
    /// </param>
    /// <param name="faultString">
    ///   Строка неисправности для нового экземпляра <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    ///    Строка неисправности предоставляет доступное для чтения объяснение неисправности.
    /// </param>
    /// <param name="faultActor">URI объекта, вызвавшего ошибку.</param>
    /// <param name="serverFault">
    ///   Описание исключения CLR.
    ///    Эти сведения также присутствует в <see cref="P:System.Runtime.Serialization.Formatters.SoapFault.Detail" /> свойство.
    /// </param>
    public SoapFault(string faultCode, string faultString, string faultActor, ServerFault serverFault)
    {
      this.faultCode = faultCode;
      this.faultString = faultString;
      this.faultActor = faultActor;
      this.detail = (object) serverFault;
    }

    internal SoapFault(SerializationInfo info, StreamingContext context)
    {
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        object obj = enumerator.Value;
        if (string.Compare(name, nameof (faultCode), true, CultureInfo.InvariantCulture) == 0)
        {
          int num1 = ((string) obj).IndexOf(':');
          int num2;
          this.faultCode = num1 <= -1 ? (string) obj : ((string) obj).Substring(num2 = num1 + 1);
        }
        else if (string.Compare(name, nameof (faultString), true, CultureInfo.InvariantCulture) == 0)
          this.faultString = (string) obj;
        else if (string.Compare(name, nameof (faultActor), true, CultureInfo.InvariantCulture) == 0)
          this.faultActor = (string) obj;
        else if (string.Compare(name, nameof (detail), true, CultureInfo.InvariantCulture) == 0)
          this.detail = obj;
      }
    }

    /// <summary>
    ///   Заполняет указанный <see cref="T:System.Runtime.Serialization.SerializationInfo" /> с данными для сериализации <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> объекта.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> для заполнения данными.
    /// </param>
    /// <param name="context">
    ///   Назначение (см. <see cref="T:System.Runtime.Serialization.StreamingContext" />) для текущей сериализации.
    /// </param>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("faultcode", (object) ("SOAP-ENV:" + this.faultCode));
      info.AddValue("faultstring", (object) this.faultString);
      if (this.faultActor != null)
        info.AddValue("faultactor", (object) this.faultActor);
      info.AddValue("detail", this.detail, typeof (object));
    }

    /// <summary>
    ///   Возвращает или задает код ошибки для <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    /// </summary>
    /// <returns>
    ///   Код ошибки для этого <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    /// </returns>
    public string FaultCode
    {
      get
      {
        return this.faultCode;
      }
      set
      {
        this.faultCode = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает сообщение об ошибке для <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    /// </summary>
    /// <returns>
    ///   Сообщение об ошибке для <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    /// </returns>
    public string FaultString
    {
      get
      {
        return this.faultString;
      }
      set
      {
        this.faultString = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает Актор неисправности для <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    /// </summary>
    /// <returns>
    ///   Актор неисправности для <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    /// </returns>
    public string FaultActor
    {
      get
      {
        return this.faultActor;
      }
      set
      {
        this.faultActor = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает дополнительные сведения, необходимые для <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    /// </summary>
    /// <returns>
    ///   Дополнительные сведения, необходимые для <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.
    /// </returns>
    public object Detail
    {
      get
      {
        return this.detail;
      }
      set
      {
        this.detail = value;
      }
    }
  }
}
