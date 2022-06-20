namespace ORM2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.case_information")]
    public partial class case_information
    {
        [Key]
        public int case_id { get; set; }

        [Required]
        [StringLength(8000)]
        public string case_model { get; set; }
        [ForeignKey("case_form_factor")]
        public int case_form_factor_id { get; set; }

        [ForeignKey("maker")]
        public int case_maker_id { get; set; }

        public virtual case_form_factor case_form_factor { get; set; }

        public virtual maker maker { get; set; }
    }
}
