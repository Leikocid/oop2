using System.ComponentModel.DataAnnotations;

namespace Lab11.Models {
    public class Computer {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { set; get; }

        public byte[] Logo { set; get; }

        [Required]
        public Processor Processor { set; get; }

        [Required]
        public int MemorySize { set; get; }

        [Required]
        public string VideoCard { set; get; }

        [Required]
        public string DiskSize { set; get; }
    }
}
