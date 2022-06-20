namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.resolution")]
    public partial class resolution
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public resolution()
        {
            video_card_information = new HashSet<video_card_information>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int resolution_id { get; set; }

        [Column("resolution")]
        [StringLength(8000)]
        public string resolution1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<video_card_information> video_card_information { get; set; }
    }
}
