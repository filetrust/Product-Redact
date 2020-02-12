using FluentAssertions;
using NUnit.Framework;

namespace Glasswall.Redact.Api.Tests
{
    public class SettingsBuilderTests
    {
        [Test]
        public void BuildCreatesValidXml()
        {
            // Arrange
            var json = "[{\"SearchTerm\": \"bob\",\"ReplaceWith\": \"#\"}]";
            var builder = new SettingsBuilder();

            // Act
            var xml = builder.Build(json);

            // Assert
            xml.Should().HaveLength(270);
        }

        [Test]
        public void BuildThrowsException()
        {
            // Arrange
            var json = "[{\"SearchTerm\": \"bob\",\"ReplaceWith\": \"#\"}]";
            var builder = new SettingsBuilder();

            // Act
            var xml = builder.Build(json);

            // Assert
            xml.Should().HaveLength(270);
        }


    }
}