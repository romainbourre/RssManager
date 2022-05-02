using System;
using System.Collections.Generic;
using RssManager.Application.Interfaces;


namespace RssManager.Application.Tests.Providers;

public class DeterministIdGenerator : IIdGenerator
{
    private readonly  LinkedList<Guid> ids = new();

    public void SetNextId(Guid nextId)
    {
        this.ids.AddLast(nextId);
    }
    public Guid GetNext()
    {
        Guid? id = this.ids.First?.Value;
        
        if (id == null)
            return Guid.NewGuid();
        
        this.ids.RemoveFirst();
        return id.Value;
    }
}
