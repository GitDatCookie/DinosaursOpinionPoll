using AI_Project.DBContext;
using AI_Project.Models.UserModels.AdminUserComponentModels;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AI_Project.Services
{
    public class QuestionaireService : IQuestionaireService
    {
        private readonly AI_ProjectDbContext _dbContext;
        public QuestionaireService(AI_ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateQuestionaire(QuestionaireViewModel questionaireViewModel)
        {
            _dbContext.SaveChanges();
        }

        public void CreateQuestionairePage(Guid questionaireId, QuestionairePageViewModel pageViewModel)
        {
           //Question questionaireModel = _dbContext.QuestionaireModels.Include(i => i.PageList).Where(x => x.QuestionaireId == questionaireId).FirstOrDefault();

           // QuestionairePageModel page = new QuestionairePageModel()
           // {
           //     OrderID = pageViewModel.OrderID,
           //     RandomGroupID = pageViewModel.RandomGroupID,
           //     IsRandomised = pageViewModel.IsRandomised,
           //     OrderedItems = pageViewModel.Order
           // };

           // questionaireModel.PageList.Add(page);
           // _dbContext.SaveChanges();

        }

        public void DeleteQuestionaire(Guid questionaireId)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestionairePage(Guid questionairePageId)
        {
            throw new NotImplementedException();
        }

        public void EditQuestionaire(QuestionaireModel questionaire)
        {
            throw new NotImplementedException();
        }

        public void EditQuestionairePage(QuestionairePageModel questionairePage)
        {
            throw new NotImplementedException();
        }

        public QuestionaireModel GetQuestionaireById(Guid questionaireId)
        {
            QuestionaireModel questionaire = _dbContext.QuestionaireModels
                .Include(rq => rq.RandomComponentGroups)
                .Include(rp => rp.RandomPageGroups)
                .Include(p => p.PageList)
                .Where(x => x.QuestionaireId == questionaireId)
                .FirstOrDefault();
            return questionaire;
        }

        public void GetQuestionairePageById(Guid questionairePageId)
        {
            throw new NotImplementedException();
        }

        public void GetQuestionairePages()
        {
            throw new NotImplementedException();
        }

        public void GetQuestionaires()
        {
            throw new NotImplementedException();
        }

        public QuestionaireViewModel GetShuffleQuestionairePages(string token)
        {
            //QuestionaireModel questionaire = GetQuestionaireByToken(token);

            //List<QuestionairePageModel> initialList = questionaire.PageList.OrderBy(p => p.OrderID).ToList();

            //var randomPagesWithIndex = initialList
            //    .Select((page, index) => new {QuestionairePageModel = page, Index = index})
            //    .Where(w => w.QuestionairePageModel.IsRandomised)
            //    .ToList();

            //var groupedRandomPages = randomPagesWithIndex
            //    .GroupBy(g => g.QuestionairePageModel.RandomGroupID);

            //foreach(var group in groupedRandomPages)
            //{
            //    var indices = group.Select(s => s.Index).ToList();

            //    var groupPages = group.Select(s => s.QuestionairePageModel).ToList();

            //    var shuffledPages = groupPages.OrderBy(o => Guid.NewGuid()).ToList();

            //    for(int i = 0; i < indices.Count; i++)
            //    {
            //        initialList[indices[i]] = shuffledPages[i];
            //    }
            //}

            QuestionaireViewModel questionaireViewModel = new(); // { QuestionaireId = questionaire.QuestionaireId };
            return questionaireViewModel;
        }

        private QuestionaireModel GetQuestionaireByToken(string token)
        {
            QuestionaireModel questionaire = _dbContext.QuestionaireModels.Include(p => p.PageList).Where(x => x.PublicToken == token).FirstOrDefault();
            return questionaire;
        }

        private string CreatePublikToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[16];
                rng.GetBytes(bytes);
                
                string token =  Convert.ToBase64String(bytes)
                    .Replace("/", "_")
                    .Replace("+", "-")
                    .Replace("=", "");

                if(_dbContext.QuestionaireModels.Where(x => x.PublicToken == token).Any())
                {
                    return CreatePublikToken();
                }

                return token;
            }
        }
    }
}
