using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousingCoo.Presentation
{

    public class MasterMenuPageMenuItem
    {
        public MasterMenuPageMenuItem()
        {
            TargetType = typeof(MasterMenuPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}