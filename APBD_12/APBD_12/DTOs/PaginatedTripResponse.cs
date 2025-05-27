namespace APBD_12.DTOs;

public class PaginatedTripResponse
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<TripDTO> Trips { get; set; }
}
