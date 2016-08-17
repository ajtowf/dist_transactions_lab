using Common.Entities;
using FluentNHibernate.Mapping;

namespace Common.PersistentStorage.NHibernate
{
    public class ItemMap : ClassMap<Item>
    {
        public ItemMap()
        {
            Table("Items");

            Id(x => x.Id);
            Map(x => x.Message);
        }
    }
}