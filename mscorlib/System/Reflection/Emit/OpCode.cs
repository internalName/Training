// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.OpCode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Emit
{
  /// <summary>Описывает инструкцию IL.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public struct OpCode
  {
    internal const int OperandTypeMask = 31;
    internal const int FlowControlShift = 5;
    internal const int FlowControlMask = 15;
    internal const int OpCodeTypeShift = 9;
    internal const int OpCodeTypeMask = 7;
    internal const int StackBehaviourPopShift = 12;
    internal const int StackBehaviourPushShift = 17;
    internal const int StackBehaviourMask = 31;
    internal const int SizeShift = 22;
    internal const int SizeMask = 3;
    internal const int EndsUncondJmpBlkFlag = 16777216;
    internal const int StackChangeShift = 28;
    private string m_stringname;
    private StackBehaviour m_pop;
    private StackBehaviour m_push;
    private OperandType m_operand;
    private OpCodeType m_type;
    private int m_size;
    private byte m_s1;
    private byte m_s2;
    private FlowControl m_ctrl;
    private bool m_endsUncondJmpBlk;
    private int m_stackChange;
    private static volatile string[] g_nameCache;

    internal OpCode(OpCodeValues value, int flags)
    {
      this.m_stringname = (string) null;
      this.m_pop = (StackBehaviour) (flags >> 12 & 31);
      this.m_push = (StackBehaviour) (flags >> 17 & 31);
      this.m_operand = (OperandType) (flags & 31);
      this.m_type = (OpCodeType) (flags >> 9 & 7);
      this.m_size = flags >> 22 & 3;
      this.m_s1 = (byte) ((uint) value >> 8);
      this.m_s2 = (byte) value;
      this.m_ctrl = (FlowControl) (flags >> 5 & 15);
      this.m_endsUncondJmpBlk = (uint) (flags & 16777216) > 0U;
      this.m_stackChange = flags >> 28;
    }

    internal bool EndsUncondJmpBlk()
    {
      return this.m_endsUncondJmpBlk;
    }

    internal int StackChange()
    {
      return this.m_stackChange;
    }

    /// <summary>Тип операнда инструкции IL.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Тип операнда IL.
    /// </returns>
    [__DynamicallyInvokable]
    public OperandType OperandType
    {
      [__DynamicallyInvokable] get
      {
        return this.m_operand;
      }
    }

    /// <summary>
    ///   Производительность управления потоком для инструкции IL.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Тип управления потоком.
    /// </returns>
    [__DynamicallyInvokable]
    public FlowControl FlowControl
    {
      [__DynamicallyInvokable] get
      {
        return this.m_ctrl;
      }
    }

    /// <summary>Тип инструкции IL.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Тип инструкции IL.
    /// </returns>
    [__DynamicallyInvokable]
    public OpCodeType OpCodeType
    {
      [__DynamicallyInvokable] get
      {
        return this.m_type;
      }
    }

    /// <summary>
    ///   Описывает, каким образом инструкция IL извлекает данные из стека.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Способ, с помощью которого инструкция IL извлекает данные из стека.
    /// </returns>
    [__DynamicallyInvokable]
    public StackBehaviour StackBehaviourPop
    {
      [__DynamicallyInvokable] get
      {
        return this.m_pop;
      }
    }

    /// <summary>
    ///   Описывает, каким образом инструкция IL помещает операнд в стек.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Способ, с помощью которого инструкция IL помещает операнд в стек.
    /// </returns>
    [__DynamicallyInvokable]
    public StackBehaviour StackBehaviourPush
    {
      [__DynamicallyInvokable] get
      {
        return this.m_push;
      }
    }

    /// <summary>Размер инструкции IL.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Размер инструкции IL.
    /// </returns>
    [__DynamicallyInvokable]
    public int Size
    {
      [__DynamicallyInvokable] get
      {
        return this.m_size;
      }
    }

    /// <summary>
    ///   Возвращает числовое значение инструкции на промежуточном языке (IL).
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Числовое значение инструкции IL.
    /// </returns>
    [__DynamicallyInvokable]
    public short Value
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_size == 2)
          return (short) ((int) this.m_s1 << 8 | (int) this.m_s2);
        return (short) this.m_s2;
      }
    }

    /// <summary>Имя инструкции IL.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Имя инструкции IL.
    /// </returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        if (this.Size == 0)
          return (string) null;
        string[] strArray = OpCode.g_nameCache;
        if (strArray == null)
        {
          strArray = new string[287];
          OpCode.g_nameCache = strArray;
        }
        OpCodeValues opCodeValues = (OpCodeValues) (ushort) this.Value;
        int index = (int) opCodeValues;
        if (index > (int) byte.MaxValue)
        {
          if (index < 65024 || index > 65054)
            return (string) null;
          index = 256 + (index - 65024);
        }
        string str1 = Volatile.Read<string>(ref strArray[index]);
        if (str1 != null)
          return str1;
        string str2 = Enum.GetName(typeof (OpCodeValues), (object) opCodeValues).ToLowerInvariant().Replace("_", ".");
        Volatile.Write<string>(ref strArray[index], str2);
        return str2;
      }
    }

    /// <summary>
    ///   Проверяет, является ли заданный объект равен данном <see langword="Opcode" />.
    /// </summary>
    /// <param name="obj">Объект для сравнения на этот объект.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="Opcode" /> и равен этому объекту, в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is OpCode)
        return this.Equals((OpCode) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.OpCode" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.OpCode" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(OpCode obj)
    {
      return (int) obj.Value == (int) this.Value;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.OpCode" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.OpCode" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.OpCode" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(OpCode a, OpCode b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.OpCode" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.OpCode" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.OpCode" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(OpCode a, OpCode b)
    {
      return !(a == b);
    }

    /// <summary>
    ///   Возвращает созданный хэш-код для этого <see langword="Opcode" />.
    /// </summary>
    /// <returns>Возвращает хэш-код данного экземпляра.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this.Value;
    }

    /// <summary>
    ///   Возвращает этот <see langword="Opcode" /> как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.String" /> содержащий имя этого <see langword="Opcode" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.Name;
    }
  }
}
