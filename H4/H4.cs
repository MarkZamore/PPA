// SRP

public class Order
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}

public class PriceCalculator
{
    public double CalculateTotalPrice(Order order)
    {
        return order.Quantity * order.Price * 0.9;
    }
}

public class PaymentProcessor
{
    public void ProcessPayment(string paymentDetails)
    {
        Console.WriteLine("Payment processed using: " + paymentDetails);
    }
}

public class EmailService
{
    public void SendConfirmationEmail(string email)
    {
        Console.WriteLine("Confirmation email sent to: " + email);
    }
}

// OCP

public abstract class Employee
{
    public string Name { get; set; }
    public double BaseSalary { get; set; }
    public abstract double CalculateSalary();
}

public class PermanentEmployee : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary * 1.2;
    }
}

public class ContractEmployee : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary * 1.1;
    }
}

public class InternEmployee : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary * 0.8;
    }
}

// ISP

public interface IPrinter
{
    void Print(string content);
}

public interface IScanner
{
    void Scan(string content);
}

public interface IFax
{
    void Fax(string content);
}

public class AllInOnePrinter : IPrinter, IScanner, IFax
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }

    public void Scan(string content)
    {
        Console.WriteLine("Scanning: " + content);
    }

    public void Fax(string content)
    {
        Console.WriteLine("Faxing: " + content);
    }
}

public class BasicPrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }
}

// DIP

public interface INotificationSender
{
    void Send(string message);
}

public class EmailSender : INotificationSender
{
    public void Send(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}

public class SmsSender : INotificationSender
{
    public void Send(string message)
    {
        Console.WriteLine("SMS sent: " + message);
    }
}

public class NotificationService
{
    private readonly INotificationSender _notificationSender;

    public NotificationService(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    public void SendNotification(string message)
    {
        _notificationSender.Send(message);
    }
}