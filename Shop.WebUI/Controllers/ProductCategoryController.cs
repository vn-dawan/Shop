using Shop.Core.Models;
using Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        InMemoryRepository<ProductCategory> context;

        public ProductCategoryController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }


        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory p = new ProductCategory();
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                context.Insert(p);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory p, int id)
        {
            ProductCategory pCategoryToEdit = context.FindById(id);
            try
            {
                if (pCategoryToEdit == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(pCategoryToEdit);
                    }
                    else
                    {
                        //context.Update(pToEdit);
                        pCategoryToEdit.Category = p.Category;
                        context.Commit();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                ProductCategory pCategoryToDelete = context.FindById(id);
                if (pCategoryToDelete == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    context.Delete(id);
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
    }
}