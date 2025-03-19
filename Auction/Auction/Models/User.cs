namespace Auction.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int UserID { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        [StringLength(30)]
        public string Password { get; set; }

        [StringLength(30)]
        public string Role { get; set; }
    }
}
