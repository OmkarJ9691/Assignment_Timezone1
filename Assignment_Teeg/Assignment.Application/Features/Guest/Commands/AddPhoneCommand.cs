using System;
using Assignment.Application.Common;
using Assignment.Application.Interface;
using Assignment.Domain;
using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace Assignment.Application.Features.Guest.Commands
{
	public class AddPhoneCommand : IRequest<ResponseModel>
	{
        public Guid Id { get; set; }

        [JsonProperty("PhoneNumbers")]
        public List<string> PhoneNumbers { get; set; } = new List<string>();

        internal class AddPhoneCommandHandler : IRequestHandler<AddPhoneCommand, ResponseModel>
        {
            private readonly IApplicationDbContext _context;
            public AddPhoneCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseModel> Handle(AddPhoneCommand request, CancellationToken cancellationToken)
            {
                ResponseModel responseModel = new ResponseModel();
                ErrorModel errorModel1 = new ErrorModel();
                var guest = new Domain.Guest();
                try
                {
                    if (request.PhoneNumbers.IsValidPhoneNumber())
                    {
                        guest = await _context.Guest.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                        if (guest != null)
                        {
                            List<string> phoneNumbers = guest.PhoneNumbers.Split(',').ToList();
                            phoneNumbers = phoneNumbers.Union(request.PhoneNumbers).ToList();
                            guest.PhoneNumbers = string.Join(",", phoneNumbers);

                            await _context.SaveChangesAsync();
                            Log.Information("AddPhoneCommand => Guest phone number added successfully. {@Guid}", guest.Id);
                            responseModel.GuestId = guest.Id;
                            responseModel.Status = Status.Success.ToString();
                        }
                        else
                        {
                            Log.Information("AddPhoneCommand => Guest not available. {@Guid}", request.Id);
                            responseModel.GuestId = request.Id;
                            responseModel.Status = Status.Failure.ToString();
                            errorModel1.ErrorMessage = "Guest Id is not present.";
                            responseModel.ErrorModel = errorModel1;
                        }
                    }
                    else
                    {
                        errorModel1.ErrorMessage = "Please enter a valid phone number.";
                        responseModel.ErrorModel = errorModel1;
                    }
                }
                catch (Exception ex)
                {
                    Log.Information("AddPhoneCommand => ", ex.Message);
                    responseModel.GuestId = guest != null ? guest.Id : request.Id;
                    responseModel.ErrorModel.ErrorMessage = "Error occoured while saving the guest phone number";                    
                }
                return responseModel;
            }
        }
    }
}

