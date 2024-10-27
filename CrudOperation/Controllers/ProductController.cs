using CrudOperation.Data;
using CrudOperation.Models.Model;
using CrudOperation.Models.viewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Controllers
{
    public class ProductController : Controller
    {
        private readonly productCategoryDbcontext productCategory;

        public ProductController(productCategoryDbcontext productCategory)
        {
            this.productCategory = productCategory; 
        }
        public IActionResult List(int pg=0)
        {
            var existingProduct= productCategory.Products.Include("Categorys").ToList();
            const int pageSize = 10;
            if (pg < 0)
            {
                pg = 0;
            }
            int resCount=productCategory.Products.Count();
             var pager=new Pager(resCount,pg,pageSize);
            int recskip = (pg) * pageSize;
            var data=productCategory.Products.Skip(recskip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }
        [HttpGet]
        [ActionName("add")]
        public IActionResult add()

        {
            var categories= productCategory.Categories.ToList();
            var ProductRequest = new addProductRequest
            {
                Products = categories.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text=x.CategoryName, Value = x.CategoryId.ToString() })
            };
            return View(ProductRequest);
        }
        [HttpPost]
        [ActionName("add")]
        public IActionResult add(addProductRequest AddProductRequest) {

            var product = new Product
            {  
                ProductName = AddProductRequest.ProductName,
                CategoryId = Convert.ToInt32(AddProductRequest.SelectedProduct)


        };
            //productCategory.Products.Add(product);
            //productCategory.SaveChanges();
            //return RedirectToAction("List");
            var existingCategory = productCategory.Categories.Find(product.CategoryId);
            if (existingCategory != null)
            {
                productCategory.Products.Add(product);
                productCategory.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Add");
            }



            //List<Category> list = new List<Category>();
            //var categorys = new Product
            //{
            //     ProductName = addProductRequest.ProductName,
            //};
            //foreach(var categoryId in addProductRequest.SelectedProducts)
            //{
            //   int  categoryIdAsint=Convert.ToInt32(categoryId);
            //   var exitingCategory=  productCategory.Categories.Find(categoryIdAsint);
            //    if(exitingCategory != null)
            //    {
            //        list.Add(exitingCategory);
            //    }

            //   categorys.
            //}


        }
        [HttpGet]
        [ActionName("Edit")]
        public IActionResult Edit(int id)
        {
            var product = productCategory.Products.Find(id);
            var categories = productCategory.Categories.ToList();
            if (product != null)
            {
                var editProduct = new EditProductRequest
                {
                 ProductId=product.ProductId,
                  ProductName=product.ProductName,
                  CategoryList = categories.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() })

                };
                return View(editProduct);
            }
            else {
                return View();
            }
           
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(EditProductRequest editProductRequest)
        {
            var products = new Product
            {
                ProductId =editProductRequest.ProductId,
                 ProductName=editProductRequest.ProductName,
                 CategoryId=Convert.ToInt32(editProductRequest.SelectedCategory)

            };
            var existProduct = productCategory.Products.Find(products.ProductId);
            if(existProduct != null)
            {
                existProduct.ProductId = products.ProductId;
                existProduct.ProductName = products.ProductName;
                existProduct.CategoryId = products.CategoryId;
                productCategory.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View("Edit");
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete(EditProductRequest deleteProduct) {
            var product = new Product
            {
                ProductId = deleteProduct.ProductId,
                ProductName = deleteProduct.ProductName,
                CategoryId = Convert.ToInt32(deleteProduct.SelectedCategory)
            };
            var deletedProduct=productCategory.Products.Find(product.ProductId);
            if(deleteProduct != null)
            {
                productCategory.Products.Remove(deletedProduct);
                productCategory.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View(null);
            }
        }
    }
}
