// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CryptoConfig
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Получает доступ к сведениям о криптографической конфигурации.
  /// </summary>
  [ComVisible(true)]
  public class CryptoConfig
  {
    private static volatile Dictionary<string, string> defaultOidHT = (Dictionary<string, string>) null;
    private static volatile Dictionary<string, object> defaultNameHT = (Dictionary<string, object>) null;
    private static volatile Dictionary<string, string> machineOidHT = (Dictionary<string, string>) null;
    private static volatile Dictionary<string, string> machineNameHT = (Dictionary<string, string>) null;
    private static volatile Dictionary<string, Type> appNameHT = new Dictionary<string, Type>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static volatile Dictionary<string, string> appOidHT = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static volatile string version = (string) null;
    private const string MachineConfigFilename = "machine.config";
    private static volatile bool s_fipsAlgorithmPolicy;
    private static volatile bool s_haveFipsAlgorithmPolicy;
    private static object s_InternalSyncObject;

    /// <summary>
    ///   Указывает, следует ли среда выполнения применять политику для создания только Federal сведения обработки Standard (FIPS) certified алгоритмы.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> для принудительного выполнения политики; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool AllowOnlyFipsAlgorithms
    {
      [SecuritySafeCritical] get
      {
        if (!CryptoConfig.s_haveFipsAlgorithmPolicy)
        {
          if (Utils._GetEnforceFipsPolicySetting())
          {
            if (Environment.OSVersion.Version.Major >= 6)
            {
              bool pfEnabled;
              uint fipsAlgorithmMode = Win32Native.BCryptGetFipsAlgorithmMode(out pfEnabled);
              CryptoConfig.s_fipsAlgorithmPolicy = (fipsAlgorithmMode != 0U && fipsAlgorithmMode != 3221225524U) | pfEnabled;
              CryptoConfig.s_haveFipsAlgorithmPolicy = true;
            }
            else
            {
              CryptoConfig.s_fipsAlgorithmPolicy = Utils.ReadLegacyFipsPolicy();
              CryptoConfig.s_haveFipsAlgorithmPolicy = true;
            }
          }
          else
          {
            CryptoConfig.s_fipsAlgorithmPolicy = false;
            CryptoConfig.s_haveFipsAlgorithmPolicy = true;
          }
        }
        return CryptoConfig.s_fipsAlgorithmPolicy;
      }
    }

    private static string Version
    {
      [SecurityCritical] get
      {
        if (CryptoConfig.version == null)
          CryptoConfig.version = ((RuntimeType) typeof (CryptoConfig)).GetRuntimeAssembly().GetVersion().ToString();
        return CryptoConfig.version;
      }
    }

    private static object InternalSyncObject
    {
      get
      {
        if (CryptoConfig.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref CryptoConfig.s_InternalSyncObject, obj, (object) null);
        }
        return CryptoConfig.s_InternalSyncObject;
      }
    }

    private static Dictionary<string, string> DefaultOidHT
    {
      get
      {
        if (CryptoConfig.defaultOidHT == null)
          CryptoConfig.defaultOidHT = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
          {
            {
              "SHA",
              "1.3.14.3.2.26"
            },
            {
              "SHA1",
              "1.3.14.3.2.26"
            },
            {
              "System.Security.Cryptography.SHA1",
              "1.3.14.3.2.26"
            },
            {
              "System.Security.Cryptography.SHA1CryptoServiceProvider",
              "1.3.14.3.2.26"
            },
            {
              "System.Security.Cryptography.SHA1Cng",
              "1.3.14.3.2.26"
            },
            {
              "System.Security.Cryptography.SHA1Managed",
              "1.3.14.3.2.26"
            },
            {
              "SHA256",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "System.Security.Cryptography.SHA256",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "System.Security.Cryptography.SHA256CryptoServiceProvider",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "System.Security.Cryptography.SHA256Cng",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "System.Security.Cryptography.SHA256Managed",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "SHA384",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "System.Security.Cryptography.SHA384",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "System.Security.Cryptography.SHA384CryptoServiceProvider",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "System.Security.Cryptography.SHA384Cng",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "System.Security.Cryptography.SHA384Managed",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "SHA512",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "System.Security.Cryptography.SHA512",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "System.Security.Cryptography.SHA512CryptoServiceProvider",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "System.Security.Cryptography.SHA512Cng",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "System.Security.Cryptography.SHA512Managed",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "RIPEMD160",
              "1.3.36.3.2.1"
            },
            {
              "System.Security.Cryptography.RIPEMD160",
              "1.3.36.3.2.1"
            },
            {
              "System.Security.Cryptography.RIPEMD160Managed",
              "1.3.36.3.2.1"
            },
            {
              "MD5",
              "1.2.840.113549.2.5"
            },
            {
              "System.Security.Cryptography.MD5",
              "1.2.840.113549.2.5"
            },
            {
              "System.Security.Cryptography.MD5CryptoServiceProvider",
              "1.2.840.113549.2.5"
            },
            {
              "System.Security.Cryptography.MD5Managed",
              "1.2.840.113549.2.5"
            },
            {
              "TripleDESKeyWrap",
              "1.2.840.113549.1.9.16.3.6"
            },
            {
              "RC2",
              "1.2.840.113549.3.2"
            },
            {
              "System.Security.Cryptography.RC2CryptoServiceProvider",
              "1.2.840.113549.3.2"
            },
            {
              "DES",
              "1.3.14.3.2.7"
            },
            {
              "System.Security.Cryptography.DESCryptoServiceProvider",
              "1.3.14.3.2.7"
            },
            {
              "TripleDES",
              "1.2.840.113549.3.7"
            },
            {
              "System.Security.Cryptography.TripleDESCryptoServiceProvider",
              "1.2.840.113549.3.7"
            }
          };
        return CryptoConfig.defaultOidHT;
      }
    }

    private static Dictionary<string, object> DefaultNameHT
    {
      get
      {
        if (CryptoConfig.defaultNameHT == null)
        {
          Dictionary<string, object> dictionary = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          Type type1 = typeof (SHA1CryptoServiceProvider);
          Type type2 = typeof (MD5CryptoServiceProvider);
          Type type3 = typeof (RIPEMD160Managed);
          Type type4 = typeof (HMACMD5);
          Type type5 = typeof (HMACRIPEMD160);
          Type type6 = typeof (HMACSHA1);
          Type type7 = typeof (HMACSHA256);
          Type type8 = typeof (HMACSHA384);
          Type type9 = typeof (HMACSHA512);
          Type type10 = typeof (MACTripleDES);
          Type type11 = typeof (RSACryptoServiceProvider);
          Type type12 = typeof (DSACryptoServiceProvider);
          Type type13 = typeof (DESCryptoServiceProvider);
          Type type14 = typeof (TripleDESCryptoServiceProvider);
          Type type15 = typeof (RC2CryptoServiceProvider);
          Type type16 = typeof (RijndaelManaged);
          Type type17 = typeof (DSASignatureDescription);
          Type type18 = typeof (RSAPKCS1SHA1SignatureDescription);
          Type type19 = typeof (RSAPKCS1SHA256SignatureDescription);
          Type type20 = typeof (RSAPKCS1SHA384SignatureDescription);
          Type type21 = typeof (RSAPKCS1SHA512SignatureDescription);
          Type type22 = typeof (RNGCryptoServiceProvider);
          string str1 = "System.Security.Cryptography.AesCryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str2 = "System.Security.Cryptography.RSACng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str3 = "System.Security.Cryptography.DSACng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str4 = "System.Security.Cryptography.AesManaged, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str5 = "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str6 = "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str7 = "System.Security.Cryptography.MD5Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str8 = "System.Security.Cryptography.SHA1Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str9 = "System.Security.Cryptography.SHA256Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str10 = "System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str11 = "System.Security.Cryptography.SHA384Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str12 = "System.Security.Cryptography.SHA384CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str13 = "System.Security.Cryptography.SHA512Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str14 = "System.Security.Cryptography.SHA512CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          bool onlyFipsAlgorithms = CryptoConfig.AllowOnlyFipsAlgorithms;
          object obj1 = (object) typeof (SHA256Managed);
          if (onlyFipsAlgorithms)
            obj1 = (object) str9;
          object obj2 = onlyFipsAlgorithms ? (object) str11 : (object) typeof (SHA384Managed);
          object obj3 = onlyFipsAlgorithms ? (object) str13 : (object) typeof (SHA512Managed);
          string str15 = "System.Security.Cryptography.DpapiDataProtector, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add("RandomNumberGenerator", (object) type22);
          dictionary.Add("System.Security.Cryptography.RandomNumberGenerator", (object) type22);
          dictionary.Add("SHA", (object) type1);
          dictionary.Add("SHA1", (object) type1);
          dictionary.Add("System.Security.Cryptography.SHA1", (object) type1);
          dictionary.Add("System.Security.Cryptography.SHA1Cng", (object) str8);
          dictionary.Add("System.Security.Cryptography.HashAlgorithm", (object) type1);
          dictionary.Add("MD5", (object) type2);
          dictionary.Add("System.Security.Cryptography.MD5", (object) type2);
          dictionary.Add("System.Security.Cryptography.MD5Cng", (object) str7);
          dictionary.Add("SHA256", obj1);
          dictionary.Add("SHA-256", obj1);
          dictionary.Add("System.Security.Cryptography.SHA256", obj1);
          dictionary.Add("System.Security.Cryptography.SHA256Cng", (object) str9);
          dictionary.Add("System.Security.Cryptography.SHA256CryptoServiceProvider", (object) str10);
          dictionary.Add("SHA384", obj2);
          dictionary.Add("SHA-384", obj2);
          dictionary.Add("System.Security.Cryptography.SHA384", obj2);
          dictionary.Add("System.Security.Cryptography.SHA384Cng", (object) str11);
          dictionary.Add("System.Security.Cryptography.SHA384CryptoServiceProvider", (object) str12);
          dictionary.Add("SHA512", obj3);
          dictionary.Add("SHA-512", obj3);
          dictionary.Add("System.Security.Cryptography.SHA512", obj3);
          dictionary.Add("System.Security.Cryptography.SHA512Cng", (object) str13);
          dictionary.Add("System.Security.Cryptography.SHA512CryptoServiceProvider", (object) str14);
          dictionary.Add("RIPEMD160", (object) type3);
          dictionary.Add("RIPEMD-160", (object) type3);
          dictionary.Add("System.Security.Cryptography.RIPEMD160", (object) type3);
          dictionary.Add("System.Security.Cryptography.RIPEMD160Managed", (object) type3);
          dictionary.Add("System.Security.Cryptography.HMAC", (object) type6);
          dictionary.Add("System.Security.Cryptography.KeyedHashAlgorithm", (object) type6);
          dictionary.Add("HMACMD5", (object) type4);
          dictionary.Add("System.Security.Cryptography.HMACMD5", (object) type4);
          dictionary.Add("HMACRIPEMD160", (object) type5);
          dictionary.Add("System.Security.Cryptography.HMACRIPEMD160", (object) type5);
          dictionary.Add("HMACSHA1", (object) type6);
          dictionary.Add("System.Security.Cryptography.HMACSHA1", (object) type6);
          dictionary.Add("HMACSHA256", (object) type7);
          dictionary.Add("System.Security.Cryptography.HMACSHA256", (object) type7);
          dictionary.Add("HMACSHA384", (object) type8);
          dictionary.Add("System.Security.Cryptography.HMACSHA384", (object) type8);
          dictionary.Add("HMACSHA512", (object) type9);
          dictionary.Add("System.Security.Cryptography.HMACSHA512", (object) type9);
          dictionary.Add("MACTripleDES", (object) type10);
          dictionary.Add("System.Security.Cryptography.MACTripleDES", (object) type10);
          dictionary.Add("RSA", (object) type11);
          dictionary.Add("System.Security.Cryptography.RSA", (object) type11);
          dictionary.Add("System.Security.Cryptography.AsymmetricAlgorithm", (object) type11);
          dictionary.Add("RSAPSS", (object) str2);
          dictionary.Add("DSA-FIPS186-3", (object) str3);
          dictionary.Add("DSA", (object) type12);
          dictionary.Add("System.Security.Cryptography.DSA", (object) type12);
          dictionary.Add("ECDsa", (object) str6);
          dictionary.Add("ECDsaCng", (object) str6);
          dictionary.Add("System.Security.Cryptography.ECDsaCng", (object) str6);
          dictionary.Add("ECDH", (object) str5);
          dictionary.Add("ECDiffieHellman", (object) str5);
          dictionary.Add("ECDiffieHellmanCng", (object) str5);
          dictionary.Add("System.Security.Cryptography.ECDiffieHellmanCng", (object) str5);
          dictionary.Add("DES", (object) type13);
          dictionary.Add("System.Security.Cryptography.DES", (object) type13);
          dictionary.Add("3DES", (object) type14);
          dictionary.Add("TripleDES", (object) type14);
          dictionary.Add("Triple DES", (object) type14);
          dictionary.Add("System.Security.Cryptography.TripleDES", (object) type14);
          dictionary.Add("RC2", (object) type15);
          dictionary.Add("System.Security.Cryptography.RC2", (object) type15);
          dictionary.Add("Rijndael", (object) type16);
          dictionary.Add("System.Security.Cryptography.Rijndael", (object) type16);
          dictionary.Add("System.Security.Cryptography.SymmetricAlgorithm", (object) type16);
          dictionary.Add("AES", (object) str1);
          dictionary.Add("AesCryptoServiceProvider", (object) str1);
          dictionary.Add("System.Security.Cryptography.AesCryptoServiceProvider", (object) str1);
          dictionary.Add("AesManaged", (object) str4);
          dictionary.Add("System.Security.Cryptography.AesManaged", (object) str4);
          dictionary.Add("DpapiDataProtector", (object) str15);
          dictionary.Add("System.Security.Cryptography.DpapiDataProtector", (object) str15);
          dictionary.Add("http://www.w3.org/2000/09/xmldsig#dsa-sha1", (object) type17);
          dictionary.Add("System.Security.Cryptography.DSASignatureDescription", (object) type17);
          dictionary.Add("http://www.w3.org/2000/09/xmldsig#rsa-sha1", (object) type18);
          dictionary.Add("System.Security.Cryptography.RSASignatureDescription", (object) type18);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", (object) type19);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", (object) type20);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha512", (object) type21);
          dictionary.Add("http://www.w3.org/2000/09/xmldsig#sha1", (object) type1);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#sha256", obj1);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#sha512", obj3);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#ripemd160", (object) type3);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#des-cbc", (object) type13);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#tripledes-cbc", (object) type14);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-tripledes", (object) type14);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#aes128-cbc", (object) type16);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-aes128", (object) type16);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#aes192-cbc", (object) type16);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-aes192", (object) type16);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#aes256-cbc", (object) type16);
          dictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-aes256", (object) type16);
          dictionary.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315", (object) "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments", (object) "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2001/10/xml-exc-c14n#", (object) "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2001/10/xml-exc-c14n#WithComments", (object) "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2000/09/xmldsig#base64", (object) "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/TR/1999/REC-xpath-19991116", (object) "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/TR/1999/REC-xslt-19991116", (object) "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2000/09/xmldsig#enveloped-signature", (object) "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2002/07/decrypt#XML", (object) "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform", (object) "System.Security.Cryptography.Xml.XmlLicenseTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2000/09/xmldsig# X509Data", (object) "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2000/09/xmldsig# KeyName", (object) "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue", (object) "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue", (object) "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2000/09/xmldsig# RetrievalMethod", (object) "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2001/04/xmlenc# EncryptedKey", (object) "System.Security.Cryptography.Xml.KeyInfoEncryptedKey, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("http://www.w3.org/2000/09/xmldsig#hmac-sha1", (object) type6);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#md5", (object) type2);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#sha384", obj2);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-md5", (object) type4);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160", (object) type5);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", (object) type7);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha384", (object) type8);
          dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha512", (object) type9);
          dictionary.Add("2.5.29.10", (object) "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
          dictionary.Add("2.5.29.19", (object) "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
          dictionary.Add("2.5.29.14", (object) "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
          dictionary.Add("2.5.29.15", (object) "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
          dictionary.Add("2.5.29.37", (object) "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
          dictionary.Add("X509Chain", (object) "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
          dictionary.Add("1.2.840.113549.1.9.3", (object) "System.Security.Cryptography.Pkcs.Pkcs9ContentType, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("1.2.840.113549.1.9.4", (object) "System.Security.Cryptography.Pkcs.Pkcs9MessageDigest, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("1.2.840.113549.1.9.5", (object) "System.Security.Cryptography.Pkcs.Pkcs9SigningTime, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("1.3.6.1.4.1.311.88.2.1", (object) "System.Security.Cryptography.Pkcs.Pkcs9DocumentName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          dictionary.Add("1.3.6.1.4.1.311.88.2.2", (object) "System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          CryptoConfig.defaultNameHT = dictionary;
        }
        return CryptoConfig.defaultNameHT;
      }
    }

    [SecurityCritical]
    private static void InitializeConfigInfo()
    {
      if (CryptoConfig.machineNameHT != null)
        return;
      lock (CryptoConfig.InternalSyncObject)
      {
        if (CryptoConfig.machineNameHT != null)
          return;
        ConfigNode configNode = CryptoConfig.OpenCryptoConfig();
        if (configNode != null)
        {
          foreach (ConfigNode child in configNode.Children)
          {
            if (CryptoConfig.machineNameHT != null)
            {
              if (CryptoConfig.machineOidHT != null)
                break;
            }
            if (CryptoConfig.machineNameHT == null && string.Compare(child.Name, "cryptoNameMapping", StringComparison.Ordinal) == 0)
              CryptoConfig.machineNameHT = CryptoConfig.InitializeNameMappings(child);
            else if (CryptoConfig.machineOidHT == null && string.Compare(child.Name, "oidMap", StringComparison.Ordinal) == 0)
              CryptoConfig.machineOidHT = CryptoConfig.InitializeOidMappings(child);
          }
        }
        if (CryptoConfig.machineNameHT == null)
          CryptoConfig.machineNameHT = new Dictionary<string, string>();
        if (CryptoConfig.machineOidHT != null)
          return;
        CryptoConfig.machineOidHT = new Dictionary<string, string>();
      }
    }

    /// <summary>
    ///   Добавляет набор имен алгоритм сопоставления для текущего домена приложения.
    /// </summary>
    /// <param name="algorithm">Алгоритм для сопоставления.</param>
    /// <param name="names">
    ///   Массив имен для сопоставления с алгоритмом.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name=" algorithm" /> Или <paramref name="names" /> параметр <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="algorithm" /> нельзя получить доступ из за пределами сборки.
    /// 
    ///   -или-
    /// 
    ///   Одной из записей в <paramref name="names" /> параметр пуст или <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void AddAlgorithm(Type algorithm, params string[] names)
    {
      if (algorithm == (Type) null)
        throw new ArgumentNullException(nameof (algorithm));
      if (!algorithm.IsVisible)
        throw new ArgumentException(Environment.GetResourceString("Cryptography_AlgorithmTypesMustBeVisible"), nameof (algorithm));
      if (names == null)
        throw new ArgumentNullException(nameof (names));
      string[] strArray = new string[names.Length];
      Array.Copy((Array) names, (Array) strArray, strArray.Length);
      foreach (string str in strArray)
      {
        if (string.IsNullOrEmpty(str))
          throw new ArgumentException(Environment.GetResourceString("Cryptography_AddNullOrEmptyName"));
      }
      lock (CryptoConfig.InternalSyncObject)
      {
        foreach (string index in strArray)
          CryptoConfig.appNameHT[index] = algorithm;
      }
    }

    /// <summary>
    ///   Создает новый экземпляр заданного криптографического объекта с заданными аргументами.
    /// </summary>
    /// <param name="name">
    ///   Простое имя криптографического объекта для создания экземпляра.
    /// </param>
    /// <param name="args">
    ///   Аргументы, используемые для создания заданного криптографического объекта.
    /// </param>
    /// <returns>
    ///   Новый экземпляр заданного криптографического объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Алгоритм, описание которого <paramref name="name" /> параметр использовался с включенным режимом федеральным стандартам обработки информации (FIPS), но не является FIPS-совместимым.
    /// </exception>
    [SecuritySafeCritical]
    public static object CreateFromName(string name, params object[] args)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      Type type = (Type) null;
      CryptoConfig.InitializeConfigInfo();
      lock (CryptoConfig.InternalSyncObject)
        type = CryptoConfig.appNameHT.GetValueOrDefault(name);
      if (type == (Type) null)
      {
        string valueOrDefault = CryptoConfig.machineNameHT.GetValueOrDefault(name);
        if (valueOrDefault != null)
        {
          type = Type.GetType(valueOrDefault, false, false);
          if (type != (Type) null && !type.IsVisible)
            type = (Type) null;
        }
      }
      if (type == (Type) null)
      {
        object valueOrDefault = CryptoConfig.DefaultNameHT.GetValueOrDefault(name);
        if (valueOrDefault != null)
        {
          if ((object) (valueOrDefault as Type) != null)
            type = (Type) valueOrDefault;
          else if (valueOrDefault is string)
          {
            type = Type.GetType((string) valueOrDefault, false, false);
            if (type != (Type) null && !type.IsVisible)
              type = (Type) null;
          }
        }
      }
      if (type == (Type) null)
      {
        type = Type.GetType(name, false, false);
        if (type != (Type) null && !type.IsVisible)
          type = (Type) null;
      }
      if (type == (Type) null)
        return (object) null;
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        return (object) null;
      if (args == null)
        args = new object[0];
      MethodBase[] constructors = (MethodBase[]) runtimeType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance);
      if (constructors == null)
        return (object) null;
      List<MethodBase> methodBaseList = new List<MethodBase>();
      for (int index = 0; index < constructors.Length; ++index)
      {
        MethodBase methodBase = constructors[index];
        if (methodBase.GetParameters().Length == args.Length)
          methodBaseList.Add(methodBase);
      }
      if (methodBaseList.Count == 0)
        return (object) null;
      object state;
      RuntimeConstructorInfo method = Type.DefaultBinder.BindToMethod(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, methodBaseList.ToArray(), ref args, (ParameterModifier[]) null, (CultureInfo) null, (string[]) null, out state) as RuntimeConstructorInfo;
      if ((ConstructorInfo) method == (ConstructorInfo) null || typeof (Delegate).IsAssignableFrom(method.DeclaringType))
        return (object) null;
      object obj = method.Invoke(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, Type.DefaultBinder, args, (CultureInfo) null);
      if (state != null)
        Type.DefaultBinder.ReorderArgumentArray(ref args, state);
      return obj;
    }

    /// <summary>
    ///   Создает новый экземпляр заданного криптографического объекта.
    /// </summary>
    /// <param name="name">
    ///   Простое имя криптографического объекта для создания экземпляра.
    /// </param>
    /// <returns>
    ///   Новый экземпляр заданного криптографического объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Алгоритм, описание которого <paramref name="name" /> параметр использовался с включенным режимом федеральным стандартам обработки информации (FIPS), но не является FIPS-совместимым.
    /// </exception>
    public static object CreateFromName(string name)
    {
      return CryptoConfig.CreateFromName(name, (object[]) null);
    }

    /// <summary>
    ///   Добавляет набор имен для сопоставления идентификатора объекта для текущего домена приложения.
    /// </summary>
    /// <param name="oid">
    ///   Идентификатор объекта (OID) для сопоставления.
    /// </param>
    /// <param name="names">
    ///   Массив имен для сопоставления с идентификатором Объекта.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name=" oid" /> Или <paramref name="names" /> параметр <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Одной из записей в <paramref name="names" /> параметр пуст или <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void AddOID(string oid, params string[] names)
    {
      if (oid == null)
        throw new ArgumentNullException(nameof (oid));
      if (names == null)
        throw new ArgumentNullException(nameof (names));
      string[] strArray = new string[names.Length];
      Array.Copy((Array) names, (Array) strArray, strArray.Length);
      foreach (string str in strArray)
      {
        if (string.IsNullOrEmpty(str))
          throw new ArgumentException(Environment.GetResourceString("Cryptography_AddNullOrEmptyName"));
      }
      lock (CryptoConfig.InternalSyncObject)
      {
        foreach (string index in strArray)
          CryptoConfig.appOidHT[index] = oid;
      }
    }

    /// <summary>
    ///   Возвращает идентификатор объекта (OID) алгоритма, соответствующего заданному простому имени.
    /// </summary>
    /// <param name="name">
    ///   Простое имя алгоритма, для которого необходимо получить идентификатор.
    /// </param>
    /// <returns>OID заданного алгоритма.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public static string MapNameToOID(string name)
    {
      return CryptoConfig.MapNameToOID(name, OidGroup.AllGroups);
    }

    [SecuritySafeCritical]
    internal static string MapNameToOID(string name, OidGroup oidGroup)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      CryptoConfig.InitializeConfigInfo();
      string str = (string) null;
      lock (CryptoConfig.InternalSyncObject)
        str = CryptoConfig.appOidHT.GetValueOrDefault(name);
      if (str == null)
        str = CryptoConfig.machineOidHT.GetValueOrDefault(name);
      if (str == null)
        str = CryptoConfig.DefaultOidHT.GetValueOrDefault(name);
      if (str == null)
        str = X509Utils.GetOidFromFriendlyName(name, oidGroup);
      return str;
    }

    /// <summary>Кодирует идентификатор указанного объекта (OID).</summary>
    /// <param name="str">Кодируемый OID.</param>
    /// <returns>Массив байтов, содержащий кодированный OID.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   Произошла ошибка при кодировании OID.
    /// </exception>
    public static byte[] EncodeOID(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      char[] chArray = new char[1]{ '.' };
      string[] strArray = str.Split(chArray);
      uint[] numArray1 = new uint[strArray.Length];
      for (int index = 0; index < strArray.Length; ++index)
        numArray1[index] = (uint) int.Parse(strArray[index], (IFormatProvider) CultureInfo.InvariantCulture);
      byte[] numArray2 = new byte[numArray1.Length * 5];
      int destinationIndex = 0;
      if (numArray1.Length < 2)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOID"));
      byte[] numArray3 = CryptoConfig.EncodeSingleOIDNum(numArray1[0] * 40U + numArray1[1]);
      Array.Copy((Array) numArray3, 0, (Array) numArray2, destinationIndex, numArray3.Length);
      int num = destinationIndex + numArray3.Length;
      for (int index = 2; index < numArray1.Length; ++index)
      {
        byte[] numArray4 = CryptoConfig.EncodeSingleOIDNum(numArray1[index]);
        Buffer.InternalBlockCopy((Array) numArray4, 0, (Array) numArray2, num, numArray4.Length);
        num += numArray4.Length;
      }
      if (num > (int) sbyte.MaxValue)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_Config_EncodedOIDError"));
      byte[] numArray5 = new byte[num + 2];
      numArray5[0] = (byte) 6;
      numArray5[1] = (byte) num;
      Buffer.InternalBlockCopy((Array) numArray2, 0, (Array) numArray5, 2, num);
      return numArray5;
    }

    private static byte[] EncodeSingleOIDNum(uint dwValue)
    {
      if ((int) dwValue < 128)
        return new byte[1]{ (byte) dwValue };
      if (dwValue < 16384U)
        return new byte[2]
        {
          (byte) (dwValue >> 7 | 128U),
          (byte) (dwValue & (uint) sbyte.MaxValue)
        };
      if (dwValue < 2097152U)
        return new byte[3]
        {
          (byte) (dwValue >> 14 | 128U),
          (byte) (dwValue >> 7 | 128U),
          (byte) (dwValue & (uint) sbyte.MaxValue)
        };
      if (dwValue < 268435456U)
        return new byte[4]
        {
          (byte) (dwValue >> 21 | 128U),
          (byte) (dwValue >> 14 | 128U),
          (byte) (dwValue >> 7 | 128U),
          (byte) (dwValue & (uint) sbyte.MaxValue)
        };
      return new byte[5]
      {
        (byte) (dwValue >> 28 | 128U),
        (byte) (dwValue >> 21 | 128U),
        (byte) (dwValue >> 14 | 128U),
        (byte) (dwValue >> 7 | 128U),
        (byte) (dwValue & (uint) sbyte.MaxValue)
      };
    }

    private static Dictionary<string, string> InitializeNameMappings(ConfigNode nameMappingNode)
    {
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      foreach (ConfigNode child1 in nameMappingNode.Children)
      {
        if (string.Compare(child1.Name, "cryptoClasses", StringComparison.Ordinal) == 0)
        {
          foreach (ConfigNode child2 in child1.Children)
          {
            if (string.Compare(child2.Name, "cryptoClass", StringComparison.Ordinal) == 0 && child2.Attributes.Count > 0)
            {
              DictionaryEntry attribute = child2.Attributes[0];
              dictionary2.Add((string) attribute.Key, (string) attribute.Value);
            }
          }
        }
        else if (string.Compare(child1.Name, "nameEntry", StringComparison.Ordinal) == 0)
        {
          string key1 = (string) null;
          string key2 = (string) null;
          foreach (DictionaryEntry attribute in child1.Attributes)
          {
            if (string.Compare((string) attribute.Key, "name", StringComparison.Ordinal) == 0)
              key1 = (string) attribute.Value;
            else if (string.Compare((string) attribute.Key, "class", StringComparison.Ordinal) == 0)
              key2 = (string) attribute.Value;
          }
          if (key1 != null && key2 != null)
          {
            string valueOrDefault = dictionary2.GetValueOrDefault(key2);
            if (valueOrDefault != null)
              dictionary1.Add(key1, valueOrDefault);
          }
        }
      }
      return dictionary1;
    }

    private static Dictionary<string, string> InitializeOidMappings(ConfigNode oidMappingNode)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (ConfigNode child in oidMappingNode.Children)
      {
        if (string.Compare(child.Name, "oidEntry", StringComparison.Ordinal) == 0)
        {
          string str = (string) null;
          string key = (string) null;
          foreach (DictionaryEntry attribute in child.Attributes)
          {
            if (string.Compare((string) attribute.Key, "OID", StringComparison.Ordinal) == 0)
              str = (string) attribute.Value;
            else if (string.Compare((string) attribute.Key, "name", StringComparison.Ordinal) == 0)
              key = (string) attribute.Value;
          }
          if (key != null && str != null)
            dictionary.Add(key, str);
        }
      }
      return dictionary;
    }

    [SecurityCritical]
    private static ConfigNode OpenCryptoConfig()
    {
      string str = Config.MachineDirectory + "machine.config";
      new FileIOPermission(FileIOPermissionAccess.Read, str).Assert();
      if (!File.Exists(str))
        return (ConfigNode) null;
      CodeAccessPermission.RevertAssert();
      ConfigNode configNode1 = new ConfigTreeParser().Parse(str, "configuration", true);
      if (configNode1 == null)
        return (ConfigNode) null;
      ConfigNode configNode2 = (ConfigNode) null;
      foreach (ConfigNode child in configNode1.Children)
      {
        bool flag = false;
        if (string.Compare(child.Name, "mscorlib", StringComparison.Ordinal) == 0)
        {
          foreach (DictionaryEntry attribute in child.Attributes)
          {
            if (string.Compare((string) attribute.Key, "version", StringComparison.Ordinal) == 0)
            {
              flag = true;
              if (string.Compare((string) attribute.Value, CryptoConfig.Version, StringComparison.Ordinal) == 0)
              {
                configNode2 = child;
                break;
              }
            }
          }
          if (!flag)
            configNode2 = child;
        }
        if (configNode2 != null)
          break;
      }
      if (configNode2 == null)
        return (ConfigNode) null;
      foreach (ConfigNode child in configNode2.Children)
      {
        if (string.Compare(child.Name, "cryptographySettings", StringComparison.Ordinal) == 0)
          return child;
      }
      return (ConfigNode) null;
    }
  }
}
