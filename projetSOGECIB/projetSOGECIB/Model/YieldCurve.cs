using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projetSOGECIB.Model.Taux;

namespace projetSOGECIB.Model
{
    class YieldCurve
    {

        private List<TauxSpot> Spots;

        public YieldCurve()
        {
            this.Spots = new List<TauxSpot>();
        }

        public void Compute(DateTime startDate, List<ITaux> taux)
        {

            List<TauxSpot> spotTampon = new List<TauxSpot>();

            foreach (ITaux tx in taux)
            {
                if (tx.StartDate == startDate)
                {
                    spotTampon.Add(new TauxSpot(startDate, tx.Maturity, tx.Value));
                }
                else
                {
                    spotTampon.Add(new TauxSpot(startDate, spotTampon.Find(x => x.Maturity == tx.StartDate), tx));
                }
            }
            IEnumerable<TauxSpot> EnumerableSpot = spotTampon.OrderBy(x => x.Maturity);
            SetList(EnumerableSpot);
        }

        private void SetList(IEnumerable<TauxSpot> enumSpot)
        {
            foreach (TauxSpot spt in enumSpot)
            {
                this.Spots.Add(spt);
            }
        }

        public List<TauxSpot> GetList()
        {
            return Spots;
        }

    }
}
