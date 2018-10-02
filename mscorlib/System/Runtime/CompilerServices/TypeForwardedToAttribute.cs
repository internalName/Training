// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TypeForwardedToAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает назначение <see cref="T:System.Type" /> в другой сборке.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class TypeForwardedToAttribute : Attribute
  {
    private Type _destination;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.CompilerServices.TypeForwardedToAttribute" /> назначения класса <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="destination">
    ///   Назначение <see cref="T:System.Type" /> в другой сборке.
    /// </param>
    [__DynamicallyInvokable]
    public TypeForwardedToAttribute(Type destination)
    {
      this._destination = destination;
    }

    /// <summary>
    ///   Возвращает пункт назначения <see cref="T:System.Type" /> в другой сборке.
    /// </summary>
    /// <returns>
    ///   Назначение <see cref="T:System.Type" /> в другой сборке.
    /// </returns>
    [__DynamicallyInvokable]
    public Type Destination
    {
      [__DynamicallyInvokable] get
      {
        return this._destination;
      }
    }

    [SecurityCritical]
    internal static TypeForwardedToAttribute[] GetCustomAttribute(RuntimeAssembly assembly)
    {
      Type[] o = (Type[]) null;
      RuntimeAssembly.GetForwardedTypes(assembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref o));
      TypeForwardedToAttribute[] forwardedToAttributeArray = new TypeForwardedToAttribute[o.Length];
      for (int index = 0; index < o.Length; ++index)
        forwardedToAttributeArray[index] = new TypeForwardedToAttribute(o[index]);
      return forwardedToAttributeArray;
    }
  }
}
