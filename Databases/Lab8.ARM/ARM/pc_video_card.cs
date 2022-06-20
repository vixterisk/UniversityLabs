namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.pc_video_card")]
    public partial class pc_video_card
    {
        [Key]
        [Column(Order = 0)]
        public int pc_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int video_card_slot_number { get; set; }

        public int? video_card_id { get; set; }

        public virtual pc_information pc_information { get; set; }

        public virtual video_card_information video_card_information { get; set; }
    }
}
