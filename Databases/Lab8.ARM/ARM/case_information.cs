namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.case_information")]
    public partial class case_information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public case_information()
        {
            pc_information = new HashSet<pc_information>();
        }

        [Key]
        public int case_id { get; set; }

        [Required]
        [StringLength(8000)]
        public string case_model { get; set; }

        public int case_form_factor_id { get; set; }

        public int case_maker_id { get; set; }

        public virtual case_form_factor case_form_factor { get; set; }

        public virtual maker maker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pc_information> pc_information { get; set; }
    }
}
