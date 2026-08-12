using System;

namespace latihribbon
{
    public class UserModel
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Role { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int IsActive { get; set; } = 1;
        /// <summary>
        /// Tandai sebagai system/protected account — tidak dapat dihapus, dinonaktifkan, atau diubah role-nya melalui UI.
        /// </summary>
        public int IsSystem { get; set; } = 0;
    }
}

