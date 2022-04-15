using Xunit;
using SEP4DataWarehouse;
using SEP4DataWarehouse.Controllers;

    // These are just some tests for testing the test framework
    // Please write some unit tests as you make the codebase so that we don't have to do it later when we want to fill in the testing section in project report
    // Refer to https://xunit.net/docs/getting-started/netcore/cmdline on how to use this. It's pretty simple, just remember to make proper use of [Fact] and [Theory] annotations
    // Also probably don't put every test in 1 class, try to group them in classes by some kind of logic
    // PS: when making a new test file make sure to put 'using SEP4DataWarehouse' at the top if u want to use some methods from the main project, which u probably do
    public class AdditionTest
    {
        private ReadingController _readingController = new ReadingController();
        
        //designed to pass
        [Fact]
        public void AdditionTestPass()
        {
            Assert.Equal(2, _readingController.AddFunction(1,1));
        }
        
    }
