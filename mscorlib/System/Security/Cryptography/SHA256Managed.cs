// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SHA256Managed
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет <see cref="T:System.Security.Cryptography.SHA256" /> хэш для входных данных с помощью управляемой библиотеки.
  /// </summary>
  [ComVisible(true)]
  public class SHA256Managed : SHA256
  {
    private static readonly uint[] _K = new uint[64]
    {
      1116352408U,
      1899447441U,
      3049323471U,
      3921009573U,
      961987163U,
      1508970993U,
      2453635748U,
      2870763221U,
      3624381080U,
      310598401U,
      607225278U,
      1426881987U,
      1925078388U,
      2162078206U,
      2614888103U,
      3248222580U,
      3835390401U,
      4022224774U,
      264347078U,
      604807628U,
      770255983U,
      1249150122U,
      1555081692U,
      1996064986U,
      2554220882U,
      2821834349U,
      2952996808U,
      3210313671U,
      3336571891U,
      3584528711U,
      113926993U,
      338241895U,
      666307205U,
      773529912U,
      1294757372U,
      1396182291U,
      1695183700U,
      1986661051U,
      2177026350U,
      2456956037U,
      2730485921U,
      2820302411U,
      3259730800U,
      3345764771U,
      3516065817U,
      3600352804U,
      4094571909U,
      275423344U,
      430227734U,
      506948616U,
      659060556U,
      883997877U,
      958139571U,
      1322822218U,
      1537002063U,
      1747873779U,
      1955562222U,
      2024104815U,
      2227730452U,
      2361852424U,
      2428436474U,
      2756734187U,
      3204031479U,
      3329325298U
    };
    private byte[] _buffer;
    private long _count;
    private uint[] _stateSHA256;
    private uint[] _W;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.SHA256Managed" /> класса с помощью управляемой библиотеки.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Включен параметр безопасности федеральным стандартам обработки информации (FIPS).
    ///    Эта реализация не является частью криптографических алгоритмов, утвержденных FIPS для платформы Windows.
    /// </exception>
    public SHA256Managed()
    {
      if (CryptoConfig.AllowOnlyFipsAlgorithms)
        throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
      this._stateSHA256 = new uint[8];
      this._buffer = new byte[64];
      this._W = new uint[64];
      this.InitializeState();
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Security.Cryptography.SHA256Managed" />.
    /// </summary>
    public override void Initialize()
    {
      this.InitializeState();
      Array.Clear((Array) this._buffer, 0, this._buffer.Length);
      Array.Clear((Array) this._W, 0, this._W.Length);
    }

    /// <summary>
    ///   При переопределении в производном классе, передает данные, записанные в объект, в <see cref="T:System.Security.Cryptography.SHA256" /> хэш-алгоритма для вычисления хэша.
    /// </summary>
    /// <param name="rgb">Входные данные.</param>
    /// <param name="ibStart">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="cbSize">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
    {
      this._HashData(rgb, ibStart, cbSize);
    }

    /// <summary>
    ///   Если переопределено в производном классе, завершает вычисление хэша после обработки последних данных криптографическим потоковым объектом.
    /// </summary>
    /// <returns>Вычисляемый хэш-код.</returns>
    protected override byte[] HashFinal()
    {
      return this._EndHash();
    }

    private void InitializeState()
    {
      this._count = 0L;
      this._stateSHA256[0] = 1779033703U;
      this._stateSHA256[1] = 3144134277U;
      this._stateSHA256[2] = 1013904242U;
      this._stateSHA256[3] = 2773480762U;
      this._stateSHA256[4] = 1359893119U;
      this._stateSHA256[5] = 2600822924U;
      this._stateSHA256[6] = 528734635U;
      this._stateSHA256[7] = 1541459225U;
    }

    [SecuritySafeCritical]
    private unsafe void _HashData(byte[] partIn, int ibStart, int cbSize)
    {
      int byteCount = cbSize;
      int srcOffsetBytes = ibStart;
      int dstOffsetBytes = (int) (this._count & 63L);
      this._count += (long) byteCount;
      fixed (uint* state = this._stateSHA256)
        fixed (byte* block = this._buffer)
          fixed (uint* expandedBuffer = this._W)
          {
            if (dstOffsetBytes > 0 && dstOffsetBytes + byteCount >= 64)
            {
              Buffer.InternalBlockCopy((Array) partIn, srcOffsetBytes, (Array) this._buffer, dstOffsetBytes, 64 - dstOffsetBytes);
              srcOffsetBytes += 64 - dstOffsetBytes;
              byteCount -= 64 - dstOffsetBytes;
              SHA256Managed.SHATransform(expandedBuffer, state, block);
              dstOffsetBytes = 0;
            }
            while (byteCount >= 64)
            {
              Buffer.InternalBlockCopy((Array) partIn, srcOffsetBytes, (Array) this._buffer, 0, 64);
              srcOffsetBytes += 64;
              byteCount -= 64;
              SHA256Managed.SHATransform(expandedBuffer, state, block);
            }
            if (byteCount > 0)
              Buffer.InternalBlockCopy((Array) partIn, srcOffsetBytes, (Array) this._buffer, dstOffsetBytes, byteCount);
          }
    }

    private byte[] _EndHash()
    {
      byte[] block = new byte[32];
      int length = 64 - (int) (this._count & 63L);
      if (length <= 8)
        length += 64;
      byte[] partIn = new byte[length];
      partIn[0] = (byte) 128;
      long num = this._count * 8L;
      partIn[length - 8] = (byte) ((ulong) (num >> 56) & (ulong) byte.MaxValue);
      partIn[length - 7] = (byte) ((ulong) (num >> 48) & (ulong) byte.MaxValue);
      partIn[length - 6] = (byte) ((ulong) (num >> 40) & (ulong) byte.MaxValue);
      partIn[length - 5] = (byte) ((ulong) (num >> 32) & (ulong) byte.MaxValue);
      partIn[length - 4] = (byte) ((ulong) (num >> 24) & (ulong) byte.MaxValue);
      partIn[length - 3] = (byte) ((ulong) (num >> 16) & (ulong) byte.MaxValue);
      partIn[length - 2] = (byte) ((ulong) (num >> 8) & (ulong) byte.MaxValue);
      partIn[length - 1] = (byte) ((ulong) num & (ulong) byte.MaxValue);
      this._HashData(partIn, 0, partIn.Length);
      Utils.DWORDToBigEndian(block, this._stateSHA256, 8);
      this.HashValue = block;
      return block;
    }

    [SecurityCritical]
    private static unsafe void SHATransform(uint* expandedBuffer, uint* state, byte* block)
    {
      uint num1 = *state;
      uint num2 = *(uint*) ((IntPtr) state + 4);
      uint num3 = state[2];
      uint num4 = state[3];
      uint num5 = state[4];
      uint num6 = state[5];
      uint num7 = state[6];
      uint num8 = state[7];
      Utils.DWORDFromBigEndian(expandedBuffer, 16, block);
      SHA256Managed.SHA256Expand(expandedBuffer);
      int index1;
      for (int index2 = 0; index2 < 64; index2 = index1 + 1)
      {
        uint num9 = num8 + SHA256Managed.Sigma_1(num5) + SHA256Managed.Ch(num5, num6, num7) + SHA256Managed._K[index2] + expandedBuffer[index2];
        uint num10 = num4 + num9;
        uint num11 = num9 + SHA256Managed.Sigma_0(num1) + SHA256Managed.Maj(num1, num2, num3);
        int index3 = index2 + 1;
        uint num12 = num7 + SHA256Managed.Sigma_1(num10) + SHA256Managed.Ch(num10, num5, num6) + SHA256Managed._K[index3] + expandedBuffer[index3];
        uint num13 = num3 + num12;
        uint num14 = num12 + SHA256Managed.Sigma_0(num11) + SHA256Managed.Maj(num11, num1, num2);
        int index4 = index3 + 1;
        uint num15 = num6 + SHA256Managed.Sigma_1(num13) + SHA256Managed.Ch(num13, num10, num5) + SHA256Managed._K[index4] + expandedBuffer[index4];
        uint num16 = num2 + num15;
        uint num17 = num15 + SHA256Managed.Sigma_0(num14) + SHA256Managed.Maj(num14, num11, num1);
        int index5 = index4 + 1;
        uint num18 = num5 + SHA256Managed.Sigma_1(num16) + SHA256Managed.Ch(num16, num13, num10) + SHA256Managed._K[index5] + expandedBuffer[index5];
        uint num19 = num1 + num18;
        uint num20 = num18 + SHA256Managed.Sigma_0(num17) + SHA256Managed.Maj(num17, num14, num11);
        int index6 = index5 + 1;
        uint num21 = num10 + SHA256Managed.Sigma_1(num19) + SHA256Managed.Ch(num19, num16, num13) + SHA256Managed._K[index6] + expandedBuffer[index6];
        num8 = num11 + num21;
        num4 = num21 + SHA256Managed.Sigma_0(num20) + SHA256Managed.Maj(num20, num17, num14);
        int index7 = index6 + 1;
        uint num22 = num13 + SHA256Managed.Sigma_1(num8) + SHA256Managed.Ch(num8, num19, num16) + SHA256Managed._K[index7] + expandedBuffer[index7];
        num7 = num14 + num22;
        num3 = num22 + SHA256Managed.Sigma_0(num4) + SHA256Managed.Maj(num4, num20, num17);
        int index8 = index7 + 1;
        uint num23 = num16 + SHA256Managed.Sigma_1(num7) + SHA256Managed.Ch(num7, num8, num19) + SHA256Managed._K[index8] + expandedBuffer[index8];
        num6 = num17 + num23;
        num2 = num23 + SHA256Managed.Sigma_0(num3) + SHA256Managed.Maj(num3, num4, num20);
        index1 = index8 + 1;
        uint num24 = num19 + SHA256Managed.Sigma_1(num6) + SHA256Managed.Ch(num6, num7, num8) + SHA256Managed._K[index1] + expandedBuffer[index1];
        num5 = num20 + num24;
        num1 = num24 + SHA256Managed.Sigma_0(num2) + SHA256Managed.Maj(num2, num3, num4);
      }
      uint* numPtr = state;
      int num25 = (int) *numPtr + (int) num1;
      *numPtr = (uint) num25;
      IntPtr num26 = (IntPtr) state + 4;
      *(int*) num26 = (int) *(uint*) num26 + (int) num2;
      IntPtr num27 = (IntPtr) (state + 2);
      *(int*) num27 = (int) *(uint*) num27 + (int) num3;
      IntPtr num28 = (IntPtr) (state + 3);
      *(int*) num28 = (int) *(uint*) num28 + (int) num4;
      IntPtr num29 = (IntPtr) (state + 4);
      *(int*) num29 = (int) *(uint*) num29 + (int) num5;
      IntPtr num30 = (IntPtr) (state + 5);
      *(int*) num30 = (int) *(uint*) num30 + (int) num6;
      IntPtr num31 = (IntPtr) (state + 6);
      *(int*) num31 = (int) *(uint*) num31 + (int) num7;
      IntPtr num32 = (IntPtr) (state + 7);
      *(int*) num32 = (int) *(uint*) num32 + (int) num8;
    }

    private static uint RotateRight(uint x, int n)
    {
      return x >> n | x << 32 - n;
    }

    private static uint Ch(uint x, uint y, uint z)
    {
      return (uint) ((int) x & (int) y ^ ((int) x ^ -1) & (int) z);
    }

    private static uint Maj(uint x, uint y, uint z)
    {
      return (uint) ((int) x & (int) y ^ (int) x & (int) z ^ (int) y & (int) z);
    }

    private static uint sigma_0(uint x)
    {
      return SHA256Managed.RotateRight(x, 7) ^ SHA256Managed.RotateRight(x, 18) ^ x >> 3;
    }

    private static uint sigma_1(uint x)
    {
      return SHA256Managed.RotateRight(x, 17) ^ SHA256Managed.RotateRight(x, 19) ^ x >> 10;
    }

    private static uint Sigma_0(uint x)
    {
      return SHA256Managed.RotateRight(x, 2) ^ SHA256Managed.RotateRight(x, 13) ^ SHA256Managed.RotateRight(x, 22);
    }

    private static uint Sigma_1(uint x)
    {
      return SHA256Managed.RotateRight(x, 6) ^ SHA256Managed.RotateRight(x, 11) ^ SHA256Managed.RotateRight(x, 25);
    }

    [SecurityCritical]
    private static unsafe void SHA256Expand(uint* x)
    {
      for (int index = 16; index < 64; ++index)
        x[index] = SHA256Managed.sigma_1(x[index - 2]) + x[index - 7] + SHA256Managed.sigma_0(x[index - 15]) + x[index - 16];
    }
  }
}
