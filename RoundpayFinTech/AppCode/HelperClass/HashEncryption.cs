using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using RoundpayFinTech.AppCode.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fintech.AppCode.HelperClass
{

    public class HashEncryption
    {
        private const string EncryptionKey = "HASH64MD5";
        private const string EncryptionKeyApp = "0eed73feb2045049a47ff5820c9c8718";
        private static Lazy<HashEncryption> Instance = new Lazy<HashEncryption>(() => new HashEncryption());
        public static HashEncryption O => Instance.Value;
        private HashEncryption() { }
        public string MD5Hash(string Data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public string MD5FileHash(string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (MD5 md5 = new MD5CryptoServiceProvider())
                {
                    byte[] hash = md5.ComputeHash(stream);
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in hash)
                    {
                        builder.AppendFormat("{0:x2}", b);
                    }
                    return builder.ToString();
                }
            }
        }
        public string GenerateJWTTokenForRAPIPay(string SecretKey, string customerMobile, string timeStamp, string agentMobile)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("customerMobile", customerMobile),
                    new Claim("timeStamp", timeStamp),
                    new Claim("agentMobile", agentMobile)
                }),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string SHA256Hash(string Data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
        public string SHA256HashBase64(string Data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));

            return Convert.ToBase64String(hash);
        }
        public string SHA256_ComputeHash(string text, string secretKey)
        {
            var sb = new StringBuilder(); ;
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            using (var hmac = new HMACSHA256(secretkeyBytes))
            {
                byte[] hash = hmac.ComputeHash(inputBytes);
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }

        public string SHA384Hash(string Data)
        {
            SHA384 sha = new SHA384Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public string SHA512Hash(string Data)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public string SHA512HashUTF8(string data)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = sha512.ComputeHash(bytes);

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public string GenerateSHA512(string keyStr, string dataStr)
        {
            UTF8Encoding mEncoding = new UTF8Encoding();
            string sResult = string.Empty;
            byte[] keyBytes;
            byte[] messageBytes;
            try
            {
                keyBytes = mEncoding.GetBytes(keyStr);
                messageBytes = mEncoding.GetBytes(dataStr);

                using (var hmacsha = new HMACSHA512(keyBytes))
                {
                    hmacsha.ComputeHash(messageBytes);
                    sResult = GetStringFromHash(hmacsha.Hash);
                }
            }
            catch (Exception ex)
            {
            }
            return sResult;
        }
        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i <= hash.Length - 1; i++)
                result.Append(hash[i].ToString("X2"));
            return result.ToString();
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ23456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString().Equals("5a6EndOquzmBhw7Y4NkUYw==") ? "2B1tRagxDX7+9dtWgrpqEw==" : res.ToString();
        }

        public string CreatePasswordNumeric(int length)
        {
            const string valid = "1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        //pushkey|orderid|amount|sessid
        public string AppEncrypt(string Data)
        {
            return MD5Hash(MD5Hash(Data) + EncryptionKey);
        }
        public string AppEncryptPayment(string Data)
        {
            return MD5Hash(MD5Hash(Data) + EncryptionKeyApp);
        }
        //Password 
        public string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string Decrypt(string cipherText)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return cipherText;
        }

        public string Encrypt(string clearText, string password, string salt)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), iterations: 65536);
                encryptor.Key = pdb.GetBytes(16);//pdb.GetBytes(32);
                encryptor.IV = Encoding.UTF8.GetBytes("IV_VALUE_16_BYTE");//pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string DevEncrypt(string Data)
        {
            return MD5Hash(MD5Hash(Data) + "hash32");
        }

        public string ConvertStringToHex(String input)
        {
            try
            {
                Byte[] stringBytes = Encoding.Unicode.GetBytes(input);
                StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
                foreach (byte b in stringBytes)
                {
                    sbBytes.AppendFormat("{0:X2}", b);
                }
                return sbBytes.ToString();
            }
            catch (Exception)
            {
                return "";
            }

        }
        public string ConvertHexToString(String hexInput)
        {
            try
            {
                int numberChars = hexInput.Length;
                byte[] bytes = new byte[numberChars / 2];
                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
                }
                return Encoding.Unicode.GetString(bytes);
            }
            catch (Exception)
            {
                return hexInput;
            }

        }
        public string EncryptForURL(string data)
        {
            string salt = Encrypt(data);
            return ConvertStringToHex(salt);
        }
        public string DecryptForURL(string encdata)
        {
            string decdata = ConvertHexToString(encdata);
            return Decrypt(decdata);
        }
        #region RSAAlgoForDevices
        RsaKeyParameters publicKey;
        public string GenerateSessionKey()
        {
            try
            {
                SecureRandom random = new SecureRandom();
                KeyGenerationParameters parameters = new KeyGenerationParameters(random, 128);
                CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator("AES");

                keyGenerator.Init(parameters);
                return Convert.ToBase64String(keyGenerator.GenerateKey());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public string GenerateSha256Hash(byte[] message)
        {
            try
            {
                IDigest digest = new Sha256Digest();
                digest.Reset();
                byte[] buffer = new byte[digest.GetDigestSize()];
                digest.BlockUpdate(message, 0, message.Length);
                digest.DoFinal(buffer, 0);

                return Convert.ToBase64String(buffer);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string EncryptUsingPublicKey(byte[] data, string path)
        {
            FileStream stream = null;
            try
            {
                IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
                stream = new FileStream(path, FileMode.Open);
                X509CertificateParser certParser = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate certificate = certParser.ReadCertificate(stream);
                this.publicKey = (RsaKeyParameters)certificate.GetPublicKey();
                cipher.Init(true, publicKey);
                stream.Close();
                return Convert.ToBase64String(cipher.DoFinal(data));

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string DecryptUsingPrivateKey(byte[] data, string path)
        {
            try
            {
                var fileStream = System.IO.File.OpenText(path);
                var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(fileStream);
                var KeyParameter = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)pemReader.ReadObject();
                AsymmetricKeyParameter privateKey = KeyParameter;
                RsaEngine e = new RsaEngine();
                e.Init(false, privateKey);

                byte[] decipheredBytes = e.ProcessBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(decipheredBytes, 0, decipheredBytes.Length);

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string DecryptUsingPrivatePFXKey(byte[] data, string path)
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(path, ApplicationSetting.ICICIUPIPrivatePFX, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.DefaultKeySet | X509KeyStorageFlags.UserKeySet);

                if (certificate.HasPrivateKey)
                {
                    using (var rsa = (RSA)certificate.PrivateKey)
                    {
                        var acryptedData = rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
                        return Encoding.UTF8.GetString(acryptedData, 0, acryptedData.Length);
                    }
                }
            }
            catch (Exception)
            {
                X509Certificate2 certificate = new X509Certificate2(path, ApplicationSetting.ICICIUPIPrivatePFX, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.DefaultKeySet);

                if (certificate.HasPrivateKey)
                {
                    using (var rsa = (RSA)certificate.PrivateKey)
                    {
                        var acryptedData = rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
                        return Encoding.UTF8.GetString(acryptedData, 0, acryptedData.Length);
                    }
                }
            }

            return string.Empty;
        }
        public string EncryptUsingSessionKey(byte[] skey, byte[] data)
        {
            try
            {
                PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new AesEngine());
                cipher.Init(true, new KeyParameter(skey));
                byte[] sourceArray = new byte[cipher.GetOutputSize(data.Length)];
                int num2 = cipher.ProcessBytes(data, 0, data.Length, sourceArray, 0);
                int num3 = cipher.DoFinal(sourceArray, num2);
                byte[] destinationArray = new byte[num2 + num3];
                Array.Copy(sourceArray, 0, destinationArray, 0, destinationArray.Length);
                return Convert.ToBase64String(destinationArray);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string EncryptUsingPublicKeyPEM(byte[] data, string path)
        {
            try
            {
                IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
                var fileStream = System.IO.File.OpenText(path);
                var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(fileStream);
                RsaKeyParameters publicKeyParameters = (RsaKeyParameters)pemReader.ReadObject();
                cipher.Init(true, publicKeyParameters);
                cipher.DoFinal(data);
                return Convert.ToBase64String(cipher.DoFinal(data));

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return string.Empty;
        }
        #endregion

        //public string AndroidEncryption() {

        //    byte[] salt = generateSalt();
        //    SecretKey key = deriveKey(password, salt);

        //    try
        //    {
        //        Cipher cipher = Cipher.getInstance(CIPHER_ALGORITHM);
        //        byte[] iv = generateIv(cipher.getBlockSize());
        //        IvParameterSpec ivParams = new IvParameterSpec(iv);
        //        cipher.init(Cipher.ENCRYPT_MODE, key, ivParams);
        //        byte[] cipherText = cipher.doFinal(plaintext.getBytes("UTF-8"));
        //        String format = new NativeUtil().stringFormat();
        //        if (salt != null)
        //        {
        //            return String.format(format,
        //                    toBase64(salt),
        //                    DELIMITER,
        //                    toBase64(iv),
        //                    DELIMITER,
        //                    toBase64(cipherText));
        //        }

        //        return String.format(format.substring(0, 6),
        //                toBase64(iv),
        //                DELIMITER,
        //                toBase64(cipherText));
        //    }
        //    catch (GeneralSecurityException e)
        //    {
        //        throw new RuntimeException(e);
        //    }
        //    catch (UnsupportedEncodingException e)
        //    {
        //        throw new RuntimeException(e);
        //    }

        //}

    }
    class AesBase64Wrapper
    {
        private static string IV = "IV_VALUE_16_BYTE";
        private static string PASSWORD = "12345678901";
        private static string SALT = "12345678901";

        public static string EncryptAndEncode(string raw)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                ICryptoTransform e = GetCryptoTransform(csp, true);
                byte[] inputBuffer = Encoding.UTF8.GetBytes(raw);
                byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                string encrypted = Convert.ToBase64String(output);
                return encrypted;
            }
        }

        public static string DecodeAndDecrypt(string encrypted)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                var d = GetCryptoTransform(csp, false);
                byte[] output = Convert.FromBase64String(encrypted);
                byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);
                string decypted = Encoding.UTF8.GetString(decryptedOutput);
                return decypted;
            }
        }

        private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), Encoding.UTF8.GetBytes(SALT), 65536);
            byte[] key = spec.GetBytes(16);


            csp.IV = Encoding.UTF8.GetBytes(IV);
            csp.Key = key;
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }
    }
}
