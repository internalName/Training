// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Хранит все данные, необходимые для сериализации или десериализации объекта.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class SerializationInfo
  {
    private const int defaultSize = 4;
    private const string s_mscorlibAssemblySimpleName = "mscorlib";
    private const string s_mscorlibFileName = "mscorlib.dll";
    internal string[] m_members;
    internal object[] m_data;
    internal Type[] m_types;
    private Dictionary<string, int> m_nameToIndex;
    internal int m_currMember;
    internal IFormatterConverter m_converter;
    private string m_fullTypeName;
    private string m_assemName;
    private Type objectType;
    private bool isFullTypeNameSetExplicit;
    private bool isAssemblyNameSetExplicit;
    private bool requireSameTokenInPartialTrust;

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Объекта для сериализации.
    /// </param>
    /// <param name="converter">
    ///   <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> Использовать во время десериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> или <paramref name="converter" /> имеет значение <see langword="null" />.
    /// </exception>
    [CLSCompliant(false)]
    public SerializationInfo(Type type, IFormatterConverter converter)
      : this(type, converter, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Объекта для сериализации.
    /// </param>
    /// <param name="converter">
    ///   <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> Использовать во время десериализации.
    /// </param>
    /// <param name="requireSameTokenInPartialTrust">
    ///   Указывает, требуется ли объект тот же токен в режиме частичного доверия.
    /// </param>
    [CLSCompliant(false)]
    public SerializationInfo(Type type, IFormatterConverter converter, bool requireSameTokenInPartialTrust)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      if (converter == null)
        throw new ArgumentNullException(nameof (converter));
      this.objectType = type;
      this.m_fullTypeName = type.FullName;
      this.m_assemName = type.Module.Assembly.FullName;
      this.m_members = new string[4];
      this.m_data = new object[4];
      this.m_types = new Type[4];
      this.m_nameToIndex = new Dictionary<string, int>();
      this.m_converter = converter;
      this.requireSameTokenInPartialTrust = requireSameTokenInPartialTrust;
    }

    /// <summary>
    ///   Возвращает или задает полное имя <see cref="T:System.Type" /> для сериализации.
    /// </summary>
    /// <returns>Полное имя типа для сериализации.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Этому свойству присвоено значение <see langword="null" />.
    /// </exception>
    public string FullTypeName
    {
      get
      {
        return this.m_fullTypeName;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.m_fullTypeName = value;
        this.isFullTypeNameSetExplicit = true;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя сборки типа для сериализации только во время сериализации.
    /// </summary>
    /// <returns>Полное имя сборки типа для сериализации.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойству присвоено значение <see langword="null" />.
    /// </exception>
    public string AssemblyName
    {
      get
      {
        return this.m_assemName;
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (this.requireSameTokenInPartialTrust)
          SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.m_assemName, value);
        this.m_assemName = value;
        this.isAssemblyNameSetExplicit = true;
      }
    }

    /// <summary>
    ///   Наборы <see cref="T:System.Type" /> объекта для сериализации.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Объекта для сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void SetType(Type type)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      if (this.requireSameTokenInPartialTrust)
        SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.ObjectType.Assembly.FullName, type.Assembly.FullName);
      if ((object) this.objectType == (object) type)
        return;
      this.objectType = type;
      this.m_fullTypeName = type.FullName;
      this.m_assemName = type.Module.Assembly.FullName;
      this.isFullTypeNameSetExplicit = false;
      this.isAssemblyNameSetExplicit = false;
    }

    private static bool Compare(byte[] a, byte[] b)
    {
      if (a == null || b == null || (a.Length == 0 || b.Length == 0) || a.Length != b.Length)
        return false;
      for (int index = 0; index < a.Length; ++index)
      {
        if ((int) a[index] != (int) b[index])
          return false;
      }
      return true;
    }

    [SecuritySafeCritical]
    internal static void DemandForUnsafeAssemblyNameAssignments(string originalAssemblyName, string newAssemblyName)
    {
      if (SerializationInfo.IsAssemblyNameAssignmentSafe(originalAssemblyName, newAssemblyName))
        return;
      CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
    }

    internal static bool IsAssemblyNameAssignmentSafe(string originalAssemblyName, string newAssemblyName)
    {
      if (originalAssemblyName == newAssemblyName)
        return true;
      System.Reflection.AssemblyName assemblyName1 = new System.Reflection.AssemblyName(originalAssemblyName);
      System.Reflection.AssemblyName assemblyName2 = new System.Reflection.AssemblyName(newAssemblyName);
      if (string.Equals(assemblyName2.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) || string.Equals(assemblyName2.Name, "mscorlib.dll", StringComparison.OrdinalIgnoreCase))
        return false;
      return SerializationInfo.Compare(assemblyName1.GetPublicKeyToken(), assemblyName2.GetPublicKeyToken());
    }

    /// <summary>
    ///   Возвращает число элементов, которые были добавлены в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <returns>
    ///   Количество элементов, которые были добавлены в текущий <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </returns>
    public int MemberCount
    {
      get
      {
        return this.m_currMember;
      }
    }

    /// <summary>Возвращает тип объекта для сериализации.</summary>
    /// <returns>Тип сериализуемого объекта.</returns>
    public Type ObjectType
    {
      get
      {
        return this.objectType;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее явно задать полное имя типа.
    /// </summary>
    /// <returns>
    ///   <see langword="True" /> Если полное имя типа было задано явно; в противном случае <see langword="false" />.
    /// </returns>
    public bool IsFullTypeNameSetExplicit
    {
      get
      {
        return this.isFullTypeNameSetExplicit;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее имя сборки было задано явно.
    /// </summary>
    /// <returns>
    ///   <see langword="True" /> Если имя сборки было явно задано; в противном случае <see langword="false" />.
    /// </returns>
    public bool IsAssemblyNameSetExplicit
    {
      get
      {
        return this.isAssemblyNameSetExplicit;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> использовать для перебора пар имя значение в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> для синтаксического анализа пар имя значение, содержащихся в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </returns>
    public SerializationInfoEnumerator GetEnumerator()
    {
      return new SerializationInfoEnumerator(this.m_members, this.m_data, this.m_types, this.m_currMember);
    }

    private void ExpandArrays()
    {
      int length = this.m_currMember * 2;
      if (length < this.m_currMember && int.MaxValue > this.m_currMember)
        length = int.MaxValue;
      string[] strArray = new string[length];
      object[] objArray = new object[length];
      Type[] typeArray = new Type[length];
      Array.Copy((Array) this.m_members, (Array) strArray, this.m_currMember);
      Array.Copy((Array) this.m_data, (Array) objArray, this.m_currMember);
      Array.Copy((Array) this.m_types, (Array) typeArray, this.m_currMember);
      this.m_members = strArray;
      this.m_data = objArray;
      this.m_types = typeArray;
    }

    /// <summary>
    ///   Добавляет значение в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения, где <paramref name="value" /> связанных с <paramref name="name" /> и сериализуется как <see cref="T:System.Type" /><paramref name="type" />.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">
    ///   Значение должно быть сериализовано.
    ///    Все дочерние элементы этого объекта сериализуются автоматически.
    /// </param>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Для связи с текущим объектом.
    ///    Этот параметр всегда должен иметь тип самого объекта или одного из его базовых классов.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Если <paramref name="name" /> или <paramref name="type" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, object value, Type type)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      this.AddValueInternal(name, value, type);
    }

    /// <summary>
    ///   Добавляет указанный объект в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранилище, где он связан с указанным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">
    ///   Значение должно быть сериализовано.
    ///    Все дочерние элементы этого объекта сериализуются автоматически.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, object value)
    {
      if (value == null)
        this.AddValue(name, value, typeof (object));
      else
        this.AddValue(name, value, value.GetType());
    }

    /// <summary>
    ///   Добавляет логическое значение в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">Логическое значение для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, bool value)
    {
      this.AddValue(name, (object) value, typeof (bool));
    }

    /// <summary>
    ///   Добавляет значение символа Юникода в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">Значение символа для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, char value)
    {
      this.AddValue(name, (object) value, typeof (char));
    }

    /// <summary>
    ///   Добавляет значение 8-разрядного целого числа со знаком в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">
    ///   <see langword="Sbyte" /> Значение для сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    [CLSCompliant(false)]
    public void AddValue(string name, sbyte value)
    {
      this.AddValue(name, (object) value, typeof (sbyte));
    }

    /// <summary>
    ///   Добавляет значение 8-разрядное целое число без знака в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">Значение байта для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, byte value)
    {
      this.AddValue(name, (object) value, typeof (byte));
    }

    /// <summary>
    ///   Добавляет значение 16-разрядное целое число со знаком в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">
    ///   <see langword="Int16" /> Значение для сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, short value)
    {
      this.AddValue(name, (object) value, typeof (short));
    }

    /// <summary>
    ///   Добавляет значение 16-разрядное целое число без знака в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">
    ///   <see langword="UInt16" /> Значение для сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    [CLSCompliant(false)]
    public void AddValue(string name, ushort value)
    {
      this.AddValue(name, (object) value, typeof (ushort));
    }

    /// <summary>
    ///   Добавляет значение в 32-разрядное знаковое целое число <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">
    ///   <see langword="Int32" /> Значение для сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, int value)
    {
      this.AddValue(name, (object) value, typeof (int));
    }

    /// <summary>
    ///   Добавляет значение в 32-разрядное целое число без знака <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">
    ///   <see langword="UInt32" /> Значение для сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    [CLSCompliant(false)]
    public void AddValue(string name, uint value)
    {
      this.AddValue(name, (object) value, typeof (uint));
    }

    /// <summary>
    ///   Добавляет значение в 64-разрядное знаковое целое число <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">Значение Int64 для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, long value)
    {
      this.AddValue(name, (object) value, typeof (long));
    }

    /// <summary>
    ///   Добавляет значение в 64-разрядное целое число без знака <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">
    ///   <see langword="UInt64" /> Значение для сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    [CLSCompliant(false)]
    public void AddValue(string name, ulong value)
    {
      this.AddValue(name, (object) value, typeof (ulong));
    }

    /// <summary>
    ///   Добавляет значение с плавающей запятой одиночной точности в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">Одно значение для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, float value)
    {
      this.AddValue(name, (object) value, typeof (float));
    }

    /// <summary>
    ///   Добавляет значение с плавающей запятой двойной точности в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">Значение типа double для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, double value)
    {
      this.AddValue(name, (object) value, typeof (double));
    }

    /// <summary>
    ///   Добавляет десятичное значение в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">Десятичное значение для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Если <paramref name="name" /> параметр <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Если значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, Decimal value)
    {
      this.AddValue(name, (object) value, typeof (Decimal));
    }

    /// <summary>
    ///   Добавляет <see cref="T:System.DateTime" /> значение в <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">
    ///   Имя, связанное со значением, чтобы десериализовать позже.
    /// </param>
    /// <param name="value">
    ///   <see cref="T:System.DateTime" /> Значение для сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Значение уже связано с <paramref name="name" />.
    /// </exception>
    public void AddValue(string name, DateTime value)
    {
      this.AddValue(name, (object) value, typeof (DateTime));
    }

    internal void AddValueInternal(string name, object value, Type type)
    {
      if (this.m_nameToIndex.ContainsKey(name))
        throw new SerializationException(Environment.GetResourceString("Serialization_SameNameTwice"));
      this.m_nameToIndex.Add(name, this.m_currMember);
      if (this.m_currMember >= this.m_members.Length)
        this.ExpandArrays();
      this.m_members[this.m_currMember] = name;
      this.m_data[this.m_currMember] = value;
      this.m_types[this.m_currMember] = type;
      ++this.m_currMember;
    }

    internal void UpdateValue(string name, object value, Type type)
    {
      int element = this.FindElement(name);
      if (element < 0)
      {
        this.AddValueInternal(name, value, type);
      }
      else
      {
        this.m_data[element] = value;
        this.m_types[element] = type;
      }
    }

    private int FindElement(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      int num;
      if (this.m_nameToIndex.TryGetValue(name, out num))
        return num;
      return -1;
    }

    private object GetElement(string name, out Type foundType)
    {
      int element = this.FindElement(name);
      if (element == -1)
        throw new SerializationException(Environment.GetResourceString("Serialization_NotFound", (object) name));
      foundType = this.m_types[element];
      return this.m_data[element];
    }

    [ComVisible(true)]
    private object GetElementNoThrow(string name, out Type foundType)
    {
      int element = this.FindElement(name);
      if (element == -1)
      {
        foundType = (Type) null;
        return (object) null;
      }
      foundType = this.m_types[element];
      return this.m_data[element];
    }

    /// <summary>
    ///   Извлекает значение из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Извлекаемого значения.
    ///    Если сохраненное значение невозможно преобразовать в этот тип, система выдает <see cref="T:System.InvalidCastException" />.
    /// </param>
    /// <returns>
    ///   Объект заданного <see cref="T:System.Type" /> связанных с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в <paramref name="type" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    [SecuritySafeCritical]
    public object GetValue(string name, Type type)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      RuntimeType castType = type as RuntimeType;
      if (castType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (RemotingServices.IsTransparentProxy(element))
      {
        if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(element), castType))
          return element;
      }
      else if ((object) foundType == (object) type || type.IsAssignableFrom(foundType) || element == null)
        return element;
      return this.m_converter.Convert(element, type);
    }

    [SecuritySafeCritical]
    [ComVisible(true)]
    internal object GetValueNoThrow(string name, Type type)
    {
      Type foundType;
      object elementNoThrow = this.GetElementNoThrow(name, out foundType);
      if (elementNoThrow == null)
        return (object) null;
      if (RemotingServices.IsTransparentProxy(elementNoThrow))
      {
        if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(elementNoThrow), (RuntimeType) type))
          return elementNoThrow;
      }
      else if ((object) foundType == (object) type || type.IsAssignableFrom(foundType) || elementNoThrow == null)
        return elementNoThrow;
      return this.m_converter.Convert(elementNoThrow, type);
    }

    /// <summary>
    ///   Возвращает логическое значение из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   Логическое значение, связанное с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в логическое значение.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public bool GetBoolean(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (bool))
        return (bool) element;
      return this.m_converter.ToBoolean(element);
    }

    /// <summary>
    ///   Извлекает значение символа Юникода из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   Символ Юникода, связанный с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в символ Юникода.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public char GetChar(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (char))
        return (char) element;
      return this.m_converter.ToChar(element);
    }

    /// <summary>
    ///   Извлекает значение 8-разрядного целого числа со знаком из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   8-разрядное целое число со знаком, связанные с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в 8-разрядное целое число со знаком.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    [CLSCompliant(false)]
    public sbyte GetSByte(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (sbyte))
        return (sbyte) element;
      return this.m_converter.ToSByte(element);
    }

    /// <summary>
    ///   Извлекает значение 8-разрядное целое число без знака, из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   8-разрядное целое число без знака, связанные с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в 8-разрядное целое число без знака.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public byte GetByte(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (byte))
        return (byte) element;
      return this.m_converter.ToByte(element);
    }

    /// <summary>
    ///   Извлекает значение 16-разрядного целого числа со знаком из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   16-разрядное целое число со знаком, связанные с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в 16-разрядное целое число со знаком.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public short GetInt16(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (short))
        return (short) element;
      return this.m_converter.ToInt16(element);
    }

    /// <summary>
    ///   Извлекает значение 16-разрядное целое число без знака, из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   16-разрядное целое число без знака, связанные с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в 16-разрядное целое число без знака.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    [CLSCompliant(false)]
    public ushort GetUInt16(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (ushort))
        return (ushort) element;
      return this.m_converter.ToUInt16(element);
    }

    /// <summary>
    ///   Извлекает значение 32-разрядное знаковое целое число из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя извлекаемого значения.</param>
    /// <returns>
    ///   32-разрядное целое число со знаком, связанные с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в 32-разрядное целое число со знаком.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public int GetInt32(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (int))
        return (int) element;
      return this.m_converter.ToInt32(element);
    }

    /// <summary>
    ///   Извлекает значение 32-разрядное целое число без знака, из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   32-разрядное целое число без знака, связанные с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в 32-разрядное целое число без знака.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    [CLSCompliant(false)]
    public uint GetUInt32(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (uint))
        return (uint) element;
      return this.m_converter.ToUInt32(element);
    }

    /// <summary>
    ///   Извлекает значение из 64-разрядное знаковое целое число <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   64-разрядное целое число со знаком, связанные с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в 64-разрядное целое число со знаком.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public long GetInt64(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (long))
        return (long) element;
      return this.m_converter.ToInt64(element);
    }

    /// <summary>
    ///   Извлекает значение 64-разрядное целое число без знака, из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   64-разрядное целое число без знака, связанные с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в 64-разрядное целое число без знака.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    [CLSCompliant(false)]
    public ulong GetUInt64(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (ulong))
        return (ulong) element;
      return this.m_converter.ToUInt64(element);
    }

    /// <summary>
    ///   Извлекает значение одиночной точности с плавающей запятой от <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя извлекаемого значения.</param>
    /// <returns>
    ///   Значение с плавающей запятой одиночной точности, связанные с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в значение с плавающей запятой одиночной точности.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public float GetSingle(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (float))
        return (float) element;
      return this.m_converter.ToSingle(element);
    }

    /// <summary>
    ///   Извлекает значение двойной точности с плавающей запятой от <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   Значение двойной точности с плавающей запятой, связанное с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в значение с плавающей запятой двойной точности.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public double GetDouble(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (double))
        return (double) element;
      return this.m_converter.ToDouble(element);
    }

    /// <summary>
    ///   Извлекает десятичное значение из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   Десятичное значение из <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в десятичное число.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public Decimal GetDecimal(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (Decimal))
        return (Decimal) element;
      return this.m_converter.ToDecimal(element);
    }

    /// <summary>
    ///   Извлекает <see cref="T:System.DateTime" /> значение из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   <see cref="T:System.DateTime" /> Значение, связанное с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в <see cref="T:System.DateTime" /> значение.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public DateTime GetDateTime(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (DateTime))
        return (DateTime) element;
      return this.m_converter.ToDateTime(element);
    }

    /// <summary>
    ///   Извлекает <see cref="T:System.String" /> значение из <see cref="T:System.Runtime.Serialization.SerializationInfo" /> хранения.
    /// </summary>
    /// <param name="name">Имя, связанное с извлекаемого значения.</param>
    /// <returns>
    ///   Класс <see cref="T:System.String" />, связанный с <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Значение, связанное с <paramref name="name" /> невозможно преобразовать в <see cref="T:System.String" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент с указанным именем не найден в текущем экземпляре.
    /// </exception>
    public string GetString(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if ((object) foundType == (object) typeof (string) || element == null)
        return (string) element;
      return this.m_converter.ToString(element);
    }

    internal string[] MemberNames
    {
      get
      {
        return this.m_members;
      }
    }

    internal object[] MemberValues
    {
      get
      {
        return this.m_data;
      }
    }
  }
}
