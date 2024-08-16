using System.Security.Cryptography;
using System.Text;
using EcrOneClick.Shared.Encryption.Abstract;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace EcrOneClick.Shared.Encryption;

public class AesEncryptionService(ILogger<AesEncryptionService> logger) : IEncryptionService
{
    public Result<string> EncryptString(string key, string plaintText)
    {
        try
        {
            var iv = new byte[16];
            byte[] array;

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWrite = new StreamWriter(cryptoStream))
                        {
                            streamWrite.Write(plaintText);                            
                        }
                        
                        array = memoryStream.ToArray();
                    }
                }
            }
            
            var encryptedText = Convert.ToBase64String(array);
            
            logger.LogInformation("[{Service}.{Method}]: Text encrypted successfully", nameof(AesEncryptionService), nameof(EncryptString));
            
            return Result.Ok(encryptedText);
        }
        catch (Exception e)
        {
            logger.LogError(e, "[{Service}.{Method}]: {Error}", nameof(AesEncryptionService), nameof(EncryptString), e.Message);
            return Result.Fail<string>(e.Message);
        }
    }

    public Result<string> DecryptString(string key, string cypherText)
    {
        try
        {
            var iv = new byte[16];
            var buffer = Convert.FromBase64String(cypherText);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
                
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            var decryptedText = streamReader.ReadToEnd();
            logger.LogInformation("[{Service}.{Method}]: Text decrypted successfully", nameof(AesEncryptionService), nameof(DecryptString));
            
            return Result.Ok(decryptedText);
        }
        catch (Exception e)
        {
            logger.LogError(e, "[{Service}.{Method}]: {Error}", nameof(AesEncryptionService), nameof(DecryptString), e.Message);
            return Result.Fail<string>(e.Message);
        }
    }
}