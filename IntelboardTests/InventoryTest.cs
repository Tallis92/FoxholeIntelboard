﻿using Moq;
using IntelboardCore.DTO;
using IntelboardCore.Models;
using Xunit;
using IntelboardCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelboardCore.DAL.Interfaces;

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
