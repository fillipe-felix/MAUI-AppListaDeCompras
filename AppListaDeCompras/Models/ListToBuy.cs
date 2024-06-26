﻿using MongoDB.Bson;

using Realms;

namespace AppListaDeCompras.Models;

public partial class ListToBuy : IRealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; }
    
    [MapTo("_anonymousUserId")]
    public ObjectId AnonymousUserId { get; set; }
    
    [MapTo("name")]
    public string Name { get; set; } = string.Empty;

    [MapTo("products")]
    public IList<Product> Products { get; }
    
    [MapTo("users")]
    public IList<User> Users { get; }
    
    [MapTo("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
}
