using System.Collections.Generic;

namespace Polliade
{
    public static class Policy
    {
        public const string SignUpSignIn = "b2c_1_susi";
        public const string EditProfile = "b2c_1_edit_profile";
        public const string ResetPassword = "b2c_1_reset";
    }

    public static class AppSettings
    {
        public static string TenantUrl = "polliadeoauthtest.onmicrosoft.com";
        public static string msalRedirectUrl = $"msal{ApplicationId}://auth";
        public static string TenantRedirectUrl = "polliadeoauthtest.b2clogin.com";
        public static string ApplicationId = "2198fda9-fe5e-46ff-8097-8b4cbc51f865";

        public static IEnumerable<string> Scopes = new[] { Policy.SignUpSignIn, Policy.EditProfile, Policy.ResetPassword };

        public static string GetAuthorityForPolicy(string policy) => $"https://{TenantRedirectUrl}/{TenantUrl}/oauth2/v2.0/authorize";
    }
}
