using System;

namespace Validator
{
    public static class Validators
    {
        public static bool checkValidDate(DateTime date)
        {
            return date is DateTime;
        }
    }
}
