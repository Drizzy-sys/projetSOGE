using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace projetSOGECIB.Model
{
    class Swap
    {
        private double Nominal;
        private DateTime StartDate;
        private DateTime Maturité;
        private double TauxFixe;
        private double Value;
        private double Frequence;

        public Swap(double nominal, DateTime startDate, DateTime maturité, double tauxFixe, double frequence)
        {
            this.Nominal = nominal;
            this.StartDate = startDate;
            this.Maturité = maturité;
            this.TauxFixe = tauxFixe;
            this.Frequence = frequence;
            this.Value = SetValue();
        }

        private double SetValue()
        {
            double res = this.Nominal;

            for(double i=0; i<(DateTime.Today - StartDate).TotalDays ; i += Frequence)
            {

                res -= Nominal * TauxFixe * CalculActu();
            }
            res -= Nominal * CalculActu();

            return res;
        }

        private double CalculActu()
        {
            double actu = 0;

            return actu;
        }

        public double GetValue()
        {
            return Value;
        }

    }
}
