using System.ComponentModel.DataAnnotations;

namespace SampleApp.Parameters
{
    public class UserParameter
    {
        public int Id { get; set; }
        [Display(Name = "Nama Lengkap")]
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        [Display(Name = "Jabatan")]
        public string Position { get; set; } = "";
    }
}
