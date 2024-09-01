//using Entity.Concrete.Models.Candidates;
//using Entity.Concrete.Models.Contacts;
//using Entity.Concrete.Models.Person;
//using Entity.Concrete.Models.Profession;
//using Entity.Concrete.Models.Qualification;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace MVC_Humsafar_Mubarak.Data
//{
//    public class ApplicationDbContext : IdentityDbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//        {

//        }
//        public DbSet<Candidate> Candidates { get; set; }
//        public DbSet<Contact> Contacts { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // Configure the primary key for Candidate
//            modelBuilder.Entity<Candidate>(b =>
//            {
//                b.HasKey(c => c.Id);

//                // Configure the unique key for Candidate
//                b.HasIndex(c => c.CandidateGuidId).IsUnique();
//                b.HasIndex(c => c.AppUserId).IsUnique();
//                b.Property(c => c.CandidateGuidId).IsRequired();
//                b.Property(c => c.AppUserId).IsRequired();
//            });

//            modelBuilder.Entity<Contact>(b =>
//            {
//                b.HasKey(c => c.Id);
//                b.HasIndex(c => c.ContactGuidId).IsUnique();
//                b.HasIndex(c => c.CandidateGuidId).IsUnique();
//                b.Property(c => c.CandidateId).IsRequired();
//                b.Property(c => c.CandidateGuidId).IsRequired();
//                b.Property(c => c.ContactGuidId).IsRequired();


//                b.ToTable("Contacts");
//            });


//            modelBuilder.Entity<MoreDetail>(b =>
//            {
//                b.HasKey(c => c.Id);
//                b.HasIndex(c => c.MoreDetailGuidId).IsUnique();
//                b.HasIndex(c => c.CandidateGuidId).IsUnique();
//                b.Property(c => c.CandidateId).IsRequired();
//                b.Property(c => c.CandidateGuidId).IsRequired();
//                b.Property(c => c.MoreDetailGuidId).IsRequired();


//                b.ToTable("MoreDetail");
//            });


//            modelBuilder.Entity<Photo>(b =>
//            {
//                b.HasKey(c => c.Id);
//                b.HasIndex(c => c.PhotoGuidId).IsUnique();
//                b.HasIndex(c => c.CandidateGuidId).IsUnique();
//                b.Property(c => c.CandidateId).IsRequired();
//                b.Property(c => c.CandidateGuidId).IsRequired();
//                b.Property(c => c.PhotoGuidId).IsRequired();


//                b.ToTable("Photos");
//            });

//            modelBuilder.Entity<Address>(b =>
//            {
//                b.HasKey(c => c.Id);
//                b.HasIndex(c => c.ContactGuidId).IsUnique();
//                b.Property(c => c.ContactId).IsRequired();
//                b.Property(c => c.ContactGuidId).IsRequired();



//                b.ToTable("Address");
//            });


//            modelBuilder.Entity<Employment>(b =>
//            {
//                b.HasKey(c => c.Id);
//                b.HasIndex(c => c.MoreDetailGuidId).IsUnique();
//                b.Property(c => c.MoreDetailId).IsRequired();
//                b.Property(c => c.MoreDetailGuidId).IsRequired();



//                b.ToTable("Employment");
//            });


//            modelBuilder.Entity<Income>(b =>
//            {
//                b.HasKey(c => c.Id);
//                b.HasIndex(c => c.MoreDetailGuidId).IsUnique();
//                b.Property(c => c.MoreDetailId).IsRequired();
//                b.Property(c => c.MoreDetailGuidId).IsRequired();



//                b.ToTable("Income");
//            });





//            modelBuilder.Entity<Education>(b =>
//            {
//                b.HasKey(c => c.Id);
//                b.HasIndex(c => c.MoreDetailGuidId).IsUnique();
//                b.Property(c => c.MoreDetailId).IsRequired();
//                b.Property(c => c.MoreDetailGuidId).IsRequired();



//                b.ToTable("Education");
//            });

//        }
//    }
//}