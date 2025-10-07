using System;
using System.Collections.Generic;

namespace VendingStuff
{
    public class MoneyProcessor
    {
        private decimal currentAmount = 0;
        private List<decimal> validCoins = new List<decimal> { 0.25m, 0.50m, 1.00m, 2.00m, 5.00m };
        
        public void AddCoin(decimal coin)
        {
            if (!validCoins.Contains(coin))
            {
                Console.WriteLine($"Монета {coin} не принимается");
                return;
            }
            
            currentAmount += coin;
            Console.WriteLine($"Внесено: {coin:C}. Всего внесено: {currentAmount:C}");
        }
        
        public decimal CheckBalance() => currentAmount;
        
        public void ClearBalance() => currentAmount = 0;
        
        public decimal CalculateChange(decimal itemPrice)
        {
            decimal change = currentAmount - itemPrice;
            currentAmount = 0;
            return change;
        }
    }
}