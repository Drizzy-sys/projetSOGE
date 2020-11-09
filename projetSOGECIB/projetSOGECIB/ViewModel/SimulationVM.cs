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

            /*ParserCSV parser = new ParserCSV();
            parser.ParseCSV();

            List<ITaux> ListTaux = parser.GetListTaux();*/

            List<ITaux> ListTaux = new List<ITaux>();
            ListTaux.Add(new TauxSpot(new DateTime(2020, 07, 02), new DateTime(2020, 08, 02), 0.34844));
            ListTaux.Add(new TauxSpot(new DateTime(2020, 07, 02), new DateTime(2020, 09, 02), 0.43188));
            ListTaux.Add(new TauxSpot(new DateTime(2020, 07, 02), new DateTime(2020, 10, 04), 0.53394));
            ListTaux.Add(new TauxForward(new DateTime(2020, 08, 02), new DateTime(2020, 11, 02), 0.561));
            ListTaux.Add(new TauxForward(new DateTime(2020, 09, 02), new DateTime(2020, 12, 02), 0.617));
            ListTaux.Add(new TauxForward(new DateTime(2020, 10, 04), new DateTime(2021, 01, 03), 0.7075));
            ListTaux.Add(new TauxForward(new DateTime(2020, 11, 02), new DateTime(2021, 02, 02), 0.73));
            ListTaux.Add(new TauxForward(new DateTime(2020, 12, 02), new DateTime(2021, 03, 02), 0.745));
            ListTaux.Add(new TauxForward(new DateTime(2021, 01, 03), new DateTime(2021, 04, 04), 0.765));
            ListTaux.Add(new TauxForward(new DateTime(2021, 02, 02), new DateTime(2021, 05, 03), 0.787));
            ListTaux.Add(new TauxForward(new DateTime(2021, 03, 02), new DateTime(2021, 06, 02), 0.817));
            ListTaux.Add(new TauxForward(new DateTime(2021, 04, 04), new DateTime(2021, 07, 04), 0.85));
            ListTaux.Add(new TauxForward(new DateTime(2021, 07, 04), new DateTime(2021, 10, 03), 0.957));
            ListTaux.Add(new TauxForward(new DateTime(2021, 07, 04), new DateTime(2022, 1, 03), 1.21));
            ListTaux.Add(new TauxForward(new DateTime(2021, 07, 04), new DateTime(2022, 7, 02), 1.495));
            ListTaux.Add(new TauxForward(new DateTime(2022, 07, 02), new DateTime(2025, 7, 02), 1.588));
            ListTaux.Add(new TauxForward(new DateTime(2025, 07, 02), new DateTime(2030, 7, 02), 1.745));
            ListTaux.Add(new TauxForward(new DateTime(2030, 07, 02), new DateTime(2040, 7, 02), 1.887));
            ListTaux.Add(new TauxForward(new DateTime(2040, 07, 02), new DateTime(2050, 7, 02), 2.102));

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
