﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.API.Extensions;
using PetFamily.Application.Commands.Volunteer.Update;
using PetFamily.Domain.Shared.Models;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects.Ids;
using PetFamily.Infrastructure.Database;

namespace PetFamily.Infrastructure.CommandHandlers.VolunteerCommands
{
    internal class UpdateVolunteerMainInfoHandler : IUpdateVolunteerMainInfoHandler
    {
        private readonly PetFamilyDbContext _context;
        private readonly ILogger<UpdateVolunteerMainInfoHandler> _logger;
        private readonly IValidator<UpdateVolunteerMainInfoCommand> _validator;

        public UpdateVolunteerMainInfoHandler(
            PetFamilyDbContext context,
            ILogger<UpdateVolunteerMainInfoHandler> logger,
            IValidator<UpdateVolunteerMainInfoCommand> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(UpdateVolunteerMainInfoCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = _validator.Validate(command);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var volunteerId = VolunteerId.Create(command.Id);

            var fullname = FullName.Create(command.Fullname.Firstname, command.Fullname.Lastname, command.Fullname.Patronymic).Value;

            var description = Description.Create(command.Description).Value;

            var workExperience = WorkExperience.Create(command.WorkExperience).Value;

            var email = EmailAddress.Parse(command.Email).Value;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

            var volunteer = await _context.Volunteers.FirstOrDefaultAsync(v => v.Id == volunteerId);

            List<Error> errors = [];

            if (volunteer is null)
            {
                errors.Add(Error.NotFound("Volunteer.Not.Found", "A volunteer with this ID was not found."));
            }
            else
            {
                if (await _context.Volunteers.FirstOrDefaultAsync(v => v.Id != volunteerId && v.Email == email, cancellationToken) is not null)
                    errors.Add(Errors.General.Conflict.IsExists("volunteer", "email"));

                if (await _context.Volunteers.FirstOrDefaultAsync(v => v.Id != volunteerId && v.PhoneNumber == phoneNumber, cancellationToken) is not null)
                    errors.Add(Errors.General.Conflict.IsExists("volunteer", "phone number"));
            }

            if (errors.Count > 0)
                return errors;

            volunteer!.UpdateMainInfo(fullname, description, workExperience, email, phoneNumber);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Update basic information about the volunteer with ID: {id}.",
                command.Id);

            return volunteer.Id.Value;
        }
    }
}
