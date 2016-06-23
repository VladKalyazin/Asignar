namespace AsignarDBEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProjectsToUsersBinding
    {
        [Key]
        public int BindingID { get; set; }

        public int ProjectID { get; set; }

        public int UserID { get; set; }

        public virtual Project Project { get; set; }

        public virtual User User { get; set; }
    }
}
