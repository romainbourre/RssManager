using System;
using System.Globalization;
using RssManager.Adapters.Persistence.InMemoryPersistence.Repositories;
using RssManager.Application.Exceptions;
using RssManager.Application.Tests.Assertions;
using RssManager.Application.Tests.Gateways;
using RssManager.Application.Tests.Givens.Gateways;
using RssManager.Application.Tests.Givens.Providers;
using RssManager.Application.Tests.Providers;
using RssManager.Application.UseCases.AddResourceForConnectedUser;
using RssManager.Domain.Entities;
using RssManager.Domain.Exceptions;
using RssManager.Domain.ValueObjects;
using Xunit;
using static RssManager.Application.Tests.Givens.Requests.AddResourceForConnectedUserBuilder;

namespace RssManager.Application.Tests.UseCases.AddResourceForConnectedUser;

public class AddResourceForConnectedUserUseCaseTests
{
    private readonly InMemoryResourceRepository resourceRepository = new();
    private readonly DeterministIdGenerator deterministIdGenerator = new();
    private readonly DeterministAuthenticationGateway deterministAuthenticationGateway = new();
    private readonly DeterministDateTimeProvider deterministDateTimeProvider = new();
    private readonly AddResourceForConnectedUserUseCase useCase;

    protected AddResourceForConnectedUserUseCaseTests()
    {
        this.useCase = new AddResourceForConnectedUserUseCase(this.resourceRepository, this.deterministAuthenticationGateway, this.deterministIdGenerator, this.deterministDateTimeProvider);
    }

    public class WithConnectedUser : AddResourceForConnectedUserUseCaseTests
    {
        public class WithCorrectData : WithConnectedUser
        {
            [Theory]
            [InlineData("37ff3eb0-383d-4a8f-a1b4-ea73a898cf86", "809fa608-05e5-4030-b99b-41d5fb2573f8", "22/05/2022",
                "An article to add", "https://new-article.fr", "The description of new article")]
            [InlineData("92561287-1d79-4f27-a0c3-ae51e67083ee", "c0678dd8-6d91-4217-9970-cece11d1efbd", "23/06/2023",
                "An another article to add", "https://another-article.fr", "The description of another article")]
            public async void ShouldBeSaveNewResource
            (
                string expectedResourceId,
                string connectedUserId,
                string expectedResourceDate,
                string resourceTitle,
                string resourceUrl,
                string resourceDescription
            )
            {
                Guid expectedNewResourceId = Guid.Parse(expectedResourceId);
                Guid connectedUserGuid = Guid.Parse(connectedUserId);
                DateTime expectedCreatedDate = DateTime.ParseExact(expectedResourceDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                this.deterministIdGenerator
                    .WillGenerate(expectedNewResourceId);

                this.deterministDateTimeProvider
                    .WillReturn(expectedCreatedDate);

                this.deterministAuthenticationGateway
                    .HaveConnectedUser(connectedUserGuid);

                AddResourceForConnectedUserRequest request = GivenAddResourceRequest()
                        .WithTitle(resourceTitle)
                        .WithUrl(resourceUrl)
                        .WithDescription(resourceDescription)
                        .Build();

                await this.useCase.Handle(request);

                this.resourceRepository
                    .ShouldContainsResource(new Resource(
                        expectedNewResourceId,
                        connectedUserGuid,
                        Title.Of(request.ResourceTitle),
                        Url.Of(request.ResourceUrl),
                        request.ResourceDescription,
                        expectedCreatedDate
                    ));
            }

            [Theory]
            [InlineData("37ff3eb0-383d-4a8f-a1b4-ea73a898cf86", "809fa608-05e5-4030-b99b-41d5fb2573f8", "22/05/2022",
                "An article to add", "https://new-article.fr", "The description of new article")]
            [InlineData("92561287-1d79-4f27-a0c3-ae51e67083ee", "c0678dd8-6d91-4217-9970-cece11d1efbd", "23/06/2023",
                "An another article to add", "https://another-article.fr", "The description of another article")]
            public async void ShouldBeReturnSavedResource
            (
                string expectedResourceId,
                string connectedUserId,
                string expectedResourceDate,
                string resourceTitle,
                string resourceUrl,
                string resourceDescription
            )
            {
                Guid expectedNewResourceId = Guid.Parse(expectedResourceId);
                Guid connectedUserGuid = Guid.Parse(connectedUserId);
                DateTime expectedCreatedDate = DateTime.ParseExact(expectedResourceDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                this.deterministIdGenerator
                    .WillGenerate(expectedNewResourceId);

                this.deterministDateTimeProvider
                    .WillReturn(expectedCreatedDate);

                this.deterministAuthenticationGateway
                    .HaveConnectedUser(connectedUserGuid);

                AddResourceForConnectedUserRequest request = GivenAddResourceRequest()
                        .WithTitle(resourceTitle)
                        .WithUrl(resourceUrl)
                        .WithDescription(resourceDescription)
                        .Build();

                await this.useCase.AssertThatForRequest(request)
                    .ReturnResponse(new AddResourceForConnectedUserResponse(
                        expectedNewResourceId,
                        resourceTitle,
                        resourceUrl
                    ));
            }
        }

        public class WithIncorrectData : AddResourceForConnectedUserUseCaseTests
        {
            public class WithEmptyTitle : WithIncorrectData
            {
                [Fact]
                public async void ShouldBePreventThatTitleIsIncorrect()
                {
                    this.deterministAuthenticationGateway
                        .HaveConnectedUser(Guid.NewGuid());

                    AddResourceForConnectedUserRequest request = GivenAddResourceRequest()
                            .WithDefaultProperties()
                            .WithTitle(string.Empty)
                            .Build();

                    await this.useCase.AssertThatForRequest(request)
                        .ThrowException<IncorrectResourceTitleException>("resource cannot have empty title");
                }
            }

            public class WithWhiteSpaceTitle : WithIncorrectData
            {
                [Fact]
                public async void ShouldBePreventThatTitleIsIncorrect()
                {
                    this.deterministAuthenticationGateway
                        .HaveConnectedUser(Guid.NewGuid());

                    AddResourceForConnectedUserRequest request = GivenAddResourceRequest()
                            .WithDefaultProperties()
                            .WithTitle(" ")
                            .Build();

                    await this.useCase.AssertThatForRequest(request)
                        .ThrowException<IncorrectResourceTitleException>("resource cannot have empty title");
                }
            }

            public class WithBadUrl : WithIncorrectData
            {
                [Fact]
                public async void ShouldBePreventThatUriIsIncorrect()
                {
                    this.deterministAuthenticationGateway
                        .HaveConnectedUser(Guid.NewGuid());

                    AddResourceForConnectedUserRequest request = GivenAddResourceRequest()
                            .WithDefaultProperties()
                            .WithUrl("badurl")
                            .Build();

                    await this.useCase.AssertThatForRequest(request)
                        .ThrowException<IncorrectResourceUrlException>("url of resource is incorrect");
                }
            }
        }
    }

    public class WithoutConnectedUser : AddResourceForConnectedUserUseCaseTests
    {
        [Fact]
        public async void ShouldPreventThatUserIsNotConnected()
        {
            AddResourceForConnectedUserRequest request = GivenAddResourceRequest()
                .WithDefaultProperties()
                .Build();

            await this.useCase.AssertThatForRequest(request)
                .ThrowException<AnyUserConnectedException>("any user is connected");
        }
    }
}
