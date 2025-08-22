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
        Assert.That(result, Is.EqualTo("lo, wo"));
    }

    [Test]
    public void SubstringBefore_ReturnsHello_WhenStringIsHelloWorldAndSeparatorIsComma()
    {
        string input = "Hello, world";
        string result = StringUtils.SubstringBefore(input, ",");
        Assert.That(result, Is.EqualTo("Hello"));
    }

    [Test]
    public void SubstringAfter_ReturnsWorld_WhenStringIsHelloWorldAndSeparatorIsSpace()
    {
        string input = "Hello, world";
        string result = StringUtils.SubstringAfter(input, StringUtils.Space);
        Assert.That(result, Is.EqualTo("world"));
    }

    // TODO: 2 теста не работают, 1 возвращает ello вместо ell, второй выдаёт ошибку о выходе за границы массива
    [Test]
    public void SubstringBetween_ReturnsELl_WhenStringIsHelloWorldAndSeparatorsAreHAndO()
    {
        string input = "Hello, world";
        string result = StringUtils.SubstringBetween(input, "H", "o");
        Assert.That(result, Is.EqualTo("ell"));
    }

    [Test]
    public void SubstringsBetween_ReturnsArray_WhenStringIsNiggerNetherNothernAndSeparatorsAreNAndR()
    {
        string input = "Nigger Nether Nother";
        string[] result = StringUtils.SubstringsBetween(input, "N", "r");
        Assert.That(result, Is.EquivalentTo(new string[] { "igge", "ethe", "othe" }));
    }
}
