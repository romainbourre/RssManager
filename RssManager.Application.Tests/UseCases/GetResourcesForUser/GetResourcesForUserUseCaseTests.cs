using System;
using System.Collections.Generic;
using RssManager.Adapters.Persistence.InMemoryPersistence.Repositories;
using RssManager.Application.Exceptions;
using RssManager.Application.Tests.Assertions;
using RssManager.Application.Tests.Givens.Repositories;
using RssManager.Application.UseCases.GetResourcesForUser;
using Xunit;
using static RssManager.Application.Tests.Givens.Requests.GetResourceForUserRequestBuilder;


namespace RssManager.Application.Tests.UseCases.GetResourcesForUser;

public class GetResourcesForUserUseCaseTests
{
    private readonly InMemoryUserRepository userRepository = new();
    private readonly InMemoryResourceRepository resourceRepository = new();
    private readonly GetResourcesForUserUseCase useCase;

    protected GetResourcesForUserUseCaseTests()
    {
        this.useCase = new GetResourcesForUserUseCase(this.userRepository, this.resourceRepository);
    }

    public class GivenExistentUser : GetResourcesForUserUseCaseTests
    {
        public class AndNoResources : GivenExistentUser
        {
            [Theory]
            [InlineData("850c2e25-f3ce-4019-848b-4e0e04a8c5b9", "Romain", "Bourré", "https://romainbourre.fr", "Blog de Romain Bourré")]
            [InlineData("61a5dd12-5ff0-4d5b-9b1e-1e96eaa32106", "Pauline", "Garnier", "https://paulinegarnier.fr", "Blog de Pauline Garnier")]
            public async void ShouldReturnUserProfileAndEmptyListOfResources(
                string id,
                string firstname,
                string lastname,
                string website,
                string description
            )
            {
                Guid userId = Guid.Parse(id);

                this.userRepository
                    .AlreadyHasUser(userId, firstname, lastname, website, description);

                GetResourcesForUserRequest request = GivenGetProfileAndResourcesRequest()
                    .WithUserId(userId)
                    .Build();

               await this.useCase.AssertThatForRequest(request)
                   .ReturnResponse(new GetResourcesForUserResponse(
                        new GetResourcesForUserResponse.UserProfile(
                            $"{firstname} {lastname}",
                            website,
                            description),
                        Array.Empty<GetResourcesForUserResponse.UserResource>())
                    );
            }
        }

        public class WithResources : GivenExistentUser
        {
            [Fact]
            public async void ShouldReturnUserProfileListOfResources()
            {
                var userId = Guid.NewGuid();
                const string firstname = "Romain";
                const string lastname = "Bourré";
                const string website = "https://romainbourre.fr";
                const string userDescription = "Blog de Romain Bourré";

                const string title = "Le titre de mon article";
                const string url = "https://romainbourre.fr/mon-article";
                const string description = "La description de mon article";

                this.userRepository
                    .AlreadyHasUser(userId, firstname, lastname, website, userDescription);

                this.resourceRepository
                    .AlreadyHasResource(userId, title, url, description);

                GetResourcesForUserRequest request = GivenGetProfileAndResourcesRequest()
                    .WithUserId(userId)
                    .Build();

                await this.useCase.AssertThatForRequest(request)
                    .ReturnResponse(new GetResourcesForUserResponse(
                        new GetResourcesForUserResponse.UserProfile(
                            $"{firstname} {lastname}",
                            website,
                            userDescription),
                        new List<GetResourcesForUserResponse.UserResource>()
                        {
                            new(
                                title,
                                url,
                                description
                            )
                        })
                    );
            }
            
            [Fact]
            public async void ShouldReturnUserProfileListOfResourcesOfUser()
            {
                var userId = Guid.NewGuid();
                const string firstname = "Romain";
                const string lastname = "Bourré";
                const string website = "https://romainbourre.fr";
                const string userDescription = "Blog de Romain Bourré";

                const string title = "Le titre de mon article";
                const string url = "https://romainbourre.fr/mon-article";
                const string description = "La description de mon article";

                this.userRepository
                    .AlreadyHasUser(userId, firstname, lastname, website, userDescription);

                this.resourceRepository
                    .AlreadyHasResource(userId, title, url, description)
                    .AlreadyHasResource(Guid.NewGuid(), "An article of another user", "https://another-user.fr/article", "Another user's article");

                GetResourcesForUserRequest request = GivenGetProfileAndResourcesRequest()
                    .WithUserId(userId)
                    .Build();
                
                await this.useCase.AssertThatForRequest(request)
                    .ReturnResponse(new GetResourcesForUserResponse(
                        new GetResourcesForUserResponse.UserProfile(
                            $"{firstname} {lastname}",
                            website,
                            userDescription),
                        new List<GetResourcesForUserResponse.UserResource>()
                        {
                            new(
                                title,
                                url,
                                description
                            ),
                        })
                    );
            }
        }
    }

    public class GivenUnknownUser : GetResourcesForUserUseCaseTests
    {
        [Fact]
        public async void ShouldPreventThatUserIsNotFound()
        {
            var userId = Guid.NewGuid();

            GetResourcesForUserRequest request = GivenGetProfileAndResourcesRequest()
                .WithUserId(userId)
                .Build();
            
            await this.useCase.AssertThatForRequest(request)
                .ThrowException<UserNotFoundException>($"user with id {userId} is not found");
        }
    }
}
