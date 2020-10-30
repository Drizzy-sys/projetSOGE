using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ProjetSGCIBconsole.src.taux
{
    class TauxForward : ITaux
    {

        public TauxForward(TauxSpot taux1, TauxSpot taux2)
        {
            this.StartDate = taux1.Maturity;
            this.Maturity = taux2.Maturity;
            this.SetValue2(taux1, taux2);
        }

        public TauxForward(DateTime startDate, DateTime maturity, double value)
        {
            this.StartDate = startDate;
            this.Maturity = maturity;
            this.Value = value;
        }

        public void SetValue(TauxSpot taux1, TauxSpot taux2)
        {
            double d1 = (taux1.Maturity - taux1.StartDate).Days / nbDaysPerYear;
            double d2 = (taux2.Maturity - taux2.StartDate).Days / nbDaysPerYear;

            double taux = ((1 + taux2.Value * d2) / (1 + taux1.Value * d1) - 1)/(d2 - d1);
            this.Value = taux;
        }

        public void SetValue2(TauxSpot taux1, TauxSpot taux2)
        {
            double nbDays1 = (taux1.Maturity - taux1.StartDate).Days;
            double nbDays2 = (taux2.Maturity - taux2.StartDate).Days;

            double taux = Math.Pow((1 + taux2.Value), nbDays2/this.nbDaysPerYear) / Math.Pow((1 + taux1.Value), nbDays1/this.nbDaysPerYear);
            this.Value = Math.Pow(taux, 1 / (nbDays2/ this.nbDaysPerYear - nbDays1/ this.nbDaysPerYear)) - 1;
        }

    }
}
