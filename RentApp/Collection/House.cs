namespace RentApp.Collection;

public class House : RealEstate
{
    public static House CreateInstance()
    {
        return new House();
    }

    public int NumbersOfFloors { get; set; }
}