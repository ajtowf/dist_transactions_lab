using System.Collections.Generic;
using Common.Entities;

namespace ApplicationService.PersistentStorage
{
    public interface IDbAbstraction
    {
        IList<Item> GetAll();

        Item Write(Item item);
    }
}
