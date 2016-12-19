using System;

namespace Induction.Core
{
    public class CommonConstants
    {
        public const string ProjectName = "Induction";

        public const string PhoneRegex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        public const string DateRegex = @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$";

        public const string NameRegex = @"^([a-zA-Z \.\&\'\-]+)$";

        public const string EmailRegex = @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$";

        public const string ZipRegex = @"^\d{5}$|^\d{5}-\d{4}$";

        public const string IntOverZeroRegex = @"^[1-9]\d*$";

        public const int PageSize = 50;
    }
}
