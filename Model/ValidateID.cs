namespace Unach.Inventory.API.Model;
public class ValidateID {
    public Boolean IsValid( string Id ) {
        if( Id.Length != 36 ) {
            return false;
        }

        return true;
    }

    public Object Message {
        get {
            var FormatIdError = new {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = "One or more validation errors occurred.",
                status = 400,
                errors = new {
                    Id = new string[]{ "The ID is required and/or the format of the is Incorrect." }
                }
            };

            return FormatIdError;
        }
    }
}
