using Cars.Core.Dto;
using Cars.Core.ServiceInterface;
using Cars.Core.Domain;
using Microsoft.Extensions.DependencyInjection;
using Cars.Data;
using Cars.Models.Car;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Car.Test
{
    public class CarsTest: TestBase
    {
        [Fact]
        public async Task Should_UpdateCar_WhenNotUpdateData()
        {
            CarDto dto = MockCarsData();
            await Svc<ICarServices>().Create(dto);

            CarDto nullUpdate = MockNullCar();
            await Svc<ICarServices>().Update(nullUpdate);

            var nullId = nullUpdate.Id;
            Assert.True(dto.Id == nullId);
        }

        [Fact]
        public async Task ShouldNot_DeleteById_WhenDidNotDeleteCar()
        {
            CarDto car = MockUpdateCarsData();
            var car1 = await Svc<ICarServices>().Create(car);
            var car2 = await Svc<ICarServices>().Create(car);
            var result = await Svc<ICarServices>().DeletePrimaryData((Guid)car2.Id);
            Assert.NotEqual(result.Id, car1.Id);
        }



        private CarDto MockCarsData()
        {
            CarDto car = new()
            {
                CarMake = "Axes",
                Year = 2000,
                CarColor = "Yellow",
                CreatedAt = DateTime.Now,
                Modifieted = DateTime.Now,
            };
            return car;
        }
        private CarDto MockUpdateCarsData()
        {
            CarDto car = new CarDto()
            {
                CarMake = "Carriage",
                Year = 1999,
                CarColor = "Green",
                CreatedAt = DateTime.Now,
                Modifieted = DateTime.Now,
            };
            return car;
        }

        private CarDto MockNullCar()
        {
            CarDto nullDto = new()
            {
                Id = Guid.Empty,
                CarMake = "Name123",
                Year = 2000,
                CarColor = "Black",
                CreatedAt = DateTime.Now.AddYears(1),
                Modifieted = DateTime.Now.AddYears(1),
            };
            return nullDto;
        }


    }
}
