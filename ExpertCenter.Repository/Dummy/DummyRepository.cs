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
        private readonly ColumnTypeDTO[] _columnTypes = [
            new ColumnTypeDTO()
            {
                Id = 0,
                Title = "Однострочный текст",
                Code = "text",
            },
            new ColumnTypeDTO()
            {
                Id = 1,
                Title = "Многострочный текст",
                Code = "multitext",
            },
            new ColumnTypeDTO()
            {
                Id = 2,
                Title = "Число",
                Code = "num",
            },
            new ColumnTypeDTO()
            {
                Id = 3,
                Title = "Еще какой-то тип",
                Code = "something",
            },
        ];
        private readonly List<UserColumnDTO> _userColumns;
        private readonly List<PriceListDTO> _priceLists;

        public DummyRepository()
        {
            _userColumns = new List<UserColumnDTO>()
            {
                new UserColumnDTO()
                {
                    Id = 0,
                    Header = "Колонка 1",
                    Type = _columnTypes[0],
                },
                new UserColumnDTO()
                {
                    Id = 1,
                    Header = "Числовая колонка",
                    Type = _columnTypes[2],
                },
                new UserColumnDTO()
                {
                    Id = 2,
                    Header = "Многострочная колонка",
                    Type = _columnTypes[1],
                },
            };

            _priceLists = new List<PriceListDTO>()
            {
                new PriceListDTO()
                {
                    Id = 0,
                    Name = "KUKUS",
                    Columns = new List<UserColumnDTO>() { _userColumns[0], _userColumns[1], },
                    Products = new List<ProductDTO>()
                    {
                        new ProductDTO()
                        {
                            Id = 0,
                            Code = "Кот",
                            Name = "Котофей",
                            UserColumnValues = new()
                            {
                                { _userColumns[0].Id, "memes" },
                                { _userColumns[1].Id, "1337" },
                            },
                        },
                        new ProductDTO()
                        {
                            Id = 1,
                            Code = "Пес",
                            Name = "Собака",
                            UserColumnValues = new()
                            {
                                { _userColumns[0].Id, "галлон" },
                                { _userColumns[1].Id, "228" },
                            },
                        },
                        new ProductDTO()
                        {
                            Id = 2,
                            Code = "Сыр",
                            Name = "Сыровар",
                            UserColumnValues = new()
                            {
                                { _userColumns[0].Id, "легкое" },
                                { _userColumns[1].Id, "322" },
                            },
                        },
                    }
                },
                new PriceListDTO(){ Id = 1, Name = "LULUS" },
                new PriceListDTO(){ Id = 2, Name = "MEMES" },
                new PriceListDTO(){ Id = 3, Name = "nigga" },
            };
        }

        public bool CreatePriceList(PriceListDTO priceList)
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

        public bool CreateProduct(ProductDTO product)
        {
            product.Id = _priceLists.Max(list => list.Products.Count > 0 ? list.Products.Max(product => product.Id) : -1) + 1;
            PriceListDTO? requiredPriceList = _priceLists.FirstOrDefault(list => list.Id == product.PriceListId);
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
                ProductDTO? productToRemove = list.Products.FirstOrDefault(prod => prod.Id == id);
                if (productToRemove != null)
                {
                    return list.Products.Remove(productToRemove);
                }
            }

            return false;
        }

        public ColumnTypeDTO? GetColumnType(string code)
        {
            return _columnTypes.FirstOrDefault(c => c.Code == code);
        }

        public IEnumerable<ColumnTypeDTO> GetColumnTypes()
        {
            return _columnTypes;
        }

        public PriceListDTO? GetPriceList(int id)
        {
            return _priceLists.FirstOrDefault(list => list.Id == id);
        }

        public IEnumerable<PriceListDTO> GetPriceLists()
        {
            return _priceLists;
        }

        public IEnumerable<UserColumnDTO> GetUnickUserColumns()
        {
            return _userColumns.DistinctBy(column => (column.Header, column.Type?.Code)).ToList();
        }
    }
}
