using Microsoft.EntityFrameworkCore;
using pja_apbd_cwic12.DTOs;
using pja_apbd_cwic12.Exceptions;
using pja_apbd_cwic12.Models;

namespace pja_apbd_cwic12.Services;

public class DbService(Tutorial12Context context) : IDbService
{
    public async Task<ResponseTripGetDTO> GetAllTripsAsync(int page, int paginate)
    {
        return new ResponseTripGetDTO()
        {
            PageNum = page,
            PageSize = paginate,
            AllPages = context.Trips.Count() / paginate + context.Trips.Count() % paginate == 0 ? 0 : 1,
            Trips = context.Trips.ToList().Select(e => new TripGetDTO()
            {
                Name = e.Name,
                Description = e.Description,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                MaxPeople = e.MaxPeople,
                Countries = e.IdCountries
                    .Select(c => new CountryGetDTO(){Name = c.Name})
                    .ToList(),
                Clients = e.ClientTrips
                    .Select(c => c.IdClientNavigation)
                    .Select(c => new ClientGetDTO()
                    {
                        FirstName = c.FirstName,
                        LastName = c.LastName
                    }).ToList()
            }
            ).Chunk(paginate)
            .ToArray()[page - 1]
            .ToList()
        };
    }

    public async Task RemoveClientByIdAsync(int idClient)
    {
        if (context.ClientTrips
            .Any(ct => ct.IdClient == idClient)) throw new ClientHasTripsException();
        Client obj = await context.Clients
            .Where(c => c.IdClient == idClient)
            .FirstAsync();
        context.Clients.Remove(obj);
        await context.SaveChangesAsync();
    }

    public async Task AssignClientToTripAsync(int idTrip, ClientPostDTO client)
    {
        if (context.Clients
            .Any(c => c.Pesel.Equals(client.Pesel))) throw new ClientExistsException();

        Trip t = await context.Trips
            .Where(t => t.IdTrip == idTrip)
            .FirstAsync();

        if (t.DateFrom <= DateTime.Now) throw new TripAlreadyStartedException();
        
        await context.Clients.AddAsync(new Client()
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Pesel = client.Pesel,
            Email = client.Email,
            Telephone = client.Telephone
        });
        
        await context.SaveChangesAsync();

        await context.ClientTrips
            .AddAsync(new ClientTrip()
            {
                IdClient = (await context.Clients
                        .Where(c => c.Pesel == client.Pesel)
                        .FirstAsync())
                    .IdClient,
                IdTrip = idTrip,
                PaymentDate = client.PaymentDate,
                RegisteredAt = DateTime.Now
            });
        
        await context.SaveChangesAsync();
    }
}