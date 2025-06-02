namespace pja_apbd_cwic12.DTOs;

public class ClientPostDTO
{
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Email { set; get; }
    public string Telephone { set; get; }
    public string Pesel { set; get; }
    public int IdTrip { set; get; }
    public DateTime? PaymentDate { set; get; }
}