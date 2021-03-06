﻿using System;
using System.Collections.Generic;
using System.Text;
using XaDemo.Data;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Collections;

namespace XaDemo.Services
{
    public class RestaurantManager
    {
        IMobileServiceTable<Restaurant> restaurants;
        MobileServiceClient client;
       
        public RestaurantManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            this.restaurants = client.GetTable<Restaurant>();
   
            
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }
     
        public async Task GenerateRandomData()
        {
            List<Restaurant> resList = new List<Restaurant> {
                new Restaurant("Burger King", "Tainan", "Fast food for burger", "Fast Food" ),
                new Restaurant("Mac Donald", "US", "Best Burger for American", "Fast Food"),
                new Restaurant("KFC", "Tainan", "The Best Fried Chicked", "Fast Food"),
                new Restaurant("Subway", "Tainan", "Good Salad", "Fast Food")
            };

            foreach (var res in resList)
            {
                await restaurants.InsertAsync(res);

            }            
        }

        //從資料庫抓資料
        public async Task<Object> GetRandomRestaurant()
        {
            var allRestaruant = await restaurants.Where(r => r.School == Constants.SchoolID).ToListAsync(); //抓符合學校ID的所有資料
            if (allRestaruant.Count == 0)
                return null;
            var rand = new Random();
            return allRestaruant[rand.Next(allRestaruant.Count)];   //回傳一筆隨機資料
        }
    }
}
