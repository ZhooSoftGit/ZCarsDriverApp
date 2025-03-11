using System.Text.Json;
using System.Text.Json.Serialization;

using ZhooSoft.Core.Session;

namespace ZCarsDriver.Services.Session
{
    public class UserSessionManager : IUserSessionManager
    {
        private const string UserSessionKey = "UserSession";
        private const string RefreshTokenKey = "RefreshToken";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Converters = { new JsonStringEnumConverter() }, // Serialize enums as strings
            WriteIndented = true
        };

        public async Task SaveUserSessionAsync(UserSession session)
        {
            // Save non-sensitive data in Preferences
            var json = JsonSerializer.Serialize(session, JsonOptions);
            Preferences.Set(UserSessionKey, json);

            // Save refresh token in SecureStorage
            if (!string.IsNullOrEmpty(session.RefreshToken))
            {
                await SecureStorage.SetAsync(RefreshTokenKey, session.RefreshToken);
            }
        }

        public async Task<UserSession?> GetUserSessionAsync()
        {
            var json = Preferences.Get(UserSessionKey, string.Empty);
            if (string.IsNullOrEmpty(json)) return null;

            var session = JsonSerializer.Deserialize<UserSession>(json, JsonOptions);
            if (session != null)
            {
                session.RefreshToken = await SecureStorage.GetAsync(RefreshTokenKey) ?? string.Empty;
            }
            return session;
        }

        public void ClearSession()
        {
            Preferences.Remove(UserSessionKey);
            SecureStorage.Remove(RefreshTokenKey);
        }
    }

}
