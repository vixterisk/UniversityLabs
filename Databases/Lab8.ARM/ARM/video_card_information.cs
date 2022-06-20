namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pcbs.video_card_information")]
    public partial class video_card_information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public video_card_information()
        {
            pc_video_card = new HashSet<pc_video_card>();
        }

        [Key]
        public int video_card_id { get; set; }

        [StringLength(8000)]
        public string video_card_model { get; set; }

        public int? resolution_id { get; set; }

        public int? video_card_maker_id { get; set; }

        public virtual maker maker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pc_video_card> pc_video_card { get; set; }

        public virtual resolution resolution { get; set; }
    }
}
