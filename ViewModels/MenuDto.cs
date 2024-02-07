namespace SampleApp.ViewModels
{
    public class MenuDto
    {
        public short Id { get; set; }
        public short? ParentId { get; set; }

        public string Name { get; set; } = null!;

        public string? Url { get; set; }
        public short? Sequence { get; set; }
    }
}
