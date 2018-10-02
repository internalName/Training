// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.PreserveSigAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, что значение HRESULT или <see langword="retval" /> должны подавляться преобразование значения подписи во время вызовов COM-взаимодействия.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class PreserveSigAttribute : Attribute
  {
    internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
    {
      if ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) == MethodImplAttributes.IL)
        return (Attribute) null;
      return (Attribute) new PreserveSigAttribute();
    }

    internal static bool IsDefined(RuntimeMethodInfo method)
    {
      return (uint) (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > 0U;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.PreserveSigAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public PreserveSigAttribute()
    {
    }
  }
}
