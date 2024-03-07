using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel
{
    public class NumericValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                int r;
                if (string.IsNullOrEmpty(s))
                {
                    return new ValidationResult(true, null);
                }
                else if (int.TryParse(s, out r))
                {
                    if(r <= 0)
                    {
                        return new ValidationResult(false, "Please enter a valid number.");
                    }else 
                        return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter a valid number.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }

    public class NumericNotNullValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                int r;
                if (int.TryParse(s, out r))
                {
                    if (r <= 0)
                    {
                        return new ValidationResult(false, "Please enter a valid number.");
                    }
                    else
                        return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter a valid number.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
    public class DaysValidation : ValidationRule
    {
        public static readonly DependencyProperty DaysProperty =
            DependencyProperty.Register("Days", typeof(int), typeof(DaysValidation), new PropertyMetadata(0));

        public int Days
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                int r;
                if (string.IsNullOrEmpty(s))
                {
                    return new ValidationResult(true, null);
                }
                if (int.TryParse(s, out r))
                {
                    if (r <= Days)
                    {
                        return new ValidationResult(false, "Reservation days must be at least " + Days);
                    }
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter a valid number.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occurred.");
            }
        }
    }
}

