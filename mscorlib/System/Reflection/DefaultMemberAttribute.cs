// Decompiled with JetBrains decompiler
// Type: System.Reflection.DefaultMemberAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет элемент типа, который является элементом по умолчанию, используемые <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DefaultMemberAttribute : Attribute
  {
    private string m_memberName;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.DefaultMemberAttribute" />.
    /// </summary>
    /// <param name="memberName">
    ///   A <see langword="String" /> содержащая имя вызываемого члена.
    ///    Это может быть конструктор, метод, свойство или поле.
    ///    При вызове члена, необходимо указать подходящий атрибут вызова.
    ///    По умолчанию членом класса можно указать с помощью передачи пустой <see langword="String" /> имени элемента.
    /// 
    ///   Элемент по умолчанию типа помечается <see langword="DefaultMemberAttribute" /> настраиваемого атрибута или в COM-обычным способом.
    /// </param>
    [__DynamicallyInvokable]
    public DefaultMemberAttribute(string memberName)
    {
      this.m_memberName = memberName;
    }

    /// <summary>Возвращает имя из атрибута.</summary>
    /// <returns>Строка, представляющая имя элемента.</returns>
    [__DynamicallyInvokable]
    public string MemberName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_memberName;
      }
    }
  }
}
