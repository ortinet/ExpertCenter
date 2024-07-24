using ExpertCenter.Domain;
using ExpertCenter.Models;
using ExpertCenter.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpertCenter.Controllers
{
    public class PriceListsController : Controller
    {
        private readonly IRepository _repository;

        public PriceListsController()
        {
            _repository = new DummyRepository();
        }

        public IActionResult All()
        {
            return View("All", _repository.GetPriceLists());
        }

        public IActionResult PriceList(int id)
        {
            PriceList? requiredList = _repository.GetPriceList(id);
            if (requiredList == null)
                return NotFound();

            return View("PriceList", requiredList);
        }

        public IActionResult New()
        {
            PriceList newPriceList = new PriceList();
            newPriceList.Columns = _repository.GetUnickUserColumns().ToList();

            NewPriceListViewModel vm = new(newPriceList, _repository.GetColumnTypes());
            return View("New", vm);
        }

        public IActionResult AddProduct(int priceListId)
        {
            var requiredPriceList = _repository.GetPriceList(priceListId);
            if (requiredPriceList == null) return NotFound();

            Product product = new Product();
            foreach (var column in requiredPriceList.Columns)
            {
                product.Properties.Add(column, null);
            }

            return View("AddProduct", product);
        }
    }
}
