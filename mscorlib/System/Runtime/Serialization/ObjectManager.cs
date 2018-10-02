// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ObjectManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace System.Runtime.Serialization
{
  /// <summary>Сохраняет сведения о объекты при их десериализации.</summary>
  [ComVisible(true)]
  public class ObjectManager
  {
    private static RuntimeType TypeOfWindowsIdentity = (RuntimeType) typeof (WindowsIdentity);
    private const int DefaultInitialSize = 16;
    private const int DefaultMaxArraySize = 4096;
    private const int NewMaxArraySize = 16777216;
    private const int MaxReferenceDepth = 100;
    private DeserializationEventHandler m_onDeserializationHandler;
    private SerializationEventHandler m_onDeserializedHandler;
    internal ObjectHolder[] m_objects;
    internal object m_topObject;
    internal ObjectHolderList m_specialFixupObjects;
    internal long m_fixupCount;
    internal ISurrogateSelector m_selector;
    internal StreamingContext m_context;
    private bool m_isCrossAppDomain;
    private readonly int MaxArraySize;
    private readonly int ArrayMask;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.ObjectManager" />.
    /// </summary>
    /// <param name="selector">
    ///   Суррогатный селектор.
    ///   <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> Определяет соответствующий суррогат для использования при десериализации объектов заданного типа.
    ///    Во время десериализации суррогатный селектор создает новый экземпляр объекта из сведений, переданных в потоке.
    /// </param>
    /// <param name="context">
    ///   Контекст потоковой передачи.
    ///   <see cref="T:System.Runtime.Serialization.StreamingContext" /> Не используется <see langword="ObjectManager" />, но передается как параметр объектами, реализующими <see cref="T:System.Runtime.Serialization.ISerializable" /> или <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />.
    ///    Эти объекты могут выполнять определенные действия в зависимости от источника сведений для десериализации.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public ObjectManager(ISurrogateSelector selector, StreamingContext context)
      : this(selector, context, true, false)
    {
    }

    [SecurityCritical]
    internal ObjectManager(ISurrogateSelector selector, StreamingContext context, bool checkSecurity, bool isCrossAppDomain)
    {
      if (checkSecurity)
        CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
      this.m_objects = new ObjectHolder[16];
      this.m_selector = selector;
      this.m_context = context;
      this.m_isCrossAppDomain = isCrossAppDomain;
      this.MaxArraySize = isCrossAppDomain || !AppContextSwitches.UseNewMaxArraySize ? 4096 : 16777216;
      this.ArrayMask = this.MaxArraySize - 1;
    }

    [SecurityCritical]
    private bool CanCallGetType(object obj)
    {
      return !RemotingServices.IsTransparentProxy(obj);
    }

    internal object TopObject
    {
      set
      {
        this.m_topObject = value;
      }
      get
      {
        return this.m_topObject;
      }
    }

    internal ObjectHolderList SpecialFixupObjects
    {
      get
      {
        if (this.m_specialFixupObjects == null)
          this.m_specialFixupObjects = new ObjectHolderList();
        return this.m_specialFixupObjects;
      }
    }

    internal ObjectHolder FindObjectHolder(long objectID)
    {
      int index = (int) (objectID & (long) this.ArrayMask);
      if (index >= this.m_objects.Length)
        return (ObjectHolder) null;
      ObjectHolder next = this.m_objects[index];
      while (next != null && next.m_id != objectID)
        next = next.m_next;
      return next;
    }

    internal ObjectHolder FindOrCreateObjectHolder(long objectID)
    {
      ObjectHolder holder = this.FindObjectHolder(objectID);
      if (holder == null)
      {
        holder = new ObjectHolder(objectID);
        this.AddObjectHolder(holder);
      }
      return holder;
    }

    private void AddObjectHolder(ObjectHolder holder)
    {
      if (holder.m_id >= (long) this.m_objects.Length && this.m_objects.Length != this.MaxArraySize)
      {
        int length = this.MaxArraySize;
        if (holder.m_id < (long) (this.MaxArraySize / 2))
        {
          length = this.m_objects.Length * 2;
          while ((long) length <= holder.m_id && length < this.MaxArraySize)
            length *= 2;
          if (length > this.MaxArraySize)
            length = this.MaxArraySize;
        }
        ObjectHolder[] objectHolderArray = new ObjectHolder[length];
        Array.Copy((Array) this.m_objects, (Array) objectHolderArray, this.m_objects.Length);
        this.m_objects = objectHolderArray;
      }
      int index = (int) (holder.m_id & (long) this.ArrayMask);
      ObjectHolder objectHolder = this.m_objects[index];
      holder.m_next = objectHolder;
      this.m_objects[index] = holder;
    }

    private bool GetCompletionInfo(FixupHolder fixup, out ObjectHolder holder, out object member, bool bThrowIfMissing)
    {
      member = fixup.m_fixupInfo;
      holder = this.FindObjectHolder(fixup.m_id);
      if (!holder.CompletelyFixed && holder.ObjectValue != null && holder.ObjectValue is ValueType)
      {
        this.SpecialFixupObjects.Add(holder);
        return false;
      }
      if (holder != null && !holder.CanObjectValueChange && holder.ObjectValue != null)
        return true;
      if (!bThrowIfMissing)
        return false;
      if (holder == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_NeverSeen", (object) fixup.m_id));
      if (holder.IsIncompleteObjectReference)
        throw new SerializationException(Environment.GetResourceString("Serialization_IORIncomplete", (object) fixup.m_id));
      throw new SerializationException(Environment.GetResourceString("Serialization_ObjectNotSupplied", (object) fixup.m_id));
    }

    [SecurityCritical]
    private void FixupSpecialObject(ObjectHolder holder)
    {
      ISurrogateSelector selector = (ISurrogateSelector) null;
      if (holder.HasSurrogate)
      {
        ISerializationSurrogate surrogate = holder.Surrogate;
        object obj = surrogate.SetObjectData(holder.ObjectValue, holder.SerializationInfo, this.m_context, selector);
        if (obj != null)
        {
          if (!holder.CanSurrogatedObjectValueChange && obj != holder.ObjectValue)
            throw new SerializationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_NotCyclicallyReferenceableSurrogate"), (object) surrogate.GetType().FullName));
          holder.SetObjectValue(obj, this);
        }
        holder.m_surrogate = (ISerializationSurrogate) null;
        holder.SetFlags();
      }
      else
        this.CompleteISerializableObject(holder.ObjectValue, holder.SerializationInfo, this.m_context);
      holder.SerializationInfo = (SerializationInfo) null;
      holder.RequiresSerInfoFixup = false;
      if (holder.RequiresValueTypeFixup && holder.ValueTypeFixupPerformed)
        this.DoValueTypeFixup((FieldInfo) null, holder, holder.ObjectValue);
      this.DoNewlyRegisteredObjectFixups(holder);
    }

    [SecurityCritical]
    private bool ResolveObjectReference(ObjectHolder holder)
    {
      int num = 0;
      try
      {
        object objectValue;
        do
        {
          objectValue = holder.ObjectValue;
          holder.SetObjectValue(((IObjectReference) holder.ObjectValue).GetRealObject(this.m_context), this);
          if (holder.ObjectValue == null)
          {
            holder.SetObjectValue(objectValue, this);
            return false;
          }
          if (num++ == 100)
            throw new SerializationException(Environment.GetResourceString("Serialization_TooManyReferences"));
          if (!(holder.ObjectValue is IObjectReference))
            break;
        }
        while (objectValue != holder.ObjectValue);
      }
      catch (NullReferenceException ex)
      {
        return false;
      }
      holder.IsIncompleteObjectReference = false;
      this.DoNewlyRegisteredObjectFixups(holder);
      return true;
    }

    [SecurityCritical]
    private bool DoValueTypeFixup(FieldInfo memberToFix, ObjectHolder holder, object value)
    {
      FieldInfo[] fieldInfoArray1 = new FieldInfo[4];
      int length = 0;
      int[] numArray = (int[]) null;
      object objectValue = holder.ObjectValue;
      while (holder.RequiresValueTypeFixup)
      {
        if (length + 1 >= fieldInfoArray1.Length)
        {
          FieldInfo[] fieldInfoArray2 = new FieldInfo[fieldInfoArray1.Length * 2];
          Array.Copy((Array) fieldInfoArray1, (Array) fieldInfoArray2, fieldInfoArray1.Length);
          fieldInfoArray1 = fieldInfoArray2;
        }
        ValueTypeFixupInfo valueFixup = holder.ValueFixup;
        objectValue = holder.ObjectValue;
        if (valueFixup.ParentField != (FieldInfo) null)
        {
          FieldInfo parentField = valueFixup.ParentField;
          ObjectHolder objectHolder = this.FindObjectHolder(valueFixup.ContainerID);
          if (objectHolder.ObjectValue != null)
          {
            if (Nullable.GetUnderlyingType(parentField.FieldType) != (Type) null)
            {
              fieldInfoArray1[length] = parentField.FieldType.GetField(nameof (value), BindingFlags.Instance | BindingFlags.NonPublic);
              ++length;
            }
            fieldInfoArray1[length] = parentField;
            holder = objectHolder;
            ++length;
          }
          else
            break;
        }
        else
        {
          holder = this.FindObjectHolder(valueFixup.ContainerID);
          numArray = valueFixup.ParentIndex;
          if (holder.ObjectValue != null)
            break;
          break;
        }
      }
      if (!(holder.ObjectValue is Array) && holder.ObjectValue != null)
        objectValue = holder.ObjectValue;
      if (length != 0)
      {
        FieldInfo[] flds = new FieldInfo[length];
        for (int index = 0; index < length; ++index)
        {
          FieldInfo fieldInfo = fieldInfoArray1[length - 1 - index];
          SerializationFieldInfo serializationFieldInfo = fieldInfo as SerializationFieldInfo;
          flds[index] = (FieldInfo) serializationFieldInfo == (FieldInfo) null ? fieldInfo : (FieldInfo) serializationFieldInfo.FieldInfo;
        }
        TypedReference target = TypedReference.MakeTypedReference(objectValue, flds);
        if (memberToFix != (FieldInfo) null)
          memberToFix.SetValueDirect(target, value);
        else
          TypedReference.SetTypedReference(target, value);
      }
      else if (memberToFix != (FieldInfo) null)
        FormatterServices.SerializationSetValue((MemberInfo) memberToFix, objectValue, value);
      if (numArray != null && holder.ObjectValue != null)
        ((Array) holder.ObjectValue).SetValue(objectValue, numArray);
      return true;
    }

    [Conditional("SER_LOGGING")]
    private void DumpValueTypeFixup(object obj, FieldInfo[] intermediateFields, FieldInfo memberToFix, object value)
    {
      StringBuilder stringBuilder = new StringBuilder("  " + obj);
      if (intermediateFields != null)
      {
        for (int index = 0; index < intermediateFields.Length; ++index)
          stringBuilder.Append("." + intermediateFields[index].Name);
      }
      stringBuilder.Append("." + memberToFix.Name + "=" + value);
    }

    [SecurityCritical]
    internal void CompleteObject(ObjectHolder holder, bool bObjectFullyComplete)
    {
      FixupHolderList missingElements = holder.m_missingElements;
      object member = (object) null;
      ObjectHolder holder1 = (ObjectHolder) null;
      int num = 0;
      if (holder.ObjectValue == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_MissingObject", (object) holder.m_id));
      if (missingElements == null)
        return;
      if (holder.HasSurrogate || holder.HasISerializable)
      {
        SerializationInfo serInfo = holder.m_serInfo;
        if (serInfo == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFixupDiscovered"));
        if (missingElements != null)
        {
          for (int index = 0; index < missingElements.m_count; ++index)
          {
            if (missingElements.m_values[index] != null && this.GetCompletionInfo(missingElements.m_values[index], out holder1, out member, bObjectFullyComplete))
            {
              object objectValue = holder1.ObjectValue;
              if (this.CanCallGetType(objectValue))
                serInfo.UpdateValue((string) member, objectValue, objectValue.GetType());
              else
                serInfo.UpdateValue((string) member, objectValue, typeof (MarshalByRefObject));
              ++num;
              missingElements.m_values[index] = (FixupHolder) null;
              if (!bObjectFullyComplete)
              {
                holder.DecrementFixupsRemaining(this);
                holder1.RemoveDependency(holder.m_id);
              }
            }
          }
        }
      }
      else
      {
        for (int index = 0; index < missingElements.m_count; ++index)
        {
          FixupHolder fixup = missingElements.m_values[index];
          if (fixup != null && this.GetCompletionInfo(fixup, out holder1, out member, bObjectFullyComplete))
          {
            if (holder1.TypeLoadExceptionReachable)
            {
              holder.TypeLoadException = holder1.TypeLoadException;
              if (holder.Reachable)
                throw new SerializationException(Environment.GetResourceString("Serialization_TypeLoadFailure", (object) holder.TypeLoadException.TypeName));
            }
            if (holder.Reachable)
              holder1.Reachable = true;
            switch (fixup.m_fixupType)
            {
              case 1:
                if (holder.RequiresValueTypeFixup)
                  throw new SerializationException(Environment.GetResourceString("Serialization_ValueTypeFixup"));
                ((Array) holder.ObjectValue).SetValue(holder1.ObjectValue, (int[]) member);
                break;
              case 2:
                MemberInfo fi = (MemberInfo) member;
                if (fi.MemberType != MemberTypes.Field)
                  throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
                if (holder.RequiresValueTypeFixup && holder.ValueTypeFixupPerformed)
                {
                  if (!this.DoValueTypeFixup((FieldInfo) fi, holder, holder1.ObjectValue))
                    throw new SerializationException(Environment.GetResourceString("Serialization_PartialValueTypeFixup"));
                }
                else
                  FormatterServices.SerializationSetValue(fi, holder.ObjectValue, holder1.ObjectValue);
                if (holder1.RequiresValueTypeFixup)
                {
                  holder1.ValueTypeFixupPerformed = true;
                  break;
                }
                break;
              default:
                throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
            }
            ++num;
            missingElements.m_values[index] = (FixupHolder) null;
            if (!bObjectFullyComplete)
            {
              holder.DecrementFixupsRemaining(this);
              holder1.RemoveDependency(holder.m_id);
            }
          }
        }
      }
      this.m_fixupCount -= (long) num;
      if (missingElements.m_count != num)
        return;
      holder.m_missingElements = (FixupHolderList) null;
    }

    [SecurityCritical]
    private void DoNewlyRegisteredObjectFixups(ObjectHolder holder)
    {
      if (holder.CanObjectValueChange)
        return;
      LongList dependentObjects = holder.DependentObjects;
      if (dependentObjects == null)
        return;
      dependentObjects.StartEnumeration();
      while (dependentObjects.MoveNext())
      {
        ObjectHolder objectHolder = this.FindObjectHolder(dependentObjects.Current);
        objectHolder.DecrementFixupsRemaining(this);
        if (objectHolder.DirectlyDependentObjects == 0)
        {
          if (objectHolder.ObjectValue != null)
            this.CompleteObject(objectHolder, true);
          else
            objectHolder.MarkForCompletionWhenAvailable();
        }
      }
    }

    /// <summary>
    ///   Возвращает объект с заданным идентификатором объекта.
    /// </summary>
    /// <param name="objectID">
    ///   Идентификатор запрашиваемого объекта.
    /// </param>
    /// <returns>
    ///   Объект с заданным Идентификатором объекта, если он был ранее сохранен или <see langword="null" /> если объект не был зарегистрирован.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="objectID" /> Меньше или равно нулю.
    /// </exception>
    public virtual object GetObject(long objectID)
    {
      if (objectID <= 0L)
        throw new ArgumentOutOfRangeException(nameof (objectID), Environment.GetResourceString("ArgumentOutOfRange_ObjectID"));
      ObjectHolder objectHolder = this.FindObjectHolder(objectID);
      if (objectHolder == null || objectHolder.CanObjectValueChange)
        return (object) null;
      return objectHolder.ObjectValue;
    }

    /// <summary>
    ///   Регистрирует объект при его десериализации, сопоставляя его с <paramref name="objectID" />.
    /// </summary>
    /// <param name="obj">Регистрируемый объект.</param>
    /// <param name="objectID">
    ///   Идентификатор объекта для регистрации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="objectID" /> Меньше или равно нулю.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="objectID" /> Уже зарегистрирован для объекта, отличного от <paramref name="obj" />.
    /// </exception>
    [SecurityCritical]
    public virtual void RegisterObject(object obj, long objectID)
    {
      this.RegisterObject(obj, objectID, (SerializationInfo) null, 0L, (MemberInfo) null);
    }

    /// <summary>
    ///   Регистрирует объект при его десериализации, сопоставляя его с <paramref name="objectID" />, и запись <see cref="T:System.Runtime.Serialization.SerializationInfo" /> используется вместе с ним.
    /// </summary>
    /// <param name="obj">Регистрируемый объект.</param>
    /// <param name="objectID">
    ///   Идентификатор объекта для регистрации.
    /// </param>
    /// <param name="info">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> Используется, если <paramref name="obj" /> реализует <see cref="T:System.Runtime.Serialization.ISerializable" /> или <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />.
    ///   <paramref name="info" /> завершится любую информацию, необходимые исправления, а затем передается необходимому объекту, когда объект завершается.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="objectID" /> Меньше или равно нулю.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="objectID" /> Уже зарегистрирован для объекта, отличного от <paramref name="obj" />.
    /// </exception>
    [SecurityCritical]
    public void RegisterObject(object obj, long objectID, SerializationInfo info)
    {
      this.RegisterObject(obj, objectID, info, 0L, (MemberInfo) null);
    }

    /// <summary>
    ///   Регистрирует члена объекта при его десериализации, сопоставляя его с <paramref name="objectID" />, и записи <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </summary>
    /// <param name="obj">Регистрируемый объект.</param>
    /// <param name="objectID">
    ///   Идентификатор объекта для регистрации.
    /// </param>
    /// <param name="info">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> Используется, если <paramref name="obj" /> реализует <see cref="T:System.Runtime.Serialization.ISerializable" /> или <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />.
    ///   <paramref name="info" /> завершится любую информацию, необходимые исправления, а затем передается необходимому объекту, когда объект завершается.
    /// </param>
    /// <param name="idOfContainingObj">
    ///   Идентификатор объекта, который содержит <paramref name="obj" />.
    ///    Этот параметр является обязательным, только если <paramref name="obj" /> является типом значения.
    /// </param>
    /// <param name="member">
    ///   Поле в содержащем объекте где <paramref name="obj" /> существует.
    ///    Этот параметр имеет смысл только в том случае, если <paramref name="obj" /> является типом значения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="objectID" /> Меньше или равно нулю.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="objectID" /> Уже зарегистрирован для объекта, отличного от <paramref name="obj" />, или <paramref name="member" /> не <see cref="T:System.Reflection.FieldInfo" /> и <paramref name="member" /> не <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void RegisterObject(object obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member)
    {
      this.RegisterObject(obj, objectID, info, idOfContainingObj, member, (int[]) null);
    }

    internal void RegisterString(string obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member)
    {
      this.AddObjectHolder(new ObjectHolder(obj, objectID, info, (ISerializationSurrogate) null, idOfContainingObj, (FieldInfo) member, (int[]) null));
    }

    /// <summary>
    ///   Регистрирует член массива, содержащийся в объекте при его десериализации, сопоставляя его с <paramref name="objectID" />, и записи <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </summary>
    /// <param name="obj">Регистрируемый объект.</param>
    /// <param name="objectID">
    ///   Идентификатор объекта для регистрации.
    /// </param>
    /// <param name="info">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> Используется, если <paramref name="obj" /> реализует <see cref="T:System.Runtime.Serialization.ISerializable" /> или <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />.
    ///   <paramref name="info" /> завершится любую информацию, необходимые исправления, а затем передается необходимому объекту, когда объект завершается.
    /// </param>
    /// <param name="idOfContainingObj">
    ///   Идентификатор объекта, который содержит <paramref name="obj" />.
    ///    Этот параметр является обязательным, только если <paramref name="obj" /> является типом значения.
    /// </param>
    /// <param name="member">
    ///   Поле в содержащем объекте где <paramref name="obj" /> существует.
    ///    Этот параметр имеет смысл только в том случае, если <paramref name="obj" /> является типом значения.
    /// </param>
    /// <param name="arrayIndex">
    ///   Если <paramref name="obj" /> является <see cref="T:System.ValueType" /> и членом массива <paramref name="arrayIndex" /> содержит индекс в этом массиве где <paramref name="obj" /> существует.
    ///   <paramref name="arrayIndex" /> учитывается, если <paramref name="obj" /> не является ни <see cref="T:System.ValueType" /> и является членом массива.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="objectID" /> Меньше или равно нулю.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="objectID" /> Уже зарегистрирован для объекта, отличного от <paramref name="obj" />, или <paramref name="member" /> не <see cref="T:System.Reflection.FieldInfo" /> и <paramref name="member" /> не <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void RegisterObject(object obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member, int[] arrayIndex)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (objectID <= 0L)
        throw new ArgumentOutOfRangeException(nameof (objectID), Environment.GetResourceString("ArgumentOutOfRange_ObjectID"));
      if (member != (MemberInfo) null && !(member is RuntimeFieldInfo) && !(member is SerializationFieldInfo))
        throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
      ISerializationSurrogate surrogate = (ISerializationSurrogate) null;
      if (this.m_selector != null)
      {
        ISurrogateSelector selector;
        surrogate = this.m_selector.GetSurrogate(!this.CanCallGetType(obj) ? typeof (MarshalByRefObject) : obj.GetType(), this.m_context, out selector);
      }
      if (obj is IDeserializationCallback)
        this.AddOnDeserialization(new DeserializationEventHandler(((IDeserializationCallback) obj).OnDeserialization));
      if (arrayIndex != null)
        arrayIndex = (int[]) arrayIndex.Clone();
      ObjectHolder objectHolder = this.FindObjectHolder(objectID);
      if (objectHolder == null)
      {
        ObjectHolder holder = new ObjectHolder(obj, objectID, info, surrogate, idOfContainingObj, (FieldInfo) member, arrayIndex);
        this.AddObjectHolder(holder);
        if (holder.RequiresDelayedFixup)
          this.SpecialFixupObjects.Add(holder);
        this.AddOnDeserialized(obj);
      }
      else
      {
        if (objectHolder.ObjectValue != null)
          throw new SerializationException(Environment.GetResourceString("Serialization_RegisterTwice"));
        objectHolder.UpdateData(obj, info, surrogate, idOfContainingObj, (FieldInfo) member, arrayIndex, this);
        if (objectHolder.DirectlyDependentObjects > 0)
          this.CompleteObject(objectHolder, false);
        if (objectHolder.RequiresDelayedFixup)
          this.SpecialFixupObjects.Add(objectHolder);
        if (objectHolder.CompletelyFixed)
        {
          this.DoNewlyRegisteredObjectFixups(objectHolder);
          objectHolder.DependentObjects = (LongList) null;
        }
        if (objectHolder.TotalDependentObjects > 0)
          this.AddOnDeserialized(obj);
        else
          this.RaiseOnDeserializedEvent(obj);
      }
    }

    [SecurityCritical]
    internal void CompleteISerializableObject(object obj, SerializationInfo info, StreamingContext context)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (!(obj is ISerializable))
        throw new ArgumentException(Environment.GetResourceString("Serialization_NotISer"));
      RuntimeType type = (RuntimeType) obj.GetType();
      RuntimeConstructorInfo runtimeConstructorInfo;
      try
      {
        runtimeConstructorInfo = !(type == ObjectManager.TypeOfWindowsIdentity) || !this.m_isCrossAppDomain ? ObjectManager.GetConstructor(type) : WindowsIdentity.GetSpecialSerializationCtor();
      }
      catch (Exception ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_ConstructorNotFound", (object) type), ex);
      }
      runtimeConstructorInfo.SerializationInvoke(obj, info, context);
    }

    internal static RuntimeConstructorInfo GetConstructor(RuntimeType t)
    {
      RuntimeConstructorInfo serializationCtor = t.GetSerializationCtor();
      if ((ConstructorInfo) serializationCtor == (ConstructorInfo) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_ConstructorNotFound", (object) t.FullName));
      return serializationCtor;
    }

    /// <summary>Выполняет все записанные адресные привязки.</summary>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Исправление не была завершена успешно.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void DoFixups()
    {
      int num = -1;
      while (num != 0)
      {
        num = 0;
        ObjectHolderListEnumerator fixupEnumerator = this.SpecialFixupObjects.GetFixupEnumerator();
        while (fixupEnumerator.MoveNext())
        {
          ObjectHolder current = fixupEnumerator.Current;
          if (current.ObjectValue == null)
            throw new SerializationException(Environment.GetResourceString("Serialization_ObjectNotSupplied", (object) current.m_id));
          if (current.TotalDependentObjects == 0)
          {
            if (current.RequiresSerInfoFixup)
            {
              this.FixupSpecialObject(current);
              ++num;
            }
            else if (!current.IsIncompleteObjectReference)
              this.CompleteObject(current, true);
            if (current.IsIncompleteObjectReference && this.ResolveObjectReference(current))
              ++num;
          }
        }
      }
      if (this.m_fixupCount == 0L)
      {
        if (this.TopObject is TypeLoadExceptionHolder)
          throw new SerializationException(Environment.GetResourceString("Serialization_TypeLoadFailure", (object) ((TypeLoadExceptionHolder) this.TopObject).TypeName));
      }
      else
      {
        for (int index = 0; index < this.m_objects.Length; ++index)
        {
          for (ObjectHolder next = this.m_objects[index]; next != null; next = next.m_next)
          {
            if (next.TotalDependentObjects > 0)
              this.CompleteObject(next, true);
          }
          if (this.m_fixupCount == 0L)
            return;
        }
        throw new SerializationException(Environment.GetResourceString("Serialization_IncorrectNumberOfFixups"));
      }
    }

    private void RegisterFixup(FixupHolder fixup, long objectToBeFixed, long objectRequired)
    {
      ObjectHolder createObjectHolder = this.FindOrCreateObjectHolder(objectToBeFixed);
      if (createObjectHolder.RequiresSerInfoFixup && fixup.m_fixupType == 2)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFixupType"));
      createObjectHolder.AddFixup(fixup, this);
      this.FindOrCreateObjectHolder(objectRequired).AddDependency(objectToBeFixed);
      ++this.m_fixupCount;
    }

    /// <summary>
    ///   Записывает адресную привязку для элемента объекта, который будет выполнен позже.
    /// </summary>
    /// <param name="objectToBeFixed">
    ///   Идентификатор объекта, которому требуется ссылка на <paramref name="objectRequired" /> объект.
    /// </param>
    /// <param name="member">
    ///   Элемент <paramref name="objectToBeFixed" /> где исправление должно быть выполнено.
    /// </param>
    /// <param name="objectRequired">
    ///   Идентификатор объекта, необходимые для <paramref name="objectToBeFixed" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="objectToBeFixed" /> Или <paramref name="objectRequired" /> меньше или равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="member" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual void RecordFixup(long objectToBeFixed, MemberInfo member, long objectRequired)
    {
      if (objectToBeFixed <= 0L || objectRequired <= 0L)
        throw new ArgumentOutOfRangeException(objectToBeFixed <= 0L ? nameof (objectToBeFixed) : nameof (objectRequired), Environment.GetResourceString("Serialization_IdTooSmall"));
      if (member == (MemberInfo) null)
        throw new ArgumentNullException(nameof (member));
      if (!(member is RuntimeFieldInfo) && !(member is SerializationFieldInfo))
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", (object) member.GetType().ToString()));
      this.RegisterFixup(new FixupHolder(objectRequired, (object) member, 2), objectToBeFixed, objectRequired);
    }

    /// <summary>
    ///   Записывает адресную привязку для элемента объекта, который будет выполнен позже.
    /// </summary>
    /// <param name="objectToBeFixed">
    ///   Идентификатор объекта, которому требуется ссылка на <paramref name="objectRequired" />.
    /// </param>
    /// <param name="memberName">
    ///   Имя члена <paramref name="objectToBeFixed" /> где исправление должно быть выполнено.
    /// </param>
    /// <param name="objectRequired">
    ///   Идентификатор объекта, необходимые для <paramref name="objectToBeFixed" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="objectToBeFixed" /> или <paramref name="objectRequired" /> меньше или равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="memberName" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual void RecordDelayedFixup(long objectToBeFixed, string memberName, long objectRequired)
    {
      if (objectToBeFixed <= 0L || objectRequired <= 0L)
        throw new ArgumentOutOfRangeException(objectToBeFixed <= 0L ? nameof (objectToBeFixed) : nameof (objectRequired), Environment.GetResourceString("Serialization_IdTooSmall"));
      if (memberName == null)
        throw new ArgumentNullException(nameof (memberName));
      this.RegisterFixup(new FixupHolder(objectRequired, (object) memberName, 4), objectToBeFixed, objectRequired);
    }

    /// <summary>
    ///   Записывает адресную привязку одного элемента в массив.
    /// </summary>
    /// <param name="arrayToBeFixed">
    ///   Идентификатор массива, используемого для записи адресной привязки.
    /// </param>
    /// <param name="index">
    ///   Индекс в <paramref name="arrayFixup" /> запрашиваются исправления.
    /// </param>
    /// <param name="objectRequired">
    ///   Идентификатор объекта, который будет указывать на текущий элемент массива после завершения исправления.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="arrayToBeFixed" /> Или <paramref name="objectRequired" /> меньше или равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="index" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual void RecordArrayElementFixup(long arrayToBeFixed, int index, long objectRequired)
    {
      int[] indices = new int[1]{ index };
      this.RecordArrayElementFixup(arrayToBeFixed, indices, objectRequired);
    }

    /// <summary>
    ///   Записывает адресные привязки заданных элементов в массив, который будет выполнен позже.
    /// </summary>
    /// <param name="arrayToBeFixed">
    ///   Идентификатор массива, используемого для записи адресной привязки.
    /// </param>
    /// <param name="indices">
    ///   Индексы в многомерном массиве, который запрошен для исправления.
    /// </param>
    /// <param name="objectRequired">
    ///   Идентификатор объекта, элементы массива будет указывать после завершения исправления.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="arrayToBeFixed" /> Или <paramref name="objectRequired" /> меньше или равно нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="indices" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual void RecordArrayElementFixup(long arrayToBeFixed, int[] indices, long objectRequired)
    {
      if (arrayToBeFixed <= 0L || objectRequired <= 0L)
        throw new ArgumentOutOfRangeException(arrayToBeFixed <= 0L ? "objectToBeFixed" : nameof (objectRequired), Environment.GetResourceString("Serialization_IdTooSmall"));
      if (indices == null)
        throw new ArgumentNullException(nameof (indices));
      this.RegisterFixup(new FixupHolder(objectRequired, (object) indices, 1), arrayToBeFixed, objectRequired);
    }

    /// <summary>
    ///   Вызывает событие десериализации для любого зарегистрированного объекта, который реализует <see cref="T:System.Runtime.Serialization.IDeserializationCallback" />.
    /// </summary>
    public virtual void RaiseDeserializationEvent()
    {
      if (this.m_onDeserializedHandler != null)
        this.m_onDeserializedHandler(this.m_context);
      if (this.m_onDeserializationHandler == null)
        return;
      this.m_onDeserializationHandler((object) null);
    }

    internal virtual void AddOnDeserialization(DeserializationEventHandler handler)
    {
      this.m_onDeserializationHandler += handler;
    }

    internal virtual void RemoveOnDeserialization(DeserializationEventHandler handler)
    {
      this.m_onDeserializationHandler -= handler;
    }

    [SecuritySafeCritical]
    internal virtual void AddOnDeserialized(object obj)
    {
      this.m_onDeserializedHandler = SerializationEventsCache.GetSerializationEventsForType(obj.GetType()).AddOnDeserialized(obj, this.m_onDeserializedHandler);
    }

    internal virtual void RaiseOnDeserializedEvent(object obj)
    {
      SerializationEventsCache.GetSerializationEventsForType(obj.GetType()).InvokeOnDeserialized(obj, this.m_context);
    }

    /// <summary>
    ///   Вызывает метод, помеченный атрибутом <see cref="T:System.Runtime.Serialization.OnDeserializingAttribute" />.
    /// </summary>
    /// <param name="obj">
    ///   Экземпляр типа, содержащего вызываемый метод.
    /// </param>
    public void RaiseOnDeserializingEvent(object obj)
    {
      SerializationEventsCache.GetSerializationEventsForType(obj.GetType()).InvokeOnDeserializing(obj, this.m_context);
    }
  }
}
