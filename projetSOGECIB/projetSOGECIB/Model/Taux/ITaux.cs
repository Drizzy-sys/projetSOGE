﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSOGECIB.Model.Taux
{
    class ITaux
    {
        public double nbDaysPerYear = 365;
        public DateTime Maturity { get; set; }
        public DateTime StartDate { get; set; }
        public double Value { get; set; }


    }
}
