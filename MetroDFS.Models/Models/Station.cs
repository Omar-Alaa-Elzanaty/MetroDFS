using MetroDFS.Models.Comman.Enums;
using MetroDFS.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroDFS.Services.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<ChildStation> Childs { get; set; }
    }
}
