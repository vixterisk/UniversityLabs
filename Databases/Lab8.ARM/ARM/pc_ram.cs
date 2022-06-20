namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.pc_ram")]
    public partial class pc_ram
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int pc_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ram_slot_number { get; set; }

        public int? ram_id { get; set; }

        public virtual pc_information pc_information { get; set; }

        public virtual ram_information ram_information { get; set; }
    }
}
