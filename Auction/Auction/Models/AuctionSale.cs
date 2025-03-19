namespace Auction.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class AuctionSale
    {
        [Key]
        public int SaleId { get; set; }

        public DateTime? DateSale { get; set; }

        public double? StartCost { get; set; }

        public double? EndCost { get; set; }

        [StringLength(30)]
        public string SignSale { get; set; }

        [StringLength(30)]
        public string FamBuyer { get; set; }

        public int ObjectId { get; set; }

        public virtual ObjectSell ObjectSell { get; set; }
    }
}
