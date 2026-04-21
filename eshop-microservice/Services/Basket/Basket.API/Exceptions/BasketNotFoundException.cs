using BuildingBlocks.Exceptions;

namespace Basket.API.Exceptions
{
    public class BasketNotFoundException : NotFoundExeption
    {
        public BasketNotFoundException(string userName) :base("Basket", userName)
        {
            
        }
    }
}
