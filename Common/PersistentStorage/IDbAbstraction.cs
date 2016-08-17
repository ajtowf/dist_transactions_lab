using System.Collections.Generic;
using Common.Entities;

namespace Common.PersistentStorage
{
    public interface IDbAbstraction
    {
        IList<Item> GetAll();

        Item Write(Item item);
    }
}
