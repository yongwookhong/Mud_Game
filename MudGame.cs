using System;
using System.Collections.Generic;
using System.Numerics;

namespace MUD_GAME
{
    internal class Player
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public List<Item> Inventory { get; set; }

        public Player()
        {
            Level = 1;
            Name = "Chad";
            Job = "전사";
            Attack = 10;
            Defense = 5;
            Health = 100;
            Gold = 1500;
            Inventory = new List<Item>();
        }
    }

    internal class Shop
    {
        public int Gold { get; set; }
        public List<Item> Items { get; set; }

        public Shop()
        {
            Gold = 800;
            Items = new List<Item>
            {
                new Item("수련자 갑옷", 1000, "방어력 +5 | 수련에 도움을 주는 갑옷입니다."),
                new Item("무쇠갑옷", 1500, "방어력 +9 | 무쇠로 만들어져 튼튼한 갑옷입니다."),
                new Item("스파르타의 갑옷", 3500, "방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다."),
                new Item("낡은 검", 600, "공격력 +2 | 쉽게 볼 수 있는 낡은 검입니다."),
                new Item("청동 도끼", 1500, "공격력 +5 | 어디선가 사용됐던거 같은 도끼입니다."),
                new Item("스파르타의 창", 2000, "공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다.")
            };
        }
    }

    internal class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public Item(string name, int price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }
    }

    internal class MudGame
    {
        static Player player = new Player(); // Player 객체 생성
        static Shop shop = new Shop();       // Shop 객체 생성

        static void Main(string[] args)
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            while (true)
            {
                Console.WriteLine("\n1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        DisplayPlayerStatus();
                        break;
                    case "2":
                        ManageInventory();
                        break;
                    case "3":
                        DisplayShop();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }

        static void DisplayPlayerStatus()
        {
            Console.WriteLine("\n상태 보기");
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            Console.WriteLine($"공격력 : {player.Attack}");
            Console.WriteLine($"방어력 : {player.Defense}");
            Console.WriteLine($"체 력 : {player.Health}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            Console.ReadLine(); // 사용자 입력 받아오지만 여기서는 무시
        }

        static void ManageInventory()
        {
            Console.WriteLine("\n인벤토리");
            Console.WriteLine("보유 중인 아이템을 전부 보여줍니다.");
            Console.WriteLine("이때 장착중인 아이템 앞에는 [E] 표시를 붙여 줍니다.");
            Console.WriteLine("처음 시작시에는 아이템이 없습니다.");

            while (true)
            {
                Console.WriteLine("\n[아이템 목록]");
                DisplayInventory();

                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        ManageEquippedItems();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                        break;
                }
            }
        }

        static void DisplayInventory()
        {
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Item item = player.Inventory[i];
                string equippedStatus = player.Inventory.Contains(item) ? "[E]" : "";
                Console.WriteLine($"{i + 1}. {equippedStatus}{item.Name} | 가격: {item.Price} G | {item.Description}");
            }
        }

        static void ManageEquippedItems()
        {
            Console.WriteLine("\n장착 관리");
            DisplayInventory();

            Console.Write("장착 또는 해제할 아이템 번호를 입력하세요 (0. 나가기): ");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int itemIndex) && itemIndex >= 1 && itemIndex <= player.Inventory.Count)
            {
                Item selectedItem = player.Inventory[itemIndex - 1];

                if (player.Inventory.Contains(selectedItem))
                {
                    player.Inventory.Remove(selectedItem);
                    Console.WriteLine($"{selectedItem.Name}을(를) 해제했습니다.");
                }
                else
                {
                    player.Inventory.Add(selectedItem);
                    Console.WriteLine($"{selectedItem.Name}을(를) 장착했습니다.");
                }
            }
            else if (userInput == "0")
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
            }
        }

        static void DisplayShop()
        {
            Console.WriteLine("\n상점");
            Console.WriteLine($"보유 골드: {shop.Gold} G");

            while (true)
            {
                Console.WriteLine("\n[아이템 목록]");
                DisplayShopItems();

                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        BuyItem();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                        break;
                }
            }
        }

        static void DisplayShopItems()
        {
            for (int i = 0; i < shop.Items.Count; i++)
            {
                Item item = shop.Items[i];
                string purchaseStatus = player.Inventory.Contains(item) ? " (구매완료)" : "";
                Console.WriteLine($"{i + 1}. {item.Name} | 가격: {item.Price} G | {item.Description}{purchaseStatus}");
            }
        }

        static void BuyItem()
        {
            Console.WriteLine("\n아이템 구매");
            DisplayShopItems();

            Console.Write("구매할 아이템 번호를 입력하세요 (0. 나가기): ");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int itemIndex) && itemIndex >= 1 && itemIndex <= shop.Items.Count)
            {
                Item selectedShopItem = shop.Items[itemIndex - 1];

                if (player.Inventory.Contains(selectedShopItem))
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
                else if (player.Gold >= selectedShopItem.Price)
                {
                    player.Gold -= selectedShopItem.Price;
                    player.Inventory.Add(selectedShopItem);
                    Console.WriteLine($"{selectedShopItem.Name}을(를) 구매했습니다.");
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
            else if (userInput == "0")
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
            }
        }
    }
}
