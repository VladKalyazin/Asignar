namespace AsignarDBEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Defect
    {
        public int DefectID { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        public int ProjectID { get; set; }

        public int AssigneeUserID { get; set; }

        public int DefectPriorityID { get; set; }

        public int DefectStatusID { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual DefectPriority DefectPriority { get; set; }

        public virtual Project Project { get; set; }

        public virtual DefectStatus DefectStatus { get; set; }

        public virtual User User { get; set; }
    }
}
