using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Authorizaton;
using Core.Aspects.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.ResultTool;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Add(Category category)
        {
            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _categoryDal.Add(category);
            return new SuccessResult(Message.CategoryAdded);
        }

        public IResult Delete(Category category)
        {
            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _categoryDal.Delete(category);
            return new SuccessResult(Message.CategoryDeleted);
        }

        [Authorize]
        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(x => x.Id == id));
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Update(Category category)
        {
            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _categoryDal.Update(category);
            return new SuccessResult(Message.CategoryUpdated);
        }
    }
}
