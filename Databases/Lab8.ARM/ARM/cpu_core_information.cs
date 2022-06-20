namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.cpu_core_information")]
    public partial class cpu_core_information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cpu_core_information()
        {
            cpu_information = new HashSet<cpu_information>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int core_id { get; set; }

        [StringLength(8000)]
        public string core_name { get; set; }

        public int? cpu_core_maker_id { get; set; }

        public virtual maker maker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cpu_information> cpu_information { get; set; }
    }
}
