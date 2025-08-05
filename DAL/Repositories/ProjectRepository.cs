using DAL.Models;
using DAL.Repositories.Interfaces;
using MongoDB.Driver;

namespace DAL.Repositories;

public class ProjectRepository(IMongoDatabase database) : IProjectRepository
{
    private readonly IMongoCollection<ProjectDocument> _collection = database
        .GetCollection<ProjectDocument>("projects");

    public async Task<IEnumerable<ProjectDocument>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<ProjectDocument> GetByIdAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<ProjectDocument>> GetByUserIdsAsync(IEnumerable<int> userIds)
    {
        var filter = Builders<ProjectDocument>.Filter.In(p => p.UserId, userIds);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<ProjectDocument> CreateAsync(ProjectDocument project)
    {
        await _collection.InsertOneAsync(project);
        return project;
    }

    public async Task<ProjectDocument> UpdateAsync(ProjectDocument project)
    {
        var result = await _collection.ReplaceOneAsync(x => x.Id == project.Id, project);
        return result.IsAcknowledged && result.ModifiedCount > 0 ? project : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}