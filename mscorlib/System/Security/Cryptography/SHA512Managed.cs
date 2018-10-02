// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SHA512Managed
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет <see cref="T:System.Security.Cryptography.SHA512" /> хэш-алгоритма для входных данных с помощью управляемой библиотеки.
  /// </summary>
  [ComVisible(true)]
  public class SHA512Managed : SHA512
  {
    private static readonly ulong[] _K = new ulong[80]
    {
      4794697086780616226UL,
      8158064640168781261UL,
      13096744586834688815UL,
      16840607885511220156UL,
      4131703408338449720UL,
      6480981068601479193UL,
      10538285296894168987UL,
      12329834152419229976UL,
      15566598209576043074UL,
      1334009975649890238UL,
      2608012711638119052UL,
      6128411473006802146UL,
      8268148722764581231UL,
      9286055187155687089UL,
      11230858885718282805UL,
      13951009754708518548UL,
      16472876342353939154UL,
      17275323862435702243UL,
      1135362057144423861UL,
      2597628984639134821UL,
      3308224258029322869UL,
      5365058923640841347UL,
      6679025012923562964UL,
      8573033837759648693UL,
      10970295158949994411UL,
      12119686244451234320UL,
      12683024718118986047UL,
      13788192230050041572UL,
      14330467153632333762UL,
      15395433587784984357UL,
      489312712824947311UL,
      1452737877330783856UL,
      2861767655752347644UL,
      3322285676063803686UL,
      5560940570517711597UL,
      5996557281743188959UL,
      7280758554555802590UL,
      8532644243296465576UL,
      9350256976987008742UL,
      10552545826968843579UL,
      11727347734174303076UL,
      12113106623233404929UL,
      14000437183269869457UL,
      14369950271660146224UL,
      15101387698204529176UL,
      15463397548674623760UL,
      17586052441742319658UL,
      1182934255886127544UL,
      1847814050463011016UL,
      2177327727835720531UL,
      2830643537854262169UL,
      3796741975233480872UL,
      4115178125766777443UL,
      5681478168544905931UL,
      6601373596472566643UL,
      7507060721942968483UL,
      8399075790359081724UL,
      8693463985226723168UL,
      9568029438360202098UL,
      10144078919501101548UL,
      10430055236837252648UL,
      11840083180663258601UL,
      13761210420658862357UL,
      14299343276471374635UL,
      14566680578165727644UL,
      15097957966210449927UL,
      16922976911328602910UL,
      17689382322260857208UL,
      500013540394364858UL,
      748580250866718886UL,
      1242879168328830382UL,
      1977374033974150939UL,
      2944078676154940804UL,
      3659926193048069267UL,
      4368137639120453308UL,
      4836135668995329356UL,
      5532061633213252278UL,
      6448918945643986474UL,
      6902733635092675308UL,
      7801388544844847127UL
    };
    private byte[] _buffer;
    private ulong _count;
    private ulong[] _stateSHA512;
    private ulong[] _W;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.SHA512Managed" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Включен параметр безопасности федеральным стандартам обработки информации (FIPS).
    ///    Эта реализация не является частью криптографических алгоритмов, утвержденных FIPS для платформы Windows.
    /// </exception>
    public SHA512Managed()
    {
      if (CryptoConfig.AllowOnlyFipsAlgorithms)
        throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
      this._stateSHA512 = new ulong[8];
      this._buffer = new byte[128];
      this._W = new ulong[80];
      this.InitializeState();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.SHA512Managed" /> с помощью управляемой библиотеки.
    /// </summary>
    public override void Initialize()
    {
      this.InitializeState();
      Array.Clear((Array) this._buffer, 0, this._buffer.Length);
      Array.Clear((Array) this._W, 0, this._W.Length);
    }

    /// <summary>
    ///   При переопределении в производном классе, передает данные, записанные в объект, в <see cref="T:System.Security.Cryptography.SHA512Managed" /> хэш-алгоритма для вычисления хэша.
    /// </summary>
    /// <param name="rgb">Входные данные.</param>
    /// <param name="ibStart">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="cbSize">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    [SecuritySafeCritical]
    protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
    {
      this._HashData(rgb, ibStart, cbSize);
    }

    /// <summary>
    ///   Если переопределено в производном классе, завершает вычисление хэша после обработки последних данных криптографическим потоковым объектом.
    /// </summary>
    /// <returns>Вычисляемый хэш-код.</returns>
    [SecuritySafeCritical]
    protected override byte[] HashFinal()
    {
      return this._EndHash();
    }

    private void InitializeState()
    {
      this._count = 0UL;
      this._stateSHA512[0] = 7640891576956012808UL;
      this._stateSHA512[1] = 13503953896175478587UL;
      this._stateSHA512[2] = 4354685564936845355UL;
      this._stateSHA512[3] = 11912009170470909681UL;
      this._stateSHA512[4] = 5840696475078001361UL;
      this._stateSHA512[5] = 11170449401992604703UL;
      this._stateSHA512[6] = 2270897969802886507UL;
      this._stateSHA512[7] = 6620516959819538809UL;
    }

    [SecurityCritical]
    private unsafe void _HashData(byte[] partIn, int ibStart, int cbSize)
    {
      int byteCount = cbSize;
      int srcOffsetBytes = ibStart;
      int dstOffsetBytes = (int) ((long) this._count & (long) sbyte.MaxValue);
      this._count += (ulong) byteCount;
      fixed (ulong* state = this._stateSHA512)
        fixed (byte* block = this._buffer)
          fixed (ulong* expandedBuffer = this._W)
          {
            if (dstOffsetBytes > 0 && dstOffsetBytes + byteCount >= 128)
            {
              Buffer.InternalBlockCopy((Array) partIn, srcOffsetBytes, (Array) this._buffer, dstOffsetBytes, 128 - dstOffsetBytes);
              srcOffsetBytes += 128 - dstOffsetBytes;
              byteCount -= 128 - dstOffsetBytes;
              SHA512Managed.SHATransform(expandedBuffer, state, block);
              dstOffsetBytes = 0;
            }
            while (byteCount >= 128)
            {
              Buffer.InternalBlockCopy((Array) partIn, srcOffsetBytes, (Array) this._buffer, 0, 128);
              srcOffsetBytes += 128;
              byteCount -= 128;
              SHA512Managed.SHATransform(expandedBuffer, state, block);
            }
            if (byteCount > 0)
              Buffer.InternalBlockCopy((Array) partIn, srcOffsetBytes, (Array) this._buffer, dstOffsetBytes, byteCount);
          }
    }

    [SecurityCritical]
    private byte[] _EndHash()
    {
      byte[] block = new byte[64];
      int length = 128 - (int) ((long) this._count & (long) sbyte.MaxValue);
      if (length <= 16)
        length += 128;
      byte[] partIn = new byte[length];
      partIn[0] = (byte) 128;
      ulong num = this._count * 8UL;
      partIn[length - 8] = (byte) (num >> 56 & (ulong) byte.MaxValue);
      partIn[length - 7] = (byte) (num >> 48 & (ulong) byte.MaxValue);
      partIn[length - 6] = (byte) (num >> 40 & (ulong) byte.MaxValue);
      partIn[length - 5] = (byte) (num >> 32 & (ulong) byte.MaxValue);
      partIn[length - 4] = (byte) (num >> 24 & (ulong) byte.MaxValue);
      partIn[length - 3] = (byte) (num >> 16 & (ulong) byte.MaxValue);
      partIn[length - 2] = (byte) (num >> 8 & (ulong) byte.MaxValue);
      partIn[length - 1] = (byte) (num & (ulong) byte.MaxValue);
      this._HashData(partIn, 0, partIn.Length);
      Utils.QuadWordToBigEndian(block, this._stateSHA512, 8);
      this.HashValue = block;
      return block;
    }

    [SecurityCritical]
    private static unsafe void SHATransform(ulong* expandedBuffer, ulong* state, byte* block)
    {
      ulong num1 = *state;
      ulong num2 = (ulong) *(long*) ((IntPtr) state + 8);
      ulong num3 = state[2];
      ulong num4 = state[3];
      ulong num5 = state[4];
      ulong num6 = state[5];
      ulong num7 = state[6];
      ulong num8 = state[7];
      Utils.QuadWordFromBigEndian(expandedBuffer, 16, block);
      SHA512Managed.SHA512Expand(expandedBuffer);
      int index1;
      for (int index2 = 0; index2 < 80; index2 = index1 + 1)
      {
        ulong num9 = num8 + SHA512Managed.Sigma_1(num5) + SHA512Managed.Ch(num5, num6, num7) + SHA512Managed._K[index2] + expandedBuffer[index2];
        ulong num10 = num4 + num9;
        ulong num11 = num9 + SHA512Managed.Sigma_0(num1) + SHA512Managed.Maj(num1, num2, num3);
        int index3 = index2 + 1;
        ulong num12 = num7 + SHA512Managed.Sigma_1(num10) + SHA512Managed.Ch(num10, num5, num6) + SHA512Managed._K[index3] + expandedBuffer[index3];
        ulong num13 = num3 + num12;
        ulong num14 = num12 + SHA512Managed.Sigma_0(num11) + SHA512Managed.Maj(num11, num1, num2);
        int index4 = index3 + 1;
        ulong num15 = num6 + SHA512Managed.Sigma_1(num13) + SHA512Managed.Ch(num13, num10, num5) + SHA512Managed._K[index4] + expandedBuffer[index4];
        ulong num16 = num2 + num15;
        ulong num17 = num15 + SHA512Managed.Sigma_0(num14) + SHA512Managed.Maj(num14, num11, num1);
        int index5 = index4 + 1;
        ulong num18 = num5 + SHA512Managed.Sigma_1(num16) + SHA512Managed.Ch(num16, num13, num10) + SHA512Managed._K[index5] + expandedBuffer[index5];
        ulong num19 = num1 + num18;
        ulong num20 = num18 + SHA512Managed.Sigma_0(num17) + SHA512Managed.Maj(num17, num14, num11);
        int index6 = index5 + 1;
        ulong num21 = num10 + SHA512Managed.Sigma_1(num19) + SHA512Managed.Ch(num19, num16, num13) + SHA512Managed._K[index6] + expandedBuffer[index6];
        num8 = num11 + num21;
        num4 = num21 + SHA512Managed.Sigma_0(num20) + SHA512Managed.Maj(num20, num17, num14);
        int index7 = index6 + 1;
        ulong num22 = num13 + SHA512Managed.Sigma_1(num8) + SHA512Managed.Ch(num8, num19, num16) + SHA512Managed._K[index7] + expandedBuffer[index7];
        num7 = num14 + num22;
        num3 = num22 + SHA512Managed.Sigma_0(num4) + SHA512Managed.Maj(num4, num20, num17);
        int index8 = index7 + 1;
        ulong num23 = num16 + SHA512Managed.Sigma_1(num7) + SHA512Managed.Ch(num7, num8, num19) + SHA512Managed._K[index8] + expandedBuffer[index8];
        num6 = num17 + num23;
        num2 = num23 + SHA512Managed.Sigma_0(num3) + SHA512Managed.Maj(num3, num4, num20);
        index1 = index8 + 1;
        ulong num24 = num19 + SHA512Managed.Sigma_1(num6) + SHA512Managed.Ch(num6, num7, num8) + SHA512Managed._K[index1] + expandedBuffer[index1];
        num5 = num20 + num24;
        num1 = num24 + SHA512Managed.Sigma_0(num2) + SHA512Managed.Maj(num2, num3, num4);
      }
      ulong* numPtr = state;
      long num25 = (long) *numPtr + (long) num1;
      *numPtr = (ulong) num25;
      IntPtr num26 = (IntPtr) state + 8;
      *(long*) num26 = *(long*) num26 + (long) num2;
      IntPtr num27 = (IntPtr) (state + 2);
      *(long*) num27 = *(long*) num27 + (long) num3;
      IntPtr num28 = (IntPtr) (state + 3);
      *(long*) num28 = *(long*) num28 + (long) num4;
      IntPtr num29 = (IntPtr) (state + 4);
      *(long*) num29 = *(long*) num29 + (long) num5;
      IntPtr num30 = (IntPtr) (state + 5);
      *(long*) num30 = *(long*) num30 + (long) num6;
      IntPtr num31 = (IntPtr) (state + 6);
      *(long*) num31 = *(long*) num31 + (long) num7;
      IntPtr num32 = (IntPtr) (state + 7);
      *(long*) num32 = *(long*) num32 + (long) num8;
    }

    private static ulong RotateRight(ulong x, int n)
    {
      return x >> n | x << 64 - n;
    }

    private static ulong Ch(ulong x, ulong y, ulong z)
    {
      return (ulong) ((long) x & (long) y ^ ((long) x ^ -1L) & (long) z);
    }

    private static ulong Maj(ulong x, ulong y, ulong z)
    {
      return (ulong) ((long) x & (long) y ^ (long) x & (long) z ^ (long) y & (long) z);
    }

    private static ulong Sigma_0(ulong x)
    {
      return SHA512Managed.RotateRight(x, 28) ^ SHA512Managed.RotateRight(x, 34) ^ SHA512Managed.RotateRight(x, 39);
    }

    private static ulong Sigma_1(ulong x)
    {
      return SHA512Managed.RotateRight(x, 14) ^ SHA512Managed.RotateRight(x, 18) ^ SHA512Managed.RotateRight(x, 41);
    }

    private static ulong sigma_0(ulong x)
    {
      return SHA512Managed.RotateRight(x, 1) ^ SHA512Managed.RotateRight(x, 8) ^ x >> 7;
    }

    private static ulong sigma_1(ulong x)
    {
      return SHA512Managed.RotateRight(x, 19) ^ SHA512Managed.RotateRight(x, 61) ^ x >> 6;
    }

    [SecurityCritical]
    private static unsafe void SHA512Expand(ulong* x)
    {
      for (int index = 16; index < 80; ++index)
        x[index] = SHA512Managed.sigma_1(x[index - 2]) + x[index - 7] + SHA512Managed.sigma_0(x[index - 15]) + x[index - 16];
    }
  }
}
