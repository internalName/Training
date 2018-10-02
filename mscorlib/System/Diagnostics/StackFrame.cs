// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.StackFrame
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
  /// <summary>
  ///   Предоставляет сведения об объекте <see cref="T:System.Diagnostics.StackFrame" />, который представляет вызов функции в стеке вызовов для текущего потока.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public class StackFrame
  {
    private MethodBase method;
    private int offset;
    private int ILOffset;
    private string strFileName;
    private int iLineNumber;
    private int iColumnNumber;
    [OptionalField]
    private bool fIsLastFrameFromForeignExceptionStackTrace;
    /// <summary>
    ///   Определяет значение, возвращаемое методом <see cref="M:System.Diagnostics.StackFrame.GetNativeOffset" />, или <see cref="M:System.Diagnostics.StackFrame.GetILOffset" />, если смещение в исходном коде или коде на языке MSIL неизвестно.
    ///    Это поле является константой.
    /// </summary>
    public const int OFFSET_UNKNOWN = -1;

    internal void InitMembers()
    {
      this.method = (MethodBase) null;
      this.offset = -1;
      this.ILOffset = -1;
      this.strFileName = (string) null;
      this.iLineNumber = 0;
      this.iColumnNumber = 0;
      this.fIsLastFrameFromForeignExceptionStackTrace = false;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.StackFrame" />.
    /// </summary>
    public StackFrame()
    {
      this.InitMembers();
      this.BuildStackFrame(0, false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.StackFrame" /> и дополнительно может собирать сведения об источнике.
    /// </summary>
    /// <param name="fNeedFileInfo">
    ///   Значение <see langword="true" />, чтобы зафиксировать имя файла, номер строки и номер столбца кадра стека; в противном случае — значение <see langword="false" />.
    /// </param>
    public StackFrame(bool fNeedFileInfo)
    {
      this.InitMembers();
      this.BuildStackFrame(0, fNeedFileInfo);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.StackFrame" />, соответствующий кадру, который расположен над текущим кадром стека.
    /// </summary>
    /// <param name="skipFrames">
    ///   Количество пропускаемых кадров вверх по стеку.
    /// </param>
    public StackFrame(int skipFrames)
    {
      this.InitMembers();
      this.BuildStackFrame(skipFrames + 0, false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.StackFrame" />, соответствующий кадру, который расположен над текущим кадром стека и дополнительно может собирать сведения об источнике.
    /// </summary>
    /// <param name="skipFrames">
    ///   Количество пропускаемых кадров вверх по стеку.
    /// </param>
    /// <param name="fNeedFileInfo">
    ///   Значение <see langword="true" />, чтобы зафиксировать имя файла, номер строки и номер столбца кадра стека; в противном случае — значение <see langword="false" />.
    /// </param>
    public StackFrame(int skipFrames, bool fNeedFileInfo)
    {
      this.InitMembers();
      this.BuildStackFrame(skipFrames + 0, fNeedFileInfo);
    }

    internal StackFrame(bool DummyFlag1, bool DummyFlag2)
    {
      this.InitMembers();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.StackFrame" />, содержащий только заданное имя файла и номер строки.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="lineNumber">Номер строки в указанном файле.</param>
    public StackFrame(string fileName, int lineNumber)
    {
      this.InitMembers();
      this.BuildStackFrame(0, false);
      this.strFileName = fileName;
      this.iLineNumber = lineNumber;
      this.iColumnNumber = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.StackFrame" />, содержащий только заданное имя файла, номер строки и номер столбца.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="lineNumber">Номер строки в указанном файле.</param>
    /// <param name="colNumber">Номер столбца в указанном файле.</param>
    public StackFrame(string fileName, int lineNumber, int colNumber)
    {
      this.InitMembers();
      this.BuildStackFrame(0, false);
      this.strFileName = fileName;
      this.iLineNumber = lineNumber;
      this.iColumnNumber = colNumber;
    }

    internal virtual void SetMethodBase(MethodBase mb)
    {
      this.method = mb;
    }

    internal virtual void SetOffset(int iOffset)
    {
      this.offset = iOffset;
    }

    internal virtual void SetILOffset(int iOffset)
    {
      this.ILOffset = iOffset;
    }

    internal virtual void SetFileName(string strFName)
    {
      this.strFileName = strFName;
    }

    internal virtual void SetLineNumber(int iLine)
    {
      this.iLineNumber = iLine;
    }

    internal virtual void SetColumnNumber(int iCol)
    {
      this.iColumnNumber = iCol;
    }

    internal virtual void SetIsLastFrameFromForeignExceptionStackTrace(bool fIsLastFrame)
    {
      this.fIsLastFrameFromForeignExceptionStackTrace = fIsLastFrame;
    }

    internal virtual bool GetIsLastFrameFromForeignExceptionStackTrace()
    {
      return this.fIsLastFrameFromForeignExceptionStackTrace;
    }

    /// <summary>Получает метод, в котором выполняется кадр.</summary>
    /// <returns>Метод, в котором выполняется кадр.</returns>
    public virtual MethodBase GetMethod()
    {
      return this.method;
    }

    /// <summary>
    ///   Получает смещение от начала исходного, скомпилированного JIT-компилятором кода для исполняемого метода.
    ///    Управление созданием этих сведений отладки осуществляется классом <see cref="T:System.Diagnostics.DebuggableAttribute" />.
    /// </summary>
    /// <returns>
    ///   Смещение от начала кода, скомпилированного JIT-компилятором для выполняемого метода.
    /// </returns>
    public virtual int GetNativeOffset()
    {
      return this.offset;
    }

    /// <summary>
    ///   Получает смещение от начала кода на языке MSIL для выполняемого метода.
    ///    Это смещение может быть приблизительным, в зависимости от того, создает ли JIT-компилятор код отладки.
    ///    Управление созданием этих сведений отладки осуществляется классом <see cref="T:System.Diagnostics.DebuggableAttribute" />.
    /// </summary>
    /// <returns>
    ///   Смещение от начала кода на языке MSIL для выполняемого метода.
    /// </returns>
    public virtual int GetILOffset()
    {
      return this.ILOffset;
    }

    /// <summary>
    ///   Получает имя файла, содержащего выполняемый код.
    ///    Эти сведения обычно извлекаются из символов отладки для исполняемого файла.
    /// </summary>
    /// <returns>
    ///   Имя файла или <see langword="null" />, если имя файла определить не удалось.
    /// </returns>
    [SecuritySafeCritical]
    public virtual string GetFileName()
    {
      if (this.strFileName != null)
        new FileIOPermission(PermissionState.None)
        {
          AllFiles = FileIOPermissionAccess.PathDiscovery
        }.Demand();
      return this.strFileName;
    }

    /// <summary>
    ///   Получает номер строки в файле, содержащем выполняемый код.
    ///    Эти сведения обычно извлекаются из символов отладки для исполняемого файла.
    /// </summary>
    /// <returns>
    ///   Номер строки в файле или 0 (нуль), если номер строки в файле определить не удалось.
    /// </returns>
    public virtual int GetFileLineNumber()
    {
      return this.iLineNumber;
    }

    /// <summary>
    ///   Получает номер столбца в файле, содержащем выполняемый код.
    ///    Эти сведения обычно извлекаются из символов отладки для исполняемого файла.
    /// </summary>
    /// <returns>
    ///   Номер столбца в файле или 0 (нуль), если номер столбца в файле определить не удалось.
    /// </returns>
    public virtual int GetFileColumnNumber()
    {
      return this.iColumnNumber;
    }

    /// <summary>
    ///   Создает доступное для чтения представление трассировки стека.
    /// </summary>
    /// <returns>
    ///   Доступное для чтения представление трассировки стека.
    /// </returns>
    [SecuritySafeCritical]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder((int) byte.MaxValue);
      if (this.method != (MethodBase) null)
      {
        stringBuilder.Append(this.method.Name);
        if ((object) (this.method as MethodInfo) != null && this.method.IsGenericMethod)
        {
          Type[] genericArguments = this.method.GetGenericArguments();
          stringBuilder.Append("<");
          int index = 0;
          bool flag = true;
          for (; index < genericArguments.Length; ++index)
          {
            if (!flag)
              stringBuilder.Append(",");
            else
              flag = false;
            stringBuilder.Append(genericArguments[index].Name);
          }
          stringBuilder.Append(">");
        }
        stringBuilder.Append(" at offset ");
        if (this.offset == -1)
          stringBuilder.Append("<offset unknown>");
        else
          stringBuilder.Append(this.offset);
        stringBuilder.Append(" in file:line:column ");
        bool flag1 = this.strFileName != null;
        if (flag1)
        {
          try
          {
            new FileIOPermission(PermissionState.None)
            {
              AllFiles = FileIOPermissionAccess.PathDiscovery
            }.Demand();
          }
          catch (SecurityException ex)
          {
            flag1 = false;
          }
        }
        if (!flag1)
          stringBuilder.Append("<filename unknown>");
        else
          stringBuilder.Append(this.strFileName);
        stringBuilder.Append(":");
        stringBuilder.Append(this.iLineNumber);
        stringBuilder.Append(":");
        stringBuilder.Append(this.iColumnNumber);
      }
      else
        stringBuilder.Append("<null>");
      stringBuilder.Append(Environment.NewLine);
      return stringBuilder.ToString();
    }

    private void BuildStackFrame(int skipFrames, bool fNeedFileInfo)
    {
      using (StackFrameHelper StackF = new StackFrameHelper((Thread) null))
      {
        StackF.InitializeSourceInfo(0, fNeedFileInfo, (Exception) null);
        int numberOfFrames = StackF.GetNumberOfFrames();
        skipFrames += StackTrace.CalculateFramesToSkip(StackF, numberOfFrames);
        if (numberOfFrames - skipFrames <= 0)
          return;
        this.method = StackF.GetMethodBase(skipFrames);
        this.offset = StackF.GetOffset(skipFrames);
        this.ILOffset = StackF.GetILOffset(skipFrames);
        if (!fNeedFileInfo)
          return;
        this.strFileName = StackF.GetFilename(skipFrames);
        this.iLineNumber = StackF.GetLineNumber(skipFrames);
        this.iColumnNumber = StackF.GetColumnNumber(skipFrames);
      }
    }
  }
}
