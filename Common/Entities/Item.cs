using System;

namespace Common.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public static Item Create => new Item {Message = Guid.NewGuid().ToString("N")};
    }
}