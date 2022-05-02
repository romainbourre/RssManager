using System;
using RssManager.Application.Tests.Providers;


namespace RssManager.Application.Tests.Givens.Providers;

public static class IdGeneratorGiven
{
    
    public static DeterministIdGenerator WillGenerate(this DeterministIdGenerator idGenerator, Guid nextId)
    {
        idGenerator.SetNextId(nextId);
        return idGenerator;
    }
}
