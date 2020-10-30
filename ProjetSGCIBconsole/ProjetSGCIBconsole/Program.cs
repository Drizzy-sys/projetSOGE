using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using ProjetSGCIBconsole.src;
using ProjetSGCIBconsole.src.taux;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProjetSGCIBconsole
{
    class Program
    {

        static void Main(string[] args)
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




            RateCurve curve = new RateCurve();
            curve.Compute(ListTaux); 
            foreach(TauxSpot spt in curve.GetList())
            {
                Console.WriteLine($"Date départ : {spt.StartDate} ; Maturité : {spt.Maturity} ; Valeur : {spt.Value * 100}%");
            }

            Console.Read();

        }
    }
}
