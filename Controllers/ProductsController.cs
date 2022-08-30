using API_Aplication.Data;
using API_Aplication.Generics;
using API_Aplication.Model;
using API_Aplication.Model.ViewModel;
using API_Aplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ApplicationDbContext _dbContext;
        public ProductsController(IProductsRepository productsRepository, ApplicationDbContext dbContext)
        {
            _productsRepository = productsRepository;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetAllProducts()
        {
            AResponse<List<ProductsViewModel>> aResponse = new AResponse<List<ProductsViewModel>>();
            try
            {
                //get all products in db
                var retrivedproducts = await _productsRepository.GetProducts();
                var productViewModel = retrivedproducts.Select(x => new ProductsViewModel
                {
                    id = x.id,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    Amount = x.Amount
                }).ToList();

                aResponse.Successful = true;
                aResponse.Message = "Retrived All Products";
                aResponse.Data = productViewModel;


            }
            catch (Exception e)
            {
                aResponse.Successful = false;
                aResponse.Message = e.Message;
            }
            return Ok(aResponse);
        }

        [HttpGet("{id}")]
        /// <summary>
        /// get a single product
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAProduct(string id)
        {

            AResponse<ProductsViewModel> aResponse = new AResponse<ProductsViewModel>();
            try
            {
                var retriveAProduct = await _productsRepository.GetProductById(id);
                ProductsViewModel productviewmodel = new ProductsViewModel
                {
                    id = retriveAProduct.id,
                    Amount = retriveAProduct.Amount,
                    Quantity = retriveAProduct.Quantity,
                    Name = retriveAProduct.Name
                };

                aResponse.Message = "Successfully retrived a Product";
                aResponse.Data = productviewmodel;
                aResponse.Successful = true;

            }
            catch (Exception e)
            {
                aResponse.Successful = false;
                aResponse.Message = e.Message;
            }
            return Ok(aResponse);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductsViewModel productsViewModel)
        {
            //initialize AResponse
            AResponse<Products> aResponse = new AResponse<Products>();
            try
            {
                var product = new Products()
                {
                    id = productsViewModel.id,
                    Name = productsViewModel.Name,
                    Amount = productsViewModel.Amount,
                    Quantity = productsViewModel.Quantity,
                    Date = DateTime.Now
                };

                Products productCreated = await _productsRepository.CreateProduct(product);

                if (productCreated == null)
                {
                    throw new InvalidOperationException("could not add product to db");
                }
                aResponse.Message = "Successfully created Product";
                aResponse.Data = productCreated;
                aResponse.Successful = true;

                return Ok(aResponse);
            }
            catch (Exception e)
            {
                aResponse.Successful = false;
                aResponse.Message = e.Message;

                return Ok(aResponse);
            }
        }

        [HttpPut("{id}")]
        /// <summary>
        /// update a single product
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UpdateProduct(string id, Products products)
        {
            //intialize aResponse
            AResponse<Products> aResponse = new AResponse<Products>();

            if(id != products.id)
            {
                aResponse.Successful = false;
                aResponse.Message = "Product Not Updated";
                return Ok(aResponse);
            }

            try
            {
                await _productsRepository.UpdateProducts(products);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            aResponse.Data = products;
            aResponse.Successful = true;
            aResponse.Message = "Your Product has been Updated";
            return Ok(aResponse);
        }

        [HttpDelete("{id}")]
        /// <summary>
        /// delete a single product
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DeleteProduct(string id)
        {
            //initialize Aresponse
            AResponse<string> aResponse = new AResponse<string>();

            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                aResponse.Successful = false;
                aResponse.Message = "Product is not Found for deletion";
                return Ok(aResponse);
                
            }

            await _productsRepository.DeleteProduct(product);
            


            aResponse.Successful = true;
            aResponse.Message = "Product was deleted Successfully";
            return Ok(aResponse);
        }

    }
}
