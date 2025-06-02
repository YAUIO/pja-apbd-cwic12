namespace pja_apbd_cwic12.DTOs;

public class TripGetDTO
{
    public string Name { set; get; }
    public string Description { set; get; }
    public DateTime DateFrom { set; get; }
    public DateTime DateTo { set; get; }
    public int MaxPeople { set; get; }
    public List<CountryGetDTO> Countries { set; get; }
    public List<ClientGetDTO> Clients { set; get; }
}