
using System;
using System.Diagnostics;
#region Q1
// a)
// 1- Fields (Owner, Balance) are public → anyone can change them directly
// 2- Withdraw has no validation → can withdraw more than balance

// b)
// - Make fields private
// - Use properties
// - Add validation in Withdraw

// c)
// Public fields are bad because:
// - No control on data
// - Can break the object easily
#endregion


#region Q2
// Field: normal variable inside class
// Property: controlled access using get/set

// Yes, property can contain logic

using System.Diagnostics;

public double Price = 100;

public double PriceWithTax
{
    get { return Price * 1.14; }
}
#endregion


#region Q3
// a)
// this[int index] is called an Indexer
// It lets you use object like an array → obj[0]

// b)
// register[10] → runtime error (out of range)

// safer version:
public string this[int index]
{
    get
    {
        if (index >= 0 && index < names.Length)
            return names[index];
        return null;
    }
    set
    {
        if (index >= 0 && index < names.Length)
            names[index] = value;
    }
}

// c)
// Yes, class can have more than one indexer
// Example: access by string (search by name)
#endregion


#region Q4
// a)
// static means shared between all objects
// TotalOrders is shared
// Item is for each object

// b)
// No
// static method cannot access non-static fields directly
#endregion

#region Q11
public enum TicketType
{
    Standard,
    VIP,
    IMAX
}

public struct SeatLocation
{
    public char Row;
    public int Number;

    public override string ToString()
    {
        return $"{Row}-{Number}";
    }
}
#endregion


#region Q12
public class Ticket
{
    private string movieName;
    private double price;

    public string MovieName
    {
        get { return movieName; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                movieName = value;
        }
    }

    public TicketType Type { get; set; }

    public SeatLocation Seat { get; set; }

    public double Price
    {
        get { return price; }
        set
        {
            if (value > 0)
                price = value;
        }
    }

    public double PriceAfterTax
    {
        get { return Price * 1.14; }
    }

    private static int ticketCounter = 0;

    public int TicketId { get; private set; }

    public Ticket()
    {
        ticketCounter++;
        TicketId = ticketCounter;
    }

    public static int GetTotalTicketsSold()
    {
        return ticketCounter;
    }
}
#endregion


#region Q13
public class Cinema
{
    private Ticket[] tickets = new Ticket[20];

    public Ticket this[int index]
    {
        get
        {
            if (index >= 0 && index < tickets.Length)
                return tickets[index];
            return null;
        }
        set
        {
            if (index >= 0 && index < tickets.Length)
                tickets[index] = value;
        }
    }

    public Ticket GetByMovie(string name)
    {
        foreach (var t in tickets)
        {
            if (t != null && t.MovieName == name)
                return t;
        }
        return null;
    }

    public bool AddTicket(Ticket t)
    {
        for (int i = 0; i < tickets.Length; i++)
        {
            if (tickets[i] == null)
            {
                tickets[i] = t;
                return true;
            }
        }
        return false;
    }
}
#endregion


#region Q14
public static class BookingHelper
{
    private static int counter = 0;

    public static double CalcGroupDiscount(int numberOfTickets, double pricePerTicket)
    {
        double total = numberOfTickets * pricePerTicket;

        if (numberOfTickets >= 5)
            return total * 0.9;

        return total;
    }

    public static string GenerateBookingReference()
    {
        counter++;
        return $"BK-{counter}";
    }
}
#endregion


#region Q15
class Program
{
    static void Main()
    {
        Cinema cinema = new Cinema();

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Enter Ticket {i + 1}");

            Ticket t = new Ticket();

            Console.Write("Movie Name: ");
            t.MovieName = Console.ReadLine();

            Console.Write("Type (0=Standard,1=VIP,2=IMAX): ");
            t.Type = (TicketType)int.Parse(Console.ReadLine());

            Console.Write("Row: ");
            char row = Console.ReadLine()[0];

            Console.Write("Seat Number: ");
            int num = int.Parse(Console.ReadLine());

            t.Seat = new SeatLocation { Row = row, Number = num };

            Console.Write("Price: ");
            t.Price = double.Parse(Console.ReadLine());

            cinema.AddTicket(t);
        }

        Console.WriteLine("\nAll Tickets:");

        for (int i = 0; i < 3; i++)
        {
            var t = cinema[i];
            Console.WriteLine($"#{t.TicketId} | {t.MovieName} | {t.Type} | Seat {t.Seat} | {t.Price} | {t.PriceAfterTax}");
        }

        Console.Write("\nSearch Movie: ");
        string name = Console.ReadLine();

        var found = cinema.GetByMovie(name);

        if (found != null)
            Console.WriteLine($"Found: {found.MovieName}");
        else
            Console.WriteLine("Not Found");

        Console.WriteLine($"\nTotal Tickets: {Ticket.GetTotalTicketsSold()}");

        Console.WriteLine(BookingHelper.GenerateBookingReference());
        Console.WriteLine(BookingHelper.GenerateBookingReference());

        Console.WriteLine($"Discount: {BookingHelper.CalcGroupDiscount(5, 80)}");
    }
}
#endregion
