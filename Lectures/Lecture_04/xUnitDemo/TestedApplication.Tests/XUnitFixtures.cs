using System;
using System.Diagnostics;
using Xunit;

namespace TestedApplication.Tests
{
    public class NonSharedFixture : IDisposable
    {
        public NonSharedFixture()
        {
            Debug.WriteLine("Non shared fixture created.");
        }

        public void Dispose()
        {

            Debug.WriteLine("Non shared fixture destroyed.");
        }
    }

    public class ClassSharedFixture : IDisposable
    {
        public ClassSharedFixture()
        {
            Debug.WriteLine("Class shared fixture created.");
        }

        public void Dispose()
        {

            Debug.WriteLine("Class shared fixture destroyed.");
        }

    }

    public class GroupSharedFixture : IDisposable
    {
        public GroupSharedFixture()
        {
            Debug.WriteLine("Group shared fixture created.");
        }

        public void Dispose()
        {
            Debug.WriteLine("Group shared fixture destroyed.");
        }

    }


    [CollectionDefinition("GroupName")]
    public class TestGroupTestCollection : ICollectionFixture<GroupSharedFixture> { }


    [Collection("GroupName")]
    public class XUnitExampleTest1 : IDisposable, IClassFixture<ClassSharedFixture>
    {
        private readonly ClassSharedFixture _classSharedFixture;
        private readonly GroupSharedFixture _groupSharedFixture;
        private readonly NonSharedFixture _nonSharedFixture;

        public XUnitExampleTest1(ClassSharedFixture classSharedFixture,GroupSharedFixture groupSharedFixture)
        {
            _classSharedFixture = classSharedFixture;
            _groupSharedFixture = groupSharedFixture;
            _nonSharedFixture = new NonSharedFixture();
        }
        [Fact()]
        public void ParameterLessTest()
        {
            //ARRANGE
            int first = 5;
            int second = 6;
            
            //ACT
            var result = first + second;

            //ASSERT
            Assert.Equal(11,result);
        }

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2,1,2,Skip = "This test is skipped.")]
        public void TestWithParameters(int first, int second, int expectedResult)
        {
            //ARRANGE


            //ACT
            var result = first + second;

            //ASSERT
            Assert.Equal(expectedResult, result);
        }

        public void Dispose()
        {
            _nonSharedFixture?.Dispose();
        }
    }


    [Collection("GroupName")]
    public class XUnitExampleTest2 : IDisposable, IClassFixture<ClassSharedFixture>
    {
        private readonly ClassSharedFixture _classSharedFixture;
        private readonly GroupSharedFixture _groupSharedFixture;
        private readonly NonSharedFixture _nonSharedFixture;

        public XUnitExampleTest2(ClassSharedFixture classSharedFixture,GroupSharedFixture groupSharedFixture)
        {
            _classSharedFixture = classSharedFixture;
            _groupSharedFixture = groupSharedFixture;
            _nonSharedFixture = new NonSharedFixture();
        }
        [Fact()]
        public void ParameterLessTest()
        {
            //ARRANGE
            int first = 5;
            int second = 6;
            
            //ACT
            var result = first + second;

            //ASSERT
            Assert.Equal(11,result);
        }

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2,1,2,Skip = "This test is skipped.")]
        public void TestWithParameters(int first, int second, int expectedResult)
        {
            //ARRANGE


            //ACT
            var result = first + second;

            //ASSERT
            Assert.Equal(expectedResult, result);
        }

        public void Dispose()
        {
            _nonSharedFixture?.Dispose();
        }
    }


}
