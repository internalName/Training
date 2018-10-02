// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIExpando
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IExpando instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
  internal interface UCOMIExpando : UCOMIReflect
  {
    FieldInfo AddField(string name);

    PropertyInfo AddProperty(string name);

    MethodInfo AddMethod(string name, Delegate method);

    void RemoveMember(MemberInfo m);
  }
}
