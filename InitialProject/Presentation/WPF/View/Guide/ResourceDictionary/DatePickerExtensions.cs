using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace InitialProject.Presentation.WPF.View.Guide.ResourceDictionary
{
    public static class DatePickerExtensions
    {
        public static readonly DependencyProperty BlackoutDatesProperty = DependencyProperty.RegisterAttached(
        "BlackoutDates",
        typeof(IEnumerable<DateTime>),
        typeof(DatePickerExtensions),
        new PropertyMetadata(null, OnBlackoutDatesChanged));

        public static void SetBlackoutDates(DatePicker element, IEnumerable<DateTime> value)
        {
            element.SetValue(BlackoutDatesProperty, value);
        }

        public static IEnumerable<DateTime> GetBlackoutDates(DatePicker element)
        {
            return (IEnumerable<DateTime>)element.GetValue(BlackoutDatesProperty);
        }

        private static void OnBlackoutDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DatePicker datePicker && e.NewValue is IEnumerable<DateTime> dates)
            {
                var minDate = datePicker.DisplayDateStart ?? DateTime.MinValue;
                var maxDate = datePicker.DisplayDateEnd ?? DateTime.MaxValue;
                var allDates = Enumerable.Range(0, (maxDate - minDate).Days).Select(d => minDate.AddDays(d));

                var disallowedDates = allDates.Except(dates);

                datePicker.BlackoutDates.Clear();
                foreach (var date in disallowedDates)
                {
                    datePicker.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }
        }

    }

}
