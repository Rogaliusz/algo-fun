using Xunit;

namespace AlgoFun.StringCalculator.Tests;

public class StringCalculatorTests
{
    [Theory]
    [InlineData("1 + 3", 4)]
    [InlineData("1 * 3", 3)]
    [InlineData("1 * -3", -3)]
    [InlineData("3 / 1", 3)]
    [InlineData("3 - 1", 2)]
    [InlineData("3 - 1 * 3", 0)]
    [InlineData("2 * (3 * (1 + 5))", 36)]
    [InlineData("10 * 10 * 10", 1000)]
    public void CalculatorTests(string expression, decimal expectedResult)
    {
        // Arrange
        var calculator = new StringCalculator();
        
        // Act 
        var result = calculator.Evaluate(expression);
        
        // Arrange
        Assert.Equal(expectedResult, result);
    }
}