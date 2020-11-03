using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Prism.Mvvm;
using projetSOGECIB.Model.Taux;
using System.Windows;
using System.ComponentModel;
using System.Windows.Automation.Peers;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Collections.Specialized;
using LiveCharts;
using LiveCharts.Wpf;
using projetSOGECIB.Model;

namespace projetSOGECIB
{

    
    internal class SimulationVM : BindableBase
    {
        public Func<double, string> YFormatter { get; set; }
        public SeriesCollection Series { get; set; }
        public List<string> Labels { get; set; }
        private ChartValues<double> valuesTaux { get; set; }
        private bool isFix;

        public BindableCollection<DateTime> ListMaturities { get; set; }

        private DateTime startDateSwap = new DateTime(2020,1,1);
        private DateTime maturitySwap;
        private double nominal = 100;
        private double tauxFixe = 5;
        private int frequence = 180;
        private double valeurSwap = 0;
        public SimulationVM()
        {
            DateTime startDate = new DateTime(2010, 7, 2);

            List<TauxSpot> listTaux = createList(startDate);
            valuesTaux = new ChartValues<double>();

            Series = new SeriesCollection {
            new LineSeries {
            Title = "Valeurs de taux spot",
            Values = new ChartValues<double>(),
            PointGeometry = null
            }
            };
            Labels = new List<string>();
            YFormatter = value => String.Format("{0:C} %", value.ToString());


            foreach (TauxSpot spt in listTaux)
            {
                Series[0].Values.Add(spt.Value * 100);
                Labels.Add(spt.Maturity.ToShortDateString() );
            }
            ListMaturities = new BindableCollection<DateTime> {startDateSwap.AddYears(1), startDateSwap.AddYears(2), startDateSwap.AddYears(3), startDateSwap.AddYears(5), startDateSwap.AddYears(10), startDateSwap.AddYears(20)};
            maturitySwap = ListMaturities.First();
        }

        public bool IsFix
        {
            get { return isFix; }
            set
            {
                SetProperty(ref isFix, value);
            }
        }

        public DateTime StartDateSwap
        {
            get { return startDateSwap; }
            set
            {
                SetProperty(ref startDateSwap, value);
            }
        }
        
        public DateTime MaturitySwap
        {
            get { return maturitySwap; }
            set
            {
                SetProperty(ref maturitySwap, value);
            }
        }

        public double Nominal
        {
            get { return nominal; }
            set
            {
                SetProperty(ref nominal, value);
            }
        }
        public double TauxFixe
        {
            get { return tauxFixe; }
            set
            {
                SetProperty(ref tauxFixe, value);
            }

        }
        public int Frequence
        {
            get { return frequence; }
            set
            {
                SetProperty(ref frequence, value);
            }

        }
        
        public double ValeurSwap
        {
            get { return valeurSwap; }
            set
            {
                SetProperty(ref valeurSwap, value);
            }

        }



        public ChartValues<double> GetChartValues(List<double> valeurs)
        {
            return new ChartValues<double>(valeurs);
        }


        public List<TauxSpot> createList(DateTime startDate) // Affiner le quadrillage !!
        {
            List<ITaux> ListTaux = new List<ITaux>();

            ListTaux.Add(new TauxSpot(startDate, startDate.AddMonths(3), 0.53394 / 100));
            ListTaux.Add(new TauxSpot(startDate, startDate.AddMonths(1), 0.34844 / 100));
            ListTaux.Add(new TauxSpot(startDate, startDate.AddMonths(2), 0.43188 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(1), startDate.AddMonths(4), 0.5610 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(2), startDate.AddMonths(5), 0.6170 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(3), startDate.AddMonths(6), 0.7075 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(4), startDate.AddMonths(7), 0.7300 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(5), startDate.AddMonths(8), 0.7450 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(6), startDate.AddMonths(9), 0.7650 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(7), startDate.AddMonths(10), 0.7870 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(8), startDate.AddMonths(11), 0.8170 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(9), startDate.AddMonths(12), 0.8500 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(12), startDate.AddMonths(15), 0.9570 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(12), startDate.AddMonths(18), 1.2100 / 100));
            ListTaux.Add(new TauxForward(startDate.AddMonths(12), startDate.AddMonths(24), 1.4950 / 100));



            YieldCurve yieldCurve = new YieldCurve();
            yieldCurve.Compute(startDate, ListTaux);
            return yieldCurve.GetList();

        }

        public double CalculSwap()
        {
            Swap swap = new Swap(Nominal, StartDateSwap, MaturitySwap, TauxFixe, frequence);

            return swap.GetValue();
        }

    }
}
