using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class TaskViewModel
    {
        public DefectExtendedViewModel DefectInfo { get; set; }

        public NewDefectViewModel ChangeDefect { get; set; }
    }
}
