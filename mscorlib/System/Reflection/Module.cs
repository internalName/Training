// Decompiled with JetBrains decompiler
// Type: System.Reflection.Module
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>Выполняет отражение для модуля.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Module))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class Module : _Module, ISerializable, ICustomAttributeProvider
  {
    /// <summary>
    ///   Объект <see langword="TypeFilter" />, который фильтрует список типов, определенных в этом модуле на основе имени.
    ///    Это поле учитывает регистр и доступно только для чтения.
    /// </summary>
    public static readonly TypeFilter FilterTypeName;
    /// <summary>
    ///   Объект <see langword="TypeFilter" />, который фильтрует список типов, определенных в этом модуле на основе имени.
    ///    Это поле доступно только для чтения. В нем не учитывается регистр.
    /// </summary>
    public static readonly TypeFilter FilterTypeNameIgnoreCase;
    private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

    static Module()
    {
      __Filters filters = new __Filters();
      Module.FilterTypeName = new TypeFilter(filters.FilterTypeName);
      Module.FilterTypeNameIgnoreCase = new TypeFilter(filters.FilterTypeNameIgnoreCase);
    }

    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Reflection.Module" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(Module left, Module right)
    {
      if ((object) left == (object) right)
        return true;
      if ((object) left == null || (object) right == null || (left is RuntimeModule || right is RuntimeModule))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Reflection.Module" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(Module left, Module right)
    {
      return !(left == right);
    }

    /// <summary>Определяет, равны ли этот модуль и заданный объект.</summary>
    /// <param name="o">Объект, сравниваемый с данным экземпляром.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="o" /> равно данному экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      return base.Equals(o);
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>Возвращает имя модуля.</summary>
    /// <returns>
    ///   Объект <see langword="String" /> представляет имя этого модуля.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ScopeName;
    }

    /// <summary>
    ///   Возвращает коллекцию, содержащую пользовательские атрибуты этого модуля.
    /// </summary>
    /// <returns>
    ///   Коллекция, содержащая пользовательские атрибуты этого модуля.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<CustomAttributeData> CustomAttributes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<CustomAttributeData>) this.GetCustomAttributesData();
      }
    }

    /// <summary>Возвращает все пользовательские атрибуты.</summary>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see langword="Object" /> содержащий все пользовательские атрибуты.
    /// </returns>
    public virtual object[] GetCustomAttributes(bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>Получает настраиваемые атрибуты заданного типа.</summary>
    /// <param name="attributeType">Тип атрибута.</param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see langword="Object" /> содержащий все пользовательские атрибуты заданного типа.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является объектом <see cref="T:System.Type" />, предоставляемым средой выполнения.
    ///    Например, <paramref name="attributeType" /> является объектом <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// </exception>
    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает значение, указывающее, применен ли указанный тип атрибута для данного модуля.
    /// </summary>
    /// <param name="attributeType">
    ///   Тип настраиваемого атрибута для проверки.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> был применен к этому модулю; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является объектом <see cref="T:System.Type" />, предоставляемым средой выполнения.
    ///    Например, <paramref name="attributeType" /> является объектом <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// </exception>
    public virtual bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает список <see cref="T:System.Reflection.CustomAttributeData" /> объектов для текущего модуля, который может использоваться в контексте только для отражения.
    /// </summary>
    /// <returns>
    ///   Универсальный список объектов <see cref="T:System.Reflection.CustomAttributeData" />, представляющих данные об атрибутах, которые были применены к текущему модулю.
    /// </returns>
    public virtual IList<CustomAttributeData> GetCustomAttributesData()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает метод или конструктор, определенный заданным токеном метаданных.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий метод или конструктор в модуле.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodBase" /> объект, представляющий метод или конструктор, который определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для метода или конструктора в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="MethodSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public MethodBase ResolveMethod(int metadataToken)
    {
      return this.ResolveMethod(metadataToken, (Type[]) null, (Type[]) null);
    }

    /// <summary>
    ///   Возвращает метод или конструктор, определенный заданным маркером метаданных, в контексте, определенном заданными параметрами универсального типа.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий метод или конструктор в модуле.
    /// </param>
    /// <param name="genericTypeArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="genericMethodArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodBase" /> объект, представляющий метод, который определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для метода или конструктора в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="MethodSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода), а аргументы необходимые универсального типа не были указаны для одной или обеих <paramref name="genericTypeArguments" /> и <paramref name="genericMethodArguments" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public virtual MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает поле, определенное заданным токеном метаданных.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий поле в модуле.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> объект, предоставляющий поле, которое определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для поля в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> Определяет поле, родительская <see langword="TypeSpec" /> имеет подпись, содержащую тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public FieldInfo ResolveField(int metadataToken)
    {
      return this.ResolveField(metadataToken, (Type[]) null, (Type[]) null);
    }

    /// <summary>
    ///   Возвращает поля, определенного указанным токеном метаданных, в контексте, определенном заданными параметрами универсального типа.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий поле в модуле.
    /// </param>
    /// <param name="genericTypeArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="genericMethodArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> объект, предоставляющий поле, которое определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для поля в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> Определяет поле, родительская <see langword="TypeSpec" /> имеет подпись, содержащую тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода), а аргументы необходимые универсального типа не были указаны для одной или обеих <paramref name="genericTypeArguments" /> и <paramref name="genericMethodArguments" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public virtual FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает тип, определенный заданным токеном метаданных.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий тип в модуле.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий тип, который определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для типа в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="TypeSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public Type ResolveType(int metadataToken)
    {
      return this.ResolveType(metadataToken, (Type[]) null, (Type[]) null);
    }

    /// <summary>
    ///   Возвращает тип, определенный указанным токеном метаданных, в контексте, определенном заданными параметрами универсального типа.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий тип в модуле.
    /// </param>
    /// <param name="genericTypeArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="genericMethodArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий тип, который определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для типа в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="TypeSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода), а аргументы необходимые универсального типа не были указаны для одной или обеих <paramref name="genericTypeArguments" /> и <paramref name="genericMethodArguments" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public virtual Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает тип члена, определенный заданным токеном метаданных.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий тип или член в модуле.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MemberInfo" /> объект, представляющий тип или член, который определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для типа или члена в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="MethodSpec" /> или <see langword="TypeSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> идентифицирует свойство или событие.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public MemberInfo ResolveMember(int metadataToken)
    {
      return this.ResolveMember(metadataToken, (Type[]) null, (Type[]) null);
    }

    /// <summary>
    ///   Возвращает тип или член, определяемый указанным токеном метаданных, в контексте, определенном заданными параметрами универсального типа.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий тип или член в модуле.
    /// </param>
    /// <param name="genericTypeArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="genericMethodArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MemberInfo" /> объект, представляющий тип или член, который определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для типа или члена в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="MethodSpec" /> или <see langword="TypeSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода), а аргументы необходимые универсального типа не были указаны для одной или обеих <paramref name="genericTypeArguments" /> и <paramref name="genericMethodArguments" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> идентифицирует свойство или событие.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public virtual MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveMember(metadataToken, genericTypeArguments, genericMethodArguments);
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает большой двоичный объект подписи, определенный токеном метаданных.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий подпись в модуле.
    /// </param>
    /// <returns>
    ///   Массив байтов, представляющий большой двоичный объект подписи.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является допустимым <see langword="MemberRef" />, <see langword="MethodDef" />, <see langword="TypeSpec" />, подписи или <see langword="FieldDef" /> маркеров в области текущего модуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public virtual byte[] ResolveSignature(int metadataToken)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveSignature(metadataToken);
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает строку, определенную заданным токеном метаданных.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий строку в куче строк модуля.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.String" /> содержащий строковое значение из кучи строк метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для строки в области текущего модуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public virtual string ResolveString(int metadataToken)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveString(metadataToken);
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает пару значений, определяющих природу кода в модуле и платформе, модуль.
    /// </summary>
    /// <param name="peKind">
    ///   При возвращении данного метода сочетание <see cref="T:System.Reflection.PortableExecutableKinds" /> значений, определяющих природу кода в модуле.
    /// </param>
    /// <param name="machine">
    ///   Если этот метод возвращает, один из <see cref="T:System.Reflection.ImageFileMachine" /> значения, указывающие платформы в модуле.
    /// </param>
    public virtual void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        runtimeModule.GetPEKind(out peKind, out machine);
      throw new NotImplementedException();
    }

    /// <summary>Возвращает версию потока метаданных.</summary>
    /// <returns>
    ///   32-разрядное целое число, представляющее версию потока метаданных.
    ///    Старшие байты два представляют основной номер версии, а два байта низкого порядка дополнительный номер версии.
    /// </returns>
    public virtual int MDStreamVersion
    {
      get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.MDStreamVersion;
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Предоставляет <see cref="T:System.Runtime.Serialization.ISerializable" /> реализацию для сериализованных объектов.
    /// </summary>
    /// <param name="info">
    ///   Сведения и данные, необходимые для сериализации или десериализации объекта.
    /// </param>
    /// <param name="context">Контекст для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает указанный тип, выполняя поиск в модуле с заданным требованием к учету регистра.
    /// </summary>
    /// <param name="className">
    ///   Имя искомого типа.
    ///    Имя должно содержать пространство имен.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для поиска без учета регистра. В противном случае используется значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see langword="Type" />, представляющий указанный тип, если тип находится в этом модуле. В противном случае возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="className" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываются инициализаторы класса и создается исключение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="className" /> представляет собой строку нулевой длины.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Для <paramref name="className" /> требуется зависимая сборка, которую не удается найти.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Для <paramref name="className" /> требуется зависимая сборка, которая была найдена, но ее не удалось загрузить.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения, а для <paramref name="className" /> требуется зависимая сборка, которая не была предварительно загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Для <paramref name="className" /> требуется зависимая сборка, однако файл не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="className" /> требуется зависимая сборка, которая была скомпилирована для версии среды выполнения более поздней, чем текущая загруженная версия.
    /// </exception>
    [ComVisible(true)]
    public virtual Type GetType(string className, bool ignoreCase)
    {
      return this.GetType(className, false, ignoreCase);
    }

    /// <summary>
    ///   Возвращает заданный тип (выполняет поиск с учетом регистра).
    /// </summary>
    /// <param name="className">
    ///   Имя искомого типа.
    ///    Имя должно содержать пространство имен.
    /// </param>
    /// <returns>
    ///   Объект <see langword="Type" />, представляющий указанный тип, если тип находится в этом модуле. В противном случае возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="className" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываются инициализаторы класса и создается исключение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="className" /> представляет собой строку нулевой длины.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Для <paramref name="className" /> требуется зависимая сборка, которую не удается найти.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Для <paramref name="className" /> требуется зависимая сборка, которая была найдена, но ее не удалось загрузить.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения, а для <paramref name="className" /> требуется зависимая сборка, которая не была предварительно загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Для <paramref name="className" /> требуется зависимая сборка, однако файл не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Для <paramref name="className" /> требуется зависимая сборка, которая была скомпилирована для версии среды выполнения более поздней, чем текущая загруженная версия.
    /// </exception>
    [ComVisible(true)]
    public virtual Type GetType(string className)
    {
      return this.GetType(className, false, false);
    }

    /// <summary>
    ///   Возвращает указанный тип, определяя, следует ли учитывать регистр при поиске модуля и необходимость создания исключения, если тип не найден.
    /// </summary>
    /// <param name="className">
    ///   Имя типа для поиска.
    ///    Имя должно быть полное пространство имен.
    /// </param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" />, чтобы создать исключение, если тип не удается найти; значение <see langword="false" />, чтобы вернуть значение <see langword="null" />.
    /// </param>
    /// <param name="ignoreCase">
    ///   <see langword="true" /> Поиск без учета регистра; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий указанный тип, если тип, объявленный в этом модуле; в противном случае — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="className" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызваны инициализаторы класса и создается исключение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="className" /> представляет собой строку нулевой длины.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="throwOnError" /> является <see langword="true" />, не удается найти тип.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Для <paramref name="className" /> требуется зависимая сборка, которую не удалось найти.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Для <paramref name="className" /> требуется зависимая сборка, которая была найдена, но ее не удалось загрузить.
    /// 
    ///   -или-
    /// 
    ///   Текущая сборка была загружена в контекст только для отражения, а для <paramref name="className" /> требуется зависимая сборка, которая не была предварительно загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Для <paramref name="className" /> требуется зависимая сборка, однако файл не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="className" /> требуется зависимая сборка, которая была скомпилирована для версии позже, чем момент загруженную версию среды выполнения.
    /// </exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает строку, представляющую полное имя и путь к этому модулю.
    /// </summary>
    /// <returns>Полное имя модуля.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствуют необходимые разрешения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual string FullyQualifiedName
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает массив классов, удовлетворяющих указанному фильтру и критериям фильтра.
    /// </summary>
    /// <param name="filter">
    ///   Делегат, который используется для фильтрования классов.
    /// </param>
    /// <param name="filterCriteria">
    ///   Объект, который используется для фильтрования классов.
    /// </param>
    /// <returns>
    ///   Массив типа <see langword="Type" />, который содержит классы, удовлетворяющие критериям фильтра.
    /// </returns>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">
    ///   Невозможно загрузить один или несколько классов в модуле.
    /// </exception>
    public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
    {
      Type[] types = this.GetTypes();
      int length = 0;
      for (int index = 0; index < types.Length; ++index)
      {
        if (filter != null && !filter(types[index], filterCriteria))
          types[index] = (Type) null;
        else
          ++length;
      }
      if (length == types.Length)
        return types;
      Type[] typeArray = new Type[length];
      int num = 0;
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] != (Type) null)
          typeArray[num++] = types[index];
      }
      return typeArray;
    }

    /// <summary>Возвращает все типы, определенные в этом модуле.</summary>
    /// <returns>
    ///   Массив типа <see langword="Type" />, содержащий типы, определенные в модуле, отраженном этим экземпляром.
    /// </returns>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">
    ///   Невозможно загрузить один или несколько классов в модуле.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public virtual Type[] GetTypes()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает универсальный уникальный идентификатор (UUID), по которому можно различить две версии модуля.
    /// </summary>
    /// <returns>
    ///   Значение свойства <see cref="T:System.Guid" />, по которому можно различить две версии модуля.
    /// </returns>
    public virtual Guid ModuleVersionId
    {
      get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.ModuleVersionId;
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает токен, который определяет модуль в метаданных.
    /// </summary>
    /// <returns>
    ///   Целочисленный токен, который идентифицирует текущий модуль в метаданных.
    /// </returns>
    public virtual int MetadataToken
    {
      get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.MetadataToken;
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли объект ресурсом.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если объект является ресурсом; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsResource()
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.IsResource();
      throw new NotImplementedException();
    }

    /// <summary>Возвращает глобальные поля, определенные в модуле.</summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.FieldInfo" />, представляющих глобальные поля, определенные в модуле. Если глобальные поля отсутствуют, возвращается пустой массив.
    /// </returns>
    public FieldInfo[] GetFields()
    {
      return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   Возвращает глобальные поля, определенные в модуле, которые соответствуют заданным флагам привязки.
    /// </summary>
    /// <param name="bindingFlags">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.BindingFlags" />, которая определяет границы поиска.
    /// </param>
    /// <returns>
    ///   Массив типа <see cref="T:System.Reflection.FieldInfo" />, представляющий глобальные поля, определенные в модуле, который соответствует указанным флагам привязки. Если глобальные поля не соответствуют флагам привязки, возвращается пустой массив.
    /// </returns>
    public virtual FieldInfo[] GetFields(BindingFlags bindingFlags)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.GetFields(bindingFlags);
      throw new NotImplementedException();
    }

    /// <summary>Возвращает поле с указанным именем.</summary>
    /// <param name="name">Имя поля.</param>
    /// <returns>
    ///   Объект <see langword="FieldInfo" /> с указанным именем или <see langword="null" />, если поле не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public FieldInfo GetField(string name)
    {
      return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   Возвращает поле с указанным именем и атрибутами привязки.
    /// </summary>
    /// <param name="name">Имя поля.</param>
    /// <param name="bindingAttr">
    ///   Один из битовых флагов <see langword="BindingFlags" />, используемых для управления поиском.
    /// </param>
    /// <returns>
    ///   Объект <see langword="FieldInfo" /> с указанным именем и атрибутами привязки или <see langword="null" />, если поле не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.GetField(name, bindingAttr);
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает глобальные методы, определенные в модуле.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MethodInfo" />, представляющих все глобальные методы, определенные в модуле. Если глобальные методы отсутствуют, возвращается пустой массив.
    /// </returns>
    public MethodInfo[] GetMethods()
    {
      return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   Возвращает глобальные методы, определенные в модуле, которые соответствуют заданным флагам привязки.
    /// </summary>
    /// <param name="bindingFlags">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.BindingFlags" />, которая определяет границы поиска.
    /// </param>
    /// <returns>
    ///   Массив типа <see cref="T:System.Reflection.MethodInfo" /> — представляет глобальные методы, определенные в модуле, который соответствует указанным флагам связывания. Если глобальные методы не соответствуют флагам привязки, возвращается пустой массив.
    /// </returns>
    public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.GetMethods(bindingFlags);
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает метод, имеющий заданное имя, сведения о привязке, соглашение о вызовах и типы и модификаторы параметров.
    /// </summary>
    /// <param name="name">Имя метода.</param>
    /// <param name="bindingAttr">
    ///   Один из битовых флагов <see langword="BindingFlags" />, используемых для управления поиском.
    /// </param>
    /// <param name="binder">
    ///   Объект, реализующий <see langword="Binder" />, содержащий свойства, связанные с этим методом.
    /// </param>
    /// <param name="callConvention">
    ///   Соглашение о вызовах для метода.
    /// </param>
    /// <param name="types">Искомые типы параметров.</param>
    /// <param name="modifiers">
    ///   Массив модификаторов параметров, используемый для работы привязки с подписями параметров, в которых были изменены типы.
    /// </param>
    /// <returns>
    ///   Объект <see langword="MethodInfo" /> в соответствии с указанными условиями или <see langword="null" />, если метод не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> имеет значение <see langword="null" />, <paramref name="types" /> имеет значение <see langword="null" /> или <paramref name="types" /> (i) имеет значение <see langword="null" />.
    /// </exception>
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException(nameof (types));
      }
      return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Возвращает метод, имеющий указанные имя и типы параметров.
    /// </summary>
    /// <param name="name">Имя метода.</param>
    /// <param name="types">Искомые типы параметров.</param>
    /// <returns>
    ///   Объект <see langword="MethodInfo" /> в соответствии с указанными условиями или <see langword="null" />, если метод не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> имеет значение <see langword="null" />, <paramref name="types" /> имеет значение <see langword="null" /> или <paramref name="types" /> (i) имеет значение <see langword="null" />.
    /// </exception>
    public MethodInfo GetMethod(string name, Type[] types)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException(nameof (types));
      }
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, types, (ParameterModifier[]) null);
    }

    /// <summary>Возвращает метод с указанным именем.</summary>
    /// <param name="name">Имя метода.</param>
    /// <returns>
    ///   Объект <see langword="MethodInfo" /> с указанным именем или <see langword="null" />, если метод не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public MethodInfo GetMethod(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>
    ///   Возвращает реализацию метода в соответствии с указанными критериями.
    /// </summary>
    /// <param name="name">Имя метода.</param>
    /// <param name="bindingAttr">
    ///   Один из битовых флагов <see langword="BindingFlags" />, используемых для управления поиском.
    /// </param>
    /// <param name="binder">
    ///   Объект, реализующий <see langword="Binder" />, содержащий свойства, связанные с этим методом.
    /// </param>
    /// <param name="callConvention">
    ///   Соглашение о вызовах для метода.
    /// </param>
    /// <param name="types">Искомые типы параметров.</param>
    /// <param name="modifiers">
    ///   Массив модификаторов параметров, используемый для работы привязки с подписями параметров, в которых были изменены типы.
    /// </param>
    /// <returns>
    ///   Объект <see langword="MethodInfo" /> объект, содержащий сведения о реализации, как указано, или <see langword="null" /> Если метод не существует.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    protected virtual MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotImplementedException();
    }

    /// <summary>Возвращает строку, представляющую имя модуля.</summary>
    /// <returns>Имя модуля.</returns>
    public virtual string ScopeName
    {
      get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.ScopeName;
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает <see langword="String" /> удалены представляющую имя модуля без пути.
    /// </summary>
    /// <returns>Имя модуля без пути.</returns>
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.Name;
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает соответствующий <see cref="T:System.Reflection.Assembly" /> для этого экземпляра <see cref="T:System.Reflection.Module" />.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="Assembly" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Assembly Assembly
    {
      [__DynamicallyInvokable] get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.Assembly;
        throw new NotImplementedException();
      }
    }

    /// <summary>Возвращает дескриптор для модуля.</summary>
    /// <returns>
    ///   A <see cref="T:System.ModuleHandle" /> структуры для текущего модуля.
    /// </returns>
    public ModuleHandle ModuleHandle
    {
      get
      {
        return this.GetModuleHandle();
      }
    }

    internal virtual ModuleHandle GetModuleHandle()
    {
      return ModuleHandle.EmptyHandle;
    }

    /// <summary>
    ///   Возвращает <see langword="X509Certificate" /> объект, соответствующий сертификату, включаемому в подпись Authenticode сборки, которой принадлежит этот модуль.
    ///    Если сборка не была, с подписью Authenticode <see langword="null" /> возвращается.
    /// </summary>
    /// <returns>
    ///   <see langword="X509Certificate" /> Объект, или <see langword="null" /> сборки, к которой относится этот модуль не подпись Authenticode.
    /// </returns>
    public virtual X509Certificate GetSignerCertificate()
    {
      throw new NotImplementedException();
    }

    void _Module.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Module.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Module.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Module.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
