// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>
  ///   Предоставляет функциональные возможности по умолчанию для всех атрибутов SOAP.
  /// </summary>
  [ComVisible(true)]
  public class SoapAttribute : Attribute
  {
    /// <summary>
    ///   Пространство имен XML, в который сериализуется целевой объект текущего атрибута SOAP.
    /// </summary>
    protected string ProtXmlNamespace;
    private bool _bUseAttribute;
    private bool _bEmbedded;
    /// <summary>
    ///   Объект отражения, который используется производными классами атрибут <see cref="T:System.Runtime.Remoting.Metadata.SoapAttribute" /> класс, чтобы задать сведения о сериализации XML.
    /// </summary>
    protected object ReflectInfo;

    internal void SetReflectInfo(object info)
    {
      this.ReflectInfo = info;
    }

    /// <summary>Возвращает или задает имя пространства имен XML.</summary>
    /// <returns>
    ///   Имя пространства имен XML, под которым сериализуется целевой объект текущего атрибута.
    /// </returns>
    public virtual string XmlNamespace
    {
      get
      {
        return this.ProtXmlNamespace;
      }
      set
      {
        this.ProtXmlNamespace = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, будет ли целевой объект текущего атрибута сериализован как атрибут XML вместо поля XML.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если целевой объект текущего атрибута необходимо сериализовать в качестве атрибута XML; <see langword="false" /> Если целевой объект необходимо сериализовать в качестве подэлемента.
    /// </returns>
    public virtual bool UseAttribute
    {
      get
      {
        return this._bUseAttribute;
      }
      set
      {
        this._bUseAttribute = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее на необходимость вложения типа при сериализации протокола SOAP.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если целевой объект должен быть вложен во время сериализации SOAP; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool Embedded
    {
      get
      {
        return this._bEmbedded;
      }
      set
      {
        this._bEmbedded = value;
      }
    }
  }
}
