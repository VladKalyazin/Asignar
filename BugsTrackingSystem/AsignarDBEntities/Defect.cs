namespace AsignarDBEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Defect
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Defect()
        {
            Comments = new HashSet<Comment>();
            DefectAttachments = new HashSet<DefectAttachment>();
        }

        public int DefectID { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        public int ProjectID { get; set; }

        public int AssigneeUserID { get; set; }

        public int DefectPriorityID { get; set; }

        public int DefectStatusID { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefectAttachment> DefectAttachments { get; set; }

        public virtual DefectPriority DefectPriority { get; set; }

        public virtual Project Project { get; set; }

        public virtual DefectStatus DefectStatus { get; set; }

        public virtual User User { get; set; }
    }
}
