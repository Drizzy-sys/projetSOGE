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

        private DateTime maturitySwap = DateTime.Today;
        public SimulationVM()
        {
            List<TauxSpot> listTaux = createList();
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
        }

        public bool IsFix
        {
            get { return isFix; }
            set
            {
                SetProperty(ref isFix, value);
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

        public ChartValues<double> GetChartValues(List<double> valeurs)
        {
            return new ChartValues<double>(valeurs);
        }


        public List<TauxSpot> createList() // Affiner le quadrillage !!
        {
            List<ITaux> ListTaux = new List<ITaux>();

            ListTaux.Add(new TauxSpot(DateTime.Today.AddMonths(3), 0.53394 / 100));
            ListTaux.Add(new TauxSpot(DateTime.Today.AddMonths(1), 0.34844 / 100));
            ListTaux.Add(new TauxSpot(DateTime.Today.AddMonths(2), 0.43188 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(1), DateTime.Today.AddMonths(4), 0.5610 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(2), DateTime.Today.AddMonths(5), 0.6170 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(3), DateTime.Today.AddMonths(6), 0.7075 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(4), DateTime.Today.AddMonths(7), 0.7300 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(5), DateTime.Today.AddMonths(8), 0.7450 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(6), DateTime.Today.AddMonths(9), 0.7650 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(7), DateTime.Today.AddMonths(10), 0.7870 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(8), DateTime.Today.AddMonths(11), 0.8170 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(9), DateTime.Today.AddMonths(12), 0.8500 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(12), DateTime.Today.AddMonths(15), 0.9570 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(12), DateTime.Today.AddMonths(18), 1.2100 / 100));
            ListTaux.Add(new TauxForward(DateTime.Today.AddMonths(12), DateTime.Today.AddMonths(24), 1.4950 / 100));



            YieldCurve yieldCurve = new YieldCurve();
            yieldCurve.Compute(ListTaux);
            return yieldCurve.GetList();

        }

    }
}
