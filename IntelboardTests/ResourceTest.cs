using IntelboardCore.Models;
using IntelboardCore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using IntelboardCore.DAL.Interfaces;

namespace IntelboardTests
{
    public class ResourceTest
    {
        [Fact]
        public async Task CanCallEditResourceAsync_ReturnCorrectly()
        {
            // Arrange
            var mockManager = new Mock<IResourceManager>();
            var resource = new Resource() {Id = 1, Name = "Salvage"};
             

            mockManager.Setup(r => r.EditResourceAsync(resource)).Returns(Task.CompletedTask);

            // Act
            await mockManager.Object.EditResourceAsync(resource);

            // Assert
            mockManager.Verify(m => m.EditResourceAsync(resource), Times.Once);
        }
    }

}
