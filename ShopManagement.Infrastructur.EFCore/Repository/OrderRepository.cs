﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructur.EFCore.Repository
{
    public class OrderRepository:RepositoryBase<long , Order> , IOrderRepository
    {
        public OrderRepository( ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }


        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;


     

        public double GetAmountBy(long id)
        {
            var order = _context.Orders.Select(x => new { x.PayAmount, x.Id })
                .FirstOrDefault(x => x.Id == id);

            if (order != null)
                return order.PayAmount;

            return 0;
        }

        public Order GetBy(long id)
        {

            return  _context.Orders.FirstOrDefault(x => x.Id == id);

            
        }

        public List<OrderViewModel> Serach(OrderSearchModel searchModel)
        {
            var accounts
                = _accountContext.Accounts.Select(x => new { x.Id, x.Fullname }).ToList();
            var query = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                IssueTrackingNo = x.IssueTrackingNo,
                PaymentMethodId = x.PaymentMethod,
                RefId = x.RefId,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi()

            });

            query = query.Where(x => x.IsCanceled == searchModel.IsCanceld);
            
            if(searchModel.AccountId > 0) 
                query = query.Where(x=> x.AccountId == searchModel.AccountId);

            var orders = query.OrderByDescending(x => x.Id).ToList();

            foreach (var order in orders)
            {
                order.AccountFullName = accounts.FirstOrDefault(x=>x.Id == order.AccountId)?.Fullname;
                order.PaymentMethod = PaymentMethod.GetBy(order.PaymentMethodId).Name;

            }

            return orders;
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _context.Products.Select(x => new { x.Id, x.Name }).ToList();
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);

            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.Items.Select(x => new OrderItemViewModel
            {
                Id =x.Id,
                Count = x.Count,
                DiscountRate = x.DiscountRate,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).ToList();

            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }
            return items;

        }
    }
}
