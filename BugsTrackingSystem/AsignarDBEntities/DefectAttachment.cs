namespace AsignarDBEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DefectAttachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AttachmentID { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public int DefectID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual Defect Defect { get; set; }
    }
}
