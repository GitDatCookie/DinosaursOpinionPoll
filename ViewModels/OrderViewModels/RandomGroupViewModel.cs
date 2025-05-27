using AI_Project.Enums;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.OrderModels
{
    public class RandomGroupViewModel
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public ERandomGroupType RandomGroupType { get; set; }
    }
}
