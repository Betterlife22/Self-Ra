
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Selfra_Entity.Model;

namespace Selfra_Repositories.Base
{
    public class SelfraDBContext : IdentityDbContext <ApplicationUser, ApplicationRole, Guid,ApplicationUserClaims, ApplicationUserRoles,ApplicationUserLogins ,ApplicationRoleClaims, ApplicationUserTokens>
    {
        public SelfraDBContext(DbContextOptions<SelfraDBContext> options) : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Total)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Package>()
                .Property(t => t.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Course>()
             .HasOne(c => c.Category)
             .WithMany(cat => cat.Courses)  // <-- Specify the collection navigation property here!
             .HasForeignKey(c => c.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Creator)
                .WithMany()
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ForumComment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ForumComment>()
                .HasOne(c => c.Post)
                .WithMany()
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MentorContact>()
             .HasOne(mc => mc.Mentor)
             .WithMany()
             .HasForeignKey(mc => mc.MentorId)
             .OnDelete(DeleteBehavior.Restrict); // Prevent cascade

            modelBuilder.Entity<MentorContact>()
                .HasOne(mc => mc.User)
                .WithMany()
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(mc => mc.Conversation)
                .WithMany()
                .HasForeignKey(mc => mc.ConversationId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>()
                .HasOne(mc => mc.Sender)
                .WithMany()
                .HasForeignKey(mc => mc.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostVote>()
                .HasOne(mc => mc.User)
                .WithMany()
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PostVote>()
                .HasOne(mc => mc.Post)
                .WithMany()
                .HasForeignKey(mc => mc.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuizResult>()
                .HasOne(mc => mc.User)
                .WithMany()
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<QuizResult>()
                .HasOne(mc => mc.Quiz)
                .WithMany()
                .HasForeignKey(mc => mc.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReadingProgress>()
                .HasOne(mc => mc.User)
                .WithMany()
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ReadingProgress>()
                .HasOne(mc => mc.Book)
                .WithMany()
                .HasForeignKey(mc => mc.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserCourseProgress>()
                .HasOne(mc => mc.User)
                .WithMany()
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserCourseProgress>()
                .HasOne(mc => mc.Course)
                .WithMany()
                .HasForeignKey(mc => mc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserLessonProgress>()
                .HasOne(mc => mc.User)
                .WithMany()
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserLessonProgress>()
                .HasOne(mc => mc.Lesson)
                .WithMany()
                .HasForeignKey(mc => mc.LessonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserNewsPreference>()
                .HasOne(mc => mc.User)
                .WithMany()
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserNewsPreference>()
                .HasOne(mc => mc.NewsCategory)
                .WithMany()
                .HasForeignKey(mc => mc.NewTagId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserPackage>()
                .HasOne(mc => mc.User)
                .WithMany()
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserPackage>()
                .HasOne(mc => mc.Package)
                .WithMany()
                .HasForeignKey(mc => mc.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationParticipant> ConversationParticipants { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<FoodDetail> FoodDetails { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<MeditationLessonContent> MeditationLessonContents { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<MentorContact> MentorContacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostVote> PostVotes { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
        public DbSet<ReadingProgress> ReadingProgresses { get; set; }
        public DbSet<SportsActivity> SportsActivities { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserCourseProgress> UserCourseProgresses { get; set; }
        public DbSet<UserLessonProgress> UserLessonProgresses { get; set; }
        public DbSet<UserNewsPreference> UserNewsPreferences { get; set; }
        public DbSet<UserPackage> UserPackages { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public virtual DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
        public virtual DbSet<ApplicationUserClaims> ApplicationUserClaims => Set<ApplicationUserClaims>();
        public virtual DbSet<ApplicationUserRoles> ApplicationUserRoles => Set<ApplicationUserRoles>();
        public virtual DbSet<ApplicationUserLogins> ApplicationUserLogins => Set<ApplicationUserLogins>();
        public virtual DbSet<ApplicationRoleClaims> ApplicationRoleClaims => Set<ApplicationRoleClaims>();
        public virtual DbSet<ApplicationUserTokens> ApplicationUserTokens => Set<ApplicationUserTokens>();
    }
}
