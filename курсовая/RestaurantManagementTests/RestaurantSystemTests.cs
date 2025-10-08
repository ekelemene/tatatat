using System;
using System.Collections.Generic;
using Xunit;
using ConsoleApp1; // пространство имён твоей программы

namespace RestaurantManagementTests
{
    public class RestaurantSystemTests
    {
        private RestaurantSystem system;

        public RestaurantSystemTests()
        {
            system = new RestaurantSystem();
            system.InitializeData(); // подгружаем данные
        }

        [Fact]
        public void RegisterClient_ShouldAddNewClient()
        {

            var client = system.RegisterClient("Тестовый Клиент", "9999999999");
            Assert.NotNull(client);
            Assert.True(client.Id > 0);
            Assert.Contains(client, system.GetClients());
        }

        [Fact]
        public void ReserveTable_ShouldReturnTrueForAvailableTable()
        {
            var client = system.RegisterClient("Бронирующий", "8888888888");
            bool result = system.ReserveTable(client.Id, 3, DateTime.Now);
            Assert.True(result);
        }

        [Fact]
        public void CreateOrder_ShouldAddOrderSuccessfully()
        {
            var client = system.RegisterClient("Заказчик", "7777777777");
            system.ReserveTable(client.Id, 3, DateTime.Now);
            var dishIds = new List<int> { 1, 2 };
            bool created = system.CreateOrder(client.Id, 3, dishIds);
            Assert.True(created);
        }

        [Fact]
        public void GetClientOrders_ShouldReturnOrdersForClient()
        {
            var client = system.RegisterClient("История", "6666666666");
            system.ReserveTable(client.Id, 3, DateTime.Now);
            var dishIds = new List<int> { 1, 2 };
            system.CreateOrder(client.Id, 3, dishIds);
            var orders = system.GetClientOrders(client.Id);
            Assert.NotEmpty(orders);
        }

        [Fact]
        public void GetPopularDishes_ShouldReturnTop3()
        {
            var dishes = system.GetPopularDishes();
            Assert.True(dishes.Count <= 3);
        }

        [Fact]
        public void GetAverageCheck_ShouldReturnNonNegative()
        {
            var avg = system.GetAverageCheck(DateTime.Parse("2025-05-01"), DateTime.Parse("2025-06-01"));
            Assert.True(avg >= 0);
        }

        [Fact]
        public void GetTotalRevenue_ShouldBePositive()
        {
            var revenue = system.GetTotalRevenue();
            Assert.True(revenue >= 0);
        }

        [Fact]
        public void AddWaiter_ShouldAddNewWaiter()
        {
            int oldCount = system.GetWaiters().Count;
            system.AddWaiter("Новый Официант", new List<int> { 1 });
            int newCount = system.GetWaiters().Count;
            Assert.True(newCount > oldCount);
        }
    }
}
