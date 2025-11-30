using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErrorBlazorLoginApp.Models
{
    public class Error
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

}