namespace ArchitectXpress.Models;

public class PassengerInfoEvent
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Discount { get; set; }

    public PassengerInfoEvent()
    {

    }

    public PassengerInfoEvent(string name, int discount)
    {
        Name = name;
        Discount = discount;
    }
}
