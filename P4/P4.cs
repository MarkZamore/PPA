using System;
using System.Collections.Generic;

public class Order
{
    public List<(string Product, int Quantity, double Price)> Items { get; private set; } = new List<(string, int, double)>();
    public IPayment Payment { get; set; }
    public IDelivery Delivery { get; set; }
    public double Price { get; private set; }

    public void AddItem(string product, int quantity, double price)
    {
        Items.Add((product, quantity, price));
    }

    public void CalculatePrice(DiscountCalculator discountCalculator)
    {
        double subtotal = 0;
        foreach (var item in Items)
        {
            subtotal += item.Quantity * item.Price;
        }
        Price = discountCalculator.ApplyDiscount(subtotal);
    }

    public void ProcessOrder()
    {
        Payment.ProcessPayment(Price);
        Delivery.DeliverOrder(this);
    }
}

public interface IPayment
{
    void ProcessPayment(double amount);
}

public class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"{amount} Credit Card");
    }
}

public class PayPalPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"{amount} PayPal");
    }
}

public class BankTransferPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"{amount} Bank Transfer.");
    }
}

public interface IDelivery
{
    void DeliverOrder(Order order);
}

public class CourierDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Доставлено курьером");
    }
}

public class PostDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Доставлено почтой");
    }
}

public class PickUpPointDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Доставлено в пункт самовывоза");
    }
}

public interface INotification
{
    void SendNotification(string message);
}

public class EmailNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Уведомление по почте: {message}");
    }
}

public class SmsNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Уведомление по смс: {message}");
    }
}

public abstract class DiscountCalculator
{
    public abstract double ApplyDiscount(double totalAmount);
}

public class NoDiscount : DiscountCalculator
{
    public override double ApplyDiscount(double totalAmount)
    {
        return totalAmount;
    }
}

public class PercentageDiscount : DiscountCalculator
{
    private readonly double _percentage;
    public PercentageDiscount(double percentage)
    {
        _percentage = percentage;
    }

    public override double ApplyDiscount(double totalAmount)
    {
        return totalAmount - (totalAmount * _percentage / 100);
    }
}

public class NotificationService
{
    private readonly INotification _notification;

    public NotificationService(INotification notification)
    {
        _notification = notification;
    }

    public void NotifyClient(string message)
    {
        _notification.SendNotification(message);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Order order = new Order();
        order.AddItem("Вещь 1", 1, 50);
        order.AddItem("Вущь 2", 2, 100);

        order.Payment = new CreditCardPayment();
        order.Delivery = new CourierDelivery();

        DiscountCalculator discountCalculator = new PercentageDiscount(10);
        order.CalculatePrice(discountCalculator);

        order.ProcessOrder();

        NotificationService notificationService = new NotificationService(new EmailNotification());
        notificationService.NotifyClient("Заказ оформлен");
    }
}