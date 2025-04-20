namespace WebApplication1.DTO
{
    public class ResUploadFile
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;
        public string Base64Data { get; set; } = String.Empty;
    }
}
