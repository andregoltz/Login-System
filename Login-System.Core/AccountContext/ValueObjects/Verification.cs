using Login_System.Core.SharedContext.ValueObjects;

namespace Login_System.Core.AccountContext.ValueObjects
{
    public class Verification : ValueObject
    {
        public Verification() { }
        public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
        public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
        public DateTime? VerifiedAt { get; set; } = null;
        public bool IsActive => VerifiedAt != null && ExpiresAt == null;

        public void Verify(string code)
        {
            if (IsActive)
                throw new Exception("0x04 - Este item já foi ativado");

            if (ExpiresAt < DateTime.UtcNow)
                throw new Exception("0x05 - Exte código já expirou");

            if(!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("0x06 - Código de verificação inválido");

            ExpiresAt = null;
            VerifiedAt = DateTime.UtcNow;
        }
    }
}
