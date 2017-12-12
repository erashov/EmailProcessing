using System.ComponentModel.DataAnnotations;

namespace EmailProcessing.DAL.Entities
{
    public class TypePram
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}