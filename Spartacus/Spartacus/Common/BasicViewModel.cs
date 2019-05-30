using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Spartacus.Common
{
    public class BasicViewModel : Screen, IWikiDocumentation
    {
        public string WikiUrl { get; set; } = "https://github.com/PROXiCiDE/Spartacus/wiki";
    }
}
