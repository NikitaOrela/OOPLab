using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingStuff
{
    public class MachineContents
    {
        private List<ItemInMachine> allItems = new List<ItemInMachine>
        {
            new ItemInMachine { ProductName = "Шоколадка", PriceValue = 1.50m, AvailablePieces = 10, Type = "Сладкое" },
            new ItemInMachine { ProductName = "Чипсы", PriceValue = 2.00m, AvailablePieces = 8, Type = "Снеки" },
            new ItemInMachine { ProductName = "Кола", PriceValue = 1.25m, AvailablePieces = 6, Type = "Напитки" },
            new ItemInMachine { ProductName = "Конфеты", PriceValue = 1.00m, AvailablePieces = 9, Type = "Сладкое" },
            new ItemInMachine { ProductName = "Вода", PriceValue = 0.75m, AvailablePieces = 7, Type = "Напитки" },
            new ItemInMachine { ProductName = "Сухарики", PriceValue = 1.75m, AvailablePieces = 5, Type = "Снеки" }
        };

        public void ShowAllItems()
        {
            Console.WriteLine("\n ДОСТУПНЫЕ ТОВАРЫ: ");
            for (int i = 0; i < allItems.Count; i++)
            {
                var product = allItems[i];
                Console.WriteLine($"{i + 1}. {product.ProductName} - {product.PriceValue:C} (в наличии: {product.AvailablePieces})");
            }
        }

        public ItemInMachine FindItem(int number)
        {
            if (number >= 0 && number < allItems.Count)
                return allItems[number];
            return null;
        }

        public bool ProcessPurchase(int itemNumber, decimal userMoney)
        {
            if (itemNumber < 0 || itemNumber >= allItems.Count)
            {
                Console.WriteLine("Нет такого номера");
                return false;
            }

            var chosenProduct = allItems[itemNumber];
            
            if (chosenProduct.AvailablePieces <= 0)
            {
                Console.WriteLine("Уже всё раскупили");
                return false;
            }

            if (userMoney < chosenProduct.PriceValue)
            {
                Console.WriteLine($"Не хватает денег! Добавьте {chosenProduct.PriceValue - userMoney:C}");
                return false;
            }

            chosenProduct.AvailablePieces--;
            Console.WriteLine($"Заберите ваш {chosenProduct.Type}");
            return true;
        }

        public void AddProductToMachine(string productName, decimal cost, int howMany, string productType)
        {
            allItems.Add(new ItemInMachine 
            { 
                ProductName = productName, 
                PriceValue = cost, 
                AvailablePieces = howMany, 
                Type = productType 
            });
        }

        public decimal TakeEarnings()
        {
            decimal totalCash = allItems.Sum(item => item.PriceValue * (10 - item.AvailablePieces));
            foreach (var product in allItems)
            {
                product.AvailablePieces = 10;
            }
            return totalCash;
        }
    }
}