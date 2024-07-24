using ExpertCenter.Domain;
using ExpertCenter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpertCenter.Controllers
{
    public class PriceListsController : Controller
    {
        List<PriceList> _lists;
        UserColumn kukusColumn, lulusColumn;

        ColumnType[] _columnTypes = [
            new ColumnType()
            {
                Title = "Однострочный текст",
                Code = "text",
            },
            new ColumnType()
            {
                Title = "Многострочный текст",
                Code = "multitext",
            },
            new ColumnType()
            {
                Title = "Число",
                Code = "num",
            },
        ];

        public PriceListsController()
        {
            UserColumn kukusColumn = new UserColumn()
            {
                Id = 0,
                Header = "kukus",
                Type = _columnTypes[0],
            };
            UserColumn lulusColumn = new UserColumn()
            {
                Id = 1,
                Header = "multiText",
                Type = _columnTypes[1],
            };

            _lists = new List<PriceList>()
            {
                new PriceList()
                {
                    Id = 0,
                    Name = "KUKUS",
                    Columns = new List<UserColumn>() { kukusColumn, lulusColumn, },
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Id = 0,
                            Code = "Кот",
                            Name = "Котофей",
                            Properties = new Dictionary<UserColumn, string>()
                            {
                                { kukusColumn, "memes" },
                                { lulusColumn, "полиморфизм" },
                            }
                        },
                        new Product()
                        {
                            Id = 1,
                            Code = "Пес",
                            Name = "Собака",
                            Properties = new Dictionary<UserColumn, string>()
                            {
                                { kukusColumn, "галлон" },
                                { lulusColumn, "насвай" },
                            }
                        },
                        new Product()
                        {
                            Id = 2,
                            Code = "Сыр",
                            Name = "Сыровар",
                            Properties = new Dictionary<UserColumn, string>()
                            {
                                { kukusColumn, "легкое" },
                                { lulusColumn, "чижик" },
                            }
                        },
                    }
                },
                new PriceList(){ Id = 1, Name = "LULUS" },
                new PriceList(){ Id = 2, Name = "MEMES" },
                new PriceList(){ Id = 3, Name = "nigga" },
            };
        }

        public IActionResult All()
        {
            return View("All", _lists);
        }

        public IActionResult PriceList(int id)
        {
            PriceList? requiredList = _lists.FirstOrDefault(x => x.Id == id);
            if (requiredList == null)
                return NotFound();

            return View("PriceList", requiredList);
        }

        public IActionResult New()
        {
            PriceList newPriceList = new PriceList();
            newPriceList.Columns = new List<UserColumn>()
            {
                new UserColumn() { Header = "Text column", Type = _columnTypes[0] },
                new UserColumn() { Header = "Num column", Type = _columnTypes[2] },
            };

            NewPriceListViewModel vm = new(newPriceList,
            [
                new ColumnType()
                {
                    Title = "Однострочный текст",
                    Code = "text",
                },
                new ColumnType()
                {
                    Title = "Многострочный текст",
                    Code = "multitext",
                },
                new ColumnType()
                {
                    Title = "Число",
                    Code = "num",
                },
            ]);

            return View("New", vm);
        }

        public IActionResult AddProduct(int priceListId)
        {
            Product product = new Product()
            {
                Properties = new Dictionary<UserColumn, string>()
                {
                    { kukusColumn, "" },
                    { lulusColumn, "" },
                }
            };

            return View("AddProduct", product);
        }
    }
}
