namespace RentApp.Collection;

public class Office : RealEstate
{
    public static Office CreateInstance()
    {
        return new Office();
    }


    public int FloorNumber { get; set; }
}