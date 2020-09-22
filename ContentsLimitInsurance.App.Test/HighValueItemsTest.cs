using AutoMapper;
using ContentsLimitInsurance.App.Dtos;
using ContentsLimitInsurance.App.Entities;
using NUnit.Framework;

namespace ContentsLimitInsurance.App.Test
{
    [TestFixture]
    public class HighValueItemsTest
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<HighValueItemDto, HighValueItem>();
                opts.CreateMap<HighValueItem, HighValueItemDto>();
            });

            _mapper = config.CreateMapper();

        }

        [Test]
        public void AddHighValueItem()
        {
            HighValueItemDto newHighValueItemDto = new HighValueItemDto()
            {
                Name = "TV",
                Value = 2000,
                Category = 1 //Eletronics
            };

            HighValueItem newHighValueItem = new HighValueItem();

            newHighValueItem = _mapper.Map(newHighValueItemDto, newHighValueItem);

            Assert.AreEqual(newHighValueItemDto.Name, newHighValueItem.Name);
            Assert.AreEqual(newHighValueItemDto.Value, newHighValueItem.Value);
            Assert.AreEqual(newHighValueItemDto.Category, newHighValueItem.Category);
        }
    }
}