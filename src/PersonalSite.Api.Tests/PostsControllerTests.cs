using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PersonalSite.Api.Controllers;
using PersonalSite.Api.Data;
using PersonalSite.Api.Models;
using Xunit;

namespace PersonalSite.Api.Tests
{
    public class PostsControllerTests
    {
        private DbContextOptions<BlogDbContext> _dbContextOptions;

        public PostsControllerTests()
        {
            // Use InMemory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test run
                .Options;
        }

        private BlogDbContext CreateContext() => new BlogDbContext(_dbContextOptions);

        private void SeedContext(BlogDbContext context)
        {
            context.Posts.AddRange(
                new Post { Id = 1, Title = "Test Post 1", Content = "Content 1", Author = "Author 1", PublishedDate = DateTime.UtcNow },
                new Post { Id = 2, Title = "Test Post 2", Content = "Content 2", Author = "Author 2", PublishedDate = DateTime.UtcNow.AddDays(-1) }
            );
            context.SaveChanges();
        }

        [Fact]
        public async Task GetPosts_ReturnsAllPosts()
        {
            // Arrange
            using var context = CreateContext();
            SeedContext(context);
            var controller = new PostsController(context);

            // Act
            var result = await controller.GetPosts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Post>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Post>>(actionResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetPost_ExistingId_ReturnsPost()
        {
            // Arrange
            using var context = CreateContext();
            SeedContext(context);
            var controller = new PostsController(context);
            var expectedPostId = 1;

            // Act
            var result = await controller.GetPost(expectedPostId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Post>>(result);
            var model = Assert.IsType<Post>(actionResult.Value);
            Assert.Equal(expectedPostId, model.Id);
            Assert.Equal("Test Post 1", model.Title);
        }

        [Fact]
        public async Task GetPost_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            using var context = CreateContext();
            // No seeding, or seed with different IDs
            var controller = new PostsController(context);
            var nonExistingId = 99;

            // Act
            var result = await controller.GetPost(nonExistingId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Post>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task CreatePost_ValidPost_ReturnsCreatedAtActionResult()
        {
            // Arrange
            using var context = CreateContext();
            var controller = new PostsController(context);
            var newPost = new Post { Title = "New Post", Content = "New Content", Author = "New Author", PublishedDate = DateTime.UtcNow };

            // Act
            var result = await controller.CreatePost(newPost);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Post>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(nameof(PostsController.GetPost), createdAtActionResult.ActionName);
            Assert.NotNull(createdAtActionResult.RouteValues["id"]);
            Assert.Equal(1, await context.Posts.CountAsync()); // Assuming ID is auto-generated starting from 1 by InMemory DB
            var createdPost = await context.Posts.SingleAsync();
            Assert.Equal("New Post", createdPost.Title);
        }

        [Fact]
        public async Task UpdatePost_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            using var context = CreateContext();
            SeedContext(context); // Seed with post ID 1
            var controller = new PostsController(context);
            // Detach the existing entity if it's already tracked from SeedContext
            var existingPost = context.Posts.Local.FirstOrDefault(p => p.Id == 1);
            if (existingPost != null)
            {
                context.Entry(existingPost).State = EntityState.Detached;
            }
            var updatedPost = new Post { Id = 1, Title = "Updated Title", Content = "Updated Content", Author = "Author 1", PublishedDate = DateTime.UtcNow };


            // Act
            var result = await controller.UpdatePost(1, updatedPost);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var postInDb = await context.Posts.FindAsync(1);
            Assert.Equal("Updated Title", postInDb.Title);
        }

        [Fact]
        public async Task UpdatePost_MismatchedId_ReturnsBadRequest()
        {
            // Arrange
            using var context = CreateContext();
            var controller = new PostsController(context);
            var postToUpdate = new Post { Id = 2, Title = "Mismatched" }; // Actual ID is 2

            // Act
            var result = await controller.UpdatePost(1, postToUpdate); // Calling with ID 1

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdatePost_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            using var context = CreateContext();
            var controller = new PostsController(context);
            var postToUpdate = new Post { Id = 99, Title = "Non Existent" };

            // Act
            var result = await controller.UpdatePost(99, postToUpdate);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task DeletePost_ExistingId_ReturnsNoContentAndRemovesPost()
        {
            // Arrange
            using var context = CreateContext();
            SeedContext(context); // Seed with post ID 1 and 2
            var controller = new PostsController(context);
            var postIdToDelete = 1;

            // Act
            var result = await controller.DeletePost(postIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Posts.FindAsync(postIdToDelete));
            Assert.Equal(1, await context.Posts.CountAsync()); // Only post 2 should remain
        }

        [Fact]
        public async Task DeletePost_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            using var context = CreateContext();
            var controller = new PostsController(context);
            var nonExistingId = 99;

            // Act
            var result = await controller.DeletePost(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
