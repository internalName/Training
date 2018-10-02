// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.CodeAccessSecurityAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Указывает базовый класс атрибута для безопасности доступа к коду.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public abstract class CodeAccessSecurityAttribute : SecurityAttribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> с указанным <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    protected CodeAccessSecurityAttribute(SecurityAction action)
      : base(action)
    {
    }
  }
}
