// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.StackTrace
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
  /// <summary>
  ///   Представляет трассировку стека — упорядоченный набор из одного или нескольких кадров стека.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public class StackTrace
  {
    private StackFrame[] frames;
    private int m_iNumOfFrames;
    /// <summary>
    ///   Определяет значение по умолчанию для числа методов, чтобы исключить из трассировки стека.
    ///    Это поле является константой.
    /// </summary>
    public const int METHODS_TO_SKIP = 0;
    private int m_iMethodsToSkip;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> класс из кадра вызывающего.
    /// </summary>
    public StackTrace()
    {
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, false, (Thread) null, (Exception) null);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> класс из кадра вызывающего, дополнительно может собирать сведения об источнике.
    /// </summary>
    /// <param name="fNeedFileInfo">
    ///   <see langword="true" /> Чтобы записать имя файла, номер строки и номер столбца; в противном случае — <see langword="false" />.
    /// </param>
    public StackTrace(bool fNeedFileInfo)
    {
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, fNeedFileInfo, (Thread) null, (Exception) null);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> класс из кадра вызывающего, пропуская указанное число кадров.
    /// </summary>
    /// <param name="skipFrames">
    ///   Количество пропускаемых кадров вверх по стеку, с которого начинается трассировка.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="skipFrames" /> Параметр имеет отрицательное значение.
    /// </exception>
    public StackTrace(int skipFrames)
    {
      if (skipFrames < 0)
        throw new ArgumentOutOfRangeException(nameof (skipFrames), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(skipFrames + 0, false, (Thread) null, (Exception) null);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> класс из кадра вызывающего, пропуская заданное число фрагментов и Дополнительно может собирать сведения об источнике.
    /// </summary>
    /// <param name="skipFrames">
    ///   Количество пропускаемых кадров вверх по стеку, с которого начинается трассировка.
    /// </param>
    /// <param name="fNeedFileInfo">
    ///   <see langword="true" /> Чтобы записать имя файла, номер строки и номер столбца; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="skipFrames" /> Параметр имеет отрицательное значение.
    /// </exception>
    public StackTrace(int skipFrames, bool fNeedFileInfo)
    {
      if (skipFrames < 0)
        throw new ArgumentOutOfRangeException(nameof (skipFrames), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(skipFrames + 0, fNeedFileInfo, (Thread) null, (Exception) null);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> с использованием предоставленного объекта исключения.
    /// </summary>
    /// <param name="e">
    ///   Объект исключения, из которого создается трассировка стека.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> является <see langword="null" />.
    /// </exception>
    public StackTrace(Exception e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, false, (Thread) null, e);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> класса с использованием предоставленного объекта исключения и Дополнительно может собирать сведения об источнике.
    /// </summary>
    /// <param name="e">
    ///   Объект исключения, из которого создается трассировка стека.
    /// </param>
    /// <param name="fNeedFileInfo">
    ///   <see langword="true" /> Чтобы записать имя файла, номер строки и номер столбца; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> является <see langword="null" />.
    /// </exception>
    public StackTrace(Exception e, bool fNeedFileInfo)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, fNeedFileInfo, (Thread) null, e);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> класса с использованием предоставленного объекта исключения, пропуская указанное число кадров.
    /// </summary>
    /// <param name="e">
    ///   Объект исключения, из которого создается трассировка стека.
    /// </param>
    /// <param name="skipFrames">
    ///   Количество пропускаемых кадров вверх по стеку, с которого начинается трассировка.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> является <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="skipFrames" /> Параметр имеет отрицательное значение.
    /// </exception>
    public StackTrace(Exception e, int skipFrames)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (skipFrames < 0)
        throw new ArgumentOutOfRangeException(nameof (skipFrames), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(skipFrames + 0, false, (Thread) null, e);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> класса с использованием предоставленного объекта исключения, пропуская заданное число фрагментов и Дополнительно может собирать сведения об источнике.
    /// </summary>
    /// <param name="e">
    ///   Объект исключения, из которого создается трассировка стека.
    /// </param>
    /// <param name="skipFrames">
    ///   Количество пропускаемых кадров вверх по стеку, с которого начинается трассировка.
    /// </param>
    /// <param name="fNeedFileInfo">
    ///   <see langword="true" /> Чтобы записать имя файла, номер строки и номер столбца; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> является <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="skipFrames" /> Параметр имеет отрицательное значение.
    /// </exception>
    public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (skipFrames < 0)
        throw new ArgumentOutOfRangeException(nameof (skipFrames), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(skipFrames + 0, fNeedFileInfo, (Thread) null, e);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> класс, содержащий один кадр.
    /// </summary>
    /// <param name="frame">
    ///   Кадр, <see cref="T:System.Diagnostics.StackTrace" /> должен содержаться в объекте.
    /// </param>
    public StackTrace(StackFrame frame)
    {
      this.frames = new StackFrame[1];
      this.frames[0] = frame;
      this.m_iMethodsToSkip = 0;
      this.m_iNumOfFrames = 1;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.StackTrace" /> класс для определенного потока и Дополнительно может собирать сведения об источнике.
    /// 
    ///   Не используйте эту перегрузку конструктора.
    /// </summary>
    /// <param name="targetThread">
    ///   Поток, который запрашивается, трассировка стека.
    /// </param>
    /// <param name="needFileInfo">
    ///   <see langword="true" /> Чтобы записать имя файла, номер строки и номер столбца; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток <paramref name="targetThread" /> не приостанавливается.
    /// </exception>
    [Obsolete("This constructor has been deprecated.  Please use a constructor that does not require a Thread parameter.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public StackTrace(Thread targetThread, bool needFileInfo)
    {
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, needFileInfo, targetThread, (Exception) null);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void GetStackFramesInternal(StackFrameHelper sfh, int iSkip, bool fNeedFileInfo, Exception e);

    internal static int CalculateFramesToSkip(StackFrameHelper StackF, int iNumFrames)
    {
      int num = 0;
      string strB = "System.Diagnostics";
      for (int i = 0; i < iNumFrames; ++i)
      {
        MethodBase methodBase = StackF.GetMethodBase(i);
        if (methodBase != (MethodBase) null)
        {
          Type declaringType = methodBase.DeclaringType;
          if (!(declaringType == (Type) null))
          {
            string strA = declaringType.Namespace;
            if (strA == null || string.Compare(strA, strB, StringComparison.Ordinal) != 0)
              break;
          }
          else
            break;
        }
        ++num;
      }
      return num;
    }

    private void CaptureStackTrace(int iSkip, bool fNeedFileInfo, Thread targetThread, Exception e)
    {
      this.m_iMethodsToSkip += iSkip;
      using (StackFrameHelper StackF = new StackFrameHelper(targetThread))
      {
        StackF.InitializeSourceInfo(0, fNeedFileInfo, e);
        this.m_iNumOfFrames = StackF.GetNumberOfFrames();
        if (this.m_iMethodsToSkip > this.m_iNumOfFrames)
          this.m_iMethodsToSkip = this.m_iNumOfFrames;
        if (this.m_iNumOfFrames != 0)
        {
          this.frames = new StackFrame[this.m_iNumOfFrames];
          for (int i = 0; i < this.m_iNumOfFrames; ++i)
          {
            StackFrame stackFrame = new StackFrame(true, true);
            stackFrame.SetMethodBase(StackF.GetMethodBase(i));
            stackFrame.SetOffset(StackF.GetOffset(i));
            stackFrame.SetILOffset(StackF.GetILOffset(i));
            stackFrame.SetIsLastFrameFromForeignExceptionStackTrace(StackF.IsLastFrameFromForeignExceptionStackTrace(i));
            if (fNeedFileInfo)
            {
              stackFrame.SetFileName(StackF.GetFilename(i));
              stackFrame.SetLineNumber(StackF.GetLineNumber(i));
              stackFrame.SetColumnNumber(StackF.GetColumnNumber(i));
            }
            this.frames[i] = stackFrame;
          }
          if (e == null)
            this.m_iMethodsToSkip += StackTrace.CalculateFramesToSkip(StackF, this.m_iNumOfFrames);
          this.m_iNumOfFrames -= this.m_iMethodsToSkip;
          if (this.m_iNumOfFrames >= 0)
            return;
          this.m_iNumOfFrames = 0;
        }
        else
          this.frames = (StackFrame[]) null;
      }
    }

    /// <summary>Возвращает число кадров в трассировке стека.</summary>
    /// <returns>Число кадров в трассировке стека.</returns>
    public virtual int FrameCount
    {
      get
      {
        return this.m_iNumOfFrames;
      }
    }

    /// <summary>Возвращает указанный кадр стека.</summary>
    /// <param name="index">Индекс запрашиваемого кадра стека.</param>
    /// <returns>Указанный кадр стека.</returns>
    public virtual StackFrame GetFrame(int index)
    {
      if (this.frames != null && index < this.m_iNumOfFrames && index >= 0)
        return this.frames[index + this.m_iMethodsToSkip];
      return (StackFrame) null;
    }

    /// <summary>
    ///   Возвращает копию всех кадров стека в текущей трассировке стека.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Diagnostics.StackFrame" /> представляет вызов функции в трассировке стека.
    /// </returns>
    [ComVisible(false)]
    public virtual StackFrame[] GetFrames()
    {
      if (this.frames == null || this.m_iNumOfFrames <= 0)
        return (StackFrame[]) null;
      StackFrame[] stackFrameArray = new StackFrame[this.m_iNumOfFrames];
      Array.Copy((Array) this.frames, this.m_iMethodsToSkip, (Array) stackFrameArray, 0, this.m_iNumOfFrames);
      return stackFrameArray;
    }

    /// <summary>
    ///   Создает доступное для чтения представление трассировки стека.
    /// </summary>
    /// <returns>
    ///   Доступное для чтения представление трассировки стека.
    /// </returns>
    public override string ToString()
    {
      return this.ToString(StackTrace.TraceFormat.TrailingNewLine);
    }

    internal string ToString(StackTrace.TraceFormat traceFormat)
    {
      bool flag1 = true;
      string str1 = "at";
      string format = "in {0}:line {1}";
      if (traceFormat != StackTrace.TraceFormat.NoResourceLookup)
      {
        str1 = Environment.GetResourceString("Word_At");
        format = Environment.GetResourceString("StackTrace_InFileLineNumber");
      }
      bool flag2 = true;
      StringBuilder stringBuilder = new StringBuilder((int) byte.MaxValue);
      for (int index1 = 0; index1 < this.m_iNumOfFrames; ++index1)
      {
        StackFrame frame = this.GetFrame(index1);
        MethodBase method = frame.GetMethod();
        if (method != (MethodBase) null)
        {
          if (flag2)
            flag2 = false;
          else
            stringBuilder.Append(Environment.NewLine);
          stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "   {0} ", (object) str1);
          Type declaringType = method.DeclaringType;
          if (declaringType != (Type) null)
          {
            stringBuilder.Append(declaringType.FullName.Replace('+', '.'));
            stringBuilder.Append(".");
          }
          stringBuilder.Append(method.Name);
          if ((object) (method as MethodInfo) != null && method.IsGenericMethod)
          {
            Type[] genericArguments = method.GetGenericArguments();
            stringBuilder.Append("[");
            int index2 = 0;
            bool flag3 = true;
            for (; index2 < genericArguments.Length; ++index2)
            {
              if (!flag3)
                stringBuilder.Append(",");
              else
                flag3 = false;
              stringBuilder.Append(genericArguments[index2].Name);
            }
            stringBuilder.Append("]");
          }
          stringBuilder.Append("(");
          ParameterInfo[] parameters = method.GetParameters();
          bool flag4 = true;
          for (int index2 = 0; index2 < parameters.Length; ++index2)
          {
            if (!flag4)
              stringBuilder.Append(", ");
            else
              flag4 = false;
            string str2 = "<UnknownType>";
            if (parameters[index2].ParameterType != (Type) null)
              str2 = parameters[index2].ParameterType.Name;
            stringBuilder.Append(str2 + " " + parameters[index2].Name);
          }
          stringBuilder.Append(")");
          if (flag1 && frame.GetILOffset() != -1)
          {
            string str2 = (string) null;
            try
            {
              str2 = frame.GetFileName();
            }
            catch (NotSupportedException ex)
            {
              flag1 = false;
            }
            catch (SecurityException ex)
            {
              flag1 = false;
            }
            if (str2 != null)
            {
              stringBuilder.Append(' ');
              stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, format, (object) str2, (object) frame.GetFileLineNumber());
            }
          }
          if (frame.GetIsLastFrameFromForeignExceptionStackTrace())
          {
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.GetResourceString("Exception_EndStackTraceFromPreviousThrow"));
          }
        }
      }
      if (traceFormat == StackTrace.TraceFormat.TrailingNewLine)
        stringBuilder.Append(Environment.NewLine);
      return stringBuilder.ToString();
    }

    private static string GetManagedStackTraceStringHelper(bool fNeedFileInfo)
    {
      return new StackTrace(0, fNeedFileInfo).ToString();
    }

    internal enum TraceFormat
    {
      Normal,
      TrailingNewLine,
      NoResourceLookup,
    }
  }
}
