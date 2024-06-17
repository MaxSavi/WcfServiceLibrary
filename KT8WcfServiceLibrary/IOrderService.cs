using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KT8WcfServiceLibrary
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        Task<List<Book>> GetBooksAsync();

        [OperationContract]
        Task<Book> GetBookByIdAsync(int bookId);

        [OperationContract]
        Task AddBookAsync(Book book);


        [OperationContract]
        Task<List<Order>> GetOrdersAsync();

        [OperationContract]
        Task<Order> GetOrderByIdAsync(int orderId);

        [OperationContract]
        Task AddOrderAsync(Order order);
    }

    [DataContract]
    public class Book
    {
        [DataMember]
        public int BookId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public DateTime PublishedDate { get; set; }
    }

    // Класс данных
    [DataContract]
    public class Order
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public List<OrderItem> Items { get; set; }
    }

    [DataContract]
    public class OrderItem
    {
        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}