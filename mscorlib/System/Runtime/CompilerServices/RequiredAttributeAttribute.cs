// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RequiredAttributeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, что импортирующий компилятор должен полностью понимать семантику определения типа или отказаться от его использования.
  ///     Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class RequiredAttributeAttribute : Attribute
  {
    private Type requiredContract;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.RequiredAttributeAttribute" />.
    /// </summary>
    /// <param name="requiredContract">
    ///   Тип, который должен быть полностью понятен импортирующему компилятору.
    /// 
    ///   Этот параметр не поддерживается в .NET Framework 2.0 и более поздних версий.
    /// </param>
    public RequiredAttributeAttribute(Type requiredContract)
    {
      this.requiredContract = requiredContract;
    }

    /// <summary>
    ///   Возвращает тип, который должен быть полностью понятен импортирующему компилятору.
    /// </summary>
    /// <returns>
    ///   Тип, который должен быть полностью понятен импортирующему компилятору.
    /// </returns>
    public Type RequiredContract
    {
      get
      {
        return this.requiredContract;
      }
    }
  }
}
