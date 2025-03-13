using CommunityToolkit.Mvvm.ComponentModel;
using ZCarsDriver.UIHelper;

namespace ZCarsDriver.UIModel
{
    public partial class CheckListItem : ObservableObject
    {
        #region Fields

        [ObservableProperty]
        private bool _isCompleted;

        [ObservableProperty]
        private string _itemName;

        #endregion

        #region Properties

        public CheckListType CheckListType { get; set; }

        public bool FrontOnly { get; set; }

        public bool IsDocument { get; set; }

        public bool IsForm { get; set; }

        #endregion
    }
}
