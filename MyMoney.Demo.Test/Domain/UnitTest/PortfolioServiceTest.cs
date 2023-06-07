using System.Threading.Tasks;

using Moq;

using MyMoney.Demo.Domain;

using Xunit;

namespace MyMoney.Demo.Test.UnitTest
{
    public class PortfolioServiceTest
    {
        [Fact]
        public async Task Execute_Successfull_With_Valid_Input_File()
        {
            //given
            var mockTransactionManager = new Mock<ICommandManager>();
            mockTransactionManager.Setup(M => M.Process(It.IsAny<string>())).Verifiable();
            var sut = new PortfolioService(mockTransactionManager.Object);

            //when
            await sut.Execute("SampleData\\input.txt");

            //then
            mockTransactionManager.Verify(v => v.Process(It.IsAny<string>()), Times.Exactly(10));
        }

        [Fact]
        public async Task Execute_Fail_With_Invalid_Input_File()
        {
            //given
            var mockTransactionManager = new Mock<ICommandManager>();
            var sut = new PortfolioService(mockTransactionManager.Object);

            //when
            await sut.Execute("invalid.txt");

            //then
            mockTransactionManager.Verify(v => v.Process(It.IsAny<string>()), Times.Never);
        }
    }
}