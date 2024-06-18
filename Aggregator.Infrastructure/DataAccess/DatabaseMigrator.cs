using Aggregator.Domain.Companies;
using Aggregator.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Infrastructure.DataAccess;

public static class DatabaseMigrator
{
    public static void MigrateDatabase<TUnitOfWork>(this IServiceProvider serviceProvider) where TUnitOfWork : UnitOfWork
    {
        using var scope = serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<TUnitOfWork>();

        if (dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite") return;

        dbContext.Database.Migrate();
    }

    public static async void InitializeData<TUnitOfWork>(this IServiceProvider serviceProvider) where TUnitOfWork : UnitOfWork
    {
        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<TUnitOfWork>();

        unitOfWork.Database.EnsureCreated();

        var service = await unitOfWork.ServiceTypes.FirstOrDefaultAsync(x => x.Name == "Kitchen");

        if (service is not null) return;

        var kitchenService = await unitOfWork.ServiceTypes.AddAsync(ServiceTypeEntity.Create("Kitchen"));
        var deliveryService = await unitOfWork.ServiceTypes.AddAsync(ServiceTypeEntity.Create("Delivery"));
        var cleaningService = await unitOfWork.ServiceTypes.AddAsync(ServiceTypeEntity.Create("Cleaning"));

        await unitOfWork.SaveChangesAsync();

        var houseCleaning = await unitOfWork.Companies.AddAsync(CompanyEntity.Create("House Cleaning", "Let's clean up your shit", "https://comenian.org/wp-content/uploads/2023/04/istockphoto-1340208950-612x612-1.jpeg"));
        var deliveryCo2000 = await unitOfWork.Companies.AddAsync(CompanyEntity.Create("Delivery Co 2000"));
        var handyman = await unitOfWork.Companies.AddAsync(CompanyEntity.Create("Handyman", "Our hands are not for boredom", "https://media.istockphoto.com/id/1463132842/vector/wrench-in-hand-screwdriver-brush-repair-and-service-sign.jpg?s=612x612&w=0&k=20&c=RBWR7k6jh09E9UDXOqviT9hAaex4qmrqX-6gYPnEGbk="));

        await unitOfWork.SaveChangesAsync();

        _ = unitOfWork.Services.AddAsync(ServiceEntity.Create("Let's clean", "Let's clean up your problem", houseCleaning.Entity, cleaningService.Entity, new ServiceDataNode
        {
            Name = "Our services",
            Children =
            [
                new ServiceDataNode
                {
                    Name = "House cleaning",
                    Description = "Cool cleaning the house",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDCW7C-Qhp03bfw2bJuEYTNdUzrN8jACYtJQ&s",
                    Price = 125,
                },
                new ServiceDataNode
                {
                    Name = "Hotel cleaning",
                    Description = "Cool cleaning the hotel",
                    Price = 1250
                }
            ]
        }));

        await unitOfWork.SaveChangesAsync();

        _ = unitOfWork.Services.AddAsync(ServiceEntity.Create("Delivery", null, deliveryCo2000.Entity, deliveryService.Entity, new ServiceDataNode
        {
            Name = "Delivery services",
            Children =
            [
                new ServiceDataNode
                {
                    Name = "Delivery outside the city",
                    Description = "Fast, expensive",
                    Price = 100
                }
            ]
        }));

        await unitOfWork.SaveChangesAsync();

        _ = unitOfWork.Services.AddAsync(ServiceEntity.Create("Super Cleaning", null, handyman.Entity, cleaningService.Entity, new ServiceDataNode
        {
            Name = "Services",
            Children =
            [
                new ServiceDataNode
                {
                    Name = "Cleaning of industrial areas",
                    Description = "Expensive",
                    Price = 1000
                },
                new ServiceDataNode
                {
                    Name = "House cleaning",
                    Description = "Fast",
                    Price = 100
                }
            ]
        }));

        await unitOfWork.SaveChangesAsync();

        _ = unitOfWork.Services.AddAsync(ServiceEntity.Create("Super Delivery", null, handyman.Entity, deliveryService.Entity, new ServiceDataNode
        {
            Name = "Delivery",
            Children =
            [
                new ServiceDataNode
                {
                    Name = "Home delivery",
                    Price = 100,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRpQUCVJrIC0w18HMX8gQtrjFiu0LxvYGaHRA&s"
                }
            ]
        }));

        await unitOfWork.SaveChangesAsync();

        _ = unitOfWork.Services.AddAsync(ServiceEntity.Create("Super Kitchen", null, handyman.Entity, kitchenService.Entity, new ServiceDataNode
        {
            Name = "Menu",
            Children =
            [
                new ServiceDataNode
                {
                    Name = "Breakfast",
                    Description = "Tasty breakfast",
                    Children =
                    [
                        new ServiceDataNode
                        {
                            Name = "Coffee",
                            Price= 10,
                            ImageUrl = "https://img.freepik.com/free-vector/coffee-cup-tan-colour_78370-3051.jpg",
                            Description = "Awesome coffee"
                        },
                        new ServiceDataNode
                        {
                            Name = "Porridge",
                            Price= 15,
                            ImageUrl = "https://cdn.apartmenttherapy.info/image/upload/f_jpg,q_auto:eco,c_fill,g_auto,w_1500,ar_1:1/k%2FPhoto%2FRecipe%20Ramp%20Up%2F2022-04-Porridge%2FIMG_7918_R3",
                            Description = "Oatmeal porridge"
                        },
                    ]
                },
                new ServiceDataNode
                {
                    Name = "Lunch",
                    Description = "Hearty lunch",
                    Children =
                    [
                        new ServiceDataNode
                        {
                            Name = "Sausage",
                            Price= 8,
                            ImageUrl = "https://assets.epicurious.com/photos/5748afc15a5fbbae31ae4af4/1:1/w_2560%2Cc_limit/shutterstock_409001401.jpg",
                            Description = "Pork sausage"
                        },
                        new ServiceDataNode
                        {
                            Name = "Beef soup",
                            Price= 20,
                            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQaAP8Mur19EIpDcDHsQ6rfIyRY4q1prqpX6w&s",
                        },
                    ]
                },
                new ServiceDataNode
                {
                    Name = "Dinner",
                    Description = "Tasty dinner",
                    Children =
                    [
                        new ServiceDataNode
                        {
                            Name = "Fried chicken",
                            Price= 20,
                            ImageUrl = "https://www.andy-cooks.com/cdn/shop/articles/20230826032636-andy-20cooks-20-20korean-20fried-20chicken.jpg?v=1693088250",
                            Description = "Fried chicken with vegetables"
                        },
                        new ServiceDataNode
                        {
                            Name = "Boiled potatoes",
                            Price= 10,
                            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ3whXx8JZvTN3LO0uSsuQKo_sPA0hmjPxZQA&s",
                        },
                    ]
                }
            ]
        }));

        await unitOfWork.SaveChangesAsync();
    }
}