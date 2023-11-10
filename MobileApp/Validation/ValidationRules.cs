using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MobileApp.Validation
{
    public class IsNotNullOrEmptyRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        public bool Check(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
    public class IsNotNullRule : IValidationRule<object>
    {
        public string ValidationMessage { get; set; }
        public bool Check(object value)
        {
            return value is not null;
        }
    }
    public class IsValidDate : IValidationRule<DateTime>
    {
        public string ValidationMessage { get; set; }
        public bool Check(DateTime value)
        {
            return value != null && value != DateTime.MinValue && value != DateTime.MaxValue;
        }
    }
    public class EmailRule : IValidationRule<string>
    {
        private readonly Regex _regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public string ValidationMessage { get; set; }

        public bool Check(string value) =>
            value is string str && _regex.IsMatch(str);
    }
    public class StringIsNumerRule : IValidationRule<string>
    {

        public string ValidationMessage { get; set; }

        public bool Check(string value) =>
            value is string && int.TryParse(value,out int val);
    }
    public class PhoneNumberRule : IValidationRule<string>
    {
        private readonly Regex _regex = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
        public string ValidationMessage { get; set; }

        public bool Check(string value) =>
            value is string str && _regex.IsMatch(str);
    }
    public class IsDateOnlyRule : IValidationRule<string>
    {

        public string ValidationMessage { get; set; }

        public bool Check(string value) =>
            value is string && DateOnly.TryParse(value, out DateOnly val);
    }
    public class ValueMustBeEqualRule<T> : IValidationRule<string>
    {
        private readonly ValidatableObject<T> _compareObject;
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            bool eqality = value is string str && _compareObject.Value != null && value.Equals(_compareObject.Value.ToString());
            return eqality; 
        }

        public ValueMustBeEqualRule(ValidatableObject<T> validatableObject)
        {
            _compareObject = validatableObject;
        }
    }
}

