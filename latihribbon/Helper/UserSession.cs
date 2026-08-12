using System;

namespace latihribbon
{
    public static class UserSession
    {
        public static string CurrentUser { get; set; } = "System";
        public static int CurrentUserId { get; set; } = 0;
        public static string CurrentRole { get; set; } = "Default";

        public static void SetSession(string username, int userId = 0, string role = "")
        {
            CurrentUser = string.IsNullOrWhiteSpace(username) ? "System" : username.Trim();
            CurrentUserId = userId;
            CurrentRole = role ?? string.Empty;
        }

        public static void Clear()
        {
            CurrentUser = "System";
            CurrentUserId = 0;
            CurrentRole = string.Empty;
        }
    }
}
