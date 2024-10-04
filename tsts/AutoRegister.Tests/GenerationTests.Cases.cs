namespace AutoRegister.Tests;

public partial class GenerationTests
{
   #region methods

   [Fact]
   public async Task TestCase1()
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

                           [RegisterScoped]
                           public class Bar { }

                           [RegisterTransient]
                           public class Baz { }

                           [RegisterSingleton]
                           public class Bang : IBaz { }

                           [TryRegisterScoped]
                           public class Far { }

                           [RegisterKeyedTransient(serviceKey: "MyFazKey")]
                           public class Faz { }

                           [TryRegisterKeyedSingleton(serviceKey: "MyFangKey")]
                           public class Fang : IBaz { }

                           public interface IBaz { }

                           """;

      const string expected = """
                              // <auto-generated>
                              //     Automatically generated by AutoRegister.
                              //     Changes made to this file may be lost and may cause undesirable behaviour.
                              // </auto-generated>
                              using Microsoft.Extensions.DependencyInjection;
                              using Microsoft.Extensions.DependencyInjection.Extensions;
                              public static class AutoRegisterServiceCollectionExtension
                              {
                                  public static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegisterFromTestProject(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
                                  {
                                      return AutoRegister(serviceCollection);
                                  }
                              
                                  internal static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegister(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
                                  {
                                      serviceCollection.AddHostedService<Foo>();
                              serviceCollection.AddScoped<Bar>();
                              serviceCollection.AddTransient<Baz>();
                              serviceCollection.AddSingleton<IBaz, Bang>();
                              serviceCollection.TryAddScoped<Far>();
                              serviceCollection.AddKeyedTransient<Faz>("MyFazKey");
                              serviceCollection.TryAddKeyedSingleton<IBaz, Fang>("MyFangKey");
                                      return serviceCollection;
                                  }
                              }
                              """;

      await RunGeneratorAsync(input, expected);
   }

   #endregion
}