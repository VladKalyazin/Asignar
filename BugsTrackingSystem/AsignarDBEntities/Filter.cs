namespace AsignarDBEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Filter
    {
        public int FilterID { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        public int? ProjectID { get; set; }

        public int? AssigneeUserID { get; set; }

        public int? DefectPriorityID { get; set; }

        public int? DefectStatusID { get; set; }

        public virtual DefectPriority DefectPriority { get; set; }

        public virtual DefectStatus DefectStatus { get; set; }

        public virtual Project Project { get; set; }

        public virtual User User { get; set; }
    }
}
