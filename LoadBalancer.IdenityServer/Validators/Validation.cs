using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.IdenityServer.Validators
{
    public class Validation
    {
        public class NameAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                string? stringValue = value as string;
                if (stringValue!.Any(char.IsDigit))
                {
                    ErrorMessage = "Name is incorrect";
                    return false;
                }
                return true;
            }
        }
        public class EmailAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                string? stringValue = value as string;
                bool check = System.Text.RegularExpressions.Regex.IsMatch(stringValue!, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (!check)
                {
                    ErrorMessage = "Wrong email";
                    return false;
                }
                return true;
            }
        }
    }  
}
