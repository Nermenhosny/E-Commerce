﻿using StackExchange.Redis;
using Store.Repository.BasketRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository.BasketRepository
{
    public class BasketRepository : IBasketRepository
    {
        public BasketRepository(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();
        }

        private readonly IDatabase database;

        public async Task<bool> DeleteBasketAsync(string basketId)
      => await database.KeyDeleteAsync(basketId);
        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
              var  data = await database.StringGetAsync(basketId);
            if (data.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var isCreated = await database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if (!isCreated)
                return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
