using AutoMapper;
using DevFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using DevFramework.Core.Aspects.Postsharp.CacheAspects;
using DevFramework.Core.Aspects.Postsharp.LogAspects;
using DevFramework.Core.Aspects.Postsharp.PerformanceAspect;
using DevFramework.Core.Aspects.Postsharp.TransactionAspects;
using DevFramework.Core.Aspects.Postsharp.ValidationAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Core.Utilities.Mappings.AutoMapper;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    //[LogAspect(typeof(DatabaseLogger))]
    //[LogAspect(typeof(FileLogger))]
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        //[FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[LogAspect(typeof(DatabaseLogger))]
        //[LogAspect(typeof(FileLogger))]
        public Product Add(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidator(), product);
            return _productDal.Add(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        //[LogAspect(typeof(DatabaseLogger))]
        //[LogAspect(typeof(FileLogger))]
        //[PerformanceCounterAspect(2)]
        //[SecuredOperationAspect(Roles="Admin,Editor,Student")]
        public List<Product> GetAll()
        {
            //Thread.Sleep(3000);
            //return _productDal.GetList().Select(p => new Product
            //{
            //    ProductId = p.ProductId,
            //    CategoryId = p.CategoryId,
            //    ProductName = p.ProductName,
            //    QuantityPerUnit = p.QuantityPerUnit,
            //    UnitPrice = p.UnitPrice
            //}).ToList();

            //List<Product> products = AutoMapperHelper.MapToSameTypeList(_productDal.GetList());
            var products = _mapper.Map<List<Product>>(_productDal.GetList());
            return products;

        }



        public Product GetByid(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }

        [TransactionScopeAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            //using (TransactionScope scope=new TransactionScope())
            //{
            //    try
            //    {
            //        _productDal.Add(product1);
            //        //Business Codes
            //        _productDal.Update(product2);
            //        scope.Complete();
            //    }
            //    catch
            //    {
            //        scope.Dispose();

            //    }
            //}

            _productDal.Add(product1);
            //Business Codes
            _productDal.Update(product2);
        }

        //[FluentValidationAspect(typeof(ProductValidator))]
        public Product Update(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidator(), product);
            return _productDal.Update(product);
        }
    }
}
