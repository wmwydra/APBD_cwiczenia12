using APBD_12.DTOs;
using APBD_12.Models;
using APBD_12.Repositories;

namespace APBD_12.Services;

public class ClientService : IClientService
{
    private readonly IDbRepository _dbRepository;

    public ClientService(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }
    
    public async Task DeleteClientAync(int IdClient, CancellationToken cancellationToken)
    {
        var hasTrips = await _dbRepository.ClientHasTripsAsync(IdClient, cancellationToken);
        if (hasTrips)
            throw new InvalidOperationException("Client has trips");
        await _dbRepository.DeleteClientAsync(IdClient, cancellationToken);
    }

    public async Task AddClientToTripAsync(ClientTripDTO clientTrip, CancellationToken cancellationToken)
    {
        //1. Sprawdź czy klient isntieje
        var clientExists = await _dbRepository.DoesClientExistAsync(clientTrip.Pesel, cancellationToken);
        if (clientExists) 
            throw new InvalidOperationException("Client already exists");
        
        var client = new Client 
        {
            FirstName = clientTrip.FirstName, 
            LastName = clientTrip.LastName, 
            Email = clientTrip.Email, 
            Telephone = clientTrip.Telephone, 
            Pesel = clientTrip.Pesel
        };
                    
        await _dbRepository.AddClient(client, cancellationToken);            
        
        //2. Sprawdź czy klient już jest zapisany
        var clientRegistered = await _dbRepository.ClientAlreadyRegisteredAsync(
            clientTrip.IdTrip, clientTrip.Pesel, cancellationToken);
        if (clientRegistered)
            throw new InvalidOperationException("Client is already registered on this trip");
        
        //3. Sprawdź czy wycieczka istnieje i jest w przyszłości
        var tripExists = await _dbRepository.DoesTripExistAsync(clientTrip.IdTrip, cancellationToken);
        var tripInTheFuture = await _dbRepository.TripInTheFuture(clientTrip.IdTrip, cancellationToken);
        if (!tripExists || !tripInTheFuture)
            throw new InvalidOperationException("Trip does not exists or has already happened");    
        
        var clientTripEntity = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = clientTrip.IdTrip,
            RegisteredAt = DateTime.UtcNow,
            PaymentDate = clientTrip.PaymentDate != default ? clientTrip.PaymentDate : null
        };

        await _dbRepository.AddClientToTripAsync(clientTripEntity, cancellationToken);
        
    }
}