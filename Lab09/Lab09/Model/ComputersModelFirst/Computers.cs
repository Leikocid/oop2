//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab09.Model.ComputersModelFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class Computers
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public byte[] Logo { get; set; }
        public int MemorySize { get; set; }
        public string VideoCard { get; set; }
        public string DiskSize { get; set; }
        public int Processor_Id { get; set; }
    
        public virtual Processors Processors { get; set; }
    }
}