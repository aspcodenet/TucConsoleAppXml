using System;

namespace TucConsoleAppXml
{
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string BookTitle { get; set; }
        public decimal Salesprice { get; set; }
        public DateTime Published { get; set; }
        public string Description { get; set; }
        public string Externalid { get; set; }
    }
}