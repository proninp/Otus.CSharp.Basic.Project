using FinanceManager.Bot.Services.StateHandlers.Providers;

namespace FinanceManager.Bot.Tests.StateHandlers.Providers;

public class DecimalNumberProviderTests
{
    private readonly DecimalNumberProvider _provider = new();

    [Theory]
    [InlineData("10.567", 10.57, true)]
    [InlineData("10.564", 10.56, true)]
    [InlineData("-5.555", -5.56, true)]
    [InlineData("0", 0, true)]
    [InlineData("1,23", 1.23, true)]
    [InlineData("  7.89  ", 7.89, true)]
    [InlineData(null, 0, false)]
    [InlineData("", 0, false)]
    [InlineData("   ", 0, false)]
    [InlineData("abc", 0, false)]
    [InlineData("10.99999999999999999999", 11.00, true)]
    [InlineData("99999999999999999999999999999999999999999999999999", 0, false)]
    public void Provide_ShouldParseAndRoundCorrectly(string? input, decimal expected, bool expectedResult)
    {
        // Act
        var result = _provider.Provide(input, out var actual);

        // Assert
        Assert.Equal(expectedResult, result);
        if (expectedResult)
        {
            Assert.Equal(expected, actual);
        }
    }
}
