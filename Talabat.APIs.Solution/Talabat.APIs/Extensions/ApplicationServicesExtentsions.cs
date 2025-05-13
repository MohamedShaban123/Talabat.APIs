using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
    public  static class ApplicationServicesExtentsions
    {


        public static IServiceCollection AddApplicationServices( this IServiceCollection services )
        {
             services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
             services.AddScoped(typeof(IProductService), typeof(ProductService));
             services.AddScoped(typeof(IOrderService), typeof(OrderService));
             services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));




            //webApplicationBuilder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile( new MappingProfile());
            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {

                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                          .SelectMany(P => P.Value.Errors)
                                                          .Select(E => E.ErrorMessage)
                                                          .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);


                };

            });


            //services.AddScoped<IBasketRepository,BasketRepository>();   //old method
             services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository)); // new method
           

            return services;


        }
    }
}
