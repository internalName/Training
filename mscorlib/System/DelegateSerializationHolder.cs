// Decompiled with JetBrains decompiler
// Type: System.DelegateSerializationHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
  [Serializable]
  internal sealed class DelegateSerializationHolder : IObjectReference, ISerializable
  {
    private DelegateSerializationHolder.DelegateEntry m_delegateEntry;
    private MethodInfo[] m_methods;

    [SecurityCritical]
    internal static DelegateSerializationHolder.DelegateEntry GetDelegateSerializationInfo(SerializationInfo info, Type delegateType, object target, MethodInfo method, int targetIndex)
    {
      if (method == (MethodInfo) null)
        throw new ArgumentNullException(nameof (method));
      if (!method.IsPublic || method.DeclaringType != (Type) null && !method.DeclaringType.IsVisible)
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      Type baseType = delegateType.BaseType;
      if (baseType == (Type) null || baseType != typeof (Delegate) && baseType != typeof (MulticastDelegate))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
      if (method.DeclaringType == (Type) null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GlobalMethodSerialization"));
      DelegateSerializationHolder.DelegateEntry delegateEntry = new DelegateSerializationHolder.DelegateEntry(delegateType.FullName, delegateType.Module.Assembly.FullName, target, method.ReflectedType.Module.Assembly.FullName, method.ReflectedType.FullName, method.Name);
      if (info.MemberCount == 0)
      {
        info.SetType(typeof (DelegateSerializationHolder));
        info.AddValue("Delegate", (object) delegateEntry, typeof (DelegateSerializationHolder.DelegateEntry));
      }
      if (target != null)
      {
        string name = nameof (target) + (object) targetIndex;
        info.AddValue(name, delegateEntry.target);
        delegateEntry.target = (object) name;
      }
      string name1 = nameof (method) + (object) targetIndex;
      info.AddValue(name1, (object) method);
      return delegateEntry;
    }

    [SecurityCritical]
    private DelegateSerializationHolder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      bool flag = true;
      try
      {
        this.m_delegateEntry = (DelegateSerializationHolder.DelegateEntry) info.GetValue("Delegate", typeof (DelegateSerializationHolder.DelegateEntry));
      }
      catch
      {
        this.m_delegateEntry = this.OldDelegateWireFormat(info, context);
        flag = false;
      }
      if (!flag)
        return;
      DelegateSerializationHolder.DelegateEntry delegateEntry = this.m_delegateEntry;
      int length = 0;
      for (; delegateEntry != null; delegateEntry = delegateEntry.delegateEntry)
      {
        if (delegateEntry.target != null)
        {
          string target = delegateEntry.target as string;
          if (target != null)
            delegateEntry.target = info.GetValue(target, typeof (object));
        }
        ++length;
      }
      MethodInfo[] methodInfoArray = new MethodInfo[length];
      int index;
      for (index = 0; index < length; ++index)
      {
        string name = "method" + (object) index;
        methodInfoArray[index] = (MethodInfo) info.GetValueNoThrow(name, typeof (MethodInfo));
        if (methodInfoArray[index] == (MethodInfo) null)
          break;
      }
      if (index != length)
        return;
      this.m_methods = methodInfoArray;
    }

    private void ThrowInsufficientState(string field)
    {
      throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientDeserializationState", (object) field));
    }

    private DelegateSerializationHolder.DelegateEntry OldDelegateWireFormat(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      return new DelegateSerializationHolder.DelegateEntry(info.GetString("DelegateType"), info.GetString("DelegateAssembly"), info.GetValue("Target", typeof (object)), info.GetString("TargetTypeAssembly"), info.GetString("TargetTypeName"), info.GetString("MethodName"));
    }

    [SecurityCritical]
    private Delegate GetDelegate(DelegateSerializationHolder.DelegateEntry de, int index)
    {
      Delegate @delegate;
      try
      {
        if (de.methodName == null || de.methodName.Length == 0)
          this.ThrowInsufficientState("MethodName");
        if (de.assembly == null || de.assembly.Length == 0)
          this.ThrowInsufficientState("DelegateAssembly");
        if (de.targetTypeName == null || de.targetTypeName.Length == 0)
          this.ThrowInsufficientState("TargetTypeName");
        RuntimeType typeCompat1 = (RuntimeType) Assembly.GetType_Compat(de.assembly, de.type);
        RuntimeType typeCompat2 = (RuntimeType) Assembly.GetType_Compat(de.targetTypeAssembly, de.targetTypeName);
        if (this.m_methods != null)
        {
          object firstArgument = de.target != null ? RemotingServices.CheckCast(de.target, typeCompat2) : (object) null;
          @delegate = Delegate.CreateDelegateNoSecurityCheck(typeCompat1, firstArgument, this.m_methods[index]);
        }
        else
          @delegate = de.target == null ? Delegate.CreateDelegate((Type) typeCompat1, (Type) typeCompat2, de.methodName) : Delegate.CreateDelegate((Type) typeCompat1, RemotingServices.CheckCast(de.target, typeCompat2), de.methodName);
        if (!(@delegate.Method != (MethodInfo) null) || @delegate.Method.IsPublic)
        {
          if (@delegate.Method.DeclaringType != (Type) null)
          {
            if (@delegate.Method.DeclaringType.IsVisible)
              goto label_16;
          }
          else
            goto label_16;
        }
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      }
      catch (Exception ex)
      {
        if (ex is SerializationException)
          throw ex;
        throw new SerializationException(ex.Message, ex);
      }
label_16:
      return @delegate;
    }

    [SecurityCritical]
    public object GetRealObject(StreamingContext context)
    {
      int length = 0;
      for (DelegateSerializationHolder.DelegateEntry delegateEntry = this.m_delegateEntry; delegateEntry != null; delegateEntry = delegateEntry.Entry)
        ++length;
      int num = length - 1;
      if (length == 1)
        return (object) this.GetDelegate(this.m_delegateEntry, 0);
      object[] invocationList = new object[length];
      for (DelegateSerializationHolder.DelegateEntry de = this.m_delegateEntry; de != null; de = de.Entry)
      {
        --length;
        invocationList[length] = (object) this.GetDelegate(de, num - length);
      }
      return (object) ((MulticastDelegate) invocationList[0]).NewMulticastDelegate(invocationList, invocationList.Length);
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DelegateSerHolderSerial"));
    }

    [Serializable]
    internal class DelegateEntry
    {
      internal string type;
      internal string assembly;
      internal object target;
      internal string targetTypeAssembly;
      internal string targetTypeName;
      internal string methodName;
      internal DelegateSerializationHolder.DelegateEntry delegateEntry;

      internal DelegateEntry(string type, string assembly, object target, string targetTypeAssembly, string targetTypeName, string methodName)
      {
        this.type = type;
        this.assembly = assembly;
        this.target = target;
        this.targetTypeAssembly = targetTypeAssembly;
        this.targetTypeName = targetTypeName;
        this.methodName = methodName;
      }

      internal DelegateSerializationHolder.DelegateEntry Entry
      {
        get
        {
          return this.delegateEntry;
        }
        set
        {
          this.delegateEntry = value;
        }
      }
    }
  }
}
