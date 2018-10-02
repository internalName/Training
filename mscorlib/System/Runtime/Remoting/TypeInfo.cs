// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.TypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting
{
  [Serializable]
  internal class TypeInfo : IRemotingTypeInfo
  {
    private string serverType;
    private string[] serverHierarchy;
    private string[] interfacesImplemented;

    public virtual string TypeName
    {
      [SecurityCritical] get
      {
        return this.serverType;
      }
      [SecurityCritical] set
      {
        this.serverType = value;
      }
    }

    [SecurityCritical]
    public virtual bool CanCastTo(Type castType, object o)
    {
      if ((Type) null != castType)
      {
        if (castType == typeof (MarshalByRefObject) || castType == typeof (object))
          return true;
        if (castType.IsInterface)
        {
          if (this.interfacesImplemented != null)
            return this.CanCastTo(castType, this.InterfacesImplemented);
          return false;
        }
        if (castType.IsMarshalByRef && (this.CompareTypes(castType, this.serverType) || this.serverHierarchy != null && this.CanCastTo(castType, this.ServerHierarchy)))
          return true;
      }
      return false;
    }

    [SecurityCritical]
    internal static string GetQualifiedTypeName(RuntimeType type)
    {
      if (type == (RuntimeType) null)
        return (string) null;
      return RemotingServices.GetDefaultQualifiedTypeName(type);
    }

    internal static bool ParseTypeAndAssembly(string typeAndAssembly, out string typeName, out string assemName)
    {
      if (typeAndAssembly == null)
      {
        typeName = (string) null;
        assemName = (string) null;
        return false;
      }
      int length = typeAndAssembly.IndexOf(',');
      if (length == -1)
      {
        typeName = typeAndAssembly;
        assemName = (string) null;
        return true;
      }
      typeName = typeAndAssembly.Substring(0, length);
      assemName = typeAndAssembly.Substring(length + 1).Trim();
      return true;
    }

    [SecurityCritical]
    internal TypeInfo(RuntimeType typeOfObj)
    {
      this.ServerType = TypeInfo.GetQualifiedTypeName(typeOfObj);
      RuntimeType baseType1 = (RuntimeType) typeOfObj.BaseType;
      int length = 0;
      while ((Type) baseType1 != typeof (MarshalByRefObject) && baseType1 != (RuntimeType) null)
      {
        baseType1 = (RuntimeType) baseType1.BaseType;
        ++length;
      }
      string[] strArray1 = (string[]) null;
      if (length > 0)
      {
        strArray1 = new string[length];
        RuntimeType baseType2 = (RuntimeType) typeOfObj.BaseType;
        for (int index = 0; index < length; ++index)
        {
          strArray1[index] = TypeInfo.GetQualifiedTypeName(baseType2);
          baseType2 = (RuntimeType) baseType2.BaseType;
        }
      }
      this.ServerHierarchy = strArray1;
      Type[] interfaces = typeOfObj.GetInterfaces();
      string[] strArray2 = (string[]) null;
      bool isInterface = typeOfObj.IsInterface;
      if ((uint) interfaces.Length > 0U | isInterface)
      {
        strArray2 = new string[interfaces.Length + (isInterface ? 1 : 0)];
        for (int index = 0; index < interfaces.Length; ++index)
          strArray2[index] = TypeInfo.GetQualifiedTypeName((RuntimeType) interfaces[index]);
        if (isInterface)
          strArray2[strArray2.Length - 1] = TypeInfo.GetQualifiedTypeName(typeOfObj);
      }
      this.InterfacesImplemented = strArray2;
    }

    internal string ServerType
    {
      get
      {
        return this.serverType;
      }
      set
      {
        this.serverType = value;
      }
    }

    private string[] ServerHierarchy
    {
      get
      {
        return this.serverHierarchy;
      }
      set
      {
        this.serverHierarchy = value;
      }
    }

    private string[] InterfacesImplemented
    {
      get
      {
        return this.interfacesImplemented;
      }
      set
      {
        this.interfacesImplemented = value;
      }
    }

    [SecurityCritical]
    private bool CompareTypes(Type type1, string type2)
    {
      Type qualifiedTypeName = RemotingServices.InternalGetTypeFromQualifiedTypeName(type2);
      return type1 == qualifiedTypeName;
    }

    [SecurityCritical]
    private bool CanCastTo(Type castType, string[] types)
    {
      bool flag = false;
      if ((Type) null != castType)
      {
        for (int index = 0; index < types.Length; ++index)
        {
          if (this.CompareTypes(castType, types[index]))
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }
  }
}
