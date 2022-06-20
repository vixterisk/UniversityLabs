namespace ORM2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PcbsModel : DbContext
    {
        public PcbsModel()
            : base("name=PcbsModel")
        {
        }

        public virtual DbSet<case_form_factor> case_form_factor { get; set; }
        public virtual DbSet<case_information> case_information { get; set; }
        public virtual DbSet<maker> maker { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<case_form_factor>()
                .HasMany(e => e.case_information)
                .WithRequired(e => e.case_form_factor)
                .HasForeignKey(e => e.case_form_factor_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<maker>()
                .HasMany(e => e.case_information)
                .WithRequired(e => e.maker)
                .HasForeignKey(e => e.case_maker_id)
                .WillCascadeOnDelete(false);
        }
    }
}
