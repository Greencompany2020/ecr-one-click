using FluentResults;

namespace EcrOneClick.Shared.Encryption.Abstract;

public interface IEncryptionService
{ 
    /// <param name="key">Debe ser un string de 16, 24 o 36 carateres</param>
    Result<string> EncryptString(string key, string plaintText);

    /// <param name="key">Debe ser un string de 16, 24 o 36 carateres</param>
    Result<string> DecryptString(string key, string cypherText);
}