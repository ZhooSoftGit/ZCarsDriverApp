using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.UIModel
{
    public partial class CheckListItem : ObservableObject
    {
        [ObservableProperty]
        private string _itemName;

        [ObservableProperty]
        private bool _isCompleted;
    }
}
