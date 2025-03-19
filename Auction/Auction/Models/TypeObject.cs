namespace Auction.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TypeObject")]
    public partial class TypeObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeObject()
        {
            ObjectSells = new HashSet<ObjectSell>();
        }

        [Key]
        public int TypeId { get; set; }

        [StringLength(30)]
        public string TypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectSell> ObjectSells { get; set; }
    }
}
