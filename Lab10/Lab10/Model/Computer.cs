using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Model {
    public class Computer {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { set; get; }

        public byte[] Logo { set; get; }

        [Required]
        public Processor Processor{ set; get; }

        [Required]
        public int MemorySize { set; get;}

        [Required]
        public string VideoCard { set; get; }

        [Required]
        public string DiskSize { set; get; }
    }
}
