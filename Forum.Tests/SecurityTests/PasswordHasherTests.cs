using Forum.Security;

namespace Forum.Tests.SecurityTests;

public class PasswordHasherTests
{
    private PasswordHasher _passwordHasher;

    [SetUp]
    public void SetUp()
    {
        _passwordHasher = new PasswordHasher();
    }

    [Test]
    public void Test_HashShouldReturnHashedPassword()
    {
        //Arrange
        string password = "pass1234";
        //Act
        string result = _passwordHasher.Hash(password);
        //Assert
        Assert.That(result, Is.Not.EqualTo(password));
        Assert.That(result, Is.Not.Empty);
    }

    [Test]
    public void Test_VerifyShouldReturnTrueWhenPasswordVerified()
    {
        //Arrange
        string password = "pass1234";
        string passwordHashed = _passwordHasher.Hash(password);
        //Act
        bool result = _passwordHasher.Verify(password, passwordHashed);
        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Test_VerifyShouldReturnFalseWhenPasswordNotVerified()
    {
        //Arrange
        string password = "pass1234";
        string passwordHashed = _passwordHasher.Hash(password);
        //Act
        bool result = _passwordHasher.Verify("password", passwordHashed);
        //Assert
        Assert.That(result, Is.False);
    }
}