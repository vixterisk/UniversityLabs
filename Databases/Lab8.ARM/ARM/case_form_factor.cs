namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.case_form_factor")]
    public partial class case_form_factor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public case_form_factor()
        {
            case_information = new HashSet<case_information>();
        }

        [Key]
        public int case_form_factor_id { get; set; }

        [Required]
        [StringLength(8000)]
        public string form_factor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<case_information> case_information { get; set; }
    }
}
