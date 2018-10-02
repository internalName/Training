// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.LocalBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Представляет локальную переменную внутри метода или конструктора.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_LocalBuilder))]
  [ComVisible(true)]
  public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
  {
    private int m_localIndex;
    private Type m_localType;
    private MethodInfo m_methodBuilder;
    private bool m_isPinned;

    private LocalBuilder()
    {
    }

    internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder)
      : this(localIndex, localType, methodBuilder, false)
    {
    }

    internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder, bool isPinned)
    {
      this.m_isPinned = isPinned;
      this.m_localIndex = localIndex;
      this.m_localType = localType;
      this.m_methodBuilder = methodBuilder;
    }

    internal int GetLocalIndex()
    {
      return this.m_localIndex;
    }

    internal MethodInfo GetMethodBuilder()
    {
      return this.m_methodBuilder;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, закреплен ли в памяти объект, на который ссылается на локальную переменную.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если ссылка на объект локальная переменная, закреплен в памяти; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool IsPinned
    {
      get
      {
        return this.m_isPinned;
      }
    }

    /// <summary>Возвращает тип локальной переменной.</summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Локальной переменной.
    /// </returns>
    public override Type LocalType
    {
      get
      {
        return this.m_localType;
      }
    }

    /// <summary>
    ///   Возвращает отсчитываемый от нуля индекс локальной переменной внутри тела метода.
    /// </summary>
    /// <returns>
    ///   Целочисленное значение, представляющее порядок объявления локальной переменной внутри тела метода.
    /// </returns>
    public override int LocalIndex
    {
      get
      {
        return this.m_localIndex;
      }
    }

    /// <summary>Задает имя данной локальной переменной.</summary>
    /// <param name="name">Имя локальной переменной.</param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Нет не определен интерфейс записи символьной для вмещающего модуля.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Эти локальные данные определены в динамическом методе, а не в методе динамического типа.
    /// </exception>
    public void SetLocalSymInfo(string name)
    {
      this.SetLocalSymInfo(name, 0, 0);
    }

    /// <summary>
    ///   Задает имя и лексическую область видимости локальной переменной.
    /// </summary>
    /// <param name="name">Имя локальной переменной.</param>
    /// <param name="startOffset">
    ///   Смещение начала лексической области видимости локальной переменной.
    /// </param>
    /// <param name="endOffset">
    ///   Конечное смещение лексической области видимости локальной переменной.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Нет не определен интерфейс записи символьной для вмещающего модуля.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Эти локальные данные определены в динамическом методе, а не в методе динамического типа.
    /// </exception>
    public void SetLocalSymInfo(string name, int startOffset, int endOffset)
    {
      MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
      if ((MethodInfo) methodBuilder == (MethodInfo) null)
        throw new NotSupportedException();
      ModuleBuilder module = (ModuleBuilder) methodBuilder.Module;
      if (methodBuilder.IsTypeCreated())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
      if (module.GetSymWriter() == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper((Module) module);
      fieldSigHelper.AddArgument(this.m_localType);
      int length;
      byte[] signature1 = fieldSigHelper.InternalGetSignature(out length);
      byte[] signature2 = new byte[length - 1];
      Array.Copy((Array) signature1, 1, (Array) signature2, 0, length - 1);
      if (methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex() == -1)
        methodBuilder.m_localSymInfo.AddLocalSymInfo(name, signature2, this.m_localIndex, startOffset, endOffset);
      else
        methodBuilder.GetILGenerator().m_ScopeTree.AddLocalSymInfoToCurrentScope(name, signature2, this.m_localIndex, startOffset, endOffset);
    }

    void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
