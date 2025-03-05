using Syncfusion.Maui.Toolkit.TextInputLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooSoft.Controls
{
    public class SfEntry : SfTextInputLayout
    {
        public Entry _entry { get; set; }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(SfEntry), Keyboard.Default);

        public static readonly BindableProperty SfTextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(SfEntry), string.Empty, BindingMode.TwoWay);

        public string SfText
        {
            get => (string)GetValue(SfTextProperty);
            set => SetValue(SfTextProperty, value);
        }


        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public SfEntry()
        {
            _entry = new Entry();
            ApplyCustomStyle();
            _entry.SetBinding(Entry.TextProperty, new Binding(nameof(SfText), source: this, mode: BindingMode.TwoWay));
            _entry.SetBinding(Entry.KeyboardProperty, new Binding(nameof(Keyboard), source: this));
            Content = _entry;
        }

        private void ApplyCustomStyle()
        {
            this.Background = Colors.White;
            this.ContainerBackground = Colors.White;
            this.ContainerType = ContainerType.Outlined;
        }
    }
}
