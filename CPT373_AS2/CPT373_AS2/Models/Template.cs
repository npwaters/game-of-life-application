using System;
using System.Data.Entity;

namespace CPT373_AS2.Models
{
    public class Template
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Cell[][] Cells { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class TemplateDBContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }
    }
}
