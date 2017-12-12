using System.ComponentModel.DataAnnotations;

namespace EmailProcessing.DAL.Entities
{
    public class ParamSetting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string FullName { get; set; }
        public int? TypePramId { get; set; }
        public virtual TypePram TypePram { get; set; }
        public int? SettingId { get; set; }
        public virtual Setting Setting { get; set; }

    }
}
