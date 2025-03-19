namespace Auction.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ObjectSell")]
    public partial class ObjectSell
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ObjectSell()
        {
            AuctionSales = new HashSet<AuctionSale>();
        }

        [Key]
        public int ObjectId { get; set; }

        [StringLength(30)]
        public string ObjectName { get; set; }

        public int? ReleaseYear { get; set; }

        [StringLength(30)]
        public string ObjectOwner { get; set; }

        public DateTime? DateOfAdmission { get; set; }

        public double? EstimatedCost { get; set; }

        [StringLength(255)]
        public string Discription { get; set; }

        public int TypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuctionSale> AuctionSales { get; set; }

        public virtual TypeObject TypeObject { get; set; }
    }
}
