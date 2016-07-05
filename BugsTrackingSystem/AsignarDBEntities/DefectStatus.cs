namespace AsignarDBEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DefectStatuses")]
    public partial class DefectStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DefectStatus()
        {
            Defects = new HashSet<Defect>();
            Filters = new HashSet<Filter>();
        }

        public int DefectStatusID { get; set; }

        [Required]
        [StringLength(15)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Defect> Defects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Filter> Filters { get; set; }
    }
}
