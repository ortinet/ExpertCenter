using ExpertCenter.Domain;
using ExpertCenter.Models;
using ExpertCenter.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpertCenter.Controllers
{
    public class PriceListsController : Controller
    {
        private readonly string UnableToCreatePriceListMessage = "Не удалось создать прайс-лист";
        private readonly IRepository _repository;

        public PriceListsController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult All()
        {
            return View("All", _repository.GetPriceLists());
        }

        [HttpGet]
        public IActionResult PriceList(int id)
        {
            PriceList? requiredList = _repository.GetPriceList(id);
            if (requiredList == null)
                return NotFound();

            return View("PriceList", requiredList);
        }

        [HttpGet]
        public IActionResult New()
        {
            PriceList newPriceList = new PriceList();
            newPriceList.Columns = _repository.GetUnickUserColumns().ToList();

            NewPriceListViewModel vm = new(newPriceList, _repository.GetColumnTypes());
            return View("New", vm);
        }

        [HttpPost]
        public IActionResult CreateNew()
        {
            var name = Request.Form["name"].FirstOrDefault();
            if (string.IsNullOrEmpty(name))
                return Content(UnableToCreatePriceListMessage);

            var columnsHeaders = Request.Form["columnHeader"];
            var columnsTypeCodes = Request.Form["columnTypeCode"];

            PriceList newPriceList = new PriceList();
            newPriceList.Name = name;
            for (int i = 0; i < columnsHeaders.Count; i++)
            {
                var currentHeader = columnsHeaders[i];
                var currentTypeCode = columnsTypeCodes[i];
                if (string.IsNullOrEmpty(currentHeader) || string.IsNullOrEmpty(currentTypeCode))
                    return Content(UnableToCreatePriceListMessage);

                var currentColumnType = _repository.GetColumnType(currentTypeCode);
                if (currentColumnType == null)
                    return Content(UnableToCreatePriceListMessage);

                UserColumn column = new UserColumn()
                {
                    Header = currentHeader,
                    Type = currentColumnType,
                };
                newPriceList.Columns.Add(column);
            }

            bool creationResult = _repository.CreatePriceList(newPriceList);
            if (creationResult)
                return RedirectToAction("All");
            else return BadRequest();
        }

        [HttpGet]
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

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            //var requiredPriceList = _repository.GetPriceList(priceListId);
            //if (requiredPriceList == null) return NotFound();

            //Product product = new Product();
            //foreach (var column in requiredPriceList.Columns)
            //{
            //    product.Properties.Add(column, null);
            //}

            return View("AddProduct", product);
        }
    }
}
