// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.X509Constants
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography.X509Certificates
{
  internal static class X509Constants
  {
    internal const uint CRYPT_EXPORTABLE = 1;
    internal const uint CRYPT_USER_PROTECTED = 2;
    internal const uint CRYPT_MACHINE_KEYSET = 32;
    internal const uint CRYPT_USER_KEYSET = 4096;
    internal const uint PKCS12_ALWAYS_CNG_KSP = 512;
    internal const uint PKCS12_NO_PERSIST_KEY = 32768;
    internal const uint CERT_QUERY_CONTENT_CERT = 1;
    internal const uint CERT_QUERY_CONTENT_CTL = 2;
    internal const uint CERT_QUERY_CONTENT_CRL = 3;
    internal const uint CERT_QUERY_CONTENT_SERIALIZED_STORE = 4;
    internal const uint CERT_QUERY_CONTENT_SERIALIZED_CERT = 5;
    internal const uint CERT_QUERY_CONTENT_SERIALIZED_CTL = 6;
    internal const uint CERT_QUERY_CONTENT_SERIALIZED_CRL = 7;
    internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED = 8;
    internal const uint CERT_QUERY_CONTENT_PKCS7_UNSIGNED = 9;
    internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED_EMBED = 10;
    internal const uint CERT_QUERY_CONTENT_PKCS10 = 11;
    internal const uint CERT_QUERY_CONTENT_PFX = 12;
    internal const uint CERT_QUERY_CONTENT_CERT_PAIR = 13;
    internal const uint CERT_STORE_PROV_MEMORY = 2;
    internal const uint CERT_STORE_PROV_SYSTEM = 10;
    internal const uint CERT_STORE_NO_CRYPT_RELEASE_FLAG = 1;
    internal const uint CERT_STORE_SET_LOCALIZED_NAME_FLAG = 2;
    internal const uint CERT_STORE_DEFER_CLOSE_UNTIL_LAST_FREE_FLAG = 4;
    internal const uint CERT_STORE_DELETE_FLAG = 16;
    internal const uint CERT_STORE_SHARE_STORE_FLAG = 64;
    internal const uint CERT_STORE_SHARE_CONTEXT_FLAG = 128;
    internal const uint CERT_STORE_MANIFOLD_FLAG = 256;
    internal const uint CERT_STORE_ENUM_ARCHIVED_FLAG = 512;
    internal const uint CERT_STORE_UPDATE_KEYID_FLAG = 1024;
    internal const uint CERT_STORE_BACKUP_RESTORE_FLAG = 2048;
    internal const uint CERT_STORE_READONLY_FLAG = 32768;
    internal const uint CERT_STORE_OPEN_EXISTING_FLAG = 16384;
    internal const uint CERT_STORE_CREATE_NEW_FLAG = 8192;
    internal const uint CERT_STORE_MAXIMUM_ALLOWED_FLAG = 4096;
    internal const uint CERT_NAME_EMAIL_TYPE = 1;
    internal const uint CERT_NAME_RDN_TYPE = 2;
    internal const uint CERT_NAME_SIMPLE_DISPLAY_TYPE = 4;
    internal const uint CERT_NAME_FRIENDLY_DISPLAY_TYPE = 5;
    internal const uint CERT_NAME_DNS_TYPE = 6;
    internal const uint CERT_NAME_URL_TYPE = 7;
    internal const uint CERT_NAME_UPN_TYPE = 8;
  }
}
