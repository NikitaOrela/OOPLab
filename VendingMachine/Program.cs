using System;

namespace VendingStuff
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            var stuffManager = new MachineContents();
            var cashHandler = new MoneyProcessor();
            bool keepRunning = true;

            Console.WriteLine("Торговый автомат приветствует вас! ");

            while (keepRunning)
            {
                Console.WriteLine("\n ЧТО ХОТИТЕ СДЕЛАТЬ? ");
                Console.WriteLine("1. Посмотреть товары");
                Console.WriteLine("2. Вставить монету");
                Console.WriteLine("3. Купить товар");
                Console.WriteLine("4. Вернуть деньги");
                Console.WriteLine("5. Режим администратора");
                Console.WriteLine("6. Выйти");
                Console.Write("Выберите действие: ");

                string whatToDo = Console.ReadLine();

                switch (whatToDo)
                {
                    case "1":
                        stuffManager.ShowAllItems();
                        break;

                    case "2":
                        Console.Write("Какую монету кидаете? (0.25, 0.50, 1.00, 2.00, 5.00): ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal coinAmount))
                        {
                            cashHandler.AddCoin(coinAmount);
                        }
                        else
                        {
                            Console.WriteLine("Что-то не то ввели");
                        }
                        break;

                    case "3":
                        stuffManager.ShowAllItems();
                        Console.Write("Какой товар берете? ");
                        if (int.TryParse(Console.ReadLine(), out int productNumber))
                        {
                            if (stuffManager.ProcessPurchase(productNumber - 1, cashHandler.CheckBalance()))
                            {
                                decimal changeMoney = cashHandler.CalculateChange(
                                    stuffManager.FindItem(productNumber - 1)?.PriceValue ?? 0);
                                if (changeMoney > 0)
                                {
                                    Console.WriteLine($"Ваша сдача: {changeMoney:C}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Не понял какой товар");
                        }
                        break;

                    case "4":
                        decimal moneyBack = cashHandler.CheckBalance();
                        cashHandler.ClearBalance();
                        Console.WriteLine($"Забирайте ваши {moneyBack:C}");
                        break;

                    case "5":
                        Console.Write("Введите код администратора: ");
                        string secretCode = Console.ReadLine();
                        if (secretCode == "7777")
                        {
                            AdminStuff(stuffManager);
                        }
                        else
                        {
                            Console.WriteLine("Неверный код");
                        }
                        break;

                    case "6":
                        keepRunning = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }
            }

            Console.WriteLine("Ждем вас снова!");
        }

        static void AdminStuff(MachineContents manager)
        {
            bool adminMode = true;
            
            while (adminMode)
            {
                Console.WriteLine("\n РЕЖИМ АДМИНИСТРАТОРА ");
                Console.WriteLine("1. Добавить новый товар");
                Console.WriteLine("2. Забрать деньги из автомата");
                Console.WriteLine("3. Выйти из режима администратора");
                Console.Write("Выберите действие: ");

                string adminAction = Console.ReadLine();

                switch (adminAction)
                {
                    case "1":
                        Console.Write("Как назовем товар?: ");
                        string name = Console.ReadLine();
                        Console.Write("Сколько стоит?: ");
                        decimal price = decimal.Parse(Console.ReadLine());
                        Console.Write("Сколько штук?: ");
                        int count = int.Parse(Console.ReadLine());
                        Console.Write("Какая категория?: ");
                        string cat = Console.ReadLine();
                        
                        manager.AddProductToMachine(name, price, count, cat);
                        Console.WriteLine("Товар добавлен в автомат");
                        break;

                    case "2":
                        decimal moneyCollected = manager.TakeEarnings();
                        Console.WriteLine($"Забрали из автомата: {moneyCollected:C}");
                        break;

                    case "3":
                        adminMode = false;
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }
    }
}