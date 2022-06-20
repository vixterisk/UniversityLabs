namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.hdd_information")]
    public partial class hdd_information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hdd_information()
        {
            pc_information = new HashSet<pc_information>();
        }

        [Key]
        public int hdd_id { get; set; }

        [StringLength(8000)]
        public string hdd_model { get; set; }

        public int? hdd_memory_size { get; set; }

        public int? hdd_maker_id { get; set; }

        public virtual maker maker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pc_information> pc_information { get; set; }
    }
}
