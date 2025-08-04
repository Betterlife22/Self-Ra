
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Selfra_Entity.Entity;
using Selfra_Entity.Model;
using System.Reflection.Emit;

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
            //modelBuilder.Entity<ApplicationUserLogins>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            //modelBuilder.Entity<ApplicationUserTokens>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            //modelBuilder.Entity<ApplicationUserRoles>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<ApplicationUserRoles>(entity =>
            {
                entity.HasKey(ar => new { ar.UserId, ar.RoleId }); // Khóa composite
            });


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
        public DbSet<FcmToken> FcmTokens { get; set; }
        public DbSet<ZaloGroup> ZaloGroups { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public virtual DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
        public virtual DbSet<ApplicationUserClaims> ApplicationUserClaims => Set<ApplicationUserClaims>();
        public virtual DbSet<ApplicationUserRoles> ApplicationUserRoles => Set<ApplicationUserRoles>();
        public virtual DbSet<ApplicationUserLogins> ApplicationUserLogins => Set<ApplicationUserLogins>();
        public virtual DbSet<ApplicationRoleClaims> ApplicationRoleClaims => Set<ApplicationRoleClaims>();
        public virtual DbSet<ApplicationUserTokens> ApplicationUserTokens => Set<ApplicationUserTokens>();
    }
}
