using System;
using System.Collections.Generic;
using System.Text;

namespace DapperCrud
{
    public class Civilizations
    {
        public Int64 CivilizationId { get; set; }
        public Int64 DisplayNameId { get; set; }
        public Int64 RolloverNameId { get; set; }
        public String ShieldTexture { get; set; }
        public String ShieldGreyTexture { get; set; }
        public String Age0 { get; set; }
        public String Age1 { get; set; }
        public String Age2 { get; set; }
        public String Age3 { get; set; }
        public Int64 StorehouseTechId { get; set; }
    }
}
