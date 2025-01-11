using NewsItems.Model;

namespace NewsItems.Data
{
    public interface INewsMessageRepository
    {
        public NewsItem Get(int id);
        public List<NewsItem> Get();
        public void Add(NewsItem item);
        public void Update(int id, NewsItem item);
        public void Delete(int id);
        public void Clear();
    }
}