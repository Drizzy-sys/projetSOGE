using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projetSOGECIB.Model.Interpolation;
using projetSOGECIB.Model.Taux;
using Xamarin.Forms.Internals;

namespace projetSOGECIB.Model
{
    class Swap
    {
        private double Nominal;
        private DateTime StartDate;
        private DateTime Maturity;
        private double FrequenceFixe;
        private double FrequenceVariable;
        private double tauxActualisation = 2.5 / 100;


        public Swap(double nominal, DateTime startDate, DateTime maturity, double frequenceFixe, double frequenceVariable)
        {
            this.Nominal = nominal;
            this.StartDate = startDate;
            this.Maturity = maturity;
            this.FrequenceFixe = frequenceFixe;
            this.FrequenceVariable = frequenceVariable;
        }

        public double GetPeriodeActualisation(double frequence)
        {
            double res = (DateTime.Today - StartDate).TotalDays;

            while (res > frequence)
            {
                res -= frequence;
            }

            return res;
        }

        public double CalculJambeFixe(double tauxFixe)
        {
            double res = 0;
            double fraction = GetPeriodeActualisation(FrequenceFixe) / FrequenceFixe;
            double nbJours = (Maturity - DateTime.Today).TotalDays;
            

            while (nbJours >= FrequenceFixe)
            {
                res += Nominal * tauxFixe * Math.Pow(1 + tauxActualisation, -fraction);
                fraction++;
                nbJours -= FrequenceFixe;
            }

            res += Nominal * Math.Pow(1 + tauxActualisation, -fraction);

            return res;

        }
        
        public double CalculJambeVariable(YieldCurve curve)
        {
            InterpolationLineaire inter = new InterpolationLineaire(curve);

            double nbJours = GetPeriodeActualisation(FrequenceVariable);
            double fraction = nbJours / FrequenceVariable;
            
            TauxSpot first = new TauxSpot(StartDate, DateTime.Today.AddDays(nbJours), inter.CalculValue(DateTime.Today.AddDays(nbJours)));
            nbJours += FrequenceVariable;
            TauxSpot second = new TauxSpot(StartDate, DateTime.Today.AddDays(nbJours), inter.CalculValue(DateTime.Today.AddDays(nbJours)));
            nbJours += FrequenceVariable;
            TauxForward forward = new TauxForward(first, second);

            double res = Nominal * forward.Value * Math.Pow(1 + tauxActualisation, -fraction);


            while ((Maturity - DateTime.Today.AddDays(nbJours)).TotalDays > 0)
            {
                first = second;
                second = new TauxSpot(StartDate, DateTime.Today.AddDays(nbJours), inter.CalculValue(DateTime.Today.AddDays(nbJours)));
                forward = new TauxForward(first, second);
                fraction++;
                res += Nominal * forward.Value * Math.Pow(1 + tauxActualisation, -fraction);
                nbJours += FrequenceVariable;
            }

            res += Nominal * Math.Pow(1 + tauxActualisation, -fraction);



            return res;
        }

    }
}
