using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace projetSOGECIB
{
    internal class MainWindowViewModel : BindableBase, INotifyPropertyChanged
    {

        public MainWindowViewModel()
        {
            SimuVM = new SimulationVM();

        }

        public SimulationVM SimuVM { get; set; }


    }
}
