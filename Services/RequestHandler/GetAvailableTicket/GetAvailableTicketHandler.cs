using Contracts.RequestModel.GetAvailableTicket;
using Contracts.ResponseModel.GetAvailableTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandler.GetAvailableTicket
{
    public class GetAvailableTicketHandler : IRequestHandler<GetAvailableTicketRequest, GetAvailableTicketResponse>
    {
        private readonly DBContext _db;

        public GetAvailableTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetAvailableTicketResponse> Handle(GetAvailableTicketRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<AvailableTicketData> query = _db.AvailableTickets
                    .Select(Q => new AvailableTicketData
                    {
                        EventDate = Q.EventDate,
                        Quota = Q.Quota,
                        TicketName = Q.TicketName,
                        CategoryName = Q.CategoryName,
                        TicketCode = Q.TicketCode,
                        Price = Q.Price,
                    })
                    .AsNoTracking();

                    if (request.EventDateMin != DateTimeOffset.MinValue && request.EventDateMax != DateTimeOffset.MinValue)
                        {
                            query = query.Where(Q => Q.EventDate >= request.EventDateMin && Q.EventDate <= request.EventDateMax);
                        }
                    else if (request.EventDateMin != DateTimeOffset.MinValue)
                        { 
                            query = query.Where(Q => Q.EventDate >= request.EventDateMin);
                        }
                    else if (request.EventDateMax != DateTimeOffset.MinValue)
                        {
                            query = query.Where(Q => Q.EventDate <= request.EventDateMax);
                        }

                    if (!string.IsNullOrEmpty(request.CategoryName))
                        {
                            query = query.Where(Q => Q.CategoryName == request.CategoryName);
                        }

                    if (!string.IsNullOrEmpty(request.TicketCode))
                    {
                        query = query.Where(Q => Q.TicketCode == request.TicketCode);
                    }

                    if (!string.IsNullOrEmpty(request.TicketName))
                    {
                        query = query.Where(Q => Q.TicketName == request.TicketName);
                    }

                    if (request.Price != default(decimal))
                    {
                        query = query.Where(Q => Q.Price <= request.Price);
                    }

                if (string.IsNullOrEmpty(request.OrderBy))
                {
                    request.OrderBy = "ASC";
                }

                if (string.IsNullOrEmpty(request.OrderState))
                {
                    request.OrderState = "TicketCode";
                }

                if (string.Equals(request.OrderState, "ASC", StringComparison.OrdinalIgnoreCase) || string.Equals(request.OrderState, "Ascending", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(request.OrderState, "TicketName", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderBy(Q => Q.TicketName);
                    }
                    else if (string.Equals(request.OrderState, "TicketCode", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderBy(Q => Q.TicketCode);
                    }
                }
                else if (string.Equals(request.OrderState, "DESC", StringComparison.OrdinalIgnoreCase) || string.Equals(request.OrderState, "Descending", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(request.OrderState, "TicketName", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderByDescending(Q => Q.TicketName);
                    }
                    else if (string.Equals(request.OrderState, "TicketCode", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderByDescending(Q => Q.TicketCode);
                    }
                }

                if (string.Equals(request.OrderBy, "ASC", StringComparison.OrdinalIgnoreCase) || string.Equals(request.OrderBy, "Ascending", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(request.OrderState, "TicketName", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderBy(Q => Q.TicketName);
                    }
                    else if (string.Equals(request.OrderState, "TicketCode", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderBy(Q => Q.TicketCode);
                    }
                }
                else if (string.Equals(request.OrderBy, "DESC", StringComparison.OrdinalIgnoreCase) || string.Equals(request.OrderBy, "Descending", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(request.OrderState, "TicketName", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderByDescending(Q => Q.TicketName);
                    }
                    else if (string.Equals(request.OrderState, "TicketCode", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderByDescending(Q => Q.TicketCode);
                    }
                }


                var datas = await query.ToListAsync(cancellationToken);

                    var response = new GetAvailableTicketResponse
                    {
                        AvailableTickets = datas
                    };

                    return response;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred: {ex.Message}");

                    var emptyResponse = new GetAvailableTicketResponse
                    {
                        AvailableTickets = new List<AvailableTicketData>()
                    };

                    return emptyResponse;
                }
        }
    }
}
