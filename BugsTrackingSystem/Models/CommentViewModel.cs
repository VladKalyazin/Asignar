using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class CommentViewModel
    {
        public DateTime CreationDate { get; set; }

        public string CommentText { get; set; }

        public string UserPhoto { get; set; }

        public string UserName { get; set; }
    }
}
