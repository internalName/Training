// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityCriticalAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>
  ///   Указывает, что код или сборка выполняет критические с точки зрения безопасности операции.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class SecurityCriticalAttribute : Attribute
  {
    private SecurityCriticalScope _val;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityCriticalAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public SecurityCriticalAttribute()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.SecurityCriticalAttribute" /> класса с заданной областью.
    /// </summary>
    /// <param name="scope">
    ///   Одно из значений перечисления, определяющее область действия атрибута.
    /// </param>
    public SecurityCriticalAttribute(SecurityCriticalScope scope)
    {
      this._val = scope;
    }

    /// <summary>Получает область действия атрибута.</summary>
    /// <returns>
    ///   Одно из значений перечисления, определяющее область действия атрибута.
    ///    Значение по умолчанию — <see cref="F:System.Security.SecurityCriticalScope.Explicit" />, указывает, что атрибут применяется только явно указанный.
    /// </returns>
    [Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
    public SecurityCriticalScope Scope
    {
      get
      {
        return this._val;
      }
    }
  }
}
