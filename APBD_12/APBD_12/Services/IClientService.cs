using APBD_12.DTOs;

namespace APBD_12.Services;

public interface IClientService
{
    Task DeleteClientAync(int IdClient, CancellationToken cancellationToken);
    Task AddClientToTripAsync(ClientTripDTO clientTrip, CancellationToken cancellationToken);
}