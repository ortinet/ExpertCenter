using ExpertCenter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Repository
{
    public class DummyRepository : IRepository
    {
        private readonly ColumnType[] _columnTypes = [
            new ColumnType()
            {
                Id = 0,
                Title = "Однострочный текст",
                Code = "text",
            },
            new ColumnType()
            {
                Id = 1,
                Title = "Многострочный текст",
                Code = "multitext",
            },
            new ColumnType()
            {
                Id = 2,
                Title = "Число",
                Code = "num",
            },
            new ColumnType()
            {
                Id = 3,
                Title = "Еще какой-то тип",
                Code = "something",
            },
        ];
        private readonly List<UserColumn> _userColumns;
        private readonly List<PriceList> _priceLists;

        public DummyRepository()
        {
            _userColumns = new List<UserColumn>()
            {
                new UserColumn()
                {
                    Id = 0,
                    Header = "Колонка 1",
                    Type = _columnTypes[0],
                },
                new UserColumn()
                {
                    Id = 1,
                    Header = "Числовая колонка",
                    Type = _columnTypes[2],
                },
                new UserColumn()
                {
                    Id = 2,
                    Header = "Многострочная колонка",
                    Type = _columnTypes[1],
                },
            };

            _priceLists = new List<PriceList>()
            {
                new PriceList()
                {
                    Id = 0,
                    Name = "KUKUS",
                    Columns = new List<UserColumn>() { _userColumns[0], _userColumns[1], },
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Id = 0,
                            Code = "Кот",
                            Name = "Котофей",
                            Properties = new Dictionary<int, string?>()
                            {
                                { _userColumns[0].Id, "memes" },
                                { _userColumns[1].Id, "1337" },
                            },
                            UserColumns = new List<UserColumn>() { _userColumns[0], _userColumns[1] }
                        },
                        new Product()
                        {
                            Id = 1,
                            Code = "Пес",
                            Name = "Собака",
                            Properties = new Dictionary<int, string?>()
                            {
                                { _userColumns[0].Id, "галлон" },
                                { _userColumns[1].Id, "228" },
                            },
                            UserColumns = new List<UserColumn>() { _userColumns[0], _userColumns[1] }
                        },
                        new Product()
                        {
                            Id = 2,
                            Code = "Сыр",
                            Name = "Сыровар",
                            Properties = new Dictionary<int, string?>()
                            {
                                { _userColumns[0].Id, "легкое" },
                                { _userColumns[1].Id, "322" },
                            },
                            UserColumns = new List<UserColumn>() { _userColumns[0], _userColumns[1] }
                        },
                    }
                },
                new PriceList(){ Id = 1, Name = "LULUS" },
                new PriceList(){ Id = 2, Name = "MEMES" },
                new PriceList(){ Id = 3, Name = "nigga" },
            };
        }

        public bool CreatePriceList(PriceList priceList)
        {
            _priceLists.Add(priceList);
            return true;
        }

        public bool CreateProduct(Product product)
        {
            PriceList? requiredPriceList = _priceLists.FirstOrDefault(list => list.Id == product.PriceListId);
            if (requiredPriceList == null)
            {
                return false;
            }

            requiredPriceList.Products.Add(product);
            return true;
        }

        public ColumnType? GetColumnType(string code)
        {
            return _columnTypes.FirstOrDefault(c => c.Code == code);
        }

        public IEnumerable<ColumnType> GetColumnTypes()
        {
            return _columnTypes;
        }

        public PriceList? GetPriceList(int id)
        {
            return _priceLists.FirstOrDefault(list => list.Id == id);
        }

        public IEnumerable<PriceList> GetPriceLists()
        {
            return _priceLists;
        }

        public IEnumerable<UserColumn> GetUnickUserColumns()
        {
            return _userColumns.Take(2);
        }
    }
}
