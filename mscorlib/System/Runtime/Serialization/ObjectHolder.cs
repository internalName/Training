// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ObjectHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
  internal sealed class ObjectHolder
  {
    internal const int INCOMPLETE_OBJECT_REFERENCE = 1;
    internal const int HAS_ISERIALIZABLE = 2;
    internal const int HAS_SURROGATE = 4;
    internal const int REQUIRES_VALUETYPE_FIXUP = 8;
    internal const int REQUIRES_DELAYED_FIXUP = 7;
    internal const int SER_INFO_FIXED = 16384;
    internal const int VALUETYPE_FIXUP_PERFORMED = 32768;
    private object m_object;
    internal long m_id;
    private int m_missingElementsRemaining;
    private int m_missingDecendents;
    internal SerializationInfo m_serInfo;
    internal ISerializationSurrogate m_surrogate;
    internal FixupHolderList m_missingElements;
    internal LongList m_dependentObjects;
    internal ObjectHolder m_next;
    internal int m_flags;
    private bool m_markForFixupWhenAvailable;
    private ValueTypeFixupInfo m_valueFixup;
    private TypeLoadExceptionHolder m_typeLoad;
    private bool m_reachable;

    internal ObjectHolder(long objID)
      : this((string) null, objID, (SerializationInfo) null, (ISerializationSurrogate) null, 0L, (FieldInfo) null, (int[]) null)
    {
    }

    internal ObjectHolder(object obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
    {
      this.m_object = obj;
      this.m_id = objID;
      this.m_flags = 0;
      this.m_missingElementsRemaining = 0;
      this.m_missingDecendents = 0;
      this.m_dependentObjects = (LongList) null;
      this.m_next = (ObjectHolder) null;
      this.m_serInfo = info;
      this.m_surrogate = surrogate;
      this.m_markForFixupWhenAvailable = false;
      if (obj is TypeLoadExceptionHolder)
        this.m_typeLoad = (TypeLoadExceptionHolder) obj;
      if (idOfContainingObj != 0L && (field != (FieldInfo) null && field.FieldType.IsValueType || arrayIndex != null))
      {
        if (idOfContainingObj == objID)
          throw new SerializationException(Environment.GetResourceString("Serialization_ParentChildIdentical"));
        this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
      }
      this.SetFlags();
    }

    internal ObjectHolder(string obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
    {
      this.m_object = (object) obj;
      this.m_id = objID;
      this.m_flags = 0;
      this.m_missingElementsRemaining = 0;
      this.m_missingDecendents = 0;
      this.m_dependentObjects = (LongList) null;
      this.m_next = (ObjectHolder) null;
      this.m_serInfo = info;
      this.m_surrogate = surrogate;
      this.m_markForFixupWhenAvailable = false;
      if (idOfContainingObj != 0L && arrayIndex != null)
        this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
      if (this.m_valueFixup == null)
        return;
      this.m_flags |= 8;
    }

    private void IncrementDescendentFixups(int amount)
    {
      this.m_missingDecendents += amount;
    }

    internal void DecrementFixupsRemaining(ObjectManager manager)
    {
      --this.m_missingElementsRemaining;
      if (!this.RequiresValueTypeFixup)
        return;
      this.UpdateDescendentDependencyChain(-1, manager);
    }

    internal void RemoveDependency(long id)
    {
      this.m_dependentObjects.RemoveElement(id);
    }

    internal void AddFixup(FixupHolder fixup, ObjectManager manager)
    {
      if (this.m_missingElements == null)
        this.m_missingElements = new FixupHolderList();
      this.m_missingElements.Add(fixup);
      ++this.m_missingElementsRemaining;
      if (!this.RequiresValueTypeFixup)
        return;
      this.UpdateDescendentDependencyChain(1, manager);
    }

    private void UpdateDescendentDependencyChain(int amount, ObjectManager manager)
    {
      ObjectHolder objectHolder = this;
      do
      {
        objectHolder = manager.FindOrCreateObjectHolder(objectHolder.ContainerID);
        objectHolder.IncrementDescendentFixups(amount);
      }
      while (objectHolder.RequiresValueTypeFixup);
    }

    internal void AddDependency(long dependentObject)
    {
      if (this.m_dependentObjects == null)
        this.m_dependentObjects = new LongList();
      this.m_dependentObjects.Add(dependentObject);
    }

    [SecurityCritical]
    internal void UpdateData(object obj, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainer, FieldInfo field, int[] arrayIndex, ObjectManager manager)
    {
      this.SetObjectValue(obj, manager);
      this.m_serInfo = info;
      this.m_surrogate = surrogate;
      if (idOfContainer != 0L && (field != (FieldInfo) null && field.FieldType.IsValueType || arrayIndex != null))
      {
        if (idOfContainer == this.m_id)
          throw new SerializationException(Environment.GetResourceString("Serialization_ParentChildIdentical"));
        this.m_valueFixup = new ValueTypeFixupInfo(idOfContainer, field, arrayIndex);
      }
      this.SetFlags();
      if (!this.RequiresValueTypeFixup)
        return;
      this.UpdateDescendentDependencyChain(this.m_missingElementsRemaining, manager);
    }

    internal void MarkForCompletionWhenAvailable()
    {
      this.m_markForFixupWhenAvailable = true;
    }

    internal void SetFlags()
    {
      if (this.m_object is IObjectReference)
        this.m_flags |= 1;
      this.m_flags &= -7;
      if (this.m_surrogate != null)
        this.m_flags |= 4;
      else if (this.m_object is ISerializable)
        this.m_flags |= 2;
      if (this.m_valueFixup == null)
        return;
      this.m_flags |= 8;
    }

    internal bool IsIncompleteObjectReference
    {
      get
      {
        return (uint) (this.m_flags & 1) > 0U;
      }
      set
      {
        if (value)
          this.m_flags |= 1;
        else
          this.m_flags &= -2;
      }
    }

    internal bool RequiresDelayedFixup
    {
      get
      {
        return (uint) (this.m_flags & 7) > 0U;
      }
    }

    internal bool RequiresValueTypeFixup
    {
      get
      {
        return (uint) (this.m_flags & 8) > 0U;
      }
    }

    internal bool ValueTypeFixupPerformed
    {
      get
      {
        if ((this.m_flags & 32768) != 0)
          return true;
        if (this.m_object == null)
          return false;
        if (this.m_dependentObjects != null)
          return this.m_dependentObjects.Count == 0;
        return true;
      }
      set
      {
        if (!value)
          return;
        this.m_flags |= 32768;
      }
    }

    internal bool HasISerializable
    {
      get
      {
        return (uint) (this.m_flags & 2) > 0U;
      }
    }

    internal bool HasSurrogate
    {
      get
      {
        return (uint) (this.m_flags & 4) > 0U;
      }
    }

    internal bool CanSurrogatedObjectValueChange
    {
      get
      {
        if (this.m_surrogate != null)
          return this.m_surrogate.GetType() != typeof (SurrogateForCyclicalReference);
        return true;
      }
    }

    internal bool CanObjectValueChange
    {
      get
      {
        if (this.IsIncompleteObjectReference)
          return true;
        if (this.HasSurrogate)
          return this.CanSurrogatedObjectValueChange;
        return false;
      }
    }

    internal int DirectlyDependentObjects
    {
      get
      {
        return this.m_missingElementsRemaining;
      }
    }

    internal int TotalDependentObjects
    {
      get
      {
        return this.m_missingElementsRemaining + this.m_missingDecendents;
      }
    }

    internal bool Reachable
    {
      get
      {
        return this.m_reachable;
      }
      set
      {
        this.m_reachable = value;
      }
    }

    internal bool TypeLoadExceptionReachable
    {
      get
      {
        return this.m_typeLoad != null;
      }
    }

    internal TypeLoadExceptionHolder TypeLoadException
    {
      get
      {
        return this.m_typeLoad;
      }
      set
      {
        this.m_typeLoad = value;
      }
    }

    internal object ObjectValue
    {
      get
      {
        return this.m_object;
      }
    }

    [SecurityCritical]
    internal void SetObjectValue(object obj, ObjectManager manager)
    {
      this.m_object = obj;
      if (obj == manager.TopObject)
        this.m_reachable = true;
      if (obj is TypeLoadExceptionHolder)
        this.m_typeLoad = (TypeLoadExceptionHolder) obj;
      if (!this.m_markForFixupWhenAvailable)
        return;
      manager.CompleteObject(this, true);
    }

    internal SerializationInfo SerializationInfo
    {
      get
      {
        return this.m_serInfo;
      }
      set
      {
        this.m_serInfo = value;
      }
    }

    internal ISerializationSurrogate Surrogate
    {
      get
      {
        return this.m_surrogate;
      }
    }

    internal LongList DependentObjects
    {
      get
      {
        return this.m_dependentObjects;
      }
      set
      {
        this.m_dependentObjects = value;
      }
    }

    internal bool RequiresSerInfoFixup
    {
      get
      {
        if ((this.m_flags & 4) == 0 && (this.m_flags & 2) == 0)
          return false;
        return (this.m_flags & 16384) == 0;
      }
      set
      {
        if (!value)
          this.m_flags |= 16384;
        else
          this.m_flags &= -16385;
      }
    }

    internal ValueTypeFixupInfo ValueFixup
    {
      get
      {
        return this.m_valueFixup;
      }
    }

    internal bool CompletelyFixed
    {
      get
      {
        if (!this.RequiresSerInfoFixup)
          return !this.IsIncompleteObjectReference;
        return false;
      }
    }

    internal long ContainerID
    {
      get
      {
        if (this.m_valueFixup != null)
          return this.m_valueFixup.ContainerID;
        return 0;
      }
    }
  }
}
