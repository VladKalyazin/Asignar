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
        public int AttachmentID { get; set; }

        [StringLength(100)]
        public string AttachmentLink { get; set; }

        public int DefectID { get; set; }

        public virtual Defect Defect { get; set; }
    }
}
