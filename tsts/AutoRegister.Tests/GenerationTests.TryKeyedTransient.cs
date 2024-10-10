namespace AutoRegister.Tests;

public partial class GenerationTests
{
   #region methods

   [Fact]
   public Task ShouldTryRegisterKeyedTransient() =>
      """
      using AutoRegister;
      [TryRegisterKeyedTransient(serviceKey: "BazKey")]
      public class Foo { }
      """.VerifyAsync();

   [Fact]
   public Task ShouldTryRegisterKeyedTransientFromInterface() =>
      """
      using AutoRegister;
      [TryRegisterKeyedTransient(serviceKey: "BazKey")]
      public class Foo : IFoo { }
      public interface IFoo { }

      """.VerifyAsync();

   #endregion
}