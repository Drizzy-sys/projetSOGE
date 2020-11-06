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
        YieldCurve yieldCurve;
        public Func<double, string> YFormatter { get; set; }
        public SeriesCollection Series { get; set; }
        public List<string> Labels { get; set; }
        private ChartValues<double> valuesTaux { get; set; }

        public BindableCollection<DateTime> ListMaturities { get; set; }

        private DateTime startDateSwap;
        private double valeurSwap = 0;


        //Fonction permettant de créer tout la simulation 
        public SimulationVM()
        {
            List<TauxSpot> listTaux = createList();
            DateTime startDate = startDateSwap;

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
            maturitySwap = ListMaturities.Last();
            this.CalculSwap();
        }


        private bool isFix = true;
        public bool IsFix
        {
            get { return isFix; }
            set
            {
                SetProperty(ref isFix, value);
            }
        }


        private DateTime maturitySwap;
        public DateTime MaturitySwap
        {
            get { return maturitySwap; }
            set
            {
                SetProperty(ref maturitySwap, value);
            }
        }


        private double nominal = 100;
        public double Nominal
        {
            get { return nominal; }
            set
            {
                SetProperty(ref nominal, value);
            }
        }

        private double tauxFixe = 5;
        public double TauxFixe
        {
            get { return tauxFixe; }
            set
            {
                SetProperty(ref tauxFixe, value);
            }

        }


        private int frequenceFixe = 180;
        public int FrequenceFixe
        {
            get { return frequenceFixe; }
            set
            {
                SetProperty(ref frequenceFixe, value);
            }

        }


        private int frequenceVariable = 180;
        public int FrequenceVariable
        {
            get { return frequenceVariable; }
            set
            {
                SetProperty(ref frequenceVariable, value);
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


        //Crée une liste de TauxSpot à partir d'un fichier csv
        public List<TauxSpot> createList() 
        {
           
            ParserCSV parser = new ParserCSV();
            parser.ParseCSV();

            List<ITaux> ListTaux = parser.GetListTaux();
            startDateSwap = ListTaux.First().StartDate;

            yieldCurve = new YieldCurve();
            yieldCurve.Compute(startDateSwap, ListTaux);
            return yieldCurve.GetList();

        }

        //Fonction calculant la valeur du swap
        public void CalculSwap()
        {
            Swap swap = new Swap(Nominal, startDateSwap, MaturitySwap, FrequenceFixe, FrequenceVariable);

            double variable = swap.CalculJambeVariable(yieldCurve);
            double fixe = swap.CalculJambeFixe(TauxFixe/100);

            ValeurSwap = isFix?(variable - fixe):(fixe - variable);

        }


    }
}
