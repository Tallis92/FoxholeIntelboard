using FoxholeIntelboard.DAL;
using Moq;
using IntelboardAPI.DTO;
using IntelboardAPI.Models;
using Xunit;
using IntelboardAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelboardTests
{
    public class InventoryTest
    {
        [Fact]
        public async Task getInputItemAsync_ReturnsNull_WhenInputIsNull()
        {

            var manager = new InventoryManager(
                new HttpClient(),
                new Mock<IAmmunitionManager>().Object,
                new Mock<IWeaponManager>().Object,
                new Mock<IMaterialManager>().Object
            );
            var result = await manager.getInputItemAsync(null);
            Assert.Null(result);
        }

    }
}
