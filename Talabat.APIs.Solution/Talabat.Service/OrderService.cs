using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Products_Specs.Order_Specs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        //private readonly IGenericRepository<Order> _ordersRepo;

        public OrderService( IBasketRepository basketRepository ,
                            IUnitOfWork unitOfWork , 
                            IPaymentService paymentService
                            //IGenericRepository<Product> productRepo ,
                            // IGenericRepository<DeliveryMethod> deliveryMethodsRepo ,
                            // IGenericRepository<Order> ordersRepo
                            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            //_productRepo = productRepo;
            //_deliveryMethodsRepo = deliveryMethodsRepo;
            //_ordersRepo = ordersRepo;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1. Get Basket From Baskets Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);
            // 2. Get Selected Items at Basket From Products Repo
            var orderItems = new List<OrderItem>();
            if(basket?.Items?.Count > 0)
            {
                var productRepository =  _unitOfWork.Repository<Product>();

                foreach (var item in basket.Items)
                {
                    var product = await productRepository.GetByIdAsync(item.Id);
                    var productItemOrderd = new ProdcutItemOrdered(item.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrderd, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
            // 3. Calculate SubTotal
            var subtotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);
           // 4. Get Delivery Method From Delivery Repo
           var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            var ordersRepo = _unitOfWork.Repository<Order>();
            var orderSpecs = new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId);
            var existingOrder = await ordersRepo.GetEntityWithSpecAsync(orderSpecs);
            if(existingOrder != null)
            {
                ordersRepo.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }
            // 5. Create Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal , basket.PaymentIntentId );
            await ordersRepo.AddAsync(order);
            // 6. Save To Database [T0D0]
           var result= await _unitOfWork.CompleteAsync();
            if (result <= 0)
                return null;
            return order;

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var ordersRepo =  _unitOfWork.Repository<Order>();
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await ordersRepo.GetAllWithSpecAsync(spec);
            return orders;

        }


        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var orderSpec = new OrderSpecifications(orderId, buyerEmail);
            var order = await orderRepo.GetEntityWithSpecAsync(orderSpec);
            return order;
         }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {

            var deliveryMethodsRepo = _unitOfWork.Repository<DeliveryMethod>();
            var deliveyMethods = await deliveryMethodsRepo.GetAllAsync();
            return deliveyMethods;

        }
    }

}
