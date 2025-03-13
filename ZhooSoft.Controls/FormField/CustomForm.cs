using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ZhooSoft.Controls
{
    public class FormControl : VerticalStackLayout
    {
        public static readonly BindableProperty FormFieldsProperty =
            BindableProperty.Create(nameof(FormFields), typeof(ObservableCollection<FormField>), typeof(FormControl), new ObservableCollection<FormField>(), propertyChanged: OnFormFieldsChanged);

        public ObservableCollection<FormField> FormFields
        {
            get => (ObservableCollection<FormField>)GetValue(FormFieldsProperty);
            set => SetValue(FormFieldsProperty, value);
        }

        private static void OnFormFieldsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is FormControl control && newValue is ObservableCollection<FormField> fields)
            {
                control.GenerateForm(fields);
            }
        }

        private void GenerateForm(ObservableCollection<FormField> fields)
        {
            this.Children.Clear(); // Clear previous UI elements

            foreach (var field in fields)
            {
                Label label = new Label
                {
                    Text = field.Label,
                    FontAttributes = FontAttributes.Bold
                };

                View inputControl = null;

                switch (field.Type)
                {
                    case FieldType.Text:
                        inputControl = new SfEntry
                        {
                            Hint = field.Placeholder,
                            SfText = field.Value,
                            BindingContext = field
                        };
                        inputControl.SetBinding(SfEntry.SfTextProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        break;

                    case FieldType.Number:
                        inputControl = new SfEntry
                        {
                            Keyboard = Keyboard.Numeric,
                            Hint = field.Placeholder,
                            SfText = field.Value,
                            BindingContext = field
                        };
                        inputControl.SetBinding(SfEntry.SfTextProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        break;

                    case FieldType.Date:
                        inputControl = new DatePicker
                        {
                            BindingContext = field
                        };
                        inputControl.SetBinding(DatePicker.DateProperty, nameof(FormField.DateValue), BindingMode.TwoWay);
                        break;

                    case FieldType.Picker:
                        var picker = new Picker
                        {
                            ItemsSource = field.Options,
                            BindingContext = field
                        };
                        picker.SetBinding(Picker.SelectedItemProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        inputControl = picker;
                        break;

                    case FieldType.Checkbox:
                        var checkbox = new CheckBox
                        {
                            BindingContext = field
                        };
                        checkbox.SetBinding(CheckBox.IsCheckedProperty, nameof(FormField.IsChecked), BindingMode.TwoWay);
                        inputControl = checkbox;
                        break;
                    case FieldType.RadioButton:
                        var rg = new RadioGroup();
                        rg.ItemsSource = field.Options;
                        rg.SetBinding(RadioGroup.SelectedValueProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        break;
                }

                this.Children.Add(label);
                if (inputControl != null)
                    this.Children.Add(inputControl);
            }
        }
    }

    public enum FieldType
    {
        Text,
        Number,
        Date,
        Picker,
        Checkbox,
        RadioButton
    }

    public partial class FormField : ObservableObject
    {
        [ObservableProperty]
        private string _label;

        [ObservableProperty]
        private FieldType _type;

        [ObservableProperty]
        private string _value;

        [ObservableProperty]
        private bool _isChecked;

        [ObservableProperty]
        private List<string> _options;

        [ObservableProperty]
        private string _placeholder;

        [ObservableProperty]
        private DateTime _dateValue;
    }
}
