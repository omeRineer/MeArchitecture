using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core;
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
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            var businessRules = BusinessTool.Run
                (

                );
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _productDal.Add(product);
            return new SuccessResult(Message.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _productDal.Delete(product);
            return new SuccessResult(Message.ProductDeleted);
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(x => x.Id == id));
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            var businessRules = BusinessTool.Run();
            if (!businessRules.Success)
            {
                return businessRules;
            }

            _productDal.Update(product);
            return new SuccessResult(Message.ProductUpdated);
        }



        private IResult ExistProduct(Product product)
        {
            var result = _productDal.Get(x => x.ProductName == product.ProductName);
            if (result != null)
            {
                return new ErrorResult(Message.ProductAlreadyExist);
            }
            return new SuccessResult();
        }
        private IResult ProductLimit()
        {
            var result = _productDal.GetAll().Count();
            if (result >= 15)
            {
                return new ErrorResult(Message.ProductLimitIsFull);
            }
            return new SuccessResult();
        }
    }
}
