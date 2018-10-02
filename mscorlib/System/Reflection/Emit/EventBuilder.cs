// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.EventBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>Определяет события для класса.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_EventBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class EventBuilder : _EventBuilder
  {
    private string m_name;
    private EventToken m_evToken;
    private ModuleBuilder m_module;
    private EventAttributes m_attributes;
    private TypeBuilder m_type;

    private EventBuilder()
    {
    }

    internal EventBuilder(ModuleBuilder mod, string name, EventAttributes attr, TypeBuilder type, EventToken evToken)
    {
      this.m_name = name;
      this.m_module = mod;
      this.m_attributes = attr;
      this.m_evToken = evToken;
      this.m_type = type;
    }

    /// <summary>Возвращает токен для данного события.</summary>
    /// <returns>
    ///   Возвращает <see langword="EventToken" /> для этого события.
    /// </returns>
    public EventToken GetEventToken()
    {
      return this.m_evToken;
    }

    [SecurityCritical]
    private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
    {
      if ((MethodInfo) mdBuilder == (MethodInfo) null)
        throw new ArgumentNullException(nameof (mdBuilder));
      this.m_type.ThrowIfCreated();
      TypeBuilder.DefineMethodSemantics(this.m_module.GetNativeHandle(), this.m_evToken.Token, semantics, mdBuilder.GetToken().Token);
    }

    /// <summary>
    ///   Задает метод, используемый для подписки на данное событие.
    /// </summary>
    /// <param name="mdBuilder">
    ///   Объект <see langword="MethodBuilder" /> объект, представляющий метод, используемый для подписки на это событие.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mdBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetAddOnMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.AddOn);
    }

    /// <summary>
    ///   Задает метод, используемый для отказа от подписки на это событие.
    /// </summary>
    /// <param name="mdBuilder">
    ///   Объект <see langword="MethodBuilder" /> объект, представляющий метод, используемый для отказа от подписки на это событие.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mdBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetRemoveOnMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.RemoveOn);
    }

    /// <summary>
    ///   Задает метод, используемый для вызова данного события.
    /// </summary>
    /// <param name="mdBuilder">
    ///   Объект <see langword="MethodBuilder" /> объект, представляющий метод, используемый для вызова данного события.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mdBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetRaiseMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Fire);
    }

    /// <summary>
    ///   Добавляет один из методов «other», связанное с этим событием.
    ///    «Other» методы являются методами, отличные от «с» и «вызвать» методы, связанные с событием.
    ///    Эта функция вызывается многократно добавлять столько методы «other».
    /// </summary>
    /// <param name="mdBuilder">
    ///   Объект <see langword="MethodBuilder" /> представляющий другой метод.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mdBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    public void AddOtherMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
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
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      if (binaryAttribute == null)
        throw new ArgumentNullException(nameof (binaryAttribute));
      this.m_type.ThrowIfCreated();
      TypeBuilder.DefineCustomAttribute(this.m_module, this.m_evToken.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью построителя настраиваемых атрибутов.
    /// </summary>
    /// <param name="customBuilder">
    ///   Экземпляр вспомогательного класса для описания настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="con" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException(nameof (customBuilder));
      this.m_type.ThrowIfCreated();
      customBuilder.CreateCustomAttribute(this.m_module, this.m_evToken.Token);
    }

    void _EventBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _EventBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _EventBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _EventBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
