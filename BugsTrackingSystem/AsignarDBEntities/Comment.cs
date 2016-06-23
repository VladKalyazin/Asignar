namespace AsignarDBEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int CommentID { get; set; }

        public int DefectID { get; set; }

        public int UserID { get; set; }

        [StringLength(500)]
        public string CommentText { get; set; }

        public virtual Defect Defect { get; set; }

        public virtual User User { get; set; }
    }
}
