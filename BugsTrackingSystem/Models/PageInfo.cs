using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }     // номер текущей страницы
        public int PageSize { get; set; }       // кол-во объектов на странице
        public int TotalItems { get; set; }     // всего объектов
        public int TotalPages                   // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    public class IndexViewModel
    {
        public IEnumerable<DefectViewModel> Defects { get; set; }
        public IEnumerable<ProjectViewModel> Projects { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
