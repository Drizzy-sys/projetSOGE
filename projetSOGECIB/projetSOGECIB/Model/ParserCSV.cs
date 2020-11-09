using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using projetSOGECIB.Model.Taux;

namespace projetSOGECIB.Model
{
    class ParserCSV
    {

        private List<string> Type;
        private List<string> StartDate;
        private List<string> EndDate;
        private List<string> Value;

        private List<ITaux> Taux;

        public ParserCSV()
        {
            Type = new List<string>();
            StartDate = new List<string>();
            EndDate = new List<string>();
            Value = new List<string>();
            Taux = new List<ITaux>();
        }
        public void ParseCSV()
        {
            var reader = new StreamReader(File.OpenRead(@"C:\Users\admin\Documents\code\projetSOGE\data2.csv"));
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                Type.Add(values[0]);
                StartDate.Add(values[1]);
                EndDate.Add(values[2]);
                Value.Add(values[3]);
            }

            for(int i=1; i<Type.Count(); i++)
            {
                if(Type[i] == "spot")
                {
                    double.TryParse(Value[i], out double j);
                    Taux.Add(new TauxSpot(ParseDate(StartDate[i]), ParseDate(EndDate[i]), j/100));
                }
                else
                {
                    double.TryParse(Value[i], out double j);
                    Taux.Add(new TauxForward(ParseDate(StartDate[i]), ParseDate(EndDate[i]), j/100));
                }
            }

        }

        public DateTime ParseDate(string date)
        {
            Console.WriteLine(date);
            var tmp = date.Split('/');
            int jour = int.Parse(tmp[0]);
            int mois = int.Parse(tmp[1]);
            int annee = int.Parse(tmp[2]);
            return (new DateTime(annee, mois, jour));
        }

        public List<ITaux> GetListTaux()
        {
            return Taux;
        }

    }
}
