namespace KeepMoney.Application.Common.Security;

public interface IPasswordHasher
{
    string Hash(string password);
    public bool Verify(string password, string passwordHash);
}

