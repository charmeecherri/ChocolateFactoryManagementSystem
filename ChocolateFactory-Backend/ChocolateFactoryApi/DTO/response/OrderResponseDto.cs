﻿using ChocolateFactoryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.response
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string status { get; set; }
    }
}
