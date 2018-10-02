// Decompiled with JetBrains decompiler
// Type: System.Reflection.LocalVariableInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Обнаруживает атрибуты локальной переменной и предоставляет доступ к ее метаданным.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public class LocalVariableInfo
  {
    private RuntimeType m_type;
    private int m_isPinned;
    private int m_localIndex;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.LocalVariableInfo" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected LocalVariableInfo()
    {
    }

    /// <summary>
    ///   Возвращает удобным для чтения пользователем строку, описывающую локальную переменную.
    /// </summary>
    /// <returns>
    ///   Строка, отображающая сведения о локальной переменной, включая имя типа, индекс и закрепленные состояния.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      string str = this.LocalType.ToString() + " (" + (object) this.LocalIndex + ")";
      if (this.IsPinned)
        str += " (pinned)";
      return str;
    }

    /// <summary>Возвращает тип локальной переменной.</summary>
    /// <returns>Тип локальной переменной.</returns>
    [__DynamicallyInvokable]
    public virtual Type LocalType
    {
      [__DynamicallyInvokable] get
      {
        return (Type) this.m_type;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Boolean" /> значение, указывающее, является ли ссылка на объект в локальной переменной закреплен в памяти.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если объект, на который ссылается переменная закреплен в памяти; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsPinned
    {
      [__DynamicallyInvokable] get
      {
        return (uint) this.m_isPinned > 0U;
      }
    }

    /// <summary>
    ///   Возвращает индекс локальной переменной внутри тела метода.
    /// </summary>
    /// <returns>
    ///   Целочисленное значение, представляющее порядок объявления локальной переменной внутри тела метода.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual int LocalIndex
    {
      [__DynamicallyInvokable] get
      {
        return this.m_localIndex;
      }
    }
  }
}
