using System.ComponentModel.DataAnnotations;

namespace EmailProcessing.DAL.Entities
{
    public class TypeRequest
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string FormatMessage { get; set; }
    }
}