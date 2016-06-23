namespace AsignarDBEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            Defects = new HashSet<Defect>();
            Filters = new HashSet<Filter>();
            ProjectsToUsersBindings = new HashSet<ProjectsToUsersBinding>();
        }

        public int ProjectID { get; set; }

        [StringLength(30)]
        public string ProjectName { get; set; }

        [StringLength(3)]
        public string Prefix { get; set; }

        public DateTime? CreationDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Defect> Defects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Filter> Filters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectsToUsersBinding> ProjectsToUsersBindings { get; set; }
    }
}
