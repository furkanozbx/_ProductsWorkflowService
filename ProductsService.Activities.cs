using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ServiceModel;
using _ProductsModel;

namespace _ProductsWorkflowService
{
    
    public sealed class ProductExists : CodeActivity<Boolean>
    {
        public InArgument<NorthwindEntities> Database { get; set; }
        public InArgument<int> ProductId { get; set; }


        protected override bool Execute(CodeActivityContext context)
        {
            int productId = ProductId.Get(context);
            NorthwindEntities database = Database.Get(context);
            int numProducts = (from p in database.Products
                               where p.ProductID == productId
                               select p).Count();
            return numProducts > 0;
        }

    }

    public sealed class FindProduct : CodeActivity<Product>
    {
        public InArgument<NorthwindEntities> Database { get; set; }
        public InArgument<int> ProductId { get; set; }

        protected override Product Execute(CodeActivityContext context)
        {
            int productId = ProductId.Get(context);
            NorthwindEntities database = Database.Get(context);
            Product matchingProduct = database.Products.First(p=>p.ProductID == productId);
            return matchingProduct;
        }
    }
}
