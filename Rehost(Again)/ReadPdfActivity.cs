using System;
using System.Activities;
using System.Drawing;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Rehost_Again_
{
    [ToolboxBitmap(typeof(resfinder),"Resources.pdf.png")]
    public class ReadPdfActivity : CodeActivity<string>
    {
        [RequiredArgument]
        public InArgument<string> PdfFilePath { get; set; }
        
        protected override string Execute(CodeActivityContext context)
        {
            var filePath = PdfFilePath.Get(context);

            // Implement PDF reading logic here
            string pdfText = ReadPdf(filePath); // Implement ReadPdf method to get text from PDF

            return pdfText;
        }

        private string ReadPdf(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new ArgumentException("Invalid file path or file does not exist.");
            }

            try
            {
                using (var reader = new PdfReader(filePath))
                {
                    StringBuilder text = new StringBuilder();
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                    }
                    return text.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to read the PDF file.", ex);
            }
        }
    }
}
