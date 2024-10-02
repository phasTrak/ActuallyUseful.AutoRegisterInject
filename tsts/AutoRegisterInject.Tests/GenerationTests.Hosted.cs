namespace AutoRegisterInject.Tests;

public partial class GenerationTests
{
   #region methods

   [Fact]
   public async Task ShouldRegisterHosted()
   {
      const string input = """

                           using Microsoft.Extensions.Hosting;
                           using System.Threading;
                           using System.Threading.Tasks;
                           [RegisterHostedService]
                           public class Foo : IHostedService 
                           {
                               public Task StartAsync(CancellationToken stoppingToken) => throw new System.NotImplementedException();
                               public Task StopAsync(CancellationToken stoppingToken) => throw new System.NotImplementedException();
                           }
                           """;

      const string expected = """
                              // <auto-generated>
                              //     Automatically generated by AutoRegisterInject.
                              //     Changes made to this file may be lost and may cause undesirable behaviour.
                              // </auto-generated>
                              using Microsoft.Extensions.DependencyInjection;
                              using Microsoft.Extensions.DependencyInjection.Extensions;
                              public static class AutoRegisterInjectServiceCollectionExtension
                              {
                                  public static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegisterFromTestProject(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
                                  {
                                      return AutoRegister(serviceCollection);
                                  }
                              
                                  internal static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegister(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
                                  {
                                      serviceCollection.AddHostedService<Foo>();
                                      return serviceCollection;
                                  }
                              }
                              """;

      await RunGeneratorAsync(input, expected);
   }

   [Fact]
   public async Task ShouldRegisterHostedWithBaseClass()
   {
      const string input = """

                           using Microsoft.Extensions.Hosting;
                           using System.Threading;
                           using System.Threading.Tasks;
                           [RegisterHostedService]
                           public class Foo : BackgroundService
                           {
                               protected override async Task ExecuteAsync(CancellationToken stoppingToken) => throw new System.NotImplementedException();
                           }

                           """;

      const string expected = """
                              // <auto-generated>
                              //     Automatically generated by AutoRegisterInject.
                              //     Changes made to this file may be lost and may cause undesirable behaviour.
                              // </auto-generated>
                              using Microsoft.Extensions.DependencyInjection;
                              using Microsoft.Extensions.DependencyInjection.Extensions;
                              public static class AutoRegisterInjectServiceCollectionExtension
                              {
                                  public static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegisterFromTestProject(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
                                  {
                                      return AutoRegister(serviceCollection);
                                  }
                              
                                  internal static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegister(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
                                  {
                                      serviceCollection.AddHostedService<Foo>();
                                      return serviceCollection;
                                  }
                              }
                              """;

      await RunGeneratorAsync(input, expected);
   }

   [Fact]
   public async Task ShouldRegisterHostedWithInterfaceAndIgnoreIt()
   {
      const string input = """

                           using Microsoft.Extensions.Hosting;
                           using System.Threading;
                           using System.Threading.Tasks;
                           [RegisterHostedService]
                           public class Foo : IHostedService, IFoo
                           {
                               public Task StartAsync(CancellationToken stoppingToken) => throw new System.NotImplementedException();
                               public Task StopAsync(CancellationToken stoppingToken) => throw new System.NotImplementedException();
                           }
                           public interface IFoo { }

                           """;

      const string expected = """
                              // <auto-generated>
                              //     Automatically generated by AutoRegisterInject.
                              //     Changes made to this file may be lost and may cause undesirable behaviour.
                              // </auto-generated>
                              using Microsoft.Extensions.DependencyInjection;
                              using Microsoft.Extensions.DependencyInjection.Extensions;
                              public static class AutoRegisterInjectServiceCollectionExtension
                              {
                                  public static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegisterFromTestProject(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
                                  {
                                      return AutoRegister(serviceCollection);
                                  }
                              
                                  internal static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegister(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
                                  {
                                      serviceCollection.AddHostedService<Foo>();
                                      return serviceCollection;
                                  }
                              }
                              """;

      await RunGeneratorAsync(input, expected);
   }

   #endregion
}