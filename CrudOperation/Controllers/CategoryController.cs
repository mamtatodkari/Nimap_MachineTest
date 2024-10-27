using CrudOperation.Data;
using CrudOperation.Models.Model;
using CrudOperation.Models.viewModel;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperation.Controllers
{
    public class CategoryController : Controller
    {
        private productCategoryDbcontext dbcontext;
        public CategoryController( productCategoryDbcontext productCategoryDbcontext)
        {
            this.dbcontext = productCategoryDbcontext;
                
        }
        [HttpGet]
        public IActionResult add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("add")]
        public IActionResult add(addCategoryRequest categoryRequest)
        {
            var Category = new Category
            {
                CategoryName = categoryRequest.CategoryName

            };
            dbcontext.Categories.Add(Category);
            dbcontext.SaveChanges();

            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var lists= dbcontext.Categories.ToList();
            //var lists = dbcontext.Categories.Where(x => x.CategoryId>10).ToList();
            return View(lists);
        }
        [HttpGet]
        [ActionName("Edit")]
        public IActionResult Edit(int id)

        {
            var category=dbcontext.Categories.Find(id);
            if (category != null)
            {
                var EditCategory = new EditCategoryRequest
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                    
                };
                return View(EditCategory);
            }
            else
            {
                return View(null);
            }
          
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(EditCategoryRequest editCategoryRequest)

        {
            var category = new Category
            {
               CategoryId=editCategoryRequest.CategoryId,
               CategoryName=editCategoryRequest.CategoryName
            };
          var existing=  dbcontext.Categories.Find(category.CategoryId);

            if(existing != null)
            {
                existing.CategoryId=category.CategoryId;
                existing.CategoryName=category.CategoryName;
                dbcontext.SaveChanges();
                return RedirectToAction("List");
               
            }
            else
            {
                return RedirectToAction("Edit");
            }
            
               
                
               
            }
        [HttpPost]
        public IActionResult Delete(EditCategoryRequest editCategoryRequest)
        {
            var existing = dbcontext.Categories.Find(editCategoryRequest.CategoryId);
                if(existing != null)
            {
                dbcontext.Categories.Remove(existing);
                dbcontext.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
           
        }

    }

