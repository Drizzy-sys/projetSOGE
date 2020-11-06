using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace projetSOGECIB
{
    internal class MainWindowViewModel : BindableBase, INotifyPropertyChanged
    {

        public MainWindowViewModel()
        {
            SimuVM = new SimulationVM();
            CalculCommand = new DelegateCommand(CalculSwap);

        }

        public SimulationVM SimuVM { get; set; }
        public DelegateCommand CalculCommand { get; private set; }


        public void CalculSwap()
        {
            SimuVM.CalculSwap();
        }


    }
}
