namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.pc_information")]
    public partial class pc_information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pc_information()
        {
            pc_ram = new HashSet<pc_ram>();
            pc_video_card = new HashSet<pc_video_card>();
        }

        [Key]
        public int pc_id { get; set; }

        public int? cpu_id { get; set; }

        public int? hdd_id { get; set; }

        public int? case_id { get; set; }

        public virtual case_information case_information { get; set; }

        public virtual cpu_information cpu_information { get; set; }

        public virtual hdd_information hdd_information { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pc_ram> pc_ram { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pc_video_card> pc_video_card { get; set; }
    }
}
