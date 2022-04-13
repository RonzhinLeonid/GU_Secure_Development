using Les1.DAL;
using Les1.Validations;
using System;
using Xunit;

namespace ValidationTests
{
    public class DebetCardUpdateValidationTests
    {
        private static readonly FluentValidationService<DebetCardRequest> validation = new DebetCardUpdateValidations();
        public void DebetCardRequest_IsCorrect()
        {
            var request = new DebetCardRequest
            {
                Id = 1,
                Number = "string",
                Holder = "string",
                ExpireMonth = 10,
                ExpireYear = 2023
            };
            var actual = validation.ValidateEntity(request);
            Assert.Equal(0, actual.Count);
        }
        [Fact]
        public void DebetCardRequest_NumberCannotBeEmpty()
        {
            var request = new DebetCardRequest
            {
                Id = 1,
                Number = "",
                Holder = "string",
                ExpireMonth = 10,
                ExpireYear = 2023
            };
            var actual = validation.ValidateEntity(request)[0];
            Assert.Equal("BRL-100.1", actual.Code);
        }
        [Fact]
        public void DebetCardRequest_HolderCannotBeEmpty()
        {
            var request = new DebetCardRequest
            {
                Id = 1,
                Number = "string",
                Holder = "",
                ExpireMonth = 10,
                ExpireYear = 2023
            };
            var actual = validation.ValidateEntity(request)[0];
            Assert.Equal("BRL-100.2", actual.Code);
        }
        [Fact]
        public void DebetCardRequest_ExpireMonthCannotBeEmpty()
        {
            var request = new DebetCardRequest
            {
                Id = 1,
                Number = "string",
                Holder = "string",
                ExpireYear = 2023
            };
            var actual = validation.ValidateEntity(request)[0];
            Assert.Equal("BRL-100.31", actual.Code);
        }
        [Fact]
        public void DebetCardRequest_ExpireYearCannotBeEmpty()
        {
            var request = new DebetCardRequest
            {
                Id = 1,
                Number = "string",
                Holder = "string",
                ExpireMonth = 10
            };
            var actual = validation.ValidateEntity(request)[0];
            Assert.Equal("BRL-100.41", actual.Code);
        }

        [Theory]
        [InlineData(-1, "BRL-100.32")]
        [InlineData(70, "BRL-100.33")]
        public void DebetCardRequest_ErrorExpireMonth(int month, string resul)
        {
            var request = new DebetCardRequest
            {
                Id = 1,
                Number = "string",
                Holder = "string",
                ExpireMonth = month,
                ExpireYear = 2023
            };
            var actual = validation.ValidateEntity(request)[0];
            Assert.Equal(resul, actual.Code);
        }
        [Theory]
        [InlineData(1999, "BRL-100.42")]
        [InlineData(2222, "BRL-100.43")]
        public void DebetCardRequest_ErrorExpireYear(int year, string resul)
        {
            var request = new DebetCardRequest
            {
                Id = 1,
                Number = "string",
                Holder = "string",
                ExpireMonth = 10,
                ExpireYear = year
            };
            var actual = validation.ValidateEntity(request)[0];
            Assert.Equal(resul, actual.Code);
        }
        [Fact]
        public void DebetCardUpdRequest_IdCannotBeEmpty()
        {
            var request = new DebetCardRequest
            {
                Number = "string",
                Holder = "string",
                ExpireMonth = 10,
                ExpireYear = 2023
            };
            var actual = validation.ValidateEntity(request)[0];
            Assert.Equal("BRL-100.51", actual.Code);
        }
        [Fact]
        public void DebetCardUpdRequest_IdCannotBeLower_0()
        {
            var request = new DebetCardRequest
            {
                Id = -1,
                Number = "string",
                Holder = "string",
                ExpireMonth = 10,
                ExpireYear = 2023
            };
            var actual = validation.ValidateEntity(request)[0];
            Assert.Equal("BRL-100.52", actual.Code);
        }
    }
}
