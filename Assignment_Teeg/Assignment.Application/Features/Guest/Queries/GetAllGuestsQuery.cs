using System;
using Assignment.Application.Interface;
using Assignment.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Assignment.Application.Features.Guest.Queries
{
	public class GetAllGuestsQuery : IRequest<IEnumerable<GuestModel>>
	{
        internal class GetAllGuestsQueryHandler : IRequestHandler<GetAllGuestsQuery, IEnumerable<GuestModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllGuestsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<GuestModel>> Handle(GetAllGuestsQuery request, CancellationToken cancellationToken)
            {
                var guest = await _context.Guest.ToListAsync();
                List<GuestModel> result = new List<GuestModel>();
                try
                {
                    foreach (var item in guest)
                    {
                        GuestModel guestModel = new GuestModel();
                        guestModel.Id = item.Id;
                        guestModel.Title = (GuestModel.TitleList)item.Title;
                        guestModel.FirstName = item.FirstName;
                        guestModel.LastName = item.LastName;
                        guestModel.BirthDate = item.BirthDate;
                        guestModel.Email = item.Email;
                        guestModel.PhoneNumbers = item.PhoneNumbers.Split(',').ToList();

                        result.Add(guestModel);
                    }
                    Log.Information("GetAllGuestQuery => All guest retrieve successfully.");
                }
                catch (Exception ex)
                {
                    Log.Information("GetAllGuestQuery => ", ex.Message);
                }
                return result;
            }
        }
    }
}

