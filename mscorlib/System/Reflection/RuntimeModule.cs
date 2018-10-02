// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimeModule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
  [Serializable]
  internal class RuntimeModule : Module
  {
    private RuntimeType m_runtimeType;
    private RuntimeAssembly m_runtimeAssembly;
    private IntPtr m_pRefClass;
    private IntPtr m_pData;
    private IntPtr m_pGlobals;
    private IntPtr m_pFields;

    internal RuntimeModule()
    {
      throw new NotSupportedException();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetType(RuntimeModule module, string className, bool ignoreCase, bool throwOnError, ObjectHandleOnStack type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall")]
    private static extern bool nIsTransientInternal(RuntimeModule module);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetScopeName(RuntimeModule module, StringHandleOnStack retString);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetFullyQualifiedName(RuntimeModule module, StringHandleOnStack retString);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern RuntimeType[] GetTypes(RuntimeModule module);

    [SecuritySafeCritical]
    internal RuntimeType[] GetDefinedTypes()
    {
      return RuntimeModule.GetTypes(this.GetNativeHandle());
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsResource(RuntimeModule module);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetSignerCertificate(RuntimeModule module, ObjectHandleOnStack retData);

    private static RuntimeTypeHandle[] ConvertToTypeHandleArray(Type[] genericArguments)
    {
      if (genericArguments == null)
        return (RuntimeTypeHandle[]) null;
      int length = genericArguments.Length;
      RuntimeTypeHandle[] runtimeTypeHandleArray = new RuntimeTypeHandle[length];
      for (int index = 0; index < length; ++index)
      {
        Type genericArgument = genericArguments[index];
        if (genericArgument == (Type) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
        Type underlyingSystemType = genericArgument.UnderlyingSystemType;
        if (underlyingSystemType == (Type) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
        if ((object) (underlyingSystemType as RuntimeType) == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
        runtimeTypeHandleArray[index] = underlyingSystemType.GetTypeHandleInternal();
      }
      return runtimeTypeHandleArray;
    }

    [SecuritySafeCritical]
    public override byte[] ResolveSignature(int metadataToken)
    {
      System.Reflection.MetadataToken metadataToken1 = new System.Reflection.MetadataToken(metadataToken);
      if (!this.MetadataImport.IsValidToken((int) metadataToken1))
        throw new ArgumentOutOfRangeException(nameof (metadataToken), Environment.GetResourceString("Argument_InvalidToken", (object) metadataToken1, (object) this));
      if (!metadataToken1.IsMemberRef && !metadataToken1.IsMethodDef && (!metadataToken1.IsTypeSpec && !metadataToken1.IsSignature) && !metadataToken1.IsFieldDef)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidToken", (object) metadataToken1, (object) this), nameof (metadataToken));
      ConstArray constArray = !metadataToken1.IsMemberRef ? this.MetadataImport.GetSignatureFromToken(metadataToken) : this.MetadataImport.GetMemberRefProps(metadataToken);
      byte[] numArray = new byte[constArray.Length];
      for (int index = 0; index < constArray.Length; ++index)
        numArray[index] = constArray[index];
      return numArray;
    }

    [SecuritySafeCritical]
    public override unsafe MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      System.Reflection.MetadataToken metadataToken1 = new System.Reflection.MetadataToken(metadataToken);
      if (!this.MetadataImport.IsValidToken((int) metadataToken1))
        throw new ArgumentOutOfRangeException(nameof (metadataToken), Environment.GetResourceString("Argument_InvalidToken", (object) metadataToken1, (object) this));
      RuntimeTypeHandle[] typeHandleArray1 = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
      RuntimeTypeHandle[] typeHandleArray2 = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
      try
      {
        if (!metadataToken1.IsMethodDef && !metadataToken1.IsMethodSpec)
        {
          if (!metadataToken1.IsMemberRef)
            throw new ArgumentException(nameof (metadataToken), Environment.GetResourceString("Argument_ResolveMethod", (object) metadataToken1, (object) this));
          if (*(byte*) this.MetadataImport.GetMemberRefProps((int) metadataToken1).Signature.ToPointer() == (byte) 6)
            throw new ArgumentException(nameof (metadataToken), Environment.GetResourceString("Argument_ResolveMethod", (object) metadataToken1, (object) this));
        }
        IRuntimeMethodInfo runtimeMethodInfo = ModuleHandle.ResolveMethodHandleInternal(this.GetNativeHandle(), (int) metadataToken1, typeHandleArray1, typeHandleArray2);
        Type type = (Type) RuntimeMethodHandle.GetDeclaringType(runtimeMethodInfo);
        if (type.IsGenericType || type.IsArray)
        {
          System.Reflection.MetadataToken metadataToken2 = new System.Reflection.MetadataToken(this.MetadataImport.GetParentToken((int) metadataToken1));
          if (metadataToken1.IsMethodSpec)
            metadataToken2 = new System.Reflection.MetadataToken(this.MetadataImport.GetParentToken((int) metadataToken2));
          type = this.ResolveType((int) metadataToken2, genericTypeArguments, genericMethodArguments);
        }
        return RuntimeType.GetMethodBase(type as RuntimeType, runtimeMethodInfo);
      }
      catch (BadImageFormatException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), (Exception) ex);
      }
    }

    [SecurityCritical]
    private FieldInfo ResolveLiteralField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      System.Reflection.MetadataToken metadataToken1 = new System.Reflection.MetadataToken(metadataToken);
      if (!this.MetadataImport.IsValidToken((int) metadataToken1) || !metadataToken1.IsFieldDef)
        throw new ArgumentOutOfRangeException(nameof (metadataToken), string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_InvalidToken", (object) metadataToken1, (object) this), Array.Empty<object>()));
      string name = this.MetadataImport.GetName((int) metadataToken1).ToString();
      Type type = this.ResolveType(this.MetadataImport.GetParentToken((int) metadataToken1), genericTypeArguments, genericMethodArguments);
      type.GetFields();
      try
      {
        return type.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
      catch
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_ResolveField", (object) metadataToken1, (object) this), nameof (metadataToken));
      }
    }

    [SecuritySafeCritical]
    public override unsafe FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      System.Reflection.MetadataToken metadataToken1 = new System.Reflection.MetadataToken(metadataToken);
      if (!this.MetadataImport.IsValidToken((int) metadataToken1))
        throw new ArgumentOutOfRangeException(nameof (metadataToken), Environment.GetResourceString("Argument_InvalidToken", (object) metadataToken1, (object) this));
      RuntimeTypeHandle[] typeHandleArray1 = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
      RuntimeTypeHandle[] typeHandleArray2 = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
      try
      {
        IRuntimeFieldInfo runtimeFieldInfo = (IRuntimeFieldInfo) null;
        if (!metadataToken1.IsFieldDef)
        {
          if (!metadataToken1.IsMemberRef)
            throw new ArgumentException(nameof (metadataToken), Environment.GetResourceString("Argument_ResolveField", (object) metadataToken1, (object) this));
          if (*(byte*) this.MetadataImport.GetMemberRefProps((int) metadataToken1).Signature.ToPointer() != (byte) 6)
            throw new ArgumentException(nameof (metadataToken), Environment.GetResourceString("Argument_ResolveField", (object) metadataToken1, (object) this));
          runtimeFieldInfo = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), (int) metadataToken1, typeHandleArray1, typeHandleArray2);
        }
        IRuntimeFieldInfo field = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), metadataToken, typeHandleArray1, typeHandleArray2);
        RuntimeType reflectedType = RuntimeFieldHandle.GetApproxDeclaringType(field.Value);
        if (reflectedType.IsGenericType || reflectedType.IsArray)
          reflectedType = (RuntimeType) this.ResolveType(ModuleHandle.GetMetadataImport(this.GetNativeHandle()).GetParentToken(metadataToken), genericTypeArguments, genericMethodArguments);
        return RuntimeType.GetFieldInfo(reflectedType, field);
      }
      catch (MissingFieldException ex)
      {
        return this.ResolveLiteralField((int) metadataToken1, genericTypeArguments, genericMethodArguments);
      }
      catch (BadImageFormatException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), (Exception) ex);
      }
    }

    [SecuritySafeCritical]
    public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      System.Reflection.MetadataToken metadataToken1 = new System.Reflection.MetadataToken(metadataToken);
      if (metadataToken1.IsGlobalTypeDefToken)
        throw new ArgumentException(Environment.GetResourceString("Argument_ResolveModuleType", (object) metadataToken1), nameof (metadataToken));
      if (!this.MetadataImport.IsValidToken((int) metadataToken1))
        throw new ArgumentOutOfRangeException(nameof (metadataToken), Environment.GetResourceString("Argument_InvalidToken", (object) metadataToken1, (object) this));
      if (!metadataToken1.IsTypeDef && !metadataToken1.IsTypeSpec && !metadataToken1.IsTypeRef)
        throw new ArgumentException(Environment.GetResourceString("Argument_ResolveType", (object) metadataToken1, (object) this), nameof (metadataToken));
      RuntimeTypeHandle[] typeHandleArray1 = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
      RuntimeTypeHandle[] typeHandleArray2 = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
      try
      {
        Type runtimeType = (Type) this.GetModuleHandle().ResolveTypeHandle(metadataToken, typeHandleArray1, typeHandleArray2).GetRuntimeType();
        if (runtimeType == (Type) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_ResolveType", (object) metadataToken1, (object) this), nameof (metadataToken));
        return runtimeType;
      }
      catch (BadImageFormatException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), (Exception) ex);
      }
    }

    [SecuritySafeCritical]
    public override unsafe MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      System.Reflection.MetadataToken metadataToken1 = new System.Reflection.MetadataToken(metadataToken);
      if (metadataToken1.IsProperty)
        throw new ArgumentException(Environment.GetResourceString("InvalidOperation_PropertyInfoNotAvailable"));
      if (metadataToken1.IsEvent)
        throw new ArgumentException(Environment.GetResourceString("InvalidOperation_EventInfoNotAvailable"));
      if (metadataToken1.IsMethodSpec || metadataToken1.IsMethodDef)
        return (MemberInfo) this.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
      if (metadataToken1.IsFieldDef)
        return (MemberInfo) this.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
      if (metadataToken1.IsTypeRef || metadataToken1.IsTypeDef || metadataToken1.IsTypeSpec)
        return (MemberInfo) this.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
      if (metadataToken1.IsMemberRef)
      {
        if (!this.MetadataImport.IsValidToken((int) metadataToken1))
          throw new ArgumentOutOfRangeException(nameof (metadataToken), Environment.GetResourceString("Argument_InvalidToken", (object) metadataToken1, (object) this));
        if (*(byte*) this.MetadataImport.GetMemberRefProps((int) metadataToken1).Signature.ToPointer() == (byte) 6)
          return (MemberInfo) this.ResolveField((int) metadataToken1, genericTypeArguments, genericMethodArguments);
        return (MemberInfo) this.ResolveMethod((int) metadataToken1, genericTypeArguments, genericMethodArguments);
      }
      throw new ArgumentException(nameof (metadataToken), Environment.GetResourceString("Argument_ResolveMember", (object) metadataToken1, (object) this));
    }

    [SecuritySafeCritical]
    public override string ResolveString(int metadataToken)
    {
      System.Reflection.MetadataToken metadataToken1 = new System.Reflection.MetadataToken(metadataToken);
      if (!metadataToken1.IsString)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_ResolveString"), (object) metadataToken, (object) this.ToString()));
      if (!this.MetadataImport.IsValidToken((int) metadataToken1))
        throw new ArgumentOutOfRangeException(nameof (metadataToken), string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_InvalidToken", (object) metadataToken1, (object) this), Array.Empty<object>()));
      string userString = this.MetadataImport.GetUserString(metadataToken);
      if (userString == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_ResolveString"), (object) metadataToken, (object) this.ToString()));
      return userString;
    }

    public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
    {
      ModuleHandle.GetPEKind(this.GetNativeHandle(), out peKind, out machine);
    }

    public override int MDStreamVersion
    {
      [SecuritySafeCritical] get
      {
        return ModuleHandle.GetMDStreamVersion(this.GetNativeHandle());
      }
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return this.GetMethodInternal(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    internal MethodInfo GetMethodInternal(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (this.RuntimeType == (RuntimeType) null)
        return (MethodInfo) null;
      if (types == null)
        return this.RuntimeType.GetMethod(name, bindingAttr);
      return this.RuntimeType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    internal RuntimeType RuntimeType
    {
      get
      {
        if (this.m_runtimeType == (RuntimeType) null)
          this.m_runtimeType = ModuleHandle.GetModuleType(this.GetNativeHandle());
        return this.m_runtimeType;
      }
    }

    [SecuritySafeCritical]
    internal bool IsTransientInternal()
    {
      return RuntimeModule.nIsTransientInternal(this.GetNativeHandle());
    }

    internal MetadataImport MetadataImport
    {
      [SecurityCritical] get
      {
        return ModuleHandle.GetMetadataImport(this.GetNativeHandle());
      }
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      return CustomAttribute.GetCustomAttributes(this, typeof (object) as RuntimeType);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.GetCustomAttributes(this, underlyingSystemType);
    }

    [SecuritySafeCritical]
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.IsDefined(this, underlyingSystemType);
    }

    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return CustomAttributeData.GetCustomAttributesInternal(this);
    }

    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      UnitySerializationHolder.GetUnitySerializationInfo(info, 5, this.ScopeName, this.GetRuntimeAssembly());
    }

    [SecuritySafeCritical]
    [ComVisible(true)]
    public override Type GetType(string className, bool throwOnError, bool ignoreCase)
    {
      if (className == null)
        throw new ArgumentNullException(nameof (className));
      RuntimeType o = (RuntimeType) null;
      RuntimeModule.GetType(this.GetNativeHandle(), className, throwOnError, ignoreCase, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return (Type) o;
    }

    [SecurityCritical]
    internal string GetFullyQualifiedName()
    {
      string s = (string) null;
      RuntimeModule.GetFullyQualifiedName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    public override string FullyQualifiedName
    {
      [SecuritySafeCritical] get
      {
        string fullyQualifiedName = this.GetFullyQualifiedName();
        if (fullyQualifiedName != null)
        {
          bool flag = true;
          try
          {
            Path.GetFullPathInternal(fullyQualifiedName);
          }
          catch (ArgumentException ex)
          {
            flag = false;
          }
          if (flag)
            new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullyQualifiedName).Demand();
        }
        return fullyQualifiedName;
      }
    }

    [SecuritySafeCritical]
    public override Type[] GetTypes()
    {
      return (Type[]) RuntimeModule.GetTypes(this.GetNativeHandle());
    }

    public override Guid ModuleVersionId
    {
      [SecuritySafeCritical] get
      {
        Guid mvid;
        this.MetadataImport.GetScopeProps(out mvid);
        return mvid;
      }
    }

    public override int MetadataToken
    {
      [SecuritySafeCritical] get
      {
        return ModuleHandle.GetToken(this.GetNativeHandle());
      }
    }

    public override bool IsResource()
    {
      return RuntimeModule.IsResource(this.GetNativeHandle());
    }

    public override FieldInfo[] GetFields(BindingFlags bindingFlags)
    {
      if (this.RuntimeType == (RuntimeType) null)
        return new FieldInfo[0];
      return this.RuntimeType.GetFields(bindingFlags);
    }

    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (this.RuntimeType == (RuntimeType) null)
        return (FieldInfo) null;
      return this.RuntimeType.GetField(name, bindingAttr);
    }

    public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
    {
      if (this.RuntimeType == (RuntimeType) null)
        return new MethodInfo[0];
      return this.RuntimeType.GetMethods(bindingFlags);
    }

    public override string ScopeName
    {
      [SecuritySafeCritical] get
      {
        string s = (string) null;
        RuntimeModule.GetScopeName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s));
        return s;
      }
    }

    public override string Name
    {
      [SecuritySafeCritical] get
      {
        string fullyQualifiedName = this.GetFullyQualifiedName();
        int num = fullyQualifiedName.LastIndexOf('\\');
        if (num == -1)
          return fullyQualifiedName;
        return new string(fullyQualifiedName.ToCharArray(), num + 1, fullyQualifiedName.Length - num - 1);
      }
    }

    public override Assembly Assembly
    {
      get
      {
        return (Assembly) this.GetRuntimeAssembly();
      }
    }

    internal RuntimeAssembly GetRuntimeAssembly()
    {
      return this.m_runtimeAssembly;
    }

    internal override ModuleHandle GetModuleHandle()
    {
      return new ModuleHandle(this);
    }

    internal RuntimeModule GetNativeHandle()
    {
      return this;
    }

    [SecuritySafeCritical]
    public override X509Certificate GetSignerCertificate()
    {
      byte[] o = (byte[]) null;
      RuntimeModule.GetSignerCertificate(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      if (o == null)
        return (X509Certificate) null;
      return new X509Certificate(o);
    }
  }
}
