using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechTest2025.Models;

namespace TechTest2025.Tests.Models
{
    [TestFixture]
    public class PersonTests
    {
        private static IList<ValidationResult> ValidateModel(Person person)
        {
            var context = new ValidationContext(person, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(person, context, results, true);
            return results;
        }

        [Test]
        public void Name_IsRequired()
        {
            var person = new Person
            {
                Name = null,
                Address = "123 Main St",
                DateOfBirth = DateTime.Today.AddYears(-20)
            };

            var results = ValidateModel(person);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r => r.MemberNames.Contains(nameof(Person.Name))));
        }

        [Test]
        public void Name_MinimumLength()
        {
            var person = new Person
            {
                Name = "A",
                Address = "123 Main St",
                DateOfBirth = DateTime.Today.AddYears(-20)
            };

            var results = ValidateModel(person);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r => r.MemberNames.Contains(nameof(Person.Name))));
        }

        [Test]
        public void Name_MaximumLength()
        {
            var person = new Person
            {
                Name = new string('A', 101),
                Address = "123 Main St",
                DateOfBirth = DateTime.Today.AddYears(-20)
            };

            var results = ValidateModel(person);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r => r.MemberNames.Contains(nameof(Person.Name))));
        }

        [Test]
        public void Address_IsRequired()
        {
            var person = new Person
            {
                Name = "John Doe",
                Address = null,
                DateOfBirth = DateTime.Today.AddYears(-20)
            };

            var results = ValidateModel(person);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r => r.MemberNames.Contains(nameof(Person.Address))));
        }

        [Test]
        public void Address_MaximumLength()
        {
            var person = new Person
            {
                Name = "John Doe",
                Address = new string('A', 201),
                DateOfBirth = DateTime.Today.AddYears(-20)
            };

            var results = ValidateModel(person);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r => r.MemberNames.Contains(nameof(Person.Address))));
        }

        [Test]
        public void DateOfBirth_IsPlausible()
        {
            var person = new Person
            {
                Name = "John Doe",
                Address = "123 Main St",
                DateOfBirth = default
            };

            var results = ValidateModel(person);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r => r.ErrorMessage.Contains("Date of birth is too far in the past.")));
        }

        [Test]
        public void DateOfBirth_MustBeInThePast()
        {
            var person = new Person
            {
                Name = "John Doe",
                Address = "123 Main St",
                DateOfBirth = DateTime.Today.AddDays(1)
            };

            var results = ValidateModel(person);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r => r.ErrorMessage.Contains("Date of birth must be in the past")));
        }

        [Test]
        public void ValidPerson_PassesValidation()
        {
            var person = new Person
            {
                Name = "Jane Doe",
                Address = "456 Elm St",
                DateOfBirth = DateTime.Today.AddYears(-30)
            };

            var results = ValidateModel(person);
            Assert.That(results, Is.Empty);
        }
    }
}
