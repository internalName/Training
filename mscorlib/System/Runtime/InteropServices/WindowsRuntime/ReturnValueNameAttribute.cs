// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ReturnValueNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Задает имя возвращаемого значения метода в компоненте Среда выполнения Windows.
  /// </summary>
  [AttributeUsage(AttributeTargets.Delegate | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class ReturnValueNameAttribute : Attribute
  {
    private string m_Name;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.WindowsRuntime.ReturnValueNameAttribute" /> класса и задает имя возвращаемого значения.
    /// </summary>
    /// <param name="name">Имя возвращаемого значения.</param>
    [__DynamicallyInvokable]
    public ReturnValueNameAttribute(string name)
    {
      this.m_Name = name;
    }

    /// <summary>
    ///   Возвращает имя, которое было указано для возвращаемого значения метода в Среда выполнения Windows компонента.
    /// </summary>
    /// <returns>Имя возвращаемого значения метода.</returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Name;
      }
    }
  }
}
