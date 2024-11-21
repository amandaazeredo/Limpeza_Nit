using System.IO;

namespace API.Domain.Model
{
    public class ExcelInfo
    {
        public MemoryStream Stream { get; set; } = new MemoryStream();
        public string FileName { get; set; }

        public ExcelInfo(IFormFile file)
        {
            file.CopyTo(Stream);
            FileName = file.FileName.Replace(".xlsx","");
        }
    }
}
