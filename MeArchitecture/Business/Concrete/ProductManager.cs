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
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            ValidationTool<Product>.Validate(new ProductValidator(), product);

            var businessRules = BusinessTool.Run
                (

                );
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(x => x.Id == id));
        }

        public IResult Update(Product product)
        {
            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }



        private IResult ExistProduct(Product product)
        {
            var result = _productDal.Get(x => x.ProductName == product.ProductName);
            if (result != null)
            {
                return new ErrorResult("Product is exist");
            }
            return new SuccessResult();
        }
        private IResult ProductLimit()
        {
            var result = _productDal.GetAll().Count();
            if (result >= 15)
            {
                return new ErrorResult("Product limit is full");
            }
            return new SuccessResult();
        }
    }
}
