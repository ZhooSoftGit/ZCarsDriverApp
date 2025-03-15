using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.UIModel
{
    public partial class Cab : ObservableObject
    {
        [ObservableProperty]
        private string registrationNumber;

        [ObservableProperty]
        private string model;

        [ObservableProperty]
        private bool isSelected;
    }
}
