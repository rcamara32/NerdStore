using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Catalog.Domain.Tests
{
    public class ProductTests
    {

        [Fact]
        public void Product_Should_Return_Exception_Name_Empty()
        {
            //arrange

            //act
            var ex = Assert.Throws<DomainException>(() =>
                new Product(
                        string.Empty,
                        "Description",
                        false,
                        100,
                        Guid.NewGuid(),
                        DateTime.Now,
                        "Image",
                        new Dimensions(1, 1, 1))
            );

            //assert
            Assert.Equal("The Product Name cannot be empty", ex.Message);
        }

        [Fact]
        public void Product_Validate_ShouldReturnExceptions()
        {
            var ex = Assert.Throws<DomainException>(() =>
                new Product("Nome", string.Empty, false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("The Product Description cannot be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 0, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("The Product Price cannot be less or equals zero", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.Empty, DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("The Product Category Id cannot be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensions(1, 1, 1))
            );

            Assert.Equal("The Product image cannot be empty", ex.Message);

            

            
        }

        [Fact]
        public void Product_Should_Return_Exception_Wrong_Height_Dimension()
        {
            //arrange
            //act
            var ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(0, 1, 1))
            );

            //assert
            Assert.Equal("The Height cannot be less or equals zero", ex.Message);
        }

        [Fact]
        public void Product_Should_Return_Exception_Wrong_Width_Dimension()
        {
            //arrange
            //act
            var ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(1, 0, 1))
            );

            //assert
            Assert.Equal("The Width cannot be less or equals zero", ex.Message);
        }

        [Fact]
        public void Product_Should_Return_Exception_Wrong_Depth_Dimension()
        {
            //arrange
            //act
            var ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(1, 1, 0))
            );

            //assert
            Assert.Equal("The Depth cannot be less or equals zero", ex.Message);
        }

    }
}
