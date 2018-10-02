// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.INormalizeForIsolatedStorage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO.IsolatedStorage
{
  /// <summary>
  ///   Разрешает сравнивать изолированное хранилище и домена приложения и свидетельства сборки.
  /// </summary>
  [ComVisible(true)]
  public interface INormalizeForIsolatedStorage
  {
    /// <summary>
    ///   При переопределении в производном классе возвращает нормализованную копию объекта, для которого вызывается.
    /// </summary>
    /// <returns>
    ///   Нормализованный объект, представляющий экземпляр, для которого был вызван этот метод.
    ///    Этот экземпляр может быть строка, потока или любой сериализуемый объект.
    /// </returns>
    object Normalize();
  }
}
