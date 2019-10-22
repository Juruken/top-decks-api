﻿using MongoDB.Driver;

namespace TopDecks.Api.Core.Extensions
{
    public static class MongoDatabaseExtensions
    {
        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database)
        {
            var entityName = typeof(T).Name.Pluralize().ToLowerCaseFirstLetter();
            return database.GetCollection<T>(entityName);
        }
    }
}
