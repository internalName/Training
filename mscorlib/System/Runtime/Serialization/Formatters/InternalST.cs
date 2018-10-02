// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.InternalST
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>
  ///   Сообщения трассировки журналов при компиляции инфраструктуры сериализации платформы .NET Framework.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class InternalST
  {
    private InternalST()
    {
    }

    /// <summary>Печатает сообщения трассировки SOAP.</summary>
    /// <param name="messages">
    ///   Массив сообщений трассировки для печати.
    /// </param>
    [Conditional("_LOGGING")]
    public static void InfoSoap(params object[] messages)
    {
    }

    /// <summary>Проверяет, включена ли трассировка SOAP.</summary>
    /// <returns>
    ///   <see langword="true" />, если трассировка включена; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool SoapCheckEnabled()
    {
      return BCLDebug.CheckEnabled("Soap");
    }

    /// <summary>Обрабатывает указанный массив сообщений.</summary>
    /// <param name="messages">Массив сообщений для обработки.</param>
    [Conditional("SER_LOGGING")]
    public static void Soap(params object[] messages)
    {
      if (!(messages[0] is string))
        messages[0] = (object) (messages[0].GetType().Name + " ");
      else
        messages[0] = (object) (messages[0].ToString() + " ");
    }

    /// <summary>Утверждает указанное сообщение.</summary>
    /// <param name="condition">
    ///   Логическое значение, используемое при утверждении.
    /// </param>
    /// <param name="message">
    ///   Сообщение, используемое при утверждении.
    /// </param>
    [Conditional("_DEBUG")]
    public static void SoapAssert(bool condition, string message)
    {
    }

    /// <summary>Задает значение поля.</summary>
    /// <param name="fi">
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> с данными о целевого поля.
    /// </param>
    /// <param name="target">Поле для изменения.</param>
    /// <param name="value">Задаваемое значение.</param>
    public static void SerializationSetValue(FieldInfo fi, object target, object value)
    {
      if (fi == (FieldInfo) null)
        throw new ArgumentNullException(nameof (fi));
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      FormatterServices.SerializationSetValue((MemberInfo) fi, target, value);
    }

    /// <summary>Загружает указанную сборку для отладки.</summary>
    /// <param name="assemblyString">Имя загружаемой сборки.</param>
    /// <returns>
    ///   <see cref="T:System.Reflection.Assembly" /> Для отладки.
    /// </returns>
    public static Assembly LoadAssemblyFromString(string assemblyString)
    {
      return FormatterServices.LoadAssemblyFromString(assemblyString);
    }
  }
}
