// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RuntimeWrappedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Создает оболочку для исключения, который является производным от <see cref="T:System.Exception" /> класса.
  ///    Этот класс не наследуется.
  /// </summary>
  [Serializable]
  public sealed class RuntimeWrappedException : Exception
  {
    private object m_wrappedException;

    private RuntimeWrappedException(object thrownObject)
      : base(Environment.GetResourceString(nameof (RuntimeWrappedException)))
    {
      this.SetErrorCode(-2146233026);
      this.m_wrappedException = thrownObject;
    }

    /// <summary>
    ///   Возвращает объект, который был оболочкой <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект, который был оболочкой <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> объекта.
    /// </returns>
    public object WrappedException
    {
      get
      {
        return this.m_wrappedException;
      }
    }

    /// <summary>
    ///   Задает объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий информацию об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта по возникающему исключению.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      info.AddValue("WrappedException", this.m_wrappedException, typeof (object));
    }

    internal RuntimeWrappedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.m_wrappedException = info.GetValue(nameof (WrappedException), typeof (object));
    }
  }
}
