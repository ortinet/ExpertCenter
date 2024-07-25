using ExpertCenter.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
                            UserColumnValues = new()
                            {
                                new UserColumnValue()
                                {
                                    UserColumn = _userColumns[0],
                                    Value = "memes",
                                },
                                new UserColumnValue()
                                {
                                    UserColumn = _userColumns[1],
                                    Value = "1337",
                                },
                            },
                        },
                        new Product()
                        {
                            Id = 1,
                            Code = "Пес",
                            Name = "Собака",
                            UserColumnValues = new()
                            {
                                new UserColumnValue()
                                {
                                    UserColumn = _userColumns[0],
                                    Value = "галлон",
                                },
                                new UserColumnValue()
                                {
                                    UserColumn = _userColumns[1],
                                    Value = "228",
                                },
                            },
                        },
                        new Product()
                        {
                            Id = 2,
                            Code = "Сыр",
                            Name = "Сыровар",
                            UserColumnValues = new()
                            {
                                new UserColumnValue()
                                {
                                    UserColumn = _userColumns[0],
                                    Value = "легкое",
                                },
                                new UserColumnValue()
                                {
                                    UserColumn = _userColumns[1],
                                    Value = "322",
                                },
                            },
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
            priceList.Id = _priceLists.Max(x => x.Id) + 1;
            _priceLists.Add(priceList);

            foreach (var column in priceList.Columns)
            {
                if (_userColumns.All(existingColumn => existingColumn.Id != column.Id))
                {
                    column.Id = _userColumns.Max(userColumn => userColumn.Id) + 1;
                    _userColumns.Add(column);
                }
            }

            return true;
        }

        public bool CreateProduct(Product product)
        {
            product.Id = _priceLists.Max(list => list.Products.Count > 0 ? list.Products.Max(product => product.Id) : -1) + 1;
            PriceList? requiredPriceList = _priceLists.FirstOrDefault(list => list.Id == product.PriceListId);
            if (requiredPriceList == null)
            {
                return false;
            }

            requiredPriceList.Products.Add(product);
            return true;
        }

        public bool DeleteProduct(int id)
        {
            foreach (var list in _priceLists)
            {
                Product? productToRemove = list.Products.FirstOrDefault(prod => prod.Id == id);
                if (productToRemove != null)
                {
                    return list.Products.Remove(productToRemove);
                }
            }

            return false;
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
            return _userColumns.DistinctBy(column => (column.Header, column.Type?.Code)).ToList();
        }
    }
}
