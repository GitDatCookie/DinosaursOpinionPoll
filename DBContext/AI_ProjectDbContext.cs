using AI_Project.Models;
using AI_Project.Models.OrderModels;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Models.QuestionaireComponentModels.StyleComponents;
using AI_Project.Models.UserModels;
using AI_Project.Models.UserModels.AdminUserComponentModels;
using AI_Project.Models.UserModels.SubjectUserModelComponents;
using Microsoft.EntityFrameworkCore;

namespace AI_Project.DBContext
{
    public class AI_ProjectDbContext : DbContext
    {
        // Base Models
        public DbSet<EmailModel> Emails { get; set; }
        public DbSet<FreeTextModel> FreeTexts { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<VideoModel> Videos { get; set; }

        // Style Models
        public DbSet<NumberFieldStyleModel> NumberStyleModels { get; set; }
        public DbSet<ComponentStyleModel> StyleModels{ get; set; }
        
        // Order Models
        public DbSet<RandomGroupModel> RandomGroups { get; set; }

        // Admin Models
        public DbSet<AdminUserModel> AdminUsers { get; set; }
        public DbSet<QuestionaireModel> QuestionaireModels { get; set; }
        public DbSet<QuestionairePageModel> QuestionairePages{ get; set; }

        // User Models
        public DbSet<AnswerModel> Answers { get; set; }
        public DbSet<SubjectUserModel> SubjectUsers { get; set; }
        public DbSet<AIConversationModel> Conversations { get; set; }
        public DbSet<AIMessageModel> Messages{ get; set; }


        public AI_ProjectDbContext(DbContextOptions<AI_ProjectDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
