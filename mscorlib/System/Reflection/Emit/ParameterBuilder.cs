// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ParameterBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
  /// <summary>Создает или связывает сведения о параметрах.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ParameterBuilder))]
  [ComVisible(true)]
  public class ParameterBuilder : _ParameterBuilder
  {
    private string m_strParamName;
    private int m_iPosition;
    private ParameterAttributes m_attributes;
    private MethodBuilder m_methodBuilder;
    private ParameterToken m_pdToken;

    /// <summary>Задает маршалинг для данного параметра.</summary>
    /// <param name="unmanagedMarshal">
    ///   Сведения о маршалинге для данного параметра.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="unmanagedMarshal" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public virtual void SetMarshal(UnmanagedMarshal unmanagedMarshal)
    {
      if (unmanagedMarshal == null)
        throw new ArgumentNullException(nameof (unmanagedMarshal));
      byte[] bytes = unmanagedMarshal.InternalGetBytes();
      TypeBuilder.SetFieldMarshal(this.m_methodBuilder.GetModuleBuilder().GetNativeHandle(), this.m_pdToken.Token, bytes, bytes.Length);
    }

    /// <summary>Задает значение параметра по умолчанию.</summary>
    /// <param name="defaultValue">
    ///   По умолчанию значение этого параметра.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр не является одним из поддерживаемых типов.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="defaultValue" /> не соответствует типу параметра.
    /// 
    ///   -или-
    /// 
    ///   Параметр имеет тип <see cref="T:System.Object" /> или другой ссылочный тип, <paramref name="defaultValue" /> не <see langword="null" />, и значение не может быть назначен ссылочного типа.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void SetConstant(object defaultValue)
    {
      TypeBuilder.SetConstantValue(this.m_methodBuilder.GetModuleBuilder(), this.m_pdToken.Token, this.m_iPosition == 0 ? this.m_methodBuilder.ReturnType : this.m_methodBuilder.m_parameterTypes[this.m_iPosition - 1], defaultValue);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью большого двоичного объекта настраиваемых атрибутов.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="binaryAttribute">
    ///   Большой двоичный объект байтов, представляющий атрибуты.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="con" /> или <paramref name="binaryAttribute" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      if (binaryAttribute == null)
        throw new ArgumentNullException(nameof (binaryAttribute));
      TypeBuilder.DefineCustomAttribute(this.m_methodBuilder.GetModuleBuilder(), this.m_pdToken.Token, ((ModuleBuilder) this.m_methodBuilder.GetModule()).GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью построителя настраиваемых атрибутов.
    /// </summary>
    /// <param name="customBuilder">
    ///   Экземпляр вспомогательного класса для определения настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="con" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException(nameof (customBuilder));
      customBuilder.CreateCustomAttribute((ModuleBuilder) this.m_methodBuilder.GetModule(), this.m_pdToken.Token);
    }

    private ParameterBuilder()
    {
    }

    [SecurityCritical]
    internal ParameterBuilder(MethodBuilder methodBuilder, int sequence, ParameterAttributes attributes, string strParamName)
    {
      this.m_iPosition = sequence;
      this.m_strParamName = strParamName;
      this.m_methodBuilder = methodBuilder;
      this.m_strParamName = strParamName;
      this.m_attributes = attributes;
      this.m_pdToken = new ParameterToken(TypeBuilder.SetParamInfo(this.m_methodBuilder.GetModuleBuilder().GetNativeHandle(), this.m_methodBuilder.GetToken().Token, sequence, attributes, strParamName));
    }

    /// <summary>Получает маркер для этого параметра.</summary>
    /// <returns>Возвращает маркер для этого параметра.</returns>
    public virtual ParameterToken GetToken()
    {
      return this.m_pdToken;
    }

    void _ParameterBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ParameterBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ParameterBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ParameterBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_pdToken.Token;
      }
    }

    /// <summary>Получает имя данного параметра.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Получает имя данного параметра.
    /// </returns>
    public virtual string Name
    {
      get
      {
        return this.m_strParamName;
      }
    }

    /// <summary>Получает позицию подписи для данного параметра.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Получает позицию подписи для данного параметра.
    /// </returns>
    public virtual int Position
    {
      get
      {
        return this.m_iPosition;
      }
    }

    /// <summary>Извлекает атрибуты для этого параметра.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает атрибуты для этого параметра.
    /// </returns>
    public virtual int Attributes
    {
      get
      {
        return (int) this.m_attributes;
      }
    }

    /// <summary>Сообщает, является ли это входной параметр.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Сообщает, является ли это входной параметр.
    /// </returns>
    public bool IsIn
    {
      get
      {
        return (uint) (this.m_attributes & ParameterAttributes.In) > 0U;
      }
    }

    /// <summary>
    ///   Сообщает, является ли этот параметр является выходным параметром.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Сообщает, является ли этот параметр является выходным параметром.
    /// </returns>
    public bool IsOut
    {
      get
      {
        return (uint) (this.m_attributes & ParameterAttributes.Out) > 0U;
      }
    }

    /// <summary>
    ///   Сообщает, является ли этот параметр является необязательным.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Указывает, является ли этот параметр является необязательным.
    /// </returns>
    public bool IsOptional
    {
      get
      {
        return (uint) (this.m_attributes & ParameterAttributes.Optional) > 0U;
      }
    }
  }
}
