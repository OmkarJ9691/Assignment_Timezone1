using System;
using Assignment.Application.Interface;
using Assignment.Domain;
using MediatR;
using Serilog;
using static Assignment.Domain.Guest;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Globalization;
using Assignment.Application.Common;
using Newtonsoft.Json;

namespace Assignment.Application.Features.Guest.Commands
{
	public class AddGuestCommand : IRequest<ResponseModel>
	{
        public Guid Id { get; set; }

        [JsonProperty("Title")]
        public TitleList Title { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("LastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonProperty("BirthDate")]
        public string BirthDate { get; set; } = string.Empty;

        [JsonProperty("Email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("PhoneNumbers")]
        public List<string> PhoneNumbers { get; set; } = new List<string>();

        internal class AddGuestCommandHandler : IRequestHandler<AddGuestCommand, ResponseModel>
        {
            private readonly IApplicationDbContext _context;
            public AddGuestCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseModel> Handle(AddGuestCommand request, CancellationToken cancellationToken)
            {
                ResponseModel responseModel = new ResponseModel();
                ErrorModel errorModel1 = new ErrorModel();
                var guest = new Domain.Guest();

                try
                {
                    var errorModel = request.ValidateGuest();

                    if (string.IsNullOrEmpty(errorModel?.ErrorMessage))
                    {
                        guest.Id = request.Id;
                        guest.Title = Enum.IsDefined(typeof(TitleList), request.Title) == true ? request.Title : TitleList.NA;
                        guest.FirstName = request.FirstName;
                        guest.LastName = request.LastName;
                        guest.BirthDate = request.BirthDate;
                        guest.Email = request.Email;
                        guest.PhoneNumbers = string.Join(",", request.PhoneNumbers);

                        await _context.Guest.AddAsync(guest);

                        await _context.SaveChangesAsync();
                        Log.Information("AddGuestCommand => Guest added successfully. {@Guid}", guest.Id);
                        responseModel.GuestId = guest.Id;
                        responseModel.Status = Status.Success.ToString();
                    }
                    else
                    {
                        responseModel.ErrorModel = errorModel;
                    }
                }
                catch (Exception ex)
                {
                    responseModel.GuestId = guest.Id;
                    errorModel1.ErrorMessage = "Error occoured while saving the guest";
                    responseModel.ErrorModel = errorModel1;
                    Log.Information("AddGuestCommand => ", ex.Message);
                }
                return responseModel;
            }
        }
    }
}

