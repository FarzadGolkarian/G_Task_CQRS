using System;

namespace G_Task.Common.Exceptions;

public static class ErrorMessages
{
    public const string NationalCodeInvalid = "National Code {0} Is duplicate.";
    public const string ValidationNationalCodeInvalid = "National Code {0} Is Not Valid.";
    public const string PersonCreated = "Person created successfully.";
    public const string PersonUpdated = "Person updated successfully.";
    public const string PersonCreatedError = "An error occurred while creating the person.";
    public const string PersonUpdatingError = "An error occurred while updating the person.";
    public const string PersonAddress = "Entering an address is required.";
    public const string PersonAddressType = "Entering an AddressType is required.";
    public const string PersonNotFound = "Person with code {0} NotFound.";
    public const string ChangePersonStatus = "Change Person Status successfully.";
    public const string ChangeAddressStatus = "Change Address Status successfully.";
    public const string ChangePersonStatusError = "An error occurred while Change Person Status.";
    public const string ChangeAddressStatusError = "An error occurred while Change Address Status.";
    public const string PersonDeleted = "Person deleted successfully.";
    public const string AddressDeleted = "Address deleted successfully.";
    public const string PersonDeletedError = "An error occurred while deleting the person.";
    public const string ValidationEnumType = " Type with name {0} not found.";

}