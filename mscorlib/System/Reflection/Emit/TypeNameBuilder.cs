// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.TypeNameBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
  internal class TypeNameBuilder
  {
    private IntPtr m_typeNameBuilder;

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern IntPtr CreateTypeNameBuilder();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void ReleaseTypeNameBuilder(IntPtr pAQN);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void OpenGenericArguments(IntPtr tnb);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void CloseGenericArguments(IntPtr tnb);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void OpenGenericArgument(IntPtr tnb);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void CloseGenericArgument(IntPtr tnb);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddName(IntPtr tnb, string name);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddPointer(IntPtr tnb);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddByRef(IntPtr tnb);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddSzArray(IntPtr tnb);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddArray(IntPtr tnb, int rank);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddAssemblySpec(IntPtr tnb, string assemblySpec);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void ToString(IntPtr tnb, StringHandleOnStack retString);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void Clear(IntPtr tnb);

    [SecuritySafeCritical]
    internal static string ToString(Type type, TypeNameBuilder.Format format)
    {
      if ((format == TypeNameBuilder.Format.FullName || format == TypeNameBuilder.Format.AssemblyQualifiedName) && (!type.IsGenericTypeDefinition && type.ContainsGenericParameters))
        return (string) null;
      TypeNameBuilder typeNameBuilder = new TypeNameBuilder(TypeNameBuilder.CreateTypeNameBuilder());
      typeNameBuilder.Clear();
      typeNameBuilder.ConstructAssemblyQualifiedNameWorker(type, format);
      string str = typeNameBuilder.ToString();
      typeNameBuilder.Dispose();
      return str;
    }

    private TypeNameBuilder(IntPtr typeNameBuilder)
    {
      this.m_typeNameBuilder = typeNameBuilder;
    }

    [SecurityCritical]
    internal void Dispose()
    {
      TypeNameBuilder.ReleaseTypeNameBuilder(this.m_typeNameBuilder);
    }

    [SecurityCritical]
    private void AddElementType(Type elementType)
    {
      if (elementType.HasElementType)
        this.AddElementType(elementType.GetElementType());
      if (elementType.IsPointer)
        this.AddPointer();
      else if (elementType.IsByRef)
        this.AddByRef();
      else if (elementType.IsSzArray)
      {
        this.AddSzArray();
      }
      else
      {
        if (!elementType.IsArray)
          return;
        this.AddArray(elementType.GetArrayRank());
      }
    }

    [SecurityCritical]
    private void ConstructAssemblyQualifiedNameWorker(Type type, TypeNameBuilder.Format format)
    {
      Type type1 = type;
      while (type1.HasElementType)
        type1 = type1.GetElementType();
      List<Type> typeList = new List<Type>();
      for (Type type2 = type1; type2 != (Type) null; type2 = type2.IsGenericParameter ? (Type) null : type2.DeclaringType)
        typeList.Add(type2);
      for (int index = typeList.Count - 1; index >= 0; --index)
      {
        Type type2 = typeList[index];
        string name = type2.Name;
        if (index == typeList.Count - 1 && type2.Namespace != null && type2.Namespace.Length != 0)
          name = type2.Namespace + "." + name;
        this.AddName(name);
      }
      if (type1.IsGenericType && (!type1.IsGenericTypeDefinition || format == TypeNameBuilder.Format.ToString))
      {
        Type[] genericArguments = type1.GetGenericArguments();
        this.OpenGenericArguments();
        for (int index = 0; index < genericArguments.Length; ++index)
        {
          TypeNameBuilder.Format format1 = format == TypeNameBuilder.Format.FullName ? TypeNameBuilder.Format.AssemblyQualifiedName : format;
          this.OpenGenericArgument();
          this.ConstructAssemblyQualifiedNameWorker(genericArguments[index], format1);
          this.CloseGenericArgument();
        }
        this.CloseGenericArguments();
      }
      this.AddElementType(type);
      if (format != TypeNameBuilder.Format.AssemblyQualifiedName)
        return;
      this.AddAssemblySpec(type.Module.Assembly.FullName);
    }

    [SecurityCritical]
    private void OpenGenericArguments()
    {
      TypeNameBuilder.OpenGenericArguments(this.m_typeNameBuilder);
    }

    [SecurityCritical]
    private void CloseGenericArguments()
    {
      TypeNameBuilder.CloseGenericArguments(this.m_typeNameBuilder);
    }

    [SecurityCritical]
    private void OpenGenericArgument()
    {
      TypeNameBuilder.OpenGenericArgument(this.m_typeNameBuilder);
    }

    [SecurityCritical]
    private void CloseGenericArgument()
    {
      TypeNameBuilder.CloseGenericArgument(this.m_typeNameBuilder);
    }

    [SecurityCritical]
    private void AddName(string name)
    {
      TypeNameBuilder.AddName(this.m_typeNameBuilder, name);
    }

    [SecurityCritical]
    private void AddPointer()
    {
      TypeNameBuilder.AddPointer(this.m_typeNameBuilder);
    }

    [SecurityCritical]
    private void AddByRef()
    {
      TypeNameBuilder.AddByRef(this.m_typeNameBuilder);
    }

    [SecurityCritical]
    private void AddSzArray()
    {
      TypeNameBuilder.AddSzArray(this.m_typeNameBuilder);
    }

    [SecurityCritical]
    private void AddArray(int rank)
    {
      TypeNameBuilder.AddArray(this.m_typeNameBuilder, rank);
    }

    [SecurityCritical]
    private void AddAssemblySpec(string assemblySpec)
    {
      TypeNameBuilder.AddAssemblySpec(this.m_typeNameBuilder, assemblySpec);
    }

    [SecuritySafeCritical]
    public override string ToString()
    {
      string s = (string) null;
      TypeNameBuilder.ToString(this.m_typeNameBuilder, JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    [SecurityCritical]
    private void Clear()
    {
      TypeNameBuilder.Clear(this.m_typeNameBuilder);
    }

    internal enum Format
    {
      ToString,
      FullName,
      AssemblyQualifiedName,
    }
  }
}
