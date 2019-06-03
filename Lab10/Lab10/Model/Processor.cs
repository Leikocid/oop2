using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Model {

    public class Processor {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public int CoresCount { get; set; }

        public virtual ICollection<Computer> Computer { get; set; }

        public Processor() {
            this.Computer = new HashSet<Computer>();
        }

        public static void CopyProperties(Processor source, Processor destination) {
            destination.Id = source.Id;
            destination.Model = source.Model;
            destination.Developer = source.Developer;
            destination.CoresCount = source.CoresCount;
        }
    }
}

