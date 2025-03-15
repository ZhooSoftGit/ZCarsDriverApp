using ZCarsDriver.ViewModel;
using ZhooSoft.Core;

namespace ZCarsDriver.Views.Driver;

public partial class OtpVerificationPage : BaseContentPage<OtpVerificationViewModel>
{
	public OtpVerificationPage()
	{
		InitializeComponent();
	}

    private void OtpEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry && entry.Text.Length == 1)
        {
            var parent = entry.Parent as Frame;
            if (parent != null)
            {
                parent.BorderColor = Colors.Black;
            }

            // Move focus to next entry
            var entries = new[] { Entry1, Entry2, Entry3, Entry4 };
            int index = Array.IndexOf(entries, entry);
            if (index < entries.Length - 1)
            {
                entries[index + 1].Focus();
            }
        }
    }
}