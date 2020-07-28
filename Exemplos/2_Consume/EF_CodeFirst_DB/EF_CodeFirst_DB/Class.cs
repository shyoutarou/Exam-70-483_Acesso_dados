namespace EF_CodeFirst_DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Class")]
    public partial class Class
    {
        public int ClassID { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        public int? StudentID { get; set; }

        public virtual Student Student { get; set; }
    }
}
