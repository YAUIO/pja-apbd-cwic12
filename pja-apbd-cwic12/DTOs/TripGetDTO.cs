namespace pja_apbd_cwic12.DTOs;

public class TripGetDTO
{
    public string Name { set; get; }
    public string Description { set; get; }
    public DateOnly DateFrom { set; get; }
    public DateOnly DateTo { set; get; }
    public int MaxPeople { set; get; }
    public List<CountryGetDTO> Countries { set; get; }
    public List<ClientGetDTO> Clients { set; get; }
}