using MetroDFS.Models.Comman.Enums;
using MetroDFS.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroDFS.Models.Models
{
    public class ChildStation
    {
        public int StationId { get; set; }
        public virtual Station Station { get; set; }
        public int ParentStationId { get; set; }
        public float Distance { get; set; }
        public virtual Station ParentStation { get; set; }
        public Lines Line { get; set; }
        public string LineDirection { get; set; }
    }
}
