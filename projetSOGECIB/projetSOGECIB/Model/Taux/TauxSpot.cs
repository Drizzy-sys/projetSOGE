using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSOGECIB.Model.Taux
{
    class TauxSpot : ITaux
    {
        public TauxSpot(DateTime startDate, DateTime maturity, double value)
        {
            this.StartDate = startDate;
            this.Maturity = maturity;
            this.Value = value;
        }

        public TauxSpot(DateTime startDate, ITaux tauxSpot, ITaux tauxForward)
        {
            Debug.Assert(tauxSpot.Maturity == tauxForward.StartDate);
            this.StartDate = startDate;
            this.Maturity = tauxForward.Maturity;
            this.SetValue(tauxSpot, tauxForward);
        }


        public void SetValue(ITaux spot, ITaux forward)
        {
            double taux;
            double nbDaysForward = (forward.Maturity - forward.StartDate).Days / nbDaysPerYear;
            double nbDaysSpot = (spot.Maturity - spot.StartDate).Days / nbDaysPerYear;
            double nbDaysActualSpot = (this.Maturity - this.StartDate).Days / nbDaysPerYear;
            taux = Math.Pow(forward.Value + 1, nbDaysForward) * Math.Pow(1 + spot.Value, nbDaysSpot);
            taux = Math.Pow(taux, 1 / nbDaysActualSpot) - 1;
            this.Value = taux;

        }



    }
}