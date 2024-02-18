using System;
using Assignment.Application.Interface;
using Assignment.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Assignment.Application.Features.Guest.Queries
{
	public class GetGuestByIdQuery : IRequest<GuestModel>
	{
        public Guid Id { get; set; }

        internal class GetGuestByIdQueryHandler : IRequestHandler<GetGuestByIdQuery, GuestModel>
        {
            private readonly IApplicationDbContext _context;
            public GetGuestByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<GuestModel> Handle(GetGuestByIdQuery request, CancellationToken cancellationToken)
            {
                var guest = await _context.Guest.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                GuestModel result = new GuestModel();

                try
                {
                    if (guest != null)
                    {
                        result.Id = guest.Id;
                        result.Title = (GuestModel.TitleList)guest.Title;
                        result.FirstName = guest.FirstName;
                        result.LastName = guest.LastName;
                        result.BirthDate = guest.BirthDate;
                        result.Email = guest.Email;
                        result.PhoneNumbers = guest.PhoneNumbers.Split(',').ToList();

                        Log.Information("GetGuestByIdQuery => Guest retrieve successfully. {@Guid}", guest.Id);
                    }
                    else
                    {
                        Log.Information("GetGuestByIdQuery => No guest found. {@Guid}", request.Id);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Log.Information("GetGuestByIdQuery => ", ex.Message);
                }
                return result;
            }
        }
    }
}

