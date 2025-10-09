using ParsMedeQ.Application.Persistance.Schema;

namespace ParsMedeQ.Infrastructure.Services.CartServices;

public class CartStockValidator
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public CartStockValidator(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task ValidateCartsAsync()
    {
        var carts = await _writeUnitOfWork.CartWriteRepository.GetCartsAsync();

        foreach (var cart in carts)
        {
            bool modified = false;

            foreach (var item in cart.CartItems.ToList())
            {
                var product = (await _writeUnitOfWork.ProductWriteRepository.FindById(item.ProductId)).Value;
                if (product == null)
                {
                    await _writeUnitOfWork.CartWriteRepository.RemoveFromCartAsync(cart.UserId, cart.AnonymousId, item.Id);
                    modified = true;
                    continue;
                }

                if (product.Stock == 0)
                {
                    // محصول ناموجود → حذف از سبد
                    await _writeUnitOfWork.CartWriteRepository.RemoveFromCartAsync(cart.UserId, cart.AnonymousId, item.Id);
                    modified = true;
                }
                else if (item.Quantity > product.Stock)
                {
                    // اصلاح تعداد
                    item.Quantity = product.Stock;
                    modified = true;
                }
            }

            if (modified)
            {
                Console.WriteLine($"Cart {cart.Id} اصلاح شد.");
                await _writeUnitOfWork.SaveChangesAsync();
            }
        }
    }
}
