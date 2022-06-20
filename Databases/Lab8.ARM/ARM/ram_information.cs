namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.ram_information")]
    public partial class ram_information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ram_information()
        {
            pc_ram = new HashSet<pc_ram>();
        }

        [Key]
        public int ram_id { get; set; }

        [StringLength(8000)]
        public string ram_model { get; set; }

        public int? ddr_generation { get; set; }

        public int? ram_memory_size { get; set; }

        public int? ram_maker_id { get; set; }

        public virtual ddr_generation ddr_generation1 { get; set; }

        public virtual maker maker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pc_ram> pc_ram { get; set; }
    }
}
