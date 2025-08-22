using NUnit.Framework;
using org.krista.tools;

namespace org.krista.tools.tests;

[TestFixture]
public class StringUtils_Tests
{
    [Test]
    public void IsEmpty_ReturnsTrue_WhenStringIsNull()
    {
        string? input = null;
        bool result = StringUtils.IsEmpty(input);
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void IsEmpty_ReturnsTrue_WhenStringIsEmpty()
    {
        string input = StringUtils.Empty;
        bool result = StringUtils.IsEmpty(input);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsEmpty_ReturnsFalse_WhenStringIsSpace()
    {
        string input = StringUtils.Space;
        bool result = StringUtils.IsEmpty(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsNotEmpty_ReturnsFalse_WhenStringIsNull()
    {
        string? input = null;
        bool result = StringUtils.IsNotEmpty(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void Equals_ReturnsTrue_WhenStringsAreEqual()
    {
        string? str1 = "123";
        string? str2 = new string(['1', '2', '3']);
        bool result = StringUtils.Equals(str1, str2);
        Assert.That(result, Is.True);
    }

    [Test]
    public void Equals_ReturnsFalse_WhenStringsAreNotEqual()
    {
        string? str1 = "123";
        string? str2 = "456";
        bool result = StringUtils.Equals(str1, str2);
        Assert.That(result, Is.False);
    }

    [Test]
    public void Left_ReturnsHello_WhenStringIsHelloWorldAndLenIs5()
    {
        string input = "Hello, world";
        string result = StringUtils.Left(input, 5);
        Assert.That(result, Is.EqualTo("Hello"));
    }

    [Test]
    public void Right_ReturnsWorld_WhenStringIsHelloWorldAndLenIs5()
    {
        string input = "Hello, world";
        string result = StringUtils.Right(input, 5);
        Assert.That(result, Is.EqualTo("world"));
    }

    [Test]
    public void Mid_ReturnsLowo_WhenStringIsHelloWorldAndPosIs3AndLenIs3()
    {
        string input = "Hello, world";
        string result = StringUtils.Mid(input, 3, 3);
        Console.WriteLine(result);
        Assert.That(result, Is.EqualTo("lo, wo"));
    }
}
