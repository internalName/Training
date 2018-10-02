// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.CodeConnectAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Указывает, предоставляется коду доступ к ресурсам сети.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class CodeConnectAccess
  {
    /// <summary>
    ///   Содержит значение, используемое для представления порта по умолчанию.
    /// </summary>
    public static readonly int DefaultPort = -3;
    /// <summary>
    ///   Содержит значение, используемое для представления значения порта в URI, где создан код.
    /// </summary>
    public static readonly int OriginPort = -4;
    /// <summary>
    ///   Содержит значение, используемое для представления схемы в URL-адрес, из которого получен код.
    /// </summary>
    public static readonly string OriginScheme = "$origin";
    /// <summary>
    ///   Содержит строковое значение, представляющее шаблон схемы.
    /// </summary>
    public static readonly string AnyScheme = "*";
    private string _LowerCaseScheme;
    private string _LowerCasePort;
    private int _IntPort;
    private const string DefaultStr = "$default";
    private const string OriginStr = "$origin";
    internal const int NoPort = -1;
    internal const int AnyPort = -2;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.CodeConnectAccess" />.
    /// </summary>
    /// <param name="allowScheme">
    ///   Схема URI, представленный текущим экземпляром.
    /// </param>
    /// <param name="allowPort">
    ///   Порт, представленный текущим экземпляром.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство <paramref name="allowScheme" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="allowScheme" /> является пустой строкой ("").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="allowScheme" /> содержит символы, которые не разрешены в схемах.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="allowPort" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="allowPort" /> содержит более 65 535.
    /// </exception>
    public CodeConnectAccess(string allowScheme, int allowPort)
    {
      if (!CodeConnectAccess.IsValidScheme(allowScheme))
        throw new ArgumentOutOfRangeException(nameof (allowScheme));
      this.SetCodeConnectAccess(allowScheme.ToLower(CultureInfo.InvariantCulture), allowPort);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Policy.CodeConnectAccess" /> экземпляр, который представляет доступ к указанному порту с использованием кода схемы источника.
    /// </summary>
    /// <param name="allowPort">
    ///   Порт, представленный возвращенным экземпляром.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.Security.Policy.CodeConnectAccess" /> экземпляра для указанного порта.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="allowPort" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="allowPort" /> содержит более 65 535.
    /// </exception>
    public static CodeConnectAccess CreateOriginSchemeAccess(int allowPort)
    {
      CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
      codeConnectAccess.SetCodeConnectAccess(CodeConnectAccess.OriginScheme, allowPort);
      return codeConnectAccess;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Policy.CodeConnectAccess" /> экземпляр, который представляет доступ к указанному порту с использованием любой схемы.
    /// </summary>
    /// <param name="allowPort">
    ///   Порт, представленный возвращенным экземпляром.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.Security.Policy.CodeConnectAccess" /> экземпляра для указанного порта.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="allowPort" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="allowPort" /> содержит более 65 535.
    /// </exception>
    public static CodeConnectAccess CreateAnySchemeAccess(int allowPort)
    {
      CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
      codeConnectAccess.SetCodeConnectAccess(CodeConnectAccess.AnyScheme, allowPort);
      return codeConnectAccess;
    }

    private CodeConnectAccess()
    {
    }

    private void SetCodeConnectAccess(string lowerCaseScheme, int allowPort)
    {
      this._LowerCaseScheme = lowerCaseScheme;
      if (allowPort == CodeConnectAccess.DefaultPort)
        this._LowerCasePort = "$default";
      else if (allowPort == CodeConnectAccess.OriginPort)
      {
        this._LowerCasePort = "$origin";
      }
      else
      {
        if (allowPort < 0 || allowPort > (int) ushort.MaxValue)
          throw new ArgumentOutOfRangeException(nameof (allowPort));
        this._LowerCasePort = allowPort.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
      this._IntPort = allowPort;
    }

    /// <summary>
    ///   Возвращает схему URI, представленный текущим экземпляром.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> идентифицирующий схему URI, преобразованную в нижний регистр.
    /// </returns>
    public string Scheme
    {
      get
      {
        return this._LowerCaseScheme;
      }
    }

    /// <summary>Получает порт, представленный текущим экземпляром.</summary>
    /// <returns>
    ///   A <see cref="T:System.Int32" /> значение, идентифицирующее порт компьютера, используется в сочетании с <see cref="P:System.Security.Policy.CodeConnectAccess.Scheme" /> свойство.
    /// </returns>
    public int Port
    {
      get
      {
        return this._IntPort;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, будет ли два <see cref="T:System.Security.Policy.CodeConnectAccess" /> объекты представляют одинаковые схему и порт.
    /// </summary>
    /// <param name="o">
    ///   Объект, сравниваемый с текущим <see cref="T:System.Security.Policy.CodeConnectAccess" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если два объекта представляют одинаковые схему и порт; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      if (this == o)
        return true;
      CodeConnectAccess codeConnectAccess = o as CodeConnectAccess;
      if (codeConnectAccess == null || !(this.Scheme == codeConnectAccess.Scheme))
        return false;
      return this.Port == codeConnectAccess.Port;
    }

    /// <summary>Служит хэш-функцией для определенного типа.</summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Object" />.
    /// </returns>
    public override int GetHashCode()
    {
      return this.Scheme.GetHashCode() + this.Port.GetHashCode();
    }

    internal CodeConnectAccess(string allowScheme, string allowPort)
    {
      if (allowScheme == null || allowScheme.Length == 0)
        throw new ArgumentNullException(nameof (allowScheme));
      if (allowPort == null || allowPort.Length == 0)
        throw new ArgumentNullException(nameof (allowPort));
      this._LowerCaseScheme = allowScheme.ToLower(CultureInfo.InvariantCulture);
      if (this._LowerCaseScheme == CodeConnectAccess.OriginScheme)
        this._LowerCaseScheme = CodeConnectAccess.OriginScheme;
      else if (this._LowerCaseScheme == CodeConnectAccess.AnyScheme)
        this._LowerCaseScheme = CodeConnectAccess.AnyScheme;
      else if (!CodeConnectAccess.IsValidScheme(this._LowerCaseScheme))
        throw new ArgumentOutOfRangeException(nameof (allowScheme));
      this._LowerCasePort = allowPort.ToLower(CultureInfo.InvariantCulture);
      if (this._LowerCasePort == "$default")
        this._IntPort = CodeConnectAccess.DefaultPort;
      else if (this._LowerCasePort == "$origin")
      {
        this._IntPort = CodeConnectAccess.OriginPort;
      }
      else
      {
        this._IntPort = int.Parse(allowPort, (IFormatProvider) CultureInfo.InvariantCulture);
        if (this._IntPort < 0 || this._IntPort > (int) ushort.MaxValue)
          throw new ArgumentOutOfRangeException(nameof (allowPort));
        this._LowerCasePort = this._IntPort.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    internal bool IsOriginScheme
    {
      get
      {
        return (object) this._LowerCaseScheme == (object) CodeConnectAccess.OriginScheme;
      }
    }

    internal bool IsAnyScheme
    {
      get
      {
        return (object) this._LowerCaseScheme == (object) CodeConnectAccess.AnyScheme;
      }
    }

    internal bool IsDefaultPort
    {
      get
      {
        return this.Port == CodeConnectAccess.DefaultPort;
      }
    }

    internal bool IsOriginPort
    {
      get
      {
        return this.Port == CodeConnectAccess.OriginPort;
      }
    }

    internal string StrPort
    {
      get
      {
        return this._LowerCasePort;
      }
    }

    internal static bool IsValidScheme(string scheme)
    {
      if (scheme == null || scheme.Length == 0 || !CodeConnectAccess.IsAsciiLetter(scheme[0]))
        return false;
      for (int index = scheme.Length - 1; index > 0; --index)
      {
        if (!CodeConnectAccess.IsAsciiLetterOrDigit(scheme[index]) && scheme[index] != '+' && (scheme[index] != '-' && scheme[index] != '.'))
          return false;
      }
      return true;
    }

    private static bool IsAsciiLetterOrDigit(char character)
    {
      if (CodeConnectAccess.IsAsciiLetter(character))
        return true;
      if (character >= '0')
        return character <= '9';
      return false;
    }

    private static bool IsAsciiLetter(char character)
    {
      if (character >= 'a' && character <= 'z')
        return true;
      if (character >= 'A')
        return character <= 'Z';
      return false;
    }
  }
}
