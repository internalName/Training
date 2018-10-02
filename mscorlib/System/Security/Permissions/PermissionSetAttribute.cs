// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PermissionSetAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Util;
using System.Text;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает выполнять действия по безопасности для <see cref="T:System.Security.PermissionSet" /> для применения в коде с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class PermissionSetAttribute : CodeAccessSecurityAttribute
  {
    private string m_file;
    private string m_name;
    private bool m_unicode;
    private string m_xml;
    private string m_hex;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Permissions.PermissionSetAttribute" /> класса с заданными параметрами безопасности действием.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений перечисления, указывающее действие по обеспечению безопасности.
    /// </param>
    public PermissionSetAttribute(SecurityAction action)
      : base(action)
    {
      this.m_unicode = false;
    }

    /// <summary>
    ///   Возвращает или задает файл, содержащий XML-представление набора пользовательских разрешений необходимо объявлять.
    /// </summary>
    /// <returns>
    ///   Задать физический путь к файлу, содержащему XML-представление разрешения.
    /// </returns>
    public string File
    {
      get
      {
        return this.m_file;
      }
      set
      {
        this.m_file = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, является ли заданный файл <see cref="P:System.Security.Permissions.PermissionSetAttribute.File" /> в стандарте Юникода или в кодировке ASCII.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если файл представлен в Юникоде; в противном случае — <see langword="false" />.
    /// </returns>
    public bool UnicodeEncoded
    {
      get
      {
        return this.m_unicode;
      }
      set
      {
        this.m_unicode = value;
      }
    }

    /// <summary>Возвращает или задает имя набора разрешений.</summary>
    /// <returns>
    ///   Имя доступного только для чтения <see cref="T:System.Security.NamedPermissionSet" /> (один или несколько наборов разрешений, которые содержатся в политике по умолчанию и не могут быть изменены).
    /// </returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        this.m_name = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает XML-представление набора разрешений.
    /// </summary>
    /// <returns>XML-представление набора разрешений.</returns>
    public string XML
    {
      get
      {
        return this.m_xml;
      }
      set
      {
        this.m_xml = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает шестнадцатеричное представление набора разрешений в кодировке XML.
    /// </summary>
    /// <returns>
    ///   Шестнадцатеричное представление XML-документа в кодировке набор разрешений.
    /// </returns>
    public string Hex
    {
      get
      {
        return this.m_hex;
      }
      set
      {
        this.m_hex = value;
      }
    }

    /// <summary>Этот метод не используется.</summary>
    /// <returns>
    ///   Пустая ссылка (<see langword="nothing" /> в Visual Basic) во всех случаях.
    /// </returns>
    public override IPermission CreatePermission()
    {
      return (IPermission) null;
    }

    private PermissionSet BruteForceParseStream(Stream stream)
    {
      Encoding[] encodingArray = new Encoding[3]
      {
        Encoding.UTF8,
        Encoding.ASCII,
        Encoding.Unicode
      };
      StreamReader input = (StreamReader) null;
      Exception exception = (Exception) null;
      int index = 0;
      while (input == null)
      {
        if (index < encodingArray.Length)
        {
          try
          {
            stream.Position = 0L;
            input = new StreamReader(stream, encodingArray[index]);
            return this.ParsePermissionSet(new Parser(input));
          }
          catch (Exception ex)
          {
            if (exception == null)
              exception = ex;
          }
          ++index;
        }
        else
          break;
      }
      throw exception;
    }

    private PermissionSet ParsePermissionSet(Parser parser)
    {
      SecurityElement topElement = parser.GetTopElement();
      PermissionSet permissionSet = new PermissionSet(PermissionState.None);
      permissionSet.FromXml(topElement);
      return permissionSet;
    }

    /// <summary>
    ///   Создает и возвращает новый набор разрешений на основе этого объекта атрибута набор разрешений.
    /// </summary>
    /// <returns>Новый набор разрешений.</returns>
    [SecuritySafeCritical]
    public PermissionSet CreatePermissionSet()
    {
      if (this.m_unrestricted)
        return new PermissionSet(PermissionState.Unrestricted);
      if (this.m_name != null)
        return PolicyLevel.GetBuiltInSet(this.m_name);
      if (this.m_xml != null)
        return this.ParsePermissionSet(new Parser(this.m_xml.ToCharArray()));
      if (this.m_hex != null)
        return this.BruteForceParseStream((Stream) new MemoryStream(System.Security.Util.Hex.DecodeHexString(this.m_hex)));
      if (this.m_file != null)
        return this.BruteForceParseStream((Stream) new FileStream(this.m_file, FileMode.Open, FileAccess.Read));
      return new PermissionSet(PermissionState.None);
    }
  }
}
