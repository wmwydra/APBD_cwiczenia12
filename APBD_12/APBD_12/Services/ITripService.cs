using APBD_12.DTOs;
using APBD_12.Models;

namespace APBD_12.Services;

public interface ITripService
{
    Task<PaginatedTripResponse> GetSortedTrips(int pageNum, int pageSize);
}