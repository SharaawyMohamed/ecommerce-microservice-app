namespace Basket.API.Models
{
	public record BasketChekoutDtoV1(
		 string UserName,
		 decimal TotalPrice,
		 string FirstName,
		 string LastName,
		 string EmailAddress,
		 string AddressLine,
		 string Country,
		 string State,
		 string ZipCode,
		 string CardName,
		 string CardNumber,
		 string Expiration,
		 string CVV,
		 int PaymentMethod);
}
