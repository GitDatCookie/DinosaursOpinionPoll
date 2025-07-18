﻿using AI_Project.Enums;
using AI_Project.Models.UserModels.SubjectUserModelComponents;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.UserModels
{
    public class SubjectUserModel
    {
        [Key]
        public Guid UserId { get; set; }
        public bool IsTreatmentGroup { get; set; }

        #region ForeignTables
        public ICollection<AnswerModel> Answers { get; set; } = [];
        public ICollection<AIConversationModel> Conversations { get; set; } = [];
        #endregion
    }
}
