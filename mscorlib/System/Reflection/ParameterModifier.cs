// Decompiled with JetBrains decompiler
// Type: System.Reflection.ParameterModifier
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Присоединяет модификатор к параметрам, позволяя привязке работать с подписями параметров с измененными типами.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct ParameterModifier
  {
    private bool[] _byRef;

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.Reflection.ParameterModifier" />, которая представляет указанное число параметров.
    /// </summary>
    /// <param name="parameterCount">Число параметров.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="parameterCount" /> является отрицательным значением.
    /// </exception>
    public ParameterModifier(int parameterCount)
    {
      if (parameterCount <= 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_ParmArraySize"));
      this._byRef = new bool[parameterCount];
    }

    internal bool[] IsByRefArray
    {
      get
      {
        return this._byRef;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, которое указывает, будет ли параметр в указанном положении индекса изменен текущим элементом <see cref="T:System.Reflection.ParameterModifier" />.
    /// </summary>
    /// <param name="index">
    ///   Позиция индекса параметра, состояние изменения которого проверяется или задается.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если параметр в этой позиции индекса будет изменен этим элементом <see cref="T:System.Reflection.ParameterModifier" />. В противном случае — <see langword="false" />.
    /// </returns>
    public bool this[int index]
    {
      get
      {
        return this._byRef[index];
      }
      set
      {
        this._byRef[index] = value;
      }
    }
  }
}
