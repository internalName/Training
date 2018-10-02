// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Маркер, который возвращается при добавлении обработчика событий Среда выполнения Windows события.
  ///    Этот маркер используется для последующего удаления обработчика событий из события.
  /// </summary>
  [__DynamicallyInvokable]
  public struct EventRegistrationToken
  {
    internal ulong m_value;

    internal EventRegistrationToken(ulong value)
    {
      this.m_value = value;
    }

    internal ulong Value
    {
      get
      {
        return this.m_value;
      }
    }

    /// <summary>
    ///   Указывает, равны ли два экземпляра <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" />.
    /// </summary>
    /// <param name="left">Первый экземпляр для сравнения.</param>
    /// <param name="right">Второй экземпляр для сравнения.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если эти два объекта равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(EventRegistrationToken left, EventRegistrationToken right)
    {
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Показывает, являются ли два экземпляра <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" /> неравными.
    /// </summary>
    /// <param name="left">Первый экземпляр для сравнения.</param>
    /// <param name="right">Второй экземпляр для сравнения.</param>
    /// <returns>
    ///   <see langword="true" /> Если два экземпляра не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(EventRegistrationToken left, EventRegistrationToken right)
    {
      return !left.Equals((object) right);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли указанный объект текущему объекту.
    /// </summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    ///   <see langword="true" />  Если текущий объект равен <paramref name="obj" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is EventRegistrationToken))
        return false;
      return (long) ((EventRegistrationToken) obj).Value == (long) this.Value;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>Хэш-код данного экземпляра.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_value.GetHashCode();
    }
  }
}
