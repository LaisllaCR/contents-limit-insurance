using AutoMapper;
using ContentsLimitInsurance.App.Controllers;
using ContentsLimitInsurance.App.Dtos;
using ContentsLimitInsurance.App.Models;
using ContentsLimitInsurance.App.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentsLimitInsurance.App.Test
{
    [TestFixture]
    public class HighValueItemsTest
    {
        private HighValueItemDto _newHighValueItem;
        private List<CategoryWithItemsDto> _categoriesWithItems;
        private List<HighValueItemDto> _userItems;
        private Mock<IHighValueItemsService> _highValueItemServiceMock;
        private HighValueItemController _controller;
        private const int userId = 1;

        [SetUp]
        public void Setup()
        {
            _newHighValueItem = new HighValueItemDto
            {
                Name = "SmartTV",
                ItemCategoryId = 1,
                Value = 1200,
                UserId = userId,
            };

            _categoriesWithItems = new List<CategoryWithItemsDto>
            {
                new CategoryWithItemsDto
                {
                    Category = new ItemCategoryDto{
                        ItemCategoryId = 1,
                        Name = "Eletronics",
                    },
                    Items = new List<HighValueItemDto>
                    {
                        new HighValueItemDto
                        {
                            HighValueItemId = 1,
                            Name = "Smartphone",
                            ItemCategoryId = 1,
                            Value = 700,
                            UserId = userId,
                        },
                        new HighValueItemDto
                        {
                            HighValueItemId = 2,
                            Name = "Laptop",
                            ItemCategoryId = 1,
                            Value = 1700,
                            UserId = userId,
                        },
                    }
                },
                new CategoryWithItemsDto
                {
                    Category = new ItemCategoryDto{
                        ItemCategoryId = 1,
                        Name = "Clothing",
                    },
                    Items = new List<HighValueItemDto>
                    {
                        new HighValueItemDto
                        {
                            HighValueItemId = 3,
                            Name = "Dress",
                            ItemCategoryId = 2,
                            Value = 1700,
                            UserId = userId,
                        }
                    }
                },
            };

            _userItems = new List<HighValueItemDto>()
            {
                new HighValueItemDto
                {
                    HighValueItemId = 1,
                    Name = "Smartphone",
                    ItemCategoryId = 1,
                    Value = 700,
                    UserId = userId,
                },
                new HighValueItemDto
                {
                    HighValueItemId = 2,
                    Name = "Laptop",
                    ItemCategoryId = 1,
                    Value = 1700,
                    UserId = userId,
                },
                new HighValueItemDto
                {
                    HighValueItemId = 3,
                    Name = "Dress",
                    ItemCategoryId = 2,
                    Value = 1700,
                    UserId = userId,
                }
            };

            _highValueItemServiceMock = new Mock<IHighValueItemsService>();
            _highValueItemServiceMock.Setup(x => x.GetHighValueItemsPerCategoriesByUser(userId))
                .Returns(_categoriesWithItems);
            _highValueItemServiceMock.Setup(x => x.GetHighValueItemsByUser(userId))
                .Returns(_userItems);

            _controller = new HighValueItemController(
                _highValueItemServiceMock.Object);
        }

        [Test]
        public async Task ShouldReturnBadRequestForNullPostRequests ()
        {
            var actionResult = await _controller.PostHighValueItem(null);

            var result = actionResult.Result as BadRequestResult;
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Test]
        public async Task ShouldReturnBadRequestForInvalidModelStatePostRequests()
        {
            HighValueItemDto emptyHighValueItem = new HighValueItemDto();
            var actionResult = await _controller.PostHighValueItem(emptyHighValueItem);

            var result = actionResult.Result as BadRequestResult;
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Test]
        public async Task ShouldReturnCreatedForValidPostRequests()
        {
            _highValueItemServiceMock
                .Setup(x => x.AddHighValueItem(It.IsAny<HighValueItemDto>()))
                .Returns<HighValueItemDto>(x => _newHighValueItem);

            var actionResult = await _controller.PostHighValueItem(_newHighValueItem);

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsAssignableFrom<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task ShouldSaveHighValueItem()
        {
            HighValueItemDto savedHighValueItem = null;
            _highValueItemServiceMock
                .Setup(x => x.AddHighValueItem(It.IsAny<HighValueItemDto>()))
                .Callback<HighValueItemDto>(highValueItem =>
                {
                    savedHighValueItem = highValueItem;
                });

            await _controller.PostHighValueItem(_newHighValueItem);

            _highValueItemServiceMock
                .Verify(x => x.AddHighValueItem(It.IsAny<HighValueItemDto>()), Times.Once
                );

            Assert.NotNull(savedHighValueItem);
            Assert.AreEqual(_newHighValueItem.Name, savedHighValueItem.Name);
            Assert.AreEqual(_newHighValueItem.Value, savedHighValueItem.Value);
            Assert.AreEqual(_newHighValueItem.ItemCategoryId, savedHighValueItem.ItemCategoryId);
            Assert.AreEqual(_newHighValueItem.UserId, savedHighValueItem.UserId);
            Assert.AreEqual(_categoriesWithItems.First().Category.ItemCategoryId, savedHighValueItem.ItemCategoryId);
        }

        [Test]
        public async Task ShouldReturnNotFoundForInvalidIdDeleteRequests()
        {
            int invalidId = 0;

            _highValueItemServiceMock
                .Setup(x => x.HighValueItemExists(It.IsAny<int>()))
                .Returns<int>(x => false);

            var actionResult = await _controller.DeleteHighValueItem(invalidId);

            var result = actionResult.Result as NotFoundResult;
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Test]
        public async Task ShouldReturnOkForValidIdDeleteRequests()
        {
            int validId = 1;
            HighValueItemDto removedHighValueItem = new HighValueItemDto()
            {
                HighValueItemId = 1,
                Name = "Smartphone",
                ItemCategoryId = 1,
                Value = 700,
                UserId = userId,
            };

            _highValueItemServiceMock
                .Setup(x => x.HighValueItemExists(It.IsAny<int>()))
                .Returns<int>(x => true);

            _highValueItemServiceMock
                .Setup(x => x.DeleteHighValueItem(It.IsAny<int>()))
                .Returns<int>(x => removedHighValueItem);

            var actionResult = await _controller.DeleteHighValueItem(validId);

            var result = actionResult.Result as OkObjectResult;
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Test]
        public async Task ShouldRemoveHighValueItem()
        {
            int validId = 1;
            HighValueItemDto removedHighValueItem = new HighValueItemDto()
            {
                HighValueItemId = 1,
                Name = "Smartphone",
                ItemCategoryId = 1,
                Value = 700,
                UserId = userId,
            };

            _highValueItemServiceMock
                .Setup(x => x.HighValueItemExists(It.IsAny<int>()))
                .Returns<int>(x => true);

            _highValueItemServiceMock
                .Setup(x => x.DeleteHighValueItem(It.IsAny<int>()))
                .Returns<int>(x => removedHighValueItem);

            var actionResult = await _controller.DeleteHighValueItem(validId);

            var getActionResult = await _controller.GetHighValueItem(validId);
            var resultGet = getActionResult.Result as NotFoundResult;

            Assert.IsAssignableFrom<NotFoundResult>(resultGet);
        }

        [Test]
        public async Task ShouldReturnOkForHighValueItemsPerCategoriesByUserRequests()
        {
            int userId = 1;
            var actionResult = await _controller.GetHighValueItemsPerCategoriesByUser(userId);

            var result = actionResult.Result as OkObjectResult;
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Test]
        public async Task ShouldReturnOkHighValueItemsByUserRequests()
        {
            int userId = 1;
            var actionResult = await _controller.GetHighValueItemsByUser(userId);

            var result = actionResult.Result as OkObjectResult;
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }
    }
}