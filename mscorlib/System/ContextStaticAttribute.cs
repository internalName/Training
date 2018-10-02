// Decompiled with JetBrains decompiler
// Type: System.ContextStaticAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает, что значение статического поля уникально для определенного контекста.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public class ContextStaticAttribute : Attribute
  {
  }
}
