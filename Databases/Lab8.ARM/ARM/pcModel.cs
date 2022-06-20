namespace ARM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class pcModel : DbContext
    {
        internal int cpu_id;

        public pcModel()
            : base("name=pcModel")
        {
        }

        public virtual DbSet<case_form_factor> case_form_factor { get; set; }
        public virtual DbSet<case_information> case_information { get; set; }
        public virtual DbSet<cpu_core_information> cpu_core_information { get; set; }
        public virtual DbSet<cpu_information> cpu_information { get; set; }
        public virtual DbSet<ddr_generation> ddr_generation { get; set; }
        public virtual DbSet<hdd_information> hdd_information { get; set; }
        public virtual DbSet<maker> maker { get; set; }
        public virtual DbSet<pc_information> pc_information { get; set; }
        public virtual DbSet<pc_ram> pc_ram { get; set; }
        public virtual DbSet<pc_video_card> pc_video_card { get; set; }
        public virtual DbSet<ram_information> ram_information { get; set; }
        public virtual DbSet<resolution> resolution { get; set; }
        public virtual DbSet<video_card_information> video_card_information { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<case_form_factor>()
                .HasMany(e => e.case_information)
                .WithRequired(e => e.case_form_factor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ddr_generation>()
                .HasMany(e => e.ram_information)
                .WithOptional(e => e.ddr_generation1)
                .HasForeignKey(e => e.ddr_generation);

            modelBuilder.Entity<maker>()
                .HasMany(e => e.case_information)
                .WithRequired(e => e.maker)
                .HasForeignKey(e => e.case_maker_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<maker>()
                .HasMany(e => e.cpu_core_information)
                .WithOptional(e => e.maker)
                .HasForeignKey(e => e.cpu_core_maker_id);

            modelBuilder.Entity<maker>()
                .HasMany(e => e.cpu_information)
                .WithOptional(e => e.maker)
                .HasForeignKey(e => e.cpu_maker_id);

            modelBuilder.Entity<maker>()
                .HasMany(e => e.hdd_information)
                .WithOptional(e => e.maker)
                .HasForeignKey(e => e.hdd_maker_id);

            modelBuilder.Entity<maker>()
                .HasMany(e => e.ram_information)
                .WithOptional(e => e.maker)
                .HasForeignKey(e => e.ram_maker_id);

            modelBuilder.Entity<maker>()
                .HasMany(e => e.video_card_information)
                .WithOptional(e => e.maker)
                .HasForeignKey(e => e.video_card_maker_id);

            modelBuilder.Entity<pc_information>()
                .HasMany(e => e.pc_ram)
                .WithRequired(e => e.pc_information)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pc_information>()
                .HasMany(e => e.pc_video_card)
                .WithRequired(e => e.pc_information)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ram_information>()
                .HasMany(e => e.pc_ram)
                .WithOptional(e => e.ram_information)
                .WillCascadeOnDelete();
        }
    }
}
