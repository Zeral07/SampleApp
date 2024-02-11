using System.ComponentModel.DataAnnotations;

namespace SampleApp.ViewModels
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nama Lengkap")]
        public string FullName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        [Display(Name = "Jabatan")]
        public string Position { get; set; } = "";
        public short RowStatus { get; set; }

        public string? CreatedBy { get; set; } = "";

        public DateTime CreatedTime { get; set; }

        public string? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedTime { get; set; }
    }
}
