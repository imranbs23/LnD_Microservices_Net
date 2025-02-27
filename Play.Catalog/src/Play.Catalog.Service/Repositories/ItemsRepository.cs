using System.Data.Common;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories{
    public class ItemsRepository{
        private const string _collectionName = "items";
        private readonly IMongoCollection<Item> _dbCollection;
        private readonly FilterDefinitionBuilder<Item> _filterBuilder = Builders<Item>.Filter;
        public ItemsRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Catalog");
            _dbCollection = database.GetCollection<Item>(_collectionName);            
        }

        public async Task<IReadOnlyCollection<Item>> GetAllAsync(){
            return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetAsync(Guid id){
            FilterDefinition<Item> filter = _filterBuilder.Eq(entity => entity.Id, id);
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item entity){
            if(entity == null)
            throw new ArgumentNullException(nameof(entity));

            await _dbCollection.InsertOneAsync(entity);
        }

          public async Task UpdateAsync(Item entity){
            if(entity == null)
            throw new ArgumentNullException(nameof(entity));

            FilterDefinition<Item> filter = _filterBuilder.Eq(exisitnfEntity => exisitnfEntity.Id, entity.Id);

            await _dbCollection.ReplaceOneAsync(filter,entity);
        }

        public async Task RemoveAsync(Guid id){
            FilterDefinition<Item> filter = _filterBuilder.Eq(entity => entity.Id, id);
            await _dbCollection.DeleteOneAsync(filter);
        }

    }
}