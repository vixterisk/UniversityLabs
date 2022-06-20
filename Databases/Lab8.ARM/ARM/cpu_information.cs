namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.cpu_information")]
    public partial class cpu_information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cpu_information()
        {
            pc_information = new HashSet<pc_information>();
        }

        [StringLength(8000)]
        public string cpu_model { get; set; }

        public int? core_id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cpu_id { get; set; }

        public int? cpu_maker_id { get; set; }

        public virtual cpu_core_information cpu_core_information { get; set; }

        public virtual maker maker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pc_information> pc_information { get; set; }
    }
}
