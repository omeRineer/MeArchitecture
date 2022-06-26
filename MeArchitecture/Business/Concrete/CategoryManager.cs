using Business.Abstract;
using Business.Contains.Messages;
using Business.ValidationRules.FluentValidation;
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

        public IResult Add(Category category)
        {
            ValidationTool<Category>.Validate(new CategoryValidator(), category);

            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult Delete(Category category)
        {
            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(x => x.Id == id));
        }

        public IResult Update(Category category)
        {
            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
    }
}
