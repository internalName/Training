// Decompiled with JetBrains decompiler
// Type: System.Globalization.DaylightTime
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>Определяет период летнего времени.</summary>
  [ComVisible(true)]
  [Serializable]
  public class DaylightTime
  {
    internal DateTime m_start;
    internal DateTime m_end;
    internal TimeSpan m_delta;

    private DaylightTime()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Globalization.DaylightTime" /> класса с заданной начальной, окончания и время различий.
    /// </summary>
    /// <param name="start">
    ///   Объект, представляющий дату и время начала летнего времени.
    ///    Значение должно быть в формате местного времени.
    /// </param>
    /// <param name="end">
    ///   Объект, представляющий дату и время окончания летнего времени.
    ///    Значение должно быть в формате местного времени.
    /// </param>
    /// <param name="delta">
    ///   Объект, представляющий разность между стандартным и летнее время в тактах.
    /// </param>
    public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
    {
      this.m_start = start;
      this.m_end = end;
      this.m_delta = delta;
    }

    /// <summary>
    ///   Возвращает объект, представляющий дату и время начала летнего времени.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий дату и время начала летнего времени.
    ///    Значение в формате местного времени.
    /// </returns>
    public DateTime Start
    {
      get
      {
        return this.m_start;
      }
    }

    /// <summary>
    ///   Возвращает объект, представляющий дату и время окончания летнего времени.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий дату и время окончания летнего времени.
    ///    Значение в формате местного времени.
    /// </returns>
    public DateTime End
    {
      get
      {
        return this.m_end;
      }
    }

    /// <summary>
    ///   Возвращает интервал времени, которое представляет разницу между зимним временем и летнее время.
    /// </summary>
    /// <returns>
    ///   Интервал времени, которое представляет разницу между зимним временем и летнее время.
    /// </returns>
    public TimeSpan Delta
    {
      get
      {
        return this.m_delta;
      }
    }
  }
}
