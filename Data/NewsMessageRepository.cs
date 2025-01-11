using System.Security.Cryptography;
using NewsItems.Exception;
using NewsItems.Model;

namespace NewsItems.Data
{
    public class NewsMessageRepository : INewsMessageRepository
    {
        private readonly Dictionary<int, NewsItem> items = [];

        public void Add(NewsItem item)
        {
            if (item.Id == null)
                throw new ExceptionInvalidParameters();

            if (items.ContainsKey(item.Id.Value))
                throw new ExceptionNewsItemExists(item.Id.Value.ToString());

            item.Id = item.Id.Value;
            items.Add(item.Id.Value, item);
        }

        public void Delete(int id)
        {
            if (!items.ContainsKey(id))
                throw new ExceptionNewsItemNotFound();
            items.Remove(id);
        }

        public List<NewsItem> Get()
        {
            return [..items.Values];
        }

        public NewsItem Get(int id)
        {
           return items.TryGetValue(id, out NewsItem? item) ? item : throw new ExceptionNewsItemNotFound(id.ToString());
        }

        public void Update(int id, NewsItem item)
        {
            if (item.Id == null || !id.Equals(item.Id.Value))
                throw new ExceptionInvalidParameters();

            if(!items.ContainsKey(id))
                throw new ExceptionNewsItemNotFound();

            items.Remove(id);
            items.Add(id, item);
        }

        public void Clear() { items.Clear(); }
    }
}
