using backend.Data;
using backend.Models;

namespace backend.Tests;

public class ModelValidationTest
{
    [Fact]
    public void TestEatenPercantageNegative()
    {
        Feeding f = new Feeding();
        f.eatenpercentage = -10;
        Assert.False(ModelValidation.IsValidFeedingData(f), "Keine negativen Werte erlaubt");
    }

    [Fact]
    public void TestEatenPercantageAbove100()
    {
        Feeding f = new Feeding();
        f.eatenpercentage = 110;
        Assert.False(ModelValidation.IsValidFeedingData(f), "Keine Werte über 100 % erlaubt");
    }

    [Fact]
    public void TestEatenPercantageValid()
    {
        Feeding f = new Feeding();
        f.eatenpercentage = 20;
        Assert.True(ModelValidation.IsValidFeedingData(f), "Erlaubter Wert");
    }

    [Theory]
    [InlineData("")]
    [InlineData("katze")]
    [InlineData("Herr Müller")]
    [InlineData("A2")]
    public void TestUngueligeKatzennamen(string name)
    {
        Feeding f = new Feeding();
        f.catname = name;
        Assert.False(ModelValidation.IsValidCatname(f), "Nur einfache Namen zugelassen");

    }

    [Theory]
    [InlineData("Mietze")]
    [InlineData("Katze")]
    [InlineData("Kitty")]
    [InlineData("Garfield")]
    public void TestGueligeKatzennamen(string name)
    {
        Feeding f = new Feeding();
        f.catname = name;
        Assert.True(ModelValidation.IsValidCatname(f), "Einfache Namen ok");

    }
}