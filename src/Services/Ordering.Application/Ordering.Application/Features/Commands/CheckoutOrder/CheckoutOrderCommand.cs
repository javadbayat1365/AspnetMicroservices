using MediatR;

namespace Ordering.Application.Features.Commands.CheckoutOrder;

public record CheckoutOrderCommand(
 string UserName,
 decimal TotalPrice,
 string FirstName ,
 string LastName ,
 string EmailAddress,
 string AddressLine,
 string Country ,
 string State ,
 string ZipCode,
 string CardName ,
 string CardNumber,
 string Expiration ,
 string CVV ,
 int PaymentMethod):IRequest<int>;
