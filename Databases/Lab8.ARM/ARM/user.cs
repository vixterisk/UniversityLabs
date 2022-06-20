namespace ARM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("users.user")]
    public partial class user
    {
        public int id { get; set; }

        [Required]
        [StringLength(8000)]
        public string login { get; set; }

        [Required]
        [StringLength(8000)]
        public string password { get; set; }

        [Required]
        [StringLength(8000)]
        public string salt { get; set; }

        [Column(TypeName = "date")]
        public DateTime reg_date { get; set; }

        public bool? is_admin { get; set; }
    }
}
