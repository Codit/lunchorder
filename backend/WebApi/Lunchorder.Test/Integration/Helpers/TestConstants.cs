using System;

namespace Lunchorder.Test.Integration.Helpers
{
    public class TestConstants
    {
        public class Badges
        {
            public class Badge1
            {
                public const string Id = "5de5ecfd-6e4d-47e3-b129-96614e745ee5";
                public const string Name = "Badge 1";
                public const string Icon = "Icon 1";
                public const string Description = "Description 1";
            }
        }

        public class Favorites
        {
            public class Favorite1 { public const string MenuEntryId = "f2d6b026-0e57-45a9-9270-f8fa4590079c"; }
            public class Favorite2 { public const string MenuEntryId = "bd341859-1e68-4650-a8a5-31c409f91b3a"; }
            public class Favorite3 { public const string MenuEntryId = "63a04825-1489-46cb-83c5-8dfda58c27c0"; }
        }

        public class User1
        {
            public const string Id = "9761a644-c2f6-476d-978f-58accaa09d0a";
            public const string Username = "tuser";
            public const string Email = "products@codit.eu";
            public const string Password = "test-us3r";

            public class Profile
            {
                public const string FirstName = "Test";
                public const string LastName = "User";
                public const string Picture = "http://pic.tu.re";
                public const string Culture = "en-US";
            }
        }

        public class IncorrectUser1
        {
            public const string Username = "Idonotexist";
            public const string Password = "completely-wrong-password";
        }

        public const string SeedPathPrefix = "Integration/Helpers/Seed/";
    }
}