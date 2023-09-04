namespace RentApp.Collection;

public class Apartament : RealEstate
{
    public static Apartament CreateInstance()
    {
        return new Apartament();
    }

    public int FloorNumber { get; set; }
}