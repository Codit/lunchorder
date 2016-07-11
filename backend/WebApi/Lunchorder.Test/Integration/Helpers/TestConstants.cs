namespace Lunchorder.Test.Integration.Helpers
{
    public class TestConstants
    {
        public class User1
        {
            public const string Username = "tuser";
            public const string Password = "test-us3r";
        }

        public class IncorrectUser1
        {
            public const string Username = "Idonotexist";
            public const string Password = "completely-wrong-password";
        }

        public const string SeedPathPrefix = "Integration/Helpers/Seed/";
    }
}
