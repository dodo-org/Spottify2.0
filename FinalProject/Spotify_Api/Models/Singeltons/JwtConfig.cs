namespace Spotify_Api.Models.Singeltons
{
    public class JwtConfig
    {
        private static readonly Lazy<JwtConfig> _instance = new Lazy<JwtConfig>(() => new JwtConfig());

        public static JwtConfig Instance => _instance.Value;

        public string Key { get; private set; }
        public string Issuer { get; private set; }
        public string Audience { get; private set; }
        public string Subject { get; private set; }

        // Private constructor to prevent direct instantiation
        private JwtConfig()
        {
            Key = "DeinGeheimerSchlüsselDerSuperLangSeinMuss";
            Issuer = "DeinIssuer";
            Audience = "DeineAudience";
            Subject = "DeinSubject";
        }
    }
}
