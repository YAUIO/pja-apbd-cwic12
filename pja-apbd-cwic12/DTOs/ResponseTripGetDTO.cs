namespace pja_apbd_cwic12.DTOs;

public class ResponseTripGetDTO
{
    public int PageNum { set; get; }
    public int PageSize { set; get; }
    public int AllPages { set; get; }
    public List<TripGetDTO> Trips { set; get; }
}