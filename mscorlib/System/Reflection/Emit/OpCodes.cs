// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.OpCodes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Содержит поля, предоставляющие инструкции промежуточного языка MSIL (Microsoft) для выпуска <see cref="T:System.Reflection.Emit.ILGenerator" /> члены класса (например, <see cref="M:System.Reflection.Emit.ILGenerator.Emit(System.Reflection.Emit.OpCode)" />).
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public class OpCodes
  {
    /// <summary>
    ///   Заполняет пространство, если коды операции содержат исправления.
    ///    Никаких значимых операций не выполняется, хотя может быть пройден цикл обработки.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Nop = new OpCode(OpCodeValues.Nop, 6556325);
    /// <summary>
    ///   Сообщает инфраструктуре CLI, что необходимо оповестить отладчик о достижении точки останова.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Break = new OpCode(OpCodeValues.Break, 6556197);
    /// <summary>Загружает аргумент с индексом 0 в стек вычислений.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_0 = new OpCode(OpCodeValues.Ldarg_0, 275120805);
    /// <summary>Загружает аргумент с индексом 1 в стек вычислений.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_1 = new OpCode(OpCodeValues.Ldarg_1, 275120805);
    /// <summary>Загружает аргумент с индексом 2 в стек вычислений.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_2 = new OpCode(OpCodeValues.Ldarg_2, 275120805);
    /// <summary>Загружает аргумент с индексом 3 в стек вычислений.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_3 = new OpCode(OpCodeValues.Ldarg_3, 275120805);
    /// <summary>
    ///   Загружает в стек вычислений локальную переменную с индексом 0.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_0 = new OpCode(OpCodeValues.Ldloc_0, 275120805);
    /// <summary>
    ///   Загружает в стек вычислений локальную переменную с индексом 1.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_1 = new OpCode(OpCodeValues.Ldloc_1, 275120805);
    /// <summary>
    ///   Загружает в стек вычислений локальную переменную с индексом 2.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_2 = new OpCode(OpCodeValues.Ldloc_2, 275120805);
    /// <summary>
    ///   Загружает в стек вычислений локальную переменную с индексом 3.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_3 = new OpCode(OpCodeValues.Ldloc_3, 275120805);
    /// <summary>
    ///   Извлекает верхнее значение в стеке вычислений и сохраняет его в списке локальных переменных с индексом 0.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_0 = new OpCode(OpCodeValues.Stloc_0, -261877083);
    /// <summary>
    ///   Извлекает верхнее значение в стеке вычислений и сохраняет его в списке локальных переменных с индексом 1.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_1 = new OpCode(OpCodeValues.Stloc_1, -261877083);
    /// <summary>
    ///   Извлекает верхнее значение в стеке вычислений и сохраняет его в списке локальных переменных с индексом 2.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_2 = new OpCode(OpCodeValues.Stloc_2, -261877083);
    /// <summary>
    ///   Извлекает верхнее значение в стеке вычислений и сохраняет его в списке локальных переменных с индексом 3.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_3 = new OpCode(OpCodeValues.Stloc_3, -261877083);
    /// <summary>
    ///   Загружает аргумент (на который ссылается указанное короткое значение индекса) в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_S = new OpCode(OpCodeValues.Ldarg_S, 275120818);
    /// <summary>
    ///   Загружает адрес аргумента (короткая форма) в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarga_S = new OpCode(OpCodeValues.Ldarga_S, 275382962);
    /// <summary>
    ///   Сохраняет значение, находящееся на вершине стека вычислений, в ячейке аргумента с заданным индексом (короткая форма).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Starg_S = new OpCode(OpCodeValues.Starg_S, -261877070);
    /// <summary>
    ///   Загружает в стек вычислений локальную переменную с указанным индексом (короткая форма).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_S = new OpCode(OpCodeValues.Ldloc_S, 275120818);
    /// <summary>
    ///   Загружает в стек вычислений адрес локальной переменной с указанным индексом (короткая форма).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloca_S = new OpCode(OpCodeValues.Ldloca_S, 275382962);
    /// <summary>
    ///   Извлекает значение из верхней части стека вычислений и сохраняет его в списке локальных переменных с <paramref name="index" /> (короткая форма).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_S = new OpCode(OpCodeValues.Stloc_S, -261877070);
    /// <summary>
    ///   Передает пустую ссылку (тип <see langword="O" />) в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldnull = new OpCode(OpCodeValues.Ldnull, 275909285);
    /// <summary>
    ///   Помещает целочисленное значение-1 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_M1 = new OpCode(OpCodeValues.Ldc_I4_M1, 275382949);
    /// <summary>
    ///   Помещает целочисленное значение 0 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_0 = new OpCode(OpCodeValues.Ldc_I4_0, 275382949);
    /// <summary>
    ///   Помещает целочисленное значение 1 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_1 = new OpCode(OpCodeValues.Ldc_I4_1, 275382949);
    /// <summary>
    ///   Помещает целочисленное значение 2 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_2 = new OpCode(OpCodeValues.Ldc_I4_2, 275382949);
    /// <summary>
    ///   Помещает целочисленное значение 3 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_3 = new OpCode(OpCodeValues.Ldc_I4_3, 275382949);
    /// <summary>
    ///   Помещает целочисленное значение 4 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_4 = new OpCode(OpCodeValues.Ldc_I4_4, 275382949);
    /// <summary>
    ///   Помещает целочисленное значение 5 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_5 = new OpCode(OpCodeValues.Ldc_I4_5, 275382949);
    /// <summary>
    ///   Помещает целочисленное значение 6 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_6 = new OpCode(OpCodeValues.Ldc_I4_6, 275382949);
    /// <summary>
    ///   Помещает целочисленное значение 7 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_7 = new OpCode(OpCodeValues.Ldc_I4_7, 275382949);
    /// <summary>
    ///   Помещает целочисленное значение 8 в стек вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_8 = new OpCode(OpCodeValues.Ldc_I4_8, 275382949);
    /// <summary>
    ///   Помещает предоставленное <see langword="int8" /> значение в стек вычислений как <see langword="int32" />, краткая форма.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_S = new OpCode(OpCodeValues.Ldc_I4_S, 275382960);
    /// <summary>
    ///   Помещает переданное значение типа <see langword="int32" /> стек с <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4 = new OpCode(OpCodeValues.Ldc_I4, 275384994);
    /// <summary>
    ///   Помещает переданное значение типа <see langword="int64" /> в стек вычислений как <see langword="int64" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I8 = new OpCode(OpCodeValues.Ldc_I8, 275516067);
    /// <summary>
    ///   Помещает переданное значение типа <see langword="float32" /> в стек вычислений как тип <see langword="F" /> (float).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_R4 = new OpCode(OpCodeValues.Ldc_R4, 275647153);
    /// <summary>
    ///   Помещает переданное значение типа <see langword="float64" /> в стек вычислений как тип <see langword="F" /> (float).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_R8 = new OpCode(OpCodeValues.Ldc_R8, 275778215);
    /// <summary>
    ///   Копирует текущее верхнее значение в стеке вычислений и помещает копию в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Dup = new OpCode(OpCodeValues.Dup, 275258021);
    /// <summary>Удаляет значение, находящееся на вершине стека.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Pop = new OpCode(OpCodeValues.Pop, -261875035);
    /// <summary>
    ///   Прекращает выполнение текущего метода и переходит к заданному методу.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Jmp = new OpCode(OpCodeValues.Jmp, 23333444);
    /// <summary>
    ///   Вызывает метод, на который ссылается переданный дескриптор метода.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Call = new OpCode(OpCodeValues.Call, 7842372);
    /// <summary>
    ///   Вызывает метод, заданный в стеке вычислений (как указатель на точку входа), с аргументами, описанными в соглашении о вызовах.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Calli = new OpCode(OpCodeValues.Calli, 7842377);
    /// <summary>
    ///   Выполняет возврат из текущего метода, помещая возвращаемое значение (если имеется) из стека вычислений вызываемого метода в стек вычислений вызывающего метода.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ret = new OpCode(OpCodeValues.Ret, 23440101);
    /// <summary>
    ///   Обеспечивает безусловную передачу управления конечной инструкции (короткая форма).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Br_S = new OpCode(OpCodeValues.Br_S, 23331343);
    /// <summary>
    ///   Передает управление конечной инструкции, если <paramref name="value" /> является <see langword="false" />, пустая ссылка или ноль.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Brfalse_S = new OpCode(OpCodeValues.Brfalse_S, -261868945);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если <paramref name="value" /> является <see langword="true" />, не равно null или ненулевое значение.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Brtrue_S = new OpCode(OpCodeValues.Brtrue_S, -261868945);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если два значения равны.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Beq_S = new OpCode(OpCodeValues.Beq_S, -530308497);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если первое значение больше второго или равно ему.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bge_S = new OpCode(OpCodeValues.Bge_S, -530308497);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если первое значение больше второго.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bgt_S = new OpCode(OpCodeValues.Bgt_S, -530308497);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если первое значение меньше второго или равно ему.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ble_S = new OpCode(OpCodeValues.Ble_S, -530308497);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если первое значение меньше второго значения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Blt_S = new OpCode(OpCodeValues.Blt_S, -530308497);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма) в случае неравенства двух целочисленных значений без знака или двух неупорядоченных значений с плавающей запятой.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bne_Un_S = new OpCode(OpCodeValues.Bne_Un_S, -530308497);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если первое значение больше второго (при сравнении целочисленных значений без знака или неупорядоченных значений с плавающей запятой).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bge_Un_S = new OpCode(OpCodeValues.Bge_Un_S, -530308497);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если первое значение больше второго (при сравнении целочисленных значений без знака или неупорядоченных значений с плавающей запятой).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bgt_Un_S = new OpCode(OpCodeValues.Bgt_Un_S, -530308497);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если первое значение меньше второго или равно ему (при сравнении целочисленных значений без знака или неупорядоченных значений с плавающей запятой).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ble_Un_S = new OpCode(OpCodeValues.Ble_Un_S, -530308497);
    /// <summary>
    ///   Передает управление конечной инструкции (короткая форма), если первое значение меньше второго (при сравнении целочисленных значений без знака или неупорядоченных значений с плавающей запятой).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Blt_Un_S = new OpCode(OpCodeValues.Blt_Un_S, -530308497);
    /// <summary>
    ///   Обеспечивает безусловную передачу управления конечной инструкции.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Br = new OpCode(OpCodeValues.Br, 23333376);
    /// <summary>
    ///   Передает управление конечной инструкции, если <paramref name="value" /> является <see langword="false" />, пустая ссылка (<see langword="Nothing" /> в Visual Basic), или ноль.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Brfalse = new OpCode(OpCodeValues.Brfalse, -261866912);
    /// <summary>
    ///   Передает управление конечной инструкции, если <paramref name="value" /> является <see langword="true" />, не равно null или ненулевое значение.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Brtrue = new OpCode(OpCodeValues.Brtrue, -261866912);
    /// <summary>
    ///   Передает управление конечной инструкции, если два значения равны.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Beq = new OpCode(OpCodeValues.Beq, -530308512);
    /// <summary>
    ///   Передает управление конечной инструкции, если первое значение больше второго или равно ему.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bge = new OpCode(OpCodeValues.Bge, -530308512);
    /// <summary>
    ///   Передает управление конечной инструкции, если первое значение больше второго.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bgt = new OpCode(OpCodeValues.Bgt, -530308512);
    /// <summary>
    ///   Передает управление конечной инструкции, если первое значение меньше второго значения или равно ему.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ble = new OpCode(OpCodeValues.Ble, -530308512);
    /// <summary>
    ///   Передает управление конечной инструкции, если первое значение меньше второго.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Blt = new OpCode(OpCodeValues.Blt, -530308512);
    /// <summary>
    ///   Передает управление конечной инструкции в случае неравенства двух целочисленных значений без знака или двух неупорядоченных значений с плавающей запятой.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bne_Un = new OpCode(OpCodeValues.Bne_Un, -530308512);
    /// <summary>
    ///   Передает управление конечной инструкции, если первое значение больше второго (при сравнении целочисленных значений без знака или неупорядоченных значений с плавающей запятой).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bge_Un = new OpCode(OpCodeValues.Bge_Un, -530308512);
    /// <summary>
    ///   Передает управление конечной инструкции, если первое значение больше второго (при сравнении целочисленных значений без знака или неупорядоченных значений с плавающей запятой).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bgt_Un = new OpCode(OpCodeValues.Bgt_Un, -530308512);
    /// <summary>
    ///   Передает управление конечной инструкции, если первое значение меньше второго или равно ему (при сравнении целочисленных значений без знака или неупорядоченных значений с плавающей запятой).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ble_Un = new OpCode(OpCodeValues.Ble_Un, -530308512);
    /// <summary>
    ///   Передает управление конечной инструкции, если первое значение меньше второго (при сравнении целочисленных значений без знака или неупорядоченных значений с плавающей запятой).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Blt_Un = new OpCode(OpCodeValues.Blt_Un, -530308512);
    /// <summary>Реализует таблицу переходов.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Switch = new OpCode(OpCodeValues.Switch, -261866901);
    /// <summary>
    ///   Загружает значение типа <see langword="int8" /> как <see langword="int32" /> стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I1 = new OpCode(OpCodeValues.Ldind_I1, 6961829);
    /// <summary>
    ///   Загружает значение типа <see langword="unsigned int8" /> как <see langword="int32" /> стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_U1 = new OpCode(OpCodeValues.Ldind_U1, 6961829);
    /// <summary>
    ///   Загружает значение типа <see langword="int16" /> как <see langword="int32" /> стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I2 = new OpCode(OpCodeValues.Ldind_I2, 6961829);
    /// <summary>
    ///   Загружает значение типа <see langword="unsigned int16" /> как <see langword="int32" /> стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_U2 = new OpCode(OpCodeValues.Ldind_U2, 6961829);
    /// <summary>
    ///   Загружает значение типа <see langword="int32" /> как <see langword="int32" /> стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I4 = new OpCode(OpCodeValues.Ldind_I4, 6961829);
    /// <summary>
    ///   Загружает значение типа <see langword="unsigned int32" /> как <see langword="int32" /> стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_U4 = new OpCode(OpCodeValues.Ldind_U4, 6961829);
    /// <summary>
    ///   Загружает значение типа <see langword="int64" /> как <see langword="int64" /> стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I8 = new OpCode(OpCodeValues.Ldind_I8, 7092901);
    /// <summary>
    ///   Загружает значение типа <see langword="native int" /> как <see langword="native int" /> стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I = new OpCode(OpCodeValues.Ldind_I, 6961829);
    /// <summary>
    ///   Загружает значение типа <see langword="float32" /> как тип <see langword="F" /> (float) в стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_R4 = new OpCode(OpCodeValues.Ldind_R4, 7223973);
    /// <summary>
    ///   Загружает значение типа <see langword="float64" /> как тип <see langword="F" /> (float) в стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_R8 = new OpCode(OpCodeValues.Ldind_R8, 7355045);
    /// <summary>
    ///   Загружает ссылку на объект с типом <see langword="O" /> (ссылка на объект) стек вычислений косвенно.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_Ref = new OpCode(OpCodeValues.Ldind_Ref, 7486117);
    /// <summary>
    ///   Сохраняет значение ссылки на объект по указанному адресу.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_Ref = new OpCode(OpCodeValues.Stind_Ref, -530294107);
    /// <summary>
    ///   Сохраняет значение с типом <see langword="int8" /> по указанному адресу.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I1 = new OpCode(OpCodeValues.Stind_I1, -530294107);
    /// <summary>
    ///   Сохраняет значение с типом <see langword="int16" /> по указанному адресу.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I2 = new OpCode(OpCodeValues.Stind_I2, -530294107);
    /// <summary>
    ///   Сохраняет значение с типом <see langword="int32" /> по указанному адресу.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I4 = new OpCode(OpCodeValues.Stind_I4, -530294107);
    /// <summary>
    ///   Сохраняет значение с типом <see langword="int64" /> по указанному адресу.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I8 = new OpCode(OpCodeValues.Stind_I8, -530290011);
    /// <summary>
    ///   Сохраняет значение с типом <see langword="float32" /> по указанному адресу.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_R4 = new OpCode(OpCodeValues.Stind_R4, -530281819);
    /// <summary>
    ///   Сохраняет значение с типом <see langword="float64" /> по указанному адресу.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_R8 = new OpCode(OpCodeValues.Stind_R8, -530277723);
    /// <summary>
    ///   Складывает два значения и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Add = new OpCode(OpCodeValues.Add, -261739867);
    /// <summary>
    ///   Вычитает одно значение из другого и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Sub = new OpCode(OpCodeValues.Sub, -261739867);
    /// <summary>
    ///   Умножает два значения и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Mul = new OpCode(OpCodeValues.Mul, -261739867);
    /// <summary>
    ///   Производит деление двух значений и помещает результат в виде чисел с плавающей запятой (типа <see langword="F" />) или как частное (типа <see langword="int32" />) в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Div = new OpCode(OpCodeValues.Div, -261739867);
    /// <summary>
    ///   Выполняет деление двух целочисленных значений без знака и помещает результат (<see langword="int32" />) в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Div_Un = new OpCode(OpCodeValues.Div_Un, -261739867);
    /// <summary>
    ///   Делит одно значение на другое и помещает остаток в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Rem = new OpCode(OpCodeValues.Rem, -261739867);
    /// <summary>
    ///   Делит одно значение без знака на другое значение без знака и помещает остаток в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Rem_Un = new OpCode(OpCodeValues.Rem_Un, -261739867);
    /// <summary>
    ///   Вычисляет побитовое И двух значений и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode And = new OpCode(OpCodeValues.And, -261739867);
    /// <summary>
    ///   Вычисляет побитовое дополнение двух целочисленных значений, находящихся на вершине стека, и помещает результат в стек.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Or = new OpCode(OpCodeValues.Or, -261739867);
    /// <summary>
    ///   Вычисляет побитовое исключающее ИЛИ двух верхних значений в стеке вычислений и помещает результат обратно в стек.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Xor = new OpCode(OpCodeValues.Xor, -261739867);
    /// <summary>
    ///   Смещает целочисленное значение влево (с заполнением нулями) на заданное число бит и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Shl = new OpCode(OpCodeValues.Shl, -261739867);
    /// <summary>
    ///   Смещает целочисленное значение вправо (с знаковым битом) на заданное число бит и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Shr = new OpCode(OpCodeValues.Shr, -261739867);
    /// <summary>
    ///   Смещает целочисленное значение без знака вправо (с заполнением нулями) на заданное число бит и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Shr_Un = new OpCode(OpCodeValues.Shr_Un, -261739867);
    /// <summary>
    ///   Отвергает значение и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Neg = new OpCode(OpCodeValues.Neg, 6691493);
    /// <summary>
    ///   Вычисляет побитовое дополнение целочисленного значения, находящегося на вершине стека, и помещает результат в стек с тем же типом.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Not = new OpCode(OpCodeValues.Not, 6691493);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="int8" />, затем расширяет его <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I1 = new OpCode(OpCodeValues.Conv_I1, 6953637);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="int16" />, затем расширяет его <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I2 = new OpCode(OpCodeValues.Conv_I2, 6953637);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I4 = new OpCode(OpCodeValues.Conv_I4, 6953637);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="int64" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I8 = new OpCode(OpCodeValues.Conv_I8, 7084709);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="float32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_R4 = new OpCode(OpCodeValues.Conv_R4, 7215781);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="float64" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_R8 = new OpCode(OpCodeValues.Conv_R8, 7346853);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="unsigned int32" />, и расширяет его до <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U4 = new OpCode(OpCodeValues.Conv_U4, 6953637);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="unsigned int64" />, и расширяет его до <see langword="int64" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U8 = new OpCode(OpCodeValues.Conv_U8, 7084709);
    /// <summary>
    ///   Вызывает метод объекта с поздней привязкой и помещает возвращаемое значение в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Callvirt = new OpCode(OpCodeValues.Callvirt, 7841348);
    /// <summary>
    ///   Копирует тип значения по адресу объекта (тип <see langword="&amp;" />, <see langword="*" /> или <see langword="native int" />) по адресу конечного объекта (тип <see langword="&amp;" />, <see langword="*" /> или <see langword="native int" />).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Cpobj = new OpCode(OpCodeValues.Cpobj, -530295123);
    /// <summary>
    ///   Копирует объект с типом значения, размещенный по указанному адресу, на вершину стека вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldobj = new OpCode(OpCodeValues.Ldobj, 6698669);
    /// <summary>
    ///   Помещает в стек ссылку на новый объект, представляющий строковой литерал, хранящийся в метаданных.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldstr = new OpCode(OpCodeValues.Ldstr, 275908266);
    /// <summary>
    ///   Создает новый объект или новый экземпляр типа значения и помещает ссылку на объект (тип <see langword="O" />) в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Newobj = new OpCode(OpCodeValues.Newobj, 276014660);
    /// <summary>
    ///   Предпринимает попытку привести объект, передаваемый по ссылке, к указанному классу.
    /// </summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static readonly OpCode Castclass = new OpCode(OpCodeValues.Castclass, 7513773);
    /// <summary>
    ///   Проверяет, является ли ссылка на объект (тип <see langword="O" />) является экземпляром определенного класса.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Isinst = new OpCode(OpCodeValues.Isinst, 6989485);
    /// <summary>
    ///   Преобразует значение целого числа без знака на вершине стека вычислений в <see langword="float32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_R_Un = new OpCode(OpCodeValues.Conv_R_Un, 7346853);
    /// <summary>
    ///   Преобразует тип значения из упакованной формы в распакованную.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Unbox = new OpCode(OpCodeValues.Unbox, 6990509);
    /// <summary>
    ///   Создает объект исключения, находящийся в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Throw = new OpCode(OpCodeValues.Throw, -245061883);
    /// <summary>
    ///   Выполняет поиск значения поля в объекте, ссылка на который находится в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldfld = new OpCode(OpCodeValues.Ldfld, 6727329);
    /// <summary>
    ///   Ищет адрес поля в объекте, ссылка на который находится в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldflda = new OpCode(OpCodeValues.Ldflda, 6989473);
    /// <summary>
    ///   Заменяет значение в поле объекта, по ссылке на объект или указателю, на новое значение.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stfld = new OpCode(OpCodeValues.Stfld, -530270559);
    /// <summary>
    ///   Помещает в стек вычислений значение статического поля.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldsfld = new OpCode(OpCodeValues.Ldsfld, 275121825);
    /// <summary>Помещает в стек вычислений адрес статического поля.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldsflda = new OpCode(OpCodeValues.Ldsflda, 275383969);
    /// <summary>
    ///   Заменяет значение статического поля на значение из стека вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stsfld = new OpCode(OpCodeValues.Stsfld, -261876063);
    /// <summary>
    ///   Копирует значение с заданным типом из стека вычислений в указанный адрес памяти.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stobj = new OpCode(OpCodeValues.Stobj, -530298195);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений со знаком <see langword="int8" /> и расширяет его до <see langword="int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(OpCodeValues.Conv_Ovf_I1_Un, 6953637);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений со знаком <see langword="int16" /> и расширяет его до <see langword="int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(OpCodeValues.Conv_Ovf_I2_Un, 6953637);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений со знаком <see langword="int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(OpCodeValues.Conv_Ovf_I4_Un, 6953637);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений со знаком <see langword="int64" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(OpCodeValues.Conv_Ovf_I8_Un, 7084709);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений <see langword="unsigned int8" /> и расширяет его до <see langword="int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(OpCodeValues.Conv_Ovf_U1_Un, 6953637);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений <see langword="unsigned int16" /> и расширяет его до <see langword="int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(OpCodeValues.Conv_Ovf_U2_Un, 6953637);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений <see langword="unsigned int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(OpCodeValues.Conv_Ovf_U4_Un, 6953637);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений <see langword="unsigned int64" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(OpCodeValues.Conv_Ovf_U8_Un, 7084709);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений со знаком <see langword="native int" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I_Un = new OpCode(OpCodeValues.Conv_Ovf_I_Un, 6953637);
    /// <summary>
    ///   Преобразует значение без знака на вершине стека вычислений <see langword="unsigned native int" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U_Un = new OpCode(OpCodeValues.Conv_Ovf_U_Un, 6953637);
    /// <summary>
    ///   Преобразует тип значения в ссылку на объект (тип <see langword="O" />).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Box = new OpCode(OpCodeValues.Box, 7477933);
    /// <summary>
    ///   Помещает в стек вычислений ссылку на объект — новый одномерный массив с индексацией от нуля, состоящий из элементов заданного типа.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Newarr = new OpCode(OpCodeValues.Newarr, 7485101);
    /// <summary>
    ///   Помещает в стек вычислений сведения о числе элементов одномерного массива с индексацией от нуля.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldlen = new OpCode(OpCodeValues.Ldlen, 6989477);
    /// <summary>
    ///   Загружает адрес элемента массива с заданным индексом массива на вершину стека вычислений как <see langword="&amp;" /> (управляемый указатель).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelema = new OpCode(OpCodeValues.Ldelema, -261437779);
    /// <summary>
    ///   Загружает элемент типа <see langword="int8" /> заданным индексом массива на вершину стека вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I1 = new OpCode(OpCodeValues.Ldelem_I1, -261437787);
    /// <summary>
    ///   Загружает элемент типа <see langword="unsigned int8" /> заданным индексом массива на вершину стека вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_U1 = new OpCode(OpCodeValues.Ldelem_U1, -261437787);
    /// <summary>
    ///   Загружает элемент типа <see langword="int16" /> заданным индексом массива на вершину стека вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I2 = new OpCode(OpCodeValues.Ldelem_I2, -261437787);
    /// <summary>
    ///   Загружает элемент типа <see langword="unsigned int16" /> заданным индексом массива на вершину стека вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_U2 = new OpCode(OpCodeValues.Ldelem_U2, -261437787);
    /// <summary>
    ///   Загружает элемент типа <see langword="int32" /> заданным индексом массива на вершину стека вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I4 = new OpCode(OpCodeValues.Ldelem_I4, -261437787);
    /// <summary>
    ///   Загружает элемент типа <see langword="unsigned int32" /> заданным индексом массива на вершину стека вычислений как <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_U4 = new OpCode(OpCodeValues.Ldelem_U4, -261437787);
    /// <summary>
    ///   Загружает элемент типа <see langword="int64" /> заданным индексом массива на вершину стека вычислений как <see langword="int64" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I8 = new OpCode(OpCodeValues.Ldelem_I8, -261306715);
    /// <summary>
    ///   Загружает элемент типа <see langword="native int" /> заданным индексом массива на вершину стека вычислений как <see langword="native int" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I = new OpCode(OpCodeValues.Ldelem_I, -261437787);
    /// <summary>
    ///   Загружает элемент типа <see langword="float32" /> заданным индексом массива на вершину стека вычислений как <see langword="F" /> (float).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_R4 = new OpCode(OpCodeValues.Ldelem_R4, -261175643);
    /// <summary>
    ///   Загружает элемент типа <see langword="float64" /> заданным индексом массива на вершину стека вычислений как <see langword="F" /> (float).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_R8 = new OpCode(OpCodeValues.Ldelem_R8, -261044571);
    /// <summary>
    ///   Загружает элемент, содержащий ссылку на объект с заданным индексом массива на вершину стека вычислений как <see langword="O" /> (ссылка на объект).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_Ref = new OpCode(OpCodeValues.Ldelem_Ref, -260913499);
    /// <summary>
    ///   Заменяет элемент массива с заданным индексом <see langword="native int" /> значение в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I = new OpCode(OpCodeValues.Stelem_I, -798697819);
    /// <summary>
    ///   Заменяет элемент массива с заданным индексом <see langword="int8" /> значение в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I1 = new OpCode(OpCodeValues.Stelem_I1, -798697819);
    /// <summary>
    ///   Заменяет элемент массива с заданным индексом <see langword="int16" /> значение в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I2 = new OpCode(OpCodeValues.Stelem_I2, -798697819);
    /// <summary>
    ///   Заменяет элемент массива с заданным индексом <see langword="int32" /> значение в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I4 = new OpCode(OpCodeValues.Stelem_I4, -798697819);
    /// <summary>
    ///   Заменяет элемент массива с заданным индексом <see langword="int64" /> значение в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I8 = new OpCode(OpCodeValues.Stelem_I8, -798693723);
    /// <summary>
    ///   Заменяет элемент массива с заданным индексом <see langword="float32" /> значение в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_R4 = new OpCode(OpCodeValues.Stelem_R4, -798689627);
    /// <summary>
    ///   Заменяет элемент массива с заданным индексом <see langword="float64" /> значение в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_R8 = new OpCode(OpCodeValues.Stelem_R8, -798685531);
    /// <summary>
    ///   Заменяет элемент массива с заданным индексом на значение object ref (тип <see langword="O" />) в стеке вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_Ref = new OpCode(OpCodeValues.Stelem_Ref, -798681435);
    /// <summary>
    ///   Загружает элемент с заданным индексом массива на вершину стека вычислений как тип, указанный в инструкции.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem = new OpCode(OpCodeValues.Ldelem, -261699923);
    /// <summary>
    ///   Заменяет элемент массива с заданным индексом на значение в стеке вычислений, тип которого указан в инструкции.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem = new OpCode(OpCodeValues.Stelem, 6669997);
    /// <summary>
    ///   Преобразует упакованной типа, указанного в инструкции формы в распакованную.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Unbox_Any = new OpCode(OpCodeValues.Unbox_Any, 6727341);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений в значение <see langword="int8" /> со знаком, расширяет его до <see langword="int32" /> и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I1 = new OpCode(OpCodeValues.Conv_Ovf_I1, 6953637);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений <see langword="unsigned int8" /> и расширяет его до <see langword="int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U1 = new OpCode(OpCodeValues.Conv_Ovf_U1, 6953637);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений со знаком <see langword="int16" /> и расширяет его до <see langword="int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I2 = new OpCode(OpCodeValues.Conv_Ovf_I2, 6953637);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений <see langword="unsigned int16" /> и расширяет его до <see langword="int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U2 = new OpCode(OpCodeValues.Conv_Ovf_U2, 6953637);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений со знаком <see langword="int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I4 = new OpCode(OpCodeValues.Conv_Ovf_I4, 6953637);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений <see langword="unsigned int32" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U4 = new OpCode(OpCodeValues.Conv_Ovf_U4, 6953637);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений со знаком <see langword="int64" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I8 = new OpCode(OpCodeValues.Conv_Ovf_I8, 7084709);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений <see langword="unsigned int64" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U8 = new OpCode(OpCodeValues.Conv_Ovf_U8, 7084709);
    /// <summary>
    ///   Извлекает адрес (тип <see langword="&amp;" />) внедренный в ссылку с определенным типом.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Refanyval = new OpCode(OpCodeValues.Refanyval, 6953645);
    /// <summary>
    ///   Создает исключение <see cref="T:System.ArithmeticException" /> если значение не является конечным числом.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ckfinite = new OpCode(OpCodeValues.Ckfinite, 7346853);
    /// <summary>
    ///   Помещает в стек вычислений ссылку на экземпляр определенного типа.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Mkrefany = new OpCode(OpCodeValues.Mkrefany, 6699693);
    /// <summary>
    ///   Преобразует токен метаданных в его представление времени выполнения, а затем помещает в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldtoken = new OpCode(OpCodeValues.Ldtoken, 275385004);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="unsigned int16" />, и расширяет его до <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U2 = new OpCode(OpCodeValues.Conv_U2, 6953637);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="unsigned int8" />, и расширяет его до <see langword="int32" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U1 = new OpCode(OpCodeValues.Conv_U1, 6953637);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="native int" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I = new OpCode(OpCodeValues.Conv_I, 6953637);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений со знаком <see langword="native int" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I = new OpCode(OpCodeValues.Conv_Ovf_I, 6953637);
    /// <summary>
    ///   Преобразует значение со знаком на вершине стека вычислений <see langword="unsigned native int" />, и создает исключение <see cref="T:System.OverflowException" /> в случае переполнения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U = new OpCode(OpCodeValues.Conv_Ovf_U, 6953637);
    /// <summary>
    ///   Складывает два целых числа, выполняет проверку переполнения и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Add_Ovf = new OpCode(OpCodeValues.Add_Ovf, -261739867);
    /// <summary>
    ///   Складывает два целочисленных значения без знака, выполняет проверку переполнения и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Add_Ovf_Un = new OpCode(OpCodeValues.Add_Ovf_Un, -261739867);
    /// <summary>
    ///   Умножает два целочисленных значения, выполняет проверку переполнения и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Mul_Ovf = new OpCode(OpCodeValues.Mul_Ovf, -261739867);
    /// <summary>
    ///   Умножает два целочисленных значения без знака, выполняет проверку переполнения и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Mul_Ovf_Un = new OpCode(OpCodeValues.Mul_Ovf_Un, -261739867);
    /// <summary>
    ///   Вычитает одно целочисленное значение из другого, выполняет проверку переполнения и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Sub_Ovf = new OpCode(OpCodeValues.Sub_Ovf, -261739867);
    /// <summary>
    ///   Вычитает одно целочисленное значение без знака из другого, выполняет проверку переполнения и помещает результат в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Sub_Ovf_Un = new OpCode(OpCodeValues.Sub_Ovf_Un, -261739867);
    /// <summary>
    ///   Передает управление из <see langword="fault" /> или <see langword="finally" /> предложение блока исключения обратно обработчику исключений Common Language Infrastructure (CLI).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Endfinally = new OpCode(OpCodeValues.Endfinally, 23333605);
    /// <summary>
    ///   Выполняет выход из защищенной области кода с безусловной передачей управления указанной конечной инструкции.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Leave = new OpCode(OpCodeValues.Leave, 23333376);
    /// <summary>
    ///   Выполняет выход из защищенной области кода с безусловной передачей управления указанной конечной инструкции (короткая форма).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Leave_S = new OpCode(OpCodeValues.Leave_S, 23333391);
    /// <summary>
    ///   Сохраняет значение с типом <see langword="native int" /> по указанному адресу.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I = new OpCode(OpCodeValues.Stind_I, -530294107);
    /// <summary>
    ///   Преобразует значение на вершине стека вычислений в <see langword="unsigned native int" />, и расширяет его до <see langword="native int" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U = new OpCode(OpCodeValues.Conv_U, 6953637);
    /// <summary>Эта инструкция зарезервирована.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix7 = new OpCode(OpCodeValues.Prefix7, 6554757);
    /// <summary>Эта инструкция зарезервирована.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix6 = new OpCode(OpCodeValues.Prefix6, 6554757);
    /// <summary>Эта инструкция зарезервирована.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix5 = new OpCode(OpCodeValues.Prefix5, 6554757);
    /// <summary>Эта инструкция зарезервирована.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix4 = new OpCode(OpCodeValues.Prefix4, 6554757);
    /// <summary>Эта инструкция зарезервирована.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix3 = new OpCode(OpCodeValues.Prefix3, 6554757);
    /// <summary>Эта инструкция зарезервирована.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix2 = new OpCode(OpCodeValues.Prefix2, 6554757);
    /// <summary>Эта инструкция зарезервирована.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix1 = new OpCode(OpCodeValues.Prefix1, 6554757);
    /// <summary>Эта инструкция зарезервирована.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefixref = new OpCode(OpCodeValues.Prefixref, 6554757);
    /// <summary>
    ///   Возвращает неуправляемый указатель на список аргументов текущего метода.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Arglist = new OpCode(OpCodeValues.Arglist, 279579301);
    /// <summary>
    ///   Сравнивает два значения.
    ///    Если они равны, целочисленное значение 1 <see langword="(int32" />) помещается в стек вычислений; в противном случае — 0 (<see langword="int32" />) помещается в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ceq = new OpCode(OpCodeValues.Ceq, -257283419);
    /// <summary>
    ///   Сравнивает два значения.
    ///    Если первое значение больше второго, целочисленное значение 1 <see langword="(int32" />) помещается в стек вычислений; в противном случае — 0 (<see langword="int32" />) помещается в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Cgt = new OpCode(OpCodeValues.Cgt, -257283419);
    /// <summary>
    ///   Сравнивает два значения без знака или два неупорядоченных значения.
    ///    Если первое значение больше второго, целочисленное значение 1 <see langword="(int32" />) помещается в стек вычислений; в противном случае — 0 (<see langword="int32" />) помещается в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Cgt_Un = new OpCode(OpCodeValues.Cgt_Un, -257283419);
    /// <summary>
    ///   Сравнивает два значения.
    ///    Если первое значение меньше второго, целочисленное значение 1 <see langword="(int32" />) помещается в стек вычислений; в противном случае — 0 (<see langword="int32" />) помещается в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Clt = new OpCode(OpCodeValues.Clt, -257283419);
    /// <summary>
    ///   Сравнивает значения без знака или неупорядоченных <paramref name="value1" /> и <paramref name="value2" />.
    ///    Если <paramref name="value1" /> является менее <paramref name="value2" />, целочисленное значение 1 <see langword="(int32" />) помещается в вычисление стека; в противном случае — 0 (<see langword="int32" />) помещается в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Clt_Un = new OpCode(OpCodeValues.Clt_Un, -257283419);
    /// <summary>
    ///   Помещает в неуправляемый указатель (типа <see langword="native int" />) в машинный код, реализующий заданный метод в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldftn = new OpCode(OpCodeValues.Ldftn, 279579300);
    /// <summary>
    ///   Помещает в неуправляемый указатель (типа <see langword="native int" />) на машинный код, реализующий виртуальный метод, связанный с заданным объектом в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldvirtftn = new OpCode(OpCodeValues.Ldvirtftn, 11184804);
    /// <summary>
    ///   Загружает аргумент (на который ссылается указанное значение индекса) в стек.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg = new OpCode(OpCodeValues.Ldarg, 279317166);
    /// <summary>Загружает адрес аргумента в стек вычислений.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarga = new OpCode(OpCodeValues.Ldarga, 279579310);
    /// <summary>
    ///   Сохраняет значение, находящееся на вершине стека вычислений, в ячейке аргумента с заданным индексом.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Starg = new OpCode(OpCodeValues.Starg, -257680722);
    /// <summary>
    ///   Загружает в стек вычислений локальную переменную с указанным индексом.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc = new OpCode(OpCodeValues.Ldloc, 279317166);
    /// <summary>
    ///   Загружает в стек вычислений адрес локальной переменной с указанным индексом.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloca = new OpCode(OpCodeValues.Ldloca, 279579310);
    /// <summary>
    ///   Извлекает верхнее значение в стеке вычислений и сохраняет его в списке локальных переменных с заданным индексом.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc = new OpCode(OpCodeValues.Stloc, -257680722);
    /// <summary>
    ///   Выделяет определенное количество байтов из пула локальной динамической памяти и помещает в адрес (временный указатель, тип <see langword="*" />) первого выделенного байта в стек вычислений.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Localloc = new OpCode(OpCodeValues.Localloc, 11156133);
    /// <summary>
    ///   Передает управление из <see langword="filter" /> предложение исключения обратно обработчику исключений Common Language Infrastructure (CLI).
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Endfilter = new OpCode(OpCodeValues.Endfilter, -240895259);
    /// <summary>
    ///   Указывает, что адрес на вершине стека вычислений может не выровнен по естественному размеру следующей непосредственно за <see langword="ldind" />, <see langword="stind" />, <see langword="ldfld" />, <see langword="stfld" />, <see langword="ldobj" />, <see langword="stobj" />, <see langword="initblk" />, или <see langword="cpblk" /> инструкции.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Unaligned = new OpCode(OpCodeValues.Unaligned_, 10750096);
    /// <summary>
    ///   Указывает, что адрес на вершине стека вычислений, возможно, является изменяемым и результаты чтения данной области невозможно кэшировать либо невозможно запретить множественные сохранения в эту область.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Volatile = new OpCode(OpCodeValues.Volatile_, 10750085);
    /// <summary>
    ///   Выполняет инструкцию вызова метода (префиксом которой является), предварительно удаляя кадр стека текущего метода.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Tailcall = new OpCode(OpCodeValues.Tail_, 10750085);
    /// <summary>
    ///   Инициализирует каждое поле типа значения с определенным адресом пустой ссылкой или значением 0 соответствующего простого типа.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Initobj = new OpCode(OpCodeValues.Initobj, -257673555);
    /// <summary>
    ///   Ограничивает тип, для которого был вызван виртуальный метод.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Constrained = new OpCode(OpCodeValues.Constrained_, 10750093);
    /// <summary>
    ///   Копирует заданное число байт из исходного адреса в конечный.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Cpblk = new OpCode(OpCodeValues.Cpblk, -794527067);
    /// <summary>
    ///   Инициализирует блок памяти с определенным адресом, присваивая его начальному значению с заданным размером.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Initblk = new OpCode(OpCodeValues.Initblk, -794527067);
    /// <summary>Возвращает текущее исключение.</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Rethrow = new OpCode(OpCodeValues.Rethrow, 27526917);
    /// <summary>
    ///   Помещает в стек вычислений сведения о размере (в байтах) заданного типа значения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Sizeof = new OpCode(OpCodeValues.Sizeof, 279579309);
    /// <summary>
    ///   Извлекает токен типа, внедренный в ссылку с определенным типом.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Refanytype = new OpCode(OpCodeValues.Refanytype, 11147941);
    /// <summary>
    ///   Указывает, что последующая операция, связанная с адресом массива, не выполняет никаких проверок во время выполнения и возвращает управляемый указатель, изменение которого запрещено.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Readonly = new OpCode(OpCodeValues.Readonly_, 10750085);

    private OpCodes()
    {
    }

    /// <summary>
    ///   Возвращает true или false, в зависимости от того, принимает ли заданный код операции однобайтовый аргумент.
    /// </summary>
    /// <param name="inst">Экземпляр объекта Opcode.</param>
    /// <returns>
    ///   <see langword="True" /> или <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TakesSingleByteArgument(OpCode inst)
    {
      switch (inst.OperandType)
      {
        case OperandType.ShortInlineBrTarget:
        case OperandType.ShortInlineI:
        case OperandType.ShortInlineVar:
          return true;
        default:
          return false;
      }
    }
  }
}
