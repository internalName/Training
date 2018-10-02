// Decompiled with JetBrains decompiler
// Type: System.Object
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>
  ///   Поддерживает все классы в иерархии классов .NET Framework и предоставляет низкоуровневые службы для производных классов.
  ///    Он является исходным базовым классом для всех классов платформы .NET Framework и корнем иерархии типов.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class Object
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Object" />.
    /// </summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public Object()
    {
    }

    /// <summary>Возвращает строку, представляющую текущий объект.</summary>
    /// <returns>Строка, представляющая текущий объект.</returns>
    [__DynamicallyInvokable]
    public virtual string ToString()
    {
      return this.GetType().ToString();
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект текущему объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект равен текущему объекту; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool Equals(object obj)
    {
      return RuntimeHelpers.Equals(this, obj);
    }

    /// <summary>
    ///   Определяет, считаются ли равными указанные экземпляры объектов.
    /// </summary>
    /// <param name="objA">Первый из сравниваемых объектов.</param>
    /// <param name="objB">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если указанные объекты равны; в противном случае — <see langword="false" />.
    ///    Если оба параметра <paramref name="objA" /> и <paramref name="objB" /> имеют значение NULL, метод возвращает значение <see langword="true" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool Equals(object objA, object objB)
    {
      if (objA == objB)
        return true;
      if (objA == null || objB == null)
        return false;
      return objA.Equals(objB);
    }

    /// <summary>
    ///   Определяет, совпадают ли указанные экземпляры <see cref="T:System.Object" />.
    /// </summary>
    /// <param name="objA">Первый из сравниваемых объектов.</param>
    /// <param name="objB">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />Если <paramref name="objA" /> является тем же экземпляром <paramref name="objB" /> или оба являются null; в противном случае — <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool ReferenceEquals(object objA, object objB)
    {
      return objA == objB;
    }

    /// <summary>Служит хэш-функцией по умолчанию.</summary>
    /// <returns>Хэш-код для текущего объекта.</returns>
    [__DynamicallyInvokable]
    public virtual int GetHashCode()
    {
      return RuntimeHelpers.GetHashCode(this);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" /> для текущего экземпляра.
    /// </summary>
    /// <returns>Точный тип текущего экземпляра в среде выполнения.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Type GetType();

    /// <summary>
    ///   Позволяет объекту попытаться освободить ресурсы и выполнить другие операции очистки, перед тем как он будет уничтожен во время сборки мусора.
    /// </summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    ~Object()
    {
    }

    /// <summary>
    ///   Создает неполную копию текущего объекта <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    ///   Неполная копия объекта <see cref="T:System.Object" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    protected extern object MemberwiseClone();

    [SecurityCritical]
    private void FieldSetter(string typeName, string fieldName, object val)
    {
      FieldInfo fieldInfo = this.GetFieldInfo(typeName, fieldName);
      if (fieldInfo.IsInitOnly)
        throw new FieldAccessException(Environment.GetResourceString("FieldAccess_InitOnly"));
      Message.CoerceArg(val, fieldInfo.FieldType);
      fieldInfo.SetValue(this, val);
    }

    private void FieldGetter(string typeName, string fieldName, ref object val)
    {
      FieldInfo fieldInfo = this.GetFieldInfo(typeName, fieldName);
      val = fieldInfo.GetValue(this);
    }

    private FieldInfo GetFieldInfo(string typeName, string fieldName)
    {
      Type type = this.GetType();
      while ((Type) null != type && !type.FullName.Equals(typeName))
        type = type.BaseType;
      if ((Type) null == type)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) typeName));
      FieldInfo field = type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
      if ((FieldInfo) null == field)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadField"), (object) fieldName, (object) typeName));
      return field;
    }
  }
}
