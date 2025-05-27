using APBD_12.Models;

namespace APBD_12.Repositories;

public interface IDbRepository
{
    Task<Trip> GetTripByIdAsync(int IdTrip, CancellationToken cancellationToken);
    Task<List<Trip>> GetTripsAsync();
    
    Task DeleteClientAsync(int IdClient, CancellationToken cancellationToken);
    
    Task<bool> ClientHasTripsAsync(int IdClient, CancellationToken cancellationToken);
    
    Task<bool> DoesClientExistAsync(string Pesel, CancellationToken cancellationToken);
    
    Task<bool> ClientAlreadyRegisteredAsync(int IdTrip, string Pesel, CancellationToken cancellationToken);
    
    Task<bool> DoesTripExistAsync(int IdTrip, CancellationToken cancellationToken);
    Task<bool> TripInTheFuture(int IdTrip, CancellationToken cancellationToken);
    
    Task AddClient(Client client, CancellationToken cancellationToken);

    Task AddClientToTripAsync(ClientTrip clientTrip, CancellationToken cancellationToken);

}