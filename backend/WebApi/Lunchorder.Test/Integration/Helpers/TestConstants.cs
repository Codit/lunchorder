using System;

namespace Lunchorder.Test.Integration.Helpers
{
    public class TestConstants
    {
        public class VendorOrderHistory
        {
            public class VendorOrderHistory1
            {
                public const string Id = "5af19786-948f-41cd-bc98-944a811cacae";
                public const string VendorId = "07f24005-cf66-47bd-9368-16007e1ed0a2";
                public static DateTime OrderDate = DateTime.Parse("2016/07/26");
            }

            public class VendorOrderHistory2
            {
                public const string Id = "d4ce508f-49cf-7d00-e04a-2f2b614cb35d";
                public const string VendorId = "76827ef6-c4fd-4e81-afcd-d8487dcf493c";
                public static DateTime OrderDate = DateTime.Parse("2015/06/18");
            }
        }

        public class Menu
        {
            public class Menu1
            {
                public const string Id = "d0e0cba9-ab95-4666-ac54-dd44d2a5530a";
            }

            public class Menu2
            {
                public const string Id = "5632f42b-c0ef-45b1-b6cb-1f7c8ebc95c9";
            }

            public class Menu3
            {
                public const string Id = "96597ea7-8412-447a-8b47-43ddb51425a0";
            }
        }

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
        

            public class User3
        {
            public const string Id = "d185114e-e34c-4abd-8de6-020f814e8377";
            public const string Username = "tuser3";
            public const string Email = "products@codit.eu";
            public const string Password = "test-us3r";
        }

        public class User2
        {
            public const string Id = "f28763cf-569c-4043-a31e-e92842eba077";
            public const string Username = "tuser2";
            public const string Email = "products@codit.eu";
            public const string Password = "test-us3r";
        }

        public class User1
        {
            public const string Id = "9761a644-c2f6-476d-978f-58accaa09d0a";
            public const string UserName = "tuser";
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