using pja_apbd_cwic12.DTOs;

namespace pja_apbd_cwic12.Services;

public interface IDbService
{
    public Task<ResponseTripGetDTO> GetAllTripsAsync(int page, int paginate);

    public Task RemoveClientByIdAsync(int idClient);

    public Task AssignClientToTripAsync(int idTrip, ClientPostDTO client);
}