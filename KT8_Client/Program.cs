using KT8WcfServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KT8_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("\nSync Client:\n");
            var binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.Message; //шифрование
            binding.Security.Message.ClientCredentialType =
            MessageCredentialType.Windows; 
            var factory = new ChannelFactory<IOrderService>(binding, new EndpointAddress("http://localhost:8733/Design_Time_Addresses/KT8WcfServiceLibrary/OrderService/mx"));
            var client = factory.CreateChannel(); // создаем соединение 
            repeat:
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Выберите опцию:");
            Console.WriteLine("Добавление OrderItem - введите 1");
            Console.WriteLine("Вывод всех GetOrdersAsync - введите 2");
            Console.WriteLine("Фильтрация GetOrdersAsync по OrderId - введите 3 ");
            Console.WriteLine("Добавление книжки - введите 4");
            Console.WriteLine("Вывод всех книжек - введите 5");
            Console.WriteLine("Фильтрация по GetBookByIdAsync по BookId введите - 6");
            Console.WriteLine("---------------------------------------------------------------");

            //var client = factory.CreateChannel();
            int comand = Convert.ToInt32(Console.ReadLine());
            
            switch (comand)
            {
                case 0:

                    break;
                case 1:
                    Console.WriteLine("Ввидите  OrderId:");
                    int OrderId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ввидите ItemId:");
                    int ItemId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ввидите CustomerName:");
                    string CustomerName = Console.ReadLine();
                    Console.WriteLine("Ввидите Количество:");
                    int Quantity = Convert.ToInt32(Console.ReadLine());
                    List<OrderItem> orderItems = new List<OrderItem>();
                    OrderItem orderItem = new OrderItem() { ItemId = ItemId, ProductName = CustomerName, Quantity = Quantity };

                    orderItems.Add(orderItem);
                    Order order = new Order()
                    {
                        OrderId = OrderId,
                        CustomerName = CustomerName,
                        OrderDate = DateTime.Now,
                        Items = orderItems,
                    };
                    await client.AddOrderAsync(order);
                    goto repeat;
                case 2:
                    Console.WriteLine("Вывод всех GetOrdersAsync:");
                    var asyncOrders = await client.GetOrdersAsync();
                    foreach (var orders in asyncOrders)
                    {
                        Console.WriteLine($"Order ID: {orders.OrderId}, Customer Name: {orders.CustomerName}, Order Date: {orders.OrderDate}");
                    }
                    goto repeat;

                case 3:
                    Console.WriteLine("Фильтрацию по OrderId, введите Id для фильтрации: ");
                    int OrderIds = Convert.ToInt32(Console.ReadLine());
                    var asyncSingleOrder = await client.GetOrderByIdAsync(OrderIds);
                    Console.WriteLine($"Single Order - ID: {asyncSingleOrder.OrderId}, Customer Name: {asyncSingleOrder.CustomerName}");
                    goto repeat;

                case 4:
                    Console.WriteLine("Ввидите  Author:");
                    string Author = Console.ReadLine();
                    Console.WriteLine("Ввидите BookId:");
                    int BookId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ввидите Title:");
                    string Title = Console.ReadLine();

                    var book = new Book
                    {
                        Author = Author,
                        BookId = BookId,
                        PublishedDate = DateTime.Now,
                        Title = Title,
                    };
                    await client.AddBookAsync(book);
                    goto repeat;

                case 5:
                    Console.WriteLine("Вывод всех GetBooksAsync:");
                    //Получаем список всех GetBooksAsync 
                    var asyncbook = await client.GetBooksAsync();
                    foreach (var bookS in asyncbook)
                    {
                        Console.WriteLine($"BookId ID: {bookS.BookId}, Customer Title: {bookS.Title}, Order PublishedDate: {bookS.PublishedDate}");
                    }
                    goto repeat;
                case 6:
                    Console.WriteLine("Фильтрацию по BookId, введите Id для фильтрации: ");
                    int GetBookByIdAsync = Convert.ToInt32(Console.ReadLine());
                    //Console.WriteLine("\nAsync Client:\n"); 
                    var books1 = await client.GetBookByIdAsync(GetBookByIdAsync);
                    Console.WriteLine($"BookId ID: {books1.BookId}, Customer Title: {books1.Title}, Order PublishedDate: {books1.PublishedDate}");

                    goto repeat;
            }

            //var Order1 = new Order //создаем очередь
            //{
            //    OrderId = 2,
            //    CustomerName = "Jane Doe",
            //    OrderDate = DateTime.Now,
            //    Items = new List<OrderItem>
            //            {
            //                new OrderItem { ItemId = 1, ProductName = "Smartphone", Quantity = 1 },
            //                new OrderItem { ItemId = 2, ProductName = "Headphones", Quantity = 2 }
            //            }
            //};
            //await client.AddOrderAsync(Order1); //добавление очереди 
            //List<string> list = new List<string>();
            //var asyncOrders = await client.GetOrdersAsync();
            //foreach (var order in asyncOrders)
            //{
            //    Console.WriteLine($"Order ID: {order.OrderId}, Customer Name: {order.CustomerName}, Order Date: {order.OrderDate}");
            //}

            //Console.WriteLine("\nAsync Client:\n");
            //var asyncSingleOrder = await client.GetOrderByIdAsync(2);
            //Console.WriteLine($"Single Order - ID: {asyncSingleOrder.OrderId}, Customer Name: {asyncSingleOrder.CustomerName}");
            //var book = new Book
            //{
            //    Author="Максим Максимыч",
            //    BookId =1,
            //    PublishedDate = DateTime.Now,
            //    Title = "Акакий",
            //};
            //await client.AddBookAsync(book);
            //book.BookId = 2;
            //book.Author = "Пупа Пупкин";
            //book.Title = "Максиму 70 баллов!";
            //await client.AddBookAsync(book);


            ////Получаем список всех заказов
            //var asyncbook = await client.GetBooksAsync();
            //foreach (var bookS in asyncbook)
            //{
            //    Console.WriteLine($"BookId ID: {bookS.BookId}, Customer Title: {bookS.Title}, Order PublishedDate: {bookS.PublishedDate}");
            //}


            //Console.WriteLine("\nAsync Client:\n");
            //var book1 = await client.GetBookByIdAsync(1);
            //Console.WriteLine($"BookId ID: {book1.BookId}, Customer Title: {book1.Title}, Order PublishedDate: {book1.PublishedDate}");

            Console.ReadLine();
        }
    }
}