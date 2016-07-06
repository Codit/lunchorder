using System.Diagnostics.Contracts;
using System.Reflection;
using NUnit.Framework;

namespace Codit.Testing
{
    public abstract class TestBase<TSystemUnderTest>
    {
        public const BindingFlags FlagsForGettingEverything = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
        protected TSystemUnderTest sut;

        [TestFixtureSetUp]
        protected virtual void BeforeTesting()
        {
            if (LogManager.Adapter is NoOpLoggerFactoryAdapter)
            {
                var nvc = new NameValueCollection
                              {
                                  {"level", "ALL"},
                                  {"showLogName", "true"},
                                  {"showDateTime", "true"},
                                  {"dateTimeFormat", "yyyy/MM/dd HH:mm:ss:fff"}
                              };

                LogManager.Adapter = new TraceLoggerFactoryAdapter(nvc);
            }
        }

        /// <summary>
        /// Calls Arrange, InitializeSystemUnderTest, Act
        /// </summary>
        [SetUp]
        protected virtual void BeforeEachTest()
        {
            Arrange();
            InitializeSystemUnderTest();
            Act();
        }

        [TearDown]
        protected virtual void AfterEachTest()
        {
            CleanUp();
        }

        [TestFixtureTearDown]
        protected virtual void AfterTesting()
        { }

        /// <summary>
        /// Calls CreateSut and sets the sut field.
        /// </summary>
        protected void InitializeSystemUnderTest()
        {
            sut = CreateSut();
        }

        protected void SetFieldUsingReflection<T>(object obj, string fieldName, T value)
        {
            FieldInfo fi = obj.GetType().GetField(fieldName, FlagsForGettingEverything);
            Contract.Assert(fi != null);
            Contract.Assert(fi.FieldType == typeof(T));

            fi.SetValue(obj, value);
        }

        protected abstract TSystemUnderTest CreateSut();

        protected virtual void Arrange() { }
        protected virtual void Act() { }
        protected virtual void CleanUp() { }
    }
}
