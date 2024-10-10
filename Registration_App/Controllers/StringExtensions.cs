using System;
using System.Security.Cryptography;
using System.Text;

public class StringExtensions : IDisposable
{
    private RSACryptoServiceProvider _rsa;

    public StringExtensions()
    {
        _rsa = new RSACryptoServiceProvider(2048);
    }

    public string GetPublicKey()
    {
        return Convert.ToBase64String(_rsa.ExportRSAPublicKey());
    }

    public string Encrypt(string plainText)
    {
        var messageBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = _rsa.Encrypt(messageBytes, RSAEncryptionPadding.OaepSHA1);
        return Convert.ToBase64String(encryptedBytes);
    }

    public string Decrypt(string encryptedText)
    {
        var encryptedBytes = Convert.FromBase64String(encryptedText);
        var decryptedBytes = _rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA1);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    // Clean up resources
    public void Dispose()
    {
        _rsa?.Dispose();
    }
}


/*
 
using System;
using System.Security.Cryptography;
using System.Text;
namespace CustomAuth.Controllers
{
    public class RSAHelper
    {
        private RSACryptoServiceProvider _rsa;
        public RSAHelper()
        {
            _rsa = new RSACryptoServiceProvider(2048);
        }
        // Export public key
        public string GetPublicKey()
        {
            return Convert.ToBase64String(_rsa.ExportRSAPublicKey());
        }
        // Encrypt a message using the public key
        public string Encrypt(string plainText)
        {
            var messageBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = _rsa.Encrypt(messageBytes, RSAEncryptionPadding.OaepSHA1);
            return Convert.ToBase64String(encryptedBytes);
        }
        // Decrypt a message using the private key
        public string Decrypt(string encryptedText)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var decryptedBytes = _rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA1);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        // Clean up resources
        public void Dispose()
        {
            _rsa?.Dispose();
        }
    }
}







 
 
 
 
 
 */