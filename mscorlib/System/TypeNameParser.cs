// Decompiled with JetBrains decompiler
// Type: System.TypeNameParser
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace System
{
  internal sealed class TypeNameParser : IDisposable
  {
    private static readonly char[] SPECIAL_CHARS = new char[7]
    {
      ',',
      '[',
      ']',
      '&',
      '*',
      '+',
      '\\'
    };
    [SecurityCritical]
    private SafeTypeNameParserHandle m_NativeParser;

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _CreateTypeNameParser(string typeName, ObjectHandleOnStack retHandle, bool throwOnError);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetNames(SafeTypeNameParserHandle pTypeNameParser, ObjectHandleOnStack retArray);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetTypeArguments(SafeTypeNameParserHandle pTypeNameParser, ObjectHandleOnStack retArray);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetModifiers(SafeTypeNameParserHandle pTypeNameParser, ObjectHandleOnStack retArray);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetAssemblyName(SafeTypeNameParserHandle pTypeNameParser, StringHandleOnStack retString);

    [SecuritySafeCritical]
    internal static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
    {
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      if (typeName.Length > 0 && typeName[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Format_StringZeroLength"));
      Type type = (Type) null;
      SafeTypeNameParserHandle typeNameParser1 = TypeNameParser.CreateTypeNameParser(typeName, throwOnError);
      if (typeNameParser1 != null)
      {
        using (TypeNameParser typeNameParser2 = new TypeNameParser(typeNameParser1))
          type = typeNameParser2.ConstructType(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
      }
      return type;
    }

    [SecuritySafeCritical]
    private TypeNameParser(SafeTypeNameParserHandle handle)
    {
      this.m_NativeParser = handle;
    }

    [SecuritySafeCritical]
    public void Dispose()
    {
      this.m_NativeParser.Dispose();
    }

    [SecuritySafeCritical]
    private unsafe Type ConstructType(Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
    {
      Assembly assembly = (Assembly) null;
      string assemblyName = this.GetAssemblyName();
      if (assemblyName.Length > 0)
      {
        assembly = TypeNameParser.ResolveAssembly(assemblyName, assemblyResolver, throwOnError, ref stackMark);
        if (assembly == (Assembly) null)
          return (Type) null;
      }
      string[] names = this.GetNames();
      if (names == null)
      {
        if (throwOnError)
          throw new TypeLoadException(Environment.GetResourceString("Arg_TypeLoadNullStr"));
        return (Type) null;
      }
      Type typeStart = TypeNameParser.ResolveType(assembly, names, typeResolver, throwOnError, ignoreCase, ref stackMark);
      if (typeStart == (Type) null)
        return (Type) null;
      SafeTypeNameParserHandle[] typeArguments = this.GetTypeArguments();
      Type[] genericArgs = (Type[]) null;
      if (typeArguments != null)
      {
        genericArgs = new Type[typeArguments.Length];
        for (int index = 0; index < typeArguments.Length; ++index)
        {
          using (TypeNameParser typeNameParser = new TypeNameParser(typeArguments[index]))
            genericArgs[index] = typeNameParser.ConstructType(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
          if (genericArgs[index] == (Type) null)
            return (Type) null;
        }
      }
      int[] modifiers = this.GetModifiers();
      fixed (int* numPtr = modifiers)
      {
        IntPtr pModifiers = new IntPtr((void*) numPtr);
        return RuntimeTypeHandle.GetTypeHelper(typeStart, genericArgs, pModifiers, modifiers == null ? 0 : modifiers.Length);
      }
    }

    [SecuritySafeCritical]
    private static Assembly ResolveAssembly(string asmName, Func<AssemblyName, Assembly> assemblyResolver, bool throwOnError, ref StackCrawlMark stackMark)
    {
      Assembly assembly;
      if (assemblyResolver == null)
      {
        if (throwOnError)
        {
          assembly = (Assembly) RuntimeAssembly.InternalLoad(asmName, (Evidence) null, ref stackMark, false);
        }
        else
        {
          try
          {
            assembly = (Assembly) RuntimeAssembly.InternalLoad(asmName, (Evidence) null, ref stackMark, false);
          }
          catch (FileNotFoundException ex)
          {
            return (Assembly) null;
          }
        }
      }
      else
      {
        assembly = assemblyResolver(new AssemblyName(asmName));
        if (assembly == (Assembly) null & throwOnError)
          throw new FileNotFoundException(Environment.GetResourceString("FileNotFound_ResolveAssembly", (object) asmName));
      }
      return assembly;
    }

    private static Type ResolveType(Assembly assembly, string[] names, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
    {
      string str = TypeNameParser.EscapeTypeName(names[0]);
      Type type;
      if (typeResolver != null)
      {
        type = typeResolver(assembly, str, ignoreCase);
        if (type == (Type) null & throwOnError)
        {
          string resourceString;
          if (!(assembly == (Assembly) null))
            resourceString = Environment.GetResourceString("TypeLoad_ResolveTypeFromAssembly", (object) str, (object) assembly.FullName);
          else
            resourceString = Environment.GetResourceString("TypeLoad_ResolveType", (object) str);
          throw new TypeLoadException(resourceString);
        }
      }
      else
        type = !(assembly == (Assembly) null) ? assembly.GetType(str, throwOnError, ignoreCase) : (Type) RuntimeType.GetType(str, throwOnError, ignoreCase, false, ref stackMark);
      if (type != (Type) null)
      {
        BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic;
        if (ignoreCase)
          bindingAttr |= BindingFlags.IgnoreCase;
        for (int index = 1; index < names.Length; ++index)
        {
          type = type.GetNestedType(names[index], bindingAttr);
          if (type == (Type) null)
          {
            if (throwOnError)
              throw new TypeLoadException(Environment.GetResourceString("TypeLoad_ResolveNestedType", (object) names[index], (object) names[index - 1]));
            break;
          }
        }
      }
      return type;
    }

    private static string EscapeTypeName(string name)
    {
      if (name.IndexOfAny(TypeNameParser.SPECIAL_CHARS) < 0)
        return name;
      StringBuilder sb = StringBuilderCache.Acquire(16);
      foreach (char ch in name)
      {
        if (Array.IndexOf<char>(TypeNameParser.SPECIAL_CHARS, ch) >= 0)
          sb.Append('\\');
        sb.Append(ch);
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    [SecuritySafeCritical]
    private static SafeTypeNameParserHandle CreateTypeNameParser(string typeName, bool throwOnError)
    {
      SafeTypeNameParserHandle o = (SafeTypeNameParserHandle) null;
      TypeNameParser._CreateTypeNameParser(typeName, JitHelpers.GetObjectHandleOnStack<SafeTypeNameParserHandle>(ref o), throwOnError);
      return o;
    }

    [SecuritySafeCritical]
    private string[] GetNames()
    {
      string[] o = (string[]) null;
      TypeNameParser._GetNames(this.m_NativeParser, JitHelpers.GetObjectHandleOnStack<string[]>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    private SafeTypeNameParserHandle[] GetTypeArguments()
    {
      SafeTypeNameParserHandle[] o = (SafeTypeNameParserHandle[]) null;
      TypeNameParser._GetTypeArguments(this.m_NativeParser, JitHelpers.GetObjectHandleOnStack<SafeTypeNameParserHandle[]>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    private int[] GetModifiers()
    {
      int[] o = (int[]) null;
      TypeNameParser._GetModifiers(this.m_NativeParser, JitHelpers.GetObjectHandleOnStack<int[]>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    private string GetAssemblyName()
    {
      string s = (string) null;
      TypeNameParser._GetAssemblyName(this.m_NativeParser, JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }
  }
}
