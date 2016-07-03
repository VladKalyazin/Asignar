using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class ProjectExtendedViewModel : IEnumerable
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Prefix { get; set; }

        public int UsersCount { get; set; }

        public int DefectsCount { get; set; }

        public IEnumerable<DefectViewModel> Defects { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
