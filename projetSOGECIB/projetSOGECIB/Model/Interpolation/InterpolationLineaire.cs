using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projetSOGECIB.Model.Taux;

namespace projetSOGECIB.Model.Interpolation
{
    class InterpolationLineaire
    {
        private IEnumerable<TauxSpot> Curve;

        public InterpolationLineaire(YieldCurve curve) => Curve = curve.GetList().ToList();

        public double CalculValue(DateTime date)
        {
            TauxSpot previous = Curve.ElementAt(0);
            TauxSpot next = Curve.ElementAt(1);
            int i = 2;
            while((next.Maturity - date).TotalDays < 0)
            {
                previous = next;
                next = Curve.ElementAt(i);
                i++;
            }
            double coeff = (next.Value - previous.Value) / ((next.Maturity - previous.Maturity).TotalDays);

            double result = coeff * (date - previous.Maturity).TotalDays + previous.Value;
            return result;
        }


    }
}
